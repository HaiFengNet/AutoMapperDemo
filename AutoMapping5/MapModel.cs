
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping5
{
    //定义一个Person类
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }


    public class Order
    {
        public Customer Customer { get; set; }
        public decimal GetTotal()
        {
            return 100M;
        }
    }


    public class Customer
    {
        public string Name { get; set; }
    }

    public class OrderDto
    {
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
    }
}
