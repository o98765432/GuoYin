using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DtCms.Web.UI;

namespace DtCms.Web.Admin
{
    public partial class admin_center : ManagePage
    {
        protected DtCms.Model.ShowWebSet showwebset = new Model.ShowWebSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                showwebset = new BLL.ShowWebSet().GetModel(Session["ver"].ToString());

            }

        }

        protected void lbtnMakeHtml_Click(object sender, EventArgs e)
        {
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPage("Cultrue", "/index.html");

            DataSet ds = new BLL.Channel().GetChannelRptBind(" kindid=1 and parentid=0");

            if (ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPageInfo("jieInfo", "/index" + ds.Tables[0].Rows[i]["id"].ToString() + ".html", DtCms.Common.Utils.returnIntByString(ds.Tables[0].Rows[i]["id"].ToString()));
                    
                    }
                }
            }

            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlChannerList(0);
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlChannerList(113);
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPage("", "/index.html");
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPage("Search", "/index.html");
            //CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPage("WebMap", "/index.html"); 
            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='Admin_Center.aspx'</script>");
            Response.End();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPage("", "/index.html");
            //CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPage("WebMap", "/index.html");
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPage("Search", "/index.html"); 

            //CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPage("Cultrue", "/index.html");

            DataSet ds = new BLL.Channel().GetChannelRptBind(" kindid=1 and parentid=0");

            if (ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPageInfo("jieInfo", "/index" + ds.Tables[0].Rows[i]["id"].ToString() + ".html", DtCms.Common.Utils.returnIntByString(ds.Tables[0].Rows[i]["id"].ToString()));

                    }
                }
            }

            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlChannerList(0);
            CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlChannerList(113);
            DataSet da = new BLL.Channel().GetChannelListByClassId(0, Session["ver"].ToString(), " SortId asc,Id desc ");

            if (da != null && da.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < da.Tables[0].Rows.Count; i++)
                {

                    DataSet da1 = new BLL.Channel().GetChannelListByClassId(DtCms.Common.Utils.returnIntByString(da.Tables[0].Rows[i]["id"].ToString()), Session["ver"].ToString(), " SortId asc,Id desc ");


                    if (da1 != null && da1.Tables[0].Rows.Count > 0)
                    {

                        for (int j = 0; j < da1.Tables[0].Rows.Count; j++)
                        {


                            int showtypeid = DtCms.Common.Utils.returnIntByString(da1.Tables[0].Rows[j]["PageType"].ToString());


                            if (showtypeid == 2)
                            {

                                int classId = DtCms.Common.Utils.returnIntByString(da1.Tables[0].Rows[j]["id"].ToString());


                               CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlNewsPage(classId);

                            }
                            else if (showtypeid == 3)
                            {
                                CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlSinglePage(DtCms.Common.Utils.returnIntByString(da1.Tables[0].Rows[j]["id"].ToString()));



                            }
                            else if (showtypeid == 13)
                            {
                                CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlSinglePage(DtCms.Common.Utils.returnIntByString(da1.Tables[0].Rows[j]["id"].ToString()));



                            }
                            else if (showtypeid == 23)
                            {
                                CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlSinglePage(DtCms.Common.Utils.returnIntByString(da1.Tables[0].Rows[j]["id"].ToString()));



                            }
                            else if (showtypeid == 4)
                            {
                                CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlProductPage(DtCms.Common.Utils.returnIntByString(da1.Tables[0].Rows[j]["id"].ToString()));


                            }
                            else if (showtypeid == 5)
                            {

                                CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlHrPage(DtCms.Common.Utils.returnIntByString(da1.Tables[0].Rows[j]["id"].ToString()));

                            }
                            else if (showtypeid == 6)
                            {

                                CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlDownLoadPage(DtCms.Common.Utils.returnIntByString(da1.Tables[0].Rows[j]["id"].ToString()));

                            }
                            //else if (showtypeid == 8)
                            //{

                            //    CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlIndexPage(da1.Tables[0].Rows[j]["webpath"].ToString() + "/", "index.html");

                            ////}
                            //else if (showtypeid == 12)
                            //{

                            //    CreateHtmlByWeb.ShowAllHtmlCreate.CreateHtmlVideoPage(DtCms.Common.Utils.returnIntByString(da1.Tables[0].Rows[j]["id"].ToString()));

                            //}


                        }

                    }

                }


            }


            Response.Write("<script type='text/javascript'>alert('生成成功');location.href='Admin_Center.aspx'</script>");
            Response.End();

        }
    }
}
