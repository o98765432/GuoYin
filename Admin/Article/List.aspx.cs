using System;
using System.IO;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using DtCms.Web.UI;
using CreateHtmlByWeb;

namespace DtCms.Web.Admin.Article
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小
        public readonly int kindId = (int)Channel.Article;                      //类别种类

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
            this.pagesize = webset.PicturePageNum;

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

            if (classId == 99)
            {
                classId = 100;
                newsclassid = 100;
            }

            channelmodel = channel.GetModel(this.classId);


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
                this.ddlProperty.Visible = false;

                ChannelTreeBind(this.classId, channelmodel.Title, (int)Channel.Article, this.ddlClassId, ver);


                this.RptBind("Id>0 and ver='" + ver + "'" + CombSqlTxt(this.kindId, this.newsclassid, this.keywords, this.property), "SortId asc,AddTime desc,id desc");

                this.ddlClassId.SelectedValue = newsclassid + "";

            }

            if (classId == 160)
            {
                Button1.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
            }

        }

        #region 数据列表绑定
        private void RptBind(string strWhere, string orderby)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Article bll = new DtCms.BLL.Article();
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
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            DtCms.BLL.Article bll = new DtCms.BLL.Article();
            DtCms.Model.Article model = bll.GetModel(id);
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
            RptBind("Id>0 and ver='" + ver + "' " + CombSqlTxt(this.kindId, this.classId, this.keywords, this.property), "AddTime desc");
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

            DtCms.BLL.Article bll = new DtCms.BLL.Article();
            DtCms.Model.Article model;
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
                    //保存日志
                    SaveLogs("[资讯模块]删除文章：" + model.Title);
                    //删除记录
                    bll.Delete(kindId, id, ver);
                }
            }


            this.rptList.DataBind();
            Response.Write("<script type='text/javascript'>alert('删除成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=" + Request.QueryString["page"] + "&classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void lbtnMakeHtml_Click(object sender, EventArgs e)
        {

            CreateHtmlNewsPage(classId);
            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='list.aspx?classid=" + this.classId + "'</script>");
            Response.End();
        }

        public static void CreateHtmlNewsPage(int typeid)
        {

            //生成详细网页

            CreateNewsSinglePageByWeb(typeid, "");
            ShowAllHtmlCreate.CreateHtmlNewsList(typeid);

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

        protected void updateInfo_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Article bll = new DtCms.BLL.Article();

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);

                int sortid = Convert.ToInt32(((TextBox)rptList.Items[i].FindControl("paixuSortId")).Text);

                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("IsLock");

                int islock;

                if (cb.Checked)
                    islock = 1;
                else
                    islock = 0;

                bll.UpdateField(id, " sortid=" + sortid + ",islock=" + islock);

            }

            this.rptList.DataBind();
            Response.Write("<script type='text/javascript'>alert('修改成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=" + Request.QueryString["page"] + "&classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

           ShowAllHtmlCreate.CreateHtmlNewsList(classId);
            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='list.aspx?classid=" + this.classId + "'</script>");
            Response.End();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Article bll = new DtCms.BLL.Article();
            string allid = string.Empty;
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
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

                CreateNewsSinglePageByWeb(this.classId, allid);

            }

            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='List.aspx?" + CombUrlTxt(this.newsclassid, this.keywords, this.property) + "page=0&classid=" + this.classId + "'</script>");
            Response.End();
        }

        public static void CreateNewsSinglePageByWeb(int typeid, string allidinfo)
        {


            DtCms.Model.Channel channel = new DtCms.BLL.Channel().GetModel(typeid);

            if (channel != null)
            {

                var showver= HttpContext.Current.Session["ver"] != null ? (HttpContext.Current.Session["ver"].ToString().Equals("cn") ? "" : (HttpContext.Current.Session["ver"].ToString()) + "/") : "";

                string allid = new DtCms.BLL.Channel().returnAllStringTop(typeid, HttpContext.Current.Session["ver"].ToString());

                string tempUrl = "/Admin/Template/" + showver + channel.WebPath + "/news.html";


                string html = FileManager.ReadFile(HttpContext.Current.Server.MapPath(tempUrl));


                html = ShowAllHtmlCreate.returnAllProductInfo(html, "1,2,3,4,5");

                html = new TemplateHtm().ReplaceListTag(html);


                string strwhere = "  IsLock=1 and ver='" + HttpContext.Current.Session["ver"].ToString() + "' and ClassId in(" + new DtCms.BLL.Channel().returnAllStringTop(typeid, HttpContext.Current.Session["ver"].ToString()) + ")";


                if (!string.IsNullOrEmpty(allidinfo))
                {

                    strwhere = "  IsLock=1  and id in(" + allidinfo + ") ";

                }
                if (html == null)
                {
                    return;
                }

                DataSet ds = new DtCms.BLL.Article().GetList(strwhere);


                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    string showver1 = HttpContext.Current.Session["ver"] != null ? (HttpContext.Current.Session["ver"].ToString().Equals("cn") ? "" : (HttpContext.Current.Session["ver"].ToString())) : "";

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string htmlpath = "/" + showver1 + "/" + channel.WebPath + "/" + ds.Tables[0].Rows[i]["id"] + ".html";

                        if (ds.Tables[0].Rows[i]["htmlpath"] != null && (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["htmlpath"].ToString())))
                        {

                            var classID = ds.Tables[0].Rows[i]["ClassId"].ToString();
                            if (classID == "85" || classID == "86" || classID == "87" || classID == "88" || classID == "89")
                            {
                                htmlpath = ds.Tables[0].Rows[i]["htmlpath"].ToString();
                            }
                            else
                            {
                                htmlpath = "/" + showver1 + ds.Tables[0].Rows[i]["htmlpath"].ToString();
                            }

                        }
                        else
                        {

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~" + channel.WebPath)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + channel.WebPath));
                            }

                            htmlpath = "/" + showver1 + "/" + channel.WebPath + "/" + ds.Tables[0].Rows[i]["id"] + ".html";


                            new DtCms.BLL.Article().UpdateField(Convert.ToInt32(ds.Tables[0].Rows[i]["id"]), " htmlpath='" + htmlpath + "'");

                        }


                        string newshtml = html;



                        DtCms.Model.Channel channerl = new DtCms.BLL.Channel().GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["classid"]));

                        if (channerl != null)
                        {


                            newshtml = newshtml.Replace("@ChannerTitle@", channerl.Title + "");

                            newshtml = newshtml.Replace("@ChannerId@", channerl.Id + "");

                            newshtml = newshtml.Replace("@ChannerImgUrl@", channerl.ImgUrl + "");

                            DtCms.Model.Channel channerl2 = new DtCms.BLL.Channel().GetModel(channerl.ParentId);

                            if (channerl2.ParentId != 0)
                            {

                                newshtml = newshtml.Replace("@ChannerPrTitle@", channerl2.Title);

                                newshtml = newshtml.Replace("@ChannerPrId@", channerl2.Id + "");

                            }
                        }

                        newshtml = newshtml.Replace("@ChannerTitle@", "");

                        newshtml = newshtml.Replace("@ChannerId@", "");


                        newshtml = newshtml.Replace("@ChannerPrTitle@", "");

                        newshtml = newshtml.Replace("@ChannerPrId@", "");



                        newshtml = newshtml.Replace("@Title@", ds.Tables[0].Rows[i]["Title"] != null ? ds.Tables[0].Rows[i]["Title"].ToString() : "");

                        newshtml = newshtml.Replace("@id@", ds.Tables[0].Rows[i]["id"] != null ? ds.Tables[0].Rows[i]["id"].ToString() : "");


                        newshtml = newshtml.Replace("@ContentInfo@", ds.Tables[0].Rows[i]["content"] != null ? ds.Tables[0].Rows[i]["content"].ToString() : "");

                        if (ds.Tables[0].Rows[i]["Keyword"] != null && !string.IsNullOrEmpty(ds.Tables[0].Rows[i]["Keyword"].ToString()))
                        {

                            newshtml = newshtml.Replace("@SeoKeyWords@", ds.Tables[0].Rows[i]["Keyword"] != null ? ds.Tables[0].Rows[i]["Keyword"].ToString() : "");

                        }

                        newshtml = newshtml.Replace("@ImgUrl@", ds.Tables[0].Rows[i]["ImgUrl"] != null ? ds.Tables[0].Rows[i]["ImgUrl"].ToString() : "");

                        newshtml = newshtml.Replace("@ImgUrl2@", ds.Tables[0].Rows[i]["BigImgUrl"] != null ? ds.Tables[0].Rows[i]["BigImgUrl"].ToString() : "");

                        if (ds.Tables[0].Rows[i]["Description"] != null && !string.IsNullOrEmpty(ds.Tables[0].Rows[i]["Description"].ToString()))
                        {


                            newshtml = newshtml.Replace("@SeoDes@", ds.Tables[0].Rows[i]["Description"] != null ? ds.Tables[0].Rows[i]["Description"].ToString() : "");

                        }
                        newshtml = newshtml.Replace("@Herf@", ds.Tables[0].Rows[i]["Herf"] != null ? ds.Tables[0].Rows[i]["Herf"].ToString() : "");

                        if (ds.Tables[0].Rows[i]["Form"] != null && !string.IsNullOrEmpty(ds.Tables[0].Rows[i]["Form"].ToString()))
                        {


                            newshtml = newshtml.Replace("@SeoTitle@", ds.Tables[0].Rows[i]["Form"] != null ? ds.Tables[0].Rows[i]["Form"].ToString() : "");

                        }

                        newshtml = newshtml.Replace("@AddTime@", ds.Tables[0].Rows[i]["AddTime"] != null ? DateTime.Parse(ds.Tables[0].Rows[i]["AddTime"].ToString()).ToString("yyyy-MM-dd") : "");

                        newshtml = newshtml.Replace("@filepath@", ds.Tables[0].Rows[i]["filepath"] != null ? ds.Tables[0].Rows[i]["filepath"].ToString() : "");

                        newshtml = newshtml.Replace("@Download@", ds.Tables[0].Rows[i]["Download"] != null ? ds.Tables[0].Rows[i]["Download"].ToString() : "");

                        newshtml = newshtml.Replace("@Form@", ds.Tables[0].Rows[i]["IndexImgUrl"] != null ? ds.Tables[0].Rows[i]["IndexImgUrl"].ToString() : "");

                        newshtml = newshtml.Replace("@SubTitle@", ds.Tables[0].Rows[i]["SubTitle"] != null ? ds.Tables[0].Rows[i]["SubTitle"].ToString() : "");

                        newshtml = newshtml.Replace("@htmlpath@", ds.Tables[0].Rows[i]["htmlpath"] != null ? ds.Tables[0].Rows[i]["htmlpath"].ToString() : "");

                        newshtml = newshtml.Replace("@Daodu@", ds.Tables[0].Rows[i]["Daodu"] != null ? ds.Tables[0].Rows[i]["Daodu"].ToString() : "");

                        newshtml = newshtml.Replace("@Author@", ds.Tables[0].Rows[i]["Author"] != null ? ds.Tables[0].Rows[i]["Author"].ToString() : "");

                        newshtml = newshtml.Replace("@UpdateTime@", ds.Tables[0].Rows[i]["AddTime"] != null ? (ShowAllHtmlCreate.returnEnDate(DateTime.Parse(ds.Tables[0].Rows[i]["AddTime"].ToString()).ToString("yyyy.MM"))) : "");

                        newshtml = newshtml.Replace("@ContentInfo2@", ds.Tables[0].Rows[i]["content"].ToString() != null ? DtCms.Common.Utils.CutString(ds.Tables[0].Rows[i]["content"].ToString(), 400) : "");

                        newshtml = newshtml.Replace("@IndexImgUrl@", ds.Tables[0].Rows[i]["IndexImgUrl"] != null ? ds.Tables[0].Rows[i]["IndexImgUrl"].ToString() : "");

                        newshtml = newshtml.Replace("@ImgUrl5@", ds.Tables[0].Rows[i]["ImgUrl5"] != null ? ds.Tables[0].Rows[i]["ImgUrl5"].ToString() : "");


                        if (ds.Tables[0].Rows[i]["Editor"] != null)
                        {

                            string allimg = "";

                            string[] txtimg = ds.Tables[0].Rows[i]["Editor"].ToString().Split(',');

                            foreach (string imginfo in txtimg)
                            {

                                allimg += "<li><img  src=\"/minimg" + imginfo + "\" title=\"" + ds.Tables[0].Rows[i]["Title"] + "\" rel=\"" + imginfo + "\"/></li> ";

                            }
                            newshtml = newshtml.Replace("@allimg@", allimg);

                        }



                        for (int g = 0; g < ds.Tables[0].Columns.Count; g++)
                        {


                            newshtml = newshtml.Replace("@" + ds.Tables[0].Columns[g] + "@", ds.Tables[0].Rows[i]["" + ds.Tables[0].Columns[g] + ""] != null ? ds.Tables[0].Rows[i]["" + ds.Tables[0].Columns[g] + ""].ToString() : "");


                        }

                        FileManager.WriteFile(HttpContext.Current.Server.MapPath("~" + htmlpath), newshtml);

                    }

                }

            }

        }

    }
}
