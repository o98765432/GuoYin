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
using DtCms.Common;
using DtCms.Web.UI;

namespace DtCms.Web.Admin
{
    public partial class login : System.Web.UI.Page
    {
        protected string kaifaname = ConfigurationManager.ConnectionStrings["kaifaname"] != null ? ConfigurationManager.ConnectionStrings["kaifaname"].ConnectionString : "";
        protected string kaifapassword = ConfigurationManager.ConnectionStrings["kaifapassword"] != null ? ConfigurationManager.ConnectionStrings["kaifapassword"].ConnectionString : "";

        DtCms.BLL.Administrator bll = new DtCms.BLL.Administrator();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 
            }
        }

        protected void loginsubmit_Click(object sender, EventArgs e)
        {
            if (tboxValidator.Text.Trim().CompareTo(Session["CheckCode"]) != 0)
            {
                lblValidator.Text = "验证码错误";
                this.txtUserName.Text = "";
                this.txtUserPwd.Text = "";
            }
            else
            {
                string UserName = txtUserName.Text.Trim();
                string UserPwd = txtUserPwd.Text.Trim();
                if (UserName.Equals("") || UserPwd.Equals(""))
                {
                    lbMsg.Text = "请输入您要登录用户名或密码";
                    Response.Write("<script type='text/javascript'>alert('请输入您要登录用户名或密码');history.go(-1);</script>");
                    Response.End();
                }
                else
                {
                    if (Session["AdminLoginSun"] == null)
                    {
                        Session["AdminLoginSun"] = 1;
                    }
                    else
                    {
                        Session["AdminLoginSun"] = Convert.ToInt32(Session["AdminLoginSun"]) + 1;
                    }
                    var db = new DBUtility.CommandInfo();

                    //判断登录
                    if (Session["AdminLoginSun"] != null && Convert.ToInt32(Session["AdminLoginSun"]) > 3)
                    {
                        lbMsg.Text = "登录错误超过3次，请关闭浏览器重新登录。";
                        Response.Write("<script type='text/javascript'>alert('登录错误超过3次，请关闭浏览器重新登录。');history.go(-1);</script>");
                        Response.End();
                    }
                    else if (bll.chkAdminLogin(UserName, DESEncrypt.Encrypt(UserPwd)))
                    {
                        DtCms.Model.Administrator model = new DtCms.Model.Administrator();
                        model = bll.GetModel(UserName);
                        Session["AdminNo"] = model.Id;
                        Session["AdminName"] = model.UserName;
                        Session["AdminType"] = model.UserType;
                        Session["AdminLevel"] = model.UserLevel;
                         
                        //设置超时时间
                        Session.Timeout = 45;
                        Session["AdminLoginSun"] = null;

                        //string ver = "";
                        //if (RadioButton1.Checked)
                        //{
                        //    ver = "cn";
                        //}
                        //else
                        //{
                        //    ver = "hk";
                        //}
                        var ver = RadioButtonList1.SelectedValue;
                        Session["ver"] = ver;
                         
                        //写入Cookies
                        Utils.WriteCookie("AdminName", "DtCms", DESEncrypt.Encrypt(model.UserName));
                        Utils.WriteCookie("AdminPwd", "DtCms", model.UserPwd);
                        Utils.WriteCookie("AdminVer", "DtCms", ver);
                        //保存日志
                        new DtCms.Web.UI.ManagePage().SaveLogs(UserName, "[用户登录]状态：登录成功！");

                        Response.Redirect("admin_index.aspx");
                    }
                    else
                    {
                        lbMsg.Text = "您输入的用户名或密码不正确";
                        Response.Write("<script type='text/javascript'>alert('您输入的用户名或密码不正确');history.go(-1);</script>");
                        Response.End();
                        //保存日志
                        new DtCms.Web.UI.ManagePage().SaveLogs(UserName, "[用户登录] 状态：登录失败！");
                    }
                }
            }
        }




    }
}
