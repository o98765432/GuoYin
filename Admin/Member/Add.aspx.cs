using System;
using System.IO;
using System.Net;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Member
{
    public partial class Add : DtCms.Web.UI.ManagePage
    {

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
                btnSave.Visible = addflag;
                chkLoginLevel("addMember");
                //绑定类别
                ChannelTreeBind(0, "请选择所属类别...", (int)Channel.Member, this.ddlClassId,ver);
                if (!string.IsNullOrEmpty(Request.Params["classId"]))
                {
                    ddlClassId.SelectedValue = Request.Params["classId"].Trim();
                }
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {



            DtCms.BLL.Member bll = new DtCms.BLL.Member();
            DtCms.Model.Member model = new DtCms.Model.Member();

            model.Title = txtTitle.Text.Trim();
            model.AddTime = DateTime.Now;
            model.ClassId = int.Parse(this.ddlClassId.SelectedValue);
            model.Content = this.txtContent.Value;
            model.Content1 = "";
            model.Field1 = this.txtCongshujigou.Text.Trim();
            model.Field2 = this.txtTrueName.Text.Trim();
            model.Field3 = "";
          
            model.IsLock = 0;
            model.IsRed = 0;
            if (this.cblItem.Items[0].Selected)
            {
                model.IsLock = 1;
            }
            model.Job = "";

            model.UserAddr = "";
            model.UserAge = 0;
            model.UserBirth = "";
            model.UserCompanyAddr = "";
            model.UserCompanyPhone = "";
            model.UserEmail = this.txtEmail.Text.Trim();
            model.UserGrade = "";
            model.UserIntegral = 0;
            model.UserPhone = "";
            model.UserPwd = this.txtUserPwd.Text.Trim();

            if (rbMale.Checked)
            {
                model.UserSex = "男";
            }
            if (rbFemale.Checked)
            {
                model.UserSex = "女";
            }


            model.Ver = ver;

            bll.Add(model);
            //保存日志
            SaveLogs("[会员模块]添加会员：" + model.Title);

            JscriptPrint("增加会员成功啦！", "List.aspx?classId=" + ddlClassId.SelectedValue, "Success");
        }
    }
}
