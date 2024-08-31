using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace promaneger.DTOs
{
    internal class TasksDTO
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [Key]
        public int TaskId { get; set; } // 任务ID
        /// <summary>
        /// 任务标题
        /// </summary>
        public string? Title { get; set; } // 任务标题

        /// <summary>
        /// 任务描述
        /// </summary>
        public string? Description { get; set; } // 任务描述

        /// <summary>
        /// 任务状态 (例如：0=未开始, 1=进行中, 2=已完成)
        /// </summary>
        public int Status { get; set; } // 任务状态

        /// <summary>
        /// 是否完成 (例如：0=未完成, 1=已完成)
        /// </summary>
        public int WithStatus => Status switch
        {
            1 => 0,
            0 => 0,
            2 => 1,

        };

        /// <summary>
        /// 获取已完成状态
        /// </summary>
        public string DoneStatus => Status switch
        {
            1 => "进行中",
            0 => "未开始",
            2 => "已完成",

        };

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StarttaskDate { get; set; } = DateTime.Now;// 开始日期

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime EndtaskDate { get; set; } = DateTime.Now;// 截止日期,默认现在

        /// <summary>
        /// 所属项目ID
        /// </summary>
        public string? ProjectTitle { get; set; } // 所属项目ID
    }
}
