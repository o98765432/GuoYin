using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;

using DtCms.Common;
using DtCms.Web.UI;
namespace DtCms.Web.Admin.swfupload
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            System.Drawing.Image thumbnail_image = null;
            System.Drawing.Image original_image = null;
            System.Drawing.Bitmap final_image = null;
            System.Drawing.Graphics graphic = null;
            MemoryStream ms = null;

            try
            {
                // Get the data
             //   HttpPostedFile jpeg_image_upload = Request.Files["Filedata"];

              //  string _refilepath = context.Request.QueryString["ReFilePath"]; //ȡ�÷��صĶ�������
              //  string _upfilepath = context.Request.QueryString["UpFilePath"]; //ȡ���ϴ��Ķ�������
             //   int _iswater; //Ĭ�ϴ�ˮӡ
                //if (!int.TryParse(context.Request.QueryString["IsWater"] as string, out _iswater))
               // {
               //     _iswater = 1;
              //  }
                int _iswater = 1; //Ĭ�ϴ�ˮӡ
                HttpPostedFile _upfile = Request.Files["Filedata"];
               // string _delfile = context.Request.Params[_refilepath];

                if (_upfile == null)
                {
                    Response.Write("{msg: 0, msbox: \"��ѡ��Ҫ�ϴ��ļ���\"}");
                    return;
                }
                UpLoad upFiles = new UpLoad();
                string msg = upFiles.fileSaveAs3(_upfile, _iswater);
                //ɾ���Ѵ��ڵľ��ļ�
              //  if (!string.IsNullOrEmpty(_delfile))
              //  {
              //      string _filename = Utils.GetMapPath(_delfile);
             //       if (File.Exists(_filename))
             //       {
            //            File.Delete(_filename);
            //        }
            //    }

                
            

                Response.StatusCode = 200;
                //Response.Write(thumbnail_id);
                Response.Write(msg);
            }
            catch
            {
                // If any kind of error occurs return a 500 Internal Server error
                Response.StatusCode = 500;
                Response.Write("An error occured");
                Response.End();
            }
            finally
            {
                // Clean up
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
                Response.End();
            }

        }
    }
}
