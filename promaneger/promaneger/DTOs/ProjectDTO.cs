using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace promaneger.DTOs
{
    internal class ProjectDTO
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        [Key]
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目标题
        /// </summary>
        public string? Title { get; set; }


        /// <summary>
        /// 项目描述
        /// </summary>
        public string? Description { get; set; } // 项目描述

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate { get; set; } = DateTime.Now;// 开始日期

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; } = DateTime.Now; // 结束日期


        /// <summary>
        /// 重要性 0-适中，1-重要 ,-1-次要
        /// </summary>
        public int Emergency { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string? Mainson { get; set; }

        /// <summary>
        /// 事项总数
        /// </summary>
        public int TotalTasks { get; set; }

        /// <summary>
        /// 事项已完成数
        /// </summary>
        public int CompletedTasks { get; set; }

        /// <summary>
        /// 计算进度百分比
        /// </summary>
        public double ProgressPercentage => TotalTasks == 0 ? 0 : Math.Round((double)CompletedTasks / TotalTasks * 100, 2); // 保留两位小数

        /// <summary>
        /// 获取重要性状态
        /// </summary>
        public string EmergencyStatus => Emergency switch
        {
            1 => "重要",
            0 => "适中",
            2 => "次要",

        };
    }


}

