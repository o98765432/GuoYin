using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace DtCms.Web.Admin.Manage
{
    public partial class list : DtCms.Web.UI.ManagePage
    {
        public int pcount;   //总条数
        public int page;     //当前页
        public int pagesize; //设置每页显示的大小

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.ManagePageNum;
            if (!IsPostBack)
            {

               // this.lbtnDel.Visible = deleteflag;

                chkLoginLevel("viewManage");
                RptBind("");
            }
        }

        //绑定数据
        void RptBind(string where)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Administrator bll = new DtCms.BLL.Administrator();
            //利用PAGEDDAGASOURCE类来分页
            PagedDataSource pg = new PagedDataSource();
            pg.DataSource = bll.GetList(where).Tables[0].DefaultView;
            pg.AllowPaging = true;
            pg.PageSize = pagesize;
            pg.CurrentPageIndex = page;
            //获得总条数
            pcount = bll.GetCount(where);
            //绑定数据
            rptList.DataSource = pg;
            rptList.DataBind();
        }

        //批量删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delManage");
            DtCms.BLL.Administrator bll = new DtCms.BLL.Administrator();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //保存日志
                    SaveLogs("[管理员管理]删除管理员：" + bll.GetModel(id).UserName);
                    //删除记录
                    bll.Delete(id);
                }
            }
            JscriptPrint("批量删除成功啦！", "", "Success");
            RptBind("");
        }
    }
}
