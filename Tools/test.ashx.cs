﻿using System;
using System.Collections.Generic;
using System.Web;
using DtCms.Web.UI;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// test 的摘要说明
    /// </summary>
    public class test : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
             
            string _refilepath = context.Request.QueryString["ReFilePath"]; //取得返回的对象名称
            string _upfilepath = context.Request.QueryString["UpFilePath"]; //取得上传的对象名称
            int _iswater; //默认打水印
            if (!int.TryParse(context.Request.QueryString["IsWater"] as string, out _iswater))
            {
                _iswater = 1;
            }
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            string _delfile = context.Request.Params[_refilepath];

            if (_upfile == null)
            {
                context.Response.Write("{msg: 0, msbox: \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _iswater);
            //删除已存在的旧文件

            //返回成功信息
            context.Response.Write(msg);
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