using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CreateHtmlByWeb;
using System.IO;

namespace DtCms.Web.Admin.Config
{
    public partial class admin_config : DtCms.Web.UI.ManagePage
    {
        private DtCms.BLL.WebSet bll = new DtCms.BLL.WebSet();
        protected string webmapinfo = "0";
        protected string showver = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            showver = Session["ver"] != null ? (Session["ver"].ToString().Equals("cn") ? "" : (Session["ver"].ToString()) + "/") : "";

            if (!Page.IsPostBack)
            {
                //  LoadWevSet();

                DtCms.Model.ShowWebSet showwebset = new BLL.ShowWebSet().GetModel(Session["ver"].ToString());

                if (showwebset != null)
                {

                    this.txtWebName.Text = showwebset.seoTitle;

                    this.txtWebTel.Text = showwebset.tel;

                    this.txtWebFax.Text = showwebset.fax;

                    this.txtWebEmail.Text = showwebset.links;

                    this.txtWebCrod.Text = showwebset.bhao;

                    this.txtWebKeywords.Text = showwebset.seoKey;

                    this.txtWebDescription.Text = showwebset.seoDes;

                    this.txtWebCopyright.SelectedValue = showwebset.htmlcontent;

                    this.txtver.Value = showwebset.Ver;

                    this.txtid.Value = showwebset.id + "";

                    webmapinfo = showwebset.htmlcontent;



                    this.txtWebUrl.Text = showwebset.other1;
                }
                else
                {

                    this.txtver.Value = Session["ver"].ToString();

                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            Model.ShowWebSet showwebset = new Model.ShowWebSet();


            showwebset.seoTitle = this.txtWebName.Text;

            showwebset.tel = this.txtWebTel.Text;

            showwebset.fax = this.txtWebFax.Text;

            showwebset.links = this.txtWebEmail.Text;

            showwebset.bhao = this.txtWebCrod.Text;

            showwebset.seoKey = this.txtWebKeywords.Text;

            showwebset.seoDes = this.txtWebDescription.Text;

            showwebset.htmlcontent = this.txtWebCopyright.SelectedValue;

            showwebset.Ver = this.txtver.Value;

            showwebset.id = DtCms.Common.Utils.returnIntByString(this.txtid.Value);

            showwebset.other1 = this.txtWebUrl.Text;

            showwebset.other2 = "";

            showwebset.other3 = "";

            if (showwebset.id > 0)
            {

                new BLL.ShowWebSet().Update(showwebset);

            }
            else
            {

                new BLL.ShowWebSet().Add(showwebset);

            }

            if (!showwebset.htmlcontent.Equals("0"))
            {

                string tempUrl = "/Admin/WebTemplate/Map/index" + showwebset.htmlcontent + ".html";

                string html = FileManager.ReadFile(HttpContext.Current.Server.MapPath(tempUrl));
                 

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/admin/Template/" + showver + "WebMap")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/admin/Template/" + showver + "WebMap"));
                }

                FileManager.HtmlWriteFile(HttpContext.Current.Server.MapPath("~/admin/Template/" + showver + "WebMap/index.html"), html);

            }
            JscriptPrint("系统设置成功啦！", "admin_config.aspx", "Success");

        }
    }
}
