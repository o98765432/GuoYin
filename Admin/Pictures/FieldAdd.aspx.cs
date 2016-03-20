using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Pictures
{
    public partial class FieldAdd : DtCms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            chkLoginLevel("addPictures");
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.Model.PicturesField model = new DtCms.Model.PicturesField();
            DtCms.BLL.PicturesField bll = new DtCms.BLL.PicturesField();

            model.Title = txtTitle.Text.Trim();
            model.FieldType = ddlFieldType.SelectedValue;
            model.FieldRemark = DtCms.Common.Utils.ToHtml(txtFieldRemark.Text);
            model.IsNull = false;
            model.IsNull = cbIsNull.Checked;
            model.SortId = int.Parse(txtSortId.Text.Trim());
            model.Ver = Session["ver"].ToString();
            bll.Add(model);
            //保存日志
            SaveLogs("[图文模块]增加扩展字段：" + model.Title);
            JscriptPrint("字段增加成功啦！", "FieldAdd.aspx", "Success");
        }
    }
}
