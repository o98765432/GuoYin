using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.Admin.Handler
{
    /// <summary>
    /// changeJobPaiXuAndIslock 的摘要说明
    /// </summary>
    public class changeJobPaiXuAndIslock : IHttpHandler
    {
        protected string paixuInfo;
        protected string islockInfo;
        protected string ver;
        protected string strWhere;
        protected int classid;

        protected Model.Job jobModel = new Model.Job();
        protected BLL.Job jobBll = new BLL.Job();

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
                   string[] paixuMessage =  item.Split('-');
                    if(paixuMessage[0] != "" && paixuMessage[1] != "")
                    {
                        jobModel.Id = Convert.ToInt32(paixuMessage[0]);
                        jobModel.SortId = Convert.ToInt32(paixuMessage[1]);
                        jobModel.Ver = ver;

                        strWhere = " SortId = '" + jobModel.SortId + "' ";
                        jobBll.UpdateField(jobModel.Id, strWhere);
                    }
                   
                }

                string[] islock = islockInfo.Split('|');

                foreach (var item in islock)
                {
                    string[] islockMessage = item.Split('-');

                    if(islockMessage[0] != "" && islockMessage[1] != "")
                    {

                        jobModel.Id = Convert.ToInt32(islockMessage[0]);
                        if (islockMessage[1] == "false")
                        {
                            jobModel.IsLock = 0;
                        }
                        else
                        {
                            jobModel.IsLock = 1;
                        }

                        jobModel.Ver = ver;

                        strWhere = " IsLock = '"+jobModel.IsLock+"' ";
                        jobBll.UpdateField(jobModel.Id,strWhere);
                    }
                    

                }

                context.Response.Redirect("../Job/List.aspx?classId=" + this.classid + "" + "&showtype=1");
                
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