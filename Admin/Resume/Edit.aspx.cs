using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Resume
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        public DtCms.Model.Resume model = new DtCms.Model.Resume();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要回复的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                chkLoginLevel("editResume");
                if(Id>0)
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
            DtCms.BLL.Resume bll = new DtCms.BLL.Resume();
            model = bll.GetModel(_id);
            txtReContent.Text = DtCms.Common.Utils.ToTxt(model.ReContent);
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Resume bll = new DtCms.BLL.Resume();
            DtCms.Model.Resume model = bll.GetModel(this.Id);
            model.ReContent = DtCms.Common.Utils.ToHtml(txtReContent.Text);
            model.ReTime = DateTime.Now;
            model.IsGood = int.Parse(this.rbItem.SelectedValue);
            bll.Update(model);
            //保存日志
            SaveLogs("[简历管理]回复留言：" + model.Title);
            JscriptPrint("简历回复成功啦！", "List.aspx", "Success");
        }
    }
}
