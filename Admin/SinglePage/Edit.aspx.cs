using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web.Admin.SinglePage
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        protected int classid;
        protected int id;
        protected int drpClassId;
        protected string classList;
        protected BLL.SinglePage singel = new BLL.SinglePage();
        protected string strtitle = "添加";
        protected Model.Channel channelmodel = new Model.Channel();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.id = Convert.ToInt32(Request.QueryString["id"]);
            }

            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classid"]);


            channelmodel = new BLL.Channel().GetModel(classid);

            if (!IsPostBack)
            {

                //绑定类别
                ChannelTreeBind(classid, channelmodel.Title, (int)Channel.SinglePage, this.ddlClassId, "cn");

                if (id > 0)
                {
                    strtitle = "修改";
                    getSingelpageInfoById(id);
                    btnSave.Visible = updateflag;
                }
                else
                {


                    Model.SinglePage nowmodel = null;

                    if (channelmodel.PageType.Equals("13"))
                    {

                        nowmodel = new BLL.SinglePage().GetModelByClassId(classid, Session["ver"].ToString());
                    }
                    if (nowmodel != null && nowmodel.Id > 0)
                    {
                        strtitle = "修改";
                        getSingelpageInfoById(nowmodel.Id);

                        id = nowmodel.Id;
                        btnSave.Visible = updateflag;

                    }
                    else
                    {

                        this.radNeilians.Checked = true;
                        this.radWailians.Checked = false;
                        btnSave.Visible = addflag;

                    }


                }
            }
        }


        /// <summary>
        /// 根据id获得该条id下的单页面信息
        /// </summary>
        public void getSingelpageInfoById(int sid)
        {
            try
            {

                BLL.SinglePage bllSingle = new BLL.SinglePage();
                Model.SinglePage modelSingle = new Model.SinglePage();

                modelSingle = bllSingle.GetModel(sid);

                if (modelSingle != null)
                {

                    txtTitle.Text = modelSingle.Title;
                    this.ddlClassId.SelectedValue = modelSingle.ClassId.ToString();

                    txtSortId.Text = modelSingle.SortId.ToString();

                    txtImgUrl.Text = modelSingle.ImgUrl;
                    txtImgUrl1.Value = modelSingle.ImgUrlSmall;

                    txtContent.Value = modelSingle.Description;
                    txtFilepath.Text = modelSingle.HtmlPath;

                    txtSeoDescription.Text = modelSingle.SeoDescription;
                    txtSeoTitle.Text = modelSingle.SeoTitle;
                    txtSeoKeyword.Text = modelSingle.SeoKeyWord;


                    this.txtid.Value = modelSingle.Id + "";

                    if (modelSingle.HrefFalg == 1)
                    {

                        this.radNeilians.Checked = true;
                        this.radWailians.Checked = false;
                    }
                    else
                    {
                        this.wailians.Text = modelSingle.Href;
                        this.wailians.Style["display"] = "block";
                        this.radNeilians.Checked = false;
                        this.radWailians.Checked = true;

                    }

                    if (modelSingle.IsLock == 0)
                    {
                        this.IsLock.Checked = false;
                    }

                    if (modelSingle.IsTop == 1)
                    {

                        this.IsTop.Checked = true;

                    }

                    if (modelSingle.IsHot == 1)
                    {
                        this.IsHot.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

                BLL.SinglePage bllSingle = new BLL.SinglePage();

                Model.SinglePage modelSingle = new Model.SinglePage();

                modelSingle.Id = DtCms.Common.Utils.returnIntByString(this.txtid.Value);
                modelSingle.Ver = Session["ver"].ToString();
                modelSingle.Title = txtTitle.Text;
                modelSingle.ClassId = DtCms.Common.Utils.returnIntByString(this.ddlClassId.SelectedValue);

                modelSingle.SortId = Convert.ToInt32(txtSortId.Text);
                modelSingle.ImgUrl = txtImgUrl.Text;
                modelSingle.ImgUrlSmall = txtImgUrl1.Value;

                modelSingle.Description = txtContent.Value;
                modelSingle.HtmlPath = txtFilepath.Text;

                if (this.radNeilians.Checked == true)//内链存1
                {
                    modelSingle.HrefFalg = 1;
                    modelSingle.Href = this.neilians.Text;
                }
                if (this.radWailians.Checked == true)//外链存0
                {
                    modelSingle.HrefFalg = 0;
                    modelSingle.Href = this.wailians.Text;
                }

                modelSingle.SeoTitle = txtSeoTitle.Text;
                modelSingle.SeoKeyWord = txtSeoKeyword.Text;
                modelSingle.SeoDescription = txtSeoDescription.Text;


                modelSingle.IsMsg = 0;
                modelSingle.IsTop = 0;
                modelSingle.IsRed = 0;
                modelSingle.IsHot = 0;
                modelSingle.IsSlide = 0;
                modelSingle.IsLock = 0;
                modelSingle.IsRed = 0;


                if (this.IsHot.Checked)
                {
                    modelSingle.IsHot = 1;
                }
                else
                {
                    modelSingle.IsHot = 0;

                }


                if (this.IsTop.Checked)
                {
                    modelSingle.IsTop = 1;
                }
                else
                {
                    modelSingle.IsTop = 0;

                }


                modelSingle.IsSlide = 0;

                if (this.IsLock.Checked)
                {
                    modelSingle.IsLock = 1;
                }
                else
                {
                    modelSingle.IsLock = 0;
                }

                SaveLogs("[图文链接模块]添加图文链接：" + modelSingle.Title);

                if (modelSingle.Id > 0)
                {
                    bllSingle.Update(modelSingle);
                    CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlSinglePage(classid);
                    if (channelmodel.PageType.Equals("13"))
                    {
                        Response.Write("<script type='text/javascript'>alert('修改成功，并生成静态网页了');location.href='edit.aspx?classid=" + this.classid + "&showmatypeid=" + this.classid + "'</script>");
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>alert('修改成功，并生成静态网页了');location.href='list.aspx?classid=" + this.classid + "&showmatypeid=" + this.classid + "'</script>");

                    }
                }
                else
                {
                    bllSingle.Add(modelSingle);
                   
                    CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlSinglePage(classid);
                    if (channelmodel.PageType.Equals("13"))
                    {

                        Response.Write("<script type='text/javascript'>alert('添加成功，并生成静态网页了');location.href='edit.aspx?classid=" + this.classid + "&showmatypeid=" + this.classid + "'</script>");
                    }
                    else
                    {

                        Response.Write("<script type='text/javascript'>alert('添加成功，并生成静态网页了');location.href='list.aspx?classid=" + this.classid + "&showmatypeid=" + this.classid + "'</script>");


                    }
                }
        }


        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        { }
    }
}