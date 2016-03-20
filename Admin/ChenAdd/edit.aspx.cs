using System;
using System.IO;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using DtCms.Web.UI;

namespace DtCms.Web.Admin.ChenAdd
{
    public partial class edit : DtCms.Web.UI.ManagePage
    {
        public int Id = 0;
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
        protected int classid;
        public int kindId;
        protected int drpClassId;
        protected string classList;
        protected BLL.Chenadd shipLink = new BLL.Chenadd();
        protected string strtitle = "添加";
        protected Model.Channel channelmodel = new Model.Channel();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.Id = DtCms.Common.Utils.returnIntByString(Request.QueryString["id"]);
            }

            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classid"]);

            if (!IsPostBack)
            {

                ChannelTreeBind(0, channelmodel.Title, (int)Channel.Article, this.txtclassid, ver);


                channelmodel = new BLL.Channel().GetModel(classid);


                if (this.Id > 0)
                {
                    strtitle = "修改";
                    getBannerInfoById();

                }

            }
        }

        public void getBannerInfoById()
        {
            BLL.Chenadd bannner = new BLL.Chenadd();
            Model.Chenadd model = new Model.Chenadd();

            model = bannner.GetModel(this.Id);

            if (model != null)
            {

                this.txtclassid.SelectedValue = model.classid + "";

                this.txtfenge.Text = model.fenge;

                this.txtheight.Text = model.height + "";

                this.txtmemo.Text = model.memo;

                this.txttitle.Text = model.title;

                this.txttype.SelectedValue = model.type + "";

                this.txtwidth.Text = model.width + "";

                this.txtziduan.Text = model.ziduan;

                this.txttypeid.SelectedValue = model.typeid + "";

                this.txtid.Value = model.id + "";

                this.txtbitian.SelectedValue = model.bitian + "";

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            Model.Chenadd model = new Model.Chenadd();
            BLL.Chenadd bllBanner = new BLL.Chenadd();

            model.classid = DtCms.Common.Utils.returnIntByString(this.txtclassid.SelectedValue);

            model.fenge = this.txtfenge.Text;

            model.height = DtCms.Common.Utils.returnIntByString(this.txtheight.Text);

            model.memo = this.txtmemo.Text;

            model.title = this.txttitle.Text;

            model.type = DtCms.Common.Utils.returnIntByString(this.txttype.SelectedValue);

            model.width = DtCms.Common.Utils.returnIntByString(this.txtwidth.Text);

            model.ziduan = this.txtziduan.Text;

            model.typeid = DtCms.Common.Utils.returnIntByString(this.txttypeid.SelectedValue);

            model.id = DtCms.Common.Utils.returnIntByString(this.txtid.Value);

            model.bitian = DtCms.Common.Utils.returnIntByString(this.txtbitian.SelectedValue);

            if (model.id > 0)
            {

                bllBanner.Update(model);

                Response.Write("<script type='text/javascript'>alert('修改成功');location.href='list.aspx?classid=" + classid + "'</script>");
                Response.End();
            }
            else
            {
                if (!bllBanner.Exists(model.type, model.ziduan))
                {


                    new BLL.Chenadd().Add(model);


                    Response.Write("<script type='text/javascript'>alert('添加成功');location.href='list.aspx?classid=" + classid + "'</script>");
                    Response.End();

                }
                else
                {

                    Response.Write("<script type='text/javascript'>alert('对不起本类别已经存在字段(" + model.ziduan + ")，请重新输入!');history.go(-1);'</script>");

                }
            }





        }



    }
}