
using Newtonsoft.Json;

using Prism.Mvvm;
using Prism.Regions;
using promaneger.DTOs;
using promaneger.HttpClients;
using promaneger.Service;
using Syncfusion.ProjIO;
using Syncfusion.UI.Xaml.Kanban;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace promaneger.ViewModels
{
    class TodoViewModel : BindableBase,INotifyPropertyChanged
    {

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

        private readonly HttpRestClient HttpClient;//请求api的客户端

        //对话服务(自定义)
        private readonly DialogHostService DialogHostService;

        ///<summary>
        ///构造函数
        ///</summary>
        ///
        public TodoViewModel(HttpRestClient _HttpClient, DialogHostService _DialogHostService, IRegionManager _RegionManager)
        {
             HttpClient = _HttpClient;
         
            GetTasksList();//获取事项数据
      
            //对话服务
            DialogHostService = _DialogHostService;

            TaskDetails();//kanban
        


        }

       



        #region 看板数据获取


        public ObservableCollection<KanbanModel> Tasks { get; set; }

        public void TaskDetails()
        {
            Tasks = new ObservableCollection<KanbanModel>();
            Tasks.Add(new KanbanModel()
            {
                Title = "Universal App",//任务标题
                ID = "27654",//
                Description = "Incorporate feedback into functional specifications",//任务描述
                Category = "Open",
                ColorKey = "Low",
                Tags = new string[] { "Deployment" },
                ImageURL = new Uri("/images/icon.jpg", UriKind.RelativeOrAbsolute)
            });

            

        

            // 将 TasksList 中的数据复制到 task 中
            foreach (var t in TasksList)
            {
                Tasks.Add(new KanbanModel()
                {
                    Title = t.Title,//任务标题
                    ID = t.TaskId.ToString(),//
                    Description = t.Description,//任务描述
                    Category = t.Status switch//(例如：0=Open, 1=In Progress, 2=Review,Done)
                    {
                        1 => "Open",
                        0 => "In Progress",
                        2 => "Review,Done",

                    },
                    ColorKey = "Low",
                    Tags = new string[] { "Deployment" },
                    ImageURL = new Uri("/images/icon.jpg", UriKind.RelativeOrAbsolute)
                   
                });
            }








        }

        #endregion

        #region
        
        #endregion



    }
}
