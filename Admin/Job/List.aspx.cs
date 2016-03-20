using System;
using System.IO;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using DtCms.Web.UI;

namespace DtCms.Web.Admin.Job
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小
        public readonly int kindId = (int)Channel.Job;                      //类别种类
        protected BLL.Channel channel = new BLL.Channel();
        public int classId;
        public string keywords = "";
        public string property = "";

        public string strWhere;
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
        protected DataSet ds;
        protected string classList;
        protected int GdClaId;
        protected int newsclassid = 0;
        protected Model.Channel channelmodel = new Model.Channel();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.ArticlePageNum;
            if (!int.TryParse(Request.Params["classId"] as string, out this.classId))
            {
                this.classId = 0;
            }
            else
            { 
                this.classId = Convert.ToInt32(Request.QueryString["classId"]);

                if (!string.IsNullOrEmpty(Request.QueryString["newsclassid"]))
                {

                    newsclassid = DtCms.Common.Utils.returnIntByString(Request.QueryString["newsclassid"]);
                }
                else
                {

                    newsclassid = this.classId;

                }

            }

            if (!string.IsNullOrEmpty(Request.Params["keywords"]))
            {
                this.keywords = Request.Params["keywords"].Trim();
            }
            if (!string.IsNullOrEmpty(Request.Params["property"]))
            {
                this.property = Request.Params["property"].Trim();
            }

            channelmodel = channel.GetModel(this.classId);

            

            if (!Page.IsPostBack)
            {
                this.ddlProperty.Visible = false;

                //绑定类别
                ChannelTreeBind(this.GdClaId, "所有类别", (int)Channel.Job, this.ddlClassId, ver);
                strWhere = "Id>0 and ver='" + ver + "'" + CombSqlTxt(this.kindId, this.newsclassid, this.keywords, this.property);
                this.RptBind("job.id desc,AddTime desc");
                ChannelTreeBind(this.classId, channelmodel.Title, (int)Channel.Pictures, this.ddlClassId, ver);
                this.ddlClassId.SelectedValue = newsclassid + "";

            }
        }

        #region 数据列表绑定
        private void RptBind( string orderby)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Job bll = new DtCms.BLL.Job();
            //获得总条数
            this.pcount = bll.GetCount(strWhere);
            if (this.pcount > 0)
            {
                this.lbtnDel.Enabled = true;
            }
            else
            {
                this.lbtnDel.Enabled = false;
            }
            if (this.classId > 0)
            {
                this.ddlClassId.SelectedValue = this.classId.ToString();
            }
            this.txtKeywords.Text = this.keywords;
            this.ddlProperty.SelectedValue = this.property;

            this.rptList.DataSource = bll.getPageListJobAndChannel(this.pagesize, this.page, "job.id>0 and job.ver='" + ver + "'", orderby);
            this.rptList.DataBind();
           
        }
        #endregion

        //设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            DtCms.BLL.Job bll = new DtCms.BLL.Job();
            DtCms.Model.Job model = bll.GetModel(id);
            switch (e.CommandName.ToLower())
            {
               
                case "ibtntop":
                    if (model.IsTop == 1)
                        bll.UpdateField(id, "IsTop=0");
                    else
                        bll.UpdateField(id, "IsTop=1");
                    break;
                case "ibtnred":
                    if (model.IsRed == 1)
                        bll.UpdateField(id, "IsRed=0");
                    else
                        bll.UpdateField(id, "IsRed=1");
                    break;
             
            } 
        }
        //类别筛选
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                Response.Redirect("List.aspx?" + CombUrlTxt(DtCms.Common.Utils.returnIntByString(this.ddlClassId.SelectedValue), this.keywords, this.property) + "page=0&classid=" + this.classId);
            }
            else
            {
                Response.Redirect("List.aspx?" + CombUrlTxt(DtCms.Common.Utils.returnIntByString(this.ddlClassId.SelectedValue), this.keywords, this.property) + "page=0&classid=" + this.classId);
            }
        }

        //属性筛选
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(newsclassid, this.keywords, this.ddlProperty.SelectedValue) + "page=0&classid=" + this.classId);
        }

        //关健字查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(newsclassid, txtKeywords.Text.Trim(), this.property) + "page=0&classid=" + this.classId);
        }
        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
             
            DtCms.BLL.Job bll = new DtCms.BLL.Job();
            DtCms.Model.Job model;
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    model = bll.GetModel(id);
                    //删除图片

                    //保存日志
                    SaveLogs("[招聘模块]删除招聘：" + model.Title);
                    //删除记录
                    bll.Delete(id);
                }
            }

            this.rptList.DataBind();
            Response.Write("<script type='text/javascript'>alert('删除成功');location.href='List.aspx?" + CombUrlTxt(newsclassid, this.keywords, this.property) + "page=" + Request.QueryString["page"] + "&classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void lbtnMakeHtml_Click(object sender, EventArgs e)
        {

            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlHrPage(classId);
            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='list.aspx?classid=" + this.classId + "'</script>");
            Response.End();

        }

        protected void updateInfo_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Job bll = new DtCms.BLL.Job();

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);

                int sortid = Convert.ToInt32(((TextBox)rptList.Items[i].FindControl("paixuSortId")).Text);

                bll.UpdateField(id, " sortid=" + sortid);

            }


            this.rptList.DataBind();
            Response.Write("<script type='text/javascript'>alert('修改成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=" + Request.QueryString["page"] + "&classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlHrPageList(classId);
            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='list.aspx?classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Job bll = new DtCms.BLL.Job();
            string allid = string.Empty;
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    if (!string.IsNullOrEmpty(allid))
                    {

                        allid += "," + id;

                    }
                    else
                    {

                        allid = id + "";

                    }



                }

            }

            if (!string.IsNullOrEmpty(allid))
            {

                CreateHtmlByWeb.ShowAllHtmlCreate.CreateNewsSinglePageByWeb(this.classId, allid);

            }

            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=0&classid=" + this.classId + "'</script>");
            Response.End();
        }


       
    }
}
