using System;
using System.Data;
using System.Web;
using DtCms.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace DtCms.Web.Admin.ShowChanner
{
    public partial class edit : ManagePage
    {
        private DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
        protected DtCms.Model.Channel model = new DtCms.Model.Channel();
        public int kindId; //种类
        public int classId;    //ID
        protected string path = "Article";
        public int pId;
        public int ClassLayer=0;//级别 
        protected int leftPageModule;
        protected int contentPageModule;
        protected string leftimg = string.Empty;
        protected string rightimg = string.Empty;
        protected string rightcontentimg = string.Empty;
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
            if (int.TryParse(Request.Params["classId"], out classId))
            {

            }
            else
            {
                classId = 0;
            }

            if (int.TryParse(Request.Params["kindId"], out kindId))
            {

            }
            else
            {
                kindId = 0;
            }


            if (!Page.IsPostBack)
            {




                int shownowid = DtCms.Common.Utils.returnIntByString(Request.QueryString["shownowid"]);


                Model.Channel kindchannel = new Model.Channel();

                kindchannel.Title = "设为一级栏目";

                kindchannel.Id = 0;

                if (kindId > 0)
                {

                    kindchannel = new BLL.Channel().GetModel(kindId);
                }

                Model.Channel channelmodel = new BLL.Channel().GetModel(shownowid);

                model = bll.GetModel(classId, "cn");


                if (kindId == 0 && classId == 0 && shownowid == 0)
                {
                    this.ddlClassId.Items.Add(new ListItem("设为一级栏目", "0"));

                }
                else
                {

                    if (channelmodel != null)
                    {

                        ChannelTreeBind(kindId, kindchannel.Title, kindchannel.Id, this.ddlClassId, "cn");
                    }
                    else if (model != null)
                    {
                        if (model.ParentId == 0)
                        {
                            this.ddlClassId.Items.Add(new ListItem("设为一级栏目", "0"));
                        }
                        else
                        {

                            if (kindId == classId)
                            {

                                this.ddlClassId.Items.Add(new ListItem(kindchannel.Title, kindchannel.ParentId + ""));
                            }
                            else
                            {

                                ChannelTreeBind(kindId, kindchannel.Title, kindchannel.Id, this.ddlClassId, "cn");

                            }

                        }

                    }

                }




                if (!string.IsNullOrEmpty(Request.QueryString["shownowid"]))
                {

                    this.ddlClassId.SelectedValue = Request.QueryString["shownowid"];

                }

                if (model != null)
                {
                    ShowInfo();
                }
            }

        }




        /// <summary>
        /// 绑定数据
        /// </summary>
        private void ShowInfo()
        {

            this.txtTitle.Text = model.Title;
            this.txtPageUrl.Text = model.PageUrl;
            this.txtSortId.Text = model.SortId.ToString();
            this.txtImgUrl.Text = model.ImgUrl;
            this.txtContent.Value = model.Content;

            this.txtKeyword.Text = model.Keyword;
            this.txtZhaiyao.Text = model.Description;
            this.txtFilepath.Text = model.Filepath;
            txtWebPath.Text = model.WebPath;
            this.txtwidth.Text = model.width + "";
            this.txtheight.Text = model.height + "";

            this.txtWebIndex.Text = model.WebIndex;
            

            if (model.FrontDeskDisplay == 0)
            {

                this.FrontDeskDisplay.Checked = false;
            }

            if (model.LeftDisplay == 0)
            {

                this.LeftDisplay.Checked = false;
            }


            txtHtmlSize.Text = model.HtmlSize;

            ddlClassId.SelectedValue = model.ParentId.ToString();

            this.DrpPageType.SelectedValue = model.PageType;
        }


        //保存修改
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                model.Title = txtTitle.Text.Trim();
                model.PageUrl = txtPageUrl.Text.Trim();
                model.SortId = int.Parse(txtSortId.Text.Trim());
                model.Content = this.txtContent.Value.Trim();
                model.ImgUrl = this.txtImgUrl.Text.Trim();
                model.Filepath = this.txtFilepath.Text.Trim();
                model.Ver = Session["ver"].ToString();

                if (this.txtKeyword.Text.Trim() == "")
                {
                    model.Keyword = this.txtTitle.Text;
                }
                else
                {
                    model.Keyword = this.txtKeyword.Text.Trim();
                }

                model.Description = this.txtZhaiyao.Text.Trim();


                int parentId = int.Parse(this.ddlClassId.SelectedValue);

                model.ParentId = parentId;
                model.KindId = showkindid;
                model.Id = classId;

                int classLayer = 1;                                         //栏目深度
                string classList = "";

                if (parentId > 0)
                {
                    DataSet ds = bll.GetChannelListByClassId(parentId, Session["ver"].ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        classList = dr["ClassList"].ToString().Trim() + kindId + ",";
                        classLayer = Convert.ToInt32(dr["ClassLayer"]) + 1;
                    }
                }
                else
                {
                    classList = "," + kindId + ",";
                    classLayer = 1;
                    if (kindId == classId && kindId != 0)
                    {
                        classLayer = 2;

                    }

                }

                model.ClassLayer = classLayer;

                model.ClassList = classList;

                model.width = DtCms.Common.Utils.returnIntByString(this.txtwidth.Text);

                model.height = DtCms.Common.Utils.returnIntByString(this.txtheight.Text);

                model.WebPath = txtWebPath.Text.Trim();

                model.HtmlSize = this.txtHtmlSize.Text;

                model.WebIndex = this.txtWebIndex.Text;

                model.PageType = this.DrpPageType.SelectedValue;

                if (this.LeftDisplay.Checked)
                {

                    model.LeftDisplay = 1;

                }
                if (this.FrontDeskDisplay.Checked)
                {

                    model.FrontDeskDisplay = 1;
                }

                if (classId == 0)
                {


                    classId = bll.Add(model);

                    if (pId == 0 && showkindid == 1)
                    {
                        model.PageType = "9";
                        model.ParentId = classId;
                        model.Id = 0;
                        model.ClassLayer = 2;
                        model.ClassList += "1,";
                        model.KindId = 1;
                        model.width = 800;
                        model.height = 520; 
                        model.Title = "Banner广告图";  
                        model.Id= new BLL.Channel().Add(model);
                        bll.Update(model);

                        model.PageType = "2";
                        model.KindId = 2;
                        model.width = 0;
                        model.height =0; 
                        model.Title = "新闻中心";
                        model.ParentId = classId;
                        model.WebPath = "ShowNews";
                        model.HtmlSize = 4+"";
                        int shownews = new BLL.Channel().Add(model);
                        model.Id = shownews;
                        bll.Update(model);
                        if (shownews > 0) {

                            model.PageType = "2";
                            model.KindId = 3;
                            model.Title = "名人艺术沙龙";
                            model.ParentId = shownews;
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            new BLL.Channel().Add(model);

                            model.PageType = "2";
                            model.KindId = 3;
                            model.Title = "祭孔大典";
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            model.ParentId = shownews;
                            new BLL.Channel().Add(model);


                            model.PageType = "2";
                            model.KindId = 3;
                            model.Title = "儒家高峰论坛";
                            model.ParentId = shownews;
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            new BLL.Channel().Add(model);

                            model.PageType = "2";
                            model.KindId = 3;
                            model.Title = "慈善拍卖晚宴";
                            model.ParentId = shownews;
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            new BLL.Channel().Add(model);

                        }
                         

                        model.PageType = "2";
                        model.KindId = 4;
                        model.Title = "文化节照片";
                        model.ParentId = classId;
                        model.WebPath = "ShowPs";
                        model.width = 244;
                        model.height = 183;
                        model.HtmlSize = 6 + "";
                        shownews = new BLL.Channel().Add(model);
                        model.Id = shownews;
                        bll.Update(model);
                        if (shownews > 0)
                        {

                            model.PageType = "2";
                            model.KindId = 5;
                            model.Title = "名人艺术沙龙";
                            model.ParentId = shownews;
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            new BLL.Channel().Add(model);

                            model.PageType = "2";
                            model.KindId = 5;
                            model.Title = "祭孔大典";
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            model.ParentId = shownews;
                            new BLL.Channel().Add(model);


                            model.PageType = "2";
                            model.KindId = 5;
                            model.Title = "儒家高峰论坛";
                            model.ParentId = shownews;
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            new BLL.Channel().Add(model);

                            model.PageType = "2";
                            model.KindId = 5;
                            model.Title = "慈善拍卖晚宴";
                            model.ParentId = shownews;
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            new BLL.Channel().Add(model);

                        }


                        model.PageType = "12";
                        model.KindId = 6;
                        model.Title = "视频";
                        model.ParentId = classId;
                        model.WebPath = "ShowVideo";
                        model.width = 326;
                        model.height = 230;
                        shownews = new BLL.Channel().Add(model);
                        model.Id = shownews;
                        model.HtmlSize = 1000 + "";
                        bll.Update(model);
                        if (shownews > 0)
                        {

                            model.PageType = "12";
                            model.KindId = 7;
                            model.Title = "名人艺术沙龙";
                            model.ParentId = shownews;
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            new BLL.Channel().Add(model);

                            model.PageType = "12";
                            model.KindId = 7;
                            model.Title = "祭孔大典";
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            model.ParentId = shownews;
                            new BLL.Channel().Add(model);


                            model.PageType = "12";
                            model.KindId = 7;
                            model.Title = "儒家高峰论坛";
                            model.ParentId = shownews;
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            new BLL.Channel().Add(model);

                            model.PageType = "12";
                            model.KindId = 7;
                            model.Title = "慈善拍卖晚宴";
                            model.ParentId = shownews;
                            model.ClassLayer = 3;
                            model.ClassList = "1,1,1,";
                            new BLL.Channel().Add(model);

                        }



                    }



                    //保存日志
                    SaveLogs("[栏目类别]添加类别：" + model.Title);

                    Response.Write("<script type='text/javascript'>alert('添加成功');location.href='List.aspx?kindId=" + kindId + "&path=" + path + "&showkindid=" + showkindid + "'</script>");
                    Response.End();

                }
                else
                {

                    bll.Update(model);

                    //保存日志
                    SaveLogs("[栏目类别]修改类别：" + model.Title);

                    Response.Write("<script type='text/javascript'>alert('修改成功');location.href='List.aspx?kindId=" + kindId + "&path=" + path + "&showkindid=" + showkindid + "'</script>");
                    Response.End();


                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



    }
}
