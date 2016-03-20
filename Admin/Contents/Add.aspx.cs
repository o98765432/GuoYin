using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Contents
{
    public partial class Add : DtCms.Web.UI.ManagePage
    {
        protected int classid;

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["classid"].ToString() != "" && Request.QueryString["classid"] != null)
            {
                classid = Convert.ToInt32(Request.QueryString["classid"]);
            }

            if (!Page.IsPostBack)
            {
                btnSave.Visible = addflag;

                chkLoginLevel("addContents");
                //绑定类别
                ChannelTreeBind(this.classid, "请选择所属类别...", (int)Channel.Contents, this.ddlClassId, ver);
                if (!string.IsNullOrEmpty(Request.Params["classId"]))
                {
                    ddlClassId.SelectedValue = Request.Params["classId"].Trim();
                }
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
            DtCms.Model.Contents model = new DtCms.Model.Contents();
            model.CallIndex = txtCallIndex.Text.Trim();
            model.Title = txtTitle.Text.Trim();
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            model.Content = txtContent.Value;
            model.Ver = ver;
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
            bll.Add(model);
            //保存日志
            SaveLogs("[内容模块]添加内容页：" + model.Title);

            JscriptPrint("内容添加成功啦！", "List.aspx?classId=" + ddlClassId.SelectedValue, "Success");
        }

        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlClassId.SelectedIndex > 0)
            {
                DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
                DtCms.Model.Channel model = new DtCms.Model.Channel();
                model = bll.GetModel(int.Parse(this.ddlClassId.SelectedValue),ver);
                if (model != null)
                {
                    this.txtFilepath.Text = model.Filepath;
                    this.litSize.Text = model.Content;
                }
            }
        }
    }
}
