using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Contents
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
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
        protected BLL.Contents contents = new BLL.Contents();
        protected string classList;
        protected int drpClassId;
        protected string strtitle = "添加";
        protected int classid;
        protected Model.Channel channelmodel = new Model.Channel();

        protected void Page_Load(object sender, EventArgs e)
        {
            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classid"]);

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.Id = Convert.ToInt32(Request.QueryString["id"]);
            }

            DataSet ds = contents.GetClassListById(Id, ver);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    classList = ds.Tables[0].Rows[i]["ClassList"].ToString();
                }

                this.drpClassId = Convert.ToInt32(classList.Substring(1, 3));
            }


            if (!Page.IsPostBack)
            {
                btnSave.Visible = updateflag;

                channelmodel = new BLL.Channel().GetModel(classid);

                ChannelTreeBind(this.classid, channelmodel.Title, (int)Channel.Pictures, this.ddlClassId, ver); 

                if (Id > 0)
                {
                    strtitle = "修改";
                    ShowInfo(this.Id);
                }
                else
                {

                    this.radNeilians.Checked = true;
                    this.radWailians.Checked = false;
                }

            }


        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
            DtCms.Model.Contents model = bll.GetModel(_id);
            txtCallIndex.Text = model.CallIndex;
            txtTitle.Text = model.Title;
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtSortId.Text = model.SortId.ToString();
            txtContent.Value = model.Content;

            this.txtDescription.Text = model.Description;
            txtFilepath.Text = model.Filepath;
            txtImgUrl.Text = model.ImgUrl;
            txtIntro.Text = model.Intro;
            txtKeyword.Text = model.Keyword;

             if (model.HerfFlag == 1)
            {

                this.radNeilians.Checked = true;
                this.radWailians.Checked = false;
            }
            else
            {
                this.wailians.Text = model.HerfFlag+"";
                this.wailians.Style["display"] = "block";
                this.radNeilians.Checked = false;
                this.radWailians.Checked = true;

            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
            DtCms.Model.Contents model = bll.GetModel(this.Id);


            if (model == null)
            {
                model = new Model.Contents();
            }


            model.CallIndex = txtCallIndex.Text.Trim();
            model.Title = txtTitle.Text.Trim();
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            model.Content = txtContent.Value;
            model.Ver =ver;
            model.SortId = int.Parse(txtSortId.Text.Trim());

            model.ImgUrl = this.txtImgUrl.Text.Trim();
            model.ImgUrl2 = "";
            model.Intro = this.txtIntro.Text.Trim();
            model.Keyword = this.txtKeyword.Text.Trim();
            model.Description = this.txtDescription.Text.Trim();
            model.Filepath = this.txtFilepath.Text.Trim();

            if (this.radNeilians.Checked == true)//内链存1
            {
                model.HerfFlag = 1;
                model.Herf = this.neilians.Text;
            }
            if (this.radWailians.Checked == true)//外链存0
            {
                model.HerfFlag = 0;
                model.Herf = this.wailians.Text;
            }

            //保存日志
            SaveLogs("[内容模块]编辑内容页：" + model.Title); 
            if (this.Id > 0)
            {
                bll.Update(model);
                Response.Write("<script type='text/javascript'>alert('修改成功');location.href='list.aspx?classid=" + classid + "'</script>");
                Response.End();
            }
            else
            {
                bll.Add(model);
                Response.Write("<script type='text/javascript'>alert('添加成功');location.href='list.aspx?classid=" + classid + "'</script>");
                Response.End();
            }
        }

        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlClassId.SelectedIndex > 0)
            {
                DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
                DtCms.Model.Channel model = new DtCms.Model.Channel();
                model = bll.GetModel(int.Parse(this.ddlClassId.SelectedValue),ver);

                this.txtFilepath.Text = model.Filepath;
                this.litSize.Text = model.Content;
            }
        }

    }
}
