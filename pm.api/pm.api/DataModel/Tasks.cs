using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pm.api.DataModel
{
    /// <summary>
    /// 任务
    /// </summary>
    [Table("Tasks")]
    public class Tasks
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
        /// 开始日期
        /// </summary>
        public DateTime StarttaskDate { get; set; } // 开始日期

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime EndtaskDate { get; set; } // 截止日期

        /// <summary>
        /// 所属项目ID
        /// </summary>
        public string? ProjectTitle { get; set; } // 所属项目ID
    }
}
