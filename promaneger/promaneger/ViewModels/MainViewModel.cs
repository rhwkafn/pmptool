using promaneger.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using promaneger.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace promaneger.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        public MainViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;

            
            GoBackCommand = new DelegateCommand(() =>
            {    /*journal需要不为null*/
                if (journal != null && journal.CanGoBack)
                    journal.GoBack();
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                    journal.GoForward();
            });

        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });

            

        }
        //导航日志
        private IRegionNavigationJournal journal;
        //区域选择
        private ObservableCollection<MenuBar> menuBars;
        private readonly IRegionManager regionManager;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        void CreateMenuBar()
        {
            //MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "项目", NameSpace = "ItemView" });
            MenuBars.Add(new MenuBar() { Icon = "FormatListBulleted", Title = "事项", NameSpace = "TodoView" });//原本为待办，现改为事项
            MenuBars.Add(new MenuBar() { Icon = "ChartLine", Title = "概览", NameSpace = "GeneralView" });
            MenuBars.Add(new MenuBar() { Icon = "Account", Title = "服务文档", NameSpace = "DocumentView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }
        //页面跳转
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        //上一步下一步
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }



        #region 默认首页

        /// <summary>
        /// 默认首页
        /// </summary>
        /// <param name="loginName">登录姓名</param>
        public void SetDefaultNav(string loginName)
        {
            NavigationParameters paras = new NavigationParameters();
            paras.Add("LoginName", loginName);
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("ItemView",
             callback =>
             {
                 journal = callback.Context.NavigationService.Journal;//记录导航足迹
             }, paras);
        }
        #endregion


    }
}
