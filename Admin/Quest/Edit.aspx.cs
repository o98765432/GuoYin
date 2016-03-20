using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Quest
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        public DtCms.Model.Quest model = new DtCms.Model.Quest();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要回复的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {

                if (Id > 0)
                {
                    btnSave.Visible = updateflag;
                    ShowInfo(this.Id);
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
            DtCms.BLL.Quest bll = new DtCms.BLL.Quest();
            model = bll.GetModel(_id);
            txtReContent.Text = model.recontent;
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Quest bll = new DtCms.BLL.Quest();
            DtCms.Model.Quest model = bll.GetModel(this.Id);
            model.recontent = DtCms.Common.Utils.ToHtml(txtReContent.Text);
            model.retime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            bll.UpdateField(model.id, " recontent='" + model.recontent + "' , retime='" + model.retime + "' ,IsLock=1 ");
            //保存日志
             JscriptPrint("留言回复成功啦！", "List.aspx", "Success");
        }
    }
}
