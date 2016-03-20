using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Quest
{
    public partial class list : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小

        public string keywords = "";
        public string property = "";
        protected int classId;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.FeedbackPageNum;

            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }

            
            if (!string.IsNullOrEmpty(Request.Params["property"]))
            {
                this.property = Request.Params["property"].Trim();
            }

            if (!Page.IsPostBack)
            {
                this.lbtnDel.Visible = deleteflag; 
               this.RptBind("Id>0 and ver='" + Session["ver"].ToString() + "'" + CombSqlTxt(this.keywords, this.property));
                
            }
        }

        #region 数据列表绑定
        private void RptBind(string strWhere)
        {
            DtCms.BLL.Quest bll = new DtCms.BLL.Quest();
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
            if (!string.IsNullOrEmpty(this.keywords))
            {
                this.txtKeywords.Text = this.keywords;
            }
            this.ddlProperty.SelectedValue = this.property;

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, "AddTime desc");
            this.rptList.DataBind();
        }
        #endregion

        //属性筛选
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                Response.Redirect("List.aspx?classid=" + int.Parse(this.ddlClassId.SelectedValue) + "&" + CombUrlTxt((int)Channel.LeaveWord, int.Parse(this.ddlClassId.SelectedValue), this.keywords, this.ddlProperty.SelectedValue) + "page=0");
            }
            else
            {
                Response.Redirect("List.aspx?" + CombUrlTxt((int)Channel.LeaveWord, 0, this.keywords, this.ddlProperty.SelectedValue) + "page=0");
            }
        }

        //查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            //转向页面
            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                Response.Redirect("List.aspx?classid=" + int.Parse(this.ddlClassId.SelectedValue) + "&" + CombUrlTxt((int)Channel.LeaveWord, int.Parse(this.ddlClassId.SelectedValue), this.txtKeywords.Text.Trim(), this.property) + "page=0");
            }
            else
            {
                Response.Redirect("List.aspx?" + CombUrlTxt((int)Channel.LeaveWord, 0, this.txtKeywords.Text.Trim(), this.property) + "page=0");
            }
        }
        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Quest bll = new DtCms.BLL.Quest();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //保存日志
                  //  SaveLogs("[留言管理]删除留言：" + bll.GetModel(id).Title);
                    //删除记录
                    bll.Delete(id);
                }
            }

            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                JscriptPrint("批量删除成功啦！", "List.aspx?classid=" + int.Parse(this.ddlClassId.SelectedValue) + "&" + CombUrlTxt((int)Channel.LeaveWord, _classId, this.keywords, this.property) + "page=0", "Success");
            }
            else
            {
                JscriptPrint("批量删除成功啦！", "List.aspx?" + CombUrlTxt((int)Channel.LeaveWord, 0, this.keywords, this.property) + "page=0", "Success");
            }
        }

        //分类筛选
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {


            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                Response.Redirect("List.aspx?classid=" + int.Parse(this.ddlClassId.SelectedValue) + "&" + CombUrlTxt((int)Channel.LeaveWord, int.Parse(this.ddlClassId.SelectedValue), this.keywords, this.ddlProperty.SelectedValue) + "page=0");
            }
            else
            {
                Response.Redirect("List.aspx?" + CombUrlTxt((int)Channel.LeaveWord, 0, this.keywords, this.ddlProperty.SelectedValue) + "page=0");
            }
        }

        //审核
        protected void lbtnAudit_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Quest bll = new DtCms.BLL.Quest();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "IsLock=0");
                    //保存日志
                   // SaveLogs("[留言管理]审核留言：" + bll.GetModel(id).Title);
                }
            }
            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                JscriptPrint("批量审核通过啦！", "List.aspx?" + CombUrlTxt((int)Channel.LeaveWord, int.Parse(this.ddlClassId.SelectedValue), this.keywords, this.property) + "page=" + this.page, "Success");
            }
            else
            {
                JscriptPrint("批量审核通过啦！", "List.aspx?" + CombUrlTxt((int)Channel.LeaveWord, 0, this.keywords, this.property) + "page=" + this.page, "Success");
            }
        }

        protected void lbtnMakeHtml_Click(object sender, EventArgs e)
        {

            CreateHtmlByWeb.ShowAllHtmlCreate.CreateQuestInfoByWeb("ShowMessage/","index.html",10);
            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='list.aspx'</script>");
            Response.End();


        }

    }
}
