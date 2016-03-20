using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web
{
    /// <summary>
    /// ShowclickPro 的摘要说明
    /// </summary>
    public class ShowclickPro : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (!string.IsNullOrEmpty(context.Request.QueryString["id"]))
            {

                Model.Product model = new BLL.Product().GetModel(Common.Utils.returnIntByString(context.Request.QueryString["id"]));

                if (model != null)
                {

                    new BLL.Product().UpdateField(model.Id, "Click=Click+1");

                    

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