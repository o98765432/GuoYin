using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace DtCms.Web.ashx
{
    /// <summary>
    /// CodeIsError 的摘要说明
    /// </summary>
    public class CodeIsError : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            string codeNum = context.Request.QueryString["checkcode"];
            //context.Response.Write("100");
            if (context.Session["CheckCode"] != null && !string.IsNullOrEmpty(codeNum) && codeNum.CompareTo(context.Session["CheckCode"]) != 0)
            {
                context.Response.Write("1");
                //Response.Write("<script>alert('对不起，您输入的验证码错误!');window.location='/feedback.html';</script>");
            }
            else
            {
                context.Response.Write("0");
                //.Feedback bll = new BLL.Feedback();

            }
            context.Response.End();
            //context.Response.Write("Hello World");
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