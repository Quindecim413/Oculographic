using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OculoControls
{
    public partial class DiscreteCommandsDrawer : UserControl
    {
        private int _pSize = 10;
        public int PointRadius
        {
            get { return _pSize; }
            set
            {
                if (value < 1) _pSize = 1;
                else _pSize = value;
            }
        }
        public Color ActiveColor { get; set; }
        public Color NonActiveColor { get; set; }
        public Color TargetColor { get; set; }
        public Color SelectedTarget { get; set; }
        public DiscreteCommandsDrawer()
        {
            InitializeComponent();
            TargetColor = Color.Red;
            SelectedTarget = Color.LightGreen;
            ActiveColor = Color.Yellow;
            NonActiveColor = Color.DarkGray;
            ShowData(_pointsMap, Numbers);
        }
        private PointF[,] _pointsLocations = new PointF[0, 0];
        public PointF[,] PointsLocations
        {
            get => _pointsLocations;
            private set
            {
                if (value == null) _pointsLocations = new PointF[0, 0];
                else _pointsLocations = value;
            }
        }
        private byte[,] _pointsMap = new byte[3, 3]{
  { 1, 0, 1 },
  { 0, 2, 0 },
  { 1, 0, 1 },
 };
        public byte[,] PointsMap
        {
            get => _pointsMap;
            private set
            {
                if (value == null) _pointsMap = new byte[0, 0];
                else _pointsMap = value;
            }
        }
        public const byte NOT_A_POINT = 0;
        public const byte POINT_NON_TARGET = 1;
        public const byte POINT_TARGET = 2;
        public const byte POINT_TARGET_SELECTED = 3;
        public const byte POINT_NON_TARGET_SELECTED = 4;


        private int[,] _numbers = new int[3, 3]{
  { 1, 0, 2 },
  { 0, 0, 0 },
  { 3, 0, 4 },
 };
        public int[,] Numbers
        {
            get => _numbers;
            set
            {
                if (value == null) _numbers = new int[0, 0];
                else _numbers = value;
            }
        }
        private List<Tuple<int, PointF, int>> drawData = new List<Tuple<int, PointF, int>>();
        // Подготовка массива данных, используемого для отображения поля точек внимания
        public void ShowData(byte[,] data, int[,] numbers)
        {
            PointsMap = data;
            data = PointsMap;
            Numbers = numbers;
            float h = PointsMap.GetLength(0);
            float w = PointsMap.GetLength(1);
            var pointsLocations = new PointF[(int)h, (int)w];
            drawData.Clear();
            for (int i = 0; i < h; i++)
            {
                float startY = (Height / h) * (i + 0.5f);
                for (int j = 0; j < w; j++)
                {
                    float startX = (Width / w) * (j + 0.5f);
                    pointsLocations[i, j] = new PointF(startX, startY);
                    drawData.Add(Tuple.Create((int)data[i, j], pointsLocations[i, j], numbers[i, j]));
                }
            }
            PointsLocations = pointsLocations;
            Refresh();
        }
        // Отображение всех точек выбранного тренировочного поля с учетом их статуса (не активная, активная, но не выбрана пользователем и т.д.)
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Brush activeBr = new SolidBrush(ActiveColor);
            Brush nonActiveBr = new SolidBrush(NonActiveColor);
            Brush targetBr = new SolidBrush(TargetColor);
            Brush targetselectedBr = new SolidBrush(SelectedTarget);
            Brush blackBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Font font1 = new Font("Arial", 26, FontStyle.Regular, GraphicsUnit.Pixel);
            foreach (var el in drawData)
            {
                var code = el.Item1;
                var position = el.Item2;
                switch (code)
                {
                    case POINT_NON_TARGET: // С точкой никак не проивзаимодействовали
                        e.Graphics.FillEllipse(nonActiveBr, position.X - PointRadius, position.Y - PointRadius, 2 * PointRadius, 2 * PointRadius);
                        e.Graphics.DrawString(el.Item3.ToString(), font1, Brushes.Black,
                         new RectangleF(position.X - PointRadius, position.Y - PointRadius, 2 * PointRadius, 2 * PointRadius), sf);
                        break;
                    case POINT_TARGET: // Точка установлена как цель, которую надо "выбить" оператору
                        e.Graphics.FillEllipse(targetBr, position.X - PointRadius, position.Y - PointRadius, 2 * PointRadius, 2 * PointRadius);
                        e.Graphics.FillEllipse(blackBrush, position.X - 3, position.Y - 3, 2 * 3, 2 * 3);
                        break;
                    case POINT_TARGET_SELECTED: // Точка, установлена как цель, "выбита" оператором
                        e.Graphics.FillEllipse(targetselectedBr, position.X - PointRadius, position.Y - PointRadius, 2 * PointRadius, 2 * PointRadius);
                        e.Graphics.FillEllipse(blackBrush, position.X - 3, position.Y - 3, 2 * 3, 2 * 3);
                        break;
                    case POINT_NON_TARGET_SELECTED: // какая-то любая точка "выбита" оператором
                        e.Graphics.FillEllipse(activeBr, position.X - PointRadius, position.Y - PointRadius, 2 * PointRadius, 2 * PointRadius);
                        //e.Graphics.FillEllipse(blackBrush, position.X - 3, position.Y - 3, 2 * 3, 2 * 3);
                        e.Graphics.DrawString(el.Item3.ToString(), font1, Brushes.Black,
                         new RectangleF(position.X - PointRadius, position.Y - PointRadius, 2 * PointRadius, 2 * PointRadius), sf);
                        break;
                }
            }
            e.Dispose();
            font1.Dispose();
            activeBr.Dispose();
            nonActiveBr.Dispose();
            targetBr.Dispose();
            targetselectedBr.Dispose();
        }
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            ShowData(PointsMap, Numbers);
        }
    }
}
