using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping2
{
    class Program
    {
        static void Main(string[] args)
        {            
            UserDto userDto = MemberConfiguration();
            Console.WriteLine(userDto.UserName);
            Console.WriteLine(userDto.Phone);
            Console.WriteLine(userDto.Age);
            Console.Read();
        }

        #region 构造函数映射
        /// <summary>
        /// 构造函数映射
        /// </summary>
        public static UserDto Constructor()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDto>().ForCtorParam("age1", user => user.MapFrom(src => src.Age));
            });
           return Mapper.Map<UserDto>(new User() { UserName = "fengTest", Tel = "13261468589", Age = 19 });
        }
        #endregion

        #region 容器(目前无法使用)
        /// <summary>
        /// 容器
        /// </summary>
        public static void Container()
        {
            Mapper.Initialize(cfg =>
            {
            cfg.ConstructServicesUsing(fun => new User());
                cfg.CreateMap<User, UserDto>().ForCtorParam("age1", user => user.MapFrom(src => src.Age));
            });
        }
        #endregion

        #region 条件对象映射
        /// <summary>
        /// 条件对象映射
        /// </summary>
        public static UserDto ConditionObject()
        {
            Mapper.Initialize(cfg => {
                cfg.AddConditionalObjectMapper().Where((s, d) => d.Name == s.Name + "Dto");
            });
            return Mapper.Map<UserDto>(new User() { UserName = "fengTest", Tel = "13261468589", Age = 19 });
        }
        #endregion

        #region AutoMapper 配置
        /// <summary>
        /// AutoMapper 配置
        /// </summary>
        /// <returns></returns>
        public static UserDto MemberConfiguration()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddMemberConfiguration()
                .AddMember<NameSplitMember>()
                .AddName<ReplaceName>(r => r.AddReplace("B", "A"))
                .AddName<PrePostfixName>(p => p.AddStrings(pr => pr.Prefixes, "Get", "get"))
                .AddName<PrePostfixName>(p => p.AddStrings(pr => pr.Postfixes, "Set", "set"))
                .AddName<SourceToDestinationNameMapperAttributesMember>();
                cfg.CreateMap<User, UserDto>();
            });
            return Mapper.Map<UserDto>(new User() { UserName = "fengTest", Tel = "13261468589", Age = 19 });
        } 
        #endregion
    }
}
