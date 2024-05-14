using System;
using System.Linq;

namespace oculographic {
    public struct MinMaxScaler {
        public readonly double Min;
        public readonly double ValsMin;
        public readonly double Max;
        public readonly double ValsMax;
        public readonly double NormalizationCoof;
        public MinMaxScaler(double[] data,
         double minPercentile, double maxPercentile,
         double minVal = 0, double maxVal = 1) {
            if (maxPercentile < minPercentile) {
                throw new Exception(String.Format("Invalid percentiles range: [{0}, {1}]",
                  minPercentile, maxPercentile));
            }
            int indMin = (int)((data.Length - 1) * minPercentile / 100);
            int indMax = (int)((data.Length - 1) * maxPercentile / 100);
            Array.Sort(data);
            var vals = Enumerable.Range(indMin, indMax - indMin + 1).Select(ind => data[ind]).ToArray();
            ValsMin = vals.Min();
            ValsMax = vals.Max();
            Min = minVal;
            Max = maxVal;
            NormalizationCoof = (ValsMax - ValsMin == 0) ? 1 : (ValsMax - ValsMin);
        }
        public double Transform(double val) {
            return (val - ValsMin) / (NormalizationCoof) * (Max - Min) + Min;
        }
        public double[] Transform(double[] vals) {
            var me = this;
            return vals.Select(el => me.Transform(el)).ToArray();
        }
        public double InverseTransform(double val) {
            return (val - Min) / (Max - Min) * NormalizationCoof + ValsMin;
        }
        public double[] InverseTransform(double[] vals) {
            var me = this;
            return vals.Select(el => me.InverseTransform(el)).ToArray();
        }
    }
}
