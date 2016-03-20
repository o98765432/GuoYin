using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Links
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                     //总条数
        public int page;                       //当前页
        public int pagesize;                   //设置每页显示的大小

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.LinkPageNum;
            if (!Page.IsPostBack)
            {
                this.lbtnDel.Visible = deleteflag;
                chkLoginLevel("viewLinks");
                this.RptBind(" ver='"+Session["ver"].ToString()+"' ");
            }
        }

        #region 数据列表绑定
        private void RptBind(string strWhere)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            //获得总条数
            this.pcount = bll.GetCount(strWhere);
            if (this.pcount > 0)
            {
                this.lbtnDel.Enabled = true;
            }
            else
            {
                this.lbtnDel.Enabled = false;
            }

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, "AddTime desc");
            this.rptList.DataBind();
        }
        #endregion

        //审核
        protected void lbtnAudit_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "IsLock=0");
                }
            }
            JscriptPrint("批量审核通过啦！", "List.aspx", "Success");
        }

        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delLinks");
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //删除图片
                    DeleteFile(bll.GetModel(id).ImgUrl);
                    //保存日志
                    SaveLogs("[链接管理]删除链接：" + bll.GetModel(id).Title);
                    //删除记录
                    bll.Delete(id);
                }
            }
            JscriptPrint("批量删除成功啦！", "List.aspx", "Success");
        }
    }
}
