using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Manage_Advanced
{
    public partial class edit :DtCms.Web.UI. ManagePage
    {
        public int Id;
        protected string strLevel;
        protected int strType;

        Model.Module model = new Model.Module();

        BLL.Module bll = new BLL.Module();
        protected string banben = "", pwd = "";

        protected string ver
        {
            get
            {
                if (_ver == string.Empty)
                {
                    _ver = Session["ver"].ToString();
                }
                return _ver;
            }
        }
        protected string _ver = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            chkLoginLevel("editManage");
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要修改的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                load();
                ShowInfo(this.Id);
            }
        }

        //赋值操作
        private void ShowInfo(int editID)
        {
            DtCms.BLL.Administrator bll = new DtCms.BLL.Administrator();
            DtCms.Model.Administrator model = new DtCms.Model.Administrator();
            model = bll.GetModel(editID);
            txtUserName.Text = model.UserName;
            if (model.IsLock == 1)
            {
                this.rblIsLock.Items[1].Selected = true;
            }
            else
            {
                this.rblIsLock.Items[0].Selected = true;
            }
            //txtUserPwd.Text = model.UserPwd;
            //txtUserPwd1.Text = model.UserPwd;
           // pwd = DtCms.Common.DESEncrypt.Decrypt(model.UserPwd);

            txtReadName.Text = model.ReadName;
            txtUserEmail.Text = model.UserEmail;
            this.strLevel = model.UserLevel;
            this.strType = model.UserType;
            if (model.UserType == 1)
            {
                this.rblUserType.Items[0].Selected = true;
            }
            if (model.UserType == 2)
            {
                this.rblUserType.Items[1].Selected = true;
            }
            if (model.UserType == 3)
            {
                this.rblUserType.Items[2].Selected = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Administrator bll = new DtCms.BLL.Administrator();
            DtCms.Model.Administrator model = bll.GetModel(this.Id);

            string UserPwd = txtUserPwd.Text.Trim();
            string UserLevel = string.Empty;
            int UserType = Convert.ToInt32(rblUserType.SelectedValue);
            if (UserType > 1)
            {
                UserLevel = "," + Request.Form["cbLevel"].Trim() + ",";
            }
            if (UserPwd != null && UserPwd != "")
            {
                model.UserPwd = DtCms.Common.DESEncrypt.Encrypt(UserPwd);
            }
            model.ReadName = txtReadName.Text.Trim();
            model.UserEmail = txtUserEmail.Text.Trim();
            model.UserType = UserType;
            model.IsLock = Convert.ToInt32(rblIsLock.SelectedValue);
            model.UserLevel = UserLevel;

            bll.Update(model);
            //保存日志
            SaveLogs("[管理员管理]编辑管理员：" + model.UserName);
            JscriptPrint("管理员修改成功啦！", "List.aspx", "Success");
        }

        protected void rptModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater cblModule = (Repeater)e.Item.FindControl("RptModuleList");
                //找到分类Repeater关联的数据项 
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                //提取分类ID 
                int typeid = Convert.ToInt32(rowv["id"]);
                //根据分类ID查询该分类下的
                model.ModuleFid = typeid;
                model.Ver = ver;
                DataSet ds = bll.SelectModule_fid(model);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //cblModule.DataSource = ds;
                        //cblModule.DataValueField = "id";
                        //cblModule.DataTextField = "ModuleName";
                        //cblModule.DataBind();

                        cblModule.DataSource = ds;
                        cblModule.DataBind();
                    }
                }
            }
        }


        private void load()
        {



            model.ModuleFid = 0;
            model.Ver = ver;
            DataSet ds = bll.SelectModule_fid(model);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rptModule.DataSource = ds;
                    rptModule.DataBind();
                }
            }

        }
    }
}
