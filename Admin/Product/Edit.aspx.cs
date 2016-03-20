using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using System.Net;

namespace DtCms.Web.Admin.Product
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        protected int classid;
        protected int drpClassId;
        public string itemImgs = "";
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
        protected BLL.Channel channel = new BLL.Channel();
        protected string strtitle = "添加"; 
        protected Model.Channel channelmodel = new Model.Channel();
        protected string allhtypeid = string.Empty;
        protected DataSet showda = null;
        protected DataSet shownow = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classId"]);
            showda = new BLL.Chenadd().GetList(" typeid=2 and classid=" + classid);

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.Id = Convert.ToInt32(Request.QueryString["id"]);
            }
            DataSet ds =  channel.GetClassListById(Id,ver);

             
            if (!Page.IsPostBack)
            {

                channelmodel = new BLL.Channel().GetModel(classid);

                ChannelTreeBind(this.classid, channelmodel.Title, (int)Channel.Product, this.ddlClassId, ver);

                FieldBind();

                if (Id > 0)
                {
                    strtitle = "修改";

                    ShowInfo(this.Id);



                }
                else
                {

                    DataSet dst = new BLL.Channel().GetChannelListByClassId(classid, ver, " SortId asc,Id desc ");

                    StringBuilder burs = new StringBuilder();

                    if (dst != null && dst.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                        {

                            burs.AppendFormat("<input type=\"checkbox\" class=\"showtypeidinfo\" value=\"{0}\">{1}", dst.Tables[0].Rows[i]["Id"], dst.Tables[0].Rows[i]["Title"]);

                        }
                        allhtypeid = burs.ToString();
                    }

                    this.radNeilians.Checked = true;
                    this.radWailians.Checked = false;
                }

            }
        }

        //绑定扩展字段
        private void FieldBind()
        {
             
        }

        //输出输出扩展字段的CSS
        protected string WriteCss(bool isNull, string fieldType)
        {
            StringBuilder str = new StringBuilder();
            str.Append("input");
            if (isNull == false)
                str.Append(" required");
            if (fieldType.Trim() == "Numeric")
                str.Append(" number");
            return str.ToString();
        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.Product bll = new DtCms.BLL.Product();
            DtCms.Model.Product model = bll.GetModel(_id);
            shownow = new BLL.Product().GetModelDa(this.Id);
            txtTitle.Text = model.Title;
            txtPrice.Text = model.Price.ToString();
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtContent.Value = model.Content;
            txtContent1.Text = model.Content1;
            txtContent2.Value = model.Content2;
            txtContent3.Value = model.Content3;
            txtContent4.Value = model.Content4;

            txtContent6.Value = model.Content6;
            txtContent7.Value = model.Content7;
            txtContent8.Text = model.Content8;
            txtContent9.Value = model.content9;
            txtContent10.Value = model.content10;
            txtContent11.Value = model.content11;
            txtImgUrl1.Text = model.Imgurl1;
            txtImgUrl4.Text = model.ImgUrl4;
            this.txtImgUrl2.Text = model.ImgUrl2;


            txtClick.Text = model.Click.ToString();
            txtSortId.Text = model.SortId.ToString();
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
            this.txtsubTitle.Text = model.subTitle;
            this.txtContent5.Value = model.Content5;
            this.txtFilePath.Text = model.Document;



            DataSet dst = new BLL.Channel().GetChannelListByClassId(classid, ver, " SortId asc,Id desc ");

            StringBuilder burs = new StringBuilder();

            if (dst != null && dst.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                {

                    burs.AppendFormat("<input type=\"checkbox\" class=\"showtypeidinfo\" {2} value=\"{0}\">{1}", dst.Tables[0].Rows[i]["Id"], dst.Tables[0].Rows[i]["Title"], showTitle(model.Document, Convert.ToInt32(dst.Tables[0].Rows[i]["Id"])) ? " checked=\"checked\" " : "");

                }
                allhtypeid = burs.ToString();
            }

            if (model.HerfFlag == 1)
            {

                this.radNeilians.Checked = true;
                this.radWailians.Checked = false;
            }
            else
            {
                this.wailians.Text = model.HerfFlag + "";
                this.wailians.Style["display"] = "block";
                this.radNeilians.Checked = false;
                this.radWailians.Checked = true;

            }
            this.wailians.Text = model.Herf + "";
            //赋值上传的相册
            rptAlbums.DataSource = model.PicturesAlbums;
            rptAlbums.DataBind();


            //itemImgs = model.Software;
            this.showaddinfo.Value = model.Software;
            if (!string.IsNullOrEmpty(model.Software))
            {
                string[] aa = model.Software.Split(',');
                int i=0;
                foreach (string a in aa)
                { 
                    i=i+1;
                    itemImgs = itemImgs + " <li><img src='" + a.ToString() + "' onmouseover=\"ChangePreview('" + a.ToString() + "');\" /> <a onclick=\"dlstItems_Command(this,'" + i + "','" + a.ToString() + "');\">删除</a> <input name=\"hideFiles1\" class=\"myclassinfo\" type=\"hidden\" value='" + a.ToString() + "'></li>";
                }
            }

            //赋值扩展字段
            if (model.PicturesExtensions != null)
            {
                for (int i = 0; i < this.rptField.Items.Count; i++)
                {
                    int hideId = Convert.ToInt32(((HiddenField)this.rptField.Items[i].FindControl("hideFieldId")).Value);
                    foreach (DtCms.Model.ProductExtension emodel in model.PicturesExtensions)
                    {
                        if (hideId == emodel.FieldId)
                        {
                            ((HiddenField)this.rptField.Items[i].FindControl("hideExtensionId")).Value = emodel.Id.ToString();
                            ((TextBox)this.rptField.Items[i].FindControl("txtFieldContent")).Text = emodel.Content;
                        }
                    }
                }
            }
        }


        protected bool showTitle(string alltypeid,int typeid) 
        {
             
            bool flag = false;


            string[] showtypeid = alltypeid.Split(',');

            foreach (string newstypeid in showtypeid)
            {

                if (newstypeid.Equals(typeid + "")) 
                {
                    flag = true;
                    break;
                }

            }

            return flag;

        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Product bll = new DtCms.BLL.Product();
            DtCms.Model.Product model = bll.GetModel(this.Id);

            if (model == null)
            {
                model = new Model.Product();
            }


            model.Title = txtTitle.Text.Trim();
            model.Price = decimal.Parse(txtPrice.Text.Trim());
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            //检查图片是否有变
            string hideFiles = Request.Params["hideFiles"];
            if (!string.IsNullOrEmpty(hideFiles))
            {
                string[] fileArr = hideFiles.Split(',');
                
                if (model.ImgUrl.Substring(model.ImgUrl.LastIndexOf("/") + 1) != fileArr[0].Substring(fileArr[0].LastIndexOf("/") + 1))
                {
                    //删除旧的缩略图
                  //  DeleteFile(model.ImgUrl);
                    //生成缩略图
                 //   model.ImgUrl = MakeThumbnail(fileArr[0]);
                }
            }
            else
            {
                //删除旧的缩略图
                DeleteFile(model.ImgUrl);
                //没有图片
                model.ImgUrl = "";
            }

            string filePath = this.txtFilePath.Text;
 
        
            model.Field7 = "";
            model.Content = txtContent.Value;
            model.Click = int.Parse(txtClick.Text.Trim());
            model.SortId = int.Parse(txtSortId.Text.Trim());
            model.Content1 = txtContent1.Text;
            model.Content2 = txtContent2.Value;
            model.Content3 = txtContent3.Value;
            model.Content4 = txtContent4.Value;

            model.Content6 = txtContent6.Value;
            model.Content7 = txtContent7.Value;
            model.Content8 = txtContent8.Text;
            model.content9 = txtContent9.Value;
            model.content10 = txtContent10.Value;
            model.content11 = txtContent11.Value;
            model.Imgurl1 = this.txtImgUrl1.Text.Trim();
            model.ImgUrl2 = this.txtImgUrl2.Text.Trim();
            model.Software = this.showaddinfo.Value;
            model.ImgUrl4 = this.txtImgUrl4.Text;

            if (!string.IsNullOrEmpty(model.ImgUrl))
            {
                ImageThumbnailMake.MakeThumbnail(model.ImgUrl, "/minimg" + model.ImgUrl, 221, 124, "W");

            }
 

            model.Document = this.txtFilePath.Text;

            model.IsMsg = 0;
            model.IsTop = 0;
            model.IsRed = 0;
            model.IsHot = 0;
            model.IsSlide = 0;
            model.IsLock = 0;
            model.IsMsg = 0;
            model.IsTop = 0;
            model.IsRed = 0;
            model.IsHot = 0;
            model.IsSlide = 0;
            model.IsLock = 0;
            model.IsRed = 0;
            model.Content5 = this.txtContent5.Value;
            model.subTitle = this.txtsubTitle.Text;


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

          

            //保存相册
            if (!string.IsNullOrEmpty(hideFiles))
            {
                List<DtCms.Model.ProductAlbum> als = new List<DtCms.Model.ProductAlbum>();
                string[] fileArr = hideFiles.Split(',');

                foreach (string str in fileArr)
                {
                    als.Add(new DtCms.Model.ProductAlbum { ImgUrl = str });
                }
                model.PicturesAlbums = als;
            }

            
            model.PicturesExtensions = null;
            model.Ver = ver;
            
            //保存日志
            SaveLogs("[产品模块]编辑产品：" + model.Title);

            string strnowsql = "";


            if (showda != null && showda.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < showda.Tables[0].Rows.Count; i++)
                {


                    if (!string.IsNullOrEmpty(strnowsql))
                    {
                        strnowsql += " , " + showda.Tables[0].Rows[i]["ziduan"] + " ='" + DtCms.Common.Utils.ToHtml(Request.Form["" + showda.Tables[0].Rows[i]["ziduan"] + ""]) + "'";

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

                if (!string.IsNullOrEmpty(strnowsql))
                {

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
    }
}
