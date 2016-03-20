using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Manage_Advanced
{
    public partial class add : DtCms.Web.UI.ManagePage
    {
        Model.Module model = new Model.Module();

        BLL.Module bll = new BLL.Module();
        protected string banben = "";
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
            if (!Page.IsPostBack)
            {
                chkLoginLevel("addManage");
                load();
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.Model.Administrator model = new DtCms.Model.Administrator();
            DtCms.BLL.Administrator bll = new DtCms.BLL.Administrator();
            string userLevel = string.Empty;
            string userName = txtUserName.Text.Trim();
            string userPwd = DtCms.Common.DESEncrypt.Encrypt(txtUserPwd.Text.Trim());
            string readName = txtReadName.Text.Trim();
            string userEmail = txtUserEmail.Text.Trim();
            int userType = Convert.ToInt32(rblUserType.SelectedValue);
            int isLock = Convert.ToInt32(rblIsLock.SelectedValue);
            if (bll.Exists(userName, Session["ver"].ToString()))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误了！</b>用户名已存在，请输入别的管理帐号吧！", "", "Error");
                return;
            }
            if (userType > 1)
            {
                userLevel = "," + Request.Form["cbLevel"].Trim() + ",";
            }

            model.UserName = userName;
            model.UserPwd = userPwd;
            model.ReadName = readName;
            model.UserEmail = userEmail;
            model.UserType = userType;
            model.IsLock = isLock;
            model.UserLevel = userLevel;


            bll.Add(model);
            //保存日志
            SaveLogs("[管理员管理]增加管理员：" + model.UserName);
            JscriptPrint("添加管理员成功啦！", "List.aspx", "Success");
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
