using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoMapping6
{
    public class Source { public int Value { get; set; } }
    public class Destination { public int Value { get; set; } }



    public class ParentSource { public int Value1 { get; set; } }
    public class ChildSource : ParentSource { public int Value2 { get; set; } }
    public class ParentDestination { public int Value1 { get; set; } }
    public class ChildDestination : ParentDestination { public int Value2 { get; set; } }



    //领域对象
    public class Order { }
    //电脑端订单
    public class PCOrder : Order{    public string Referrer { get; set; }}
    //手机订单
    public class MobileOrder : Order { }
    //Dtos
    public class OrderDto{    public string Referrer { get; set; }}

}
