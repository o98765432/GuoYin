using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web
{
    /// <summary>
    /// Showclick 的摘要说明
    /// </summary>
    public class Showclick : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (!string.IsNullOrEmpty(context.Request.QueryString["id"])) 
            {

                Model.Article model = new BLL.Article().GetModel(Common.Utils.returnIntByString(context.Request.QueryString["id"]));

                if (model != null)
                {

                    new BLL.Article().UpdateField(model.Id, "Click=Click+1");

                    context.Response.Write((model.Click+1)+"");

                }
            
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