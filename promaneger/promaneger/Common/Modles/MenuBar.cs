using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;

namespace promaneger.Common.Models
{
    /// <summary>
    /// 系统导航菜单实体类
    /// </summary>
    public class MenuBar : BindableBase
    {
        private string icon;

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        private string title;

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string nameSpace;

        /// <summary>
        /// 菜单命名空间
        /// </summary>
        public string NameSpace
        {
            get { return nameSpace; }
            set { nameSpace = value; }
        }
    }
}



//x: Class = "promaneger.Views.MainView"
//        xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//        xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"
//        xmlns: prism = "http://prismlibrary.com/"
//        xmlns: materialDesign = "http://materialdesigninxaml.net/winfx/xaml/themes" xmlns: viewmodels = "clr-namespace:promaneger.ViewModels" xmlns: d = "http://schemas.microsoft.com/expression/blend/2008"
//         d: DataContext = "{d:DesignInstance Type=viewmodels:MainViewModel}"
//        prism: ViewModelLocator.AutoWireViewModel = "True"
//        AllowsTransparency = "True"
//        Background = "{DynamicResource MaterialDesignPaper}"
//FontFamily = "微软雅黑"
//TextElement.FontSize = "13"
//TextElement.FontWeight = "Regular"
//TextElement.Foreground = "{DynamicResource MaterialDesignBody}"
//TextOptions.TextFormattingMode = "Ideal"
//TextOptions.TextRenderingMode = "Auto"
//WindowStartupLocation = "CenterScreen"


//        Title = "{Binding Title}" Height = "610" Width = "1080" WindowStyle = "None"