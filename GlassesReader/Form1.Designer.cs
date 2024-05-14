namespace GlassesReader
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonConnectServer = new System.Windows.Forms.Button();
            this.clientControl = new NetManager.Client.ClientControl();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.buttonReadPort = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonReloadPorts = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serialPortGlasses = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonConnectServer);
            this.groupBox1.Controls.Add(this.clientControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 242);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сеть";
            // 
            // buttonConnectServer
            // 
            this.buttonConnectServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConnectServer.Location = new System.Drawing.Point(3, 204);
            this.buttonConnectServer.Margin = new System.Windows.Forms.Padding(0);
            this.buttonConnectServer.Name = "buttonConnectServer";
            this.buttonConnectServer.Size = new System.Drawing.Size(189, 35);
            this.buttonConnectServer.TabIndex = 1;
            this.buttonConnectServer.Text = "Подключить";
            this.buttonConnectServer.UseVisualStyleBackColor = true;
            this.buttonConnectServer.Click += new System.EventHandler(this.buttonConnectServer_Click);
            // 
            // clientControl
            // 
            this.clientControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.clientControl.IPServer = ((System.Net.IPAddress)(resources.GetObject("clientControl.IPServer")));
            this.clientControl.Location = new System.Drawing.Point(3, 16);
            this.clientControl.Name = "clientControl";
            this.clientControl.Size = new System.Drawing.Size(189, 188);
            this.clientControl.TabIndex = 0;
            this.clientControl.Started += new System.EventHandler(this.clientControl_Started);
            this.clientControl.Stopped += new System.EventHandler(this.clientControl_Stopped);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(6, 32);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPorts.TabIndex = 25;
            this.comboBoxPorts.SelectedValueChanged += new System.EventHandler(this.comboBoxPorts_SelectedValueChanged);
            // 
            // buttonReadPort
            // 
            this.buttonReadPort.Location = new System.Drawing.Point(6, 59);
            this.buttonReadPort.Name = "buttonReadPort";
            this.buttonReadPort.Size = new System.Drawing.Size(186, 35);
            this.buttonReadPort.TabIndex = 26;
            this.buttonReadPort.Text = "Подключить";
            this.buttonReadPort.UseVisualStyleBackColor = true;
            this.buttonReadPort.Click += new System.EventHandler(this.buttonReadPort_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Порт для подключения";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonReloadPorts);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.buttonReadPort);
            this.groupBox2.Controls.Add(this.comboBoxPorts);
            this.groupBox2.Location = new System.Drawing.Point(12, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Очки";
            // 
            // buttonReloadPorts
            // 
            this.buttonReloadPorts.Location = new System.Drawing.Point(134, 30);
            this.buttonReloadPorts.Name = "buttonReloadPorts";
            this.buttonReloadPorts.Size = new System.Drawing.Size(58, 23);
            this.buttonReloadPorts.TabIndex = 28;
            this.buttonReloadPorts.Text = "Обнов.";
            this.buttonReloadPorts.UseVisualStyleBackColor = true;
            this.buttonReloadPorts.Click += new System.EventHandler(this.buttonReloadPorts_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            // 
            // serialPortGlasses
            // 
            this.serialPortGlasses.BaudRate = 115200;
            this.serialPortGlasses.DtrEnable = true;
            this.serialPortGlasses.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortGlasses_DataReceived);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 406);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Glasses2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonConnectServer;
        private NetManager.Client.ClientControl clientControl;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.Button buttonReadPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonReloadPorts;
        private System.Windows.Forms.Timer timer1;
        private System.IO.Ports.SerialPort serialPortGlasses;
    }
}

