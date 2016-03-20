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
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要修改的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                btnSave.Visible = updateflag;
                chkLoginLevel("editMember");
                //绑定类别
                ChannelTreeBind(0, "请选择所属类别...", (int)Channel.Member, this.ddlClassId,"cn");
                ShowInfo(this.Id);
            }
        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.Member bll = new DtCms.BLL.Member();
            DtCms.Model.Member model = bll.GetModel(_id);

            txtTitle.Text = model.Title;
            ddlClassId.SelectedValue = model.ClassId.ToString();

            this.txtUserPwd.Text = model.UserPwd;

            this.txtTrueName.Text = model.Field2;
            this.txtEmail.Text = model.UserEmail;
            this.txtCongshujigou.Text = model.Field1;

            if (model.UserSex == "男") {
                this.rbMale.Checked = true;
            }
            if (model.UserSex == "女") {
                this.rbFemale.Checked = true; 
            }

            txtContent.Value = model.Content;

            if (model.IsLock == 1) {
                this.cblItem.Items[0].Selected = true;
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
         
            DtCms.BLL.Member bll = new DtCms.BLL.Member();
            DtCms.Model.Member model = bll.GetModel(this.Id);

            model.Title = txtTitle.Text.Trim();
            model.AddTime = DateTime.Now;
            model.ClassId = int.Parse(this.ddlClassId.SelectedValue);
            model.Content = this.txtContent.Value;
            model.Content1 = "";
            model.Field1 = this.txtCongshujigou.Text.Trim();
            model.Field2 = this.txtTrueName.Text.Trim();
            model.Field3 = "";
            model.Id = Id;
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

            if (rbMale.Checked) {
                model.UserSex = "男";
            }
            if (rbFemale.Checked) {
                model.UserSex = "女";
            }

           
            model.Ver = Session["ver"].ToString();

            bll.Update(model);
            //保存日志
            SaveLogs("[会员模块]编辑会员：" + model.Title);

            JscriptPrint("编辑会员成功啦！", "List.aspx", "Success");
        }

    }
}
