using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace DtCms.Web.Admin.Manage
{
    public partial class edit :DtCms.Web.UI. ManagePage
    {
        public int Id;
        protected string strLevel;
        protected int strType;
        //权限信息
        protected string allflag = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            chkLoginLevel("editManage");
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要修改的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                //this.btnSave.Visible = updateflag;
                ShowInfo(this.Id);
            }
        }

        //赋值操作
        private void ShowInfo(int editID)
        {
            DtCms.BLL.Administrator bll = new DtCms.BLL.Administrator();
            DtCms.Model.Administrator model = new DtCms.Model.Administrator();
            model = bll.GetModel(editID);
            txtUserName.Text = model.UserName;
            if (model.IsLock == 1)
            {
                this.rblIsLock.Items[1].Selected = true;
            }
            else
            {
                this.rblIsLock.Items[0].Selected = true;
            }
            txtReadName.Text = model.ReadName;
            txtUserEmail.Text = model.UserEmail;
            this.strLevel = model.UserLevel;
            this.strType = model.UserType;

            //Model.Module module = new Model.Module();
            Model.Channel chanel = new Model.Channel();

            chanel.Ver = Session["ver"].ToString();

            chanel.ParentId = 0;

            DataSet ds = new BLL.Channel().SelectModule_fid(chanel);

            StringBuilder bur = new StringBuilder();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {


                    bur.AppendFormat("<tr>\n<td width=\"25%\" align=\"right\"  valign=\"top\"><font color=\"red\">|——" + ds.Tables[0].Rows[i]["Title"] + "：</font></td>\n");

                    bur.AppendFormat("<td width=\"75%\">");

                    bur.AppendFormat("<input class=\"selectclass\" type=\"checkbox\"   " + returnByCheack(model.UserLevel, ",select" + ds.Tables[0].Rows[i]["id"] + ",") + "     value=\"select" + ds.Tables[0].Rows[i]["id"] + "\" />查看");

                    bur.AppendFormat("<input class=\"addclass\" type=\"checkbox\"  " + returnByCheack(model.UserLevel, ",add" + ds.Tables[0].Rows[i]["id"] + ",") + "     value=\"add" + ds.Tables[0].Rows[i]["id"] + "\" />添加");

                    bur.AppendFormat("<input class=\"updateclass\" type=\"checkbox\" " + returnByCheack(model.UserLevel, ",update" + ds.Tables[0].Rows[i]["id"] + ",") + "     value=\"update" + ds.Tables[0].Rows[i]["id"] + "\" />修改");

                    bur.AppendFormat("<input class=\"deleteclass\" type=\"checkbox\" " + returnByCheack(model.UserLevel, ",delete" + ds.Tables[0].Rows[i]["id"] + ",") + "     value=\"delete" + ds.Tables[0].Rows[i]["id"] + "\" />删除");

                    bur.AppendFormat("<input class=\"smhclass\" " + (ds.Tables[0].Rows[i]["FileUrl"].ToString().IndexOf("Article") == -1 ? "style=\"display:none\"" : "") + " type=\"checkbox\" " + returnByCheack(model.UserLevel, ",smh" + ds.Tables[0].Rows[i]["id"] + ",") + "     value=\"smh" + ds.Tables[0].Rows[i]["id"] + "\" />" + (ds.Tables[0].Rows[i]["FileUrl"].ToString().IndexOf("Article") == -1 ? "" : "审核") + "");


                    bur.AppendFormat("</td></tr>");


                    chanel = new Model.Channel();

                    chanel.Ver = Session["ver"].ToString();

                    chanel.ParentId = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);


                    DataSet ds1 = new BLL.Channel().SelectModule_fid(chanel);


                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {

                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {


                            bur.AppendFormat("<tr>\n<td width=\"25%\" align=\"right\" valign=\"top\">|————" + ds1.Tables[0].Rows[j]["Title"] + "：</td>\n");

                            bur.AppendFormat("<td width=\"75%\">");

                            bur.AppendFormat("<input class=\"selectclass\" type=\"checkbox\"   " + returnByCheack(model.UserLevel, ",select" + ds1.Tables[0].Rows[j]["id"] + ",") + "     value=\"select" + ds1.Tables[0].Rows[j]["id"] + "\" />查看");

                            bur.AppendFormat("<input class=\"addclass\" type=\"checkbox\"  " + returnByCheack(model.UserLevel, ",add" + ds1.Tables[0].Rows[j]["id"] + ",") + "     value=\"add" + ds1.Tables[0].Rows[j]["id"] + "\" />添加");

                            bur.AppendFormat("<input class=\"updateclass\" type=\"checkbox\" " + returnByCheack(model.UserLevel, ",update" + ds1.Tables[0].Rows[j]["id"] + ",") + "     value=\"update" + ds1.Tables[0].Rows[j]["id"] + "\" />修改");

                            bur.AppendFormat("<input class=\"deleteclass\" type=\"checkbox\" " + returnByCheack(model.UserLevel, ",delete" + ds1.Tables[0].Rows[j]["id"] + ",") + "     value=\"delete" + ds1.Tables[0].Rows[j]["id"] + "\" />删除");

                            bur.AppendFormat("<input class=\"smhclass\" " + (ds1.Tables[0].Rows[j]["FileUrl"].ToString().IndexOf("Article") == -1 ? "style=\"display:none\"" : "") + " type=\"checkbox\" " + returnByCheack(model.UserLevel, ",smh" + ds1.Tables[0].Rows[j]["id"] + ",") + "     value=\"smh" + ds1.Tables[0].Rows[j]["id"] + "\" />" + (ds1.Tables[0].Rows[j]["FileUrl"].ToString().IndexOf("Article") == -1 ? "" : "审核") + "");


                            bur.AppendFormat("</td></tr>");



                        }
                    }

                }
            }

            allflag = bur.ToString();



            if (model.UserType == 1)
             {
                this.rblUserType.Items[0].Selected = true;
            }
            if (model.UserType == 2)
            {
                this.rblUserType.Items[1].Selected = true;
            }
            if (model.UserType == 3)
            {
                this.rblUserType.Items[2].Selected = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Administrator bll = new DtCms.BLL.Administrator();
            DtCms.Model.Administrator model = bll.GetModel(this.Id);

            string UserPwd = txtUserPwd.Text.Trim();
            string UserLevel = string.Empty;
            int UserType = Convert.ToInt32(rblUserType.SelectedValue);
            if (UserType > 1)
            {
                //UserLevel = "," + Request.Form["cbLevel"].Trim() + ",";
                if (Request.Form["cbLevel"] != null && Request.Form["cbLevel"].Trim() != "")
                {
                    UserLevel = "," + Request.Form["cbLevel"].Trim() + ",";
                }
                else
                {

                    return;
                }
            }
            if (UserPwd != null && UserPwd != "")
            {
                model.UserPwd = DtCms.Common.DESEncrypt.Encrypt(UserPwd);
            }
            model.ReadName = txtReadName.Text.Trim();
            model.UserEmail = txtUserEmail.Text.Trim();
            model.UserType = UserType;
            model.IsLock = Convert.ToInt32(rblIsLock.SelectedValue);
            model.UserLevel = this.allflaginfo.Value;



            bll.Update(model);

            if (DtCms.Common.Utils.returnIntByString(Session["AdminNo"].ToString())==model.Id) 
            {

                Session["AdminLevel"] = model.UserLevel;
            
            }

            //保存日志
            SaveLogs("[管理员管理]编辑管理员：" + model.UserName);
            JscriptPrint("管理员修改成功啦！", "List.aspx", "Success");
        }
    }
}
