using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DtCms.Common;

namespace DtCms.Web.Admin.Consultant
{
    public partial class CorparationsList : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小
        public readonly int kindId = (int)Channel.Contents;  //类别种类
        public int classId;
        public string keywords = "";
        public string property = "";


        protected int GdClaId;
        protected string ver;
        protected string classList;
        protected BLL.Channel channel = new BLL.Channel();
        protected int newsclassid = 0;
        protected Model.Channel channelmodel = new Model.Channel();

        protected void Page_Load(object sender, EventArgs e)
        {
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
                this.GdClassId = Request.QueryString["classId"];
                this.GdClaId = Convert.ToInt32(this.GdClassId);
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
            this.ver = Session["ver"].ToString();

            DataSet ds = channel.GetClassList(this.classId, ver);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    classList = ds.Tables[0].Rows[i]["ClassList"].ToString();
                }

                string[] message = classList.Split(',');

                this.GdClaId = Convert.ToInt32(message[1]);
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

              //  ChannelTreeBind(this.classId, channelmodel.Title, (int)Channel.Pictures, this.ddlClassId, "cn");

                this.RptBind("Id>0 and  typeId = 2 ");


                //this.ddlClassId.SelectedValue = newsclassid + "";
            }
        }

        #region 数据列表绑定
        private void RptBind(string strWhere)
        {
            DtCms.BLL.Consultant bll = new DtCms.BLL.Consultant();
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
               // this.ddlClassId.SelectedValue = this.classId.ToString();
            }
            if (!string.IsNullOrEmpty(this.keywords))
            {
                //this.txtKeywords.Text = this.keywords;
            }

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, "Id desc");
            this.rptList.DataBind();
        }
        #endregion

        //查询
        //protected void btnSelect_Click(object sender, EventArgs e)
        //{
        //    int _classId;
        //    string _keywords = "";
        //    if (!int.TryParse(this.ddlClassId.SelectedValue, out _classId))
        //    {
        //        _classId = 0;
        //    }
        //    if (!string.IsNullOrEmpty(this.txtKeywords.Text))
        //    {
        //        _keywords = this.txtKeywords.Text;
        //    }
        //    //转向页面
        //    Response.Redirect("List.aspx?" + CombUrlTxt(_classId, _keywords) + "page=0");
        //}

        //删除


        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delContents");
            DtCms.BLL.Consultant bll = new DtCms.BLL.Consultant();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //保存日志
                   // SaveLogs("[内容模块]删除内容页：" + bll.GetModel(id).Title);
                    bll.Delete(id);
                }
            }
            this.rptList.DataBind();
            Response.Write("<script type='text/javascript'>alert('删除成功');location.href='CorparationsList.aspx?" + CombUrlTxt(this.classId, this.keywords, this.property) + "page=0&classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void lbtnMakeHtml_Click(object sender, EventArgs e)
        {
            GenHtml();
        }

        protected void GenHtml()
        {
            DtCms.Common.HtmlWriter htmlWriter = new HtmlWriter();

            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
            DataSet ds = bll.GetList(" ver='" + Session["ver"].ToString() + "'");

            int successCount = 0, failCount = 0;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    string filePath = string.Empty, TempPath = string.Empty;
                    filePath = Server.MapPath("../../" + dr["CallIndex"].ToString() + ".html");
                    TempPath = Server.MapPath(dr["Filepath"].ToString());

                    string[] strNewHtml = {   
                       dr["id"].ToString(),
                       dr["CallIndex"].ToString(),
                       dr["Title"].ToString(),
                       dr["ClassId"].ToString(),
                       dr["Content"].ToString(),
                       dr["ImgUrl"].ToString(),
                       dr["ImgUrl2"].ToString(),
                       dr["Filepath"].ToString(),
                       dr["Keyword"].ToString(),
                       dr["Description"].ToString(),
                       dr["Intro"].ToString()
                    };

                    string[] strOldHtml = { "{#Id}", "{#CallIndex}", "{#Title}", "{#ClassId}", "{#Content}", "{#ImgUrl}", "{#ImgUrl2}", "{#Filepath}", "{#Keyword}", "{#Description}", "{#Intro}" };

                    if (htmlWriter.CreateHtml(strNewHtml, strOldHtml, TempPath, filePath))
                    {
                        successCount++;
                    }
                    else
                    {
                        failCount++;
                    }

                }

                JscriptPrint("批量生成结果   成功：" + successCount + "条,失败:" + failCount + "条！", "List.aspx?" + CombUrlTxt(this.classId, this.keywords) + "page=0", "Success");
            }
            else
            {
                JscriptMsg(350, 230, "错误提示", "对不起，没有数据！", "back", "Error");
            }

        }


        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int _classId;
            //if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            //{
            //    Response.Redirect("List.aspx?" + CombUrlTxt(_classId, this.keywords, this.property) + "page=0&classid=" + this.classId);
            //}
            //else
            //{
            //    Response.Redirect("List.aspx?" + CombUrlTxt(0, this.keywords, this.property) + "page=0&classid=" + this.classId);
            //}
        }


        #region " Property "

        private string GdClassId
        {
            get
            {
                string temp = "0";
                if (Session["jobpageindex"] != null)
                {
                    temp = Session["jobpageindex"].ToString();
                }
                return temp;
            }
            set
            {
                Session["jobpageindex"] = value;
            }
        }

        #endregion

    }
}