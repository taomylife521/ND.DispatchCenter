using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.LogModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandProvider.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 13:19:18         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 13:19:18          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
    public class CommandProvider
    {
        private static readonly CommandProvider provider;//默认依赖对象
        public static CommandProvider Instance { get { return provider; } }
        private CommandCollection _commandList { get; set; }
        public CommandCollection CommandList { get { return _commandList; } }

        static CommandProvider()
       {
           provider = new CommandProvider();
          // Initialize();
       }

        private CommandProvider()
       {
           _commandList = new CommandCollection();
       }

        public static void Initialize()
        {
          
            string pathTemp = "";
            LogHelper.WriteSystemLog(typeof(CommandProvider), "开始载入命令模块");
            #region File
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            //pathTemp = Path.Combine(path, "Config\\CommandConfig.txt").ToString();
            //string commands = File.ReadAllText(pathTemp);
            //List<CommandDescriptor> tkModules = JsonConvert.DeserializeObject<List<CommandDescriptor>>(commands, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
            #endregion

            #region Mongo
              MongoDbHelper<CommandDescriptor> mongo = new MongoDbHelper<CommandDescriptor>("commandconfig");
               List<CommandDescriptor> tkModules = mongo.FindAll();
            #endregion
               
                tkModules.ForEach(x =>
                {
                    provider.CommandList.Add(x, false);
                });
           
            LogHelper.WriteSystemLog(typeof(CommandProvider), "命令模块初始化完成");
            UIListener.UpdateCommandUI(OperatorType.Reset);
        }

        public static void Refresh()
        {
           
            string path = AppDomain.CurrentDomain.BaseDirectory;
            //扫描当前程序集下的某个dll
            CommandBuilder builder = new CommandBuilder(new CommandBuildOptions("ND.DispatchCenter_Command_ND.DispatchCenter.Command.dll"));
            CommandCollection commandCollection = builder.Build();
            commandCollection.ToList().ForEach(m =>
            {
                m.CreateTime = DateTime.Now;
                m.UpdateTime = DateTime.Now;
                CommandProvider.Instance.CommandList.Add(m);
                UIListener.UpdateCommandUI(OperatorType.Refresh);
            });
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
