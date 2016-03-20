using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
namespace DtCms.Web.Admin.IndexChanner
{
    public partial class Edit : System.Web.UI.Page
    {
        protected string allinfo = string.Empty;
        protected int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {

                id = DtCms.Common.Utils.returnIntByString(Request.QueryString["id"]);

            }

            if (!IsPostBack)
            {
               

                if (!string.IsNullOrEmpty(Request.QueryString["showallinfo"])) 
                {

                    string showstring = "";

                    if (id == 1)
                    {
                        showstring = "IsRecruitment";


                    }
                    else if (id == 2) 
                    {

                        showstring = "IsDownload";
                    
                    }
                    else if (id == 3)
                    {

                        showstring = "IsLink";

                    }
                    else if (id == 4)
                    {

                        showstring = "IsBanner";

                    }


                    string showallinfo = Request.QueryString["showallinfo"];

                    string[] showinfo = showallinfo.Split('*');



                    foreach (string nowinfo in showinfo) 
                    {

                        string[] info = nowinfo.Split(',');

                        if (DtCms.Common.Utils.returnIntByString(info[0]) > 0)
                        {
                            if(info[1].Equals("1")){

                            new BLL.Channel().UpdateField(DtCms.Common.Utils.returnIntByString(info[0]),showstring+"=1");

                            }else
                            {

                                new BLL.Channel().UpdateField(DtCms.Common.Utils.returnIntByString(info[0]), showstring + "=0");

                            }
                        }
                    }
                    Response.Write("<script type='text/javascript'>alert('恭喜您，提交成功');location.href='edit.aspx?id=" + id + "'</script>");
                    Response.End();
                }
                
                returnallinfo();

            }
        }
        protected void returnallinfo()
        {

            StringBuilder bur = new StringBuilder();

            Model.Channel channel = new Model.Channel();

            BLL.Channel bll = new BLL.Channel();

            channel.ParentId = 0;

            channel.Ver = Session["ver"].ToString();

            DataSet ds = bll.SelectModule_fid(channel);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    channel = new Model.Channel();

                    channel.Ver = Session["ver"].ToString();

                    channel.ParentId = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);


                    DataSet ds1 = new BLL.Channel().SelectModule_fid(channel);

                    bur.AppendFormat("<tr><td align=\"center\">{0}</td>", "");

                    bur.AppendFormat("<td><font color=\"#ff0000\">{0}</font></td>", ds.Tables[0].Rows[i]["Title"].ToString());

                    bur.AppendFormat("</tr>");



                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {

                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            int showtypeid = DtCms.Common.Utils.returnIntByString(ds1.Tables[0].Rows[j]["PageType"].ToString());


                            if (showtypeid == 2 || showtypeid == 3 || showtypeid == 4 || showtypeid == 5 || showtypeid == 6)
                            {

                                string showcheack = string.Empty;

                                if (id == 1 && DtCms.Common.Utils.returnIntByString(ds1.Tables[0].Rows[j]["IsRecruitment"].ToString()) == 1) 
                                {
                                
                                    showcheack ="checked=\"checked\"";
                                
                                }

                                if (id == 2 && DtCms.Common.Utils.returnIntByString(ds1.Tables[0].Rows[j]["IsDownload"].ToString()) == 1)
                                {

                                    showcheack = "checked=\"checked\"";

                                }
                                if (id == 3 && DtCms.Common.Utils.returnIntByString(ds1.Tables[0].Rows[j]["IsLink"].ToString()) == 1)
                                {

                                    showcheack = "checked=\"checked\"";

                                }
                                if (id ==4 && DtCms.Common.Utils.returnIntByString(ds1.Tables[0].Rows[j]["IsBanner"].ToString()) == 1)
                                {

                                    showcheack = "checked=\"checked\"";

                                }

                                bur.AppendFormat("<tr><td align=\"center\"><input {1}  type=\"checkbox\" class=\"showid\" value=\"{0}\"></td>", ds1.Tables[0].Rows[j]["id"].ToString(), showcheack);

                                bur.AppendFormat("<td>{0}</td>", ds1.Tables[0].Rows[j]["Title"].ToString());

                                bur.AppendFormat("</tr>");





                            }
                        }
                    }

                }
            }

            allinfo = bur.ToString();

        }
    }
}