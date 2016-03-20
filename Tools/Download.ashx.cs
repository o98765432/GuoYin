using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using DtCms.Common;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// 文件下载
    /// </summary>
    public class Download : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int _id;
            //获得下载ID
            if (!int.TryParse(context.Request.Params["id"] as string, out _id))
            {
                context.Response.Redirect("/error.aspx");
                return;
            }
            //检查下载记录是否存在
            DtCms.BLL.Downloads bll = new DtCms.BLL.Downloads();
            if (!bll.Exists(_id))
            {
                context.Response.Redirect("/error.aspx");
                return;
            }
            //下载次数+1
            bll.UpdateField(_id, "DownNum=DownNum+1");
            //取得文件绝对路径
            string filePath = bll.GetModel(_id).FilePath;
            //检查文件本地还是远程
            if (filePath.ToLower().StartsWith("http://"))
            {
                context.Response.Redirect(filePath);
                return;
            }
            else
            {
                //取得文件物理路径
                string fullFileName = Utils.GetMapPath(filePath);
                if (!File.Exists(fullFileName))
                {
                    context.Response.Redirect("/error.aspx");
                    return;
                }
                FileInfo file = new FileInfo(fullFileName);//路径
                context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8"); //解决中文乱码
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file.Name)); //解决中文文件名乱码    
                context.Response.AddHeader("Content-length", file.Length.ToString());
                context.Response.ContentType = "application/pdf";
                context.Response.WriteFile(file.FullName);
                context.Response.End();
            }

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
