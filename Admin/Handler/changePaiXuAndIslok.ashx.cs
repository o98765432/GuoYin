using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace DtCms.Web.Admin.Handler
{
    /// <summary>
    /// changePaiXuAndIslok 的摘要说明
    /// </summary>
    public class changePaiXuAndIslok : IHttpHandler
    {
        protected string paixuInfo;
        protected string isLockInfo;
        protected string ver;
        protected string classId;
        protected bool flag;

        protected BLL.Product productBLL = new BLL.Product();
        protected Model.Product prodouctModel = new Model.Product();
        

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

            try 
            {
                if (context.Request.QueryString["paixuInfo"].ToString() != "" && context.Request.QueryString["paixuInfo"] != null)
                {
                    paixuInfo = context.Request.QueryString["paixuInfo"].ToString();
                }
                if (context.Request.QueryString["isLockInfo"].ToString() != "" && context.Request.QueryString["isLockInfo"] != null)
                {
                    isLockInfo = context.Request.QueryString["isLockInfo"].ToString();
                }
                if (context.Request.QueryString["classId"].ToString() != "" && context.Request.QueryString["classId"] != null)
                {
                    classId = context.Request.QueryString["classId"].ToString();
                }
                if(context.Request.QueryString["ver"].ToString() !="" && context.Request.QueryString["ver"] != null)
                {
                    ver = context.Request.QueryString["ver"].ToString();
                }
                string[] paixu = paixuInfo.Split('|');

                foreach (string s in paixu)
                {
                    string[] paixuId = s.Split('-');

                    if (paixuId[0] != "" && paixuId[1] != "")
                    {
                        prodouctModel.Id = Convert.ToInt32(paixuId[0]);
                        prodouctModel.SortId = Convert.ToInt32(paixuId[1]);
                        prodouctModel.Ver = ver;

                        productBLL.updateProductsortId(prodouctModel);
                    }
                }



                changeIsLock(context);

                context.Response.Redirect("../Product/List.aspx?classId=" + this.classId + "" + "&showtype=1");
             
            }catch(Exception ex)
            {
                throw ex;
            }   
        }


        public void changeIsLock(HttpContext context)
        {
            try 
            {
                string[] lockInfo = isLockInfo.Split('|');

                foreach (string s in lockInfo)
                {
                    string[] IslockId = s.Split('-');

                    if(IslockId[0] != "" && IslockId[1] != null)
                    {
                        prodouctModel.Id = Convert.ToInt32(IslockId[0]);

                        if (IslockId[1] == "false")
                        {
                            prodouctModel.IsLock = 0;
                        }
                        else
                        {
                            prodouctModel.IsLock = 1;
                        }
                        prodouctModel.Ver = ver;

                        productBLL.updateProductIsLock(prodouctModel);
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
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