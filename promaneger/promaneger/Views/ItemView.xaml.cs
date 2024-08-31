using promaneger.ViewModels;
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

namespace promaneger.Views
{
    /// <summary>
    /// ItemView.xaml 的交互逻辑
    /// </summary>
    public partial class ItemView : UserControl
    {
        public ItemView()
        {
            InitializeComponent();
        }
        // 确保所有方法和字段都在这个类内部
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void FilterTextTasksBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
