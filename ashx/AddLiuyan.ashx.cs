using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.ashx
{
    /// <summary>
    /// AddLiuyan 的摘要说明
    /// </summary>
    public class AddLiuyan : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string name = context.Request.QueryString["name"];
            string email = context.Request.QueryString["email"];
            string con = context.Request.QueryString["Details"];
            if (name != null && con != null && email != null)
            {
                try
                {
                    Model.Feedback model = new Model.Feedback();
                    model.Title = "在线留言";
                    model.UserName = name.Trim();
                    model.mailBox = email.Trim();
                    model.Content = con.Trim();
                    model.TypeId = 25;
                    model.Ver = "cn";
                    model.AddTime = DateTime.Now;
                    if (new BLL.Feedback().Add(model) > 0)
                        context.Response.Write("留言成功！");
                    else
                        context.Response.Write("留言失败，请联系技术人员");
                }
                catch
                {
                    context.Response.Write("留言失败，请联系技术人员");
                }
            }
            else
                context.Response.Write("请完整填入信息");
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