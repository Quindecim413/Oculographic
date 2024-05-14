using System.Collections.Concurrent;
using System.Linq;

namespace oculographic {
class SlidingWindowSmoother {
 public readonly double[] Weights;
 private FixedSizedQueue dataQueue;
 public SlidingWindowSmoother(double[] weights) {
  var sum = weights.Sum();
  double[] normed = new double[weights.Length];
  for(int i = 0; i < normed.Length; i++) normed[i] = weights[i] / sum;
  Weights = normed;
  dataQueue = new FixedSizedQueue(weights.Length);
 }
 // Добавляет значние в очередь накопленных значений и возвращает значение, рассчитанное на основе накопленных значений
 public double Add(double val) {
  dataQueue.Enqueue(val);
  return Result;
 }
 public double Result {
  get {
   var queuedData = dataQueue.queue.ToArray();
   if (IsReady) return Enumerable.Range(0, Weights.Length).Select(ind => queuedData[ind] * Weights[ind]).Sum();
   else return queuedData[queuedData.Length - 1];
  }
 }
 public bool IsReady => dataQueue.queue.Count == dataQueue.Size;

 // Реализация очереди с фиксированной максимальной длинной
 private class FixedSizedQueue {
  public readonly ConcurrentQueue<double> queue = new ConcurrentQueue<double>();
  public int Size { get; private set; }
  public FixedSizedQueue(int size) {
   Size = size;
  }
  public void Enqueue(double obj) {
   queue.Enqueue(obj); 
   while (queue.Count > Size) {
    double outObj;
    queue.TryDequeue(out outObj);
   }
  }
 }
}
}
