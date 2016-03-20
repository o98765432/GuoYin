using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DtCms.Web.UI;
using System.Text;

namespace DtCms.Web.Admin
{
    public partial class Left : ManagePage
    { 

        protected string allmenuinfo = string.Empty;
        Model.Channel model_channel = new Model.Channel();
        BLL.Channel bll_channel = new BLL.Channel();


        protected string banben = "";
        protected string ver
        {
            get
            {
                if(_ver==string.Empty)
                {
                    _ver = Session["ver"].ToString();
                }
                return _ver;
            }
        }
        protected string _ver=string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                switch (ver)
                {
                    case "cn":
                        banben = "中文版";
                        break;
                    case "cn-tw":
                        banben = "繁体中文版";
                        break;
                    case "en":
                        banben = "英文版";
                        break;
                    case "jap":
                        banben = "日文版";
                        break;
                    case "ydl":
                        banben = "意大利版";
                        break;
                    case "xby":
                        banben = "西班牙版";
                        break;
                    case "ru":
                        banben = "俄文版";
                        break;
                    case "ko":
                        banben = "韩文版";
                        break;
                    default:
                        break;
                }
            }

        }
        protected void rptLay1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Repeater rptLay2 = (System.Web.UI.WebControls.Repeater)e.Item.FindControl("rptLay2");


                //找到分类Repeater关联的数据项 
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                //提取分类ID 
                int typeid = Convert.ToInt32(rowv["id"]);
                //根据分类ID查询该分类下的

                model_channel.ParentId = typeid;
                model_channel.Ver = ver;
                DataSet ds = bll_channel.SelectModule_fid(model_channel);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rptLay2.DataSource = ds;
                        rptLay2.DataBind();
                    }
                }
            }
        }



        protected void rptLay2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Repeater rptLay3 = (System.Web.UI.WebControls.Repeater)e.Item.FindControl("rptLay3");
                DataRowView drv = e.Item.DataItem as DataRowView;
                DataRow dr = drv.Row;
                int id = int.Parse(dr["id"].ToString());//263|262|261


                string where = " ParentId=" + id + " and ver='" + ver + "' ";//
                DataSet ds2 = bll_channel.SelectModule_fid(model_channel);
                if (ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        rptLay3.DataSource = ds2;
                        rptLay3.DataBind();
                    }
                }
            }
        }
       
      public string load(int nowkindid)
        {
            Model.Channel channel = new Model.Channel();
            BLL.Channel bll = new BLL.Channel();


            StringBuilder bur = new StringBuilder();
            channel.ParentId = 0;
            channel.Ver =ver;

            DataSet ds = bll.GetChannelRptBind(" kindid=" + nowkindid + " and parentid=0");
            if (ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        if (returnByTrueInfo(Session["AdminLevel"].ToString(), ",select" + ds.Tables[0].Rows[i]["id"] + ",") || (Session["AdminNo"].ToString().Equals("7")))
                        {

                            bur.AppendFormat("<div class=\"menu\">\n<div class=\"menuTitle\">\n<ul>");

                            bur.AppendFormat("<li class=\"title\">" + ds.Tables[0].Rows[i]["Title"] + "</li><li class=\"right\"><img src=\"images/menu_title_down.gif\" border=\"0\" /></li>\n</ul>\n");


                            bur.AppendFormat("</div>\n<div class=\"subMenu\"><ul>\n");

                            channel = new Model.Channel();

                            channel.Ver = ver;

                            channel.ParentId = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);


                            DataSet ds1 = new BLL.Channel().SelectModule_fid(channel);


                            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                            {

                                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                                {
                                    if (returnByTrueInfo(Session["AdminLevel"].ToString(), ",select" + ds1.Tables[0].Rows[j]["id"] + ",") || (Session["AdminNo"].ToString().Equals("7")))
                                    {
                                        string webpathinfo = "";

                                        int showtypeid = DtCms.Common.Utils.returnIntByString(ds1.Tables[0].Rows[j]["PageType"].ToString());

                                        if (showtypeid == 2)
                                        {

                                            webpathinfo = "Article/List.aspx";

                                        }
                                        else if (showtypeid == 3)
                                        {

                                            webpathinfo = "SinglePage/List.aspx";

                                        }
                                        else if (showtypeid == 4)
                                        {

                                            webpathinfo = "Product/List.aspx";

                                        }
                                        else if (showtypeid == 5)
                                        {

                                            webpathinfo = "job/List.aspx";

                                        }
                                        else if (showtypeid == 6)
                                        {

                                            webpathinfo = "Downloads/List.aspx";

                                        }
                                        else if (showtypeid == 7)
                                        {

                                            webpathinfo = "Pictures/List.aspx";

                                        }
                                        else if (showtypeid == 8)
                                        {

                                            webpathinfo = "Feedback/List.aspx";

                                        }
                                        else if (showtypeid == 9)
                                        {

                                            webpathinfo = "Banner/List.aspx";

                                        }
                                        else if (showtypeid == 10)
                                        {

                                            webpathinfo = "Article/List.aspx";

                                        }
                                        else if (showtypeid == 11)
                                        {

                                            webpathinfo = "Article/List.aspx";

                                        }
                                        else if (showtypeid == 12)
                                        {

                                            webpathinfo = "Video/List.aspx";

                                        }
                                        else if (showtypeid == 13)
                                        {

                                            webpathinfo = "SinglePage/edit.aspx";

                                        }
                                        else if (showtypeid == 14)
                                        {

                                            webpathinfo = "Member/List.aspx";

                                        }
                                        else if (showtypeid == 20)
                                        {

                                            webpathinfo = ds1.Tables[0].Rows[j]["PageUrl"].ToString();

                                        }


                                        bur.AppendFormat("<li><a href=\"{0}\" target=\"sysMain\">{1}</a> </li>\n", webpathinfo + "?Classid=" + ds1.Tables[0].Rows[j]["Id"].ToString() + "&" + "showmatypeid=" + ds1.Tables[0].Rows[j]["id"], ds1.Tables[0].Rows[j]["Title"]);



                                    }
                                }

                            }
                            if (Session["AdminNo"].ToString().Equals("7") && (i + 1) == ds.Tables[0].Rows.Count && nowkindid==0)
                            {
                                bur.AppendFormat("<li><a href=\"{0}\" target=\"sysMain\">{1}</a> </li>\n", "ShowChanner/List.aspx?showkindid=1&kindId=0", "孔子文化节");

                            }
                            bur.AppendFormat("</ul></div></div>");
                        }
                    }



                }
            }
            return bur.ToString();
        }


    }
}