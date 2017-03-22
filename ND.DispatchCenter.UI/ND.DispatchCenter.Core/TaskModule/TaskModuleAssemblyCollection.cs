using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskModuleAssemblyCollection.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/1 9:46:29         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/1 9:46:29          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    /// <summary>
    /// 任务模块程序集底下的集合
    /// </summary>
    public class TaskModuleAssemblyCollection : IList<TaskModuleAssemblyDescriptor>
    {
        private List<TaskModuleAssemblyDescriptor> lstDescriptor = new List<TaskModuleAssemblyDescriptor>();
        public int IndexOf(TaskModuleAssemblyDescriptor item)
        {
            return lstDescriptor.IndexOf(item);
        }

        public void Insert(int index, TaskModuleAssemblyDescriptor item)
        {
            lstDescriptor.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            lstDescriptor.RemoveAt(index);
        }

        public TaskModuleAssemblyDescriptor this[int index]
        {
            get { return lstDescriptor[index]; }
            set { lstDescriptor[index] = value; }
        }

        public void Add(TaskModuleAssemblyDescriptor item)
        {
            lstDescriptor.Add(item);
        }

        public void Clear()
        {
            lstDescriptor.Clear();
        }

        public bool Contains(TaskModuleAssemblyDescriptor item)
        {
            return lstDescriptor.Contains(item);
        }

        public void CopyTo(TaskModuleAssemblyDescriptor[] array, int arrayIndex)
        {
            lstDescriptor.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return lstDescriptor.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TaskModuleAssemblyDescriptor item)
        {
            return lstDescriptor.Remove(item);
        }

        public IEnumerator<TaskModuleAssemblyDescriptor> GetEnumerator()
        {
            return lstDescriptor.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
