using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.ArticlePictures
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        protected string img = "", bigimg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要修改的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                chkLoginLevel("editArticle");
                //绑定类别

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
        public DtCms.BLL.ArticlePictures bll = new DtCms.BLL.ArticlePictures();
        public DtCms.Model.ArticlePictures model = new DtCms.Model.ArticlePictures();
        //赋值操作
        private void ShowInfo(int _id)
        {

            model = bll.GetModel(Id);
            txtTitle.Text = model.Title;
            txtImgUrl.Text = model.ImgUrl;
            this.txtBigImgUrl.Text = model.BigImgUrl;
            txtContent.Value = model.Content;
            this.txtAddTime.Text = model.AddTime.ToString("yyyy-MM-dd");

            img = model.ImgUrl;
            bigimg = model.BigImgUrl;
            //if (model.IsMsg == 1)
            //{
            //    cblItem.Items[0].Selected = true;
            //}
            //if (model.IsTop == 1)
            //{
            //    cblItem.Items[1].Selected = true;
            //}
            if (model.IsLock == 1)
            {
                cblItem.Items[0].Selected = true;
            }
           
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            model.Title = txtTitle.Text.Trim();
            model.Id = Id;

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

            //DtCms.BLL.AllReviews bll = new DtCms.BLL.AllReviews();
            //DtCms.Model.AllReviews model = new DtCms.Model.AllReviews();
            //model.KindId = _kindId;
            //model.ParentId = _parentId;
            //model.UserName = _username.Trim();
            //model.Grade = _grade;
            //model.Content = Utils.ToHtml(_content);
            //model.IsLock = webset.IsCheckComment; //评论是否需要审核
            //model.AddTime = DateTime.Now;
            //bll.Add(model);


            model.Ver = Session["ver"].ToString();

            bll.Update(model);
            //保存日志
            SaveLogs("[资讯模块]添加文章：" + model.Title);
            JscriptPrint("文章编辑成功啦！", "List.aspx?parentId=" + Request.QueryString["parentId"] + "&kindId=" + Request.QueryString["kindId"], "Success");
        }

       

    }
}
