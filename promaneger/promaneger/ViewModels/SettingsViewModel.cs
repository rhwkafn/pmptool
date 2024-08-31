using promaneger.Common.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using System.Windows.Input;
using Prism.Regions;
using promaneger.Extensions;

namespace promaneger.ViewModels
{
    internal class SettingsViewModel : BindableBase
    {
        public SettingsViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            DelegateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;
        }
        #region 导航
        /// <summary>
        /// 定内容函数
        /// </summary>
        private ObservableCollection<MenuBar> menuBars;
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Account", Title = "账号信息", NameSpace = "UserView" });
            MenuBars.Add(new MenuBar() { Icon = "Palette", Title = "调色板", NameSpace = "SkinView" });
            
            MenuBars.Add(new MenuBar() { Icon = "InformationOutline", Title = "关于软件", NameSpace = "AboutView" });
        }

        ///加切换命令
        public DelegateCommand<MenuBar> DelegateCommand { get; private set; }
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;

            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(obj.NameSpace);
        }
        //实现接口
        private readonly IRegionManager regionManager;
        #endregion
    }
}
