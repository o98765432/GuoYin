using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreateHtmlByWeb;

namespace DtCms.Web.Admin.AllHtml
{
    public partial class edit : DtCms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {

                if (!string.IsNullOrEmpty(Request.QueryString["filepath"])) 
                {
                    this.txttitle.Text = Request.QueryString["filepath"];

                    string html = FileManager.ReadFile(Request.QueryString["filepath"]);

                    this.txtContent.Value = html;
                }
            
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txttitle.Text)) 
            {

                FileManager.HtmlWriteFile(this.txttitle.Text, this.txtContent.Value);

                Response.Write("<script type='text/javascript'>alert('恭喜您，修改成功');location.href='list.aspx'</script>");
                Response.End();
            }
        }
    }
}