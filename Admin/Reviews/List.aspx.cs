using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Reviews
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小

        public int kindId;
        public int parentId;
        public string keywords = "";
        public string property = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.ContentPageNum;
            if (!int.TryParse(Request.Params["kindId"] as string, out this.kindId))
            {
                this.kindId = -1;
            }
            if (!int.TryParse(Request.Params["parentId"] as string, out this.parentId))
            {
                this.parentId = 0;
            }
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
                chkLoginLevel("viewReviews");
                TreeBind();
                RptBind("Id>0 and ver='"+Session["ver"].ToString()+"'" + CombPlSqlTxt(this.kindId, this.parentId, this.keywords, this.property), "AddTime desc");
            }
        }

        #region 查找归类标题
        protected string ViewKind(int _kindId)
        {
            switch (_kindId)
            {
                case (int)Channel.Article:
                    return "资讯模块";
                    break;
                case (int)Channel.Pictures:
                    return "图文模块";
                    break;
                case (int)Channel.Downloads:
                    return "下载模块";
                    break;
                case (int)Channel.Contents:
                    return "内容模块";
                    break;
            }
            return "";
        }
        #endregion

        #region 查找新闻标题
        protected string ViewTitle(int _kindId, int _parentId)
        {
            string str="错误，暂无找到该信息标题！";
            switch (_kindId)
            {
                case (int)Channel.Article:
                    DtCms.BLL.Article abll = new DtCms.BLL.Article();
                    DtCms.Model.Article amodel = abll.GetModel(_parentId);
                    if (amodel != null)
                    {
                        str = amodel.Title;
                    }
                    break;
                case (int)Channel.Pictures:
                    DtCms.BLL.Pictures pbll = new DtCms.BLL.Pictures();
                    DtCms.Model.Pictures pmodel = pbll.GetModel(_parentId);
                    if (pmodel != null)
                    {
                        str = pmodel.Title;
                    }
                    break;
                case (int)Channel.Downloads:
                    DtCms.BLL.Downloads dbll = new DtCms.BLL.Downloads();
                    DtCms.Model.Downloads dmodel = dbll.GetModel(_parentId);
                    if (dmodel != null)
                    {
                        str = dmodel.Title;
                    }
                    break;
            }
            return str;
        }
        #endregion

        #region 归类绑定
        private void TreeBind()
        {
            this.ddlKindId.Items.Clear();
            this.ddlKindId.Items.Add(new ListItem("所有评论", ""));
            this.ddlKindId.Items.Add(new ListItem("资讯模块", ((int)Channel.Article).ToString()));
            this.ddlKindId.Items.Add(new ListItem("图文模块", ((int)Channel.Pictures).ToString()));
            this.ddlKindId.Items.Add(new ListItem("下载模块", ((int)Channel.Downloads).ToString()));
        }
        #endregion

        #region 数据列表绑定
        private void RptBind(string strWhere, string orderby)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.AllReviews bll = new DtCms.BLL.AllReviews();
            //获得总条数
            this.pcount = bll.GetCount(strWhere);
            if (this.pcount > 0)
            {
                this.lbtnDel.Enabled = true;
                this.lbtnAudit.Enabled = true;
            }
            else
            {
                this.lbtnDel.Enabled = false;
                this.lbtnAudit.Enabled = false;
            }
            if (this.kindId >= 0)
            {
                this.ddlKindId.SelectedValue = this.kindId.ToString();
            }
            this.txtKeywords.Text = this.keywords;
            this.ddlProperty.SelectedValue = this.property;

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, orderby);
            this.rptList.DataBind();
        }
        #endregion

        //大类筛选
        protected void ddlKindId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _kindId;
            if (int.TryParse(this.ddlKindId.SelectedValue.ToString(), out _kindId))
            {
                Response.Redirect("list.aspx?" + CombUrlTxt(_kindId, 0, this.keywords, this.property) + "page=0");
            }
            else
            {
                Response.Redirect("list.aspx?" + CombUrlTxt(-1, 0, this.keywords, this.property) + "page=0");
            }
        }

        //属性筛选
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(this.kindId, this.parentId, this.keywords, this.ddlProperty.SelectedValue) + "page=0");
        }

        //查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(this.kindId, this.parentId, txtKeywords.Text.Trim(), this.property) + "page=0");
        }

        //审核
        protected void lbtnAudit_Click(object sender, EventArgs e)
        {
            chkLoginLevel("editReviews");
            DtCms.BLL.AllReviews bll = new DtCms.BLL.AllReviews();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "IsLock=0");
                    //保存日志
                    SaveLogs("[评论信息]审核评论信息，信息ID：" + id);
                }
            }
           JscriptPrint("批量审核通过啦！", "List.aspx?" + CombUrlTxt(this.kindId, this.parentId, this.keywords, this.property) + "page=0", "Success");
        }

        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delReviews");
            DtCms.BLL.AllReviews bll = new DtCms.BLL.AllReviews();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //保存日志
                    SaveLogs("[评论信息]删除评论信息，信息ID：" + id);
                    //删除记录
                    bll.Delete(id);
                }
            }
            JscriptPrint("批量删除成功啦！", "List.aspx?" + CombUrlTxt(this.kindId, this.parentId, this.keywords, this.property) + "page=0", "Success");
        }

    }
}
