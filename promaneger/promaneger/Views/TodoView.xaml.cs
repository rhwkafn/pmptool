
using Syncfusion.UI.Xaml.Kanban;
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

namespace promaneger.Views
{
    /// <summary>
    /// TodoView.xaml 的交互逻辑
    /// </summary>
    public partial class TodoView : UserControl
    {
        public TodoView()
        {
            InitializeComponent();
           
        }
       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 窗口加载时的处理逻辑
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // 日历选择日期变化时的处理逻辑
        }


    }
  
    



}

