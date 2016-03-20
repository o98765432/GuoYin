using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Feedback
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        protected int classId;
        public DtCms.Model.Feedback model = new DtCms.Model.Feedback();
        protected DataSet ds = null;
        protected DataSet showda = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (Request.QueryString["classid"]!=null)
            {
                classId = Convert.ToInt32(Request.QueryString["classid"]);
            }
            else
            {
                classId = 0;
            }
            if (!Page.IsPostBack)
            {

                if (Id > 0)
                {
                    ShowInfo(this.Id);
                    btnSave.Visible = updateflag;
                }
                else
                {
                    btnSave.Visible = addflag;
                }

            }
        }

        //赋值操作
        private void ShowInfo(int _id)
        {

            ds = new BLL.Feedback().GetList(" id=" + _id);
            showda = new BLL.Chenadd().GetList(" typeid=6 and classid=" + Request.QueryString["classid"]);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                model.mailBox = ds.Tables[0].Rows[0]["mailBox"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                model.AddTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["AddTime"].ToString());
                model.UserTel = ds.Tables[0].Rows[0]["UserTel"].ToString();
                model.address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.sex = ds.Tables[0].Rows[0]["UserSex"].ToString();
                model.orderNum = ds.Tables[0].Rows[0]["OrderNumber"].ToString();
                txtReContent.Text = DtCms.Common.Utils.ToTxt(ds.Tables[0].Rows[0]["recontent"].ToString());


            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback(); 
            model.ReContent = DtCms.Common.Utils.ToHtml(txtReContent.Text);
            model.ReTime = DateTime.Now;
            model.Id = this.Id;
            model.IsLock = 1;
            bll.Update(model);
            //保存日志
            SaveLogs("[留言管理]回复留言：" + model.Title);
            JscriptPrint("留言回复成功啦！", "List.aspx?classId=" + Request.QueryString["classId"] + "&page=" + Request.QueryString["page"], "Success");
        }
    }
}
