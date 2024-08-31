using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace promaneger.Views.General
{
    /// <summary>
    /// SandianView.xaml 的交互逻辑
    /// </summary>
    public partial class SandianView : UserControl
    {

        public ObservableCollection<Activity> Activities { get; set; }

        public SandianView()
        {
            InitializeComponent();
            Activities = new ObservableCollection<Activity>();
            ActivitiesDataGrid.ItemsSource = Activities;
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            string name = ActivityName.Text;
            if (double.TryParse(OptimisticDuration.Text, out double optimistic) &&
                double.TryParse(MostLikelyDuration.Text, out double mostLikely) &&
                double.TryParse(PessimisticDuration.Text, out double pessimistic))
            {
                var activity = new Activity
                {
                    Name = name,
                    Optimistic = optimistic,
                    MostLikely = mostLikely,
                    Pessimistic = pessimistic,
                    ExpectedTime = (optimistic + mostLikely + pessimistic) / 3,
                    StandardDeviation = (pessimistic - optimistic) / 6,
                    Variance = Math.Pow((pessimistic - optimistic) / 6, 2)
                };
                Activities.Add(activity);
                UpdateProjectSummary();
            }
            else
            {
                MessageBox.Show("请输入有效的工期数值。");
            }
        }

        private void UpdateProjectSummary()
        {
            var totalOptimistic = Activities.Sum(a => a.Optimistic);
            var totalMostLikely = Activities.Sum(a => a.MostLikely);
            var totalPessimistic = Activities.Sum(a => a.Pessimistic);
            var totalExpectedTime = Activities.Sum(a => a.ExpectedTime);
            var totalStandardDeviation = Activities.Sum(a => a.StandardDeviation);
            var totalVariance = Activities.Sum(a => a.Variance);

            var projectSummary = new Activity
            {
                Name = "整个项目",
                Optimistic = totalOptimistic,
                MostLikely = totalMostLikely,
                Pessimistic = totalPessimistic,
                ExpectedTime = totalExpectedTime,
                StandardDeviation = totalStandardDeviation,
                Variance = totalVariance
            };

            if (Activities.Any(a => a.Name == "整个项目"))
            {
                var existingSummary = Activities.First(a => a.Name == "整个项目");
                Activities.Remove(existingSummary);
            }

            Activities.Add(projectSummary);
        }
    }

    public class Activity
    {
        public string Name { get; set; }
        public double Optimistic { get; set; }
        public double MostLikely { get; set; }
        public double Pessimistic { get; set; }
        public double ExpectedTime { get; set; }
        public double StandardDeviation { get; set; }
        public double Variance { get; set; }
    }
}

