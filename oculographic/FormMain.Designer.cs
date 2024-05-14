namespace oculographic
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.clientControl = new NetManager.Client.ClientControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxPreprocessing = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonUseSimpleWindow = new System.Windows.Forms.RadioButton();
            this.nAverageSize = new System.Windows.Forms.NumericUpDown();
            this.radioButtonUseWeightedWindow = new System.Windows.Forms.RadioButton();
            this.panelWeightedWeights = new System.Windows.Forms.Panel();
            this.buttonShowWeights = new System.Windows.Forms.Button();
            this.buttonLoadWeights = new System.Windows.Forms.Button();
            this.radioButtonDoNotUseWindow = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonConnectServer = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxWriteEEG = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxWriteOculo = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownDistanceFromScreen = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownScreenHeight = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonStartProcess = new System.Windows.Forms.Button();
            this.buttonShowAngles = new System.Windows.Forms.Button();
            this.buttonShowCharts = new System.Windows.Forms.Button();
            this.buttonShowCursor = new System.Windows.Forms.Button();
            this.buttonShowCommands = new System.Windows.Forms.Button();
            this.openFileDialogSelectWeights = new System.Windows.Forms.OpenFileDialog();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBoxPreprocessing.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nAverageSize)).BeginInit();
            this.panelWeightedWeights.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistanceFromScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenHeight)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // clientControl
            // 
            this.clientControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.clientControl.IPServer = ((System.Net.IPAddress)(resources.GetObject("clientControl.IPServer")));
            this.clientControl.Location = new System.Drawing.Point(3, 16);
            this.clientControl.Name = "clientControl";
            this.clientControl.Size = new System.Drawing.Size(189, 167);
            this.clientControl.TabIndex = 0;
            this.clientControl.ReseiveData += new System.EventHandler<NetManager.EventClientMsgArgs>(this.clientControl1_ReseiveData);
            this.clientControl.ClientError += new System.EventHandler<NetManager.EventMsgArgs>(this.clientControl1_ClientError);
            this.clientControl.Started += new System.EventHandler(this.clientControl1_Started);
            this.clientControl.Stopped += new System.EventHandler(this.clientControl1_Stopped);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBoxPreprocessing);
            this.groupBox2.Location = new System.Drawing.Point(7, 241);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 200);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки обработки";
            // 
            // groupBoxPreprocessing
            // 
            this.groupBoxPreprocessing.Controls.Add(this.flowLayoutPanel1);
            this.groupBoxPreprocessing.Location = new System.Drawing.Point(6, 17);
            this.groupBoxPreprocessing.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.groupBoxPreprocessing.Name = "groupBoxPreprocessing";
            this.groupBoxPreprocessing.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.groupBoxPreprocessing.Size = new System.Drawing.Size(186, 183);
            this.groupBoxPreprocessing.TabIndex = 16;
            this.groupBoxPreprocessing.TabStop = false;
            this.groupBoxPreprocessing.Text = "Предобрабатывать";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.radioButtonUseSimpleWindow);
            this.flowLayoutPanel1.Controls.Add(this.nAverageSize);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonUseWeightedWindow);
            this.flowLayoutPanel1.Controls.Add(this.panelWeightedWeights);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonDoNotUseWindow);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 23);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(186, 160);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // radioButtonUseSimpleWindow
            // 
            this.radioButtonUseSimpleWindow.AutoSize = true;
            this.radioButtonUseSimpleWindow.Location = new System.Drawing.Point(3, 3);
            this.radioButtonUseSimpleWindow.Name = "radioButtonUseSimpleWindow";
            this.radioButtonUseSimpleWindow.Size = new System.Drawing.Size(138, 17);
            this.radioButtonUseSimpleWindow.TabIndex = 2;
            this.radioButtonUseSimpleWindow.Text = "Скользящим средним";
            this.radioButtonUseSimpleWindow.UseVisualStyleBackColor = true;
            this.radioButtonUseSimpleWindow.CheckedChanged += new System.EventHandler(this.radioButtonUseSimpleWindow_CheckedChanged);
            // 
            // nAverageSize
            // 
            this.nAverageSize.Enabled = false;
            this.nAverageSize.Location = new System.Drawing.Point(15, 26);
            this.nAverageSize.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.nAverageSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nAverageSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nAverageSize.Name = "nAverageSize";
            this.nAverageSize.Size = new System.Drawing.Size(148, 20);
            this.nAverageSize.TabIndex = 17;
            this.nAverageSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButtonUseWeightedWindow
            // 
            this.radioButtonUseWeightedWindow.AutoSize = true;
            this.radioButtonUseWeightedWindow.Location = new System.Drawing.Point(3, 52);
            this.radioButtonUseWeightedWindow.Name = "radioButtonUseWeightedWindow";
            this.radioButtonUseWeightedWindow.Size = new System.Drawing.Size(160, 30);
            this.radioButtonUseWeightedWindow.TabIndex = 0;
            this.radioButtonUseWeightedWindow.Text = "Взвешенным скользящим\r\nсредним";
            this.radioButtonUseWeightedWindow.UseVisualStyleBackColor = true;
            this.radioButtonUseWeightedWindow.CheckedChanged += new System.EventHandler(this.radioButtonUseWeightedWindow_CheckedChanged);
            // 
            // panelWeightedWeights
            // 
            this.panelWeightedWeights.Controls.Add(this.buttonShowWeights);
            this.panelWeightedWeights.Controls.Add(this.buttonLoadWeights);
            this.panelWeightedWeights.Enabled = false;
            this.panelWeightedWeights.Location = new System.Drawing.Point(0, 85);
            this.panelWeightedWeights.Margin = new System.Windows.Forms.Padding(0);
            this.panelWeightedWeights.Name = "panelWeightedWeights";
            this.panelWeightedWeights.Size = new System.Drawing.Size(186, 48);
            this.panelWeightedWeights.TabIndex = 18;
            // 
            // buttonShowWeights
            // 
            this.buttonShowWeights.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonShowWeights.Enabled = false;
            this.buttonShowWeights.Location = new System.Drawing.Point(0, 23);
            this.buttonShowWeights.Name = "buttonShowWeights";
            this.buttonShowWeights.Size = new System.Drawing.Size(186, 23);
            this.buttonShowWeights.TabIndex = 1;
            this.buttonShowWeights.Text = "Просмотреть веса";
            this.buttonShowWeights.UseVisualStyleBackColor = true;
            this.buttonShowWeights.Click += new System.EventHandler(this.buttonShowWeights_Click);
            // 
            // buttonLoadWeights
            // 
            this.buttonLoadWeights.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonLoadWeights.Location = new System.Drawing.Point(0, 0);
            this.buttonLoadWeights.Name = "buttonLoadWeights";
            this.buttonLoadWeights.Size = new System.Drawing.Size(186, 23);
            this.buttonLoadWeights.TabIndex = 0;
            this.buttonLoadWeights.Text = "Загрузить веса";
            this.buttonLoadWeights.UseVisualStyleBackColor = true;
            this.buttonLoadWeights.Click += new System.EventHandler(this.buttonLoadWeights_Click);
            // 
            // radioButtonDoNotUseWindow
            // 
            this.radioButtonDoNotUseWindow.AutoSize = true;
            this.radioButtonDoNotUseWindow.Checked = true;
            this.radioButtonDoNotUseWindow.Location = new System.Drawing.Point(3, 136);
            this.radioButtonDoNotUseWindow.Name = "radioButtonDoNotUseWindow";
            this.radioButtonDoNotUseWindow.Size = new System.Drawing.Size(44, 17);
            this.radioButtonDoNotUseWindow.TabIndex = 1;
            this.radioButtonDoNotUseWindow.TabStop = true;
            this.radioButtonDoNotUseWindow.Text = "Нет";
            this.radioButtonDoNotUseWindow.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonConnectServer);
            this.groupBox1.Controls.Add(this.clientControl);
            this.groupBox1.Location = new System.Drawing.Point(4, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 214);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сеть";
            // 
            // buttonConnectServer
            // 
            this.buttonConnectServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConnectServer.Location = new System.Drawing.Point(3, 183);
            this.buttonConnectServer.Margin = new System.Windows.Forms.Padding(0);
            this.buttonConnectServer.Name = "buttonConnectServer";
            this.buttonConnectServer.Size = new System.Drawing.Size(189, 28);
            this.buttonConnectServer.TabIndex = 1;
            this.buttonConnectServer.Text = "Подключить";
            this.buttonConnectServer.UseVisualStyleBackColor = true;
            this.buttonConnectServer.Click += new System.EventHandler(this.buttonConnectServer_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxWriteEEG);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.checkBoxWriteOculo);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numericUpDownDistanceFromScreen);
            this.groupBox3.Controls.Add(this.numericUpDownScreenHeight);
            this.groupBox3.Location = new System.Drawing.Point(210, 232);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(188, 209);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Параметры";
            // 
            // checkBoxWriteEEG
            // 
            this.checkBoxWriteEEG.AutoSize = true;
            this.checkBoxWriteEEG.Checked = true;
            this.checkBoxWriteEEG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWriteEEG.Location = new System.Drawing.Point(18, 187);
            this.checkBoxWriteEEG.Name = "checkBoxWriteEEG";
            this.checkBoxWriteEEG.Size = new System.Drawing.Size(86, 17);
            this.checkBoxWriteEEG.TabIndex = 20;
            this.checkBoxWriteEEG.Text = "Писать ЭЭГ";
            this.checkBoxWriteEEG.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Удаление от экрана (см)";
            // 
            // checkBoxWriteOculo
            // 
            this.checkBoxWriteOculo.AutoSize = true;
            this.checkBoxWriteOculo.Checked = true;
            this.checkBoxWriteOculo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWriteOculo.Location = new System.Drawing.Point(18, 164);
            this.checkBoxWriteOculo.Name = "checkBoxWriteOculo";
            this.checkBoxWriteOculo.Size = new System.Drawing.Size(134, 17);
            this.checkBoxWriteOculo.TabIndex = 19;
            this.checkBoxWriteOculo.Text = "Писать окулографию";
            this.checkBoxWriteOculo.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Высота экрана (см)";
            // 
            // numericUpDownDistanceFromScreen
            // 
            this.numericUpDownDistanceFromScreen.Location = new System.Drawing.Point(18, 127);
            this.numericUpDownDistanceFromScreen.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownDistanceFromScreen.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDistanceFromScreen.Name = "numericUpDownDistanceFromScreen";
            this.numericUpDownDistanceFromScreen.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownDistanceFromScreen.TabIndex = 31;
            this.numericUpDownDistanceFromScreen.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // numericUpDownScreenHeight
            // 
            this.numericUpDownScreenHeight.Location = new System.Drawing.Point(18, 64);
            this.numericUpDownScreenHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownScreenHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownScreenHeight.Name = "numericUpDownScreenHeight";
            this.numericUpDownScreenHeight.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownScreenHeight.TabIndex = 30;
            this.numericUpDownScreenHeight.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonStartProcess);
            this.groupBox4.Controls.Add(this.buttonShowAngles);
            this.groupBox4.Controls.Add(this.buttonShowCharts);
            this.groupBox4.Controls.Add(this.buttonShowCursor);
            this.groupBox4.Controls.Add(this.buttonShowCommands);
            this.groupBox4.Location = new System.Drawing.Point(210, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(188, 214);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Контроль";
            // 
            // buttonStartProcess
            // 
            this.buttonStartProcess.Enabled = false;
            this.buttonStartProcess.Location = new System.Drawing.Point(0, 171);
            this.buttonStartProcess.Name = "buttonStartProcess";
            this.buttonStartProcess.Size = new System.Drawing.Size(188, 35);
            this.buttonStartProcess.TabIndex = 5;
            this.buttonStartProcess.Text = "Пуск";
            this.buttonStartProcess.UseVisualStyleBackColor = true;
            this.buttonStartProcess.Click += new System.EventHandler(this.buttonStartProcess_Click);
            // 
            // buttonShowAngles
            // 
            this.buttonShowAngles.Enabled = false;
            this.buttonShowAngles.Location = new System.Drawing.Point(6, 139);
            this.buttonShowAngles.Name = "buttonShowAngles";
            this.buttonShowAngles.Size = new System.Drawing.Size(176, 26);
            this.buttonShowAngles.TabIndex = 4;
            this.buttonShowAngles.Text = "Показать углы";
            this.buttonShowAngles.UseVisualStyleBackColor = true;
            this.buttonShowAngles.Click += new System.EventHandler(this.buttonShowAngles_Click);
            // 
            // buttonShowCharts
            // 
            this.buttonShowCharts.Location = new System.Drawing.Point(6, 103);
            this.buttonShowCharts.Name = "buttonShowCharts";
            this.buttonShowCharts.Size = new System.Drawing.Size(176, 26);
            this.buttonShowCharts.TabIndex = 3;
            this.buttonShowCharts.Text = "Показать графики";
            this.buttonShowCharts.UseVisualStyleBackColor = true;
            this.buttonShowCharts.Click += new System.EventHandler(this.buttonShowCharts_Click);
            // 
            // buttonShowCursor
            // 
            this.buttonShowCursor.Enabled = false;
            this.buttonShowCursor.Location = new System.Drawing.Point(6, 65);
            this.buttonShowCursor.Name = "buttonShowCursor";
            this.buttonShowCursor.Size = new System.Drawing.Size(176, 26);
            this.buttonShowCursor.TabIndex = 2;
            this.buttonShowCursor.Text = "Контроль курсора";
            this.buttonShowCursor.UseVisualStyleBackColor = true;
            this.buttonShowCursor.Click += new System.EventHandler(this.buttonShowCursor_Click);
            // 
            // buttonShowCommands
            // 
            this.buttonShowCommands.Enabled = false;
            this.buttonShowCommands.Location = new System.Drawing.Point(6, 26);
            this.buttonShowCommands.Name = "buttonShowCommands";
            this.buttonShowCommands.Size = new System.Drawing.Size(176, 26);
            this.buttonShowCommands.TabIndex = 1;
            this.buttonShowCommands.Text = "Контроль команд";
            this.buttonShowCommands.UseVisualStyleBackColor = true;
            this.buttonShowCommands.Click += new System.EventHandler(this.buttonShowCommands_Click);
            // 
            // openFileDialogSelectWeights
            // 
            this.openFileDialogSelectWeights.FileName = "openFileDialog1";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(404, 12);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(163, 429);
            this.textBoxLog.TabIndex = 26;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 446);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки и контроль";
            this.groupBox2.ResumeLayout(false);
            this.groupBoxPreprocessing.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nAverageSize)).EndInit();
            this.panelWeightedWeights.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistanceFromScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenHeight)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NetManager.Client.ClientControl clientControl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxPreprocessing;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButtonUseSimpleWindow;
        private System.Windows.Forms.NumericUpDown nAverageSize;
        private System.Windows.Forms.RadioButton radioButtonUseWeightedWindow;
        private System.Windows.Forms.Panel panelWeightedWeights;
        private System.Windows.Forms.Button buttonShowWeights;
        private System.Windows.Forms.Button buttonLoadWeights;
        private System.Windows.Forms.RadioButton radioButtonDoNotUseWindow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownDistanceFromScreen;
        private System.Windows.Forms.NumericUpDown numericUpDownScreenHeight;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonShowCommands;
        private System.Windows.Forms.Button buttonShowCursor;
        private System.Windows.Forms.Button buttonShowCharts;
        private System.Windows.Forms.Button buttonShowAngles;
        private System.Windows.Forms.CheckBox checkBoxWriteEEG;
        private System.Windows.Forms.CheckBox checkBoxWriteOculo;
        private System.Windows.Forms.OpenFileDialog openFileDialogSelectWeights;
        private System.Windows.Forms.Button buttonConnectServer;
        private System.Windows.Forms.Button buttonStartProcess;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}