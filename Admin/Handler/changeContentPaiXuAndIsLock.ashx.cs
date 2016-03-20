using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.Admin.Handler
{
    /// <summary>
    /// changeContentPaiXuAndIsLock 的摘要说明
    /// </summary>
    public class changeContentPaiXuAndIsLock : IHttpHandler
    {
        protected string paixuInfo;
        protected string islockInfo;
        protected string ver;
        protected string strWhere;
        protected int classid;

        protected Model.Contents contentModel = new Model.Contents();
        protected BLL.Contents contentBll = new BLL.Contents();

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
                if (context.Request.QueryString["islockInfo"].ToString() != "" && context.Request.QueryString["islockInfo"] != null)
                {
                    islockInfo = context.Request.QueryString["islockInfo"].ToString();
                }
                if (context.Request.QueryString["classId"].ToString() != "" && context.Request.QueryString["classId"] != null)
                {
                    classid = Convert.ToInt32(context.Request.QueryString["classId"].ToString());
                }
                if (context.Request.QueryString["vers"].ToString() != "" && context.Request.QueryString["vers"] != null)
                {
                    ver = context.Request.QueryString["vers"].ToString();
                }

                string[] paixu = paixuInfo.Split('|');

                foreach (var item in paixu)
                {
                    string[] paixuMessage = item.Split('-');
                    if (paixuMessage[0] != "" && paixuMessage[1] != "")
                    {
                        contentModel.Id = Convert.ToInt32(paixuMessage[0]);
                        contentModel.SortId = Convert.ToInt32(paixuMessage[1]);
                        contentModel.Ver = ver;

                        strWhere = " SortId = '" + contentModel.SortId + "' ";
                        contentBll.UpdateField(contentModel.Id, strWhere);
                    }

                }

                string[] islock = islockInfo.Split('|');

                foreach (var item in islock)
                {
                    string[] islockMessage = item.Split('-');

                    if (islockMessage[0] != "" && islockMessage[1] != "")
                    {

                        contentModel.Id = Convert.ToInt32(islockMessage[0]);
                        if (islockMessage[1] == "false")
                        {
                            contentModel.IsLock = 0;
                        }
                        else
                        {
                            contentModel.IsLock = 1;
                        }

                        contentModel.Ver = ver;

                        strWhere = " IsLock = '" + contentModel.IsLock + "' ";
                        contentBll.UpdateField(contentModel.Id, strWhere);
                    }


                }

                context.Response.Redirect("../Contents/List.aspx?classId=" + this.classid + "" + "&showtype=1");

            }
            catch (Exception ex)
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