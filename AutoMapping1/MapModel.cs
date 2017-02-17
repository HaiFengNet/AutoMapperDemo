using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping1
{
    public class Source
    {
        public int Value { get; set; }
        public string name;
        public string Name {  private get { return name; } set { name = value; } }
        public int Feng_Age;
        public string ToolName;
        public string PGender;
        public string AddressL;
    }

    public class Dest
    {
        public int Value { get; set; }
        public string name { get; set; }
        public string Name { private get { return name; } set { name = value; } }
       
        public int FengAge;
        public string CarName;
        public string Gender;
        public string Address;
    }
}
