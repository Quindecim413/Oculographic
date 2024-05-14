using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace oculographic.Approximation
{
    public class OculoTraining
    {
        Dictionary<PointF, List<Tuple<double, double, double, double>>> TrainingPoints = new Dictionary<PointF, List<Tuple<double, double, double, double>>>();
        public PointF? CurrentTrainingPoint { get; private set; }
        public OculoDataRegressor Regressor { get; private set; }

        public RectangleF TrainArea { get; set; }
        public Size ScreenSize { get; set; }
        public double DistanceFromScreen { get; set; }
        public double ScreenHeight { get; set; }
        public bool Confirmed => Regressor != null;
        public void StartFittingPoint(PointF referencePoint)
        {
            if (!TrainingPoints.ContainsKey(referencePoint)) TrainingPoints[referencePoint] = new List<Tuple<double, double, double, double>>(400);
            CurrentTrainingPoint = referencePoint;
        }
        // Прекращение набора данных для данной точки калибровки
        public void StopFitting()
        {
            CurrentTrainingPoint = null;
        }
        // Сброс параметров модели
        public void Reset()
        {
            TrainingPoints.Clear();
            CurrentTrainingPoint = null;
            Regressor = null;
        }
        // Построение модели многокритериальной линейной регрессии по заданным точкам калибровки
        public void ComfirmFitting()
        {
            List<double> xs = new List<double>();
            List<double> ys = new List<double>();
            List<double> vals1 = new List<double>();
            List<double> vals2 = new List<double>();
            List<double> vals3 = new List<double>();
            List<double> vals4 = new List<double>();
            double minPercentile = 25;
            double maxPercentile = 75;
            foreach (var pair in TrainingPoints)
            {
                var point = pair.Key;
                var vals = pair.Value;
                if (vals.Count == 0)
                    continue;
                var v1 = vals.Select(el => el.Item1);
                var v2 = vals.Select(el => el.Item2);
                var v3 = vals.Select(el => el.Item3);
                var v4 = vals.Select(el => el.Item4);
                int takeFirstPoints = Math.Max(10, vals.Count);
                MinMaxScaler scaler1 = new MinMaxScaler(v1.ToArray(), 25, 75);
                MinMaxScaler scaler2 = new MinMaxScaler(v2.ToArray(), 25, 75);
                MinMaxScaler scaler3 = new MinMaxScaler(v3.ToArray(), 25, 75);
                MinMaxScaler scaler4 = new MinMaxScaler(v4.ToArray(), 25, 75);
                var center = new double[] {scaler1.Transform(v1.Sum() / vals.Count),
          scaler2.Transform(v2.Sum() / vals.Count),
          scaler3.Transform(v3.Sum() / vals.Count),
          scaler4.Transform(v4.Sum() / vals.Count) };
                var orderedByDistanceFromCenter = vals.OrderBy(el =>
                      Math.Pow(scaler1.Transform(el.Item1) - center[0], 2) +
                      Math.Pow(scaler2.Transform(el.Item2) - center[1], 2) +
                      Math.Pow(scaler3.Transform(el.Item3) - center[2], 2) +
                      Math.Pow(scaler4.Transform(el.Item4) - center[3], 2)).ToArray();
                int indMin = (int)((vals.Count - 1) * minPercentile / 100);
                int indMax = (int)((vals.Count - 1) * maxPercentile / 100);
                var selectedByPercentile = Enumerable.Range(indMin, indMax - indMin + 1).Select(ind => orderedByDistanceFromCenter[ind]).ToArray();
                for (int i = 0; i < selectedByPercentile.Length; i++)
                {
                    xs.Add(point.X);
                    ys.Add(point.Y);
                    vals1.Add(selectedByPercentile[i].Item1);
                    vals2.Add(selectedByPercentile[i].Item2);
                    vals3.Add(selectedByPercentile[i].Item3);
                    vals4.Add(selectedByPercentile[i].Item4);
                }
            }
            Regressor = new OculoDataRegressor(TrainArea, DistanceFromScreen, ScreenSize, ScreenHeight,
             vals1.ToArray(), vals2.ToArray(), vals3.ToArray(), vals4.ToArray(), xs.ToArray(), ys.ToArray());
        }
        // Определение точки калибровки, к которой предсказание модели точки внимания находится на наименьшем расстоянии
        public PointF FindClosest(double value1, double value2, double value3, double value4)
        {
            var predicted = Predict(value1, value2, value3, value4);
            return TrainingPoints.Select(el => el.Key).OrderBy(el => Math.Pow(el.X - predicted.X, 2) + Math.Pow(el.Y - predicted.Y, 2)).First();
        }
        // Предсказание моделью точки внимания оператора
        public PointF Predict(double value1, double value2, double value3, double value4)
        {
            if (Regressor == null)
                throw new ArgumentNullException("Сначала требуется завершить тренировку вызовом метода ComfirmFitting");
            return Regressor.Predict(value1, value2, value3, value4);
        }
        // Предсказание моделью углов отклонения глаза оператора от центра поля зрения
        public PointF PredictAngles(double value1, double value2, double value3, double value4)
        {
            if (Regressor == null)
                throw new ArgumentNullException("Сначала требуется завершить тренировку вызовом метода ComfirmFitting");
            return Regressor.PredictAngles(value1, value2, value3, value4);
        }
        // Добавление новых калибровочных данных к активной точке калибровки
        public void AddNewValuesToTrainingPoint(double value1, double value2, double value3, double value4)
        {
            if (!CurrentTrainingPoint.HasValue) return;
            TrainingPoints[CurrentTrainingPoint.Value].Add(Tuple.Create(value1, value2, value3, value4));
        }
    }
}