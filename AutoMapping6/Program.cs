using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoMapping6
{
    class Program
    {
        static void Main(string[] args)
        {
            InheritMember();
        }

        #region List和数组映射处理
        /// <summary>
        /// List和数组映射处理
        /// </summary>
        public static void ListArray()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Source, Destination>();
            });
            var sources = new[] { new Source() { Value = 1 }, new Source() { Value = 2 }, new Source() { Value = 3 }, };
            IEnumerable<Destination> iEnumerableDests = Mapper.Map<IEnumerable<Destination>>(sources);
            ICollection<Destination> iCollectionDests = Mapper.Map<ICollection<Destination>>(sources);
            IList<Destination> iListDests = Mapper.Map<IList<Destination>>(sources);
            List<Destination> listDests = Mapper.Map<List<Destination>>(sources);
            Destination[] destsArr = Mapper.Map<Destination[]>(sources);

            //这里只举两个例子，其他集合同理   
            foreach (var dest in iCollectionDests)
            { Console.Write(dest.Value + ","); }
            Console.WriteLine();
            foreach (var dest in destsArr)
            { Console.Write(dest.Value + ","); }
            Console.Read();

        }
        #endregion

        #region 集合中的多态元素类型
        /// <summary>
        /// 集合中的多态元素类型
        /// </summary>
        public static void ArrayPolymorphism()
        {
            Mapper.Initialize(cfg =>
            {   //在基类中配置继承
                //cfg.CreateMap<ChildSource, ChildDestination>(); //仍然要求显示配置孩子映射，因为它不能“猜出”具体使用哪一个孩子目标映射。
                //cfg.CreateMap<ParentSource, ParentDestination>().Include<ChildSource, ChildDestination>();
                //在派生类中配置继承 
                cfg.CreateMap<ParentSource, ParentDestination>();
                cfg.CreateMap<ChildSource, ChildDestination>().IncludeBase<ParentSource, ParentDestination>();

            });
            var sources = new[] { new ParentSource() { Value1 = 11 }, new ChildSource() { Value2 = 22 }, new ParentSource() };
            var dests = Mapper.Map<ParentDestination[]>(sources);
            Console.WriteLine(dests[0]);
            Console.WriteLine(dests[1]);//ChildDestination
            Console.WriteLine(dests[2]);
            Console.Read();
        }
        #endregion

        #region 继承映射属性
        /// <summary>
        /// 继承映射属性
        /// </summary>
        public static void InheritMember()
        {
            Mapper.Initialize(cfg =>
            {
                // 在父类中配置继承映射
                cfg.CreateMap<Order, OrderDto>().Include<PCOrder, OrderDto>().Include<MobileOrder, OrderDto>()
                 .ForMember(o => o.Referrer, m => m.Ignore());//这里配置了忽略目标属性Referrer的映射  
                cfg.CreateMap<PCOrder, OrderDto>();
                cfg.CreateMap<MobileOrder, OrderDto>();
            });
            // 执行映射
            var order = new PCOrder() { Referrer = "天猫" };
            var mapped = Mapper.Map<OrderDto>(order);
            Console.WriteLine(mapped.Referrer);
            Console.Read();
        }
        #endregion
    }
}
