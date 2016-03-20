using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace DtCms.Web.Admin.Advertising
{
    public partial class BarList : DtCms.Web.UI.ManagePage
    {
        public int pcount = 0; //总条数
        public int page; //当前页
        public int pagesize; //设置每页显示的大小

        public string keywords = "";
        public int pid;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.AdPageNum;
            if (!int.TryParse(Request.Params["pid"] as string, out this.pid))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要查看的广告位不存在或者已经被删除了。", "back", "Error");
                return;
            }
            if (!string.IsNullOrEmpty(Request.Params["keywords"]))
            {
                this.keywords = Request.Params["keywords"].Trim();
            }
            if (!Page.IsPostBack)
            {
                chkLoginLevel("viewAdvertising");
                RptBind("Id>0" + CombAdSqlTxt(this.pid, this.keywords)+" and ver='"+Session["ver"].ToString()+"'");
            }
        }

        #region 列表数据绑定
        private void RptBind(string where)
        {
            //显示所属的广告位
            DtCms.BLL.Advertising aBll = new DtCms.BLL.Advertising();
            DtCms.Model.Advertising aModel = aBll.GetModel(this.pid);
            lblAdTitle.Text = "【" + aModel.Id + "】" + aModel.Title + "（" + GetAdTypeName(aModel.AdType.ToString()) + "）";

            //绑定广告条列表
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            this.txtKeywords.Text = this.keywords;
            DtCms.BLL.Adbanner bll = new DtCms.BLL.Adbanner();
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
        #endregion

        #region 返回广告位类型名称
        private string GetAdTypeName(string strId)
        {
            switch (strId)
            {
                case "1":
                    return "文字";
                case "2":
                    return "图片";
                case "3":
                    return "幻灯片";
                case "4":
                    return "动画";
                case "5":
                    return "FLV视频";
                case "6":
                    return "代码";
                default:
                    return "其它";
            }
        }
        #endregion

        #region 返回广告条状态
        protected string GetAdState(string strLock, string strTime)
        {
            if (int.Parse(strLock) == 1)
            {
                return "<font color=\"#999999\">暂停</font>";
            }
            else if (DateTime.Compare(DateTime.Parse(strTime), DateTime.Today) == -1)
            {
                return "<font color=\"#FF0000\">已过期</font>";
            }
            else
            {
                return "<font color=\"#009900\">正常</font>";
            }
        }
        #endregion

        //查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("BarList.aspx?" + CombAdUrlTxt(this.pid, txtKeywords.Text.Trim()) + "page=0");
        }

        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delAdvertising");
            DtCms.BLL.Adbanner bll = new DtCms.BLL.Adbanner();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //删除图片
                    DeleteFile(bll.GetModel(id).AdUrl);
                    //保存日志
                    SaveLogs("[广告管理]增加广告内容：" + bll.GetModel(id).Title);
                    //删除记录
                    bll.Delete(id);
                }
            }
            JscriptPrint("批量删除成功啦！", "BarList.aspx?" + CombAdUrlTxt(this.pid, this.keywords) + "page=0", "Success");
        }
    }
}
