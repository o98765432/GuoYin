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
    public partial class Add : DtCms.Web.UI.ManagePage
    {
        protected int classid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["classid"].ToString() != "" && Request.QueryString["classid"] != null)
            {
                classid = Convert.ToInt32(Request.QueryString["classid"]);
            }

            if (!Page.IsPostBack)
            {

                btnSave.Visible = addflag;
                chkLoginLevel("addProductDownloads");
                //绑定类别

                ChannelTreeBind(classid, "请选择所属类别...", (int)Channel.Product, this.ddlClassId, "cn");
               // ChannelTreeBind(0, "请选择所属类别...", (int)Channel.Product, this.ddlClassId,"cn");
                if (!string.IsNullOrEmpty(Request.Params["classId"]))
                {
                    ddlClassId.SelectedValue = Request.Params["classId"].Trim();
                }
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
            DtCms.Model.ProductDownloads model = new DtCms.Model.ProductDownloads();
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
            model.Title = txtTitle.Text.Trim();
            if (this.ddlProductId.Items.Count > 0)
            {
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
                bll.Add(model);

                //string banben = "";
                //switch (Session["ver"].ToString())
                //{
                //    case "cn":
                //        banben = "中文版";
                //        break;
                //    case "en":
                //        banben = "英文版";
                //        break;
                //    case "jap":
                //        banben = "日文版";
                //        break;
                //    case "xby":
                //        banben = "西班牙";
                //        break;
                //    case "ru":
                //        banben = "俄文";
                //        break;\
                
                //    case "ko":
                //        banben = "韩文";
                //        break;
                //    default:
                //        break;
                //}
                //保存日志
                SaveLogs("[下载模块]添加下载：" + model.Title);
                JscriptPrint("增加下载成功啦！", "List.aspx?classId=" + ddlClassId.SelectedValue, "Success");
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
            this.ddlProductId.DataSource = bll.GetList(" classid=" + int.Parse(this.ddlClassId.SelectedValue));
            this.ddlProductId.DataTextField = "Title";
            this.ddlProductId.DataValueField = "Id";
            this.ddlProductId.DataBind();
           
        }
    }
}
