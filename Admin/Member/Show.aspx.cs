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
    public partial class Show : DtCms.Web.UI.ManagePage
    {
        public int Id;
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
                ChannelTreeBind(0, "请选择所属类别...", (int)Channel.Member, this.ddlClassId,ver);
                ShowInfo(this.Id);
            }
        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.Member bll = new DtCms.BLL.Member();
            DtCms.Model.Member model = bll.GetModel(_id);

            txtTitle.Text = model.Title;
            txtAge.Text = model.UserAge.ToString();
            txtTel.Text = model.UserPhone;

            ddlClassId.SelectedValue = model.ClassId.ToString();


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
            model.Content = "";
            model.Content1 = "";
            model.Field1 = "";
            model.Field2 = "";
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
            model.UserAge = int.Parse(txtAge.Text.Trim());
            model.UserBirth = "";
            model.UserCompanyAddr = "";
            model.UserCompanyPhone = "";
            model.UserEmail = "";
            model.UserGrade = "";
            model.UserIntegral = 0;
            model.UserPhone = txtTel.Text.Trim();
            model.UserPwd = "";
            model.UserSex = "";           
            model.Ver = ver;

            bll.Update(model);
            //保存日志
            SaveLogs("[会员模块]编辑会员：" + model.Title);

            JscriptPrint("编辑会员成功啦！", "List.aspx?ClassId=" + model.ClassId, "Success");
        }

    }
}
