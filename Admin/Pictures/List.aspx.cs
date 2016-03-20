using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using DtCms.Web.UI;

namespace DtCms.Web.Admin.Pictures
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;    //总条数
        public int page;      //当前页
        public int pagesize;  //设置每页显示的大小
        public readonly int kindId = (int)Channel.Pictures;  //类别种类

        public int classId;
        public string property = "";
        public string keywords = "";
        public string prolistview = "";


        public int GdClaId;
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

            DataSet ds = channel.GetClassList(this.classId, ver);
//
           // if (ds.Tables[0].Rows.Count > 0)
            //{
               // for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
               // {
                 //   classList = ds.Tables[0].Rows[i]["ClassList"].ToString();
               // }

               // string[] message = classList.Split(',');
                                                                  
              //  this.GdClaId = Convert.ToInt32(message[1]);
            //}

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

                ChannelTreeBind(this.classId, channelmodel.Title, (int)Channel.Pictures, this.ddlClassId, ver);

             
                this.RptBind("Id>0 and ver='" + ver + "'" + CombSqlTxt(this.kindId, this.newsclassid, this.keywords, this.property), "SortId asc,AddTime desc");

                this.ddlClassId.SelectedValue = newsclassid + "";

            
            }
        }

        #region 数据列表绑定
        private void RptBind(string strWhere, string orderby)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Pictures bll = new DtCms.BLL.Pictures();
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

    
        }
        #endregion

        //设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            DtCms.BLL.Pictures bll = new DtCms.BLL.Pictures();
            DtCms.Model.Pictures model = bll.GetModel(id);
            switch (e.CommandName.ToLower())
            {
                case "ibtnmsg":
                    if (model.IsMsg == 1)
                        bll.UpdateField(id, "IsMsg=0");
                    else
                        bll.UpdateField(id, "IsMsg=1");
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
            this.RptBind("Id>0 and ver='" + ver + "'" + CombSqlTxt(this.kindId, this.newsclassid, this.keywords, this.property), "AddTime desc");
        }

        //类别筛选
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                Response.Redirect("List.aspx?" + CombUrlTxt(Convert.ToInt16(this.ddlClassId.SelectedValue), this.keywords, this.property) + "page=0&classid=" + this.classId);
            }
            else
            {
                Response.Redirect("List.aspx?" + CombUrlTxt(Convert.ToInt16(this.ddlClassId.SelectedValue), this.keywords, this.property) + "page=0&classid=" + this.classId);
            }
        }

        //属性筛选
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(Convert.ToInt32(this.ddlProperty.SelectedValue), this.keywords, this.ddlProperty.SelectedValue) + "page=0&classid=" + this.classId);
        }

        //关健字查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(this.newsclassid, txtKeywords.Text.Trim(), this.property) + "page=0&classid=" + this.classId);
        }
        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
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

            this.rptList1.DataBind();
            Response.Write("<script type='text/javascript'>alert('删除成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=0&classid=" + this.classId + "'</script>");
            Response.End();

        }

        //删除缩略图及相册图片操作
        private void ExecuteDelete(int pid)
        {
            DtCms.BLL.Pictures bll = new DtCms.BLL.Pictures();
            DtCms.Model.Pictures model = bll.GetModel(pid);

            //删除缩略图
            DeleteFile(model.ImgUrl);
            //删除相册图片
            if (model.PicturesAlbums != null)
            {
                foreach (DtCms.Model.PicturesAlbum amodel in model.PicturesAlbums)
                {
                    DeleteFile(amodel.ImgUrl);
                }
            }
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
            Response.Redirect("List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=" + this.page);
        }

        protected void ibtnViewImg_Click(object sender, ImageClickEventArgs e)
        {
            //写入Cookes
            Response.Cookies["Pro_List_View"].Value = "Img";
            Response.Cookies["Pro_List_View"].Expires = DateTime.Now.AddMonths(1);
            Response.Redirect("List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=" + this.page);
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
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlPicturesPage(classId);
            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='list.aspx?classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void updateAll_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Pictures bll = new DtCms.BLL.Pictures();

            for (int i = 0; i < rptList1.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList1.Items[i].FindControl("lb_id")).Text);

                int sortid = Convert.ToInt32(((TextBox)rptList1.Items[i].FindControl("paixuSortId")).Text);

                bll.UpdateField(id, " sortid=" + sortid);

            }


            this.rptList1.DataBind();
            Response.Write("<script type='text/javascript'>alert('修改成功');location.href='List.aspx?page=0&classid=" + this.classId + "'</script>");
            Response.End();
        }
    }
}
