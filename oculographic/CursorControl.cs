using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace oculographic
{
    public partial class CursorControl : Form
    {
        Func<Approximation.OculoTraining> RetrieveTraining;
        public CursorControl(Func<Approximation.OculoTraining> retrieveTraining)
        {
            InitializeComponent();
            RetrieveTraining = retrieveTraining;
        }
        bool IsFirstRecieveValues = true;
        DateTime lastFieldUpdate;
        const double MIN_SECOND_FOR_FIELD_UPDATE = 1.0 / 8.0;

        // Если модель натренирована, то предказывает координаты точки,
        // на которую смотрит оператор и вызывает визуальное отображение
        public void PutData(float value1, float value2, float value3, float value4)
        {
            var now = DateTime.Now;
            if (IsFirstRecieveValues)
            {
                IsFirstRecieveValues = false;
                lastFieldUpdate = now;
                return;
            }

            if ((now - lastFieldUpdate).TotalSeconds < MIN_SECOND_FOR_FIELD_UPDATE)
            {
                return;
            }
            else
            {
                lastFieldUpdate = now;
            }

            var Trainer = RetrieveTraining();
            if (Trainer != null && Trainer.Confirmed)
            {
                var p = Trainer.Predict(value1, value2, value3, value4);
                DrawCursor((int)p.X, (int)p.Y);
            }
        }
        // Отрисовка точки внимания оператора на данном окне
        public void DrawCursor(int onScreenX, int onScreenY)
        {
            if (pictureBox1.Width <= 0 || pictureBox1.Height <= 0) return;
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            int currentX = this.Location.X + pictureBox1.Location.X;
            int currentY = this.Location.Y + pictureBox1.Location.Y;
            int onBoxX = onScreenX - currentX;
            int onBoxY = onScreenY - currentY;
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = (int)numericUpDownCursorRadius.Value;
                Rectangle rect = new Rectangle(onBoxX - radius, onBoxY - radius, 2 * radius, 2 * radius);
                gr.FillEllipse(Brushes.LightGreen, rect);
            }
            labelCoordinates.Location = new Point(onBoxX, onBoxY);
            labelCoordinates.Text = "[" + onScreenX + "," + onScreenY + "]";
            pictureBox1.Image = bm;
        }
    }
}
