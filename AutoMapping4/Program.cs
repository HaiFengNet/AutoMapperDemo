using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping4
{
    class Program
    {
        static void Main(string[] args)
        {
            var destObj = UserDefindAnalysisFirst();
            Console.WriteLine("destObj.Total={0}", destObj.Total);
            Console.Read();
        }
        #region 第一个自定义值解析
        /// <summary>
        /// 自定义值解析
        /// </summary>
        /// <returns></returns>
        public static Destination UserDefindAnalysisFirst()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Source, Destination>().ForMember(dest => dest.Total, opt => {
                    opt.ResolveUsing(s =>
                    {
                        var destination = new Destination();
                        destination.Total = s.Value1 + s.Value2;
                        return destination.Total;
                    });
                });
            });
            return Mapper.Map<Destination>(new Source { Value1 = 3, Value2 = 5 });
        }
        #endregion
        #region 第二个自定义值解析
        /// <summary>
        /// 自定义值解析
        /// </summary>
        /// <returns></returns>
        public static Destination UserDefindAnalysisSecound()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Source, Destination>().ForMember(dest => dest.Total, opt => { opt.ResolveUsing<MyValueResolver>(); });
            });
            return Mapper.Map<Destination>(new Source { Value1 = 3, Value2 = 5 });
        } 
        #endregion
    }
    #region 第二个自定义值解析实现IValueResolver (泛型最后一个参数为返回值)
    /// <summary>
    /// 第二个自定义值解析实现IValueResolver (泛型最后一个参数为返回值)
    /// </summary>
    public class MyValueResolver : IValueResolver<Source, Destination, int>
    {
        public int Resolve(Source source, Destination destination, int destMember, ResolutionContext context)
        {
            destination = new Destination();
            destination.Total = source.Value1 + source.Value2;
            return destination.Total;
        }
    } 
    #endregion
}
