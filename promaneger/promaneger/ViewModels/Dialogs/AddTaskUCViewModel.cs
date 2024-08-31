using promaneger.DTOs;
using promaneger.Service;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using DryIoc;
using Prism.Mvvm;

using System.Collections.Specialized;
using System.ComponentModel;

namespace promaneger.ViewModels.Dialogs
{
    /// <summary>
    /// 添加待办实现视图模型
    /// </summary>
    internal class AddTaskUCViewModel :BindableBase,IDialogHostAware,INotifyPropertyChanged
    {
       
        /// <summary>
        /// 确定命令
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }

        /// <summary>
        /// 取消命令
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        //读取项目标题
        private List<string> _Projectname;

        public List<string> Projectname
        {
            get { return _Projectname; }
            set { _Projectname = value;
                RaisePropertyChanged();
            }
        }
        #region 新增属性
        // 通知属性更改函数
        public event PropertyChangedEventHandler PropertyChanged; // 确保声明事件
        public event Action<IDialogResult> RequestClose;

        // 确保 OnPropertyChanged 方法的实现如下
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        /// <summary>
        /// 打开过程执行
        /// </summary>
        /// <param name="parameters"></param>

        public void OnDialogOpening(IDialogParameters parameters)
        {

            // 确保参数是正确的类型
            if (parameters.TryGetValue("ProjectList", out List<ProjectDTO> projectList))
            {
                // 将项目标题转换为字符串列表并赋值给 Projectname
                Projectname = projectList.Select(p => p.Title).ToList();
                // 通知属性更改
                OnPropertyChanged(nameof(Projectname));
            }
            else
            {
                // 处理参数未找到的情况
            }

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AddTaskUCViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);

        }
        /// <summary>
        /// 确定方法
        /// </summary>
        private void Save()
        {
             if (string.IsNullOrEmpty(TasksDTO.Title) || string.IsNullOrEmpty(TasksDTO.Description) || string.IsNullOrEmpty(TasksDTO.ProjectTitle) )
            {
                MessageBox.Show("项目信息不全");
                return;
            }

            if (DialogHost.IsDialogOpen(DailogHostName))
            {
                DialogParameters paras = new DialogParameters();
                paras.Add("AddTaskUC", TasksDTO);
                DialogHost.Close(DailogHostName, new DialogResult(ButtonResult.OK, paras));
            }
        }

        /// <summary>
        /// 取消方法
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DailogHostName))
            {
                DialogHost.Close(DailogHostName, new DialogResult(ButtonResult.No));
            }
        }


        /// <summary>
        /// 事项信息
        /// </summary>
        public TasksDTO TasksDTO { get; set; } = new TasksDTO();

        public string Title => throw new NotImplementedException();

        /// <summary>
        /// 对话框主机唯一标识
        /// </summary>
        private const string DailogHostName = "RootDialog";
    }
}
