using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutoMapping7
{
    //Null值替换
    class Program
    {
        static void Main(string[] args)
        {
            RepliceNull();
        }
        #region Null值替换
        /// <summary>
        /// Null值替换
        /// </summary>
        public static void RepliceNull()
        {
            //映射
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Person, PersonInfo>().ForMember(dest => dest.Title, opt => opt.NullSubstitute("屌丝"));//源属性如果为null，置为“屌丝”
            });
            //执行映射
            var personInfo = Mapper.Map<PersonInfo>(new Person());//源属性未赋值，故为null
            var personInfo2 = Mapper.Map<PersonInfo>(new Person() { Title = "高富帅" });//源属性有值
            //输出结果
            Console.WriteLine("personInfo.Title=" + personInfo.Title);
            Console.WriteLine("personInfo2.Title=" + personInfo2.Title);
            Console.Read();
        }
        #endregion

        #region 开发泛型
        /// <summary>
        /// 开发泛型
        /// </summary>
        public static void OpenGenericity()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap(typeof(Source<>), typeof(Destination<>));
            });
            var src1 = new Source<int> { Value = 22 };
            var dest1 = Mapper.Map<Destination<int>>(src1);
            Console.WriteLine(dest1.Value);

            var src2 = new Source<string> { Value = "Hello World" };
            var dest2 = Mapper.Map<Destination<string>>(src2);
            Console.WriteLine(dest2.Value);

            Console.Read();
        }
        #endregion

        #region 投影
        /// <summary>
        /// 投影
        /// </summary>
        public static void Projection()
        {
            var calender = new CalendarEvent()
            {
                Date = DateTime.Now,
                Title = "历史性时刻"
            };

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CalendarEvent, CalendarEventForm>()
                .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Date.Date))
                .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.Date.Hour))
                .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.Date.Minute));
            });
            //输出映射前的对象
            var calenderEvent = Mapper.Map<CalendarEventForm>(calender);
            Console.WriteLine("calender.Date={0},Title={1}", calender.Date, calender.Title);
            //输出映射后的对象
            foreach (PropertyInfo info in calenderEvent.GetType().GetProperties())
            {
                Console.WriteLine(info.Name + "=" + info.GetValue(calenderEvent));
            }
            Console.Read();
        } 
        #endregion
    }



}
