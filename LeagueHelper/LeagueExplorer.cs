using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LeagueHelper
{
    public class LeagueExplorer
    {
        private HttpClient httpClient;

        public String debugAuthorizaation = "";

        public String urlRoot = "";
        public Summoner Summoner = new Summoner();

        public LockFile lockFile;

        public bool isProcessStart => Process.GetProcessesByName("LeagueClient").Length > 0;

        public Process LeagueClient => isProcessStart ? Process.GetProcessesByName("LeagueClient")[0] : null;

        public LeagueExplorer()
        {
            //allow untrusted certs
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            httpClient = new HttpClient(handler);
        }

        public async Task initializeData()
        {
            try
            {
                if (LeagueClient != null)
                {
                    String processName = getProcessFilename(LeagueClient);
                    lockFile = new LockFile(processName.Remove(processName.LastIndexOf('\\')) + "\\lockfile");
                    urlRoot = lockFile.protocal + "://" + "127.0.0.1:" + lockFile.portNumber;
                    byte[] passwordByte = System.Text.ASCIIEncoding.ASCII.GetBytes($"{lockFile.account}:{lockFile.password}");
                    String password = Convert.ToBase64String(passwordByte);
                    //DEBUG ::
                    debugAuthorizaation = password;
                    //DBUG END ::
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", password);
                    const String path = "/lol-chat/v1/me";
                    String jsonString = await httpClient.GetStringAsync(urlRoot + path);
                    dynamic classStructure = JsonConvert.DeserializeObject(jsonString);
                    getSummonerBasicDetail(classStructure);
                }
            }
            catch { }        
        }

        private void getSummonerBasicDetail(dynamic obj)
        {
            Summoner.SummonerID = obj.summonerId;
            Summoner.Name = obj.name;
            Summoner.StatusMessage = obj.statusMessage;
            Summoner.Region = obj.platformId;
            Summoner.Level = (int)obj.lol.level;
            Summoner.GameStatus = obj.lol.gameStatus;
        }

        private async Task updateGameStatus()
        {
            try
            {
                String jsonString = await httpClient.GetStringAsync(urlRoot + "/lol-chat/v1/me");
                dynamic classStructure = JsonConvert.DeserializeObject(jsonString);
                Summoner.GameStatus = classStructure.lol.gameStatus;
            }
            catch { }
            
        }

        //.............. public functions of different misc feature ...................
        public async Task<bool> isMatchFound()
        {
            try
            {
                String jsonResult = await httpClient.GetStringAsync(urlRoot + "/lol-matchmaking/v1/ready-check");
                const String foundMatchString = "InProgress";
                dynamic obj = JsonConvert.DeserializeObject(jsonResult);
                if (obj.state == foundMatchString)
                    return true;

            } catch { }
            return false;
        }

        public async Task acceptMatch()
        {
            try { var response = await httpClient.PostAsync(urlRoot + "/lol-matchmaking/v1/ready-check/accept", null); }             
            catch { }
        }

        public async Task refreshAvaiableChampionList()
        {
            dynamic avaiableChampObj = JsonConvert.DeserializeObject(await httpClient.GetStringAsync(urlRoot + "/lol-champions/v1/owned-champions-minimal"));           
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int count = avaiableChampObj.Count;
            for (int i = 0; i < count; i++)
            {
                String id = avaiableChampObj[i].id;
                String fullname = $"{avaiableChampObj[i].title} {avaiableChampObj[i].name}";
                dict.Add(id, fullname);
            }
            Summoner.AvailableChampionsNameIDPair = dict;
        }

        public async Task pickChampion(String championId)
        {
            try
            {
                dynamic objGameRoom = JsonConvert.DeserializeObject(await httpClient.GetStringAsync(urlRoot + "/lol-champ-select/v1/session"));
                int count = objGameRoom.myTeam.Count;
                int cellId = 0, actionId = 0;
                for (int i = 0; i < count; i++)
                    if (objGameRoom.myTeam[i].summonerId == Summoner.SummonerID)
                    {
                        cellId = objGameRoom.myTeam[i].cellId;
                        break;
                    }
                count = objGameRoom.actions[0].Count;

                for (int i = 0; i < count; i++)
                    if (objGameRoom.actions[0][i].actorCellId == cellId)
                        actionId = objGameRoom.actions[0][i].id;

                String jsonChampContent = $"{{\"championId\":\"{championId}\"}}";
                HttpContent content = new StringContent(jsonChampContent, Encoding.UTF8, "application/json");
                await httpClient.PatchAsync(urlRoot + $"/lol-champ-select/v1/session/actions/{actionId}", content);
            }
            catch { }
        }

        public async Task<bool> sendMessageInSelectionMenu(String message, int frequency = 1, bool isSystemMessage = false)
        {
            try
            {
                //get if player is in selection menu
                await updateGameStatus();
                if (Summoner.GameStatus != Summoner.GAME_STATUS.SELECTING_CHAMP)
                    return false;

                //get selection menu chat id
                dynamic objConversationData = JsonConvert.DeserializeObject(await httpClient.GetStringAsync(urlRoot + "/lol-chat/v1/conversations/"));
                int count = objConversationData.Count;
                String chatId = "";
                for (int i = 0; i < count; i++)
                {
                    String tempChatId = objConversationData[i].id;
                    if (tempChatId.Contains("@champ-select"))
                    {
                        chatId = tempChatId;
                        break;
                    }
                }

                if (chatId == "")
                    return false;
                    
                //Start to send message
                HttpContent messageContent = new StringContent($"{{\"body\":\"{message}\",\"type\":\"{(isSystemMessage? "celebration":"chat")}\"}}", Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                for (int i = 0; i < frequency; i++)
                {
                    response = await httpClient.PostAsync(urlRoot + "/lol-chat/v1/conversations/" + chatId + "/messages", messageContent);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        i--;
                }
            } catch { Debug.WriteLine("Some Error???"); return false; }
            Debug.WriteLine("YOU PASS ALL THINGS!!!");
            return true;
        }


        //Special trick for obtaining process file name without admin permission
        [Flags]
        private enum ProcessAccessFlags : uint
        {
            QueryLimitedInformation = 0x00001000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] int dwFlags, [Out] StringBuilder lpExeName, ref int lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        private String getProcessFilename(Process p)
        {
            int capacity = 2000;
            StringBuilder builder = new StringBuilder(capacity);
            IntPtr ptr = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, p.Id);
            if (!QueryFullProcessImageName(ptr, 0, builder, ref capacity))
            {
                return String.Empty;
            }

            return builder.ToString();
        }
        // End of Trick
    }

    public class LockFile
    {
        public String clientName = "LeagueClient";
        public String processID = "";
        public String portNumber = "";
        public String account = "riot";
        public String password = "";
        public String protocal = "https";

        public LockFile(String filename)
        {
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                String[] rawData = sr.ReadToEnd().Split(':');
                clientName = rawData[0];
                processID = rawData[1];
                portNumber = rawData[2];
                password = rawData[3];
                protocal = rawData[4];
            }
            
        }
    }

}
