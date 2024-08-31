using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace promaneger.Views.Documents
{
    /// <summary>
    /// ShouweiView.xaml 的交互逻辑
    /// </summary>
    public partial class ShouweiView : UserControl
    {
        public ShouweiView()
        {
            InitializeComponent();
            DataContext = this;

        }


        private void treeView_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem selectedItem)
            {
                string header = selectedItem.Header.ToString();
                //根据选中的项目执行相应的操作，例如展开或收回相应的Expander
                switch (header)
                {
                    case "展示全部":
                        KuaiSuCaoZuo.Visibility = Visibility.Visible;
                        GuanLiFangAn.Visibility = Visibility.Visible;

                        expanderXiangMuZhengHeGuanLi.Visibility = Visibility.Visible;
                      
                        expanderXiangMuCaiGouGuanLi.Visibility = Visibility.Visible;
                        
                        expanderShuJuMoBan.Visibility = Visibility.Visible;
                        expanderWenTi.Visibility = Visibility.Visible;
                        break;

                    case "快速操作":
                        KuaiSuCaoZuo.IsExpanded = true;

                        KuaiSuCaoZuo.Visibility = Visibility.Visible;
                        GuanLiFangAn.Visibility = Visibility.Collapsed;
                        expanderXiangMuZhengHeGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuFanWeiGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuShiJianGuanLi.Visibility = Visibility.Hidden;
                        expanderXiangMuChengBenGuanLi.Visibility = Visibility.Collapsed;

                        expanderXiangMuZhiLiangGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuRenLiZiYuanGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuGouTongGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuFengXianGuanLi.Visibility = Visibility.Collapsed;

                        expanderXiangMuCaiGouGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuGanXianRenGuanLi.Visibility = Visibility.Collapsed;
                        expanderShuJuMoBan.Visibility = Visibility.Collapsed;
                        expanderWenTi.Visibility = Visibility.Collapsed;
                        break;
                    case "管理方案":
                        KuaiSuCaoZuo.Visibility = Visibility.Collapsed;
                        GuanLiFangAn.Visibility = Visibility.Visible;
                        expanderXiangMuZhengHeGuanLi.Visibility = Visibility.Visible;
          
                        expanderXiangMuCaiGouGuanLi.Visibility = Visibility.Visible;
                       
                        expanderShuJuMoBan.Visibility = Visibility.Collapsed;
                        expanderWenTi.Visibility = Visibility.Collapsed;
                        break;
                    case "数据模板":
                        KuaiSuCaoZuo.Visibility = Visibility.Collapsed;
                        GuanLiFangAn.Visibility = Visibility.Collapsed;
                        expanderXiangMuZhengHeGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuFanWeiGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuShiJianGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuChengBenGuanLi.Visibility = Visibility.Collapsed;

                        expanderXiangMuZhiLiangGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuRenLiZiYuanGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuGouTongGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuFengXianGuanLi.Visibility = Visibility.Collapsed;

                        expanderXiangMuCaiGouGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuGanXianRenGuanLi.Visibility = Visibility.Collapsed;
                        expanderShuJuMoBan.Visibility = Visibility.Visible;
                        expanderWenTi.Visibility = Visibility.Collapsed;
                        break;
                    case "问题":
                        KuaiSuCaoZuo.Visibility = Visibility.Collapsed;
                        GuanLiFangAn.Visibility = Visibility.Collapsed;
                        expanderXiangMuZhengHeGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuFanWeiGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuShiJianGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuChengBenGuanLi.Visibility = Visibility.Collapsed;

                        expanderXiangMuZhiLiangGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuRenLiZiYuanGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuGouTongGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuFengXianGuanLi.Visibility = Visibility.Collapsed;

                        expanderXiangMuCaiGouGuanLi.Visibility = Visibility.Collapsed;
                        expanderXiangMuGanXianRenGuanLi.Visibility = Visibility.Collapsed;
                        expanderShuJuMoBan.Visibility = Visibility.Collapsed;
                        expanderWenTi.Visibility = Visibility.Visible;
                        break;


                    #region 管理方案
                    case "项目整合管理":

                        expanderXiangMuZhengHeGuanLi.IsExpanded = true;
                        expanderXiangMuFanWeiGuanLi.IsExpanded = false;
                        expanderXiangMuShiJianGuanLi.IsExpanded = false;
                        expanderXiangMuChengBenGuanLi.IsExpanded = false;

                        expanderXiangMuZhiLiangGuanLi.IsExpanded = false;
                        expanderXiangMuRenLiZiYuanGuanLi.IsExpanded = false;
                        expanderXiangMuGouTongGuanLi.IsExpanded = false;
                        expanderXiangMuFengXianGuanLi.IsExpanded = false;

                        expanderXiangMuCaiGouGuanLi.IsExpanded = false;
                        expanderXiangMuGanXianRenGuanLi.IsExpanded = false;
                        //expanderShuJuMoBan.Visibility = Visibility.Collapsed;
                        //expanderWenTi.Visibility = Visibility.Collapsed;
                        break;
                    case "项目范围管理":
                        expanderXiangMuZhengHeGuanLi.IsExpanded = false;
                        expanderXiangMuFanWeiGuanLi.IsExpanded = true;
                        expanderXiangMuShiJianGuanLi.IsExpanded = false;
                        expanderXiangMuChengBenGuanLi.IsExpanded = false;

                        expanderXiangMuZhiLiangGuanLi.IsExpanded = false;
                        expanderXiangMuRenLiZiYuanGuanLi.IsExpanded = false;
                        expanderXiangMuGouTongGuanLi.IsExpanded = false;
                        expanderXiangMuFengXianGuanLi.IsExpanded = false;

                        expanderXiangMuCaiGouGuanLi.IsExpanded = false;
                        expanderXiangMuGanXianRenGuanLi.IsExpanded = false;
                        break;
                    case "项目时间管理":
                        expanderXiangMuZhengHeGuanLi.IsExpanded = false;
                        expanderXiangMuFanWeiGuanLi.IsExpanded = false;
                        expanderXiangMuShiJianGuanLi.IsExpanded = true;
                        expanderXiangMuChengBenGuanLi.IsExpanded = false;

                        expanderXiangMuZhiLiangGuanLi.IsExpanded = false;
                        expanderXiangMuRenLiZiYuanGuanLi.IsExpanded = false;
                        expanderXiangMuGouTongGuanLi.IsExpanded = false;
                        expanderXiangMuFengXianGuanLi.IsExpanded = false;

                        expanderXiangMuCaiGouGuanLi.IsExpanded = false;
                        expanderXiangMuGanXianRenGuanLi.IsExpanded = false;
                        break;
                    case "项目成本管理":
                        expanderXiangMuZhengHeGuanLi.IsExpanded = false;
                        expanderXiangMuFanWeiGuanLi.IsExpanded = false;
                        expanderXiangMuShiJianGuanLi.IsExpanded = false;
                        expanderXiangMuChengBenGuanLi.IsExpanded = true;

                        expanderXiangMuZhiLiangGuanLi.IsExpanded = false;
                        expanderXiangMuRenLiZiYuanGuanLi.IsExpanded = false;
                        expanderXiangMuGouTongGuanLi.IsExpanded = false;
                        expanderXiangMuFengXianGuanLi.IsExpanded = false;

                        expanderXiangMuCaiGouGuanLi.IsExpanded = false;
                        expanderXiangMuGanXianRenGuanLi.IsExpanded = false;
                        break;
                    case "项目质量管理":
                        expanderXiangMuZhengHeGuanLi.IsExpanded = false;
                        expanderXiangMuFanWeiGuanLi.IsExpanded = false;
                        expanderXiangMuShiJianGuanLi.IsExpanded = false;
                        expanderXiangMuChengBenGuanLi.IsExpanded = false;

                        expanderXiangMuZhiLiangGuanLi.IsExpanded = true;
                        expanderXiangMuRenLiZiYuanGuanLi.IsExpanded = false;
                        expanderXiangMuGouTongGuanLi.IsExpanded = false;
                        expanderXiangMuFengXianGuanLi.IsExpanded = false;

                        expanderXiangMuCaiGouGuanLi.IsExpanded = false;
                        expanderXiangMuGanXianRenGuanLi.IsExpanded = false;
                        break;
                    case "项目人力资源管理":
                        expanderXiangMuZhengHeGuanLi.IsExpanded = false;
                        expanderXiangMuFanWeiGuanLi.IsExpanded = false;
                        expanderXiangMuShiJianGuanLi.IsExpanded = false;
                        expanderXiangMuChengBenGuanLi.IsExpanded = false;

                        expanderXiangMuZhiLiangGuanLi.IsExpanded = false;
                        expanderXiangMuRenLiZiYuanGuanLi.IsExpanded = true;
                        expanderXiangMuGouTongGuanLi.IsExpanded = false;
                        expanderXiangMuFengXianGuanLi.IsExpanded = false;

                        expanderXiangMuCaiGouGuanLi.IsExpanded = false;
                        expanderXiangMuGanXianRenGuanLi.IsExpanded = false;
                        break;
                    case "项目沟通管理":
                        expanderXiangMuZhengHeGuanLi.IsExpanded = false;
                        expanderXiangMuFanWeiGuanLi.IsExpanded = false;
                        expanderXiangMuShiJianGuanLi.IsExpanded = false;
                        expanderXiangMuChengBenGuanLi.IsExpanded = false;

                        expanderXiangMuZhiLiangGuanLi.IsExpanded = false;
                        expanderXiangMuRenLiZiYuanGuanLi.IsExpanded = false;
                        expanderXiangMuGouTongGuanLi.IsExpanded = true;
                        expanderXiangMuFengXianGuanLi.IsExpanded = false;

                        expanderXiangMuCaiGouGuanLi.IsExpanded = false;
                        expanderXiangMuGanXianRenGuanLi.IsExpanded = false;
                        break;
                    case "项目风险管理":
                        expanderXiangMuZhengHeGuanLi.IsExpanded = false;
                        expanderXiangMuFanWeiGuanLi.IsExpanded = false;
                        expanderXiangMuShiJianGuanLi.IsExpanded = false;
                        expanderXiangMuChengBenGuanLi.IsExpanded = false;

                        expanderXiangMuZhiLiangGuanLi.IsExpanded = false;
                        expanderXiangMuRenLiZiYuanGuanLi.IsExpanded = false;
                        expanderXiangMuGouTongGuanLi.IsExpanded = false;
                        expanderXiangMuFengXianGuanLi.IsExpanded = true;

                        expanderXiangMuCaiGouGuanLi.IsExpanded = false;
                        expanderXiangMuGanXianRenGuanLi.IsExpanded = false;
                        break;
                    case "项目采购管理":
                        expanderXiangMuZhengHeGuanLi.IsExpanded = false;
                        expanderXiangMuFanWeiGuanLi.IsExpanded = false;
                        expanderXiangMuShiJianGuanLi.IsExpanded = false;
                        expanderXiangMuChengBenGuanLi.IsExpanded = false;

                        expanderXiangMuZhiLiangGuanLi.IsExpanded = false;
                        expanderXiangMuRenLiZiYuanGuanLi.IsExpanded = false;
                        expanderXiangMuGouTongGuanLi.IsExpanded = false;
                        expanderXiangMuFengXianGuanLi.IsExpanded = false;

                        expanderXiangMuCaiGouGuanLi.IsExpanded = true;
                        expanderXiangMuGanXianRenGuanLi.IsExpanded = false;
                        break;
                    case "项目干系人管理":
                        expanderXiangMuZhengHeGuanLi.IsExpanded = false;
                        expanderXiangMuFanWeiGuanLi.IsExpanded = false;
                        expanderXiangMuShiJianGuanLi.IsExpanded = false;
                        expanderXiangMuChengBenGuanLi.IsExpanded = false;

                        expanderXiangMuZhiLiangGuanLi.IsExpanded = false;
                        expanderXiangMuRenLiZiYuanGuanLi.IsExpanded = false;
                        expanderXiangMuGouTongGuanLi.IsExpanded = false;
                        expanderXiangMuFengXianGuanLi.IsExpanded = false;

                        expanderXiangMuCaiGouGuanLi.IsExpanded = false;
                        expanderXiangMuGanXianRenGuanLi.IsExpanded = true;
                        break;




                        #endregion
                }
            }
        }

    }
}
