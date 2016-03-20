using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DtCms.Common;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// AJAX显示或提交评论
    /// </summary>
    public class Comment_json : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.Params["action"];
            if (action == "add")
            {
                //取得站点配置信息
                DtCms.Model.WebSet webset = new DtCms.BLL.WebSet().loadConfig(Utils.GetXmlMapPath("Configpath"));

                int _kindId;
                int _parentId;
                string _code = "", _username = "";

                if (context.Session["WebUserName"] == null)
                {
                    context.Response.Write("{\"msg\":2, \"msgbox\":\"对不起，您尚未登录,请您先登录！\"}");
                    return;
                }
                else
                {
                    _username = context.Session["WebUserName"].ToString();
                }
                
                int _grade;
                string _content = context.Request.Form["txtContent"];

                //获得栏目ID
                if (!int.TryParse(context.Request.Params["kindId"] as string, out _kindId))
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"无法找到你所要评论的栏目！\"}");
                    return;
                }
                //获得信息ID
                if (!int.TryParse(context.Request.Params["parentId"] as string, out _parentId))
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"无法找到你所要评论的信息！\"}");
                    return;
                }
                //校检验证码
                //if (string.IsNullOrEmpty(_code))
                //{
                //    context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入验证码！\"}");
                //    return;
                //}
                //if (context.Session["DtCode"] == null)
                //{
                //    context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，系统找不到生成的验证码！\"}");
                //    return;
                //}
                //if (_code.ToLower() != (context.Session["DtCode"].ToString()).ToLower())
                //{
                //    context.Response.Write("{\"msg\":0, \"msgbox\":\"您输入的验证码与系统的不一致！\"}");
                //    return;
                //}

                //获得评价星级
                //if (!int.TryParse(context.Request.Form["hidStar"] as string, out _grade))
                //{
                //    //context.Response.Write("{\"msg\":0, \"msgbox\":\"请对此商品作出评价再提交！\"}");
                //    //return;
                //}
                //检查用户名
                if (string.IsNullOrEmpty(_username))
                {
                    //context.Response.Write("{\"msg\":0, \"msgbox\":\"对不起，请输入您的昵称！\"}");
                    //return;
                }
                //检查输入的内容
                if (string.IsNullOrEmpty(_content))
                {
                    context.Response.Write("{\"msg\":0, \"msgbox\":\"请输入您要评论的信息内容！\"}");
                    return;
                }

                //开始写入数据
                DtCms.BLL.AllReviews bll = new DtCms.BLL.AllReviews();
                DtCms.Model.AllReviews model = new DtCms.Model.AllReviews();
                model.KindId = _kindId;
                model.ParentId = _parentId;
                model.UserName = _username.Trim();
                model.Grade = 0;
                model.Content = Utils.ToHtml(_content);
                model.IsLock = webset.IsCheckComment; //评论是否需要审核
                model.AddTime = DateTime.Now;
                model.Ver = "cn";
                bll.Add(model);
                context.Response.Write("{\"msg\":1, \"msgbox\":\"您的评论已提交成功，感谢您的支持！\"}");
                return;
            }
            else if (action == "list")
            {
                int kindId;
                int parentId;
                int pageIndex;
                int pageSize;

                //获得栏目ID
                if (!int.TryParse(context.Request.Params["kindId"] as string, out kindId))
                {
                    context.Response.Write("错误提示1，请勿提交非法字符！");
                    return;
                }
                //获得信息ID
                if (!int.TryParse(context.Request.Params["parentId"] as string, out parentId))
                {
                    context.Response.Write("错误提示2，请勿提交非法字符！");
                    return;
                }
                //获得当前页
                if (!int.TryParse(context.Request.Params["pageIndex"] as string, out pageIndex))
                {
                    context.Response.Write("错误提示3，请勿提交非法字符！");
                    return;
                }
                //获得每页大小
                if (!int.TryParse(context.Request.Params["pageSize"] as string, out pageSize))
                {
                    context.Response.Write("错误提示4，请勿提交非法字符！");
                    return;
                }

                DtCms.BLL.AllReviews bll = new DtCms.BLL.AllReviews();
                DataSet ds = bll.GetPageList(pageSize, pageIndex, "IsLock=0 and KindId=" + kindId + " and ParentId=" + parentId, "AddTime desc");
                //如果记录存在
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        context.Response.Write("<div class=\"item\">\n");
                        context.Response.Write("<div class=\"user\">\n");
                        context.Response.Write("<span class=\"u-name\">网友：" + dr["UserName"].ToString() + "</span>\n");
                        context.Response.Write("<span class=\"star star" + dr["Grade"].ToString() + "\"></span>\n");
                        context.Response.Write("<span class=\"date-ask\">" + dr["AddTime"] + "</span>\n");
                        context.Response.Write("</div>\n");
                        context.Response.Write("<dl class=\"answer\">\n");
                        context.Response.Write("<dt><b></b>评论内容：</dt>\n");
                        context.Response.Write("<dd><div class=\"content\">" + dr["Content"].ToString() + "</div></dd>\n");
                        context.Response.Write("</dl>\n");
                        context.Response.Write("</div>\n");
                    }
                }
                else
                {
                    //context.Response.Write("<p>暂无评论信息！</p>");
                }
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
