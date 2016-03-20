using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.Admin.Handler
{
    /// <summary>
    /// changePictruePaiXuAndIsLock 的摘要说明
    /// </summary>
    public class changePictruePaiXuAndIsLock : IHttpHandler
    {
        protected string paixuInfo;
        protected string islockInfo;
        protected string ver;
        protected string strWhere;
        protected int classid;

        protected Model.Pictures pictureModel = new Model.Pictures();
        protected BLL.Pictures pictureBll = new BLL.Pictures();

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
                        pictureModel.Id = Convert.ToInt32(paixuMessage[0]);
                        pictureModel.SortId = Convert.ToInt32(paixuMessage[1]);
                        pictureModel.Ver = ver;

                        strWhere = " SortId = '" + pictureModel.SortId + "' ";
                        pictureBll.UpdateField(pictureModel.Id, strWhere);
                    }

                }

                string[] islock = islockInfo.Split('|');

                foreach (var item in islock)
                {
                    string[] islockMessage = item.Split('-');

                    if (islockMessage[0] != "" && islockMessage[1] != "")
                    {

                        pictureModel.Id = Convert.ToInt32(islockMessage[0]);
                        if (islockMessage[1] == "false")
                        {
                            pictureModel.IsLock = 0;
                        }
                        else
                        {
                            pictureModel.IsLock = 1;
                        }

                        pictureModel.Ver = ver;

                        strWhere = " IsLock = '" + pictureModel.IsLock + "' ";
                        pictureBll.UpdateField(pictureModel.Id, strWhere);
                    }


                }

                context.Response.Redirect("../Pictures/List.aspx?classId=" + this.classid + "" + "&showtype=1");

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