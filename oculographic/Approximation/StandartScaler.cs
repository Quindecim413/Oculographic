using System.Linq;
using MathNet.Numerics.Statistics;

namespace oculographic.Approximation {
public class StandartScaler {
 public readonly double Std;
 public readonly double Mean;
 public StandartScaler(double[] fitData) {
  var stats = fitData.MeanStandardDeviation();
  Mean = stats.Item1;
  Std = stats.Item2;
 }
 public double[] Transform(double[] data) {
  return data.Select(el => (el - Mean) / Std).ToArray();
 }
 public double Transform(double data) {
  return (data - Mean) / Std;
 }
}
}
