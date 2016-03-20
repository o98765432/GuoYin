using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace DtCms.Web.ashx
{
    /// <summary>
    /// Pagehtml 的摘要说明
    /// </summary>
    public class Pagehtml : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int pagenow = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["indexpage"]))
            {
                pagenow = Convert.ToInt32(context.Request.QueryString["indexpage"]);
            }

            int year = DtCms.Common.Utils.returnIntByString(context.Request.QueryString["year"]);

            if (year == 0)
            {

                return;

            }


            int pagecount = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagecount"]))
            {
                pagecount = Convert.ToInt32(context.Request.QueryString["pagecount"]);
            }

            int pagesize = 14;
 
            int countall = 0;

            if (pagecount > 0)
            {
                countall = pagecount % pagesize == 0 ? pagecount / pagesize : (pagecount / pagesize + 1);
            }

            StringBuilder bur = new StringBuilder();

            if (countall > 1)
            {

                if (pagenow > 0)
                {

                    bur.AppendFormat("<a href=\"javascript:;\" class=\"changel\" tel=\"" + year + "\" rel=\"{0}\"><img src=\"/webimages/arrowl.gif\" /></a>", pagenow - 1);

                }
                else
                {
                    bur.AppendFormat("<img src=\"/webimages/arrowl.gif\" />");

                }

                int startlen = 0;

                int endlen = countall;


                if (countall > 10)
                {

                    if (pagenow > 5)
                    {

                        startlen = pagenow - 5;

                    }
                    if (countall - 5 < pagenow && pagenow < 10)
                    {

                        startlen -= (5 - (countall - 5));

                    }

                    if (countall - 5 > pagenow)
                    {

                        endlen = pagenow + 5;

                    }
                    else
                    {

                        endlen = countall;

                    }

                    if (pagenow < 5)
                    {
                        startlen -= (5 + (countall - pagenow));
                        endlen = pagenow + 5 + (5 - pagenow);

                    }



                }

                for (int i = startlen; i < endlen; i++)
                {

                    bur.AppendFormat("<a href=\"javascript:;\" class=\"num {1}\" tel=\"" + year + "\" rel=\"{0}\">{2}</a>", i, pagenow == i ? "cur" : "", i + 1);

                }
                if (pagenow < countall - 1)
                {

                    bur.AppendFormat("<a href=\"javascript:;\" class=\"changer\" tel=\"" + year + "\" rel=\"{0}\"><img src=\"/webimages/arrowr.gif\" /></a>", pagenow + 1);

                }
                else
                {
                    bur.AppendFormat("<img src=\"/webimages/arrowr.gif\" />");

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