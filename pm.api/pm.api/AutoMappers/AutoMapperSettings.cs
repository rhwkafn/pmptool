using AutoMapper;
using pm.api.DataModel;
using pm.api.DTOs;

namespace pm.api.AutoMappers
{
    /// <summary>
    /// model之间转换设置
    /// </summary>
    public class AutoMapperSettings:Profile
    {
        public AutoMapperSettings()
        {
            //登录用户信息
            CreateMap<AccountInfoDTO, AccountInfo>().ReverseMap();

            //项目信息
            CreateMap<ProjectDTO, Project>().ReverseMap();

            //事项信息
            CreateMap<TasksDTO, Task>().ReverseMap();

            //备忘录信息
            CreateMap<MemoDTO, MemoInfo>().ReverseMap();

        }
    }
}
