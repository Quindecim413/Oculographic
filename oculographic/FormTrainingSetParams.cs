using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace oculographic
{
    public partial class FormTrainingSetParams : Form
    {
        readonly FormTraining formTraining;
        List<FieldConfig> _fields = new List<FieldConfig>();
        private FieldConfig ActiveFieldConfig => _fields[comboBoxSelectConfiguration.SelectedIndex];
        public FormTrainingSetParams(FormTraining formTraining)
        {
            InitializeComponent();
            this.formTraining = formTraining;
            formTraining.OnCompleteTraining += (el, rectangle, screenSize) =>
            {
                buttonCalibrate.Text = "Калибровать";
                buttonRun.Enabled = true;
                RunFreePlay();
            };
            formTraining.OnCompleteProgram += () =>
            {
                buttonCalibrate.Enabled = true;
                StopProgram();
                MessageBox.Show("Исполнение завершено");
            };
        }
        // Производится остановка или преждевременная остановка процесса калибровки, в зависимости от состояния, в котором система находится в данный момент
        private void buttonCalibrate_Click(object sender, EventArgs e)
        {
            if (formTraining.IsCalibrating) InterruptCalibration();
            else RunCalibration();
        }
        // Инициализация процесса калибровки с заданным параметров времени калибровки на каждую возможную команду на выбранной конфигурации поля
        private void RunCalibration()
        {
            buttonCalibrate.Text = "Прервать";
            buttonRun.Enabled = false;
            formTraining.Show();
            formTraining.RunCalibration((double)numericUpDownWaiting.Value, textBoxSaveFilePath.Text);
            formTraining.WindowState = FormWindowState.Maximized;
            Hide();
        }
        // Преждевременная остановка процесса калибровки
        private void InterruptCalibration()
        {
            buttonCalibrate.Text = "Калибровать";
            buttonRun.Enabled = true;
            formTraining.InterruptCalibration();
            formTraining.WindowState = FormWindowState.Normal;
        }
        // Производится остановка или запуск процесса тренировки, в зависимости от состояния, в котором система находится в данный момент
        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (formTraining.IsRunning)
            {
                StopProgram();
                formTraining.StopDiscrete();
            }
            else
            {
                RunDiscrete();
            }
        }
        // Запуск программы выполнения тренировки с или без программы
        private void RunDiscrete()
        {
            Hide();
            textBoxCommandsList.Enabled = false;
            textBoxSaveFilePath.Enabled = false;
            buttonCalibrate.Text = "Калибровать";
            buttonRun.Text = "Прервать";
            buttonRun.Enabled = true;
            buttonCalibrate.Enabled = false;
            try
            {
                formTraining.Show();
                formTraining.WindowState = FormWindowState.Maximized;
            }
            catch { }
            if (textBoxCommandsList.Text.Trim().Length == 0)
                formTraining.RunProgram(textBoxSaveFilePath.Text, null, 
                (double)numericUpDownIntegrateCommand.Value, (double)numericUpDownMaxPerformCommand.Value, (double)numericUpDownSleepAfterCommand.Value);
            else
            {
                int[] commands = null;
                try { commands = textBoxCommandsList.Text.Trim().Split().Where(el=>el != "").Select(el => int.Parse(el)).ToArray(); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка парсинга команд");
                    StopProgram();
                    return;
                }
                
                formTraining.RunProgram(textBoxSaveFilePath.Text, commands, 
                 (double)numericUpDownIntegrateCommand.Value, (double)numericUpDownMaxPerformCommand.Value, (double)numericUpDownSleepAfterCommand.Value);
                buttonRun.Text = "Прервать";
                buttonCalibrate.Enabled = false;
            }
        }

        internal void RunFreePlay()
        {
            formTraining.RunProgram("tmp", null, 
                (int)numericUpDownIntegrateCommand.Value, (int)numericUpDownMaxPerformCommand.Value, 0);
        }

        // Остановка любой запущенной программы
        private void StopProgram()
        {
            buttonRun.Text = "Запустить";
            buttonCalibrate.Enabled = true;
            buttonCalibrate.Enabled = true;
            textBoxCommandsList.Enabled = true;
            textBoxSaveFilePath.Enabled = true;
        }
        // Переключение конфигурации поля точек
        private void comboBoxSelectConfiguration_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectConfiguration.SelectedIndex == -1) return;
            formTraining.SetActiveField(ActiveFieldConfig);
        }
        // Производится задание конфигураций для всех доступных полей точек внимания
        private void FormTrainingSetParams_Load(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>();
            FieldConfig f = new FieldConfig();
            f.Name = "5 точек v1";
            f.TrainingSequencies = new byte[,,] {
   {
    {1, 0, 0, 0, 1},
    {0, 0, 0, 0, 0},
    {0, 0, 2, 0, 0},
    {0, 0, 0, 0, 0},
    {1, 0, 0, 0, 1},
   },
   {
    {2, 0, 0, 0, 1},
    {0, 0, 0, 0, 0},
    {0, 0, 1, 0, 0},
    {0, 0, 0, 0, 0},
    {1, 0, 0, 0, 1},
   },
   {
    {1, 0, 0, 0, 2},
    {0, 0, 0, 0, 0},
    {0, 0, 1, 0, 0},
    {0, 0, 0, 0, 0},
    {1, 0, 0, 0, 1},
   },
   {
    {1, 0, 0, 0, 1},
    {0, 0, 0, 0, 0},
    {0, 0, 1, 0, 0},
    {0, 0, 0, 0, 0},
    {2, 0, 0, 0, 1},
   },
   {
    {1, 0, 0, 0, 1},
    {0, 0, 0, 0, 0},
    {0, 0, 1, 0, 0},
    {0, 0, 0, 0, 0},
    {1, 0, 0, 0, 2},
   },
  };
            f.Numbers = new int[5, 5] {
   {1, 0, 0, 0, 2},
   {0, 0, 0, 0, 0},
   {0, 0, 0, 0, 0},
   {0, 0, 0, 0, 0},
   {3, 0, 0, 0, 4},
  };
            fields.Add(f);
            f = new FieldConfig();
            f.Name = "5 точек v2";
            f.TrainingSequencies = new byte[,,] {
   {
    {0, 0, 1, 0, 0},
    {0, 0, 0, 0, 0},
    {1, 0, 2, 0, 1},
    {0, 0, 0, 0, 0},
    {0, 0, 1, 0, 0},
   },{
    {0, 0, 2, 0, 0},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {0, 0, 1, 0, 0},
   },{
    {0, 0, 1, 0, 0},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {0, 0, 2, 0, 0},
   },{
    {0, 0, 1, 0, 0},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 2},
    {0, 0, 0, 0, 0},
    {0, 0, 1, 0, 0},
   },{
    {0, 0, 1, 0, 0},
    {0, 0, 0, 0, 0},
    {2, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {0, 0, 1, 0, 0},
   },
  };
            f.Numbers = new int[5, 5] {
   {0, 0, 1, 0, 0},
   {0, 0, 0, 0, 0},
   {4, 0, 0, 0, 3},
   {0, 0, 0, 0, 0},
   {0, 0, 2, 0, 0},
  };
            fields.Add(f);
            f = new FieldConfig();
            f.Name = "7 точек";
            f.TrainingSequencies = new byte[,,] {
   {
    {1, 0, 0, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 2, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 0, 0, 1},
   },
   {
    {1, 0, 0, 0, 1},
    {0, 0, 0, 0, 0},
    {2, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 0, 0, 1},
   },
   {
    {1, 0, 0, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 2},
    {0, 0, 0, 0, 0},
    {1, 0, 0, 0, 1},
   },
   {
    {2, 0, 0, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 0, 0, 1},
   },
   {
    {1, 0, 0, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {2, 0, 0, 0, 1},
   },
   {
    {1, 0, 0, 0, 2},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 0, 0, 1},
   },
   {
    {1, 0, 0, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 0, 0, 2},
   }
  };
            f.Numbers = new int[5, 5] {
   {3, 0, 0, 0, 5},
   {0, 0, 0, 0, 0},
   {1, 0, 0, 0, 2},
   {0, 0, 0, 0, 0},
   {4, 0, 0, 0, 6},
  };
            fields.Add(f);
            f = new FieldConfig();
            f.Name = "9 точек";
            f.TrainingSequencies = new byte[,,] {
   {
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 2, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1}
   },
   {
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {2, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1}
   },
   {
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 2},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1}
   },
   {
    {2, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1}
   },
   {
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {2, 0, 1, 0, 1}
   },
   {
    {1, 0, 1, 0, 2},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1}
   },
   {
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 2}
   },
   {
    {1, 0, 2, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1}
   },
   {
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 1, 0, 1},
    {0, 0, 0, 0, 0},
    {1, 0, 2, 0, 1}
   }
  };
            f.Numbers = new int[5, 5] {
   {3, 0, 7, 0, 5},
   {0, 0, 0, 0, 0},
   {1, 0, 0, 0, 2},
   {0, 0, 0, 0, 0},
   {4, 0, 8, 0, 6},
  };
            fields.Add(f);
            f = new FieldConfig();
            f.Name = "Разреженная";
            f.TrainingSequencies = new byte[,,] {
   {
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 2, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1}
   },{
    {1, 0, 1, 0, 1},
    {0, 2, 0, 1, 0},
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1}
   },{
    {1, 0, 1, 0, 1},
    {0, 1, 0, 2, 0},
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1}
   },{
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1},
    {0, 1, 0, 2, 0},
    {1, 0, 1, 0, 1}
   },{
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1},
    {0, 2, 0, 1, 0},
    {1, 0, 1, 0, 1}
   },{
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {2, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1}
   },{
    {2, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1}
   },{
    {1, 0, 2, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1}
   },{
    {1, 0, 1, 0, 2},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1}
   },{
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 2},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1}
   },{
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 2}
   },{
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 2, 0, 1}
   },{
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {1, 0, 1, 0, 1},
    {0, 1, 0, 1, 0},
    {2, 0, 1, 0, 1}
   },
  };
            f.Numbers = new int[5, 5] {
   { 6, 0, 7, 0, 8},
   { 0, 1, 0, 2, 0},
   { 5, 0, 0, 0, 9 },
   { 0, 4, 0, 3, 0},
   { 12, 0, 11, 0, 10}
  };
            fields.Add(f);
            f = new FieldConfig();
            f.Name = "Полная";
            f.TrainingSequencies = new byte[,,] {
   {
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 2, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 2, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 2, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 2, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 2, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 2, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 2, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 2, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 2, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {2, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {2, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {2, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 2, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 2, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 2, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 2},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 2},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 2},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 2},
    {1, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 2}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 2, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 2, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 2, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {2, 1, 1, 1, 1}
   },{
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1},
    {2, 1, 1, 1, 1},
    {1, 1, 1, 1, 1}
   },
  };
            f.Numbers = new int[5, 5] {
   {11, 12, 13, 14, 15},
   {10, 2, 3, 4, 16},
   {9, 1, 0, 5, 17},
   {24, 8, 7, 6, 18},
   {23, 22, 21, 20, 19}
  };
            fields.Add(f);
            ValidateField(fields);
            _fields = fields;
            foreach (var field in fields) comboBoxSelectConfiguration.Items.Add(field.Name);
            if (_fields.Count > 0) comboBoxSelectConfiguration.SelectedIndex = 0;
        }
        // Проверка, что все поля маркеров команд заданы корректно
        public bool ValidateField(List<FieldConfig> fields)
        {
            foreach (var f in fields)
                try { f.Validate(); }
                catch (Exception e)
                {
                    MessageBox.Show("Ошибка в слое " + f.Name + "\r\n" + e.Message, "Валидация провалилась");
                    return false;
                }
            return true;
        }

        private void checkBoxHideCommandDone_CheckedChanged(object sender, EventArgs e)
        {
            formTraining.HideCursor = checkBoxHideCommandDone.Checked;
        }

        private void buttonRandomizeCommands_Click(object sender, EventArgs e)
        {
            int nCommands = (int)numericUpDownNCommands.Value;
            int maxCommand = 0;
            for (int i = 0; i < ActiveFieldConfig.Numbers.GetLength(0); i++)
            {
                for(int j = 0; j < ActiveFieldConfig.Numbers.GetLength(1); j++)
                {
                    maxCommand = Math.Max(maxCommand, ActiveFieldConfig.Numbers[i, j]);
                }
            }
            CommandsRandomizer randomizer = new CommandsRandomizer(nCommands, maxCommand);
            var generatedCommands = randomizer.Generate();
            textBoxCommandsList.Text = String.Join(" ", generatedCommands.Select(el => el.ToString()));
        }
    }
}
