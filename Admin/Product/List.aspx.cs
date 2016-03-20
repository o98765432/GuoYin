using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using DtCms.Web.UI;

namespace DtCms.Web.Admin.Product
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        protected int pcount;    //总条数
        protected int page;      //当前页
        protected int pagesize;  //设置每页显示的大小
        protected readonly int kindId = (int)Channel.Product;  //类别种类


        protected string property = "";
        protected string keywords = "";
        protected string prolistview = "";

        protected int classId;
        protected int GdClaId;
        protected string ver;
        protected string classList;
        protected BLL.Channel channel = new BLL.Channel();
        protected BLL.Product product = new BLL.Product();
        protected string sqlWhere;
        protected DataSet ds;
        protected int newsclassid = 0;
        protected Model.Channel channelmodel = new Model.Channel();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.PicturePageNum;
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
 
            if (!string.IsNullOrEmpty(Request.Params["keywords"]))
            {
                this.keywords = Request.Params["keywords"].Trim();
            }
            if (!string.IsNullOrEmpty(Request.Params["property"]))
            {
                this.property = Request.Params["property"].Trim();
            }

            if (Request.Cookies["Pro_List_View"] != null)
            {
                this.prolistview = Request.Cookies["Pro_List_View"].Value.ToString();
            }

            if (!Page.IsPostBack)
            {
                this.ddlProperty.Visible = false;

                ChannelTreeBind(this.classId, channelmodel.Title, (int)Channel.Product, this.ddlClassId, "cn");

                this.RptBind("Id>0 and ver='" + Session["ver"].ToString() + "'" + CombSqlTxt(this.kindId, this.newsclassid, this.keywords, this.property), "sortid asc");

                this.ddlClassId.SelectedValue = newsclassid + "";
            }
        }

        public void getImageByClassId()
        {
            try 
            {
                ds = product.getImage(pagesize, page, "Id>0 and ver='" + Session["ver"].ToString() + "' ", "AddTime");

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        #region 数据列表绑定
        private void RptBind(string strWhere, string orderby)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Product bll = new DtCms.BLL.Product();
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

            this.rptList1.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, orderby);
            this.rptList1.DataBind();

            //图表或列表显示
            //switch (this.prolistview)
            //{
            //    case "Txt":
            //        //this.rptList2.Visible = false;
            //        this.rptList1.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, orderby);
            //        this.rptList1.DataBind();
            //        break;
            //    default:
            //        this.rptList1.Visible = false;
            //        //this.rptList2.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, orderby);
            //        //this.rptList2.DataBind();
            //        break;
            //}
        }
        #endregion

        //设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            DtCms.BLL.Product bll = new DtCms.BLL.Product();
            DtCms.Model.Product model = bll.GetModel(id);
            switch (e.CommandName.ToLower())
            {
                case "ibtnmsg":
                    if (model.IsMsg == 1)
                        bll.UpdateField(id, "IsMsg=0");
                    else
                        bll.UpdateField(id, "IsMsg=1");
                    break;
                case "ibtnlock":
                    if (model.IsMsg == 1)
                        bll.UpdateField(id, "IsLock=0");
                    else
                        bll.UpdateField(id, "IsLock=1");
                    break;
                case "ibtntop":
                    if (model.IsTop == 1)
                        bll.UpdateField(id, "IsTop=0");
                    else
                        bll.UpdateField(id, "IsTop=1");
                    break;
                case "ibtnred":
                    if (model.IsRed == 1)
                        bll.UpdateField(id, "IsRed=0");
                    else
                        bll.UpdateField(id, "IsRed=1");
                    break;
                case "ibtnhot":
                    if (model.IsHot == 1)
                        bll.UpdateField(id, "IsHot=0");
                    else
                        bll.UpdateField(id, "IsHot=1");
                    break;
                case "ibtnslide":
                    if (model.IsSlide == 1)
                        bll.UpdateField(id, "IsSlide=0");
                    else
                        bll.UpdateField(id, "IsSlide=1");
                    break;
            }
            this.RptBind("Id>0 and ver='" + Session["ver"].ToString() + "'" + CombSqlTxt(this.kindId, this.newsclassid, this.keywords, this.property), "AddTime desc");
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

        //属性筛选
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(Convert.ToInt32(this.ddlClassId.SelectedValue), this.keywords, this.ddlProperty.SelectedValue) + "page=0&classid=" + this.classId);
        }

        //关健字查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(this.newsclassid, txtKeywords.Text.Trim(), this.property) + "page=0&classid=" + this.classId);
        }
        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            try 
            { 

                for (int i = 0; i < this.rptList1.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((Label)this.rptList1.Items[i].FindControl("lb_id")).Text);
                    CheckBox cb = (CheckBox)this.rptList1.Items[i].FindControl("cb_id");
                    if (cb.Checked)
                    {
                        ExecuteDelete(id);
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }
             
            this.rptList1.DataBind();
            Response.Write("<script type='text/javascript'>alert('删除成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=" + Request.QueryString["page"] + "&classid=" + this.classId + "'</script>");
            Response.End();
        }

        //删除缩略图及相册图片操作
        private void ExecuteDelete(int pid)
        {
            DtCms.BLL.Product bll = new DtCms.BLL.Product();
            DtCms.Model.Product model = bll.GetModel(pid);

            //删除缩略图
            DeleteFile(model.ImgUrl);
            
            //保存日志
            SaveLogs("[图文模块]删除图文：" + model.Title);
            //删除记录
            bll.Delete(pid);
        }

        protected void ibtnViewTxt_Click(object sender, ImageClickEventArgs e)
        {
            //写入Cookes
            Response.Cookies["Pro_List_View"].Value = "Txt";
            Response.Cookies["Pro_List_View"].Expires = DateTime.Now.AddMonths(1);
            Response.Redirect("List.aspx?" + CombUrlTxt(this.classId, this.keywords, this.property) + "page=" + this.page);
        }

        protected void ibtnViewImg_Click(object sender, ImageClickEventArgs e)
        {
            //写入Cookes
            Response.Cookies["Pro_List_View"].Value = "Img";
            Response.Cookies["Pro_List_View"].Expires = DateTime.Now.AddMonths(1);
            Response.Redirect("List.aspx?" + CombUrlTxt(this.classId, this.keywords, this.property) + "page=" + this.page);
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

        protected void lbtnMakeHtml_Click(object sender, EventArgs e)
        {

            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlProductPage(classId);
           
            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='list.aspx?classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void updateInfo_Click(object sender, EventArgs e)
        {

            DtCms.BLL.Product bll = new DtCms.BLL.Product();
            
            for (int i = 0; i < rptList1.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList1.Items[i].FindControl("lb_id")).Text);

                int sortid = Convert.ToInt32(((TextBox)rptList1.Items[i].FindControl("paixuSortId")).Text);

                bll.UpdateField(id, " sortid=" + sortid);

            } 

            this.rptList1.DataBind();
            Response.Write("<script type='text/javascript'>alert('修改成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=" + Request.QueryString["page"] + "&classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Product bll = new DtCms.BLL.Product();
            string allid = string.Empty;
            //批量删除
            for (int i = 0; i < rptList1.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList1.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList1.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    if (!string.IsNullOrEmpty(allid))
                    {

                        allid += "," + id;

                    }
                    else
                    {

                        allid = id + "";

                    }



                }

            }

            if (!string.IsNullOrEmpty(allid))
            {

                CreateHtmlByWeb.ShowAllHtmlCreate.CreateProductSinglePageByWeb(this.classId, allid);

            }

            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=0&classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlProductPage(classId);

            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='list.aspx?classid=" + this.classId + "'</script>");
            Response.End();
        }


    }
}
