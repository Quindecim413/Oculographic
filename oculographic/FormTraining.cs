using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OculoControls;

namespace oculographic
{
public partial class FormTraining : Form {
 Action<string> StartSaving;
 Action StopSaving;
 Action<int> SendCommand;

 DateTime lastCommandChange = DateTime.Now;
 DateTime lastConfirm = DateTime.Now;

 int _currentCalibrationLayerInd = 0;
 int _gotPointsForCommand = 0;

 public bool HideCursor;

 int[] programCodes = null;
 int currentProgramCodeInd = 0;
 double confirmationSec, maxRunSec, sleepSec;

 FieldConfig ActiveFieldConfig { get; set; }
 FormTrainingSetParams paramsForm;
 List<Tuple<DateTime, int>> commandsStored = new List<Tuple<DateTime, int>>();

 public Approximation.OculoTraining Trainer { get; private set; }
 public event Action OnStartTraining;
 public event Action<Approximation.OculoTraining, Rectangle, Size> OnCompleteTraining;
 public event Action OnCompleteProgram;

 public int ExecutingCommand { get; private set; } = -1;
 public int TargetCommand { get; private set; } = -1;
 public bool Sleeping { get; private set; } = true;
 public PointF TargetPoint { get; private set; } = new PointF();
 public bool IsCalibrating { get; private set; } = false;
 public bool IsRunning { get; private set; } = false;
 public int ExecutingCommandInd => IsCalibrating? _currentCalibrationLayerInd : currentProgramCodeInd;

 public FormTraining(Action<string> startSaving, Action stopSaving, Action<int> sendCommand) {
  InitializeComponent();
  StartSaving = startSaving;
  StopSaving = stopSaving;
  SendCommand = sendCommand;
 }
 public void SetActiveField(FieldConfig field) {
  ActiveFieldConfig = field;
  discreteCommandsDrawer1.ShowData(field.GetLayer(0), field.Numbers);
 }
 // Инициализация состояния калибровки для данной формы и вызов соответствующего обратного вызова
 internal void RunCalibration(double waitCommandSec, string savePath) {
  Sleeping = false;
  labelCommands.Text = "-";
  this.sleepSec = 0;
  WindowState = FormWindowState.Maximized;
  ExecutingCommand = -1;

  Task.Delay(300).ContinueWith(t => {
   Invoke(new Action(() => {
    IsCalibrating = true;
    _currentCalibrationLayerInd = -1;
    _gotPointsForCommand = 0;
    Trainer = new Approximation.OculoTraining();
    timer.Interval = (int)(waitCommandSec * 1000);
    timer.Start();
    OnStartTraining.Invoke();
    NextCalibrationPoint(null, null);
    StartSaving(savePath);
   }));
  });
 }
 // Обработка перерывания калибровки для данной формы и вызов соответствующего обратного вызова
 internal void InterruptCalibration() {
  IsCalibrating = false;
  Trainer = null;
  timer.Stop();
  StopSaving();
 }
 // Переход к следующей точке калибровки или завершение калибровки в случае исчерпания точек
 private void NextCalibrationPoint(object sender, EventArgs e) {
  _currentCalibrationLayerInd++;
  double expectedPoints = (timer.Interval / 1000.0) * 0.9 * (1/SamplingTime);
  if (_gotPointsForCommand < expectedPoints && _currentCalibrationLayerInd != 0) {
   _currentCalibrationLayerInd--;
   return;
  }
  labelStatus.Text = "Калибровка " + (_currentCalibrationLayerInd + 1) + "/" + ActiveFieldConfig.TotalLayers;
  if (_currentCalibrationLayerInd == ActiveFieldConfig.TotalLayers) 
   FinishCalibration();
  else {
   _gotPointsForCommand = 0;
   var indsOfActive = ActiveFieldConfig.GetActivePointOnLayer(_currentCalibrationLayerInd);
   var field = ActiveFieldConfig;
   discreteCommandsDrawer1.ShowData(field.GetLayer(_currentCalibrationLayerInd), field.Numbers);
   var controlPosition = discreteCommandsDrawer1.Location;
   var workingArea = WorkingArea;
   var controlAbsoluteLocation = new Point(workingArea.X, workingArea.Y);
   var locationOfActive = discreteCommandsDrawer1.PointsLocations[indsOfActive.Y, indsOfActive.X];
   TargetPoint = locationOfActive;
   TargetCommand = field.Numbers[indsOfActive.Y, indsOfActive.X];
   var fittingPoint = new PointF(locationOfActive.X + controlAbsoluteLocation.X, locationOfActive.Y + controlAbsoluteLocation.Y);
   Console.WriteLine("Fitting new Point: " + fittingPoint.X + " " + fittingPoint.Y);
   Trainer.StartFittingPoint(fittingPoint);
  }
 }

