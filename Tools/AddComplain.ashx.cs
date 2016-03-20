using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// AddComplain 的摘要说明
    /// </summary>
    public class AddComplain : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.QueryString["type"];
            if (!string.IsNullOrEmpty(type))
            {
                add(context);
            }
        }

        public void add(HttpContext context)
        {
            string name = context.Request.Form["name"];
            string sex = context.Request.Form["sex"];
            string tel = context.Request.Form["tel"];
            string address = context.Request.Form["address"];
            string mailBox = context.Request.Form["mailBox"];
            string ordernum = context.Request.Form["ordernum"];
            string title = context.Request.Form["title"];
            string content = context.Request.Form["content"];
           
            int count = new DtCms.BLL.Feedback().Add(new Model.Feedback
            {
                UserName = name,
                sex = sex,
                UserTel = tel,
                UserQQ = address,
                mailBox = mailBox,
                orderNum = ordernum,
                Title = title,
                Content = content,
                Ver = "cn",
                TypeId = 48
            });
            if (count > 0)
            {
                context.Response.Write("提交成功!");
            }
            else
            {
                context.Response.Write("提交失败!");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}