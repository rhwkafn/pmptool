using promaneger.DTOs;
using System;
using System.Collections.Generic;
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

namespace promaneger.Views.Dailogs
{
    /// <summary>
    /// AddProjectUC.xaml 的交互逻辑
    /// </summary>
    public partial class AddProjectUC : UserControl
    {
        public AddProjectUC()
        {
            InitializeComponent();
            myDatePicker.SelectedDate = System.DateTime.Now;
            endtime.SelectedDate = DateTime.Now;

        }
    }
}
