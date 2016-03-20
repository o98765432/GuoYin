using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web.Admin.Banner
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
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
        protected int classid;
        public int kindId;
        protected int drpClassId;
        protected string classList;
        protected BLL.Bannner shipLink = new BLL.Bannner();
        protected string strtitle = "添加";
        protected Model.Channel channelmodel = new Model.Channel();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                this.Id = Convert.ToInt32(Request.QueryString["id"]);
            }
            DataSet ds = shipLink.GetClassListById(this.Id, ver);
            

            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classid"]);

            if(!IsPostBack)
            {
                btnSave.Visible = updateflag; 

                channelmodel = new BLL.Channel().GetModel(classid);

                ChannelTreeBind(this.classid, channelmodel.Title, (int)Channel.Article, this.ddlClassId, ver); 
                if (this.Id > 0)
                {
                    strtitle = "修改";
                    getBannerInfoById();
                    btnSave.Visible = updateflag;
                }
                else 
                {
                    btnSave.Visible = addflag;
                    this.radNeilians.Checked = true;
                    this.radWailians.Checked = false;
                }
               
            }
        }

        public void getBannerInfoById()
        {
            BLL.Bannner bannner = new BLL.Bannner();
            Model.Bannner model = new Model.Bannner();

            model = bannner.GetModel(this.Id);

            if(model != null)
            {
                this.ddlClassId.SelectedValue = model.ClassId.ToString();
                txtBigTitle.Text = model.Title;
                txtBigImgurl.Text = model.Imgurl;
                txtSmallTitle.Text = model.ImgurlSmallTitle;
                this.txtImgUrl2.Text = model.ImgurlSmall;
                txtContent.Text = model.Description;
                if (model.HerfFlag == 1)
                {
                    this.radNeilians.Checked = true;
                    this.radWailians.Checked = false;
                }
                else 
                {
                    this.wailians.Text = model.Herf;
                    this.wailians.Style["display"] = "block";
                    this.radNeilians.Checked = false;
                    this.radWailians.Checked = true;
                 
                
                }

                if (model.IsLock == 0)
                {
                    this.IsLock.Checked = false;
                }

                if (model.IsTop == 1)
                {

                    this.IsTop.Checked = true;

                }
                this.txtContent1.Text = model.content1;

                this.txtContent2.Value = model.content2;

                txtSortId.Text = model.SortId.ToString();
                txtHtmlPath.Text = model.HtmlPaht;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            Model.Bannner modelBanner = new Model.Bannner();
            BLL.Bannner bllBanner = new BLL.Bannner();

            modelBanner.Id = this.Id;
            modelBanner.ClassId = Convert.ToInt32(this.ddlClassId.SelectedValue);
            modelBanner.ImgurlSmallTitle = txtSmallTitle.Text;
            modelBanner.ImgurlSmall = this.txtImgUrl2.Text;
            modelBanner.Title = txtBigTitle.Text;
            modelBanner.Imgurl = txtBigImgurl.Text;

            modelBanner.HtmlPaht = txtHtmlPath.Text;
            modelBanner.AddTime =  DateTime.Now;
            modelBanner.Ver = ver;

            modelBanner.Description = txtContent.Text;
            modelBanner.SortId = Convert.ToInt32(txtSortId.Text);
             
            modelBanner.IsTop = 0;
            modelBanner.IsRed = 0; 
            modelBanner.IsSlide = 0;
            modelBanner.IsLock = 0;
            modelBanner.content1 = this.txtContent1.Text;
            modelBanner.content2 = this.txtContent2.Value;

           

            if (this.IsTop.Checked)
            {
                modelBanner.IsTop = 1;
            }
            else
            {
                modelBanner.IsTop = 0;

            }


            modelBanner.IsSlide = 0;

            if (this.IsLock.Checked)
            {
                modelBanner.IsLock = 1;
            }
            else
            {
                modelBanner.IsLock = 0;
            }
            if (this.radNeilians.Checked == true)//内链存1
            {
                modelBanner.HerfFlag = 1;
                modelBanner.Herf = this.neilians.Text;
            }
            if (this.radWailians.Checked == true)//外链存0
            {
                modelBanner.HerfFlag = 0;
                modelBanner.Herf = this.wailians.Text;
            }

            if (!string.IsNullOrEmpty(modelBanner.Imgurl))
            {

                if (this.classid == 1028)
                {

                    DtCms.Common.ImageThumbnailMake.MakeThumbnail(modelBanner.Imgurl, "/minimg" + modelBanner.Imgurl, 130,95, "W");
                }
                else if (this.classid == 1125) 
                {

                    DtCms.Common.ImageThumbnailMake.MakeThumbnail(modelBanner.Imgurl, "/minimg" + modelBanner.Imgurl, 150 , 110, "W");
                }
                else if (this.classid == 1126)
                {

                    DtCms.Common.ImageThumbnailMake.MakeThumbnail(modelBanner.Imgurl, "/minimg" + modelBanner.Imgurl, 751 , 335, "W");
                }
                else if (this.classid == 87)
                {

                    DtCms.Common.ImageThumbnailMake.MakeThumbnail(modelBanner.Imgurl, "/minimg" + modelBanner.Imgurl, 155, 105, "W");
                }
            }

           



            if (this.Id > 0)
            {
                bllBanner.Update(modelBanner);

              

                Response.Write("<script type='text/javascript'>alert('修改成功');location.href='list.aspx?classid=" + classid + "'</script>");
                Response.End();
            }
            else
            {
                bllBanner.Add(modelBanner);
 
                Response.Write("<script type='text/javascript'>alert('添加成功');location.href='list.aspx?classid=" + classid + "'</script>");
                Response.End();
            }





        }


        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlClassId.SelectedIndex > 0)
            {
                DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
                DtCms.Model.Channel model = new DtCms.Model.Channel();
                model = bll.GetModel(int.Parse(this.ddlClassId.SelectedValue), ver);

                // this.txtFilepath.Text = model.Filepath;

            }

        }
    }
}