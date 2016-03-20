using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Pictures
{
    public partial class FieldEdit : DtCms.Web.UI.ManagePage
    {
        public int Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要修改的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                chkLoginLevel("editPictures");
                ShowInfo(this.Id);
            }
        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.PicturesField bll = new DtCms.BLL.PicturesField();
            DtCms.Model.PicturesField model = bll.GetModel(_id);
            txtTitle.Text = model.Title;
            ddlFieldType.SelectedValue = model.FieldType;
            txtFieldRemark.Text = DtCms.Common.Utils.ToTxt(model.FieldRemark);
            cbIsNull.Checked = model.IsNull;
            txtSortId.Text = model.SortId.ToString();
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.PicturesField bll = new DtCms.BLL.PicturesField();
            DtCms.Model.PicturesField model = bll.GetModel(this.Id);

            model.Title = txtTitle.Text.Trim();
            model.FieldType = ddlFieldType.SelectedValue;
            model.FieldRemark = DtCms.Common.Utils.ToHtml(txtFieldRemark.Text);
            model.IsNull = false;
            model.IsNull = cbIsNull.Checked;
            model.SortId = int.Parse(txtSortId.Text.Trim());

            model.Ver = Session["ver"].ToString();
            bll.Update(model);
            //保存日志
            SaveLogs("[图文模块]编辑扩展字段：" + model.Title);
            JscriptPrint("字段编辑成功啦！", "FieldList.aspx", "Success");
        }

    }
}
