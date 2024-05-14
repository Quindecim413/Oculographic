namespace oculographic
{
    partial class FormTrainingSetParams
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSelectConfiguration = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownWaiting = new System.Windows.Forms.NumericUpDown();
            this.buttonCalibrate = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownIntegrateCommand = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCommandsList = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownMaxPerformCommand = new System.Windows.Forms.NumericUpDown();
            this.textBoxSaveFilePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownSleepAfterCommand = new System.Windows.Forms.NumericUpDown();
            this.checkBoxHideCommandDone = new System.Windows.Forms.CheckBox();
            this.buttonRandomizeCommands = new System.Windows.Forms.Button();
            this.numericUpDownNCommands = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaiting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntegrateCommand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPerformCommand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSleepAfterCommand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNCommands)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Конфигурация";
            // 
            // comboBoxSelectConfiguration
            // 
            this.comboBoxSelectConfiguration.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.comboBoxSelectConfiguration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectConfiguration.FormattingEnabled = true;
            this.comboBoxSelectConfiguration.Location = new System.Drawing.Point(104, 12);
            this.comboBoxSelectConfiguration.Name = "comboBoxSelectConfiguration";
            this.comboBoxSelectConfiguration.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSelectConfiguration.TabIndex = 9;
            this.comboBoxSelectConfiguration.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectConfiguration_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Настройка (с)";
            // 
            // numericUpDownWaiting
            // 
            this.numericUpDownWaiting.DecimalPlaces = 1;
            this.numericUpDownWaiting.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownWaiting.Location = new System.Drawing.Point(104, 89);
            this.numericUpDownWaiting.Margin = new System.Windows.Forms.Padding(5);
            this.numericUpDownWaiting.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownWaiting.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWaiting.Name = "numericUpDownWaiting";
            this.numericUpDownWaiting.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownWaiting.TabIndex = 13;
            this.numericUpDownWaiting.Value = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            // 
            // buttonCalibrate
            // 
            this.buttonCalibrate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCalibrate.Location = new System.Drawing.Point(307, 59);
            this.buttonCalibrate.Name = "buttonCalibrate";
            this.buttonCalibrate.Size = new System.Drawing.Size(100, 23);
            this.buttonCalibrate.TabIndex = 10;
            this.buttonCalibrate.Text = "Калибровать";
            this.buttonCalibrate.UseVisualStyleBackColor = true;
            this.buttonCalibrate.Click += new System.EventHandler(this.buttonCalibrate_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(270, 157);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(137, 23);
            this.buttonRun.TabIndex = 11;
            this.buttonRun.Text = "Запустить";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 186);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Накопление команды (с)";
            // 
            // numericUpDownIntegrateCommand
            // 
            this.numericUpDownIntegrateCommand.DecimalPlaces = 1;
            this.numericUpDownIntegrateCommand.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownIntegrateCommand.Location = new System.Drawing.Point(211, 184);
            this.numericUpDownIntegrateCommand.Margin = new System.Windows.Forms.Padding(5);
            this.numericUpDownIntegrateCommand.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numericUpDownIntegrateCommand.Name = "numericUpDownIntegrateCommand";
            this.numericUpDownIntegrateCommand.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownIntegrateCommand.TabIndex = 15;
            this.numericUpDownIntegrateCommand.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 37);
            this.label4.TabIndex = 16;
            this.label4.Text = "Калибровка";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 37);
            this.label5.TabIndex = 17;
            this.label5.Text = "Тренировка";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 278);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Программа";
            // 
            // textBoxCommandsList
            // 
            this.textBoxCommandsList.Location = new System.Drawing.Point(18, 294);
            this.textBoxCommandsList.Name = "textBoxCommandsList";
            this.textBoxCommandsList.Size = new System.Drawing.Size(389, 20);
            this.textBoxCommandsList.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 221);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Время на выполнение команды (с)";
            // 
            // numericUpDownMaxPerformCommand
            // 
            this.numericUpDownMaxPerformCommand.DecimalPlaces = 1;
            this.numericUpDownMaxPerformCommand.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownMaxPerformCommand.Location = new System.Drawing.Point(211, 219);
            this.numericUpDownMaxPerformCommand.Margin = new System.Windows.Forms.Padding(5);
            this.numericUpDownMaxPerformCommand.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numericUpDownMaxPerformCommand.Name = "numericUpDownMaxPerformCommand";
            this.numericUpDownMaxPerformCommand.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownMaxPerformCommand.TabIndex = 21;
            this.numericUpDownMaxPerformCommand.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // textBoxSaveFilePath
            // 
            this.textBoxSaveFilePath.Location = new System.Drawing.Point(19, 344);
            this.textBoxSaveFilePath.Name = "textBoxSaveFilePath";
            this.textBoxSaveFilePath.Size = new System.Drawing.Size(388, 20);
            this.textBoxSaveFilePath.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 328);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(404, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Название файла (допускается путь с вложенными папками, без расширения)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 254);
            this.label9.Margin = new System.Windows.Forms.Padding(8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Отдых после команды (с)";
            // 
            // numericUpDownSleepAfterCommand
            // 
            this.numericUpDownSleepAfterCommand.DecimalPlaces = 1;
            this.numericUpDownSleepAfterCommand.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownSleepAfterCommand.Location = new System.Drawing.Point(211, 252);
            this.numericUpDownSleepAfterCommand.Margin = new System.Windows.Forms.Padding(5);
            this.numericUpDownSleepAfterCommand.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownSleepAfterCommand.Name = "numericUpDownSleepAfterCommand";
            this.numericUpDownSleepAfterCommand.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownSleepAfterCommand.TabIndex = 25;
            this.numericUpDownSleepAfterCommand.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // checkBoxHideCommandDone
            // 
            this.checkBoxHideCommandDone.AutoSize = true;
            this.checkBoxHideCommandDone.Location = new System.Drawing.Point(252, 7);
            this.checkBoxHideCommandDone.Name = "checkBoxHideCommandDone";
            this.checkBoxHideCommandDone.Size = new System.Drawing.Size(155, 30);
            this.checkBoxHideCommandDone.TabIndex = 26;
            this.checkBoxHideCommandDone.Text = "Скрывать выполненную\r\nкоманду";
            this.checkBoxHideCommandDone.UseVisualStyleBackColor = true;
            this.checkBoxHideCommandDone.CheckedChanged += new System.EventHandler(this.checkBoxHideCommandDone_CheckedChanged);
            // 
            // buttonRandomizeCommands
            // 
            this.buttonRandomizeCommands.Location = new System.Drawing.Point(270, 231);
            this.buttonRandomizeCommands.Name = "buttonRandomizeCommands";
            this.buttonRandomizeCommands.Size = new System.Drawing.Size(137, 31);
            this.buttonRandomizeCommands.TabIndex = 27;
            this.buttonRandomizeCommands.Text = "Зарандомить";
            this.buttonRandomizeCommands.UseVisualStyleBackColor = true;
            this.buttonRandomizeCommands.Click += new System.EventHandler(this.buttonRandomizeCommands_Click);
            // 
            // numericUpDownNCommands
            // 
            this.numericUpDownNCommands.Location = new System.Drawing.Point(352, 268);
            this.numericUpDownNCommands.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownNCommands.Name = "numericUpDownNCommands";
            this.numericUpDownNCommands.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownNCommands.TabIndex = 28;
            this.numericUpDownNCommands.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(268, 270);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Всего команд";
            // 
            // FormTrainingSetParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 371);
            this.ControlBox = false;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numericUpDownNCommands);
            this.Controls.Add(this.buttonRandomizeCommands);
            this.Controls.Add(this.checkBoxHideCommandDone);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDownSleepAfterCommand);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxSaveFilePath);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDownMaxPerformCommand);
            this.Controls.Add(this.textBoxCommandsList);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxSelectConfiguration);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownWaiting);
            this.Controls.Add(this.buttonCalibrate);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownIntegrateCommand);
            this.Name = "FormTrainingSetParams";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormTrainingSetParams";
            this.Load += new System.EventHandler(this.FormTrainingSetParams_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaiting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntegrateCommand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPerformCommand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSleepAfterCommand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNCommands)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxSelectConfiguration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownWaiting;
        private System.Windows.Forms.Button buttonCalibrate;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownIntegrateCommand;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCommandsList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxPerformCommand;
        private System.Windows.Forms.TextBox textBoxSaveFilePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownSleepAfterCommand;
        private System.Windows.Forms.CheckBox checkBoxHideCommandDone;
        private System.Windows.Forms.Button buttonRandomizeCommands;
        private System.Windows.Forms.NumericUpDown numericUpDownNCommands;
        private System.Windows.Forms.Label label10;
    }
}