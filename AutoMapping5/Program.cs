using AutoMapper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace AutoMapping5
{
    //动态对象之家的转换
    class Program
    {
        static void Main(string[] args)
        {

            var orderDto =Flatting();
            Console.WriteLine("orderDto.CustomerName:{0}", orderDto.CustomerName);
            Console.WriteLine("orderDto.Total:{0}", orderDto.Total);
            Console.ReadKey();
        }

        #region Dynamic和ExpandoObject映射
        /// <summary>
        /// Dynamic和ExpandoObject映射
        /// </summary>
        /// <returns></returns>
        public static ExpandoObject DynamicAndExpandoObject()
        {
            Mapper.Initialize(cfg=> { }); //没有需要创建的映射关系，但必须对Mapper做实例化处理
            dynamic dynamicObj = new ExpandoObject();//ExpandoObject对象包含可在运行时动态添加或移除的成员
            dynamicObj.Age = 12;
            dynamicObj.Name = "Feng测试";
            Person person = Mapper.Map<Person>(dynamicObj);
            Console.WriteLine("person.Age={0},Name={1}", person.Age, person.Name);
            dynamic dynamicSecond = Mapper.Map<ExpandoObject>(person);
            dynamicSecond.Address = "北京";
            Console.WriteLine("dynamicObj.Age={0},Name={1},Address={2}", dynamicSecond.Age, dynamicSecond.Name, dynamicSecond.Address);
            return dynamicSecond;
        }
        #endregion

        #region 扁平化
        /// <summary>
        /// 扁平化
        /// </summary>
        public static OrderDto Flatting()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Order, OrderDto>();
            });
           return Mapper.Map<OrderDto>(new Order(){ Customer = new Customer() { Name = "FengTest" }});
            //当使用CreateMap方法在AutoMapper中配置源类型和目标类型时，配置器会尝试将源上的属性和方法匹配到目标的属性上。如果目标属性的任何属性在源类型的属性，方法或者以Get为前缀的方法都不存在，那么AutoMapper会把目标成员的名称（按照PascalCase惯例）分割成独立的单词。
            //在OrderDto类中，Total属性匹配到了Order上的GetTotal方法。CustomerName属性匹配到了Order上的Customer.Name属性。总之，只要合适地命名目标类型属性，我们就不必配置单独的属性匹配。
        }
        #endregion
    }
}
