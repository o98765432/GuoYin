using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace DtCms.Web.Admin.Advertising
{
    public partial class AdvList : DtCms.Web.UI.ManagePage
    {
        public int pcount = 0; //总条数
        public int page; //当前页
        public int pagesize; //设置每页显示的大小

        public string keywords = "";
        public string property = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.AdPageNum;
            if (!string.IsNullOrEmpty(Request.Params["keywords"]))
            {
                this.keywords = Request.Params["keywords"].Trim();
            }
            if (!string.IsNullOrEmpty(Request.Params["property"]))
            {
                this.property = Request.Params["property"].Trim();
            }
            if (!Page.IsPostBack)
            {
                chkLoginLevel("viewAdvertising");
                RptBind("Id>0 and ver='"+Session["ver"].ToString()+"'" + CombAdSqlTxt(this.keywords, this.property));
            }
        }

        #region 数据绑定
        private void RptBind(string where)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            this.txtKeywords.Text = this.keywords;
            this.ddlProperty.SelectedValue = this.property;

            DtCms.BLL.Advertising bll = new DtCms.BLL.Advertising();
            //利用PAGEDDAGASOURCE类来分页
            PagedDataSource pg = new PagedDataSource();
            pg.DataSource = bll.GetList(where).Tables[0].DefaultView;
            pg.AllowPaging = true;
            pg.PageSize = pagesize;
            pg.CurrentPageIndex = page;
            //获得总条数
            pcount = bll.GetCount(where);
            //绑定数据
            rptList.DataSource = pg;
            rptList.DataBind();
        }
        #endregion

        #region 显示广告类型
        protected string GetTypeName(string strId)
        {
            switch (strId)
            {
                case "1":
                    return "文字";
                case "2":
                    return "图片";
                case "3":
                    return "幻灯片";
                case "4":
                    return "动画";
                case "5":
                    return "视频";
                case "6":
                    return "代码";
                default:
                    return "其它";
            }
        }
        #endregion

        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delAdvertising");
            DtCms.BLL.Advertising bll = new DtCms.BLL.Advertising();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //保存日志
                    SaveLogs("[广告管理]删除广告位：" + bll.GetModel(id).Title);
                    //删除记录
                    bll.Delete(id);
                }
            }
            JscriptPrint("批量删除成功啦！", "AdvList.aspx?" + CombUrlTxt(this.keywords, this.property) + "page=0", "Success");
        }

        //筛选属性
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("AdvList.aspx?" + CombUrlTxt(this.keywords, this.ddlProperty.SelectedValue) + "page=0");
        }
        //关健字查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdvList.aspx?" + CombUrlTxt(txtKeywords.Text.Trim(), this.property) + "page=0");
        }
    }
}
