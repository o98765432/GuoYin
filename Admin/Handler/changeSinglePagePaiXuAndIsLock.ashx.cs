using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.Admin.Handler
{
    /// <summary>
    /// changeSinglePagePaiXuAndIsLock 的摘要说明
    /// </summary>
    public class changeSinglePagePaiXuAndIsLock : IHttpHandler
    {
        protected string paixuInfo;
        protected string islockInfo;
        protected string ver;
        protected string strWhere;
        protected int classid;

        protected Model.SinglePage singleModel = new Model.SinglePage();
        protected BLL.SinglePage singleBll = new BLL.SinglePage();

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
                if (context.Request.QueryString["ver"].ToString() != "" && context.Request.QueryString["ver"] != null)
                {
                    ver = context.Request.QueryString["ver"].ToString();
                }

                string[] paixu = paixuInfo.Split('|');

                foreach (var item in paixu)
                {
                    string[] paixuMessage = item.Split('-');
                    if (paixuMessage[0] != "" && paixuMessage[1] != "")
                    {
                        singleModel.Id = Convert.ToInt32(paixuMessage[0]);
                        singleModel.SortId = Convert.ToInt32(paixuMessage[1]);
                        singleModel.Ver = ver;

                        strWhere = " SortId = '" + singleModel.SortId + "' ";
                        singleBll.UpdateField(singleModel.Id, strWhere);
                    }

                }

                string[] islock = islockInfo.Split('|');

                foreach (var item in islock)
                {
                    string[] islockMessage = item.Split('-');

                    if (islockMessage[0] != "" && islockMessage[1] != "")
                    {

                        singleModel.Id = Convert.ToInt32(islockMessage[0]);
                        if (islockMessage[1] == "false")
                        {
                            singleModel.IsLock = 0;
                        }
                        else
                        {
                            singleModel.IsLock = 1;
                        }

                        singleModel.Ver = ver;

                        strWhere = " IsLock = '" + singleModel.IsLock + "' ";
                        singleBll.UpdateField(singleModel.Id, strWhere);
                    }


                }

                context.Response.Redirect("../SinglePage/List.aspx?classId=" + this.classid + "&showtype=1");

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