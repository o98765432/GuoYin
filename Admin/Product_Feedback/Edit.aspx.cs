using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Product_Feedback
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        public DtCms.Model.Feedback model = new DtCms.Model.Feedback();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要回复的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
             
                chkLoginLevel("editProductFeedback");
            

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
            DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback();
            model = bll.GetModel(_id);
            txtReContent.Text = DtCms.Common.Utils.ToTxt(model.ReContent);
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            DtCms.BLL.ProductFeedback bll = new DtCms.BLL.ProductFeedback();
            DtCms.Model.ProductFeedback model = bll.GetModel(this.Id);
            model.ReContent = DtCms.Common.Utils.ToHtml(txtReContent.Text);
            model.ReTime = DateTime.Now;
            model.Ver = Session["ver"].ToString();
            bll.Update(model);
            //保存日志
            SaveLogs("[留言管理]回复留言：" + model.Title);
            JscriptPrint("留言回复成功啦！", "List.aspx", "Success");
        }
    }
}
