using System;
using System.IO;
using System.Net;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
 
namespace DtCms.Web.Admin.Video
{
    public partial class edit : DtCms.Web.UI.ManagePage
    {
        public int Id;

        protected string filepath = "", img = "";
        protected int drpClassId;
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
        protected BLL.Video download = new BLL.Video();
        protected string strtitle = "添加";
        protected int classid;
        protected Model.Channel channelmodel = new Model.Channel();

        protected DataSet showda = null;
        protected DataSet shownow = null;

        protected void Page_Load(object sender, EventArgs e)
        {

          

            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classid"]);

            showda = new BLL.Chenadd().GetList(" typeid=3 and classid=" + classid);



            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.Id = Convert.ToInt32(Request.QueryString["id"]);
            }
             
            if (!Page.IsPostBack)
            {
                channelmodel = new BLL.Channel().GetModel(classid);

                ChannelTreeBind(this.classid, channelmodel.Title, (int)Channel.Downloads, this.ddlClassId, ver);

                if (Id > 0)
                {
                    strtitle = "修改";
                    ShowInfo(this.Id);
                    btnSave.Visible = updateflag;
                }
                else
                {
                    btnSave.Visible = addflag;

                }

            }
        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.Video bll = new DtCms.BLL.Video();
            DtCms.Model.Video model = bll.GetModel(_id);
            shownow = new BLL.Video().GetModelDa(this.Id);
            txtTitle.Text = model.Title;
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtImgUrl.Text = model.ImgUrl;
            txtFilePath.Text = model.FilePath;
            txtContent.Value = model.Content;
            txtClick.Text = model.Click.ToString();
          
            img = model.ImgUrl;

            filepath = model.FilePath;

            this.txtSubTitle.Text = model.SubTitle;

            if (model.IsLock == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            
           cbTop.Items[0].Selected = model.isTop == 1 ? true : false;
            
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text.Trim();

            DtCms.BLL.Video bll = new DtCms.BLL.Video();
            DtCms.Model.Video model = bll.GetModel(this.Id);

            if (model == null)
            {

                model = new Model.Video();
            }
            
            model.Title = txtTitle.Text.Trim();
            model.Ver = Session["ver"].ToString();
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            model.ImgUrl = txtImgUrl.Text.Trim();


            
            
            model.FilePath = txtFilePath.Text.Trim();
            model.Content = txtContent.Value;
            model.Click = int.Parse(txtClick.Text.Trim());
            model.IsLock = 0;
            model.isTop = 0;
            model.SubTitle = this.txtSubTitle.Text.Trim();

            if (cblItem.Items[0].Selected == true)
            {
                model.IsLock = 1;
            }
            else
            {
                model.IsLock = 0;
            }
            if (cbTop.Items[0].Selected == true)
            {
                model.isTop = 1;
            }
            else
            {
                model.isTop = 0;
            }

            //保存日志
            SaveLogs("[下载模块]编辑下载：" + model.Title);



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
        }

    }
}
