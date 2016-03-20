using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace DtCms.Web.ashx
{
    /// <summary>
    /// ShowImg 的摘要说明
    /// </summary>
    public class ShowImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";



            DataSet ds =  new BLL.Channel().GetChannelRptBind(" ParentId in(select id from dt_Channel where ParentId=(select top 1 id from dt_Channel where KindId=1 and ParentId=0 order by Id desc) and kindid=2)");

            if (ds != null) {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {

                    if (!string.IsNullOrEmpty(context.Request.QueryString["typeid"]))
                    {
                        context.Response.Write("<a href=\"/ShowNews/index" + ds.Tables[0].Rows[i]["id"] + ".html\">" + ds.Tables[0].Rows[i]["title"] + "</a><span>|</span>");
                     
                        
                    }
                    else {

                        context.Response.Write("<p><a href=\"/ShowNews/index" + ds.Tables[0].Rows[i]["id"] + ".html\">" + ds.Tables[0].Rows[i]["title"] + "</a></p>");
                    
                    }
                
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