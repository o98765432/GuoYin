﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web.Admin.PageType
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小
        public readonly int kindId = (int)Channel.PageType;  //类别种类
        public int classId;
        public string keywords = "";
        public string property = "";


        protected int GdClaId;
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
        protected string classList;

        protected BLL.Channel channel = new BLL.Channel();
        protected int newsclassid = 0;
        protected Model.Channel channelmodel = new Model.Channel();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.ContentPageNum;

            this.lbtnDel.Visible = false;

            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            if (!string.IsNullOrEmpty(Request.Params["keywords"]))
            {
                this.keywords = Request.Params["keywords"].Trim();
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

            
            if (!IsPostBack)
            {
            
                this.RptBind("Id>0 and ver='" + ver + "'" + CombSqlTxt(this.kindId, this.newsclassid, this.keywords));

                this.ddlClassId.SelectedValue = this.newsclassid + "";

            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        public void RptBind(string strWhere)
        {
            DtCms.BLL.PageType bll = new DtCms.BLL.PageType();
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

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, "SortId asc,Id desc");

            this.rptList.DataBind();

        }

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDel_Click(object sender, EventArgs e)
        {

            DtCms.BLL.PageType bll = new DtCms.BLL.PageType();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //保存日志
                    SaveLogs("[内容模块]删除内容页：" + bll.GetModel(id, ver).Title);
                    bll.Delete(kindId,id,ver);
                }
            }
            Response.Write("<script type='text/javascript'>alert('删除成功');location.href='list.aspx?classid=" + this.classId + "'</script>");
            Response.End();
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
            Response.Redirect("List.aspx?" + CombUrlTxt(_classId, _keywords) + "page=0");
        }


        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(Convert.ToInt32(this.ddlClassId.SelectedValue), this.keywords, "") + "page=0&classid=" + this.classId);
        }



        protected void lbtnMakeHtml_Click(object sender, EventArgs e)
        {
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlSinglePage(classId);
            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='list.aspx?classid=" + this.classId + "'</script>");
            Response.End();
        }
    }
}