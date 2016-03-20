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
    public partial class module_update : DtCms.Web.UI.ManagePage
	{
        DtCms.Model.Module model = new DtCms.Model.Module();
        DtCms.BLL.Module bll = new DtCms.BLL.Module();
		public String ver = "cn";
		#region 页面加载方法
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				if(Request.QueryString["id"] != null)
				{
                    lblId.Text = Request.QueryString["id"];
				}
				this.loadModuleLevel1();
				this.load();
			}
		}
		#endregion
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
		}

		#region 加载模块
		protected void load()
		{
			string id = lblId.Text;
			model.Id = int.Parse(id);
			DtCms.Model.Module model_return = bll.SelectModule(model);
			ddlModule.SelectedValue = model_return.ModuleFid.ToString();
			txtTitle.Text = model_return.ModuleName;
			lblId.Text = model_return.Id.ToString();
			txtHref.Text = model_return.ModuleHref.ToString();
            this.txtAlias.Text = model_return.Order_id.ToString();
		}
		#endregion
		#region 修改模块
		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			model.ModuleName = txtTitle.Text;
			model.ModuleHref = txtHref.Text;
            model.Order_id = int.Parse(this.txtAlias.Text);
            model.ModuleAlias = "";
			model.ModuleFid = int.Parse(ddlModule.SelectedValue);
			model.Ver = ver;
			model.Id = int.Parse(lblId.Text);
			string result = bll.UpdateModule(model);
            JscriptPrint("编辑模块成功啦！", "module.aspx?id=" + Request.QueryString["typeid"], "Success");
		}
		#endregion
	}
}
