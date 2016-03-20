using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.SystemLog
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.LogPageNum;
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }

            if (!Page.IsPostBack)
            {
                chkLoginLevel("viewSystemLog");
                this.RptBind("");
            }
        }

        #region 数据列表绑定
        private void RptBind(string strWhere)
        {
            DtCms.BLL.SystemLog bll = new DtCms.BLL.SystemLog();
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

        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delSystemLog");
            DtCms.BLL.SystemLog bll = new DtCms.BLL.SystemLog();
            //清空日志
            int num = bll.Delete(7);
            //保存日志
            SaveLogs("[系统日志]共清空了" + num + "条日志！");
            JscriptPrint("共清空了" + num + "条日志！", "List.aspx", "Success");
        }
    }
}
