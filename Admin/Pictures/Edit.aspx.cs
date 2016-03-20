using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Pictures
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;

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
        protected BLL.Pictures picture = new BLL.Pictures();
        protected string strtitle = "添加";
        protected int classid;
        protected Model.Channel channelmodel = new Model.Channel();
        protected void Page_Load(object sender, EventArgs e)
        {

            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classid"]);

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.Id = Convert.ToInt32(Request.QueryString["id"]);
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
            DtCms.BLL.Pictures bll = new DtCms.BLL.Pictures();
            DtCms.Model.Pictures model = bll.GetModel(_id);
            

            txtTitle.Text = model.Title;
            txtPrice.Text = model.Price.ToString();
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtContent.Value = model.Content;
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

            if (model.HerfFlag == 1)
            {

                this.radNeilians.Checked = true;
                this.radWailians.Checked = false;
            }
            else
            {
                this.wailians.Text = model.HerfFlag+"";
                this.wailians.Style["display"] = "block";
                this.radNeilians.Checked = false;
                this.radWailians.Checked = true;

            }

            this.wailians.Text = model.Herf + "";

            this.txtListImgUrl.Text = model.ImgUrl;
            this.txtBigImgUrl.Text = model.BigImgUrl;
            
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
              try 
            {
            
            DtCms.BLL.Pictures bll = new DtCms.BLL.Pictures();
            DtCms.Model.Pictures model = bll.GetModel(this.Id);

            if (model == null) 
            
            {
                model = new Model.Pictures();
            }

            model.Title = this.txtTitle.Text.Trim();
            model.Price = decimal.Parse(txtPrice.Text.Trim());
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            //检查图片是否有变
            string hideFiles = Request.Params["hideFiles"];
          

            model.ImgUrl = this.txtListImgUrl.Text.Trim();

            model.Content = txtContent.Value;
            model.Click = int.Parse(txtClick.Text.Trim());
            model.SortId = int.Parse(txtSortId.Text.Trim());
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
            if (this.radNeilians.Checked == true)//内链存1
            {
                model.HerfFlag = 1;
                model.Herf = this.neilians.Text;
            }
            if (this.radWailians.Checked == true)//外链存0
            {
                model.HerfFlag = 0;
                model.Herf = this.wailians.Text;
            }
            model.Herf = this.wailians.Text;
            model.ListImgUrl = "";
            model.BigImgUrl = this.txtBigImgUrl.Text.Trim();
            model.Ver = ver;
 
            model.PicturesExtensions =null;


            //保存日志
            SaveLogs("[图文模块]编辑图文：" + model.Title); 
           
            if (this.Id > 0)
            {
                bll.Update(model);
                Response.Write("<script type='text/javascript'>alert('修改成功');location.href='list.aspx?classid=" + classid + "'</script>");
                Response.End();
            }
            else
            {
                bll.Add(model);
                Response.Write("<script type='text/javascript'>alert('添加成功');location.href='list.aspx?classid=" + classid + "'</script>");
                Response.End();
            }

            }
              catch (Exception ex)
              {
                  throw ex;
              }

        }
    }
}
