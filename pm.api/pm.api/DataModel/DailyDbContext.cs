using Microsoft.EntityFrameworkCore;

namespace pm.api.DataModel
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DailyDbContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public DailyDbContext(DbContextOptions<DailyDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// 账号
        /// </summary>
        public virtual DbSet<AccountInfo> AccountInfo { get; set; }



        /// <summary>
        /// 备忘录
        /// </summary>
        public virtual DbSet<MemoInfo> MemoInfo { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public virtual DbSet<Project> Project { get; set; }
        ///<summary>
        ///任务
        ///</summary>
        public virtual DbSet<Tasks> Tasks { get; set; }
    }
}
