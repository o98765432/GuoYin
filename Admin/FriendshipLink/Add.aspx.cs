using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.FriendshipLink
{
    public partial class Add : DtCms.Web.UI.ManagePage
    {
        protected int classid;
        protected BLL.FriendshipLink shipBll = new BLL.FriendshipLink();
        protected Model.FriendshipLink shipModel = new Model.FriendshipLink();
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

            if (Request.QueryString["classid"] != null && Request.QueryString["classid"].ToString() != "")
            {
                this.classid = Convert.ToInt32(Request.QueryString["classid"]);

            }
         
            if (IsPostBack)
                return;

            btnSave.Visible = addflag;

            chkLoginLevel("addDownloads");
            //绑定类别
            ChannelTreeBind(this.classid, "请选择所属类别...", (int)Channel.FriendshipLink, this.ddlClassId, ver);

            if (!string.IsNullOrEmpty(Request.Params["classId"]))
            {
                ddlClassId.SelectedValue = Request.Params["classId"].Trim();
            }
        }

        //保存


        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlClassId.SelectedIndex > 0)
            {
                DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
                DtCms.Model.Channel model = new DtCms.Model.Channel();
                model = bll.GetModel(int.Parse(this.ddlClassId.SelectedValue), ver);

                this.txtFilepath.Text = model.Filepath;

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                shipModel.ClassId = int.Parse(ddlClassId.SelectedValue);
                shipModel.Title = txtTitle.Text;
                shipModel.Href = txtHref.Text;
                shipModel.AddTime = DateTime.Now;
                shipModel.SortId = Convert.ToInt32(txtSortId.Text);
                shipModel.IsLock = Convert.ToInt32(txtIslock.Text);
                shipModel.Ver = ver;
                shipModel.ImgUrl = txtImgUrl.Text.Trim();
                shipModel.HtmlPath = txtFilepath.Text;

                int flag = shipBll.Add(shipModel);

                if (flag > 0)
                {
                    Response.Redirect("List.aspx");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}