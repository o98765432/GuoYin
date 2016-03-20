using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.ashx
{
    /// <summary>
    /// Yewuliuyan 的摘要说明
    /// </summary>
    public class Yewuliuyan : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string UserName = context.Request.QueryString["UserName"];
            string UserTel = context.Request.QueryString["UserTel"];
            string email = context.Request.QueryString["email"];
            string Address = context.Request.QueryString["Address"];
            string con = context.Request.QueryString["Details"];
            string TypeId = context.Request.QueryString["typeID"];
            string typeName = context.Request.QueryString["typeName"];
            if (UserName != null && UserTel != null && email != null && Address != null && con != null)
            {
                try
                {
                    if (TypeId == "@id@")
                    {
                        TypeId = "4";
                    }
                    if (typeName=="@Title@")
                    {
                        typeName = "航空租赁";
                    }
                    Model.Feedback model = new Model.Feedback();
                    model.Title = "业务咨询";
                    model.UserName = UserName.Trim();
                    model.UserTel = UserTel.Trim();
                    model.mailBox = email.Trim();
                    model.address = Address.Trim();
                    model.Content = con.Trim();
                    model.TypeId = 42;
                    model.Ver = "cn";
                    model.AddTime = DateTime.Now;
                    model.orderNum = TypeId.Trim();
                    model.sex = typeName.Trim();
                    if (new BLL.Feedback().Add(model) > 0)
                        context.Response.Write("提交成功！");
                    else
                        context.Response.Write("提交失败，请联系技术人员");
                }
                catch
                {
                    context.Response.Write("提交失败，请联系技术人员");
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