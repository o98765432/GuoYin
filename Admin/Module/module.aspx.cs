using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using DtCms.Web.UI;


//namespace WEB.manage.Module
//{
//    public partial class module : System.Web.UI.Page
//    {
//        DtCms.Model.Module model = new DtCms.Model.Module();
//        DtCms.BLL.Module bll = new DtCms.BLL.Module();
		

//        string ver = "cn";
		
		
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if(!Page.IsPostBack)
//            {
				
//                btnDelete.Attributes.Add("onclick", "javascrpt:return DelRec()");
//                this.loadModuleLevel1();
//                this.load();


//            }
//        }
//        private void loadModuleLevel1()
//        {
//            model.ModuleFid = 0;
//            model.Ver = ver;
//            DataSet ds = bll.SelectModule_fid(model);
//            if (ds.Tables.Count > 0)
//            {
//                if (ds.Tables[0].Rows.Count > 0)
//                {
//                    ddlModule.DataSource = ds;
//                    ddlModule.DataValueField = "id";
//                    ddlModule.DataTextField = "ModuleName";
//                    ddlModule.DataBind();
//                }
//            }
//            ddlModule.Items.Insert(0, "--选择栏目--");
//            ddlModule.Items[0].Value = "0";
//            ddlModule.SelectedValue = "0";
//        }
//        private void load()
//        {
//            model.ModuleFid = int.Parse(ddlModule.SelectedValue);
//            model.Ver = ver;
//            DataSet ds = bll.SelectModule_fid(model);
//            if(ds.Tables.Count > 0)
//            {
//                if(ds.Tables[0].Rows.Count > 0)
//                {
//                    AboutGv.DataSource = ds;
//                    AboutGv.DataBind();
//                    AboutGv.DataKeyNames = new string[] { "id" };
//                }
//            }

//        }
//        protected void AboutGv_RowDeleting(object sender, GridViewDeleteEventArgs e)
//        {

//        }
//        protected void AboutGv_RowDataBound(object sender, GridViewRowEventArgs e)
//        {
//            if(e.Row.RowIndex != -1)
//            {
//                int indexID = this.AboutGv.PageIndex * this.AboutGv.PageSize + e.Row.RowIndex + 1;
//                e.Row.Cells[0].Text = indexID.ToString();
//            }

//        }
//        protected void AboutGv_RowEditing(object sender, GridViewEditEventArgs e)
//        {
//            string p_id = AboutGv.Rows[e.NewEditIndex].Cells[1].Text.ToString();
//            Session["id"] = p_id;
//            this.Response.Redirect("module_update.aspx");

//        }
//        protected void AboutGv_RowCommand(object sender, GridViewCommandEventArgs e)
//        {
            
//        }

//        protected void AboutGv_RowCreated(object sender, GridViewRowEventArgs e)
//        {
//            //e.Row.Cells[1].Visible = false;
//            //e.Row.Cells[1].Visible = false;


//        }
//        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
//        {

//        }

//        protected void btnDelete_Click(object sender, EventArgs e)
//        {
//            int count = AboutGv.Rows.Count;
//            bool operate = false;
//            for(int x = 0 ; x < count ; x++)
//            {

//                CheckBox cbox = (CheckBox)AboutGv.Rows[x].FindControl("cbox");
//                if(cbox.Checked)
//                {
//                    model.Id = int.Parse(AboutGv.Rows[x].Cells[1].Text.ToString());
//                    String result = bll.DeleteModule(model);
//                    operate = true;

//                }

//            }
//            if(operate)
//            {
//                Response.Write("<script>alert('操作成功')</script>");
//            }
//            else
//            {
//                Response.Write("<script>alert('操作失败')</script>");
//            }
//            Response.Write("<script>window.location='module.aspx'</script>");
//        }

//        protected void AboutGv_PageIndexChanging(object sender, GridViewPageEventArgs e)
//        {
//            AboutGv.PageIndex = e.NewPageIndex;
//            this.load();
//        }

//        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            AboutGv.DataSource = null;
//            AboutGv.DataBind();
//            this.load();
//        }
//        protected void AboutGv_SelectedIndexChanged(object sender, EventArgs e)
//        {

//        }
//    }
//}

namespace DtCms.Web.Admin.Module
{

    public partial class module : DtCms.Web.UI.ManagePage
    {
        DtCms.Model.Module model = new DtCms.Model.Module();
        DtCms.BLL.Module bll = new DtCms.BLL.Module();
        public String ver = "cn";

        public int pcount;                     //总条数
        public int page;                       //当前页
        public int pagesize;                   //设置每页显示的大小

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.LinkPageNum;
            if (!Page.IsPostBack)
            {
                chkLoginLevel("viewLinks");
                this.loadModuleLevel1();
                if (Request.QueryString["id"] != null) {
                    ddlClassId.SelectedValue = Request.QueryString["id"];
                }
                this.load();
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
                    ddlClassId.DataSource = ds;
                    ddlClassId.DataValueField = "id";
                    ddlClassId.DataTextField = "ModuleName";
                    ddlClassId.DataBind();
                }
            }
            ddlClassId.Items.Insert(0, "--选择栏目--");
            ddlClassId.Items[0].Value = "0";
            ddlClassId.SelectedValue = "0";
        }


         private void load()
         {
             model.ModuleFid = int.Parse(this.ddlClassId.SelectedValue);
             model.Ver = ver;
             DataSet ds = bll.SelectModule_fid(model);
             if (ds.Tables.Count > 0)
             {
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     this.rptList.DataSource = ds;
                     this.rptList.DataBind();
                   
                 }
             }

         }

        //审核
        protected void lbtnAudit_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "IsLock=0");
                }
            }
            JscriptPrint("批量审核通过啦！", "module.aspx", "Success");
        }

        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delLinks");
            DtCms.BLL.Module bll = new DtCms.BLL.Module();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    
                    //保存日志
                    //SaveLogs("[链接管理]删除链接：" + bll.get(id).Title);
                    //删除记录
                    model.Id = id;
                    bll.DeleteModule(model);
                }
            }
            JscriptPrint("批量删除成功啦！", "module.aspx", "Success");
        }

        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.rptList.DataSource = null;
            this.rptList.DataBind();
            load();
        }
    }
}
