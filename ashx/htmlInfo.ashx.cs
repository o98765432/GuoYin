using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;

namespace DtCms.Web.ashx
{
    /// <summary>
    /// htmlInfo 的摘要说明
    /// </summary>
    public class htmlInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (string.IsNullOrEmpty(context.Request.QueryString["showdate"]))
            {

                return;

            }

            if (string.IsNullOrEmpty(context.Request.QueryString["typeid"]))
            {

                return;

            }
            int typeid = DtCms.Common.Utils.returnIntByString(context.Request.QueryString["typeid"]);

            if (typeid == 0)
            {

                return;

            }

            int pagenow = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["indexpage"]))
            {

                pagenow = Convert.ToInt32(context.Request.QueryString["indexpage"]);

            }

            StringBuilder bur = new StringBuilder();


            DataSet ds = new BLL.Article().GetPageList(14, pagenow, " classid in(" + new DtCms.BLL.Channel().returnAllStringTop(typeid, "cn") + ")  and AddTime='" + context.Request.QueryString["showdate"] + "'", "addtime desc,id desc");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    bur.AppendFormat("<dl><dt><a href=\"{0}\" title=\"{1}\"><img src=\"{2}\" /></a></dt>", ds.Tables[0].Rows[i]["htmlpath"], ds.Tables[0].Rows[i]["Title"], ds.Tables[0].Rows[i]["ImgUrl"]);

                    bur.AppendFormat("<dd><h4><a href=\"{0}\" title=\"{1}\">{1}</a></h4><p>{2}</p>", ds.Tables[0].Rows[i]["htmlpath"], ds.Tables[0].Rows[i]["Title"], DateTime.Parse(ds.Tables[0].Rows[i]["AddTime"].ToString()).ToString("yyyy-MM-dd"));

                    bur.AppendFormat("<p class=\"cur\">{0}</p>", ds.Tables[0].Rows[i]["Daodu"] != null ? (DtCms.Common.Utils.DropHTML(ds.Tables[0].Rows[i]["Daodu"].ToString(), 200)) : "");

                    bur.AppendFormat("<a href=\"{0}\"><img src=\"/webimages/index_73.png\" /></a></dd></dl> ", ds.Tables[0].Rows[i]["htmlpath"]);


                }
            }

            context.Response.Write(bur.ToString());


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