 Screen m_screen;
 public Screen CurrentScreen { get => m_screen; }
 public Rectangle WorkingArea {
  get {
   var screen = CurrentScreen;
   Rectangle workingArea = new Rectangle(this.Location.X + this.discreteCommandsDrawer1.Location.X - screen.Bounds.Left,
             this.Location.Y + this.discreteCommandsDrawer1.Location.Y - screen.Bounds.Top,
             this.discreteCommandsDrawer1.Width,
             this.discreteCommandsDrawer1.Height);
   return workingArea;
  }
 }
 // Завершение фазы калибровки модели и вызов соответствующего обратного вызова
 private void FinishCalibration() {
  IsCalibrating = false;
  timer.Stop();
  labelStatus.Text = "Остановлено";
  OnCompleteTraining.Invoke(Trainer, WorkingArea, CurrentScreen.Bounds.Size);
  if (programCodes != null)  RunDiscrete(null);
  else StopDiscrete();
  TargetPoint = new PointF();
  TargetCommand = -1;
 }
 // Отображение на форме процесса набора команд и вызов соответствующих обратного вызова и событий
 internal void StopDiscrete() {
  Action act = () => {
   labelCommands.Text = "-";
   IsRunning = false;
   programCodes = null;
   try {
    labelStatus.Text = "Остановлено";
    StopSaving();
    OnCompleteProgram?.Invoke();
   }
   catch (Exception e) {
    MessageBox.Show(e.Message + "\r\n" + e.StackTrace, "Error");
   }
  };
  if (InvokeRequired) Invoke(act);
  else act();
 }
 // Запуск процесса набора команд
 internal void RunDiscrete(string filePath) {
  IsRunning = true;
  if (Trainer == null) {
   MessageBox.Show("Сначала проведите калибровку");
   StopDiscrete();
   return;
  }
  if (!Trainer.Confirmed) {
   MessageBox.Show("Сначала завершите калибровку");
   StopDiscrete();
   return;
  }
  StartSaving(filePath);
 }
 // Инициализирует процесс тренировки
 internal void RunProgram(string filePath, int[] programCodes, double confirmationSec, double maxRunSec, double sleepSec) {
  IsRunning = true;
  this.programCodes = programCodes;
  this.confirmationSec = confirmationSec;
  this.maxRunSec = maxRunSec;
  this.sleepSec = sleepSec;
  currentProgramCodeInd = 0;
  lastCommandChange = DateTime.Now;
  if (Trainer == null) {
   MessageBox.Show("Сначала проведите калибровку");
   StopDiscrete();
   return;
  }
  if (!Trainer.Confirmed) {
   MessageBox.Show("Сначала завершите калибровку");
   StopDiscrete();
   return;
  }
  StartSaving(filePath);
 }
 // Генерирует данные для активной точки калибровки, если калибровка не завершена, в противном случае, при наличии программы тренировки выполняет её. Если программа не назначена, просто подсвечивает команду, ближайшую к точке внимания оператора
 DateTime LastRecieve;
 bool IsFirstRecieveValues = true;
 double SamplingTime = 0;

