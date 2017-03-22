using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;
using System.Threading;
using System.IO;

namespace ND.DispatchCenter.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DevExpress.Skins.SkinManager.EnableFormSkins();
                DevExpress.UserSkins.BonusSkins.Register();
                UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");


                Application.ThreadException += Application_ThreadException;////UI线程异常
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;////多线程异常
                Application.ApplicationExit += Application_ApplicationExit;

                bool initiallyOwned = true;
                bool isCreated;
                Mutex m = new Mutex(initiallyOwned, "DispatchCenter", out isCreated);   //Mutex为System.Thread;中的类
                if (!(initiallyOwned && isCreated))
                {
                    MessageBox.Show("抱歉，任务调度中心只能在一台机上运行一个实例！", "提示");
                    Application.Exit();
                }
                else
                {
                    Application.Run(new Form1());
                    //Application.Run(new FrmTest());
                }
            }
            catch(Exception ex)
            {
               // string path = AppDomain.CurrentDomain.BaseDirectory;
              // string pathTemp = Path.Combine(path, "1.txt").ToString();
              //  File.WriteAllText(pathTemp, JsonConvert.SerializeObject(ex));
                MessageBox.Show("启动失败:"+JsonConvert.SerializeObject(ex));
            }
        }

        /// <summary>
        /// 应用程序退出时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
            TaskModuleProvider.Instance.TaskList.PersistenceTaskModule();
            TaskProvider.Instance.TaskList.PersistenceTask();
            }
            catch (Exception ex)
            {
                MessageBox.Show(JsonConvert.SerializeObject(ex));
            }
        }

        /// <summary>
        /// 多线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                TaskModuleProvider.Instance.TaskList.PersistenceTaskModule();
                TaskProvider.Instance.TaskList.PersistenceTask();
                //可以记录日志并转向错误bug窗口友好提示用户
                Exception ex = e.ExceptionObject as Exception;
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(JsonConvert.SerializeObject(ex));
            }
        }
        /// <summary>
        /// UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                TaskModuleProvider.Instance.TaskList.PersistenceTaskModule();
                TaskProvider.Instance.TaskList.PersistenceTask();
                //可以记录日志并转向错误bug窗口友好提示用户
                MessageBox.Show(e.Exception.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(JsonConvert.SerializeObject(ex));
            }

        }
    }
}