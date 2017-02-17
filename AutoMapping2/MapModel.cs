using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping2
{
    public class User
    {
        public User()
        {
        }
        public int Age { get; set; }
        public string UserName { get; set; }
        [MapToAttribute("Phone")]
        public string Tel { get; set; }
    }


    public class UserDto
    {
        public UserDto(int age)
        {
            this.age = age;
        }

        private int age;
        public int Age
        {
            get { return age; }
        }
        public string UserName { get; set; }

        public string Phone { get; set; }
    }
}
