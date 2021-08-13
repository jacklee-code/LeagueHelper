using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeagueHelper
{
    public partial class MainApp : Form
    {
        private LeagueExplorer leagueExplorer;

        private String statusFail = "請先啟動英雄聯盟客戶端";
        private String statusSuccess = "已啟動";
        private bool globalStatus = false;
        private Updater updater;

        private String[] laneNames = { "top", "mid", "ad", "jg", "sup" };

        public MainApp()
        {
            InitializeComponent();
            updater = new Updater();
        }

        private async void MainApp_Load(object sender, EventArgs e)
        {
            txt_version.Text = "目前版本：" + Application.ProductVersion;
            //Initialize Stuffs
            leagueExplorer = new LeagueExplorer();
            CheckUpdate(false);
            WriteChangelogInfo();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //Microsoft JhengHei UI
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Microsoft Sans Serif", 20.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void timer_checkProccess_Tick(object sender, EventArgs e)
        {
            timer_checkProccess.Stop();
            bool currentStatus = leagueExplorer.isProcessStart;
            if (currentStatus != globalStatus)
            {
                if (currentStatus)
                    client_OnStart();
                else
                    client_OnStop();
            }

            globalStatus = currentStatus;
            timer_checkProccess.Start();
        }

        private void client_OnStop()
        {
            //Stop all function timer
            timer_requestCycle.Stop();
            timer_autoAccept.Stop();
            timer_autoPickLane.Stop();
            timer_autoPickChamp.Stop();
            timer_monitorStatus.Stop();


            txt_processStatus.Text = statusFail;
            txt_processStatus.ForeColor = Color.Red;
            btn_refresh.Visible = false;

            //Basic Function
            toggle_autoAccept.Enabled = false;
            toggle_autoPickChamp.Enabled = false;
            listbox_selectChamp.DataSource = null;
            listbox_selectChamp.Items.Clear();
            toggle_autoPickLane.Enabled = false;
            toggle_autoPickRunes.Enabled = false;

            //Clean Data
            txt_summonerName.Text = "";
            txt_statusMessage.Text = "";
            txt_gameStatus.Text = "";
            txt_gameRegion.Text = "";
            txt_summonerLevel.Text = "";

        }

        private async void client_OnStart()
        {
            await RefreshAllSummonerData();

            txt_processStatus.Text = statusSuccess;
            txt_processStatus.ForeColor = Color.Green;
            btn_refresh.Visible = true;

            //Enable Basic Function
            toggle_autoAccept.Enabled = true;
            toggle_autoPickChamp.Enabled = true;
            toggle_autoPickLane.Enabled = true;
            toggle_autoPickRunes.Enabled = true;

            //Show Avaiable Champions
            await RefreshAvaiableChampionList();

            //Start timers
            timer_requestCycle.Start();
            timer_autoAccept.Enabled = toggle_autoAccept.Checked;
            timer_autoPickLane.Enabled = toggle_autoPickLane.Checked;

            //Properties Setting
            toggle_autoAccept.Checked = Properties.Settings.Default.autoAccept;
            toggle_autoPickLane.Checked = Properties.Settings.Default.autoPickLane;
            toggle_autoPickChamp.Checked = Properties.Settings.Default.autoPickChamp;
            txt_pickLaneFrequceny.Text = Properties.Settings.Default.pickLaneFreq.ToString();
            toggle_autoPickRunes.Checked = Properties.Settings.Default.autoRunePage;

            String lanes = Properties.Settings.Default.selectedLanes;
            if (lanes.Length > 0)
            {
                foreach (string index in lanes.Split(';'))
                    checkList_selectLane.SetItemCheckState(int.Parse(index), CheckState.Checked);
            }

            if (listbox_selectChamp.Items.Count > 0)
                listbox_selectChamp.SelectedValue = Properties.Settings.Default.champId;
        }

        private async void btn_refresh_Click(object sender, EventArgs e)
        {
            timer_requestCycle.Stop();
            await RefreshAllSummonerData();
            await RefreshAvaiableChampionList();
            timer_requestCycle.Start();
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        public async void CheckUpdate(bool showMsgIfLatestVersion = true)
        {
            Version version = await updater.CheckUpdate(Application.ProductVersion);
            if (version is null)
            {
                if (showMsgIfLatestVersion)
                    MessageBox.Show("你目前已經是最新版本。");
            }
            else
            {
                if (MessageBox.Show($"最新版本：{version.versionNumber}\n更新日期：{version.date}\n\n累積新增功能：\n{version.changelog}\n是否要更新到最新版本？"
                    , $"已找到新版本", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    OpenUrl(version.url);
                }
            }
                
        }

        //cycle + refesh button
        private async Task RefreshAllSummonerData()
        {
            leagueExplorer.InitializePreloadData();
            bool success = await leagueExplorer.RefreshSummonerBasicDetail();

            if (success)
            {
                txt_summonerName.Text = leagueExplorer.Summoner.Name;
                txt_statusMessage.Text = leagueExplorer.Summoner.StatusMessage;
                txt_gameRegion.Text = leagueExplorer.Summoner.Region;
                txt_summonerLevel.Text = leagueExplorer.Summoner.Level.ToString();

                switch (leagueExplorer.Summoner.GameStatus)
                {
                    case Summoner.GAME_STATUS.PLAYING:
                        txt_gameStatus.Text = "遊玩中";
                        break;
                    case Summoner.GAME_STATUS.LOBBY:
                        txt_gameStatus.Text = "在線";
                        break;
                    case Summoner.GAME_STATUS.QUEUING:
                        txt_gameStatus.Text = "列隊等待中";
                        break;
                    case Summoner.GAME_STATUS.CREATE_ARAM:
                        txt_gameStatus.Text = "建立ARAM中";
                        break;
                    case Summoner.GAME_STATUS.CREATE_NORMAL:
                        txt_gameStatus.Text = "建立一般對戰中";
                        break;
                    case Summoner.GAME_STATUS.CREATE_CUSTOM:
                        txt_gameStatus.Text = "創建自訂遊戲";
                        break;
                    case Summoner.GAME_STATUS.SELECTING_CHAMP:
                        txt_gameStatus.Text = "選擇英雄中";
                        break;
                    case Summoner.GAME_STATUS.CREATE_PRACTICE:
                        txt_gameStatus.Text = "創建練習模式";
                        break;
                    default:
                        txt_gameStatus.Text = leagueExplorer.Summoner.GameStatus;
                        break;
                }
            }

#if DEBUG
            txt_debug1.Visible = true;
            txt_debug2.Visible = true;
            txt_debug1.Text = leagueExplorer.urlRoot;
            txt_debug2.Text = "Authorization: Basic " + leagueExplorer.password;
#endif
        }


        private async void timer_requestCycle_Tick(object sender, EventArgs e)
        {
            timer_requestCycle.Stop();
            await RefreshAllSummonerData();
            timer_requestCycle.Start();
        }

        private void toggle_AutoAccept_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle_autoAccept.Checked)
                timer_autoAccept.Start();
            else
                timer_autoAccept.Stop();
        }

        private async void timer_autoAccept_Tick(object sender, EventArgs e)
        {
            timer_autoAccept.Stop();
            bool result = await leagueExplorer.IsMatchFound();
            if (result)
                await leagueExplorer.AcceptMatch();
            timer_autoAccept.Start();
        }

        private void toggle_autopick_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle_autoPickChamp.Checked)
            {
                if (listbox_selectChamp.SelectedIndex < 0)
                {
                    MessageBox.Show("請先選擇英雄。");
                    toggle_autoPickChamp.Checked = false;
                    return;
                } else
                {
                    timer_autoPickChamp.Start();
                }
            }
                
            else
                timer_autoPickChamp.Stop();
        }

        private async void timer_autoPickChamp_Tick(object sender, EventArgs e)
        {
            timer_autoPickChamp.Stop();
            if (listbox_selectChamp.Items.Count > 0)
            {
                await leagueExplorer.PickChampion(listbox_selectChamp.SelectedValue.ToString());
                timer_autoPickChamp.Start();
            }
            else
            {
                toggle_autoPickChamp.Checked = false;
                MessageBox.Show("目前無法取得可用英雄，請刷新英雄列表");
            }
            
        }

        private void toggle_autoPickLane_CheckedChanged(object sender, EventArgs e)
        {
            if (!toggle_autoPickLane.Checked)
                timer_monitorStatus.Stop();
            timer_autoPickLane.Enabled = toggle_autoPickLane.Checked;
        }

        private async void timer_autoPickLane_Tick(object sender, EventArgs e)
        {
            int freq;
            bool boolResult = int.TryParse(txt_pickLaneFrequceny.Text, out freq);

            if (!boolResult || checkList_selectLane.CheckedItems.Count < 1)
                return;

            timer_autoPickLane.Stop();
                       
            //Start Process Lane Message
            String fullMessage = "";
            foreach (int index in checkList_selectLane.CheckedIndices)
            {
                fullMessage += $"{laneNames[index]}+";
            }
            bool finished = await leagueExplorer.SendMessageInSelectionMenu(fullMessage.Remove(fullMessage.Length-1), freq, false);
            if (finished)
                timer_monitorStatus.Start();
            else
                timer_autoPickLane.Start();
        }

        private void txt_pickLaneFrequceny_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow numerical value
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private async Task RefreshAvaiableChampionList()
        {
            try
            {
                bool success = await leagueExplorer.refreshAvaiableChampionList();
                if (success)
                {
                    listbox_selectChamp.DataSource = new BindingSource(leagueExplorer.Summoner.AvailableChampionsNameIDPair, null);
                    listbox_selectChamp.DisplayMember = "Value";
                    listbox_selectChamp.ValueMember = "Key";
                }
            }
            catch { } 
        }

        private async void btn_refreshAvaiableChamp_Click(object sender, EventArgs e)
        {
            await RefreshAvaiableChampionList();
        }

        private async void timer_monitorStatus_Tick(object sender, EventArgs e)
        {
            timer_monitorStatus.Stop();
            await RefreshAllSummonerData();
            if (leagueExplorer.Summoner.GameStatus != Summoner.GAME_STATUS.SELECTING_CHAMP)
                timer_autoPickLane.Start();
            else
                timer_monitorStatus.Start();
        }

        private async void WriteChangelogInfo()
        {
            List<Version> versions = await updater.GetVersions();
            if (versions.Count == 0)
                return;

            txt_changelog.Text = "";
            for (int i = versions.Count - 1; i >= 0; i--)
            {
                txt_changelog.Text += $"版本：{versions[i].versionNumber}\n更新日期：{versions[i].date}\n新增內容：\n{versions[i].changelog}\n\n";
            }
        }

        private void btn_CheckUpdate_Click(object sender, EventArgs e)
        {
            CheckUpdate();
            WriteChangelogInfo();
        }

        private void MainApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.autoAccept = toggle_autoAccept.Checked;
            Properties.Settings.Default.autoPickChamp = toggle_autoPickChamp.Checked;
            if (toggle_autoPickChamp.Checked)
                Properties.Settings.Default.champId = listbox_selectChamp.SelectedValue.ToString();
            Properties.Settings.Default.autoPickLane = toggle_autoPickLane.Checked;
            Properties.Settings.Default.selectedLanes = "";
            Properties.Settings.Default.autoRunePage = toggle_autoPickRunes.Checked;
            string temp = "";
            foreach (int index in checkList_selectLane.CheckedIndices)
            {
                temp += index + ";";
            }

            if (temp.Length > 0)
                Properties.Settings.Default.selectedLanes = temp.Remove(temp.Length - 1);
            Properties.Settings.Default.pickLaneFreq = int.Parse(txt_pickLaneFrequceny.Text);
            Properties.Settings.Default.Save();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            Activate();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void MainApp_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void toggle_autoPickRunes_CheckedChanged(object sender, EventArgs e)
        {
            timer_autoRunes.Enabled = toggle_autoPickRunes.Checked;
        }

        private async void timer_autoRunes_Tick(object sender, EventArgs e)
        {
            timer_autoRunes.Stop();
            bool result =  await leagueExplorer.CreateOPGGRunePage();
            Debug.WriteLine("DEBUG ::: " + result);
            timer_autoRunes.Start();
        }
    }
}
