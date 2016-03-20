using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace DtCms.Web.Admin.ProductKey
{
    public partial class list : DtCms.Web.UI.ManagePage
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
        protected int shownowid;

        protected DataSet ds;
        protected BLL.Channel channel = new BLL.Channel();
        protected int returnclassid = 0;
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
                this.returnclassid = Convert.ToInt32(Request.QueryString["returnclassid"]);
                this.shownowid = Convert.ToInt32(Request.QueryString["shownowid"]);
                 
            }

            channelmodel = channel.GetModel(this.classId);

            if (!string.IsNullOrEmpty(Request.Params["keywords"]))
            {
                this.keywords = Request.Params["keywords"].Trim();
            }

            if (!string.IsNullOrEmpty(Request.QueryString["allinfo"])) 
            {

                string[] allmodel = Request.QueryString["allinfo"].Split('*');

                foreach (string modelinfoby in allmodel) 
                {

                    string[] showinfp = modelinfoby.Split(',');

                    new BLL.Productkey().UpdateField(DtCms.Common.Utils.returnIntByString(showinfp[0]), " sortid=" + showinfp[1]);
                
                }

                Response.Write("<script type='text/javascript'>alert('修改成功');location.href='List.aspx?page=0&classid=" + this.classId + "&returnclassid=" + this.returnclassid + "&shownowid=" + shownowid + "'</script>");
                Response.End();

            }

            if (!IsPostBack)
            {

                this.lbtnDel.Visible = deleteflag;
                //ChannelTreeBind(this.classId, channelmodel.Title, (int)Channel.Article, this.ddlClassId, "cn");


                this.RptBind("Id>0 and ver='" + Session["ver"].ToString() + "' and classid=" + this.classId + " and blinfo6 like '" + shownowid + "' ");
 
            }




        }

        public void RptBind(string strWhere)
        {
            DtCms.BLL.Productkey bll = new DtCms.BLL.Productkey();
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
                Response.Redirect("List.aspx?page=0&classid=" + this.classId + "&returnclassid=" + this.returnclassid + "&shownowid=" + shownowid);
            }
            else
            {
                Response.Redirect("List.aspx?page=0&classid=" + this.classId + "&returnclassid=" + this.returnclassid + "&shownowid=" + shownowid);
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
            RptBind("Id>0 and ver='" + Session["ver"].ToString() + "' and classid=" + classId + " " + "&shownowid=" + shownowid);
        }
        protected void lbtnDel_Click(object sender, EventArgs e)
        {


            DtCms.BLL.Productkey bll = new DtCms.BLL.Productkey();
            DtCms.Model.Productkey model = new Model.Productkey();
            //批量删除
            for (int i = 0; i < rptBanner.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptBanner.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptBanner.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    model = bll.GetModel(id);
                    //删除图片
                    DeleteFile(model.blinfo1);
           
                    //删除记录
                    bll.Delete(id);
                }
            }

            Response.Write("<script type='text/javascript'>alert('删除成功');location.href='List.aspx?page=0&classid=" + this.classId + "&returnclassid=" + this.returnclassid + "&shownowid=" + shownowid+"'</script>");
            Response.End();
        }
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }
    }
}