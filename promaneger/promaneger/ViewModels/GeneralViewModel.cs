using Prism.Mvvm;
using Prism.Regions;
using promaneger.Common.Models;
using promaneger.Extensions;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace promaneger.ViewModels
{
    internal class GeneralViewModel : BindableBase
    {

        public GeneralViewModel(IRegionManager regionManager)
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
            MenuBars.Add(new MenuBar() { Icon = "ChartDonut", Title = "三点估算", NameSpace = "SandianView" });
            MenuBars.Add(new MenuBar() { Icon = "ChartDonut", Title = "网络图", NameSpace = "WangluoView" });
            MenuBars.Add(new MenuBar() { Icon = "ChartDonut", Title = "挣值管理", NameSpace = "ZhengzhiView" });
            //MenuBars.Add(new MenuBar() { Icon = "ChartTimeline", Title = "甘特图", NameSpace = "UserControl1" });
            //MenuBars.Add(new MenuBar() { Icon = "ChartSankeyVariant", Title = "图表", NameSpace = "TubiaoView" });

        }

        ///加切换命令
        public DelegateCommand<MenuBar> DelegateCommand { get; private set; }
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;

            regionManager.Regions[PrismManager.GeneralViewRegionName].RequestNavigate(obj.NameSpace);
        }
        //实现接口
        private readonly IRegionManager regionManager;
        #endregion

    }
}
