//**********************************************************************
//
// 文件名称(File Name)：        SmsEmail.cs
// 功能描述(Description)：      公共工具类
// 作者(Author)：               王进进
// 日期(Create Date)：          2016-01-06 13：10：00

using System;
using System.Configuration;
using System.Xml;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ND.DispatchCenter.CommandClientLib
{
    /// <summary>
    ///发送邮件封装类
    /// </summary>
    internal static class DispatchCenterEmailHelper
    {
       
        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="mailtitle"></param>
        /// <param name="mailcontent"></param>
        /// <param name="MailTo"></param>
        /// <param name="attachment">附件</param>
        /// <returns></returns>



        public static void SendAllMail(string mailtitle, string mailcontent, string mailTo)
        {
            try
            {
                // XmlDocument xmldoc = new XmlDocument();
                // string url = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Common/Config/email.config");
                // xmldoc.Load(url);
                // XmlNode xmlnode = xmldoc.SelectSingleNode("WebSet");
                //Task.Factory.StartNew(() =>
                // {
                string hostname = "smtp.126.com"; //xmlnode.SelectSingleNode("hostname").InnerText;
                string email = "niding@126.com";//xmlnode.SelectSingleNode("email").InnerText;
                string emailname = "你定网管理员";//xmlnode.SelectSingleNode("emailname").InnerText;
                string emailpsw = "QL@1216?niding";//xmlnode.SelectSingleNode("emailpsw").InnerText;
                ////设置发件人信箱,及显示名字
                MailAddress from = new MailAddress(email, emailname);
                //设置收件人信箱,及显示名字
                //MailAddress to = new MailAddress(mailTo, mailTo);
                //创建一个MailMessage对象
                MailMessage oMail = new MailMessage();
                oMail.From = new MailAddress(email, email);
                if (mailTo.Contains(";"))
                {
                    string[] strList = mailTo.Split(';');
                    for (int i = 0; i < strList.Length; i++)
                    {
                        oMail.To.Add(strList[i]);
                    }
                }
                else
                    oMail.To.Add(mailTo);

                oMail.Subject = mailtitle; //邮件标题
                oMail.Body = mailcontent; //邮件内容
                oMail.IsBodyHtml = true; //指定邮件格式,支持HTML格式
                oMail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");//邮件采用的编码
                oMail.Priority = MailPriority.High;//设置邮件的优先级为高

                //发送邮件服务器
                SmtpClient client = new SmtpClient();
                client.Host = hostname; //指定邮件服务器
                client.Credentials = new NetworkCredential(email, emailpsw);//指定服务器邮件,及密码
                //发送
                client.Send(oMail); //发送邮件
                oMail.Dispose(); //释放资源
                //  });


            }
            catch (Exception ex)
            {

            }
        }


    }
}
