using Microsoft.VisualBasic;
using pm.api.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pm.api.DataModel
{
    /// <summary>
    /// 待办事项数据模型
    /// </summary>
    [Table("Project")]
    public class Project
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        [Key]
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目标题
        /// </summary>
        public string ?Title { get; set; }


        /// <summary>
        /// 项目描述
        /// </summary>
        public string? Description { get; set; } // 项目描述

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; } // 开始日期

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; } // 结束日期



        /// <summary>
        /// 重要性 0-适中，1-重要 ,2-次要
        /// </summary>
        public int Emergency { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string ?Mainson { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 事项总数
        /// </summary>
        public int TotalTasks { get; set; }

        /// <summary>
        /// 事项已完成数
        /// </summary>
        public int CompletedTasks { get; set; }
    }
}

