using System;
using System.Drawing;
using System.Windows.Forms;

namespace oculographic {
public partial class ShowAngles : Form {
 Func<Approximation.OculoTraining> RetrieveTrainer;
 const int POINT_RADIUS = 10;
 public ShowAngles(Func<Approximation.OculoTraining> retrieveTrainer) {
  InitializeComponent();
  RetrieveTrainer = retrieveTrainer;
  pictureBox.Dock = DockStyle.Fill;
  pictureBox.SendToBack();
 }
 // Отображение углов предсказываемых углов отклона по горизонтали и по вертикали значка от центра поля зрения оператора
 public void PutData(float value1, float value2, float value3, float value4) {
  var trainer = RetrieveTrainer();
  if (trainer!=null && trainer.Confirmed) {
   var angles = trainer.PredictAngles(value1, value2, value3, value4);
   double angleX = angles.X;
   double angleY = angles.Y;

   angleX = Math.Max(Math.Min(angleX, 90), -90);
   angleY = Math.Max(Math.Min(angleY, 90), -90);
   double posX = (angleX + 90) / 180 * Width * 0.8 + Width * 0.1;
   double posY = (angleY + 90) / 180 * Height* 0.8 + Height * 0.1;
   Text = "Углы ("+ Math.Round(angleX, 2) + ", " + Math.Round(angleY, 2) +")";
   Bitmap bmp = new Bitmap(Width, Height);
   using(Graphics g = Graphics.FromImage(bmp)) {
    using(Brush b = new SolidBrush(Color.Red)){
     g.FillEllipse(b, (float)(posX - POINT_RADIUS), (float)(posY - POINT_RADIUS), POINT_RADIUS * 2, POINT_RADIUS * 2);
    }
   }
   pictureBox.Image = bmp;
   pictureBox.Invalidate();
  }
 }
}
}
