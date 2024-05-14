using MathNet.Numerics;
using System;
using System.Drawing;
using System.Linq;

namespace oculographic.Approximation
{
    public class OculoDataRegressor
    {
        public readonly double[] Vals1;
        public readonly double[] Vals2;
        public readonly double[] Vals3;
        public readonly double[] Vals4;
        public readonly double[] Xs;
        public readonly double[] Ys;
        public readonly RectangleF TrainArea;
        public readonly double DistanceFromScreen, ScreenWidth, ScreenHeight;

        public readonly StandartScaler[] Scalers;
        public readonly MinMaxScaler XScaler, YScaler;
        public readonly Func<double[], double> TransformAngleXScaled;
        public readonly Func<double[], double> TransformAngleYScaled;
        // Инициализация модели
        public OculoDataRegressor(RectangleF trainAreaPx, double distanceFromScreen, Size activeScreenSizePx, double screenHeight, 
         double[] vals1, double[] vals2, double[] vals3, double[] vals4, double[] xs, double[] ys)
        {
            TrainArea = trainAreaPx;
            DistanceFromScreen = distanceFromScreen;
            ScreenHeight = screenHeight;
            ScreenWidth = GetScreenWidthCM(ScreenHeight, activeScreenSizePx);

            Vals1 = vals1;
            Vals2 = vals2;
            Vals3 = vals3;
            Vals4 = vals4;
            Scalers = new StandartScaler[4];
            Scalers[0] = new StandartScaler(vals1);
            Scalers[1] = new StandartScaler(vals2);
            Scalers[2] = new StandartScaler(vals3);
            Scalers[3] = new StandartScaler(vals4);
            vals1 = Scalers[0].Transform(vals1);
            vals2 = Scalers[1].Transform(vals2);
            vals3 = Scalers[2].Transform(vals3);
            vals4 = Scalers[3].Transform(vals4);
            var data = Enumerable.Range(0, vals1.Length).Select(ind => new double[] {
                vals1[ind], vals2[ind], vals3[ind], vals4[ind]}).ToArray();

            var scalers = CreateScalersFromWindowToPhisicalScreen(trainAreaPx, activeScreenSizePx, ScreenHeight);

            XScaler = scalers.Item1;
            YScaler = scalers.Item2;
            double[] posX = XScaler.Transform(xs);
            double[] angleX = posX.Select(x => Math.Atan2(x, DistanceFromScreen)).ToArray();
            double[] posY = YScaler.Transform(ys);
            double[] angleY = posY.Select(y => Math.Atan2(y, DistanceFromScreen)).ToArray();
            TransformAngleXScaled = Fit.LinearMultiDimFunc(data, angleX,
             d => 1
             , d => Math.Atan(d[0])
             , d => Math.Atan(d[1])
             , d => Math.Atan(d[2])
             , d => Math.Atan(d[3])
             );
            TransformAngleYScaled = Fit.LinearMultiDimFunc(data, angleY,
             d => 1
             , d => Math.Atan(d[0])
             , d => Math.Atan(d[1])
             , d => Math.Atan(d[2])
             , d => Math.Atan(d[3])
             );
        }

        static double GetScreenWidthCM(double screenHeightCM, Size screenSizePx)
        {
            double ratio = screenSizePx.Width * 1.0 / screenSizePx.Height;
            double widthCM = ratio * screenHeightCM;
            return widthCM;
        }

        public static Tuple<MinMaxScaler, MinMaxScaler> CreateScalersFromWindowToPhisicalScreen( RectangleF trainAreaPx, Size screenSizePx, double screenHeightCM)
        {
            double heightCm = screenHeightCM;
            double widthCM = GetScreenWidthCM(screenHeightCM, screenSizePx);

            double offsetX = trainAreaPx.X * 1.0 / screenSizePx.Width * widthCM;
            double offsetY = trainAreaPx.Y * 1.0 / screenSizePx.Height * heightCm;

            double trainAreaWidth = widthCM * (trainAreaPx.Width / screenSizePx.Width);
            double trainAreaHeight = heightCm * (trainAreaPx.Height / screenSizePx.Height);

            double startX = -widthCM / 2 + offsetX;
            double endX = startX + trainAreaWidth;
            double startY = -heightCm / 2 + offsetY;
            double endY = startY + trainAreaHeight;

            var XScaler = new MinMaxScaler(new double[] { trainAreaPx.X, trainAreaPx.Width }, 0, 100, startX, endX);
            var YScaler = new MinMaxScaler(new double[] { trainAreaPx.Y, trainAreaPx.Height }, 0, 100, startY, endY);
            return Tuple.Create(XScaler, YScaler);
        }

            // Приведение координат точки в плоскости экрана к координатам отклонения глаза оператора
            public static PointF ConvertToAngles(PointF position, double distanceFromScreen, RectangleF trainAreaPx, Size screenSizePx, double screenHeightCM)
        {
            var scalers = CreateScalersFromWindowToPhisicalScreen(trainAreaPx, screenSizePx, screenHeightCM);
            double posX = scalers.Item1.Transform(position.X);
            double posY = scalers.Item2.Transform(position.Y);
            return new PointF((float)(Math.Atan2(posX, distanceFromScreen) / Math.PI * 180),
                        (float)(Math.Atan2(posY, distanceFromScreen) / Math.PI * 180));
        }
        // Предсказание точки внимания оператора в плоскости экрана
        public PointF Predict(double value1, double value2, double value3, double value4)
        {
            var val1 = Scalers[0].Transform(value1);
            var val2 = Scalers[1].Transform(value2);
            var val3 = Scalers[2].Transform(value3);
            var val4 = Scalers[3].Transform(value4);
            var angleX = TransformAngleXScaled(new double[] { val1, val2, val3, val4 });
            var angleY = TransformAngleYScaled(new double[] { val1, val2, val3, val4 });
            double tanX = Math.Tan(angleX);
            double tanY = Math.Tan(angleY);
            double x = XScaler.InverseTransform(DistanceFromScreen * tanX);
            double y = YScaler.InverseTransform(DistanceFromScreen * tanY);
            return new PointF((float)x, (float)y);
        }
        // Предсказание углов отклонения глаза по горизонтали и по вертикали 
        public PointF PredictAngles(double value1, double value2, double value3, double value4)
        {
            var val1 = Scalers[0].Transform(value1);
            var val2 = Scalers[1].Transform(value2);
            var val3 = Scalers[2].Transform(value3);
            var val4 = Scalers[3].Transform(value4);
            var angleX = TransformAngleXScaled(new double[] { val1, val2, val3, val4 }) / (Math.PI) * 180;
            var angleY = TransformAngleYScaled(new double[] { val1, val2, val3, val4 }) / (Math.PI) * 180;
            return new PointF((float)angleX, (float)angleY);
        }
    }
}