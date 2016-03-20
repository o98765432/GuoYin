using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Templates
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                chkLoginLevel("viewTemplates");
                RptBind();
            }
        }

        #region 模板列表绑定
        private void RptBind()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("skinname", Type.GetType("System.String"));
            dt.Columns.Add("name", Type.GetType("System.String"));
            dt.Columns.Add("img", Type.GetType("System.String"));
            dt.Columns.Add("author", Type.GetType("System.String"));
            dt.Columns.Add("createdate", Type.GetType("System.String"));
            dt.Columns.Add("ver", Type.GetType("System.String"));
            dt.Columns.Add("fordntver", Type.GetType("System.String"));

            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath("../../Templates/"));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                DataRow dr = dt.NewRow();
                DtCms.Model.Templates aboutInfo = GetTemplateAboutInfo(dir.FullName);
                dr["skinname"] = dir.Name;// 文件夹名称
                dr["name"] = aboutInfo.name;// 模板名称
                dr["img"] = "../../Templates/" + dir.Name + "/about.png";// 模板图片
                dr["author"] = aboutInfo.author;// 作者
                dr["createdate"] = aboutInfo.createdate;// 创建日期
                dr["ver"] = aboutInfo.ver;// 模板版本
                dr["fordntver"] = aboutInfo.fordntver;// 适用的版本
                dt.Rows.Add(dr);
            }

            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
        #endregion

        //生成或启用
        protected void rptList_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            chkLoginLevel("markTemplates");
            HiddenField hideSkinName = (HiddenField)e.Item.FindControl("hideSkinName");
            switch (e.CommandName.ToLower())
            {
                case "start":
                    //启用模板
                    DtCms.BLL.WebSet bll = new DtCms.BLL.WebSet();
                    DtCms.Model.WebSet model = webset;
                    model.TemplateSkin = hideSkinName.Value.ToLower();
                    //修改配置信息
                    bll.saveConifg(model, Server.MapPath(ConfigurationManager.AppSettings["Configpath"].ToString()));
                    //重新生成模板
                    MarkTemplates(hideSkinName.Value.ToLower());
                    //保存日志
                    SaveLogs("[模板管理]启用并生成了模板：" + hideSkinName.Value);
                    JscriptPrint("模板启用并全部生成成功啦！", "List.aspx", "Success");
                    break;
                case "remark":
                    //重新生成模板
                    MarkTemplates(hideSkinName.Value.ToLower());
                    //保存日志
                    SaveLogs("[模板管理]重新生成了模板：" + hideSkinName.Value);
                    JscriptPrint("模板已全部重新生成啦！", "List.aspx", "Success");
                    break;
            }
        }

        //设置当前模板
        protected void rptList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HiddenField hideSkinName = (HiddenField)e.Item.FindControl("hideSkinName");
                if (hideSkinName.Value.ToLower() == webset.TemplateSkin.ToLower())
                {
                    ((LinkButton)e.Item.FindControl("lbtnStart")).Visible = false;
                    ((LinkButton)e.Item.FindControl("lbtnReMark")).Visible = true;
                }
            }
        }

    }
}
