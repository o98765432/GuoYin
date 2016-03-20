using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using DtCms.Web.UI;
using System.Xml;

namespace DtCms.Web.Admin.Member
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                      //总条数
        public int page;                        //当前页
        public int pagesize;                    //设置每页显示的大小
        public readonly int kindId = (int)Channel.Member; //类别种类

        public int classId;
        public string keywords = "";
        public string property = "";

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
            this.pagesize = webset.DownPageNum;
            if (!int.TryParse(Request.Params["classId"] as string, out this.classId))
            {
                this.classId = 0;
            }
            if (!string.IsNullOrEmpty(Request.Params["keywords"]))
            {
                this.keywords = Request.Params["keywords"].Trim();
            }
            if (!string.IsNullOrEmpty(Request.Params["property"]))
            {
                this.property = Request.Params["property"].Trim();
            }

            if (!Page.IsPostBack)
            {

                this.lbtnDel.Visible = deleteflag;
                chkLoginLevel("viewMember");
                //绑定类别
                ChannelTreeBind(0, "所有类别", (int)Channel.Member, this.ddlClassId,ver);
                RptBind("Id>0 and ver='"+ver+"' " + CombSqlTxt(this.kindId, this.classId, this.keywords, this.property), "AddTime desc");
            }
        }

        #region 数据列表绑定
        private void RptBind(string strWhere, string orderby)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Member bll = new DtCms.BLL.Member();
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
            if (this.classId > 0)
            {
                this.ddlClassId.SelectedValue = this.classId.ToString();
            }
            this.txtKeywords.Text = this.keywords;
            this.ddlProperty.SelectedValue = this.property;

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, orderby);
            this.rptList.DataBind();
        }
        #endregion

        //设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            chkLoginLevel("editMember");
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            DtCms.BLL.Member bll = new DtCms.BLL.Member();
            DtCms.Model.Member model = bll.GetModel(id);
            switch (e.CommandName.ToLower())
            {
                case "ibtnLock":
                    if (model.IsLock == 1)
                        bll.UpdateField(id, "IsLock=0");
                    else
                        bll.UpdateField(id, "IsLock=1");
                    break;
              
            }
            RptBind("Id>0 and ver='"+ver+"'" + CombSqlTxt(this.kindId, this.classId, this.keywords, this.property), "AddTime desc");
        }

        //分类筛选
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                Response.Redirect("List.aspx?" + CombUrlTxt(_classId, this.keywords, this.property) + "page=0");
            }
            else
            {
                Response.Redirect("List.aspx?" + CombUrlTxt(0, this.keywords, this.property) + "page=0");
            }
        }

        //属性筛选
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(this.classId, this.keywords, this.ddlProperty.SelectedValue) + "page=0");
        }

        //查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(this.classId, txtKeywords.Text.Trim(), this.property) + "page=0");
        }
        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delMember");
            DtCms.BLL.Member bll = new DtCms.BLL.Member();
            DtCms.Model.Member model;
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    model = bll.GetModel(id);
                    ////删除图片
                    //DeleteFile(model.ImgUrl);
                    ////删除文件
                    //DeleteFile(model.FilePath);
                    //保存日志
                    string banben = "";
                    switch (ver)
                    {
                        case "cn":
                            banben = "中文版";
                            break;
                        case "cn-tw":
                            banben = "繁体中文版";
                            break;
                        case "en":
                            banben = "英文版";
                            break;
                        case "jap":
                            banben = "日文版";
                            break;
                        case "xby":
                            banben = "西班牙";
                            break;
                        case "ru":
                            banben = "俄文";
                            break;
                        case "ko":
                            banben = "韩文";
                            break;
                        default:
                            break;
                    }
                    SaveLogs("[图文链接模块]删除图文链接：" + model.Title+"["+banben+"]");
                    //删除记录
                    bll.Delete(this.kindId,id,ver);
                }
            }
            JscriptPrint("批量删除成功啦！", "List.aspx?" + CombUrlTxt(this.classId, this.keywords, this.property) + "page=0", "Success");
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string imgurl = "", linkurl = "";
            BLL.Member bll = new BLL.Member();
            Model.Member model = new Model.Member();
            XmlDocument xmlDoc = new XmlDocument();
            string filepath = Server.MapPath("../../xml/home.xml");
            xmlDoc.Load(filepath);
            XmlNode main = xmlDoc.SelectSingleNode("main");
            main.RemoveAll();
            int classid = 289;
            string sql = " ClassId='"+classid+"' and ver='"+ver+"'";
            //DataTable dt = bll.GetList(pid, kid, ver);
            DataSet ds=bll.GetList(sql);
            imgurl=ds.Tables[0].Rows[0]["ImgUrl"].ToString();
            linkurl = ds.Tables[0].Rows[0]["FilePath"].ToString();
            XmlElement pic = xmlDoc.CreateElement("pic");
            pic.InnerText =imgurl;
            XmlElement url1 = xmlDoc.CreateElement("url1");
            url1.InnerText = "image.html";
            XmlElement url2 = xmlDoc.CreateElement("url2");
            url2.InnerText = "download.html";
            XmlElement url3 = xmlDoc.CreateElement("url3");
            url3.InnerText = "strategy.html";
            XmlElement url4 = xmlDoc.CreateElement("url4");
            url4.InnerText = linkurl;
            XmlElement url5 = xmlDoc.CreateElement("url5");
            url5.InnerText = "join.html";
            XmlElement url6 = xmlDoc.CreateElement("url6");
            url6.InnerText = "news.html";
            XmlElement url7 = xmlDoc.CreateElement("url7");
            url7.InnerText = "brand.html";
            XmlElement url7two = xmlDoc.CreateElement("url7");
            url7two.InnerText = "marketing.html";
            XmlElement url7three = xmlDoc.CreateElement("url7");
            url7three.InnerText = "contact.html";
            main.AppendChild(pic);
            main.AppendChild(url1);
            main.AppendChild(url2);
            main.AppendChild(url3);
            main.AppendChild(url4);
            main.AppendChild(url5);
            main.AppendChild(url6);
            main.AppendChild(url7);
            main.AppendChild(url7two);
            main.AppendChild(url7three);
            //foreach (DataRow dt_row in dt.Rows)
            //{
            //    XmlElement pic = xmlDoc.CreateElement("pic");
            //    XmlElement pic2 = xmlDoc.CreateElement("pic");
            //    XmlElement url = xmlDoc.CreateElement("url");
            //    XmlElement txt = xmlDoc.CreateElement("txt");
            //    pic.InnerText = dt_row["Filepath"].ToString();//小图
            //    pic2.InnerText = dt_row["ImgUrl"].ToString();
            //    url.InnerText = "/" + int.Parse(dt_row["Id"].ToString()) + "/product_list.html";
            //    txt.InnerText = dt_row["Content"].ToString();
            //    main.AppendChild(pic);
            //    main.AppendChild(pic2);
            //    main.AppendChild(url);
            //    main.AppendChild(txt);
            //}
            xmlDoc.Save(filepath);

            //Response.Write("<script defer>alert('生成成功！');location.href='/admin/SimpleLinks/List.aspx'</script>");//避免弹框后页面移位
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('生成成功！');", true);
           
        }

    }
}
