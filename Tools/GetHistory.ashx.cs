using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// GetHistory 的摘要说明
    /// </summary>
    public class GetHistory : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request.QueryString["type"];
            if (type != null && type.Equals("list"))
            {
                GetCookies(context);
            }
            string id = context.Request.QueryString["id"];
            if(id != null)
            {
                InsertCookies(context);
            }

        }

        /// <summary>
        /// 存取Cookies的数据
        /// </summary>
        /// <param name="context"></param>
        public void InsertCookies(HttpContext context)
        {
            if (context.Request.Cookies["info"] == null)
            {
                HttpCookie cookies = new HttpCookie("info");
                string strCookies = context.Request.QueryString["id"] + ",";
                cookies.Value = strCookies;
                context.Response.AppendCookie(cookies);
            }
            else if (!string.IsNullOrEmpty(context.Request.QueryString["id"]))
            {
                HttpCookie cookies = context.Request.Cookies["info"];
                string strCookies = cookies.Value + context.Request.QueryString["id"] + ",";
                cookies.Value = strCookies;
                context.Response.AppendCookie(cookies);
            }

        }
        /// <summary>
        /// 读取Cookies的数据
        /// </summary>
        /// <param name="context"></param>
        public void GetCookies(HttpContext context)
        {
            HttpCookie cookies = context.Request.Cookies["info"];
            if (cookies != null)
            {
                string strSql = "  id in (" + cookies.Value + "0)";
                BLL.Article bll = new BLL.Article();
                DataTable dt = bll.GetList(2, strSql, "SortId asc,AddTime desc").Tables[0];
                string josn = "[";
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        josn += "{" + "\"CookiesTitle\":\"" +DtCms.Common.Utils.CutString(dt.Rows[i]["Title"].ToString().Replace("\"", "\\\""),28)+ "\" ," + "\"CookiesImgUrl\": \"" + dt.Rows[i]["ImgUrl"].ToString().Replace("\"", "\\\"") + "\"," + "\"CookiesHref\": \"" + dt.Rows[i]["htmlpath"].ToString().Replace("\"", "\\\"") + "\"},";
                    }
                    josn = josn.Substring(0, josn.Length - 1);
                }

                josn += "]";
                context.Response.Write(josn);
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