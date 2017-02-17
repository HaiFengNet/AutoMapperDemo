using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoMapping1
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalStrainer();
        }

        #region 映射前后操作
        /// <summary>
        /// 映射前后操作
        /// </summary>
        public static void BeforeAfter()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Source, Dest>()
                .BeforeMap((src, dest) => src.Value = src.Value + 1)  //映射前执行
                .AfterMap((src, dest) => src.Name = "FengTest");   //映射后执行
            });
            var mySource = new Source() { Name = "fengSource" };
            var myDest = Mapper.Map<Dest>(mySource);
            Console.WriteLine("SrcName={0}", mySource.name);
            Console.WriteLine("SrcValue={0}", mySource.Value);
            Console.WriteLine("DestName={0}", myDest.name);
            Console.WriteLine("DestValue={0}", myDest.Value);
            Console.Read();
        }
        #endregion

        #region 条件映射
        /// <summary>
        /// 条件映射
        /// </summary>
        public static void ConditionMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Source, Dest>()
                .ForMember(dest => dest.Value, opt => opt.Condition(src => src.Value > 0 && src.Value < 130));
            });
            var mySource = new Source() { Value = 200 };
            var myDest = Mapper.Map<Dest>(mySource);
            Console.WriteLine(myDest.Value);
            Console.Read();
        }
        #endregion

        #region 初始化处理（Profile实例）
        /// <summary>
        /// 初始化处理（Profile实例）
        /// </summary>
        public static void Initialize()
        {
            Mapper.Initialize(cfg => { cfg.AddProfile<MyProfile>(); });
            var mySource = new Source() { Value = 200, Name = "fengSource" };
            var myDest = Mapper.Map<Dest>(mySource);
            Console.WriteLine("SrcName={0}", mySource.name);
            Console.WriteLine("SrcValue={0}", mySource.Value);
            Console.WriteLine("DestName={0}", myDest.name);
            Console.WriteLine("DestValue={0}", myDest.Value);
            Console.Read();
        }
        #endregion

        #region 命名惯例
        /// <summary>
        /// 命名惯例
        /// </summary>
        public static void NameTradition()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();  //下划线命名法
                cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention(); //帕斯卡命名法 
                cfg.CreateMap<Source, Dest>();
            });
            var mySource = new Source() { Value = 200, Name = "fengSource", Feng_Age = 20 };
            var myDest = Mapper.Map<Dest>(mySource);
            Console.WriteLine(myDest.FengAge);
            Console.WriteLine(myDest.name);
            Console.Read();
        }
        #endregion

        #region 替换字符
        /// <summary>
        /// 替换字符
        /// </summary>
        public static void ReplaceWord()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ReplaceMemberName("Tool", "Car");
                cfg.CreateMap<Source, Dest>();
            });
            var mySource = new Source() { Value = 200, Name = "fengSource", Feng_Age = 20, ToolName = "Chrysler" };
            var myDest= Mapper.Map<Dest>(mySource);
            Console.WriteLine(myDest.CarName);
            Console.Read();
        }
        #endregion

        #region 前后缀
        /// <summary>
        /// 前后缀
        /// </summary>
        public static void  PrefixionAndSuffix()
        {
            Mapper.Initialize(cfg => {
                cfg.RecognizePrefixes("P");
                cfg.RecognizePostfixes("L");
                cfg.CreateMap<Source, Dest>();
            });
            var mySource = new Source() { Value = 200, Name = "fengSource", Feng_Age = 20, ToolName = "Chrysler",PGender="男",AddressL="北京"};
            var myDest = Mapper.Map<Dest>(mySource);
            Console.WriteLine(myDest.Gender);
            Console.WriteLine(myDest.Address);
            Console.Read();
        }
        #endregion

        #region 全局过虑
        /// <summary>
        /// 全局过虑
        /// </summary>
        public static void GlobalStrainer()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ShouldMapField = field => false;  //不映射任何字段
                cfg.ShouldMapProperty = pro => pro.GetMethod != null && pro.GetMethod.IsPrivate;
                cfg.CreateMap<Source, Dest>();
            });
            var mySource = new Source() { Value = 200, Name = "fengSource", Feng_Age = 20, ToolName = "Chrysler", PGender = "男", AddressL = "北京" };
            var myDest = Mapper.Map<Dest>(mySource);
            Console.WriteLine(myDest.Value);
            Console.WriteLine(myDest.name);
            Console.Read();

        }
        #endregion

        #region 配置可见性
        /// <summary>
        /// 配置可见性
        /// </summary>
        public static void ConfigVisible()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ShouldMapProperty = pro => pro.GetMethod.IsPublic || pro.GetMethod.IsAssembly;
                cfg.CreateMap<Source, Dest>();
            });
        }
        #endregion
    }
}
