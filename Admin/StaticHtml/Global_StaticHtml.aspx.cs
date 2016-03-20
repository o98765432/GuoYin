using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.StaticHtml
{
    public partial class Global_StaticHtml : DtCms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStaticHtml();
               
            }
        }

        private void GetStaticHtml() {
            DtCms.Common.HtmlWriter htmlWriter = new HtmlWriter();
            htmlWriter.GengerateRepeat(Server.MapPath("/Templates/default/article.htm"), Server.MapPath("/news.html"));


            JscriptPrint("批量生成成功！", "Global_StaticHtml.aspx", "Success");
        }
    }
}