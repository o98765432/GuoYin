using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// fileDownload 的摘要说明
    /// </summary>
    public class fileDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context != null)
            {
                HttpRequest request = context.Request;
                HttpResponse response = context.Response;
                //本文件使用了 QueryString 来传递文件名，你也可以不使用
                if (!string.IsNullOrEmpty(context.Request.QueryString["file"]))
                {
                    //取得客户端正在请求的文件的物理路径
                    //不使用 QueryString 时，你可以使用 request.PhysicalPath 获取
                    string path = context.Server.MapPath("~/") +
                        context.Server.UrlDecode(context.Request.QueryString["file"]).Replace("/", "\\").ToLower();
                    if (File.Exists(path))
                    {
                        string extension = Path.GetExtension(path);
                        response.ContentType = GetMimeType(extension);
                        string fileName = System.IO.Path.GetFileName(path);
                        if (request.UserAgent.ToLower().IndexOf("msie") > -1)
                        {
                            //当客户端使用IE时，对其进行编码；We should encode the filename when our visitors use IE
                            //使用 ToHexString 代替传统的 UrlEncode()；We use "ToHexString" replaced "context.Server.UrlEncode(fileName)"
                            fileName = ToHexString(fileName);
                        }
                        if (request.UserAgent.ToLower().IndexOf("firefox") > -1)
                        {
                            //为了向客户端输出空格，需要在当客户端使用 Firefox 时特殊处理
                            //we should do some special work whem our visitor has a firefox browser
                            response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                        }
                        else
                            response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                        response.WriteFile(path);
                        response.End();
                        return;
                    }
                }
            }
            //正在请求的文件不存在；Cannot find the specified file
            context.Response.Clear();
            context.Response.Write("the data you are wanting to get does not exsit.");
            context.Response.End();
        }

        #region 编码

        /// <summary>
        /// Encodes non-US-ASCII characters in a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Determines if the character needs to be encoded.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";

            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;

            return true;
        }

        /// <summary>
        /// Encodes a non-US-ASCII character.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }

            return builder.ToString();
        }


        #endregion


        /// <summary>
        /// 根据文件后缀来获取MIME类型字符串
        /// </summary>
        /// <param name="extension">文件后缀</param>
        /// <returns></returns>
        static string GetMimeType(string extension)
        {
            string mime = string.Empty;
            extension = extension.ToLower();
            switch (extension)
            {
                case ".avi": mime = "video/x-msvideo"; break;
                case ".bin":
                case ".exe":
                case ".msi":
                case ".dll":
                case ".class": mime = "application/octet-stream"; break;
                case ".csv": mime = "text/comma-separated-values"; break;
                case ".html":
                case ".htm":
                case ".shtml": mime = "text/html"; break;
                case ".css": mime = "text/css"; break;
                case ".js": mime = "text/javascript"; break;
                case ".doc":
                case ".dot":
                case ".docx": mime = "application/msword"; break;
                case ".xla":
                case ".xls":
                case ".xlsx": mime = "application/msexcel"; break;
                case ".ppt":
                case ".pptx": mime = "application/mspowerpoint"; break;
                case ".gz": mime = "application/gzip"; break;
                case ".gif": mime = "image/gif"; break;
                case ".bmp": mime = "image/bmp"; break;
                case ".jpeg":
                case ".jpg":
                case ".jpe":
                case ".png": mime = "image/jpeg"; break;
                case ".mpeg":
                case ".mpg":
                case ".mpe":
                case ".wmv": mime = "video/mpeg"; break;
                case ".mp3":
                case ".wma": mime = "audio/mpeg"; break;
                case ".pdf": mime = "application/pdf"; break;
                case ".rar": mime = "application/octet-stream"; break;
                case ".txt": mime = "text/plain"; break;
                case ".7z":
                case ".z": mime = "application/x-compress"; break;
                case ".zip": mime = "application/x-zip-compressed"; break;
                default:
                    mime = "application/octet-stream";
                    break;
            }
            return mime;
        }


        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}