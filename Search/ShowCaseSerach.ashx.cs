using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using DtCms.DBUtility;
using DtCms.DAL;
using DtCms.BLL;
namespace DtCms.Web.Search
{
    /// <summary>
    /// ShowCaseSerach 的摘要说明
    /// </summary>
    public class ShowCaseSerach : IHttpHandler
    {

        public Model.Channel channer13 = new Model.Channel();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (string.IsNullOrEmpty(context.Request.QueryString["keyword"]))
            {

                return;

            }

            int pagenow = 1;

            if (!string.IsNullOrEmpty(context.Request.QueryString["indexpage"]))
            {

                pagenow = Convert.ToInt32(context.Request.QueryString["indexpage"]);
            }

            StringBuilder bur = new StringBuilder();


            int pagesize = 10;

            string sql = "select * from dt_Product where title like '%" +
                  context.Request.QueryString["keyword"] +
                  "%' or Content like '%" +
                  context.Request.QueryString["keyword"] + "%' or subTitle like '%" +
                  context.Request.QueryString["keyword"] + "%' and ver='cn' ";

            String sqlinfo = "Select top " + pagesize + " x.*  from ( " + sql + ")  AS x  where x.Id not in(Select top " + (pagesize * pagenow) +
                " x.id from ( " + sql + ")  AS x  Order  by  x.[id]  Desc   )Order  by  x.[id]  Desc   ";

           

            

            DataSet ds = new  BLL.Article().GetSerachList(sqlinfo);
            if ((ds != null && ds.Tables[0].Rows.Count > 0))
            {
               
            
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string htmlpath = ds.Tables[0].Rows[i]["htmlpath"] != null ? ds.Tables[0].Rows[i]["htmlpath"].ToString() : "";
                    if (string.IsNullOrEmpty(htmlpath) || htmlpath.IndexOf(".html") == -1)
                    {
                        Model.Channel channer = new BLL.Channel().GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["classid"]));
                        channer13 = new BLL.Channel().GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["classid"]));
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
                    bur.AppendFormat("<dl><dt><a href=\"" + ds.Tables[0].Rows[i]["htmlpath"] + "\"><img src=\"" + ds.Tables[0].Rows[i]["ImgUrl1"] +
                        "\" width=\"200\" height=\"125\" /></a></dt><dd><h5><a href=\"" + ds.Tables[0].Rows[i]["htmlpath"] + "\">" + ds.Tables[0].Rows[i]["Title"]
                        + "</a></h5> <p class=\"zx_proinfo\">" + ds.Tables[0].Rows[i]["Content"] +
                        " </p><a href=\"" + ds.Tables[0].Rows[i]["htmlpath"] + "\" class=\"btn\">Learn More</a> </dd></dl>");
                }
            }
            else
            {
                bur.AppendFormat("1");
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