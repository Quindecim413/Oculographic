using System.Windows.Forms;

namespace oculographic {
public partial class ShowWeightsForm : Form {
 // Отображение графика с загруженными весами фильтра взвешенного скользящего среднего
 public ShowWeightsForm(double[] weights) {
  InitializeComponent();
  foreach (var w in weights) chart1.Series[0].Points.Add(w);
 }
}
}
