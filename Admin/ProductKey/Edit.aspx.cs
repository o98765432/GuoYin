using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DtCms.Common;
namespace DtCms.Web.Admin.ProductKey
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        protected string ver;
        protected int classid;
        public int kindId;
        protected int returnclassid;
        protected string classList;
        protected BLL.Productkey shipLink = new BLL.Productkey();
        protected string strtitle = "添加";
        protected Model.Channel channelmodel = new Model.Channel(); 
        protected int shownowid;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                this.Id = Convert.ToInt32(Request.QueryString["id"]);
            }
            
            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classId"]);
            returnclassid = DtCms.Common.Utils.returnIntByString(Request.QueryString["returnclassid"]);
            this.shownowid = Convert.ToInt32(Request.QueryString["shownowid"]);
         
            if (!IsPostBack)
            {
             
                channelmodel = new BLL.Channel().GetModel(classid);


                if (this.Id > 0)
                {
                    strtitle = "修改";
                    getBannerInfoById();
                    btnSave.Visible = updateflag;
                }
                else
                {
                    btnSave.Visible = addflag;
                }

            }
        }

        public void getBannerInfoById()
        {
            BLL.Productkey bannner = new BLL.Productkey();
            Model.Productkey model = new Model.Productkey();

            model = bannner.GetModel(this.Id);

            if (model != null)
            {
                this.txtBigTitle.Text = model.subject;

                this.txtblinfo1.Text = model.blinfo1;

                this.txtContent.Value = model.content;

                this.txtSortId.Text = model.sortid+"";

                this.txtFilePath.Text = model.blinfo5;

               
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            Model.Productkey modelBanner = new Model.Productkey();
            BLL.Productkey bllBanner = new BLL.Productkey();

            modelBanner.id = this.Id;
            modelBanner.classid = this.classid;
            modelBanner.blinfo1 = this.txtblinfo1.Text.Trim();
            modelBanner.subject = txtBigTitle.Text;

            if (!string.IsNullOrEmpty(modelBanner.blinfo1))
            {
                 
                  
                    DtCms.Common.ImageThumbnailMake.MakeThumbnail(Server.MapPath("~/"+modelBanner.blinfo1), Server.MapPath("~/"+"/minimg" + modelBanner.blinfo1), 180, 120, "H");
               
            }


            modelBanner.addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") ;
            modelBanner.ver = Session["ver"].ToString();

            modelBanner.content= txtContent.Value;
            modelBanner.sortid = Convert.ToInt32(txtSortId.Text);

            modelBanner.blinfo2 = "";

            modelBanner.blinfo3 = "";

            modelBanner.blinfo4 = "";

            modelBanner.blinfo5 = this.txtFilePath.Text ;

            modelBanner.blinfo6 = this.shownowid+"";

            if (this.Id > 0)
            {
                bllBanner.Update(modelBanner);
                Response.Write("<script type='text/javascript'>alert('修改成功');location.href='list.aspx?classId=" + classid + "&returnclassid=" + returnclassid + "&shownowid=" + shownowid + "'</script>");
                Response.End();
            }
            else
            {
                bllBanner.Add(modelBanner);
                Response.Write("<script type='text/javascript'>alert('添加成功');location.href='list.aspx?classId=" + classid + "&returnclassid=" + returnclassid + "&shownowid=" + shownowid + "'</script>");
                Response.End();
            }





        }


        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }
    }
}