using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.TaskModule;

//**********************************************************************
//
// 文件名称(File Name)：CommandCollection.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 13:13:56         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 13:13:56          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
    /// <summary>
    /// 命令集合
    /// </summary>
    public class CommandCollection : IList<CommandDescriptor>
    {
        private List<CommandDescriptor> _commandList = new List<CommandDescriptor>();
       
        /// <summary>
        /// 操作模块集合时的消息事件
        /// </summary>
        public event EventHandler<CommandEventArgs> onCommandOperationMessage;

        public void OnCommandOperationMessage(CommandEventArgs e)
        {
            if(onCommandOperationMessage != null)
            {
                onCommandOperationMessage(this, e);
            }
        }
        public int IndexOf(CommandDescriptor item)
        {
            return _commandList.IndexOf(item);
        }

        public void Insert(int index, CommandDescriptor item)
        {
             _commandList.Insert(index, item);
             PersistenceCommand();
             OnCommandOperationMessage(new CommandEventArgs() { OpreatorType = Helper.OperatorType.Add, Command = item });
        }

        public void RemoveAt(int index)
        {
           CommandDescriptor command= _commandList[index];
            _commandList.RemoveAt(index);
            PersistenceCommand();
            OnCommandOperationMessage(new CommandEventArgs() { OpreatorType = Helper.OperatorType.Delete,Command=command });
        }

        public CommandDescriptor this[int index]
        {
            get { return _commandList[index]; }
            set { _commandList[index] = value; }
        }

        public void Add(CommandDescriptor item)
        {
            _commandList.Add(item);
            PersistenceCommand();
            OnCommandOperationMessage(new CommandEventArgs() { OpreatorType = Helper.OperatorType.Add,Command=item });
        }

        public void Add(CommandDescriptor item, bool isPersistence = true)
        {
            _commandList.Add(item);
            if (isPersistence)
            {
                PersistenceCommand();
            }
            OnCommandOperationMessage(new CommandEventArgs() { OpreatorType = Helper.OperatorType.Add, Command = item });
        }

        public void Clear()
        {
            _commandList.Clear();
            PersistenceCommand();
            OnCommandOperationMessage(new CommandEventArgs() { OpreatorType = Helper.OperatorType.Clear });
        }
        public void Clear( bool isPersistence = true)
        {
            _commandList.Clear();
            if (isPersistence)
            {
                PersistenceCommand();
            }
            OnCommandOperationMessage(new CommandEventArgs() { OpreatorType = Helper.OperatorType.Clear });
        }

        public bool Contains(CommandDescriptor item)
        {
            return _commandList.Contains(item);
        }

        public void CopyTo(CommandDescriptor[] array, int arrayIndex)
        {
            _commandList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _commandList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CommandDescriptor item)
        {
            bool flag = _commandList.Remove(item);
            PersistenceCommand();
            OnCommandOperationMessage(new CommandEventArgs() { OpreatorType = Helper.OperatorType.Delete,Command=item });
            return flag;
        }

        public bool Remove(CommandDescriptor item,bool isPersistence=true)
        {
            bool flag = _commandList.Remove(item);
            if (isPersistence)
            {
                PersistenceCommand();
            }
            OnCommandOperationMessage(new CommandEventArgs() { OpreatorType = Helper.OperatorType.Delete, Command = item });
            return flag;
        }

        public IEnumerator<CommandDescriptor> GetEnumerator()
        {
           return _commandList.GetEnumerator();
            
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region 持久化任务模块
        private void PersistenceCommand()
        {
            try
            {
                #region File
                //string path = AppDomain.CurrentDomain.BaseDirectory;
                //path = Path.Combine(path, "Config\\CommandConfig.txt").ToString();
                ////path = "e://TaskConfig.txt";
                //File.WriteAllText(path, JsonConvert.SerializeObject(_commandList, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }));// 
                #endregion

                #region Mongo
                MongoDbHelper<CommandDescriptor> mongo = new MongoDbHelper<CommandDescriptor>("commandconfig");
                mongo.InsertBatch(_commandList, true);
                #endregion

            }
            catch (Exception ex)
            {

            }

        }
        #endregion
    }
}
