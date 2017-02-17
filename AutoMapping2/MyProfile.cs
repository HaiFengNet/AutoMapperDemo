using AutoMapper;
using AutoMapper.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping2
{
    public class UserMappingProfile : Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            //添加配置方法
            // CreateMap<User, UserDto>()
            //     .ForCtorParam("age1", opt => opt.MapFrom(src => src.Age));  //针对构造函数,第一个参数是构造函数的参数名称，第二个是参数选项Action方法。将源类型中的Age属性值给目标构造函数age1赋值。
            AddConditionalObjectMapper().Where((s, d) => d.Name == s.Name + "Dto");  //添加条件对象映射


        }
    }
}
