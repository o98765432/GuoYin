using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web.Admin.FriendshipLink
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小
        public readonly int kindId = (int)Channel.FriendshipLink;  //类别种类
        public int classId;
        public string keywords = "";
        public string property = "";


        protected int GdClaId;
        protected string ver;
        protected string classList;
        protected BLL.FriendshipLink shipBll = new BLL.FriendshipLink();
        protected BLL.Channel channel = new BLL.Channel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            this.lbtnDel.Visible = deleteflag;

            this.ver = Session["ver"].ToString();
            this.pagesize = webset.ContentPageNum;
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["keywords"]))
            {
                this.keywords = Request.QueryString["keywords"].Trim();
            }
            if(!string.IsNullOrEmpty(Request.QueryString["classId"]))
            {
                this.classId = Convert.ToInt32(Request.QueryString["classId"]);
            }


            DataSet ds = channel.GetClassList(this.classId, this.ver);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    classList = ds.Tables[0].Rows[i]["ClassList"].ToString();
                }

                string[] message = classList.Split(',');

                this.GdClaId = Convert.ToInt32(message[1]);
            }
            chkLoginLevel("viewContents");
            //ChannelTreeBind(0, "所有类别", (int)Channel.FriendshipLink, this.ddlClassId, "cn");
            ChannelTreeBind(this.GdClaId, "所有类别", (int)Channel.FriendshipLink, this.ddlClassId, "cn");
            this.RptBind("Id>0 and ver='" + Session["ver"].ToString() + "'" + CombSqlTxt(this.kindId, this.classId, this.keywords));
        }

        /// <summary>
        /// 绑定信息
        /// </summary>
        public void RptBind(string strWhere)
        {
  
            //获得总条数
            this.pcount = shipBll.GetCount(strWhere);
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

            this.rptList.DataSource = shipBll.GetPageList(this.pagesize, this.page, strWhere, "SortId asc,Id desc");
            this.rptList.DataBind();
        }


        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                Response.Redirect("List.aspx?" + CombUrlTxt(_classId, this.keywords, this.property) + "page=0");
            }
            else
            {
                Response.Redirect("List.aspx?" + CombUrlTxt(0, this.keywords, this.property) + "page=0");
            }
        }

        //查询
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
            Response.Redirect("List.aspx?" + CombUrlTxt(_classId, _keywords) + "page=0");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delContents");
            DtCms.BLL.FriendshipLink bll = new DtCms.BLL.FriendshipLink();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //保存日志
                    SaveLogs("[内容模块]删除内容页：" + bll.GetModel(id).Title);
                    bll.Delete(id);
                }
            }
            Response.Write("<script type='text/javascript'>alert('删除成功');location.href='list.aspx?classid=" + this.classId + "'</script>");
            Response.End();
        }


        protected void lbtnMakeHtml_Click(object sender, EventArgs e)
        {
           
        }
    }
}