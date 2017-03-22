using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：MongoFileHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/4/26 17:31:03         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/4/26 17:31:03          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.Helper
{
   public class MongoFileHelper
    {
        private static MongoDatabase DB;
        private static string fileTable = "files";
        private static MongoServer mongo = null;
        private static MongoFileHelper _instance { get; set; }
        public static   MongoFileHelper Instance{get{return _instance;}}//默认依赖对象
       static MongoFileHelper()
       {
           _instance = new MongoFileHelper();
       }

       private MongoFileHelper()
       {
           //使用AppSettings方式和配置文件连接，灵活控制MongoDb数据库位置
           string ConnectionString = ConfigurationManager.ConnectionStrings["MongoFileConnStr"].ConnectionString;
           //连接本地的数据库
           //string ConnectionString = "127.0.0.1";
           //连接不成功，提示
           if (String.IsNullOrEmpty(ConnectionString))
           {
               throw new ArgumentNullException("Connection string not found");
           }
           mongo = MongoServer.Create(ConnectionString);
           DB = mongo.GetDatabase("NDFileDB");
       }
       public  bool DeleteFile(string moduleName, string fileName, string rootName = "ND.DispatchCenter")
       {
           try
           {
               //利用GridFS 创建
               MongoGridFSSettings fsSetting = new MongoGridFSSettings() { Root = fileTable };
               MongoGridFS fs = new MongoGridFS(DB, fsSetting);
               MongoGridFSCreateOptions option = new MongoGridFSCreateOptions();
               option.UploadDate = DateTime.Now;
               if (fs.Exists(rootName + "_" + moduleName + "_" + fileName))
               {
                   fs.Delete(rootName + "_" + moduleName + "_" + fileName);
               }
              
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
           finally
           {
               mongo.Disconnect();
           }
       }

       #region 上传文件
       public  bool UploadFile( string moduleName, string fileName, byte[] myData,string rootName = "ND.DispatchCenter")
       {
           try
           {
               //利用GridFS 创建
               MongoGridFSSettings fsSetting = new MongoGridFSSettings() { Root = fileTable };
               MongoGridFS fs = new MongoGridFS(DB, fsSetting);
               MongoGridFSCreateOptions option = new MongoGridFSCreateOptions();
               option.UploadDate = DateTime.Now;
               if (fs.Exists(rootName + "_" + moduleName + "_" + fileName))
               {
                   fs.Delete(rootName + "_" + moduleName + "_" + fileName);
               }
               //创建文件，文件并存储数据
               using (MongoGridFSStream gfs = fs.Create(rootName+"_"+moduleName+"_"+fileName, option))
               {
                   gfs.Write(myData, 0, myData.Length);
                   gfs.Close();
               }
              
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
           finally
           {
               mongo.Disconnect();
           }
       }

       public  bool UploadCommandFile(byte[] myData)
       {
           try
           {
               //利用GridFS 创建
               MongoGridFSSettings fsSetting = new MongoGridFSSettings() { Root = fileTable };
               MongoGridFS fs = new MongoGridFS(DB, fsSetting);
               MongoGridFSCreateOptions option = new MongoGridFSCreateOptions();
               option.UploadDate = DateTime.Now;
               if (fs.Exists("ND.DispatchCenter_Command_ND.DispatchCenter.Command.dll"))
               {
                   fs.Delete("ND.DispatchCenter_Command_ND.DispatchCenter.Command.dll");
               }
               //创建文件，文件并存储数据
               using (MongoGridFSStream gfs = fs.Create("ND.DispatchCenter_Command_ND.DispatchCenter.Command.dll", option))
               {
                   gfs.Write(myData, 0, myData.Length);
                   gfs.Close();
               }
              
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
           finally
           {
               mongo.Disconnect();
           }
       } 
       #endregion


       public  byte[] ReadCommandFile()
       {
           try
           {
               //利用GridFS 创建
               //获取图片名
               MongoGridFSSettings fsSetting = new MongoGridFSSettings() { Root = fileTable };
               //通过文件名去数据库查值
               MongoGridFS fs = new MongoGridFS(DB, fsSetting);
               MongoGridFSFileInfo gfInfo = new MongoGridFSFileInfo(fs, "ND.DispatchCenter_Command_ND.DispatchCenter.Command.dll");
               MongoGridFSStream stream = gfInfo.OpenRead();
               byte[] bytes = new byte[stream.Length];
               stream.Read(bytes, 0, bytes.Length);
              
               return bytes;
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
               mongo.Disconnect();
           }
       }

       public  byte[] ReadFile(string moduleName,string fileName,string rootName = "ND.DispatchCenter")
       {
           try
           {
               //利用GridFS 创建
               //获取图片名
               MongoGridFSSettings fsSetting = new MongoGridFSSettings() { Root = fileTable };
               //通过文件名去数据库查值
               MongoGridFS fs = new MongoGridFS(DB, fsSetting);
               MongoGridFSFileInfo gfInfo = new MongoGridFSFileInfo(fs, rootName + "_" + moduleName + "_" + fileName);
               MongoGridFSStream stream = gfInfo.OpenRead();
               byte[] bytes = new byte[stream.Length];
               stream.Read(bytes, 0, bytes.Length);
               return bytes;
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
               mongo.Disconnect();
           }
       }
     
       public  byte[] ReadFile(string allFileName)
       {
           try
           {
               //利用GridFS 创建
               //获取图片名
               MongoGridFSSettings fsSetting = new MongoGridFSSettings() { Root = fileTable };
               //通过文件名去数据库查值
               MongoGridFS fs = new MongoGridFS(DB, fsSetting);
               MongoGridFSFileInfo gfInfo = new MongoGridFSFileInfo(fs, allFileName);
               MongoGridFSStream stream = gfInfo.OpenRead();
               byte[] bytes = new byte[stream.Length];
               stream.Read(bytes, 0, bytes.Length);
               return bytes;
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
               mongo.Disconnect();
           }
       }

       public  Assembly ReadAssembly(string allFileName)
       {
           try
           {
               //利用GridFS 创建
               //获取图片名
               MongoGridFSSettings fsSetting = new MongoGridFSSettings() { Root = fileTable };
               //通过文件名去数据库查值
               MongoGridFS fs = new MongoGridFS(DB, fsSetting);
               MongoGridFSFileInfo gfInfo = new MongoGridFSFileInfo(fs, allFileName);
               MongoGridFSStream stream = gfInfo.OpenRead();
               byte[] bytes = new byte[stream.Length];
               stream.Read(bytes, 0, bytes.Length);
              return Assembly.Load(bytes);
              
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
               mongo.Disconnect();
           }
       }
     
    }
}
