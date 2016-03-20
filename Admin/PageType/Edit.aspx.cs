using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web.Admin.PageType
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        protected int classid;
        protected int id;
        protected int drpClassId;
        protected string classList;
        protected BLL.PageType singel = new BLL.PageType();
        protected string strtitle = "添加";
        protected Model.Channel channelmodel = new Model.Channel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.id = Convert.ToInt32(Request.QueryString["id"]);
            }
             

            classid = DtCms.Common.Utils.returnIntByString(Request.QueryString["classid"]);


            channelmodel = new BLL.Channel().GetModel(classid);

            if (!IsPostBack)
            {

                //绑定类别

                if (id > 0)
                {
                    strtitle = "修改";
                    getSingelpageInfoById();

                }
                else
                {
                    this.radNeilians.Checked = true;
                    this.radWailians.Checked = false;
                }
            }
        }


        /// <summary>
        /// 根据id获得该条id下的单页面信息
        /// </summary>
        public void getSingelpageInfoById()
        {
            try
            {

                BLL.PageType bll = new BLL.PageType();
                Model.PageType model = new Model.PageType();

                model = bll.GetModel(this.id,Session["Ver"].ToString());

                if (model != null)
                {

                    txtPageTitle.Text = model.Title;
                    txtPageContent.Text = model.ContentInfo;
                    txtSortId.Text = model.SortId.ToString();


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                BLL.PageType bll = new BLL.PageType();

                Model.PageType model = new Model.PageType();

                if (model == null)
                {
                    model = new Model.PageType();
                }

                model.ClassId = 1;
                model.Title = this.txtPageTitle.Text;
                model.ContentInfo = this.txtPageContent.Text;
                model.SortId = int.Parse(txtSortId.Text);
                model.ver = Session["ver"].ToString();
                model.AddTime = DateTime.Now;


                SaveLogs("[图文链接模块]添加图文链接：" + model.Title);

                if (this.id > 0)
                {
                    model.Id = this.id;
                    bll.Update(model);
                    Response.Write("<script type='text/javascript'>alert('修改成功');location.href='list.aspx?classid=" + this.classid + "'</script>");
                    Response.End();
                }
                else
                {
                    bll.Add(model);
                    Response.Write("<script type='text/javascript'>alert('添加成功');location.href='list.aspx?classid=" + this.classid + "'</script>");
                    Response.End();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        { }
    }
}