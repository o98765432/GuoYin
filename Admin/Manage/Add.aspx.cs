using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace DtCms.Web.Admin.Manage
{
    public partial class add : DtCms.Web.UI.ManagePage
    {
        //权限信息
        protected string allflag = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               // this.btnSave.Visible = addflag;

                Model.Channel channel = new Model.Channel();

                channel.Ver = Session["ver"].ToString();

                channel.ParentId = 0;

                DataSet ds = new BLL.Channel().SelectModule_fid(channel);

                StringBuilder bur = new StringBuilder();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {


                        bur.AppendFormat("<tr>\n<td width=\"25%\" align=\"right\" valign=\"top\"><font color=\"red\">|——" + ds.Tables[0].Rows[i]["Title"] + "：</font></td>\n");

                        bur.AppendFormat("<td width=\"75%\">");

                        bur.AppendFormat("<input class=\"selectclass\" type=\"checkbox\" value=\"select" + ds.Tables[0].Rows[i]["id"] + "\" />查看");

                        bur.AppendFormat("<input class=\"addclass\" type=\"checkbox\" value=\"add" + ds.Tables[0].Rows[i]["id"] + "\" />添加");

                        bur.AppendFormat("<input class=\"updateclass\" type=\"checkbox\" value=\"update" + ds.Tables[0].Rows[i]["id"] + "\" />修改");

                        bur.AppendFormat("<input class=\"deleteclass\" type=\"checkbox\" value=\"delete" + ds.Tables[0].Rows[i]["id"] + "\" />删除");

                        bur.AppendFormat("<input class=\"smhclass\" " + (ds.Tables[0].Rows[i]["FileUrl"].ToString().IndexOf("Article") == -1 ? "style=\"display:none\"" : "") + " type=\"checkbox\" value=\"smh" + ds.Tables[0].Rows[i]["id"] + "\" />" + (ds.Tables[0].Rows[i]["FileUrl"].ToString().IndexOf("Article") == -1 ? "" : "审核") + "");

                        bur.AppendFormat("</td></tr>");


                        channel = new Model.Channel();

                        channel.Ver = Session["ver"].ToString();

                        channel.ParentId = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);


                        DataSet ds1 = new BLL.Channel().SelectModule_fid(channel);


                        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        {

                            for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                            {


                                bur.AppendFormat("<tr>\n<td width=\"25%\" align=\"right\" valign=\"top\">|————" + ds1.Tables[0].Rows[j]["Title"] + "：</td>\n");

                                bur.AppendFormat("<td width=\"75%\">");

                                bur.AppendFormat("<input class=\"selectclass\" type=\"checkbox\" value=\"select" + ds1.Tables[0].Rows[j]["id"] + "\" />查看");

                                bur.AppendFormat("<input class=\"addclass\" type=\"checkbox\" value=\"add" + ds1.Tables[0].Rows[j]["id"] + "\" />添加");

                                bur.AppendFormat("<input class=\"updateclass\" type=\"checkbox\" value=\"update" + ds1.Tables[0].Rows[j]["id"] + "\" />修改");

                                bur.AppendFormat("<input class=\"deleteclass\" type=\"checkbox\" value=\"delete" + ds1.Tables[0].Rows[j]["id"] + "\" />删除");

                                bur.AppendFormat("<input class=\"smhclass\" " + (ds1.Tables[0].Rows[j]["FileUrl"].ToString().IndexOf("Article") == -1 ? "style=\"display:none\"" : "") + " type=\"checkbox\" value=\"smh" + ds1.Tables[0].Rows[j]["id"] + "\" />" + (ds1.Tables[0].Rows[j]["FileUrl"].ToString().IndexOf("Article") == -1 ? "" : "审核") + "");

                                bur.AppendFormat("</td></tr>");



                            }
                        }

                    }

                    allflag = bur.ToString();

                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.Model.Administrator model = new DtCms.Model.Administrator();
            DtCms.BLL.Administrator bll = new DtCms.BLL.Administrator();
            string userLevel = string.Empty;
            string userName = txtUserName.Text.Trim();
            string userPwd = DtCms.Common.DESEncrypt.Encrypt(txtUserPwd.Text.Trim());
            string readName = txtReadName.Text.Trim();
            string userEmail = txtUserEmail.Text.Trim();
            int userType = Convert.ToInt32(rblUserType.SelectedValue);
            int isLock = Convert.ToInt32(rblIsLock.SelectedValue);
            if (bll.Exists(userName, Session["ver"].ToString()))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误了！</b>用户名已存在，请输入别的管理帐号吧！", "", "Error");
                return;
            }
            if (userType > 1)
            {
                if (Request.Form["cbLevel"] != null && Request.Form["cbLevel"].Trim() != "")
                {
                    userLevel = "," + Request.Form["cbLevel"].Trim() + ",";
                }
                else
                {
                    // Page.RegisterClientScriptBlock("sas", "<script>alert('请选择权限');</script>");


                    return;
                }

            }


            model.UserName = userName;
            model.UserPwd = userPwd;
            model.ReadName = readName;
            model.UserEmail = userEmail;
            model.UserType = userType;
            model.IsLock = isLock;
            model.UserLevel = this.allflaginfo.Value;


            bll.Add(model);
            //保存日志
            SaveLogs("[管理员管理]增加管理员：" + model.UserName);
            JscriptPrint("添加管理员成功啦！", "list.aspx", "Success");
        }
    }
}
