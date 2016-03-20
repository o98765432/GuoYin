using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WEB.manage.Module
{
    public partial class module_add : DtCms.Web.UI.ManagePage
	{
        DtCms.Model.Module model = new DtCms.Model.Module();
        DtCms.BLL.Module bll = new DtCms.BLL.Module();
		public String ver = "cn";
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!Page.IsPostBack)
			{
               
				this.loadModuleLevel1();
                if (Request.QueryString["id"] != null)
                {
                    this.ddlModule.SelectedValue = Request.QueryString["id"];
                }
			}
		}
		private void loadModuleLevel1()
		{
			model.ModuleFid = 0;
			model.Ver = ver;
			DataSet ds = bll.SelectModule_fid(model);
			 if (ds.Tables.Count > 0)
			{
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.DataSource = ds;
                    ddlModule.DataValueField = "id";
                    ddlModule.DataTextField = "ModuleName";
                    ddlModule.DataBind();
                }
			}
			ddlModule.Items.Insert(0, "--选择栏目--");
			ddlModule.Items[0].Value = "0";
			ddlModule.SelectedValue = "0";
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			model.ModuleName = txtTitle.Text;
			model.ModuleHref = txtHref.Text;
            model.Order_id = int.Parse(this.txtAlias.Text);
            model.ModuleAlias = "";

			model.ModuleFid = int.Parse(ddlModule.SelectedValue);
			model.Ver = ver;
			string result = bll.AddModule(model);
            JscriptPrint("编辑模块成功啦！", "module.aspx?id=" + Request.QueryString["id"], "Success");
		}
	}
}
