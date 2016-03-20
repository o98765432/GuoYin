using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web
{
    public partial class Message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!IsPostBack)
                {
                    string oldpath = Request.UrlReferrer.ToString();

                    if (string.IsNullOrEmpty(Request.Form["code"]))
                    {

                        Response.Write("<script type='text/javascript'>history.go(-1);</script>");
                        Response.End();


                    }

                    if (!Request.Form["code"].Equals(Session["CheckCode"]))
                    {

                        Response.Write("<script type='text/javascript'>alert('对不起，验证码不正确');history.go(-1);</script>");
                        Response.End();

                    }


                    if (string.IsNullOrEmpty(Request.Form["subject"]) || string.IsNullOrEmpty(Request.Form["username"]) || string.IsNullOrEmpty(Request.Form["xueli"]) || string.IsNullOrEmpty(Request.Form["school"]) || string.IsNullOrEmpty(Request.Form["contact"]) || string.IsNullOrEmpty(Request.Form["address"]) || string.IsNullOrEmpty(Request.Form["filepath"]))
                    {

                        Response.Write("<script type='text/javascript'>history.go(-1);</script>");
                        Response.End();

                    }

                    if (Request.Form["filepath"].IndexOf(".doc") == -1 && Request.Form["filepath"].IndexOf(".docx") == -1)
                    {

                        Response.Write("<script type='text/javascript'>history.go(-1);</script>");
                        Response.End();
                    
                    }

                    DataSet showda = new BLL.Chenadd().GetList(" typeid=6 and classid=93");

                    List<string> strname = new List<string>();

                    List<string> strvalue = new List<string>();



                    if (showda != null && showda.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < showda.Tables[0].Rows.Count; i++)
                        {
                            strname.Add(showda.Tables[0].Rows[i]["ziduan"].ToString());

                            strvalue.Add(Request.Form[showda.Tables[0].Rows[i]["ziduan"].ToString()]);


                        }

                    }

                    new BLL.Feedback().Add(strname, strvalue, Request.Form["chentypeid"]);

                    Response.Write("<script type='text/javascript'>alert('恭喜您，提交成功！');location.href='" + oldpath + "'</script>");
                    Response.End();



                }


            }
        }
    }
}