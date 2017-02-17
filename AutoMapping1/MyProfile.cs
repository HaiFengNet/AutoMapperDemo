using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping1
{
    public class MyProfile:Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<Source, Dest>();
        }
    }
}
