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
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        protected string img = "";
        protected int drpClassId;
        protected string ver = "cn";
        protected string classList;
        protected BLL.PicturesLink link = new BLL.PicturesLink();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要修改的信息不存在或参数不正确。", "back", "Error");
                return;
            }

            DataSet ds = link.GetClassListById(Id, ver);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    classList = ds.Tables[0].Rows[i]["ClassList"].ToString();
                }

                this.drpClassId = Convert.ToInt32(classList.Substring(1, 3));
            }

            if (!Page.IsPostBack)
            {
                btnSave.Visible = updateflag;
                chkLoginLevel("editPicturesLink");
                //绑定类别
                ChannelTreeBind(this.drpClassId, "请选择所属类别...", (int)Channel.PicturesLink, this.ddlClassId, "cn");
                //ChannelTreeBind(0, "请选择所属类别...", (int)Channel.PicturesLink, this.ddlClassId,"cn");
                ShowInfo(this.Id);
            }
        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.PicturesLink bll = new DtCms.BLL.PicturesLink();
            DtCms.Model.PicturesLink model = bll.GetModel(_id);

            txtTitle.Text = model.Title;
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtImgUrl.Text = model.ImgUrl;
            txtFilePath.Text = model.FilePath;
            txtContent.Value = model.Content;
            txtSubTitle.Text = model.SubTitle;
            txtSortId.Text = model.SortId.ToString();
            img = model.ImgUrl;

            if (model.IsMsg == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.IsRed == 1)
            {
                cblItem.Items[1].Selected = true;
            }
            if (model.IsLock == 1)
            {
                cblItem.Items[2].Selected = true;
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text.Trim();
          

            DtCms.BLL.PicturesLink bll = new DtCms.BLL.PicturesLink();
            DtCms.Model.PicturesLink model = bll.GetModel(this.Id);
           
            model.Title = txtTitle.Text.Trim();
            model.SubTitle = txtSubTitle.Text.Trim();
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.FilePath = txtFilePath.Text.Trim();
            model.Content = txtContent.Value;
            model.ImgUrl2 = "";
           
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
            model.SortId = int.Parse(this.txtSortId.Text.Trim());
            bll.Update(model);
            //保存日志
            SaveLogs("[图文链接模块]编辑图文链接：" + model.Title);

            JscriptPrint("编辑图文链接成功啦！", "List.aspx?classId="+this.ddlClassId.SelectedValue, "Success");
        }

    }
}
