using System;
using System.IO;
using System.Net;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Product_Downloads
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        protected int drpClassId;
        protected string ver = "cn";
        protected string classList;
        protected BLL.ProductDownloads proDownload = new BLL.ProductDownloads();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要修改的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            DataSet ds = proDownload.GetClassListById(Id, ver);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    classList = ds.Tables[0].Rows[i]["ClassList"].ToString();
                }

                this.drpClassId = Convert.ToInt32(classList.Substring(1, 3));
            }
            if (!Page.IsPostBack)
            {
                btnSave.Visible = updateflag; 
                chkLoginLevel("editProductDownloads");
                //绑定类别
                ChannelTreeBind(this.drpClassId, "请选择所属类别...", (int)Channel.Product, this.ddlClassId, "cn");
                //ChannelTreeBind(0, "请选择所属类别...", (int)Channel.Product, this.ddlClassId,"cn");
                
                ShowInfo(this.Id);

                 this.ddlClassId.SelectedValue = Request.QueryString["classid"];

                 DtCms.BLL.Product bll = new DtCms.BLL.Product();
                 this.ddlProductId.DataSource = bll.GetList(" classid=" + int.Parse(this.ddlClassId.SelectedValue));
                 this.ddlProductId.DataTextField = "Title";
                 this.ddlProductId.DataValueField = "Id";
                 this.ddlProductId.DataBind();

                 this.ddlProductId.SelectedValue = Request.QueryString["productid"];
            }
        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.ProductDownloads bll = new DtCms.BLL.ProductDownloads();
            DtCms.Model.ProductDownloads model = bll.GetModel(_id);

            txtTitle.Text = model.Title;
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtImgUrl.Text = model.ImgUrl;
            txtFilePath.Text = model.FilePath;
            txtContent.Value = model.Content;
            txtClick.Text = model.Click.ToString();
            txtDownNum.Text = model.DownNum.ToString();
            //if (model.IsMsg == 1)
            //{
            //    cblItem.Items[0].Selected = true;
            //}
            //if (model.IsRed == 1)
            //{
            //    cblItem.Items[1].Selected = true;
            //}
            if (model.IsLock == 1)
            {
                cblItem.Items[0].Selected = true;
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text.Trim();
            if (string.IsNullOrEmpty(filePath))
            {
                JscriptMsg(350, 230, "错误提示", "<b>请先上传文件！</b>上传文件不存在，请先上传文件再进行提交吧！", "", "Error");
                return;
            }

            DtCms.BLL.ProductDownloads bll = new DtCms.BLL.ProductDownloads();
            DtCms.Model.ProductDownloads model = bll.GetModel(this.Id);
            //检查文件本地还是远程
            if (filePath.ToLower().StartsWith("http://"))
            {
                try
                {
                    HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(filePath);
                    HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
                    model.FileSize = ((int)_response.ContentLength) / 1024;
                    model.FileType = filePath.Substring(filePath.LastIndexOf(".") + 1).ToUpper();
                }
                catch
                {
                    JscriptMsg(350, 230, "错误提示", "<b>远程文件不存在！</b>远程文件不存在，请先检查后再进行提交吧！", "", "Error");
                    return;
                }
            }
            else
            {
                string fullFilePath = Server.MapPath(filePath);
                if (!File.Exists(fullFilePath))
                {
                    JscriptMsg(350, 230, "错误提示", "<b>上传文件不存在！</b>上传文件不存在，请先上传文件再进行提交吧！", "", "Error");
                    return;
                }
                FileInfo upfile = new FileInfo(fullFilePath);
                model.FileType = (Path.GetExtension(fullFilePath).Substring(1)).ToUpper();
                model.FileSize = ((int)upfile.Length) / 1024;
            }
            if (this.ddlProductId.Items.Count > 0)
            {
                model.Title = txtTitle.Text.Trim();
                model.ClassId = int.Parse(ddlProductId.SelectedValue);
                model.ImgUrl = txtImgUrl.Text.Trim();
                model.FilePath = txtFilePath.Text.Trim();
                model.Content = txtContent.Value;
                model.Click = int.Parse(txtClick.Text.Trim());
                model.DownNum = int.Parse(txtDownNum.Text.Trim());
                model.IsMsg = 0;
                model.IsRed = 0;
                model.IsLock = 0;
                model.Ver = Session["ver"].ToString();
                //if (cblItem.Items[0].Selected == true)
                //{
                //    model.IsMsg = 1;
                //}
                //if (cblItem.Items[1].Selected == true)
                //{
                //    model.IsRed = 1;
                //}
                if (cblItem.Items[0].Selected == true)
                {
                    model.IsLock = 1;
                }
                bll.Update(model);
                //保存日志
                SaveLogs("[下载模块]编辑下载：" + model.Title);

                JscriptPrint("编辑下载成功啦！", "List.aspx", "Success");
            }
            else
            {
                JscriptMsg(350, 230, "错误提示", "请选择产品类别", "", "Error");
                return;
            }
        }

        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            DtCms.BLL.Product bll = new DtCms.BLL.Product();
            this.ddlProductId.DataSource = bll.GetList(" classid=" + int.Parse(this.ddlClassId.SelectedValue)+" and ver='"+Session["ver"].ToString()+"'");
            this.ddlProductId.DataTextField = "Title";
            this.ddlProductId.DataValueField = "Id";
            this.ddlProductId.DataBind();

           
        }

    }
}
