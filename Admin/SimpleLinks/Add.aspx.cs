using System;
using System.IO;
using System.Net;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.SimpleLinks
{
    public partial class Add : DtCms.Web.UI.ManagePage
    {
        protected int classid;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.QueryString["classid"].ToString() != "" && Request.QueryString["classid"] != null)
            {
                classid = Convert.ToInt32(Request.QueryString["classid"]);
            }
            if (!Page.IsPostBack)
            {

                btnSave.Visible = addflag;
                chkLoginLevel("addPicturesLink");
                //绑定类别
                ChannelTreeBind(classid, "请选择所属类别...", (int)Channel.PicturesLink, this.ddlClassId, "cn");
                //ChannelTreeBind(0, "请选择所属类别...", (int)Channel.PicturesLink, this.ddlClassId,"cn");
                if (!string.IsNullOrEmpty(Request.Params["classId"]))
                {
                    ddlClassId.SelectedValue = Request.Params["classId"].Trim();
                }
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text.Trim();
           

            DtCms.BLL.PicturesLink bll = new DtCms.BLL.PicturesLink();
            DtCms.Model.PicturesLink model = new DtCms.Model.PicturesLink();
            
            model.Title = txtTitle.Text.Trim();
            model.SubTitle = txtSubTitle.Text.Trim();
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.ImgUrl2 = "";

            model.FilePath = txtFilePath.Text.Trim();
            model.Content = txtContent.Value;
            model.SortId = int.Parse(this.txtSortId.Text);
          
            model.IsMsg = 0;
            model.IsRed = 0;
            model.IsLock = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.IsMsg = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.IsRed = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.IsLock = 1;
            }
            model.Ver = Session["ver"].ToString();
            bll.Add(model);
            //保存日志
            SaveLogs("[图文链接模块]添加图文链接：" + model.Title);

            JscriptPrint("增加图文链接成功啦！", "List.aspx?classId=" + ddlClassId.SelectedValue, "Success");
        }
    }
}
