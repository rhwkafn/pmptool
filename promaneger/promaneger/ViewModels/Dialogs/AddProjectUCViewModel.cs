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
using System.ComponentModel;

namespace promaneger.ViewModels.Dialogs
{
    /// <summary>
    /// 添加待办实现视图模型
    /// </summary>
    internal class AddProjectUCViewModel : IDialogHostAware
    {
        /// <summary>
        /// 确定命令
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }

        /// <summary>
        /// 取消命令
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        /// <summary>
        /// 打开过程执行
        /// </summary>
        /// <param name="parameters"></param>

        public void OnDialogOpening(IDialogParameters parameters)
        {

        }

        // 通知属性更改函数
        public event PropertyChangedEventHandler PropertyChanged; // 确保声明事件
        // 确保 OnPropertyChanged 方法的实现如下
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public AddProjectUCViewModel()
        {
            SaveCommand=new DelegateCommand(Save);
            CancelCommand=new DelegateCommand(Cancel);  
        }
        /// <summary>
        /// 确定方法
        /// </summary>
        private void Save()
        {
            if (string.IsNullOrEmpty(ProjectDTO.Title) || string.IsNullOrEmpty(ProjectDTO.Description) || string.IsNullOrEmpty(ProjectDTO.Mainson) )
            {
                MessageBox.Show("项目信息不全");
                return;
            }

            if (DialogHost.IsDialogOpen(DailogHostName))
            {
                DialogParameters paras = new DialogParameters();
                paras.Add("AddProjectUC", ProjectDTO);
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
                DialogHost.Close(DailogHostName,new DialogResult( ButtonResult.No));
            }
        }

        /// <summary>
        /// 待办事项信息
        /// </summary>
        public ProjectDTO ProjectDTO { get; set; } = new ProjectDTO();

        /// <summary>
        /// 对话框主机唯一标识
        /// </summary>
        private const string DailogHostName = "RootDialog";
    }
}
