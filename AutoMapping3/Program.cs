using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping3
{
    public class Program
    {
        static void Main(string[] args)
        {            
            var myDestination = UserDefindConvertThird();
            Console.WriteLine("destination.Value1={0}", myDestination.Value1);
            Console.WriteLine("destination.Value2={0}", myDestination.Value2);
            Console.WriteLine("destination.Value3={0}", myDestination.Value3);
            Console.Read();
        }
        #region 第一种方法自定义类型转换
        /// <summary>
        /// 自定义类型转换
        /// </summary>
        /// <returns></returns>
        public static Destination UserDefindConvertFirst()
        {
            Mapper.Initialize(cfg=> {
                cfg.CreateMap<Source, Destination>()
                .ConvertUsing<CustomTypeConverter>();
            });
            return Mapper.Map<Destination>(new Source() { Value1 = "5", Value2 = "05/11/2015", Value3 = typeof(Source).ToString() });
        }
        #endregion
        #region 第二种方法自定义类型转换
        /// <summary>
        ///  第二种自定义类型转换
        /// </summary>
        /// <returns></returns>
        public static Destination UserDefindConvertSecond()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<string, Type>().ConvertUsing<TypeConverter>();
                cfg.CreateMap<Source, Destination>();                
            });
            return Mapper.Map<Destination>(new Source() { Value1 = "5", Value2 = "05/11/2015", Value3 = typeof(Source).ToString() });
        }
        #endregion
        #region 第三种方法自定义类型转换
        /// <summary>
        /// 第三种方法自定义类型转换
        /// </summary>
        /// <returns></returns>
        public static Destination UserDefindConvertThird()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Source, Destination>().ConvertUsing(s =>
                {
                    var d = new Destination();
                    d.Value1 = System.Convert.ToInt32(s.Value1);
                    d.Value2 = System.Convert.ToDateTime(s.Value2);
                    d.Value3 = Type.GetType(s.Value3);
                    return d;
                });
            });
            return Mapper.Map<Destination>(new Source() { Value1 = "5", Value2 = "05/11/2015", Value3 = typeof(Source).ToString() });
        } 
        #endregion
    }
    #region 第一种方法自定义数据转换
    /// <summary>
    /// 自定义数据转换第一种方法
    /// </summary>
    public class CustomTypeConverter : ITypeConverter<Source, Destination>
    {
        public Destination Convert(Source source, Destination destination, ResolutionContext context)
        {
            destination = new Destination();
            destination.Value1 = System.Convert.ToInt32(source.Value1);
            destination.Value2 = System.Convert.ToDateTime(source.Value2);
            destination.Value3 = source.GetType();
            return destination;
        }
    }
    #endregion
    #region 第二种方法自定义数据转换
    /// <summary>
    /// 自定义数据转换第二种方法
    /// </summary>
    public class TypeConverter : ITypeConverter<string, Type>
    {
        public Type Convert(string source, Type destination, ResolutionContext context)
        {
            return Type.GetType(source);
        }
    } 
    #endregion
}