 DateTime lastFieldUpdate;
 const double MIN_SECOND_FOR_FIELD_UPDATE = 1.0 / 8.0; 
 // Очистка буфера накопленных комманд
 void ClearStoredCommands() {
  commandsStored = new List<Tuple<DateTime, int>>((int)(confirmationSec * 1.2 * 1 / SamplingTime));
 }
 bool requireNextCommand = false;
 public void PutData(float value1, float value2, float value3, float value4) {
  var now = DateTime.Now;
  if (IsFirstRecieveValues) {
   IsFirstRecieveValues = false;
   LastRecieve = DateTime.Now;
   lastFieldUpdate = LastRecieve;
   return;
  }
  else {
   double dt = (now - LastRecieve).TotalSeconds;
   LastRecieve = now;
   SamplingTime = 0.9 * SamplingTime + 0.1 * dt;
   Console.WriteLine("FPS: " + (1 / SamplingTime));
  }
  if (Trainer == null) return;
  if (!Trainer.Confirmed) {
   _gotPointsForCommand++;
   Trainer.AddNewValuesToTrainingPoint(value1, value2, value3, value4);
  } else {
   var predictedPoint = Trainer.Predict(value1, value2, value3, value4);
   var currentlyLocatedPoints = discreteCommandsDrawer1.PointsLocations;
   var pointsMap = discreteCommandsDrawer1.PointsMap;
   var numbersMap = discreteCommandsDrawer1.Numbers;
   int iMin = -1;
   int jMin = -1;
   double minDistanceSqr = double.MaxValue;
   int command = -1;
   if (requireNextCommand) {
    requireNextCommand = false;
    currentProgramCodeInd++;
    if (currentProgramCodeInd == programCodes.Length) StopDiscrete();
   }
   if (programCodes != null) {   
    if ((now - lastCommandChange).TotalSeconds > maxRunSec){
     currentProgramCodeInd++;
     lastCommandChange = now;
     ClearStoredCommands();
     lastConfirm = DateTime.Now;
    }
    if (currentProgramCodeInd == programCodes.Length) {
     StopDiscrete();
     return;
    }
    command = programCodes[currentProgramCodeInd];
   }
   // Определяем точку попадания взгляда пользователя
   for (int i = 0; i < pointsMap.GetLength(0); i++) {
    for (int j = 0; j < pointsMap.GetLength(1); j++) {
     if (pointsMap[i, j] != DiscreteCommandsDrawer.NOT_A_POINT) {
      pointsMap[i, j] = DiscreteCommandsDrawer.POINT_NON_TARGET;
      var point = currentlyLocatedPoints[i, j];
      double distanceSqr = Math.Pow(point.X - predictedPoint.X, 2) + Math.Pow(point.Y - predictedPoint.Y, 2);
      if (distanceSqr < minDistanceSqr) {
       minDistanceSqr = distanceSqr;
       iMin = i;
       jMin = j;
      }
     }
    }
   }
   Sleeping = (now - lastConfirm).TotalSeconds < sleepSec;
   if (Sleeping) {
    Console.WriteLine(DateTime.Now + " Sleep");
    // Данный блок сбрасывает выполнение команды в тот момент, пока не прошел таймаут "сна" после подтверждения предыдущей команды
    // Полностью зачищаем поле
    for (int i = 0; i < pointsMap.GetLength(0); i++)
     for (int j = 0; j < pointsMap.GetLength(1); j++) {
      if (pointsMap[i, j] != DiscreteCommandsDrawer.NOT_A_POINT) {
       pointsMap[i, j] = DiscreteCommandsDrawer.POINT_NON_TARGET;
      }
     }
    if (!this.HideCursor) {
     pointsMap[iMin, jMin] = DiscreteCommandsDrawer.POINT_NON_TARGET_SELECTED;
    }
    if (commandsStored.Count > 0) commandsStored.Clear();
   } else {
    TargetCommand = command;
    for (int i = 0; i < pointsMap.GetLength(0); i++)
     for (int j = 0; j < pointsMap.GetLength(1); j++) {
      if (numbersMap[i, j] == TargetCommand && pointsMap[i, j] != DiscreteCommandsDrawer.NOT_A_POINT) {
       pointsMap[i, j] = DiscreteCommandsDrawer.POINT_TARGET;
      }
     }
    if (iMin != -1) {
     if (!this.HideCursor) {
      if (pointsMap[iMin, jMin] == DiscreteCommandsDrawer.POINT_TARGET) // Ячейка была отмечена как цель 
       pointsMap[iMin, jMin] = DiscreteCommandsDrawer.POINT_TARGET_SELECTED; // Попало в цель
      else pointsMap[iMin, jMin] = DiscreteCommandsDrawer.POINT_NON_TARGET_SELECTED; // Не попало в цель
     }
     ExecutingCommand = numbersMap[iMin, jMin];
     TargetPoint = currentlyLocatedPoints[iMin, jMin];
    }
   }
   if ((now - lastFieldUpdate).TotalSeconds > MIN_SECOND_FOR_FIELD_UPDATE) {
    lastFieldUpdate = now;
    Action act = () => {
     discreteCommandsDrawer1.ShowData(pointsMap, discreteCommandsDrawer1.Numbers);
     if (programCodes != null) {
      labelStatus.Text = "Выполнение команды: " + command + " (" + (currentProgramCodeInd + 1) + "/" + (programCodes.Length) + ")";
      labelCommands.Text = String.Join("-", programCodes.Select((el, ind) => ind == currentProgramCodeInd ? ("[" + el.ToString() + "]") : el.ToString()));
     }
    };
    if (InvokeRequired) Invoke(act);
    else act();
   }
   if (iMin != -1){
    int commandCur = discreteCommandsDrawer1.Numbers[iMin, jMin];
    commandsStored.Add(Tuple.Create(now, commandCur));
    commandsStored = commandsStored.Where(el => (now - el.Item1).TotalSeconds <= confirmationSec).ToList();
   }
   double expectedAmountOfSamples = confirmationSec * (1/SamplingTime);
   foreach (var el in commandsStored.GroupBy(el => el.Item2)){
    if (el.Count() > (int)(expectedAmountOfSamples * 0.95)){
     if (programCodes != null){
      // Случай когда идёт выполнение по программе
      int currentCommand = programCodes[currentProgramCodeInd];
      if (el.Key == currentCommand){
       lastConfirm = DateTime.Now;
       AnnounceCommand(el.Key);
       requireNextCommand = true;
       lastCommandChange = DateTime.Now;
       ClearStoredCommands();
      }
     }
     else{
      lastConfirm = DateTime.Now;
      AnnounceCommand(el.Key);
      commandsStored.Clear();
     }
     break;
    }
   }
  }
 }
 // Отправка набранной команды через соответствующий обратный вызов
 private void AnnounceCommand(int command){
  if (IsRunning) SendCommand(command);
 }
 private void buttonParams_Click(object sender, EventArgs e){
  if (paramsForm == null) paramsForm = new FormTrainingSetParams(this);
  paramsForm.Show();
 }
 private void FormTraining_Load(object sender, EventArgs e){
  UpdateScreen();
 }
 void UpdateScreen(){
  m_screen = Screen.FromControl(this);
 }

 private void FormTraining_Move(object sender, EventArgs e){
  UpdateScreen();
 }

 private void FormTraining_Resize(object sender, EventArgs e){
  UpdateScreen();
 }

 private void FormTraining_FormClosed(object sender, FormClosedEventArgs e){
  paramsForm.Close();
 }
 private void FormTraining_FormClosing(object sender, FormClosingEventArgs e){
  paramsForm.Hide();
 }
}
}