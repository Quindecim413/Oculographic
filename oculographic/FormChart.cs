using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace oculographic
{
    public partial class FormChart : Form
    {
        Func<Approximation.OculoTraining> RetrieveTrainer;

        Series[] m_Series;
        Series[] m_SeriesDiff;

        bool inverseDiff13 = false;
        bool inverseDiff24 = false;
        public FormChart(Func<Approximation.OculoTraining> retrieveTrainer)
        {
            InitializeComponent();
            RetrieveTrainer = retrieveTrainer;
            m_Series = new Series[] { chart1.Series[0], chart2.Series[0], chart3.Series[0], chart4.Series[0] };
            m_SeriesDiff = new Series[] { chart5.Series[0], chart6.Series[0] };
        }
        private void ChartForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
        private void ResetAutoValues()
        {
            chart1.ResetAutoValues();
            chart3.ResetAutoValues();
            chart2.ResetAutoValues();
            chart4.ResetAutoValues();
            chart5.ResetAutoValues();
            chart6.ResetAutoValues();
            chart7.ResetAutoValues();
        }
        // Сброс данных с графиков
        public void Clear()
        {
            for (int i = 0; i < m_Series.Length; i++) m_Series[i].Points.Clear();
        }
        bool IsFirstRecieveValues = true;
        DateTime lastFieldUpdate;
        const double MIN_SECOND_FOR_FIELD_UPDATE = 1.0 / 20.0;

        // Добавляет данные на график, но не больше установленного количнства значений
        public void PutData(float value1, float value2, float value3, float value4)
        {
            try
            {
                var now = DateTime.Now;
                if (IsFirstRecieveValues)
                {
                    IsFirstRecieveValues = false;
                    lastFieldUpdate = now;
                    return;
                }
                if ((now - lastFieldUpdate).TotalSeconds < MIN_SECOND_FOR_FIELD_UPDATE)
                    return;
                else
                {
                    lastFieldUpdate = now;

                    if (InvokeRequired) Invoke(new Action<float, float, float, float>(mPutData), value1, value2, value3, value4);
                    else mPutData(value1, value2, value3, value4);
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Ошибка графиков", ex.Message + "\r\n------------\r\n" + ex.StackTrace);
            }
        }

        private void mPutData(float value1, float value2, float value3, float value4)
        {
            var trainer = RetrieveTrainer();
            if (trainer != null && trainer.Confirmed)
            {
                var regressor = trainer.Regressor;
                value1 = (float)regressor.Scalers[0].Transform(value1);
                value2 = (float)regressor.Scalers[1].Transform(value2);
                value3 = (float)regressor.Scalers[2].Transform(value3);
                value4 = (float)regressor.Scalers[3].Transform(value4);
            }
            var data = new double[] { value1, value2, value3, value4 };
            for (int i = 0; i < data.Length && i < m_Series.Length; i++)
            {
                m_Series[i].Points.AddY(data[i]);
                while (m_Series[i].Points.Count > nCount.Value) m_Series[i].Points.RemoveAt(0);
            }
            if (inverseDiff13) m_SeriesDiff[0].Points.AddY(data[0] - data[2]);
            else m_SeriesDiff[0].Points.AddY(data[2] - data[0]);
            if (inverseDiff24) m_SeriesDiff[1].Points.AddY(data[1] - data[3]);
            else m_SeriesDiff[1].Points.AddY(data[3] - data[1]);
            for (int i = 0; i < m_SeriesDiff.Length; i++)
                while (m_SeriesDiff[i].Points.Count > nCount.Value)
                    m_SeriesDiff[i].Points.RemoveAt(0);
            chart7.Series[0].Points.Add((value1 + value2 + value3 + value4) / 4);
            while (chart7.Series[0].Points.Count > nCount.Value)
                chart7.Series[0].Points.RemoveAt(0);
            ResetAutoValues();
        }

        // Условное отображение разницы показаний датчиков 1 и 3 со знаком минус
        private void UpdateInversion13()
        {
            if (inverseDiff13) groupBox5.Text = "(Канал 3) - (Канал 1)";
            else groupBox5.Text = "(Канал 3) - (Канал 2)";
            var points = m_SeriesDiff[0].Points.ToArray();
            m_SeriesDiff[0].Points.Clear();
            foreach (var p in points) m_SeriesDiff[0].Points.AddXY(p.XValue, -p.YValues[0]);
        }
        // Условное отображение разницы показаний датчиков 2 и 4 со знаком минус
        private void UpdateInversion24()
        {
            if (inverseDiff24) groupBox6.Text = "(Канал 4) - (Канал 2)";
            else groupBox6.Text = "(Канал 2) - (Канал 4)";
            var points = m_SeriesDiff[1].Points.ToArray();
            m_SeriesDiff[1].Points.Clear();
            foreach (var p in points) m_SeriesDiff[1].Points.AddXY(p.XValue, -p.YValues[0]);
        }
        private void buttonInvert12_Click(object sender, EventArgs e)
        {
            inverseDiff13 = !inverseDiff13;
            UpdateInversion13();
        }
        private void buttonInvert34_Click(object sender, EventArgs e)
        {
            inverseDiff24 = !inverseDiff24;
            UpdateInversion24();
        }
    }
}