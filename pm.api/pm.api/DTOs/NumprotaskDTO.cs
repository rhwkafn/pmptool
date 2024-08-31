namespace pm.api.DTOs
{
    /// <summary>
    /// 接收API统计pro，task总数的数据模型,小写无s
    /// </summary>
    public class NumprotaskDTO
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
