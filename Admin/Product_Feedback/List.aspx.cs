using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Product_Feedback
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小

        public string keywords = "";
        public string property = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.FeedbackPageNum;
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
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
                this.lbtnDel.Visible = deleteflag;
                chkLoginLevel("viewProductFeedback");
                ChannelTreeBind(0, "所有类别", (int)Channel.Product, this.ddlClassId,"cn");
                int _classId;
               
                if (Request.QueryString["classid"] != null) {
                    //GetData(Request.QueryString["classid"]);
                   
                    this.ddlClassId.SelectedValue = Request.QueryString["classid"];
                }
                else
                {
                    if (Request.QueryString["productid"] != null) {
                        this.ddlClassId.SelectedValue = Request.QueryString["productid"];
                    }
                    GetData("","");
                }
                
            }
        }

        private void GetData(string classid,string productid) 
        {
            //if (int.TryParse(Request.QueryString["classid"], out _classId))
            //{
            if (productid != "")
            {
                this.ddlProductId.SelectedValue = productid;
                this.ddlClassId.SelectedValue = classid;
                this.RptBind("Id>0 and ver='"+Session["ver"].ToString()+"' and TypeId=" + productid + CombSqlTxt(this.keywords, this.property));
            }
            else
            {
                this.RptBind("Id>0 and ver='"+Session["ver"].ToString()+"' " + CombSqlTxt(this.keywords, this.property));
            }
            //}
            //else
            //{
            //    
            //}
        }

        #region 数据列表绑定
        private void RptBind(string strWhere)
        {
            DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback();
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
            //int _classId;
            //if (int.TryParse(this.ddlProductId.SelectedValue.ToString(), out _classId))
            //{
            //    Response.Redirect("List.aspx?classid=" +  int.Parse(this.ddlProductId.SelectedValue) + "&productid="+this.ddlProductId.SelectedValue+"&" + CombUrlTxt((int)Channel.Product, int.Parse(this.ddlProductId.SelectedValue), this.keywords, this.ddlProperty.SelectedValue) + "page=0");
            //}
            //else
            //{
            //    Response.Redirect("List.aspx?" + CombUrlTxt((int)Channel.Product,0, this.keywords, this.ddlProperty.SelectedValue) + "page=0");
            //}
            this.property = this.ddlProperty.SelectedValue;
            
            if (this.ddlProductId.Items.Count > 0)
            {
                GetData(this.ddlClassId.SelectedValue, this.ddlProductId.SelectedValue);
            }
            else
            {
                GetData("","");
            }
        }

        //查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            ////转向页面
            //int _classId;
            //if (int.TryParse(this.ddlProductId.SelectedValue.ToString(), out _classId))
            //{
            //    Response.Redirect("List.aspx?classid=" +  int.Parse(this.ddlProductId.SelectedValue) + "&" + CombUrlTxt((int)Channel.Product, int.Parse(this.ddlProductId.SelectedValue), this.txtKeywords.Text.Trim(), this.property) + "page=0");
            //}
            //else
            //{
            //    Response.Redirect("List.aspx?" + CombUrlTxt((int)Channel.Product,0, this.txtKeywords.Text.Trim(), this.property) + "page=0");
            //}

            this.keywords = this.txtKeywords.Text.Trim();
            this.property = this.ddlProperty.SelectedValue;

            if (this.ddlProductId.Items.Count > 0)
            {
                GetData(this.ddlClassId.SelectedValue, this.ddlProductId.SelectedValue);
            }
            else
            {
                GetData("", "");
            }
          
        }
        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delFeedback");
            DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //保存日志
                    SaveLogs("[留言管理]删除留言：" + bll.GetModel(id).Title);
                    //删除记录
                    bll.Delete(id);
                }
            }

            int _classId;
            if (int.TryParse(this.ddlProductId.SelectedValue.ToString(), out _classId))
            {
                JscriptPrint("批量删除成功啦！", "List.aspx?classid=" +  int.Parse(this.ddlProductId.SelectedValue) + "&" + CombUrlTxt((int)Channel.Product, _classId, this.keywords, this.property) + "page=0", "Success");
            }
            else
            {
                JscriptPrint("批量删除成功啦！", "List.aspx?" + CombUrlTxt((int)Channel.Product, 0, this.keywords, this.property) + "page=0", "Success");
            }
        }

        //分类筛选
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int _classId;
            //if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            //{
            //    Response.Redirect("List.aspx?" + CombUrlTxt((int)Channel.Product, _classId, this.keywords, this.property) + "page=0");
            //}
            //else
            //{
            //    Response.Redirect("List.aspx?" + CombUrlTxt((int)Channel.Product, 0, this.keywords, this.property) + "page=0");
            //}

             

            
            this.rptList.DataSource = null;
            this.rptList.DataBind();

            DtCms.BLL.Product bll = new DtCms.BLL.Product();
            this.ddlProductId.DataSource = bll.GetList(" classid=" +  int.Parse(this.ddlClassId.SelectedValue)+" and ver='"+Session["ver"].ToString()+"'");
            this.ddlProductId.DataTextField = "Title";
            this.ddlProductId.DataValueField = "Id";
            this.ddlProductId.DataBind();
            if (this.ddlProductId.Items.Count > 0)
            {
                GetData(this.ddlClassId.SelectedValue,this.ddlProductId.SelectedValue);
            }
            //int _classId;
            //if (this.ddlProductId.Items.Count > 0)
            //{
            //    if (int.TryParse(this.ddlProductId.SelectedValue.ToString(), out _classId))
            //    {
            //        Response.Redirect("List.aspx?classid=" + int.Parse(this.ddlClassId.SelectedValue) + "&" + CombUrlTxt((int)Channel.Product, int.Parse(this.ddlProductId.SelectedValue), this.keywords, this.ddlProperty.SelectedValue) + "page=0");
            //    }
            //    else
            //    {
            //        Response.Redirect("List.aspx?" + CombUrlTxt((int)Channel.Product, 0, this.keywords, this.ddlProperty.SelectedValue) + "page=0");
            //    }
            //}
        }

        //审核
        protected void lbtnAudit_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "IsLock=0");
                    //保存日志
                    SaveLogs("[留言管理]审核留言：" + bll.GetModel(id).Title);
                }
            }
            int _classId;

            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                JscriptPrint("批量审核通过啦！", "List.aspx?" + CombUrlTxt((int)Channel.Product, int.Parse(this.ddlProductId.SelectedValue), this.keywords, this.property) + "page=" + this.page, "Success");
            }
            else
            {
                JscriptPrint("批量审核通过啦！", "List.aspx?" + CombUrlTxt((int)Channel.Product,0, this.keywords, this.property) + "page=" + this.page, "Success");
            }
        }

        protected void ddlProductId_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.rptList.DataSource = null;
            this.rptList.DataBind();

            if (this.ddlProductId.Items.Count > 0)
            {
                GetData(this.ddlClassId.SelectedValue, this.ddlProductId.SelectedValue);
            }
            else
            {
                GetData("","");
            }
            //if (this.ddlProductId.Items.Count > 0)
            //{
            //    if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            //    {
            //        Response.Redirect("List.aspx?classid=" + int.Parse(this.ddlProductId.SelectedValue) + "&" + CombUrlTxt((int)Channel.Product, int.Parse(this.ddlProductId.SelectedValue), this.keywords, this.ddlProperty.SelectedValue) + "page=0");
            //    }
            //    else
            //    {
            //        Response.Redirect("List.aspx?" + CombUrlTxt((int)Channel.Product, 0, this.keywords, this.ddlProperty.SelectedValue) + "page=0");
            //    }
            //}
        }
        
    }
}
