using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using DtCms.Web.UI;

namespace DtCms.Web.Admin.Product_Downloads
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小
        public readonly int kindId = (int)Channel.Downloads; //类别种类

        public int classId;
        public string keywords = "";
        public string property = "";

        protected int GdClaId;
        protected string ver = "cn";
        protected string classList;
        protected BLL.Channel channel = new BLL.Channel();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.DownPageNum;
            if (!int.TryParse(Request.Params["classId"] as string, out this.classId))
            {
                this.classId = 0;
            }
            else
            {
                this.GdClassId = Request.QueryString["classId"];
                this.GdClaId = Convert.ToInt32(this.GdClassId);
                this.classId = Convert.ToInt32(Request.QueryString["classId"]);
            }

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

                this.lbtnDel.Visible = deleteflag;
                chkLoginLevel("viewProductDownloads");
                //绑定类别
                ChannelTreeBind(this.GdClaId, "所有类别", (int)Channel.Product, this.ddlClassId, "cn");
                //ChannelTreeBind(0, "所有类别", (int)Channel.Product, this.ddlClassId,"cn");
             
            }
        }

        private void GetData(string classid, string productid)
        {
            this.rptList.DataSource = null;
            this.rptList.DataBind();

            if (productid != "")
            {
                //this.ddlProductId.SelectedValue = productid;
                //this.ddlClassId.SelectedValue = classid;

                //productid =this.ddlProductId.SelectedValue;
                //classid = this.ddlClassId.SelectedValue;


                classid = this.ddlProductId.SelectedValue;

                RptBind("Id>0 and classid="+classid+ CombSqlTxt(this.kindId, this.classId, this.keywords, this.property), "AddTime desc");
            }
            else
            {
                RptBind("Id>0" + CombSqlTxt(this.kindId, this.classId, this.keywords, this.property), "AddTime desc");
            }
        }

        #region 数据列表绑定
        private void RptBind(string strWhere, string orderby)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.ProductDownloads bll = new DtCms.BLL.ProductDownloads();
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

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, orderby);
            this.rptList.DataBind();
        }
        #endregion

        //设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            chkLoginLevel("editDownloads");
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            DtCms.BLL.Downloads bll = new DtCms.BLL.Downloads();
            DtCms.Model.Downloads model = bll.GetModel(id);
            switch (e.CommandName.ToLower())
            {
                case "ibtnmsg":
                    if (model.IsMsg == 1)
                        bll.UpdateField(id, "IsMsg=0");
                    else
                        bll.UpdateField(id, "IsMsg=1");
                    break;
                case "ibtnred":
                    if (model.IsRed == 1)
                        bll.UpdateField(id, "IsRed=0");
                    else
                        bll.UpdateField(id, "IsRed=1");
                    break;
            }
            RptBind("Id>0" + CombSqlTxt(this.kindId, this.classId, this.keywords, this.property), "AddTime desc");


        }

        //分类筛选
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int _classId;
            //if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            //{
            //    Response.Redirect("List.aspx?" + CombUrlTxt(_classId, this.keywords, this.property) + "page=0");
            //}
            //else
            //{
            //    Response.Redirect("List.aspx?" + CombUrlTxt(0, this.keywords, this.property) + "page=0");
            //}

            this.rptList.DataSource = null;
            this.rptList.DataBind();

            DtCms.BLL.Product bll = new DtCms.BLL.Product();
            this.ddlProductId.DataSource = bll.GetList(" classid=" + int.Parse(this.ddlClassId.SelectedValue)+" and ver='"+Session["ver"].ToString()+"'");
            this.ddlProductId.DataTextField = "Title";
            this.ddlProductId.DataValueField = "Id";
            this.ddlProductId.DataBind();
            if (this.ddlProductId.Items.Count > 0)
            {
                GetData(this.ddlClassId.SelectedValue, this.ddlProductId.SelectedValue);
            }
        }

        //属性筛选
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Response.Redirect("List.aspx?" + CombUrlTxt(this.classId, this.keywords, this.ddlProperty.SelectedValue) + "page=0");

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

        //查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(this.classId, txtKeywords.Text.Trim(), this.property) + "page=0");
        }
        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delDownloads");
            DtCms.BLL.Downloads bll = new DtCms.BLL.Downloads();
            DtCms.Model.Downloads model;
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    model = bll.GetModel(id);
                    //删除图片
                    DeleteFile(model.ImgUrl);
                    //删除文件
                    DeleteFile(model.FilePath);
                    //保存日志
                    SaveLogs("[下载模块]删除下载：" + model.Title);
                    //删除记录
                    bll.Delete(this.kindId,id,Session["ver"].ToString());
                }
            }
            JscriptPrint("批量删除成功啦！", "List.aspx?" + CombUrlTxt(this.classId, this.keywords, this.property) + "page=0", "Success");
        }

        protected void ddlProductId_SelectedIndexChanged(object sender, EventArgs e)
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
            this.rptList.DataSource = "";
            this.rptList.DataBind();

            int _classId;
            if (this.ddlProductId.Items.Count > 0)
            {
                GetData(this.ddlClassId.SelectedValue, this.ddlProductId.SelectedValue);
            }
            else
            {
                GetData("", "");
            }
         
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
