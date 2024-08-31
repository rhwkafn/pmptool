using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pm.api.ApiReponses;
using pm.api.DataModel;
using pm.api.DTOs;
using System.Security.Principal;

namespace pm.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProgameController : ControllerBase
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly DailyDbContext db;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>
        public ProgameController(DailyDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="prodto">项目信息</param>
        /// <returns>1:添加成功，-1:添加失败，-99异常</returns>
        [HttpPost]
        public IActionResult AddPro(ProjectDTO prodto)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //项目已存在则不添加
                var dbproject =db.Project.Where( t=> t.Title == prodto.Title).FirstOrDefault();
                if (dbproject != null)
                {
                    response.ResultCode = -1;//项目已存在
                    response.Msg = "对不起,项目已添加";

                    return Ok(response);
                }
                //DTO->Info
                Project innerInfo = mapper.Map<Project>(prodto);
                db.Project.Add(innerInfo);
                int result = db.SaveChanges();//受影响的行数
                if (result == 1)
                {
                    response.ResultCode = 1;
                    response.Msg = "添加项目成功";
                }
                else
                {
                    response.ResultCode = -1;
                    response.Msg = "添加项目失败";
                }
            }
            catch (Exception ex) // 捕获具体异常信息
            {
                response.ResultCode = -99;
                response.Msg = $"服务器忙,请稍后.... 错误信息: {ex.Message}";
            }

            return Ok(response);
        }


        /// <summary>
        /// 获取项目所有数据，自动计算完成数，总数
        /// </summary>
        /// <returns>1:统计成功,-99:异常</returns>
        [HttpGet]
        public IActionResult StatProject()
        {
            ApiResponse res = new ApiResponse();

            try
            {
                //linq语法
                var list = (from a in db.Project
                             where a.Title != null
                             join task in db.Tasks on a.Title equals task.ProjectTitle into tasksGroup//筛选出对应项目的已完成数和总数
                             from task in tasksGroup.DefaultIfEmpty()
                             select new { a, task })
                            .AsEnumerable() // 转换为客户端评估
                            .GroupBy(x => new
                            {
                                x.a.ProjectId,
                                x.a.Title,
                                x.a.Description,
                                x.a.StartDate,
                                x.a.EndDate,
                                x.a.Emergency,
                                x.a.Mainson
                            })
                            .Select(g => new Project
                            {
                                ProjectId = g.Key.ProjectId,
                                Title = g.Key.Title,
                                Description = g.Key.Description,
                                StartDate = g.Key.StartDate,
                                EndDate = g.Key.EndDate,
                                Emergency = g.Key.Emergency,
                                Mainson = g.Key.Mainson,
                                TotalTasks = g.Count(t => t.task != null),
                                CompletedTasks = g.Sum(t => t.task?.Status == 2 ? 1 : 0)
                            });
                var yuanresult = list.ToList();
                //以下用于更新数据库
                var query = (from a in db.Project
                             where a.Title != null
                             join task in db.Tasks on a.Title equals task.ProjectTitle into tasksGroup//筛选出对应项目的已完成数和总数
                             from task in tasksGroup.DefaultIfEmpty()
                             select new { a, task })
                            .AsEnumerable() // 转换为客户端评估
                            .GroupBy(x => new
                            {
                                x.a.Title,
                                x.a.Description,
                                x.a.StartDate,
                                x.a.EndDate,
                                x.a.Emergency,
                                x.a.Mainson
                            })
                            .Select(g => new
                            {
                                Project = new Project
                                {
                                    Title = g.Key.Title,
                                    Description = g.Key.Description,
                                    StartDate = g.Key.StartDate,
                                    EndDate = g.Key.EndDate,
                                    Emergency = g.Key.Emergency,
                                    Mainson = g.Key.Mainson,
                                    TotalTasks = g.Count(t => t.task != null),
                                    CompletedTasks = g.Sum(t => t.task?.Status == 2 ? 1 : 0)
                                },
                                ProjectId = g.Key.Title // 假设Title是唯一标识符
                            });

                var result = query.ToList();

                // 更新数据库中的TotalTasks和CompletedTasks
                foreach (var item in result)
                {
                    var projectToUpdate = db.Project.FirstOrDefault(p => p.Title == item.ProjectId);
                    if (projectToUpdate != null)
                    {
                        projectToUpdate.TotalTasks = item.Project.TotalTasks;
                        projectToUpdate.CompletedTasks = item.Project.CompletedTasks;
                    }
                }
                db.SaveChanges(); // 保存更改

                res.ResultCode = 1; // 统计成功
                res.Msg = "统计待办事项成功";
                res.ResultData = yuanresult; // 返回不同标题的项目
            }
            catch (Exception ex) // 捕获具体异常信息
            {
                res.ResultCode = -99;
                res.Msg = $"服务器忙,请稍后.... 错误信息: {ex.Message}";
            }

            return Ok(res);
        }

        /// <summary>
        /// 统计项目和项目事项数据
        /// </summary>
        /// <returns>1:统计成功,-99:异常</returns>
        [HttpGet]
        public IActionResult NumProject()
        {
            ApiResponse res = new ApiResponse();

            try
            {
                var list1 = db.Project.ToList();//所有项目记录
                var finishList1 = list1.Where(t => t.TotalTasks == t.CompletedTasks).ToList();
                var list2 = db.Tasks.ToList();//所有事项记录
                var finishList2 = list2.Where(t => t.Status == 2).ToList();

                NumprotaskDTO NumprotaskDTO = new NumprotaskDTO { TotalproCount = list1.Count,
                    FinishproCount = finishList1.Count,
                    TotaltaskCount = list2.Count,
                    FinishtaskCount=finishList2.Count
                };

                res.ResultCode = 1;//统计成功
                res.Msg = "统计待办事项成功";
                res.ResultData = NumprotaskDTO;
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "服务器忙,请稍后...";
            }

            return Ok(res);
        }


        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="newDto">新项目</param>
        /// <returns>1:修改成功，-99异常，-1:状态id错误</returns>
        [HttpPut]
        public IActionResult EditProject(ProjectDTO newDto)
        {
            ApiResponse res = new ApiResponse();

            try
            {
                var dbInfo = db.Project.FirstOrDefault(p => p.ProjectId == newDto.ProjectId); // 使用 id 筛选



                if (dbInfo != null)
                {
                    dbInfo.Description = newDto.Description;
                    dbInfo.Title = newDto.Title;
                    dbInfo.StartDate = newDto.StartDate;
                    dbInfo.EndDate = newDto.EndDate;
                    dbInfo.Emergency = newDto.Emergency;
                    dbInfo.Mainson = newDto.Mainson;

                    int result = db.SaveChanges();
                    if (result == 1)
                    {
                        res.ResultCode = 1;
                        res.Msg = "编辑成功";
                    }
                    else
                    {
                        res.ResultCode = -1;
                        res.Msg = "编辑失败";
                    }
                }
                else
                {
                    res.ResultCode = -1;
                    res.Msg = "请确认项目名称是否正确";
                }
            }
            catch (Exception ex) // 捕获具体异常信息
            {
                res.ResultCode = -99;
                res.Msg = $"服务器忙,请稍后.... 错误信息: {ex.Message}";
            }

            return Ok(res);
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="Title">项目名称</param>
        /// <returns>1:删除成功 -2:ID传错了 -1:删除失败 -99:异常</returns>
        [HttpDelete]
        public IActionResult DelProject(string Title)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var dbInfo = db.Project.FirstOrDefault(p => p.Title == Title); // 使用 Title 筛选

                if (dbInfo == null)
                {
                    apiResponse.ResultCode = -2;
                    apiResponse.Msg = "请正确传项目名称";

                    return Ok(apiResponse);
                }

                db.Project.Remove(dbInfo);
                int result = db.SaveChanges();
                if (result == 1)
                {
                    apiResponse.ResultCode = 1;
                    apiResponse.Msg = "项目删除成功";
                }
                else
                {
                    apiResponse.ResultCode = -1;
                    apiResponse.Msg = "项目删除失败";
                }
            }
            catch (Exception ex) // 捕获具体异常信息
            {
                apiResponse.ResultCode = -99;
                apiResponse.Msg = $"服务器忙,请稍后.... 错误信息: {ex.Message}";
            }

            return Ok(apiResponse);
        }
    }


}

    

