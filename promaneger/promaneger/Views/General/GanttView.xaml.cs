using DlhSoft.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using DlhSoft.Windows.Data;
using System.Windows.Threading;
using Microsoft.Win32;


namespace promaneger.Views.General
{
    /// <summary>
    /// GanttView.xaml 的交互逻辑
    /// </summary>
    public partial class GanttView : UserControl
    {
        public GanttView()
        {
            var button = new Button { Content = "打开库页面" };

            Content = button;
        }
        
        

    }
}
