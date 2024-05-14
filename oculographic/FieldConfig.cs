using System;
using System.Drawing;

namespace oculographic {
public class FieldConfig {
 public string Name;
 // массив, фактически состоящий из двумерных массивов, каждый из которых имеет одну точку калибровки, на которую оператор должен смотреть на конкретном шаге
 // места, в которых ничего не должно быть, имеют код 0, 1 - неактивная точка, 2 - активная точка
 public byte[,,] TrainingSequencies;
 // Числовые коды всех точек, которые 
 public int[,] Numbers;
 public int TotalLayers { get => TrainingSequencies.GetLength(0); }
 // Возвращает двумерный массив, описывающий 
 public byte[,] GetLayer(int index) {
  if (index > TotalLayers || index < 0)
   throw new IndexOutOfRangeException("Выбран уровень \"" + index + "\", доступно слоёв " + TotalLayers);
  byte[,] data = new byte[TrainingSequencies.GetLength(1), TrainingSequencies.GetLength(2)];
  for (int i = 0; i < TrainingSequencies.GetLength(1); i++)
   for (int j = 0; j < TrainingSequencies.GetLength(2); j++)
    data[i, j] = TrainingSequencies[index, i, j];
  return data;
 }
 // Получение точки 
 public Point GetActivePointOnLayer(int layer) {
  if (layer > TotalLayers || layer < 0)
   throw new IndexOutOfRangeException("Выбран уровень \"" + layer + "\", доступно слоёв " + TotalLayers);
  for (int i = 0; i < TrainingSequencies.GetLength(1); i++)
   for (int j = 0; j < TrainingSequencies.GetLength(2); j++)
    if (TrainingSequencies[layer, i, j] == 2) return new Point(j, i);
  return new Point(0, 0);
 }
 // Проверка соотвествия массив данных, описывающих поля маркеров, установленному формату
 public void Validate() {
  for (int step = 0; step < TrainingSequencies.GetLength(0); step++) {
   int count2 = 0;
   for (int i = 0; i < TrainingSequencies.GetLength(1); i++)
    for (int j = 0; j < TrainingSequencies.GetLength(2); j++) {
     var el = TrainingSequencies[step, i, j];
     if (el != 0 && el != 1 && el != 2)
      throw new Exception("Найдено не верное значение данных:" + el + " слой = " + step + ", i=" + i + "j=" + j);
     if (el == 2) count2++;
    }
   if (count2 != 1)
    throw new Exception("На слое " + step + " найдено значений \"активно\"=" + count2 + ". Должно быть только 1.");
  }
 }
}
}
