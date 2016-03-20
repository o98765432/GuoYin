using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web.Admin.Banner
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        protected int pcount;    //总条数
        protected int page;      //当前页
        protected int pagesize;  //设置每页显示的大小
        protected readonly int kindId = (int)Channel.Bannner;  //类别种类


        protected string property = "";
        protected string keywords = "";
        protected string prolistview = "";

        protected int classId;


        protected string ver;
        protected string classList;
        protected string sqlWhere;

        protected DataSet ds;
        protected BLL.Channel channel = new BLL.Channel();
        protected int newsclassid = 0;
        protected Model.Channel channelmodel = new Model.Channel();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ver = Session["ver"].ToString();
            this.pagesize = webset.ContentPageNum;
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
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

            channelmodel = channel.GetModel(this.classId);

            if (!string.IsNullOrEmpty(Request.Params["keywords"]))
            {
                this.keywords = Request.Params["keywords"].Trim();
            }
            if (!IsPostBack)
            {
                this.lbtnDel.Visible = deleteflag;


                if (classId!=12)
                {
                    this.Button2.Visible = false;
                }

         
                ChannelTreeBind(this.classId, channelmodel.Title, (int)Channel.Article, this.ddlClassId, "cn");


                this.RptBind("Id>0 and ver='" + Session["ver"].ToString() + "'" + CombSqlTxt(this.kindId, this.newsclassid, this.keywords, this.property));

                this.ddlClassId.SelectedValue = newsclassid + "";
            }

            
            

        }

        public void RptBind(string strWhere)
        {
            DtCms.BLL.Bannner bll = new DtCms.BLL.Bannner();
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
            if (!string.IsNullOrEmpty(this.keywords))
            {
                this.txtKeywords.Text = this.keywords;
            }

            this.rptBanner.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, "SortId asc,Id desc");
            this.rptBanner.DataBind();
        }

        //类别筛选
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                Response.Redirect("List.aspx?" + CombUrlTxt(Convert.ToInt32(this.ddlClassId.SelectedValue), this.keywords, this.property) + "page=0&classid=" + this.classId);
            }
            else
            {
                Response.Redirect("List.aspx?" + CombUrlTxt(Convert.ToInt32(this.ddlClassId.SelectedValue), this.keywords, this.property) + "page=0&classid=" + this.classId);
            }
        }
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            int _classId;
            string _keywords = "";
            if (!int.TryParse(this.ddlClassId.SelectedValue, out _classId))
            {
                _classId = 0;
            }
            if (!string.IsNullOrEmpty(this.txtKeywords.Text))
            {
                _keywords = this.txtKeywords.Text;
            }
            //转向页面
            RptBind("Id>0 and ver='" + Session["ver"].ToString() + "' " + CombSqlTxt(this.kindId, this.classId, this.keywords, this.property));
        }
        protected void lbtnDel_Click(object sender, EventArgs e)
        {

            
            DtCms.BLL.Bannner bll = new DtCms.BLL.Bannner();
            DtCms.Model.Bannner model = new Model.Bannner();
            //批量删除
            for (int i = 0; i < rptBanner.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptBanner.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptBanner.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    model = bll.GetModel(id);
                    //删除图片
                    DeleteFile(model.Imgurl);
                    DeleteFile(model.ImgurlSmall);
                    //保存日志
                    SaveLogs("[资讯模块]删除文章：" + model.Title);
                    //删除记录
                    bll.Delete(id);
                }
            }

            Response.Write("<script type='text/javascript'>alert('删除成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=0&classid=" + this.classId + "'</script>");
            Response.End();
        }
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void updateInfo_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Bannner bll = new DtCms.BLL.Bannner();

            for (int i = 0; i < rptBanner.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptBanner.Items[i].FindControl("lb_id")).Text);

                int sortid = Convert.ToInt32(((TextBox)rptBanner.Items[i].FindControl("paixuSortId")).Text);

                bll.UpdateField(id, " sortid=" + sortid);

            }


            this.rptBanner.DataBind();
            Response.Write("<script type='text/javascript'>alert('修改成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=0&classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlBannerList(this.classId);
            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=0&classid=" + this.classId + "'</script>");
            Response.End();
        }
    }
}