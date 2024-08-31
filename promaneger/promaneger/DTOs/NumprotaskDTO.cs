using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace promaneger.DTOs
{
    /// <summary>
    /// 接收API待办事项统计的数据模型
    /// </summary>
    internal class NumprotaskDTO
    {
        /// <summary>
        /// 项目总数
        /// </summary>
        public int TotalproCount { get; set; }

        /// <summary>
        /// 已完成项目数量
        /// </summary>
        public int FinishproCount { get; set; }


        /// <summary>
        /// 事项总数
        /// </summary>
        public int TotaltaskCount { get; set; }

        /// <summary>
        /// 已完成事项数量
        /// </summary>
        public int FinishtaskCount { get; set; }
    }
}
