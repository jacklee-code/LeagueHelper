using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace LeagueHelper
{
    public partial class MainApp : Form
    {
        private LeagueExplorer leagueExplorer;

        private String statusFail = "請先啟動英雄聯盟客戶端";
        private String statusSuccess = "已啟動";
        private bool globalStatus = false;

        private String[] laneNames = { "top", "mid", "ad", "jg", "sup" };

        public MainApp()
        {
            InitializeComponent();
        }

        private void MainApp_Load(object sender, EventArgs e)
        {
            //Initialize Stuffs
            leagueExplorer = new LeagueExplorer();
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
            bool currentStatus = Process.GetProcessesByName("LeagueClient").Length > 0;
            if (currentStatus != globalStatus)
            {
                if (currentStatus)
                    client_OnStart();
                else
                    client_OnStop();
            }

            globalStatus = currentStatus;
        }

        private void client_OnStop()
        {
            txt_processStatus.Text = statusFail;
            txt_processStatus.ForeColor = Color.Red;
            btn_refresh.Visible = false;

            //Basic Function
            toggle_autoAccept.Enabled = false;
            toggle_autoPickChamp.Enabled = false;
            listbox_selectChamp.Enabled = false;
            toggle_autoPickLane.Enabled = false;
            checkList_selectLane.Enabled = false;

            //Clean Data
            txt_summonerName.Text = "";
            txt_statusMessage.Text = "";

            timer_requestCycle.Stop();
        }

        private async void client_OnStart()
        {
            await refreshAllSummonerData();

            txt_processStatus.Text = statusSuccess;
            txt_processStatus.ForeColor = Color.Green;
            btn_refresh.Visible = true;

            //Basic Function
            toggle_autoAccept.Enabled = true;
            toggle_autoPickChamp.Enabled = true;
            listbox_selectChamp.Enabled = toggle_autoPickChamp.Checked;
            toggle_autoPickLane.Enabled = true;
            checkList_selectLane.Enabled = toggle_autoPickLane.Checked;

            timer_requestCycle.Start();
        }

        private async void btn_refresh_Click(object sender, EventArgs e)
        {
            timer_requestCycle.Stop();
            await refreshAllSummonerData();
            timer_requestCycle.Start();
        }

        //cycle + refesh button
        private async Task refreshAllSummonerData()
        {
            await leagueExplorer.initializeData();
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
                    txt_gameStatus.Text = "列隊中";
                    break;
                default:
                    txt_gameStatus.Text = leagueExplorer.Summoner.GameStatus;
                    break;
            }

            //DEBUG::
            txt_debug1.Text = leagueExplorer.urlRoot;
            txt_debug2.Text = "Authorization: Basic " + leagueExplorer.debugAuthorizaation;
            //DEBUG END ::
        }


        private async void timer_requestCycle_Tick(object sender, EventArgs e)
        {
            await refreshAllSummonerData();
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
            bool result = await leagueExplorer.isMatchFound();
            if (result)
                await leagueExplorer.acceptMatch();
        }

        private void toggle_autopick_CheckedChanged(object sender, EventArgs e)
        {
            if (toggle_autoPickChamp.Checked)
            {
                try
                {
                    timer_autoPickChamp.Start();
                    listbox_selectChamp.Enabled = true;
                    listbox_selectChamp.DataSource = new BindingSource(leagueExplorer.Summoner.AvailableChampionsNameIDPair, null);
                    listbox_selectChamp.DisplayMember = "Value";
                    listbox_selectChamp.ValueMember = "Key";
                }
                catch { }

            }
            else
            {
                listbox_selectChamp.Enabled = false;
                timer_autoPickChamp.Stop();
            }
        }

        private async void timer_autoPickChamp_Tick(object sender, EventArgs e)
        {

            try
            {
                if (listbox_selectChamp.Items.Count < 1)
                {
                    listbox_selectChamp.DataSource = new BindingSource(leagueExplorer.Summoner.AvailableChampionsNameIDPair, null);
                    listbox_selectChamp.DisplayMember = "Value";
                    listbox_selectChamp.ValueMember = "Key";
                }
                await leagueExplorer.pickChampion(listbox_selectChamp.SelectedValue.ToString());
            }
            catch { }
        }

        private void toggle_autoPickLane_CheckedChanged(object sender, EventArgs e)
        {
            checkList_selectLane.Enabled = toggle_autoPickLane.Checked;
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
            bool finished = await leagueExplorer.sendMessageInSelectionMenu(fullMessage.Remove(fullMessage.Length-1), freq);
            if (finished)
                toggle_autoPickLane.Checked = false;
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
    }
}
