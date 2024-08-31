using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pm.api.ApiReponses;
using pm.api.DataModel;
using pm.api.DTOs;
using System.Threading.Tasks;

namespace pm.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TasksController : ControllerBase
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
        public TasksController(DailyDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }




        /// <summary>
        /// 添加项目事项
        /// </summary>
        /// <param name="task">项目事项信息</param>
        /// <returns>1:添加成功，-1:添加失败，-99异常</returns>
        [HttpPost]
        public IActionResult AddTask(Tasks task)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                //DTO->Info
                Tasks innerInfo = mapper.Map<Tasks>(task);
                db.Tasks.Add(innerInfo);
                int result = db.SaveChanges();//受影响的行数
                if (result == 1)
                {
                    response.ResultCode = 1;
                    response.Msg = "添加待办事项成功";
                }
                else
                {
                    response.ResultCode = -1;
                    response.Msg = "添加待办事项失败";
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
        /// 统计事项数据
        /// </summary>
        /// <returns>1:统计成功,-99:异常</returns>
        [HttpGet]
        public IActionResult StatTasks()
        {
            ApiResponse res = new ApiResponse();

            try
            {
                //linq语法
                var list = from A in db.Tasks
                           where A.Status != null
                           select new TasksDTO
                           {
                               TaskId = A.TaskId,
                               Title = A.Title,
                               Description = A.Description,
                               Status = A.Status,
                               StarttaskDate = A.StarttaskDate,
                               EndtaskDate = A.EndtaskDate,
                               ProjectTitle = A.ProjectTitle,
                           };

                res.ResultCode = 1;
                res.Msg = "获取成功";
                res.ResultData = list;
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "服务器忙,请稍后...";
            }
            return Ok(res);
        }

        /// <summary>
        /// 修改事项状态
        /// </summary>
        /// <param name="newDto">新状态的事项</param>
        /// <returns>1:修改成功，-99异常，-1:状态id错误</returns>
        [HttpPut]
        public IActionResult UpdateStatus(TasksDTO newDto)
        {
            ApiResponse res = new ApiResponse();

            try
            {
                var dbInfo = db.Tasks.FirstOrDefault(p => p.TaskId == newDto.TaskId); // 使用 id筛选



                if (dbInfo != null)
                {
                    dbInfo.Description = newDto.Description;
                    dbInfo.Title = newDto.Title;
                    dbInfo.StarttaskDate = newDto.StarttaskDate;
                    dbInfo.EndtaskDate = newDto.EndtaskDate;
                    dbInfo.Status = newDto.Status == 2 ? 0: 2;
                    dbInfo.ProjectTitle = newDto.ProjectTitle;

                    int result = db.SaveChanges();
                    if (result == 1)
                    {
                        res.ResultCode = 1;
                        res.Msg = "修改事项状态成功";
                    }
                    else
                    {
                        res.ResultCode = -1;
                        res.Msg = "修改事项状态失败";
                    }
                }
                else
                {
                    res.ResultCode = -1;
                    
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
        /// 修改待办事项
        /// </summary>
        /// <param name="newDto">新待办事项</param>
        /// <returns>1:修改成功，-99异常，-1:状态id错误</returns>
        [HttpPut]
        public IActionResult EditTask(TasksDTO newDto)
        {
            ApiResponse res = new ApiResponse();

            try
            {
                var dbInfo = db.Tasks.FirstOrDefault(p => p.TaskId == newDto.TaskId); // 使用 id筛选



                if (dbInfo != null)
                {
                    dbInfo.Description = newDto.Description;
                    dbInfo.Title = newDto.Title;
                    dbInfo.StarttaskDate = newDto.StarttaskDate;
                    dbInfo.EndtaskDate = newDto.EndtaskDate;
                    dbInfo.Status = newDto.Status;
                    dbInfo.ProjectTitle = newDto.ProjectTitle;

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
        /// 删除项目事项
        /// </summary>
        /// <param name="TaskId">项目事项id</param>
        /// <returns>1:删除成功 -2:ID传错了 -1:删除失败 -99:异常</returns>
        [HttpDelete]
        public IActionResult DelTasks(int TaskId)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                
                var dbInfo = db.Tasks.FirstOrDefault(p => p.TaskId == TaskId); // 使用 id筛选

                if (dbInfo == null)
                {
                    apiResponse.ResultCode = -2;
                    apiResponse.Msg = "请正确传项目事项";

                    return Ok(apiResponse);
                }

                db.Tasks.Remove(dbInfo);
                int result = db.SaveChanges();
                if (result == 1)
                {
                    apiResponse.ResultCode = 1;
                    apiResponse.Msg = "项目事项删除成功";
                }
                else
                {
                    apiResponse.ResultCode = -1;
                    apiResponse.Msg = "项目事项删除失败";
                }
            }
            catch (Exception)
            {
                apiResponse.ResultCode = -99;
                apiResponse.Msg = "服务器忙,请稍后...";
            }

            return Ok(apiResponse);
        }
    }
}
