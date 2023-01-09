using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Text.Encodings.Web;
using System.Security.Policy;

namespace LeagueHelper
{
    public class LeagueExplorer
    {
#if DEBUG
        public String password = "";
#else
        private String password = "";
#endif


        private HttpClient httpClient;

        public String urlRoot = "";
        
        public Summoner Summoner = new Summoner();

        public LockFile lockFile;

        public bool isProcessStart
        {
            get => Process.GetProcessesByName("LeagueClient").Length > 0;
        }

        public Process LeagueClient {
            get => isProcessStart ? Process.GetProcessesByName("LeagueClient")[0] : null;
        }

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

        public void InitializePreloadData()
        {
            try
            {
                if (LeagueClient != null)
                {
                    String processName = getProcessFilename(LeagueClient);
                    lockFile = new LockFile(processName.Remove(processName.LastIndexOf('\\')) + "\\lockfile");
                    urlRoot = lockFile.protocal + "://" + "127.0.0.1:" + lockFile.portNumber;
                    byte[] passwordByte = System.Text.ASCIIEncoding.ASCII.GetBytes($"{lockFile.account}:{lockFile.password}");
                    password = Convert.ToBase64String(passwordByte);
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", password);
                }
            }
            catch { }        
        }


        private async Task<bool> UpdateGameStatus()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(urlRoot + "/lol-chat/v1/me");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return false;

                dynamic classStructure = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                Summoner.GameStatus = classStructure.lol.gameStatus;
            }
            catch { return false; }

