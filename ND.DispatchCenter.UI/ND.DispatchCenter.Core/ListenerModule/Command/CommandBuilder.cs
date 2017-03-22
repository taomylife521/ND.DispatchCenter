using ND.DispatchCenter.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandBuilder.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 16:31:15         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 16:31:15          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
   public class CommandBuilder:ICommandBuilder
    {
       private readonly CommandBuildOptions _options;
       public CommandBuilder()
           : this(new CommandBuildOptions())
        {

        }
       public CommandBuilder(CommandBuildOptions options)
        {
            _options = options;
            
        } 
        public CommandCollection Build()
        {
            CommandCollection commands = new CommandCollection();
            CommandBuildOptions options = _options;
            Type[] allTypes = options.CommandTypeScaner.ScanAll();
            AddTypeWithInterfaces(commands, allTypes);

           

            return commands;
        }

        #region AddTypeWithInterfaces
        private void AddTypeWithInterfaces(CommandCollection tasks, Type[] implementationTypes)
        {
            foreach (Type implementationType in implementationTypes)
            {
                
                if (implementationType.IsAbstract || implementationType.IsInterface)
                {
                    continue;
                }
                ICommand task = Activator.CreateInstance(implementationType) as ICommand;
                tasks.Add(new CommandDescriptor(Guid.NewGuid().ToString(), implementationType.Name.Replace("Command","").ToLower(),task.Port, implementationType.Assembly.FullName, implementationType.Name,task));// System.Guid.NewGuid().ToString()
            }
        }
        #endregion
    }
}
