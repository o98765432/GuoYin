using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

namespace DtCms.Web.Admin.Article
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;

        protected string img = "", indeximg = "",xtimg="";
        protected int drpClassId;
        protected string ver
        {
            get
            {
                if (_ver == string.Empty)
                {
                    _ver = Session["ver"].ToString();
                }
                return _ver;
            }
        }
        protected string _ver = string.Empty;
        protected string classList;
        protected BLL.Article article = new BLL.Article();
        protected string strtitle = "添加";
        protected int classid;
        protected Model.Channel channelmodel = new Model.Channel();
        public string itemImgs = "";
        protected DataSet showda = null;
        protected DataSet shownow = null;

        protected int showkindid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classid"]);


            showda = new BLL.Chenadd().GetList(" typeid=1 and classid=" + classid);

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.Id = Convert.ToInt32(Request.QueryString["id"]);
            }

            if (int.TryParse(Request.QueryString["showkindid"], out showkindid))
            {


            }

            if (!Page.IsPostBack)
            {




                channelmodel = new BLL.Channel().GetModel(classid);

                

                ChannelTreeBind(this.classid, channelmodel.Title, (int)Channel.Article, this.ddlClassId, ver);

                if (Id > 0)
                {
                    strtitle = "修改";
                    ShowInfo(this.Id);
                }
                else
                {


                    this.radNeilians.Checked = true;
                    this.radWailians.Checked = false;
                }



            }


        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.Article bll = new DtCms.BLL.Article();
            DtCms.Model.Article model = bll.GetModel(_id);

            shownow = new BLL.Article().GetModelDa(this.Id);
            txtTitle.Text = model.Title;
            txtAuthor.Text = model.Author;
            txtForm.Text = model.Form;
            txtKeyword.Text = model.Keyword;
            txtZhaiyao.Text = model.Description;
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtImgUrl.Text = model.ImgUrl;
            img = model.ImgUrl;
            txtXtImgUrl.Text = model.ImgUrl5;
            xtimg = model.ImgUrl5;
            

           

            indeximg = model.IndexImgUrl;

            if (img.Trim() == "")
            {
                img = "../Images/no_image.gif";

            }

            if (indeximg.Trim() == "")
            {
                indeximg = "../Images/no_image.gif";
            }

            this.txtBigImgUrl.Text = model.BigImgUrl;
            this.txtSortId.Text = model.SortId.ToString();
            txtDaodu.Text = model.Daodu;
            if (model.ClassId==12||model.ClassId==17||model.ClassId==18||model.ClassId==19)
            {
                this.txtzy.Value = model.Daodu;
            }
            txtContent.Value = model.Content;
            txtClick.Text = model.Click.ToString();
            txtFilepath.Text = model.Filepath;
            this.txtAddTime.Text = model.AddTime.ToString("yyyy-MM-dd");

            this.txtIndexImgUrl.Text = model.IndexImgUrl;


            this.txtSubTitle.Text = model.SubTitle;
            this.txtDownload.Text = model.Download;
            this.txtXtImgUrl.Text = model.ImgUrl5;

            this.showaddinfo.Value = model.Editor;
            if (!string.IsNullOrEmpty(model.Editor))
            {
                string[] aa = model.Editor.Split(',');
                int i = 0;
                foreach (string a in aa)
                {
                    i = i + 1;
                    itemImgs = itemImgs + " <li><img src='" + a.ToString() + "' onmouseover=\"ChangePreview('" + a.ToString() + "');\" /> <a onclick=\"dlstItems_Command(this,'" + i + "','" + a.ToString() + "');\">删除</a> <input name=\"hideFiles1\" class=\"myclassinfo\" type=\"hidden\" value='" + a.ToString() + "'></li>";
                }
            }


            if (model.IsLock == 0)
            {
                this.IsLock.Checked = false;
            }

            if (model.IsTop == 1)
            {
                this.IsTop.Checked = true;
            }

            if (model.IsHot == 1)
            {
                this.IsHot.Checked = true;
            }

            if (model.IsRed == 1)
            {
                IsRed.Checked = true;
            }

            this.wailians.Text = model.Herf + "";




        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Article bll = new DtCms.BLL.Article();
            DtCms.Model.Article model = bll.GetModel(this.Id);


            if (model == null)
            {
                model = new Model.Article();
            }

            model.Title = txtTitle.Text.Trim();
            model.Author = txtAuthor.Text.Trim();
            model.Form = txtForm.Text.Trim();
            model.Keyword = txtKeyword.Text.Trim();
            model.Description = Utils.DropHTML(txtZhaiyao.Text, 250);
            model.Daodu = Utils.DropHTML(txtDaodu.Text, 500);
            if (this.txtzy.Value!=null&&this.txtzy.Value!="")
            {
                model.Daodu = this.txtzy.Value;
            }
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.BigImgUrl = this.txtBigImgUrl.Text.Trim();
            model.Content = txtContent.Value;
            model.Click = int.Parse(txtClick.Text.Trim());
            model.Filepath = txtFilepath.Text.Trim();
            model.AddTime = DateTime.Parse(this.txtAddTime.Text+DateTime.Now.ToString(" HH:mm:ss"));

            model.IndexImgUrl = this.txtIndexImgUrl.Text.Trim();
            model.Editor = this.showaddinfo.Value;

            model.SubTitle = this.txtSubTitle.Text.Trim();
            model.Download = this.txtDownload.Text.Trim();
            GetStringSpell GetSpell = new GetStringSpell();
            model.Spell = Utils.CutString2(GetSpell.GetChineseSpell(txtTitle.Text.Trim()).ToUpper(),1);
            model.ImgUrl5 = this.txtXtImgUrl.Text;

            StringBuilder sb = new StringBuilder();
            string web = webset.WebPath;
            string webpath = webset.WebFilePath;

            BLL.Channel channel = new BLL.Channel();

            sb.AppendFormat("{0}{1}/video/{2}/", web, webpath, classid);
            string path = Server.MapPath(sb.ToString());

            string filename = "";
            string newfile = "";

            if (this.txtVideoDownload.Text.LastIndexOf('/') > -1)
            {
                filename = this.txtVideoDownload.Text.Substring(this.txtVideoDownload.Text.LastIndexOf('/') + 1);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string oldfile = Server.MapPath(this.txtVideoDownload.Text.Trim());
            newfile = path + filename;
            if (File.Exists(oldfile))
            {
                File.Move(oldfile, newfile);
            }
            if (!string.IsNullOrEmpty(model.ImgUrl))
            {

                if (this.classid == 1036)
                {

                    ImageThumbnailMake.MakeThumbnail(model.ImgUrl, "/minimg" + model.ImgUrl, 213, 136, "W");
                }
                else if (this.classid == 1050)
                {
                    ImageThumbnailMake.MakeThumbnail(model.ImgUrl, "/minimg" + model.ImgUrl, 110, 118, "W");
                }
            }


            model.Download = sb.ToString() + filename;

            model.IsMsg = 0;
            model.IsTop = 0;
            model.IsRed = 0;
            model.IsHot = 0;
            model.IsSlide = 0;
            model.IsLock = 0;

            if (this.IsHot.Checked)
            {
                model.IsHot = 1;
            }
            else
            {
                model.IsHot = 0;
            }


            if (this.IsTop.Checked)
            {
                model.IsTop = 1;
            }
            else
            {
                model.IsTop = 0;

            }


            model.IsSlide = 0;

            if (this.IsLock.Checked)
            {
                model.IsLock = 1;
            }
            else
            {
                model.IsLock = 0;
            }

            if(IsRed.Checked)
            {
                model.IsRed = 1;
            }
            else
            {
                model.IsRed = 0;
            }

            model.SortId = int.Parse(this.txtSortId.Text.Trim());

            if (this.radNeilians.Checked == true)//内链存1
            {
                model.HerfFlag = 1;
                model.Herf = this.neilians.Text;
            }
            if (this.radWailians.Checked == true)//外链存0
            {
                model.HerfFlag = 0;

            }
            model.Herf = this.wailians.Text;
            model.Ver = ver;

            SaveLogs("[资讯模块]编辑文章：" + model.Title);

            uploadImage();

            string strnowsql = "";


            if (showda != null && showda.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < showda.Tables[0].Rows.Count; i++)
                {


                    if (!string.IsNullOrEmpty(strnowsql))
                    {
                        strnowsql += " , " + showda.Tables[0].Rows[i]["ziduan"] + " ='" + DtCms.Common.Utils.ToHtml(Request.Form["" + showda.Tables[0].Rows[i]["ziduan"] + ""])+"'";

                    }
                    else
                    {
                        strnowsql = showda.Tables[0].Rows[i]["ziduan"] + " = '" + DtCms.Common.Utils.ToHtml(Request.Form["" + showda.Tables[0].Rows[i]["ziduan"] + ""]) + "'";



                    }


                }
            }


            if (this.Id > 0)
            {
                bll.Update(model);

                if (!string.IsNullOrEmpty(strnowsql)) {

                    bll.UpdateField(Id, strnowsql);
                
                }

                Response.Write("<script type='text/javascript'>alert('修改成功');location.href='list.aspx?classid=" + classid + "&page=" + Request.QueryString["page"] + "'</script>");
                Response.End();
            }
            else
            {
                int nowid = bll.Add(model);
                if (!string.IsNullOrEmpty(strnowsql))
                {

                    bll.UpdateField(nowid, strnowsql);
                }
                Response.Write("<script type='text/javascript'>alert('添加成功');location.href='list.aspx?classid=" + classid + "&page=" + Request.QueryString["page"] + "'</script>");
                Response.End();
            }

        }

        /// <summary>
        /// 上传图片
        /// </summary>
        public void uploadImage()
        {
            string newfile = "";
            string filename = "";
            string path = Server.MapPath("../../UpLoadFiles");

            if (this.txtVideoDownload.Text.LastIndexOf('/') > -1)
            {
                filename = this.txtVideoDownload.Text.Substring(this.txtVideoDownload.Text.LastIndexOf('/') + 1);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string oldfile = Server.MapPath(this.txtVideoDownload.Text.Trim());
            newfile = path + filename;
            if (File.Exists(oldfile))
            {
                File.Move(oldfile, newfile);
            }


        }
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlClassId.SelectedIndex > 0)
            {
                DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
                DtCms.Model.Channel model = new DtCms.Model.Channel();
                model = bll.GetModel(int.Parse(this.ddlClassId.SelectedValue), ver);

                this.txtFilepath.Text = model.Filepath;
                this.litSize.Text = model.Content;
            }
        }

    }
}
