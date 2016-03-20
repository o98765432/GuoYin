using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// AddFeedback 的摘要说明
    /// </summary>
    public class AddFeedback : IHttpHandler,System.Web.SessionState.IRequiresSessionState
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
            string company = context.Request.Form["company"];
            string tel = context.Request.Form["tel"];
            string address = context.Request.Form["address"];
            string content = context.Request.Form["content"];
            string code = context.Request.Form["code"].Trim();
            string checkcode = context.Session["CheckCode"].ToString().Trim();
            if (code == checkcode)
            {
                new DtCms.BLL.Feedback().Add(new Model.Feedback { UserName = name, UserQQ = company, UserTel = tel, 
                    address = address, Content = content, Ver="cn", TypeId =44});
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