            return true;
            
        }

        //private async Task<bool> CreateRunePage(RunePage runePage)
        //{
        //    try
        //    {
        //        Console.WriteLine("Start Create RunePage");
        //        await DeleteRepeatedRunePages(runePage.name);
        //        HttpContent content = new StringContent(JsonConvert.SerializeObject(runePage), Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await httpClient.PostAsync(urlRoot + "/lol-perks/v1/pages", content);
        //        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //            return false;
        //        return true;
        //    }
        //    catch { return false; }
        //}

        private async Task<bool> DeleteRepeatedRunePages(String pageName = "英雄聯盟小助手")
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(urlRoot + "/lol-perks/v1/pages");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return false;


                List<RunePage> runePages = JsonConvert.DeserializeObject<List<RunePage>>(await response.Content.ReadAsStringAsync());
            
                foreach (RunePage rune in runePages)
                {
                    if (rune.name == pageName)
                        await httpClient.DeleteAsync(urlRoot + "/lol-perks/v1/pages/" + rune.id);
                        
                }
                return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex.ToString()); return false; }
        }

        //.............. public functions of different misc feature ...................

        public async Task<bool> RefreshSummonerBasicDetail()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(urlRoot + "/lol-chat/v1/me");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return false;

                dynamic obj = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

                //object control
                Summoner.SummonerID = obj.summonerId;
                Summoner.Name = obj.name;
                Summoner.StatusMessage = obj.statusMessage;
                Summoner.Region = obj.platformId;
                Summoner.Level = (int)obj.lol.level;
                Summoner.GameStatus = obj.lol.gameStatus;
            }
            catch { return false; }

            return true;
        }

        public async Task<bool> IsMatchFound()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(urlRoot + "/lol-matchmaking/v1/ready-check");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return false;

                String jsonResult = await response.Content.ReadAsStringAsync();
                const String foundMatchString = "InProgress";
                dynamic obj = JsonConvert.DeserializeObject(jsonResult);
                if (obj.state == foundMatchString)
                    return true;

            } catch { }
            return false;
        }

        public async Task<string> GetCurrentSelectedChampionName()
        {
            try
            {
                int championId = -1;
                HttpResponseMessage response = await httpClient.GetAsync(urlRoot + "/lol-champ-select/v1/session");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return "";

                dynamic obj = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                int count = obj.myTeam.Count;
                for (int i = 0; i < count; i++)
                {
                    if (obj.myTeam[i].summonerId == Summoner.SummonerID)
                    {
                        championId = obj.myTeam[i].championId;
                        break;
                    }

                }
                if (championId == -1)
                    return "";

                response = await httpClient.GetAsync(urlRoot + $"/lol-champions/v1/inventories/{Summoner.SummonerID}/champions/{championId}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return "";


                dynamic champDetail = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

                //Debug.WriteLine(alias);

                Summoner.LastSelectedChampion = champDetail.alias;

                return Summoner.LastSelectedChampion;
            }
            catch { return ""; }
        }

        public async Task<bool> OpenOPGGPage()
        {
            String lastChamp = Summoner.LastSelectedChampion;
            String currChamp = await GetCurrentSelectedChampionName();
            if (currChamp.Length == 0 || lastChamp == currChamp)
                return false;

            String opggUrl = "https://" + "www.op.gg/champion/" + currChamp + "/statistics";
            Process.Start(new ProcessStartInfo() { FileName = opggUrl, UseShellExecute = true });
            return true;
        }

        //public async Task<bool> CreateOPGGRunePage(String region = "www", bool isOpenUrl = false, Action<string> openUrlFunc = null)
        //{
        //    String lastChamp = Summoner.LastSelectedChampion;
        //    String currChamp = await GetCurrentSelectedChampionName();
        //    if (currChamp.Length == 0 || lastChamp == currChamp)
        //        return false;

            

            

        //    //Do...
        //    String opggUrl = "https://" + region + ".op.gg/champion/" + currChamp + "/statistics";

        //    if (!(openUrlFunc is null) && isOpenUrl)
        //        openUrlFunc(opggUrl);

        //    HttpResponseMessage response = await httpClient.GetAsync(opggUrl);
        //    if (!response.IsSuccessStatusCode)
        //    {
                
        //        return false;
        //    }
                

        //    try
        //    {            
        //        RunePage runepage = new RunePage();
        //        String htmlText = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine(htmlText);
        //        String perk_page_wrap = htmlText.Split("perk-page-wrap")[1];
        //        String primary_perk = perk_page_wrap.Split("perk-page__item--mark")[1].Split("perkStyle/")[1].Split(".png")[0];
        //        String sub_perk = perk_page_wrap.Split("perk-page__item--mark")[2].Split("perkStyle/")[1].Split(".png")[0];
        //        runepage.primaryStyleId = int.Parse(primary_perk);
        //        runepage.subStyleId = int.Parse(sub_perk);
        //        String[] perkids2 = perk_page_wrap.Split("perk-page__item--active");
        //        for (int i = 1; i < perkids2.Length; i++)
        //        {
        //            String perkid = perkids2[i].Split("perk/")[1].Split(".png")[0];
        //            runepage.selectedPerkIds.Add(int.Parse(perkid));
        //        }

        //        String[] fragments = perk_page_wrap.Split("fragment__row");
        //        for (int i = 1; i < fragments.Length; i++)
        //        {
        //            String[] tmp = fragments[i].Split("active")[0].Split("perkShard/");
        //            int shardid = int.Parse(tmp[tmp.Length - 1].Split(".png")[0]);
        //            runepage.selectedPerkIds.Add(shardid);
        //        }
        //        bool result = await CreateRunePage(runepage);
        //        return result;
        //    }
        //    catch { return false; }
        //}


        public async Task AcceptMatch()
        {
            try { var response = await httpClient.PostAsync(urlRoot + "/lol-matchmaking/v1/ready-check/accept", null); }             
            catch { }
        }

        public async Task<bool> refreshAvaiableChampionList()
        {
            HttpResponseMessage response = await httpClient.GetAsync(urlRoot + "/lol-champions/v1/owned-champions-minimal");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return false;

            try
            {
                dynamic avaiableChampObj = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                Dictionary<string, string> dict = new Dictionary<string, string>();
                int count = avaiableChampObj.Count;
                for (int i = 0; i < count; i++)
                {
                    String id = avaiableChampObj[i].id;
                    String fullname = /* avaiableChampObj[i].title + ' ' + */ avaiableChampObj[i].name;
                    dict.Add(id, fullname);
                }
                Summoner.AvailableChampionsNameIDPair = dict;
            } catch { return false; }

            return true;
        }

        private async Task<int> GetActionID()
        {
            int cellId = 0;
            int actionId = -1;
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(urlRoot + "/lol-champ-select/v1/session");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return -1;

                dynamic objGameRoom = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                int count = objGameRoom.myTeam.Count;                
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
                return actionId;
            }
            catch { return actionId; }
        }
        public async Task PickChampion(String championId, bool locked = false, bool ban = false)
        {
            try
            {
                int actionId = await GetActionID();
                if (actionId == -1)
                    return;


                String jsonChampContent = $"{{\"championId\":\"{championId}\", \"completed\":{locked.ToString().ToLower()}, " +
                                          $"\"type\":\"{(ban?"ban":"pick")}\"}}";
                HttpContent content = new StringContent(jsonChampContent, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await httpClient.PatchAsync(urlRoot + $"/lol-champ-select/v1/session/actions/{actionId}", content);
                
            }
            catch { }
        }

        public async Task<bool> SendMessageInSelectionMenu(String message, int frequency = 1, bool isSystemMessage = false)
        {
            try
            {
                Debug.WriteLine("Func called");
                bool success = await UpdateGameStatus();
                if (!success)
                    return false;
                    

                //get if player is in selection menu
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
                    {
                        Debug.WriteLine("call faield");
                        i--;
                    }
                        
                }
            } catch { return false; }
            Debug.WriteLine("success");
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
