using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.Admin.Handler
{
    /// <summary>
    /// changeFriendShipPaiXuAndIsLock 的摘要说明
    /// </summary>
    public class changeFriendShipPaiXuAndIsLock : IHttpHandler
    {
        protected string paixuInfo;
        protected string islockInfo;
        protected string ver;
        protected string strWhere;
        protected int classid;

        protected Model.FriendshipLink shipModel = new Model.FriendshipLink();
        protected BLL.FriendshipLink shipBll = new BLL.FriendshipLink();


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
                        shipModel.Id = Convert.ToInt32(paixuMessage[0]);
                        shipModel.SortId = Convert.ToInt32(paixuMessage[1]);
                        shipModel.Ver = ver;

                        strWhere = " SortId = '" + shipModel.SortId + "' ";
                        shipBll.UpdateField(shipModel.Id, strWhere);
                    }

                }

                string[] islock = islockInfo.Split('|');

                foreach (var item in islock)
                {
                    string[] islockMessage = item.Split('-');

                    if (islockMessage[0] != "" && islockMessage[1] != "")
                    {

                        shipModel.Id = Convert.ToInt32(islockMessage[0]);
                        if (islockMessage[1] == "false")
                        {
                            shipModel.IsLock = 0;
                        }
                        else
                        {
                            shipModel.IsLock = 1;
                        }

                        shipModel.Ver = ver;

                        strWhere = " IsLock = '" + shipModel.IsLock + "' ";
                        shipBll.UpdateField(shipModel.Id, strWhere);
                    }


                }

                context.Response.Redirect("../FriendshipLink/List.aspx?classId=" + this.classid + "" + "&showtype=1");

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