using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Job
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        protected string strtitle = "添加";
        protected int classid;
        protected Model.Channel channelmodel = new Model.Channel();

        protected DataSet showda = null;
        protected DataSet shownow = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classid"]);

            showda = new BLL.Chenadd().GetList(" typeid=5 and classid=" + classid);


            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.Id = Convert.ToInt32(Request.QueryString["id"]);
            }
             
            if (!Page.IsPostBack)
            {

                channelmodel = new BLL.Channel().GetModel(classid);

                ChannelTreeBind(this.classid, channelmodel.Title, (int)Channel.Pictures, this.ddlClassId, "cn");
 
                if (Id > 0)
                {
                    strtitle = "修改";
                    ShowInfo(this.Id);
                }
                  
            }
        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.Job bll = new DtCms.BLL.Job();
            DtCms.Model.Job model = bll.GetModel(_id);

            shownow = new BLL.Job().GetModelDa(this.Id);
            txtTitle.Text = model.Title;
            txtAddr.Text = model.Address;
            txtBeginTime.Text = model.BeginTime;
            txtEndTime.Text = model.EndTime;
            txtContent.Value = model.Des;
            txtContent1.Value = model.Des1;
            txtContent2.Value = model.Des2;
            txtClick.Text = model.Click.ToString();

            this.txtAge.Text = model.Field1;
            this.txtEducation.Text = model.Education;
            this.txtYear.Text = model.Workyear;
            this.txtPeople.Text = model.People;

            this.txtSex.Text = model.Sex;

            this.txtFilepath.Text = model.Filepath;

            this.ddlClassId.SelectedValue = model.Classid.ToString();
            this.txtSortId.Text = model.SortId.ToString();

            this.txtCompany.Text = model.Company;

            this.txtcontact.Text = model.contact;

            this.txtshowemail.Text = model.showemail;

            this.txttel.Text = model.tel;

            this.txtKeyword.Text = model.Keyword;
            this.txtDescription.Text = model.Description;

            if (model.IsLock == 0)
            {
                this.IsLock.Checked = false;
            }


            
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Job bll = new DtCms.BLL.Job();
            DtCms.Model.Job model = bll.GetModel(this.Id);

            if (model == null)
            {
                model = new Model.Job();
            }

            model.Address = this.txtAddr.Text.Trim();
            model.AddTime = DateTime.Now;
            model.BeginTime = this.txtBeginTime.Text.Trim();
            model.EndTime = this.txtEndTime.Text.Trim();
            model.Classid = int.Parse(this.ddlClassId.SelectedValue.Trim());
            model.Click = int.Parse(this.txtClick.Text.Trim());
            model.Company = this.txtCompany.Text;
            model.Des = this.txtContent.Value;
            model.Des1 = this.txtContent1.Value;
            model.Des2 = this.txtContent2.Value;
            model.Des3 = "";
            model.Education = this.txtEducation.Text.Trim();
            model.Email = "";
            model.Field1 = this.txtAge.Text.Trim();
            model.Filepath = this.txtFilepath.Text.Trim();
            model.IsLock = 0;
            model.IsRed = 0;
            model.IsTop = 0;
            model.Jobtype = "";
            model.LinkUrl = "";
            model.People = this.txtPeople.Text;
            model.Sex = this.txtSex.Text;
            model.SortId = int.Parse(this.txtSortId.Text.Trim());
            model.Title = this.txtTitle.Text.Trim();
            model.Ver = Session["ver"].ToString();
            model.Workyear = this.txtYear.Text.Trim(); 
            model.contact= this.txtcontact.Text; 
            model.showemail = this.txtshowemail.Text; 
            model.tel = this.txttel.Text; 
            model.Keyword = this.txtKeyword.Text; 
            model.Description = this.txtDescription.Text;
             
                model.IsRed = 0;
            
                model.IsTop = 0;

                if (this.IsLock.Checked)
                {
                    model.IsLock = 1;
                }
                else
                {
                    model.IsLock = 0;
                }
            
            SaveLogs("[招聘模块]编辑招聘：" + model.Title);

            string strnowsql = "";


            if (showda != null && showda.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < showda.Tables[0].Rows.Count; i++)
                {


                    if (!string.IsNullOrEmpty(strnowsql))
                    {
                        strnowsql += " , " + showda.Tables[0].Rows[i]["ziduan"] + " ='" + DtCms.Common.Utils.ToHtml(Request.Form["" + showda.Tables[0].Rows[i]["ziduan"] + ""]) + "'";

                    }
                    else
                    {
                        strnowsql = showda.Tables[0].Rows[i]["ziduan"] + " = '" + DtCms.Common.Utils.ToHtml(Request.Form["" + showda.Tables[0].Rows[i]["ziduan"] + ""]) + "'";



                    }


                }
            }


            if (this.Id > 0)
            {
                bll.Update(model);

                if (!string.IsNullOrEmpty(strnowsql))
                {

                    bll.UpdateField(Id, strnowsql);

                }

                Response.Write("<script type='text/javascript'>alert('修改成功');location.href='list.aspx?classid=" + classid + "&page=" + Request.QueryString["page"] + "'</script>");
                Response.End();
            }
            else
            {
                int nowid = bll.Add(model);

                if (!string.IsNullOrEmpty(strnowsql))
                {

                    bll.UpdateField(nowid, strnowsql);
                }
                Response.Write("<script type='text/javascript'>alert('添加成功');location.href='list.aspx?classid=" + classid + "&page=" + Request.QueryString["page"] + "'</script>");
                Response.End();
            }

            //保存日志
           
           
        }

        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlClassId.SelectedIndex > 0)
            {
                DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
                DtCms.Model.Channel model = new DtCms.Model.Channel();
                model = bll.GetModel(int.Parse(this.ddlClassId.SelectedValue),"cn");

                this.txtFilepath.Text = model.Filepath;

            }
        }

    }
}
