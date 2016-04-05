using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Runtime.Remoting.Contexts;
using DtCms.Common;
using System.Text;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// UploadFile 的摘要说明
    /// </summary>
    public class UploadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetNoStore();
            context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.ContentType = "application/json;charset=UTF-8";

            DtCms.Model.WebSet webset = new DtCms.BLL.WebSet().loadConfig(Utils.GetXmlMapPath("Configpath"));
            StringBuilder filePath = new StringBuilder();
            filePath.Append(webset.WebPath);
            filePath.Append(webset.WebFilePath);
            if (filePath[0] != '/') filePath.Insert(0, '/');
            if (filePath[filePath.Length - 1] != '/') filePath.Append('/');
            filePath.Append(DateTime.Now.ToString("yyyyMMdd"));
            filePath.Append('/');
            string fileFullPath = context.Server.MapPath(filePath.ToString());

            if (!Directory.Exists(fileFullPath))
            {
                Directory.CreateDirectory(fileFullPath);
            }
            string _upfilepath = context.Request.QueryString["UpFilePath"];
            HttpPostedFile upfile = context.Request.Files[_upfilepath];
            string ext = Path.GetExtension(upfile.FileName);
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + Path.GetExtension(upfile.FileName); //随机文件名
            upfile.SaveAs(fileFullPath + fileName);

            string newsfilePath = "/mimimg"+ filePath.ToString();

            string newsfileFullPath = context.Server.MapPath(newsfilePath);

            if (!Directory.Exists(newsfileFullPath))
            {
                Directory.CreateDirectory(newsfileFullPath);
            }

            upfile.SaveAs(newsfileFullPath + fileName);

            //DtCms.Common.ImageThumbnailMake.MakeThumbnail(newsfileFullPath + fileName, newsfileFullPath + fileName, 58, 45, "H");

            string serverFileName = string.Concat(filePath.ToString(), fileName);
            JSONObject json = new JSONObject();
            json.Add("FileName", serverFileName);
            context.Response.Write(json.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}