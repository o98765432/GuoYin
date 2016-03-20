using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.ArticlePictures
{
    public partial class Add : DtCms.Web.UI.ManagePage
    {
        public DtCms.BLL.ArticlePictures bll = new DtCms.BLL.ArticlePictures();
        public DtCms.Model.ArticlePictures model = new DtCms.Model.ArticlePictures();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnSave.Visible = addflag;

                chkLoginLevel("addArticle");
                //绑定类别
            

                this.txtAddTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            model.Title = txtTitle.Text.Trim();


            model.BigImgUrl = this.txtBigImgUrl.Text.Trim();
            model.ImgUrl = MakeThumbnail(this.txtBigImgUrl.Text.Trim(), 73, 73);

            model.KindId = int.Parse(Request.QueryString["kindId"]);
            model.ParentId = int.Parse(Request.QueryString["parentId"]);
            model.SortId = 0;
            model.UserName = "";
            model.Ver = Session["ver"].ToString();
            model.Content = txtContent.Value;
            model.IsLock = 0;

            if (cblItem.Items[0].Selected == true)
            {
                model.IsLock = 1;
            }
            model.Grade = 0;
         
           
            model.AddTime = DateTime.Parse(txtAddTime.Text.Trim());
          
            model.Ver = Session["ver"].ToString();
           
            bll.Add(model);
            //保存日志
            SaveLogs("[资讯模块]添加文章：" + model.Title);
            JscriptPrint("文章发布成功啦！", "List.aspx?parentId="+Request.QueryString["parentId"]+"&kindId="+Request.QueryString["kindId"], "Success");
        }

      

    }
}
