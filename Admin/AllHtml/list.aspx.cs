using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
namespace DtCms.Web.Admin.AllHtml
{
    public partial class list : DtCms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // GetAllFilesInDirectory(Server.MapPath("~/Admin/Template"));
        }

        public string GetAllFilesInDirectory(string strDirectory,int leg)
        {

            StringBuilder bur = new StringBuilder();

            List<FileInfo> listFiles = new List<FileInfo>(); //保存所有的文件信息  
            DirectoryInfo directory = new DirectoryInfo(strDirectory);
            DirectoryInfo[] directoryArray = directory.GetDirectories();
            FileInfo[] fileInfoArray = directory.GetFiles();
            if (fileInfoArray.Length > 0) listFiles.AddRange(fileInfoArray);
            foreach (DirectoryInfo _directoryInfo in directoryArray)
            {


                bur.AppendFormat("<tr><td style=\"text-indent:" + 25 * leg + "px;\"><img src=\"/admin/webimages/file.png\"  height=\"20\">" + _directoryInfo.Name + "</td><td>" + _directoryInfo.FullName+ "</td><td align=\"center\"></td></tr>\n");

             
                
                bur.Append(GetAllFilesInDirectory(_directoryInfo.FullName,leg+1));//递归遍历  
 

            }
            foreach (FileInfo fileinfo in fileInfoArray)
            {


                bur.AppendFormat("<tr><td style=\"text-indent:" + 25 * leg + "px;\"><img src=\"/admin/webimages/txt.png\"  height=\"20\">" + fileinfo.Name + "</td><td>" + fileinfo.FullName + "</td><td align=\"center\"><a href=\"edit.aspx?filepath=" + fileinfo.FullName + "\">修改</a></td></tr>\n");

            }
            return bur.ToString();
        }
    }
}