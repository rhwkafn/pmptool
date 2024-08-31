using Newtonsoft.Json;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using promaneger.Common.Models;
using promaneger.DTOs;
using promaneger.Extensions;
using promaneger.HttpClients;
using promaneger.Models;
using promaneger.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace promaneger.ViewModels
{
    class DocumentViewModel : BindableBase,INotifyPropertyChanged
    {
        #region 备忘录数据
        private List<MemoInfoDTO> _MemoList;

        /// <summary>
        /// 备忘录数据
        /// </summary>
        public List<MemoInfoDTO> MemoList
        {
            get { return _MemoList; }
            set
            {
                _MemoList = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        private readonly HttpRestClient HttpClient;//请求api的客户端

        //对话服务(自定义)
        private readonly DialogHostService DialogHostService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentViewModel(HttpRestClient _HttpClient, DialogHostService _DialogHostService, IRegionManager _RegionManager)
        {


            HttpClient = _HttpClient;//请求api的客户端

            //对话服务
            DialogHostService = _DialogHostService;

            ShowAddMemoDialogCmm = new DelegateCommand(ShowAddMemoDialog);

            GetMemoList();//获取备忘录数据

            ShowEditMemoDialogCmm = new DelegateCommand<MemoInfoDTO>(ShowEditMemoDialog);

            //页面跳转
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            DelegateCommand = new DelegateCommand<MenuBar>(Navigate);
            regionManager = _RegionManager;//setting那个this不行，这样才可以

            //删除memo
            DelMemoCmm = new DelegateCommand<MemoInfoDTO>(DelMemo);




        }

        /// <summary>
        /// 创建备忘录数据
        /// </summary>
        private void GetMemoList()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = "Memo/QueryMemo";

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)//获取成功
            {
                MemoList = JsonConvert.DeserializeObject<List<MemoInfoDTO>>(response.ResultData.ToString());
            }
            else
            {
                MemoList = new List<MemoInfoDTO>();
            }
        }

        #region 备忘录

        //显示添加备忘录视图命令
        public DelegateCommand ShowAddMemoDialogCmm { get; set; }

        /// <summary>
        /// 显示添加备忘录视图
        /// </summary>
        private async void ShowAddMemoDialog()
        {
            var result = await DialogHostService.ShowDialog("AddMemoUC", null, "RootDialog");//await等待

            if (result.Result == ButtonResult.OK)
            {
                //接收数据
                if (result.Parameters.ContainsKey("AddMemoInfo"))
                {
                    var addModel = result.Parameters.GetValue<MemoInfoDTO>("AddMemoInfo");

                    //调用api实现添加备忘录

                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = RestSharp.Method.POST;
                    apiRequest.Parameters = addModel;
                    apiRequest.Route = "Memo/AddMomo";
                    ApiResponse response = HttpClient.Execute(apiRequest);
                    if (response.ResultCode == 1)//添加成功
                    {
                        MessageBox.Show(response.Msg);

                        //刷新列表
                        GetMemoList();//获取备忘录数据
                    }
                    else
                    {
                        MessageBox.Show(response.Msg);
                    }
                }
            }
        }

        /// <summary>
        /// 显示备忘录编辑界面
        /// </summary>
        public DelegateCommand<MemoInfoDTO> ShowEditMemoDialogCmm { get; set; }

        private async void ShowEditMemoDialog(MemoInfoDTO memoInfoDTO)
        {
            DialogParameters paras = new DialogParameters();
            paras.Add("OldMemoInfo", memoInfoDTO);

            var result = await DialogHostService.ShowDialog("EditMemoUC", paras);//await等待

            if (result.Result == ButtonResult.OK)
            {
                //接收数据
                if (result.Parameters.ContainsKey("NewMemoInfo"))
                {
                    var newModel = result.Parameters.GetValue<MemoInfoDTO>("NewMemoInfo");

                    //调用api实现编辑备忘录

                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = RestSharp.Method.PUT;
                    apiRequest.Parameters = newModel;
                    apiRequest.Route = "Memo/EditMemo";
                    ApiResponse response = HttpClient.Execute(apiRequest);
                    if (response.ResultCode == 1)//编辑成功
                    {
                        MessageBox.Show(response.Msg);

                        GetMemoList();//刷新列表
                    }
                    else
                    {
                        MessageBox.Show(response.Msg);
                    }
                }
            }
        }


        #endregion

        #region 实现跳转
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
            MenuBars.Add(new MenuBar() { Icon = "RomanNumeral1", Title = "启动", NameSpace = "QidongView" });
            MenuBars.Add(new MenuBar() { Icon = "RomanNumeral2", Title = "规划", NameSpace = "GuihuaView" });
            MenuBars.Add(new MenuBar() { Icon = "RomanNumeral3", Title = "执行", NameSpace = "ZhixingView" });
            MenuBars.Add(new MenuBar() { Icon = "RomanNumeral4", Title = "监控", NameSpace = "JiankongView" });
            MenuBars.Add(new MenuBar() { Icon = "RomanNumeral5", Title = "收尾", NameSpace = "ShouweiView" });
        }

        ///加切换命令
        public DelegateCommand<MenuBar> DelegateCommand { get; private set; }
        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;

            regionManager.Regions[PrismManager.DocumentsViewRegionName].RequestNavigate(obj.NameSpace);
        }
        //实现接口
        private readonly IRegionManager regionManager;


        #endregion


        #region 备忘录删除

        public DelegateCommand<MemoInfoDTO> DelMemoCmm { get; set; }//删除命令

        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <param name="memoInfoDTO"></param>
        public void DelMemo(MemoInfoDTO memoInfoDTO)
        {
            var selResult = MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.OKCancel);
            if (selResult == MessageBoxResult.OK)
            {
                ApiRequest apiRequest = new ApiRequest();
                apiRequest.Method = RestSharp.Method.DELETE;
                apiRequest.Route = $"Memo/DelMemo?MemoId={memoInfoDTO.MemoId}";

                ApiResponse response = HttpClient.Execute(apiRequest);

                if (response.ResultCode == 1)//删除成功
                {
                    MessageBox.Show(response.Msg);
                    GetMemoList();// 刷新数据
                }
                else
                {
                    MessageBox.Show(response.Msg);
                }
            }
        }

        #endregion

    }
}
