using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Web.UI;
namespace DtCms.Web.Admin.ShowChanner
{
    public partial class list : ManagePage
    {
        public int kindId=0; //栏目种类
        DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
        protected string path = "Article";
        protected int showkindid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(Request.QueryString["path"]))
            {

                path = Request.QueryString["path"];

            }

            if (int.TryParse(Request.QueryString["showkindid"], out showkindid))
            { 
                
            
            }
            //取得栏目传参
            if (int.TryParse(Request.Params["kindId"], out kindId))
            {
                if (!Page.IsPostBack)
                {
                    BindData();
                }
            }
            else
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要管理的类别种类不明确或参数不正确。", "back", "Error");
            }

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {


                int showid = 0;

                if (int.TryParse(Request.QueryString["id"], out showid))
                { }

                if (showid > 0)
                {

                    new BLL.Channel().Delete(showid, "cn");

                }
                Response.Write("<script type='text/javascript'>alert('删除成功');location.href='List.aspx?kindId=" + kindId + "&showkindid=" + showkindid + "'</script>");
                Response.End();

            }


        }

        //数据绑定
        private void BindData()
        {
            DataTable dt = bll.GetList(kindId, kindId, "cn");

            if (kindId > 0)
            {
                Model.Channel channer = bll.GetModel(kindId);


                DataRow newuser = dt.NewRow();
                newuser["Id"] = channer.Id;
                newuser["Title"] = channer.Title;
                newuser["ClassList"] = channer.ClassList;
                newuser["ClassLayer"] = channer.ClassLayer;

                dt.Rows.InsertAt(newuser, 0);
            }
             
        }

        //删除操作
        protected void rptClassList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField txtClassId = (HiddenField)e.Item.FindControl("txtClassId");
            switch (e.CommandName.ToLower())
            {
                case "btndel":
                    //保存日志
                    SaveLogs("[栏目类别]删除类别：" + bll.GetModel(Convert.ToInt32(txtClassId.Value), "cn"));
                    //删除记录
                    bll.Delete(Convert.ToInt32(txtClassId.Value), "cn");
                    BindData();
                    Response.Write("<script type='text/javascript'>alert('删除成功');location.href='List.aspx?kindId=" + kindId + "&path=" + path + "'</script>");
                    Response.End();
                    break;
            }
        }
        //美化列表
        protected void rptClassList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField txtClassLayer = (HiddenField)e.Item.FindControl("txtClassLayer");
                string LitStyle = "<span style=width:{0}px;text-align:right;display:inline-block;>{1}{2}</span>";
                string LitImg1 = "<img src=../images/folder_open.gif align=absmiddle />";
                string LitImg2 = "<img src=../images/t.gif align=absmiddle />";

                int classLayer = Convert.ToInt32(txtClassLayer.Value);
                if (classLayer == 1)
                {
                    LitFirst.Text = LitImg1;
                }
                else
                {
                    LitFirst.Text = string.Format(LitStyle, classLayer * 18, LitImg2, LitImg1);
                }
            }
        }
    }
}
