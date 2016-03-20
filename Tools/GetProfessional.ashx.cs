using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// GetProfessional 的摘要说明
    /// </summary>
    public class GetProfessional : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DtCms.BLL.Article bll = new BLL.Article();
            string keyword = context.Request.Form["keyword"].Trim();
            string where = "";
            if (keyword == "number")
            {
                where = " IsLock=1 and ver='cn' and ClassId = 46 and (Spell in ('0','1','2','3','4','5','6','7','8','9') or  Title like '%" + keyword + "%')";
            }
            else
            {
                where = " IsLock=1 and ver='cn' and ClassId = 46 and (Spell = '" + keyword + "' or  Title like '%" + keyword + "%')";
            }
            
            if (context.Request.Form["type"] != null)
            {
                int count = bll.GetCount(where);
                context.Response.Write(count % 5 > 0 ? count / 5 + 1 : count / 5);
                return;
            }
            int page = Convert.ToInt32(context.Request.Form["page"]);
            string json = DtCms.Common.JsonTools.ToJson(bll.GetPageList(5, page, where, " SortId asc,AddTime desc"));
            context.Response.Write(json);
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