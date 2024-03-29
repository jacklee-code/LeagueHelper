﻿
namespace LeagueHelper
{
    partial class MainApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainApp));
            this.label0 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.txt_debug2 = new System.Windows.Forms.TextBox();
            this.txt_debug1 = new System.Windows.Forms.TextBox();
            this.txt_gameStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_gameRegion = new System.Windows.Forms.Label();
            this.txt_summonerLevel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_statusMessage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_summonerName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabBasic = new System.Windows.Forms.TabPage();
            this.toggle_openOPGGUrl = new System.Windows.Forms.CheckBox();
            this.btn_refreshAvaiableChamp = new System.Windows.Forms.Button();
            this.txt_pickLaneFrequceny = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkList_selectLane = new System.Windows.Forms.CheckedListBox();
            this.toggle_autoPickLane = new System.Windows.Forms.CheckBox();
            this.listbox_selectChamp = new System.Windows.Forms.ComboBox();
            this.toggle_autoPickChamp = new System.Windows.Forms.CheckBox();
            this.toggle_autoAccept = new System.Windows.Forms.CheckBox();
            this.tabUpdate = new System.Windows.Forms.TabPage();
            this.txt_changelog = new System.Windows.Forms.RichTextBox();
            this.btn_CheckUpdate = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_processStatus = new System.Windows.Forms.Label();
            this.timer_checkProccess = new System.Windows.Forms.Timer(this.components);
            this.btn_refresh = new System.Windows.Forms.Button();
            this.tooltip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer_requestCycle = new System.Windows.Forms.Timer(this.components);
            this.timer_autoAccept = new System.Windows.Forms.Timer(this.components);
            this.timer_autoPickChamp = new System.Windows.Forms.Timer(this.components);
            this.timer_autoPickLane = new System.Windows.Forms.Timer(this.components);
            this.txt_version = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer_openUrl = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tabBasic.SuspendLayout();
            this.tabUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Font = new System.Drawing.Font("Microsoft JhengHei UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label0.Location = new System.Drawing.Point(12, 23);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(338, 41);
            this.label0.TabIndex = 0;
            this.label0.Text = "英雄聯盟客戶端狀態：";
            this.label0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabInfo);
            this.tabControl1.Controls.Add(this.tabBasic);
            this.tabControl1.Controls.Add(this.tabUpdate);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControl1.ItemSize = new System.Drawing.Size(25, 130);
            this.tabControl1.Location = new System.Drawing.Point(15, 79);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(919, 477);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.txt_debug2);
            this.tabInfo.Controls.Add(this.txt_debug1);
            this.tabInfo.Controls.Add(this.txt_gameStatus);
            this.tabInfo.Controls.Add(this.label5);
            this.tabInfo.Controls.Add(this.txt_gameRegion);
            this.tabInfo.Controls.Add(this.txt_summonerLevel);
            this.tabInfo.Controls.Add(this.label4);
            this.tabInfo.Controls.Add(this.label3);
            this.tabInfo.Controls.Add(this.txt_statusMessage);
            this.tabInfo.Controls.Add(this.label2);
            this.tabInfo.Controls.Add(this.txt_summonerName);
            this.tabInfo.Controls.Add(this.label1);
            this.tabInfo.Location = new System.Drawing.Point(134, 4);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Size = new System.Drawing.Size(781, 469);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.Text = "個人資料";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // txt_debug2
            // 
            this.txt_debug2.Location = new System.Drawing.Point(316, 419);
            this.txt_debug2.Name = "txt_debug2";
            this.txt_debug2.Size = new System.Drawing.Size(454, 38);
            this.txt_debug2.TabIndex = 9;
            this.txt_debug2.Visible = false;
            // 
            // txt_debug1
            // 
            this.txt_debug1.Location = new System.Drawing.Point(13, 419);
            this.txt_debug1.Name = "txt_debug1";
            this.txt_debug1.Size = new System.Drawing.Size(293, 38);
            this.txt_debug1.TabIndex = 9;
            this.txt_debug1.Visible = false;
            // 
            // txt_gameStatus
            // 
            this.txt_gameStatus.AutoSize = true;
            this.txt_gameStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_gameStatus.Location = new System.Drawing.Point(133, 91);
            this.txt_gameStatus.Name = "txt_gameStatus";
            this.txt_gameStatus.Size = new System.Drawing.Size(0, 26);
            this.txt_gameStatus.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(67, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 26);
            this.label5.TabIndex = 7;
            this.label5.Text = "狀態 ：";
            // 
            // txt_gameRegion
            // 
            this.txt_gameRegion.AutoSize = true;
            this.txt_gameRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_gameRegion.Location = new System.Drawing.Point(479, 52);
            this.txt_gameRegion.Name = "txt_gameRegion";
            this.txt_gameRegion.Size = new System.Drawing.Size(0, 26);
            this.txt_gameRegion.TabIndex = 6;
            // 
            // txt_summonerLevel
            // 
            this.txt_summonerLevel.AutoSize = true;
            this.txt_summonerLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_summonerLevel.Location = new System.Drawing.Point(479, 12);
            this.txt_summonerLevel.Name = "txt_summonerLevel";
            this.txt_summonerLevel.Size = new System.Drawing.Size(0, 26);
            this.txt_summonerLevel.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(410, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 26);
            this.label4.TabIndex = 5;
            this.label4.Text = "地區 ：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(410, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "等級 ：";
            // 
            // txt_statusMessage
            // 
            this.txt_statusMessage.AutoSize = true;
            this.txt_statusMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_statusMessage.Location = new System.Drawing.Point(133, 52);
            this.txt_statusMessage.Name = "txt_statusMessage";
            this.txt_statusMessage.Size = new System.Drawing.Size(0, 26);
            this.txt_statusMessage.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(23, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "狀態訊息 ：";
            // 
            // txt_summonerName
            // 
            this.txt_summonerName.AutoSize = true;
            this.txt_summonerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_summonerName.Location = new System.Drawing.Point(133, 12);
            this.txt_summonerName.Name = "txt_summonerName";
            this.txt_summonerName.Size = new System.Drawing.Size(0, 26);
            this.txt_summonerName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(1, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "召喚師名稱 ：";
            // 
            // tabBasic
            // 
            this.tabBasic.Controls.Add(this.toggle_openOPGGUrl);
            this.tabBasic.Controls.Add(this.btn_refreshAvaiableChamp);
            this.tabBasic.Controls.Add(this.txt_pickLaneFrequceny);
            this.tabBasic.Controls.Add(this.label8);
            this.tabBasic.Controls.Add(this.label7);
            this.tabBasic.Controls.Add(this.checkList_selectLane);
            this.tabBasic.Controls.Add(this.toggle_autoPickLane);
            this.tabBasic.Controls.Add(this.listbox_selectChamp);
            this.tabBasic.Controls.Add(this.toggle_autoPickChamp);
            this.tabBasic.Controls.Add(this.toggle_autoAccept);
            this.tabBasic.Location = new System.Drawing.Point(134, 4);
            this.tabBasic.Name = "tabBasic";
            this.tabBasic.Size = new System.Drawing.Size(781, 469);
            this.tabBasic.TabIndex = 1;
            this.tabBasic.Text = "基本功能";
            this.tabBasic.UseVisualStyleBackColor = true;
            // 
            // toggle_openOPGGUrl
            // 
            this.toggle_openOPGGUrl.AutoSize = true;
            this.toggle_openOPGGUrl.Enabled = false;
            this.toggle_openOPGGUrl.Location = new System.Drawing.Point(10, 271);
            this.toggle_openOPGGUrl.Name = "toggle_openOPGGUrl";
            this.toggle_openOPGGUrl.Size = new System.Drawing.Size(294, 34);
            this.toggle_openOPGGUrl.TabIndex = 12;
            this.toggle_openOPGGUrl.Text = "自動開啟OPGG英雄頁面";
            this.toggle_openOPGGUrl.UseVisualStyleBackColor = true;
            // 
            // btn_refreshAvaiableChamp
            // 
            this.btn_refreshAvaiableChamp.Location = new System.Drawing.Point(583, 78);
            this.btn_refreshAvaiableChamp.Name = "btn_refreshAvaiableChamp";
            this.btn_refreshAvaiableChamp.Size = new System.Drawing.Size(168, 38);
            this.btn_refreshAvaiableChamp.TabIndex = 8;
            this.btn_refreshAvaiableChamp.Text = "刷新英雄列表";
            this.btn_refreshAvaiableChamp.UseVisualStyleBackColor = true;
            this.btn_refreshAvaiableChamp.Click += new System.EventHandler(this.btn_refreshAvaiableChamp_Click);
            // 
            // txt_pickLaneFrequceny
            // 
            this.txt_pickLaneFrequceny.Location = new System.Drawing.Point(425, 145);
            this.txt_pickLaneFrequceny.Name = "txt_pickLaneFrequceny";
            this.txt_pickLaneFrequceny.Size = new System.Drawing.Size(61, 38);
            this.txt_pickLaneFrequceny.TabIndex = 7;
            this.txt_pickLaneFrequceny.Text = "7";
            this.txt_pickLaneFrequceny.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_pickLaneFrequceny.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_pickLaneFrequceny_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(485, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(157, 30);
            this.label8.TabIndex = 6;
            this.label8.Text = "次後自動暫停";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(364, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 30);
            this.label7.TabIndex = 6;
            this.label7.Text = "發送";
            // 
            // checkList_selectLane
            // 
            this.checkList_selectLane.CheckOnClick = true;
            this.checkList_selectLane.ColumnWidth = 80;
            this.checkList_selectLane.FormattingEnabled = true;
            this.checkList_selectLane.Items.AddRange(new object[] {
            "上路",
            "中路",
            "下路",
            "打野",
            "輔助"});
            this.checkList_selectLane.Location = new System.Drawing.Point(180, 145);
            this.checkList_selectLane.MultiColumn = true;
            this.checkList_selectLane.Name = "checkList_selectLane";
            this.checkList_selectLane.Size = new System.Drawing.Size(166, 103);
            this.checkList_selectLane.TabIndex = 5;
            // 
            // toggle_autoPickLane
            // 
            this.toggle_autoPickLane.AutoSize = true;
            this.toggle_autoPickLane.Enabled = false;
            this.toggle_autoPickLane.Location = new System.Drawing.Point(10, 145);
            this.toggle_autoPickLane.Name = "toggle_autoPickLane";
            this.toggle_autoPickLane.Size = new System.Drawing.Size(128, 34);
            this.toggle_autoPickLane.TabIndex = 3;
            this.toggle_autoPickLane.Text = "自動喊路";
            this.toggle_autoPickLane.UseVisualStyleBackColor = true;
            this.toggle_autoPickLane.CheckedChanged += new System.EventHandler(this.toggle_autoPickLane_CheckedChanged);
            // 
            // listbox_selectChamp
            // 
            this.listbox_selectChamp.FormattingEnabled = true;
            this.listbox_selectChamp.Location = new System.Drawing.Point(180, 80);
            this.listbox_selectChamp.Name = "listbox_selectChamp";
            this.listbox_selectChamp.Size = new System.Drawing.Size(370, 38);
            this.listbox_selectChamp.TabIndex = 2;
            // 
            // toggle_autoPickChamp
            // 
            this.toggle_autoPickChamp.AutoSize = true;
            this.toggle_autoPickChamp.Enabled = false;
            this.toggle_autoPickChamp.Location = new System.Drawing.Point(10, 80);
            this.toggle_autoPickChamp.Name = "toggle_autoPickChamp";
            this.toggle_autoPickChamp.Size = new System.Drawing.Size(152, 34);
            this.toggle_autoPickChamp.TabIndex = 1;
            this.toggle_autoPickChamp.Text = "自動選英雄";
            this.toggle_autoPickChamp.UseVisualStyleBackColor = true;
            this.toggle_autoPickChamp.CheckedChanged += new System.EventHandler(this.toggle_autopick_CheckedChanged);
            // 
            // toggle_autoAccept
            // 
            this.toggle_autoAccept.AutoSize = true;
            this.toggle_autoAccept.Enabled = false;
            this.toggle_autoAccept.Location = new System.Drawing.Point(10, 19);
            this.toggle_autoAccept.Name = "toggle_autoAccept";
            this.toggle_autoAccept.Size = new System.Drawing.Size(176, 34);
            this.toggle_autoAccept.TabIndex = 0;
            this.toggle_autoAccept.Text = "自動接受配對";
            this.toggle_autoAccept.UseVisualStyleBackColor = true;
            this.toggle_autoAccept.CheckedChanged += new System.EventHandler(this.toggle_AutoAccept_CheckedChanged);
            // 
            // tabUpdate
            // 
            this.tabUpdate.Controls.Add(this.txt_changelog);
            this.tabUpdate.Controls.Add(this.btn_CheckUpdate);
            this.tabUpdate.Controls.Add(this.label9);
            this.tabUpdate.Controls.Add(this.label6);
            this.tabUpdate.Location = new System.Drawing.Point(134, 4);
            this.tabUpdate.Name = "tabUpdate";
            this.tabUpdate.Size = new System.Drawing.Size(781, 469);
            this.tabUpdate.TabIndex = 2;
            this.tabUpdate.Text = "更新日誌";
            this.tabUpdate.UseVisualStyleBackColor = true;
            // 
            // txt_changelog
            // 
            this.txt_changelog.BackColor = System.Drawing.Color.White;
            this.txt_changelog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_changelog.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_changelog.Location = new System.Drawing.Point(14, 58);
            this.txt_changelog.Name = "txt_changelog";
            this.txt_changelog.ReadOnly = true;
            this.txt_changelog.Size = new System.Drawing.Size(749, 394);
            this.txt_changelog.TabIndex = 6;
            this.txt_changelog.Text = "";
            // 
            // btn_CheckUpdate
            // 
            this.btn_CheckUpdate.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_CheckUpdate.Location = new System.Drawing.Point(681, 13);
            this.btn_CheckUpdate.Name = "btn_CheckUpdate";
            this.btn_CheckUpdate.Size = new System.Drawing.Size(82, 30);
            this.btn_CheckUpdate.TabIndex = 5;
            this.btn_CheckUpdate.Text = "檢查更新";
            this.btn_CheckUpdate.UseVisualStyleBackColor = true;
            this.btn_CheckUpdate.Click += new System.EventHandler(this.btn_CheckUpdate_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(684, -73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 30);
            this.label9.TabIndex = 4;
            this.label9.Text = "label9";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(14, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 30);
            this.label6.TabIndex = 1;
            this.label6.Text = "更新日誌詳情：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_processStatus
            // 
            this.txt_processStatus.AutoSize = true;
            this.txt_processStatus.Font = new System.Drawing.Font("Microsoft JhengHei UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_processStatus.ForeColor = System.Drawing.Color.Red;
            this.txt_processStatus.Location = new System.Drawing.Point(329, 23);
            this.txt_processStatus.Name = "txt_processStatus";
            this.txt_processStatus.Size = new System.Drawing.Size(370, 41);
            this.txt_processStatus.TabIndex = 2;
            this.txt_processStatus.Text = "請先啟動英雄聯盟客戶端";
            this.txt_processStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer_checkProccess
            // 
            this.timer_checkProccess.Enabled = true;
            this.timer_checkProccess.Interval = 1000;
            this.timer_checkProccess.Tick += new System.EventHandler(this.timer_checkProccess_Tick);
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_refresh.BackgroundImage = global::LeagueHelper.Properties.Resources.refresh;
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_refresh.Location = new System.Drawing.Point(888, 23);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(39, 41);
            this.btn_refresh.TabIndex = 3;
            this.tooltip1.SetToolTip(this.btn_refresh, "重新取得資料");
            this.btn_refresh.UseVisualStyleBackColor = false;
            this.btn_refresh.Visible = false;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // timer_requestCycle
            // 
            this.timer_requestCycle.Interval = 500;
            this.timer_requestCycle.Tick += new System.EventHandler(this.timer_requestCycle_Tick);
            // 
            // timer_autoAccept
            // 
            this.timer_autoAccept.Interval = 1000;
            this.timer_autoAccept.Tick += new System.EventHandler(this.timer_autoAccept_Tick);
            // 
            // timer_autoPickChamp
            // 
            this.timer_autoPickChamp.Interval = 500;
            this.timer_autoPickChamp.Tick += new System.EventHandler(this.timer_autoPickChamp_Tick);
            // 
            // timer_autoPickLane
            // 
            this.timer_autoPickLane.Interval = 500;
            this.timer_autoPickLane.Tick += new System.EventHandler(this.timer_autoPickLane_Tick);
            // 
            // txt_version
            // 
            this.txt_version.AutoSize = true;
            this.txt_version.Location = new System.Drawing.Point(830, 561);
            this.txt_version.Name = "txt_version";
            this.txt_version.Size = new System.Drawing.Size(0, 15);
            this.txt_version.TabIndex = 4;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "英雄聯盟小助手";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // timer_openUrl
            // 
            this.timer_openUrl.Interval = 500;
            this.timer_openUrl.Tick += new System.EventHandler(this.timer_openUrl_Tick);
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(943, 581);
            this.Controls.Add(this.txt_version);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.txt_processStatus);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainApp";
            this.Text = "英雄聯盟小助手 by Jack";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainApp_FormClosing);
            this.Load += new System.EventHandler(this.MainApp_Load);
            this.Resize += new System.EventHandler(this.MainApp_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.tabInfo.PerformLayout();
            this.tabBasic.ResumeLayout(false);
            this.tabBasic.PerformLayout();
            this.tabUpdate.ResumeLayout(false);
            this.tabUpdate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txt_process;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.TabPage tabBasic;
        private System.Windows.Forms.Label txt_processStatus;
        private System.Windows.Forms.Timer timer_checkProccess;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.ToolTip tooltip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txt_summonerName;
        private System.Windows.Forms.Timer timer_requestCycle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txt_statusMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txt_gameRegion;
        private System.Windows.Forms.Label txt_summonerLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label txt_game;
        private System.Windows.Forms.Label txt_gameStatus;
        private System.Windows.Forms.CheckBox toggle_autoAccept;
        private System.Windows.Forms.Timer timer_autoAccept;
        private System.Windows.Forms.TextBox txt_debug1;
        private System.Windows.Forms.TextBox txt_debug2;
        private System.Windows.Forms.CheckBox toggle_autoPickChamp;
        private System.Windows.Forms.ComboBox listbox_selectChamp;
        private System.Windows.Forms.Timer timer_autoPickChamp;
        private System.Windows.Forms.Timer timer_autoPickLane;
        private System.Windows.Forms.CheckBox toggle_autoPickLane;
        private System.Windows.Forms.CheckedListBox checkList_selectLane;
        private System.Windows.Forms.CheckBox amp;
        private System.Windows.Forms.CheckBox le;
        private System.Windows.Forms.CheckBox ad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_pickLaneFrequceny;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_refreshAvaiableChamp;
        private System.Windows.Forms.TabPage tabUpdate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label txt_version;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox txt_changelog;
        private System.Windows.Forms.Button btn_CheckUpdate;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox to;
        private System.Windows.Forms.CheckBox togg;
        private System.Windows.Forms.CheckBox toggle_openOPGGUrl;
        private System.Windows.Forms.Timer timer_openUrl;
    }
}

