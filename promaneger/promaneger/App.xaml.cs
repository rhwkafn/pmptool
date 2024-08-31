using promaneger.ViewModels.Dialogs;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using promaneger.HttpClients;
using promaneger.Service;
using promaneger.ViewModels;

using promaneger.ViewModels.settings;
using promaneger.Views;
using promaneger.Views.Dailogs;
using promaneger.Views.settings;
using System;
using System.Windows;

using promaneger.Views.Documents;
using promaneger.ViewModels.Documents;
using promaneger.Views.General;
using promaneger.ViewModels.General;

namespace promaneger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()//注册syncfusion
        {
           
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<Cotest>();
            //containerRegistry.RegisterForNavigation<UserControl1, UserControl1ViewModel>();

            //左侧栏
            containerRegistry.RegisterForNavigation<IndexView,IndexViewModel>();
            containerRegistry.RegisterForNavigation<TodoView, TodoViewModel>();
            containerRegistry.RegisterForNavigation<ItemView, ItemViewModel>();
            containerRegistry.RegisterForNavigation<GeneralView, GeneralViewModel>();
            containerRegistry.RegisterForNavigation<DocumentView, DocumentViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView,SettingsViewModel>();

            //设置页
            containerRegistry.RegisterForNavigation<AboutView,AboutViewModel > ();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<UserView, UserViewModel>();
            containerRegistry.RegisterForNavigation<ProfileView, ProfileViewModel>();

            //文档页
            containerRegistry.RegisterForNavigation<QidongView, QidongViewModel>();
            containerRegistry.RegisterForNavigation<GuihuaView, GuihuaViewModel>();
            containerRegistry.RegisterForNavigation<ZhixingView, ZhixingViewModel>();
            containerRegistry.RegisterForNavigation<JiankongView, JiankongViewModel>();
            containerRegistry.RegisterForNavigation<ShouweiView, ShouweiViewModel>();

            //数据页
            containerRegistry.RegisterForNavigation<SandianView, SandianViewModel>();
            containerRegistry.RegisterForNavigation<WangluoView, WangluoViewModel>();
            containerRegistry.RegisterForNavigation<ZhengzhiView, ZhengzhiViewModel>();
            //containerRegistry.RegisterForNavigation<TubiaoView, TubiaoViewModel>();
            containerRegistry.RegisterForNavigation<GanttView, GanttViewModel>();

            //登录
            containerRegistry.RegisterDialog<LoginUC, LoginUCViewModel>();

            //请求
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));


            //自定义对话框服务
            containerRegistry.Register<DialogHostService>();
            containerRegistry.RegisterForNavigation<AddProjectUC, AddProjectUCViewModel>();
            containerRegistry.RegisterForNavigation<AddTaskUC, AddTaskUCViewModel>();
            containerRegistry.RegisterForNavigation<EditProjectUC, EditProjectUCViewModel>();
            containerRegistry.RegisterForNavigation<EditTaskUC, EditTaskUCViewModel>();
            
            containerRegistry.RegisterForNavigation<AddMemoUC, AddMemoUCViewModel>();//添加备忘录
            containerRegistry.RegisterForNavigation<EditMemoUC, EditMemoUCViewModel>();



        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInitialized()
        {
            var dialog = Container.Resolve<IDialogService>();
            //dialog.ShowDialog("LoginUC", callback =>
            //{
            //    if (callback.Result != ButtonResult.OK)
            //    {
            //        Environment.Exit(0);
            //        return;
            //    }

            //    var mainVM = Current.MainWindow.DataContext as MainViewModel;//主界面数据上下文，用于显示用户名
            //    if (mainVM != null)
            //    {
            //        if (callback.Parameters.ContainsKey("LoginName"))
            //        {
            //            string name = callback.Parameters.GetValue<string>("LoginName");

            //            mainVM.SetDefaultNav(name);
            //        }
            //    }

            //    base.OnInitialized();
            //});
            base.OnInitialized();
        }
    }
}
