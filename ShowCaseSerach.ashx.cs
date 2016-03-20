using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
namespace DtCms.Web
{
    /// <summary>
    /// ShowCaseSerach 的摘要说明
    /// </summary>
    public class ShowCaseSerach : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (string.IsNullOrEmpty(context.Request.QueryString["keyword"]) || context.Request.QueryString["keyword"].Equals("null"))
            {

                return;

            }
            string keyword = DtCms.Common.Utils.DropHTML(context.Request.QueryString["keyword"]);

            int pagenow = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["indexpage"]))
            {

                pagenow = Convert.ToInt32(context.Request.QueryString["indexpage"]);

            }

            StringBuilder bur = new StringBuilder();


            int typeid = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["typeid"]))
            {

                typeid = Convert.ToInt32(context.Request.QueryString["typeid"]);

            }
            int pagesize = 4;


            DataSet ds = new BLL.Product().GetSerachList(keyword, pagesize, pagenow);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    string htmlpath = ds.Tables[0].Rows[i]["htmlpath"] != null ? ds.Tables[0].Rows[i]["htmlpath"].ToString() : "";

                    if (string.IsNullOrEmpty(htmlpath) || htmlpath.IndexOf(".html") == -1)
                    {
                        Model.Channel channer = new BLL.Channel().GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["classid"]));

                        if (channer != null)
                        {

                            if (!channer.WebPath.Equals(""))
                            {

                                htmlpath = "/" + channer.WebPath + "/" + "index" + ds.Tables[0].Rows[i]["id"] + ".html";
                            }
                            else
                            {

                                Model.Channel channer1 = new BLL.Channel().GetModel(channer.ParentId);

                                if (channer1 != null || !channer1.WebPath.Equals(""))
                                {

                                    htmlpath = "/" + channer1.WebPath + "/" + "index" + ds.Tables[0].Rows[i]["id"] + ".html";

                                }

                            }

                        }


                    }

                    bur.AppendFormat("<dl><dt><a href=\"{0}\" title=\"{1}\"><img src=\"{2}\" /></a></dt>", htmlpath, ds.Tables[0].Rows[i]["Title"], ds.Tables[0].Rows[i]["ImgUrl"].ToString().Equals("") ? "/webimages/default.jpg" : ds.Tables[0].Rows[i]["ImgUrl"].ToString());

                    bur.AppendFormat("<dd><h4><a href=\"{0}\" title=\"{1}\">{1}</a></h4><p>{2}</p>", htmlpath, ds.Tables[0].Rows[i]["Title"], DateTime.Parse(ds.Tables[0].Rows[i]["AddTime"].ToString()).ToString("yyyy-MM-dd"));

                    bur.AppendFormat("<p class=\"cur\">{0}</p>", ds.Tables[0].Rows[i]["Daodu"] != null ? (DtCms.Common.Utils.DropHTML(ds.Tables[0].Rows[i]["Daodu"].ToString(), 200)) : "");

                    bur.AppendFormat("<a href=\"{0}\"><img src=\"/webimages/index_73.png\" /></a></dd></dl> ", htmlpath);
        		 
           

                }


            }
            context.Response.Write(bur);

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