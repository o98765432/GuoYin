using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
namespace DtCms.Web.Search
{
    /// <summary>
    /// ShowPageCountShow 的摘要说明
    /// </summary>
    public class ShowPageCountShow : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int pagenow = 0;
            string s = context.Request.QueryString["indexpage"];
            if (!string.IsNullOrEmpty(context.Request.QueryString["indexpage"]))
            {
                pagenow = Convert.ToInt32(context.Request.QueryString["indexpage"]);
            }
            int pagecount = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagecount"]))
            {
                pagecount = Convert.ToInt32(context.Request.QueryString["pagecount"]);
            }

            int pagesize = 5;



            int countall = 0;

            if (pagecount > 0)
            {
                countall = pagecount % pagesize == 0 ? pagecount / pagesize : pagecount / pagesize + 1;
            }

            StringBuilder bur = new StringBuilder();

            if (countall > 1)
            {

                bur.Append(returbPageShow(pagecount, pagesize, countall, pagenow, "index"));
            }
            context.Response.Write(bur);
        }


        //分页信息
        public static string returbPageShow(int pagecount, int pagesize, int pageall, int pagenow, string filepath)
        {

            int showleg = 10;

            StringBuilder bur = new StringBuilder();


            int newslen = showleg / 2;

            if (pageall > 1)
            {






                if (pagenow > 0)
                {
                    bur.AppendFormat("<a href=\"javascript:void(0)\" rel=\"{0}\"  class=\"changer\"><img src=\"/images/arrowl.gif\" /></a>", pagenow == 1 ? 0 : (pagenow - 1));

                }
                else
                {
                    bur.AppendFormat("<a href=\"{0}\" rel=\"0\" class=\"changer\"><img src=\"/images/arrowl.gif\" /></a>", "javascript:void");

                }


                int nowpagelist = pagenow / showleg;

                int showstart = 0;

                int showend = pageall;

                if (pageall > showleg)
                {

                    if (pagenow > newslen)
                    {

                        showstart = pagenow - newslen;

                        showend = showstart + showleg;

                        if (showend > pageall)
                        {

                            showstart = showstart - (showend - pageall);
                            showend = pageall;

                        }

                    }
                    else
                    {

                        showend = showleg;
                    }
                }


                for (int i = showstart; i < showend; i++)
                {

                    bur.AppendFormat("<a href=\"javascript:void(0)\" rel=\"{0}\" {1}>{2}</a>", i > 0 ? i : 0, (pagenow == i ? " class=\"num\" " : "class=\"num cur\""), (i + 1));

                }



                if (pageall == pagenow + 1)
                {
                    bur.AppendFormat("<a href=\"{0}\" rel=\"{1}\" class=\"changer\"><img src=\"/images/arrowr.gif\" /></a>", "javascript:void", pageall - 1);
                }
                else
                {
                    bur.AppendFormat("<a href=\"javascript:void(0)\"  rel=\"{0}\" class=\"changer\"><img src=\"/images/arrowr.gif\" /></a>", (pagenow + 1));

                }




            }
            else
            {

                bur.Append("");
            }

            return bur.ToString();

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