using EEG;
using NetManager;
using oculographic.Approximation;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace oculographic {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
            clientControl.ClientName = "Attention detector";
        }
        Action<float[]> WriteOculo = null;
        Action<double[,]> WriteEEG = null;

        SlidingWindowSmoother[] m_slidingWindowSmoother = null;
        double[] m_loadedWeights = null;

        Approximation.OculoTraining Trainer { get; set; }
        FormTraining DiscreteTrainingForm;
        FormChart ChartForm;
        CursorControl CursorControl;
        ShowAngles ShowAnglesForm;

        private bool Running { get; set; } = false;
        private bool Saving { get; set; } = false;
        private readonly object lockObjOculoWrite = new object();
        private readonly object lockObjEEGWrite = new object();

        private void buttonShowCommands_Click(object sender, EventArgs e) {
            if (DiscreteTrainingForm == null)
                DiscreteTrainingForm = new FormTraining(StartSaving, StopSaving, (commandCode) =>
                {
                    if (clientControl.Client.IsRunning)
                    {
                        string command = (commandCode + 5).ToString();
                        byte[] m_SendData = new byte[4 + 2 * command.Length];
                        Array.Copy(BitConverter.GetBytes(MessageCodes.Command), m_SendData, 4);
                        for (int i = 0; i < command.Length; i++)
                        {
                            var bts = BitConverter.GetBytes(command[i]);
                            Array.Copy(bts, 0, m_SendData, 4 + i * 2, 2);
                        }
                        clientControl.Client.SendData(clientControl.CheckedClientAddresses, m_SendData);
                        Invoke(new Action(() => { textBoxLog.Text = "Send " + command + "\r\n" + textBoxLog.Text; }));
                    }
                });
            DiscreteTrainingForm.Show();
            DiscreteTrainingForm.FormClosing += DiscreteTrainingForm_FormClosing;
            DiscreteTrainingForm.OnStartTraining += () => {
                Trainer = null;
                Invoke(new Action(() => {
                    buttonShowCursor.Enabled = false;
                    buttonShowAngles.Enabled = false;
                    if (CursorControl != null) {
                        CursorControl.Close();
                        CursorControl = null;
                    }
                    if (ShowAnglesForm != null) {
                        ShowAnglesForm.Close();
                        ShowAnglesForm = null;
                    }
                }));
            };
            DiscreteTrainingForm.OnCompleteTraining += (trainer, trainArea, screenSize) => {
                if (trainer != null) {
                    buttonShowCursor.Enabled = true;
                    buttonShowAngles.Enabled = true;

                    trainer.TrainArea = trainArea;
                    trainer.ScreenSize = screenSize;
                    trainer.ScreenHeight = (double)numericUpDownScreenHeight.Value;
                    trainer.DistanceFromScreen = (double)numericUpDownDistanceFromScreen.Value;
                    trainer.ComfirmFitting();
                    Trainer = trainer;
                }
            };
        }
        private void buttonShowCursor_Click(object sender, EventArgs e) {
            if (CursorControl == null) CursorControl = new CursorControl(() => Trainer);
            CursorControl.Show();
        }
        private void buttonShowCharts_Click(object sender, EventArgs e) {
            if (ChartForm == null) ChartForm = new FormChart(() => Trainer);
            ChartForm.Show();
        }
        private void DiscreteTrainingForm_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            DiscreteTrainingForm.Hide();
            Trainer = DiscreteTrainingForm.Trainer;
            if (Trainer != null && Trainer.Confirmed) {
                buttonShowCursor.Enabled = true;
                buttonShowAngles.Enabled = true;
            }
        }
        // Обработчик инициализации сетевого взаимодействия через NMServer
        private void clientControl1_Started(object sender, EventArgs e) {
            var act = new Action(() => {
                buttonConnectServer.Text = "Отключить";
                buttonStartProcess.Enabled = true;
            });
            if (!InvokeRequired) act();
            else Invoke(act);
        }
        // Обработчик остановки работы NMServer
        private void clientControl1_Stopped(object sender, EventArgs e) {
            var act = new Action(() => {
                buttonConnectServer.Text = "Подключить";
                buttonStartProcess.Enabled = false;
                StopProcess();
                StopSaving();
            });
            if (!InvokeRequired) act();
            else Invoke(act);
        }
        // Обработчик ошибок, произошедших при работе с NMServer
        private void clientControl1_ClientError(object sender, NetManager.EventMsgArgs e) {
            var act = new Action(() => {
                buttonConnectServer.Text = "Подключить";
                buttonStartProcess.Enabled = false;
                StopProcess();
                StopSaving();
                MessageBox.Show(e.Msg, "Ошибка NMClient");
            });
            if (!InvokeRequired) act();
            else Invoke(act);
        }
        // Обработчик входных данных, поступающих с NMServer
        private void clientControl1_ReseiveData(object sender, NetManager.EventClientMsgArgs e) {
            if (!Running) return;
            try {
                int N = BitConverter.ToInt32(e.Msg, 0);//первые четыре байта переводим в число
                if (N == 6) {
                    Frame RAW = new Frame(e.Msg);
                    double[,] data_out = new double[24, 29];
                    for (int row = 0; row < 24; row++)
                        for (int column = 0; column < 29; column++)
                            data_out[row, column] = RAW.Data[column * 24 + row];
                    if (!m_slidingWindowSmoother[0].IsReady) return; // Сохранять можно только тогда: когда к этому готов oculo
                    if (Saving) WriteEEG?.Invoke(data_out);
                }
                if (N == 23) {
                    float value1 = BitConverter.ToSingle(e.Msg, 8);
                    float value2 = BitConverter.ToSingle(e.Msg, 12);
                    float value3 = BitConverter.ToSingle(e.Msg, 16);
                    float value4 = BitConverter.ToSingle(e.Msg, 20);
                    value1 = (float)m_slidingWindowSmoother[0].Add(value1);
                    value2 = (float)m_slidingWindowSmoother[1].Add(value2);
                    value3 = (float)m_slidingWindowSmoother[2].Add(value3);
                    value4 = (float)m_slidingWindowSmoother[3].Add(value4);
                    AnnounceData(value1, value2, value3, value4);
                    // Запись данных, поступавших с очков
                    if (Saving) WriteOculo?.Invoke(new float[] { value1, value2, value3, value4 });
                }
            }
            catch (Exception er) {
                Invoke(new Action(() => { MessageBox.Show(er.Message + "\r\n" + er.StackTrace, "Ошибка при получении"); }));
                throw er;
            }
        }
        // Передаёт полученные данные с окулоинтерфейса во все активные формы
        private void AnnounceData(float value1, float value2, float value3, float value4) {
            if (!Running) return;
            DiscreteTrainingForm?.PutData(value1, value2, value3, value4);
            ChartForm?.PutData(value1, value2, value3, value4);
            CursorControl?.PutData(value1, value2, value3, value4);
            ShowAnglesForm?.PutData(value1, value2, value3, value4);
        }
        // Инициализирует объект, осуществляющий запись данных, полученных с окулоинтерфейса, а также предсказываемых моделью углов, точки внимания, исполняемой и непосредственно выбитой пользовтелем команды
        public void StartSaving(string filePath) {
            if (Saving) return;
            if (WriteOculo != null && WriteEEG != null) return;
            // Инициализация событий, которые будут записывать данные в файлы.
            DateTime startTime = DateTime.Now;
            var dateStr = "[" + startTime.ToString("yyyy-MM-dd HH-mm-ss-fff") + "]";
            if (filePath == null) filePath = "";
            int indSlash = filePath.LastIndexOf('/');
            if (indSlash == -1) indSlash = filePath.LastIndexOf('\\');
            string beforeLastSlash = indSlash == -1 ? "" : filePath.Substring(0, indSlash + 1);
            string afterLastSlash = indSlash == -1 ? filePath : filePath.Substring(indSlash + 1);
            string saveFolder = "Results\\";
            if (beforeLastSlash != "") saveFolder += beforeLastSlash + "\\";
            string saveFileOculo = saveFolder;
            string saveFileEEG = saveFolder;
            string saveGravity1 = saveFolder;
            string saveGravity2 = saveFolder;
            if (afterLastSlash != "") {
                saveFileOculo += afterLastSlash + "_";
                saveFileEEG += afterLastSlash + "_";
            }
            saveFileOculo += "oculo_" + dateStr;
            saveFileEEG += "eeg_" + dateStr;
            saveGravity1 += "sensor1_" + dateStr;
            saveGravity2 += "sensor2_" + dateStr;
            if (!Directory.Exists(saveFolder)) Directory.CreateDirectory(saveFolder);
            if (checkBoxWriteOculo.Checked) {
                WriteOculo = (data) => {
                    lock (lockObjOculoWrite) {
                        DateTime write_time = DateTime.Now;
                        double elapsed = (write_time - startTime).TotalSeconds;
                        using (StreamWriter writer = new StreamWriter(saveFileOculo, append: true)) {
                            PointF p = new PointF(float.MaxValue, float.MaxValue);
                            double angleX = double.MaxValue;
                            double angleY = double.MaxValue;
                            if (Trainer != null && Trainer.Confirmed) {
                                p = Trainer.Predict(data[0], data[1], data[2], data[3]);
                                p.X = (float)Math.Round(p.X, 1);
                                p.Y = (float)Math.Round(p.Y, 1);
                                var angles = Trainer.PredictAngles(data[0], data[1], data[2], data[3]);
                                angleX = Math.Round(angles.X, 1);
                                angleY = Math.Round(angles.Y, 1);
                            }
                            int commandTarget = -1;
                            int commandCurrent = -1;
                            PointF targetPoint = new PointF();
                            int sleeping = 0;
                            int exectingCommandInd = -1;
                            int calibrating = 0;
                            if (DiscreteTrainingForm != null) {
                                commandTarget = DiscreteTrainingForm.TargetCommand;
                                commandCurrent = DiscreteTrainingForm.ExecutingCommand;
                                exectingCommandInd = DiscreteTrainingForm.ExecutingCommandInd;
                                targetPoint = DiscreteTrainingForm.TargetPoint;
                                sleeping = DiscreteTrainingForm.Sleeping? 1 : 0;
                                calibrating = DiscreteTrainingForm.IsCalibrating ? 1 : 0;
                            }
                            writer.Write(Math.Round(elapsed, 4) + "\t" + calibrating + "\t" + exectingCommandInd + "\t");
                            writer.Write(data[0] + "\t" + data[1] + "\t" + data[2] + "\t" + data[3]);
                            if (angleX == double.MaxValue) writer.Write("\tNaN\tNaN\tNaN\tNaN");
                            else writer.Write("\t" + p.X + "\t" + p.Y + "\t" + angleX + "\t" + angleY);
                            if (commandCurrent == -1) writer.Write("\tNaN");
                            else writer.Write("\t" + commandCurrent);
                            if (commandTarget == -1)
                            {
                                writer.Write("\tNaN\tNaN\tNaN\tNaN");
                                writer.Write("\tNaN"); // sleeping
                            }
                            else
                            {
                                double screenHeight = (double)numericUpDownScreenHeight.Value;
                                double distanceFromScreen = (double)numericUpDownDistanceFromScreen.Value;
                                var area = DiscreteTrainingForm.WorkingArea;
                                var screen = DiscreteTrainingForm.CurrentScreen;
                                var screenSize = screen.Bounds.Size;
                                var angles = OculoDataRegressor.ConvertToAngles(targetPoint, distanceFromScreen,
                                    area, screenSize, screenHeight);

                                double angX = angles.X;
                                double angY = angles.Y;
                                writer.Write("\t" + Math.Round(targetPoint.X, 1) + "\t" + Math.Round(targetPoint.Y, 1));
                                writer.Write("\t" + Math.Round(angX, 1) + "\t" + Math.Round(angY, 1));
                                writer.Write("\t" + commandTarget);
                                writer.Write("\t" + sleeping);
                            }
                            writer.WriteLine();
                            writer.Flush();
                        }
                    }
                };
            }
            else WriteOculo = null;
            if (checkBoxWriteEEG.Checked) {
                WriteEEG = (data) => {
                    lock (lockObjEEGWrite) {
                        using (StreamWriter writer = new StreamWriter(saveFileEEG, append: true)) {
                            for (int i = 0; i < data.GetLength(0); i++)
                                for (int j = 0; j < data.GetLength(1); j++)
                                    writer.Write(data[i, j] + "\t");
                        }
                    }
                };
            }
            else WriteEEG = null;
            Saving = true;
        }
        public void StopSaving() {
            Saving = false;
            WriteOculo = null;
            WriteEEG = null;
        }
        public void StartProcess() {
            if (radioButtonUseWeightedWindow.Checked) {
                if (m_loadedWeights == null || m_loadedWeights.Length == 0) {
                    MessageBox.Show("Загрузите веса для взвешенного скользящего среднего!");
                    StopProcess();
                    return;
                }
                m_slidingWindowSmoother = new SlidingWindowSmoother[4];
                m_slidingWindowSmoother[0] = new SlidingWindowSmoother(m_loadedWeights);
                m_slidingWindowSmoother[1] = new SlidingWindowSmoother(m_loadedWeights);
                m_slidingWindowSmoother[2] = new SlidingWindowSmoother(m_loadedWeights);
                m_slidingWindowSmoother[3] = new SlidingWindowSmoother(m_loadedWeights);
            } else if (radioButtonUseSimpleWindow.Checked) {
                var windSize = (int)nAverageSize.Value;
                var weights = Enumerable.Repeat(1.0, windSize).ToArray();
                m_slidingWindowSmoother = new SlidingWindowSmoother[4];
                m_slidingWindowSmoother[0] = new SlidingWindowSmoother(weights);
                m_slidingWindowSmoother[1] = new SlidingWindowSmoother(weights);
                m_slidingWindowSmoother[2] = new SlidingWindowSmoother(weights);
                m_slidingWindowSmoother[3] = new SlidingWindowSmoother(weights);
            } else {
                m_slidingWindowSmoother = new SlidingWindowSmoother[4];
                m_slidingWindowSmoother[0] = new SlidingWindowSmoother(new double[] { 1 });
                m_slidingWindowSmoother[1] = new SlidingWindowSmoother(new double[] { 1 });
                m_slidingWindowSmoother[2] = new SlidingWindowSmoother(new double[] { 1 });
                m_slidingWindowSmoother[3] = new SlidingWindowSmoother(new double[] { 1 });
            }
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            Running = true;
            buttonStartProcess.Text = "Прервать";
            buttonShowCommands.Enabled = true;
        }
        public void StopProcess() {
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            Running = false;
            buttonStartProcess.Text = "Пуск";
            buttonShowCommands.Enabled = false;
            if (DiscreteTrainingForm != null) {
                if (!DiscreteTrainingForm.IsDisposed) DiscreteTrainingForm.Dispose();
                DiscreteTrainingForm = null;
            }
        }
        private void buttonLoadWeights_Click(object sender, EventArgs e) {
            if (openFileDialogSelectWeights.ShowDialog() == DialogResult.OK) {
                string fileName = openFileDialogSelectWeights.FileName;
                try {
                    using (StreamReader r = new StreamReader(fileName)) {
                        var elems = r.ReadToEnd().Split().Where(el => el != "").ToArray();
                        m_loadedWeights = elems.Select(el => double.Parse(el)).ToArray();
                    }
                    buttonShowWeights.Enabled = true;
                }
                catch (Exception ex) {
                    buttonShowWeights.Enabled = false;
                    MessageBox.Show("Ошибка:" + ex.Message, "Не удалось загрузить веса");
                }
            }
        }
        private void buttonShowWeights_Click(object sender, EventArgs e) {
            if (m_loadedWeights == null) {
                MessageBox.Show("Пока ничего не загружено");
                return;
            }
            ShowWeightsForm form = new ShowWeightsForm(m_loadedWeights);
            form.ShowDialog();
        }
        private void buttonConnectServer_Click(object sender, EventArgs e) {
            if (clientControl.Client.IsRunning) clientControl.Client.StopClient();
            else clientControl.Client.StartClient();
        }
        private void buttonStartProcess_Click(object sender, EventArgs e)
        {
            if (!Running) {
                StartProcess();
            } else {
                StopProcess();
            }
        }
        private void radioButtonUseWeightedWindow_CheckedChanged(object sender, EventArgs e) {
            panelWeightedWeights.Enabled = radioButtonUseWeightedWindow.Checked;
        }

        private void radioButtonUseSimpleWindow_CheckedChanged(object sender, EventArgs e) {
            nAverageSize.Enabled = radioButtonUseSimpleWindow.Checked;
        }
        private void buttonShowAngles_Click(object sender, EventArgs e) {
            ShowAnglesForm = new ShowAngles(() => Trainer);
            ShowAnglesForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Screen myScreen = Screen.FromControl(this);
            int y = myScreen.WorkingArea.Height;
            int x = myScreen.WorkingArea.Width;

            var loc = this.Location;
            int k = 0;
        }
    }
}