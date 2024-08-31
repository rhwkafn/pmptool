using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using promaneger.DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using promaneger.HttpClients;
using System.Net.Http;
using promaneger.Models;

using Prism.Services.Dialogs;
using Prism.Commands;
using System.Windows;
using promaneger.Service;
using System.Windows.Automation;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace promaneger.ViewModels
{
    class ItemViewModel:BindableBase,INavigationAware, INotifyPropertyChanged
    {

        #region 统计面板数据
        private List<StatPanelInfo> _StatPanelList;

        /// <summary>
        /// 统计面板数据
        /// </summary>
        public List<StatPanelInfo> StatPanelList
        {
            get { return _StatPanelList; }
            set
            {
                _StatPanelList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 显示登录用户信息

        private string _LoginInfo;

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public string LoginInfo
        {
            get { return _LoginInfo; }
            set
            {
                _LoginInfo = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// 接收导航并处理,实现导航接口
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedTo(NavigationContext navigationContext)//在页面被导航到时调用，可以用于加载数据、初始化页面等。
        {
            if (navigationContext.Parameters.ContainsKey("LoginName"))
            {
                DateTime now = DateTime.Now;//当前时间
                string[] week = new string[7] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                string loginName = navigationContext.Parameters.GetValue<string>("LoginName");

                LoginInfo = $"您好！{loginName}。今天是{now.ToString("yyyy-MM-dd")} {week[(int)now.DayOfWeek]}";

                //CallStatWait();//统计待办事项(先注释）
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)//判断当前页面是否为导航目标
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)//在页面被导航离开时调用，可以用于清理资源、保存数据等。
        {

        }
        #endregion

        #region 项目数据
        private List<ProjectDTO> _Project;

        /// <summary>
        /// 项目数据
        /// </summary>
        public List<ProjectDTO> Project
        {
            get { return _Project; }
            set
            {
                _Project = value;
                RaisePropertyChanged();
            }
        }



        /// <summary>
        /// 获取项目数据
        /// </summary>
        private void GetProjectList()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = "Progame/StatProject";

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)//获取成功
            {
                Project = JsonConvert.DeserializeObject<List<ProjectDTO>>(response.ResultData.ToString());
            }
            else
            {
                Project = new List<ProjectDTO>();
            }
        }

        #endregion

        #region 项目事项数据
        private List<TasksDTO> _TasksList;

        /// <summary>
        /// 备忘录数据
        /// </summary>
        public List<TasksDTO> TasksList
        {
            get { return _TasksList; }
            set
            {
                _TasksList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 获取事项数据
        /// </summary>
        private void GetTasksList()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = "Tasks/StatTasks";

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)//获取成功
            {
                TasksList = JsonConvert.DeserializeObject<List<TasksDTO>>(response.ResultData.ToString());
                
            }
            else
            {
                TasksList = new List<TasksDTO>();
            }
        }

        #endregion

        #region 实现项目搜索栏
        private string _filterText;        
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                RaisePropertyChanged();
                
            }
        }



        public DelegateCommand SelectProCmm { get; set; }

        private ObservableCollection<ProjectDTO> _filteredProjects;
        public ObservableCollection<ProjectDTO> FilteredProjects { get; set; }// 过滤后的项目集合





        private void FilterProjects()
        {
            FilteredProjects.Clear();
            GetProjectList();
            //返回完整数据
            if (FilterText == null)
            {
                var filteredkong = _Project.Where(p => p.ProjectId != null);
                foreach (var project in filteredkong)
                {
                    FilteredProjects.Add(project);
                }
                return;
            }
            var filtered = _Project.Where(p => p.Title.Contains(FilterText, StringComparison.OrdinalIgnoreCase));
                foreach (var project in filtered)
                {
                FilteredProjects.Add(project);
                }
                

            
            
        }
        private void SelectPro()
        {
            
            FilterProjects(); // 过滤项目
            if (FilterText == null)
            {
                
                MessageBox.Show("请输入内容");
                return;
            }

            if (FilterText != null && FilteredProjects.Count == 0)
            {
                MessageBox.Show("找不到内容");
            }
            
        }

        #endregion

        #region 实现事项搜索栏
        private string _filterTextTasks;
        public string FilterTextTasks
        {
            get => _filterTextTasks;
            set
            {
                _filterTextTasks = value;
                RaisePropertyChanged();

            }
        }



        public DelegateCommand SelectTasksCmm { get; set; }

        public ObservableCollection<TasksDTO> FilteredTasks { get; set; } // 过滤后的项目集合





        private void FilterTasks()
        {
            FilteredTasks.Clear();
            GetTasksList();

            if (FilterTextTasks == null)
            {
                var filteredkong = _TasksList.Where(p => p.TaskId != null);
                foreach (var tasks in filteredkong)
                {
                    FilteredTasks.Add(tasks);
                }
                return;

            }
            var filtered = _TasksList.Where(p => p.ProjectTitle.Contains(FilterTextTasks, StringComparison.OrdinalIgnoreCase));
            foreach (var tasks in filtered)
            {
                FilteredTasks.Add(tasks);
            }
            



        }
        private void SelectTasks()
        {

            FilterTasks(); // 过滤项目
            if (FilterTextTasks == null)
            {
                MessageBox.Show("请输入内容");
                
            }
            if (FilterTextTasks != null && FilteredTasks.Count == 0)
            {
                MessageBox.Show("找不到事项对应项目");
            }

        }

        #endregion

        private readonly HttpRestClient HttpClient;//请求api的客户端

        //对话服务(自定义)
        private readonly DialogHostService DialogHostService;

        ///<summary>
        ///构造函数
        ///</summary>
        ///
        public ItemViewModel(HttpRestClient _HttpClient, DialogHostService _DialogHostService, IRegionManager _RegionManager)
        {
            CreateStatPaneList();//创建统计面板数据
             HttpClient= _HttpClient;
            GetProjectList();//创建待办事项模拟数据
            GetTasksList();//获取事项数据
            CallStatNum();//统计num总数
            //对话服务
            DialogHostService = _DialogHostService;

            //打开添加项目命令
            ShowAddProjectDialogCmm = new DelegateCommand(ShowAddProjectDialog);

            //打开添加项目事项命令
            ShowAddTaskDialogCmm = new DelegateCommand(ShowAddTaskDialog);



            //项目搜索栏内容
            SelectProCmm = new DelegateCommand(SelectPro);
            FilteredProjects = new ObservableCollection<ProjectDTO>(_Project);

            //项目事项搜索栏内容
            SelectTasksCmm = new DelegateCommand(SelectTasks);
            FilteredTasks = new ObservableCollection<TasksDTO>(_TasksList);


            //打开编辑项目命令
            ShowEditProjectDialogCmm = new DelegateCommand<ProjectDTO>(ShowEditProjectDialog);

            //打开编辑事项命令
            ShowEditTasksDialogCmm = new DelegateCommand<TasksDTO>(ShowEditTasksDialog);

            //删除Project,task命令
            DelProjectCmm = new DelegateCommand<ProjectDTO>(DelProject);
            DelTaskCmm = new DelegateCommand<TasksDTO>(DelTask);

            //修改待办状态命令
            ChangeTaskStatusCmm = new DelegateCommand<TasksDTO>(ChangeTaskStatus);

        }





        /// <summary>
        /// 创建统计面板数据
        /// </summary>
        private void CreateStatPaneList()
        {
            StatPanelList = new List<StatPanelInfo>();

            StatPanelList.Add(new StatPanelInfo() { Icon = "ClockFast", ItemName = "项目汇总", BackColor = "#FF0CA0FF", ViewName = "WaitUC", Result = "9" });
            StatPanelList.Add(new StatPanelInfo() { Icon = "ClockCheckOutline", ItemName = "已完成项目", BackColor = "#FF1ECA3A", ViewName = "WaitUC", Result = "9" });
            StatPanelList.Add(new StatPanelInfo() { Icon = "ChartLineVariant", ItemName = "事项汇总", BackColor = "#FF02C6DC", Result = "90%" });
            StatPanelList.Add(new StatPanelInfo() { Icon = "PlaylistStar", ItemName = "已完成事项", BackColor = "#FFFFA000", ViewName = "MemoUC", Result = "20" });
        }

        #region 待办事项统计
        //由内容设置为，项目总数，已完成即100%完成度，完成比例，故在api中设置项目数总，和100%总。
        private NumprotaskDTO NumprotaskDTO { get; set; } = new NumprotaskDTO();

        /// <summary>
        /// 调用api获取统计待办实现数据
        /// </summary>
        private void CallStatNum()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = "Progame/NumProject";

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)
            {
                NumprotaskDTO = JsonConvert.DeserializeObject<NumprotaskDTO>(response.ResultData.ToString());

                RefreshWaitStat();
            }
        }

        /// <summary>
        /// 更新待办统计数据
        /// </summary>
        private void RefreshWaitStat()
        {
            StatPanelList[0].Result = NumprotaskDTO.TotalproCount.ToString();
            StatPanelList[1].Result = NumprotaskDTO.FinishproCount.ToString();
            StatPanelList[2].Result = NumprotaskDTO.TotaltaskCount.ToString();
            StatPanelList[3].Result = NumprotaskDTO.FinishtaskCount.ToString();
        }

        public void Totalrefresh()
        {

            FilterProjects();//刷新项目
            //获取事项数据
            FilterTasks();

            CallStatNum();



        }
        #endregion





        #region 添加项目，实现部分
        public DelegateCommand ShowAddProjectDialogCmm { get; set; }

        /// <summary>
        /// 打开添加项目对话框
        /// </summary>
        private async void ShowAddProjectDialog()//async 异步
        {
            var result = await DialogHostService.ShowDialog("AddProjectUC", null);//await等待

            if (result.Result == ButtonResult.OK)
            {
                //接收数据
                if (result.Parameters.ContainsKey("AddProjectUC"))
                {
                    var addModel = result.Parameters.GetValue<ProjectDTO>("AddProjectUC");
                    //调用api实现添加待办事项

                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = RestSharp.Method.POST;
                    apiRequest.Parameters = addModel;
                    apiRequest.Route = "Progame/AddPro";
                    ApiResponse response = HttpClient.Execute(apiRequest);
                    if (response.ResultCode == 1)//添加成功
                    {
                        MessageBox.Show(response.Msg);
                        //刷新统计数据
                        GetProjectList();
                        //CreateStatPaneList();
                    }
                    else
                    {
                        MessageBox.Show(response.Msg);
                    }
                }
            }
        }
        #endregion

        #region 添加项目事项，实现

        public DelegateCommand ShowAddTaskDialogCmm { get; set; }
        private async void ShowAddTaskDialog()//async 异步
        {
            var parameters = new DialogParameters
            {
                { "ProjectList", Project } // 确保将 Project 作为参数传递
            };

            var result = await DialogHostService.ShowDialog("AddTaskUC", parameters, "RootDialog"); // await等待


            if (result.Result == ButtonResult.OK)
            {
                //接收数据
                if (result.Parameters.ContainsKey("AddTaskUC"))
                {
                    var addModel = result.Parameters.GetValue<TasksDTO>("AddTaskUC");
                    //调用api实现添加待办事项

                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = RestSharp.Method.POST;
                    apiRequest.Parameters = addModel;
                    apiRequest.Route = "Tasks/AddTask";
                    ApiResponse response = HttpClient.Execute(apiRequest);
                    if (response.ResultCode == 1)//添加成功
                    {
                        MessageBox.Show(response.Msg);
                        //刷新统计数据
                        GetTasksList();
                        //CreateStatPaneList();
                    }
                    else
                    {
                        MessageBox.Show(response.Msg);
                    }
                }
            }
        }

        #endregion

        #region 编辑项目和项目事项

        public DelegateCommand<ProjectDTO> ShowEditProjectDialogCmm { get; set; }

        /// <summary>
        /// 编辑项目对话框
        /// </summary>
        private async void ShowEditProjectDialog(ProjectDTO projectDTO)//async 异步
        {
            DialogParameters paras = new DialogParameters();
            paras.Add("OldWaitInfo", projectDTO);

            var result = await DialogHostService.ShowDialog("EditProjectUC", paras);//await等待

            if (result.Result == ButtonResult.OK)
            {
                //接收数据
                if (result.Parameters.ContainsKey("ProjectDTO"))
                {
                    var newModel = result.Parameters.GetValue<ProjectDTO>("ProjectDTO");
                    //调用api实现编辑待办事项

                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = RestSharp.Method.PUT;
                    apiRequest.Parameters = newModel;
                    apiRequest.Route = "Progame/EditProject";
                    ApiResponse response = HttpClient.Execute(apiRequest);
                    if (response.ResultCode == 1)//编辑成功
                    {
                        MessageBox.Show(response.Msg);
                        //GetProjectList();//创建待办事项模拟数据
                        //GetTasksList();//获取事项数据
                        Totalrefresh();
                    }
                    else
                    {
                        MessageBox.Show(response.Msg);
                    }
                }
            }
        }


        /// <summary>
        /// 编辑事项
        /// </summary>
        public DelegateCommand<TasksDTO> ShowEditTasksDialogCmm { get; set; }

        private async void ShowEditTasksDialog(TasksDTO tasksDTO)
        {
            DialogParameters paras = new DialogParameters
            {
                { "tasks", tasksDTO },
                { "ProjectList", Project }
            };

            var result = await DialogHostService.ShowDialog("EditTaskUC", paras, "RootDialog");//await等待

            if (result.Result == ButtonResult.OK)
            {
                //接收数据
                if (result.Parameters.ContainsKey("tasksDTO"))
                {
                    var newModel = result.Parameters.GetValue<TasksDTO>("tasksDTO");

                    //调用api实现编辑备忘录

                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = RestSharp.Method.PUT;
                    apiRequest.Parameters = newModel;
                    apiRequest.Route = "Tasks/EditTask";
                    ApiResponse response = HttpClient.Execute(apiRequest);
                    if (response.ResultCode == 1)//编辑成功
                    {
                        MessageBox.Show(response.Msg);

                        Totalrefresh();
                        
                    }
                    else
                    {
                        MessageBox.Show(response.Msg);
                    }
                }
            }
        }

        #endregion

        #region 删除项目，事项
        public DelegateCommand<ProjectDTO> DelProjectCmm { get; set; }//删除命令

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="projectDTO"></param>
        public void DelProject(ProjectDTO projectDTO)
        {
            var selResult = MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.OKCancel);
            if (selResult == MessageBoxResult.OK)
            {
                ApiRequest apiRequest = new ApiRequest();
                apiRequest.Method = RestSharp.Method.DELETE;
                apiRequest.Route = $"Progame/DelProject?Title={projectDTO.Title}";

                ApiResponse response = HttpClient.Execute(apiRequest);

                if (response.ResultCode == 1)//删除成功
                {
                    MessageBox.Show(response.Msg);
                    //FilterProjects();//创建待办事项模拟数据
                    //GetTasksList();//获取事项数据
                    Totalrefresh();
                }
                else
                {
                    MessageBox.Show(response.Msg);
                }
            }
        }

        public DelegateCommand<TasksDTO> DelTaskCmm { get; set; }//删除命令

        /// <summary>
        /// 删除待办事项
        /// </summary>
        /// <param name="tasksDTO"></param>
        public void DelTask(TasksDTO tasksDTO)
        {
            var selResult = MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.OKCancel);
            if (selResult == MessageBoxResult.OK)
            {
                ApiRequest apiRequest = new ApiRequest();
                apiRequest.Method = RestSharp.Method.DELETE;
                apiRequest.Route = $"Tasks/DelTasks?TaskId={tasksDTO.TaskId}";

                ApiResponse response = HttpClient.Execute(apiRequest);

                if (response.ResultCode == 1)//删除成功
                {
                    MessageBox.Show(response.Msg);
                    Totalrefresh();// 刷新数据
                }
                else
                {
                    MessageBox.Show(response.Msg);
                }
            }
        }

        #endregion

        #region 修改事项的状态
        public DelegateCommand<TasksDTO> ChangeTaskStatusCmm { get; set; }

        /// <summary>
        /// 修改待办事项的状态
        /// </summary>
        private void ChangeTaskStatus(TasksDTO tasksDTO)
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.PUT;
            apiRequest.Parameters = tasksDTO;
            apiRequest.Route = "Tasks/UpdateStatus";
            ApiResponse response = HttpClient.Execute(apiRequest);
            if (response.ResultCode == 1)//修改成功
            {
                Totalrefresh();
            }
            else
            {
                MessageBox.Show(response.Msg);
            }
        }
        #endregion
    }
}
