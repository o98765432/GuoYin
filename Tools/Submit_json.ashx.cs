using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DtCms.Common;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// 表单提交统一处理页
    /// </summary>
    public class Submit_json : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = context.Request.Params["action"];
            //取得站点配置信息
            DtCms.Model.WebSet webset = new DtCms.BLL.WebSet().loadConfig(Utils.GetXmlMapPath("Configpath"));

            //===============================添加友情链接===============================
            if (action == "link")
            {
                string _code = context.Request.Form["txtCode"];
                string _title = context.Request.Form["txtTitle"];
                string _username = context.Request.Form["txtUserName"];
                string _usertel = context.Request.Form["txtUserTel"];
                string _usermail = context.Request.Form["txtUserMail"];
                string _weburl = context.Request.Form["txtWebUrl"];
                string _imgurl = context.Request.Form["txtImgUrl"];
                int _isimage;

                //校检验证码
                if (string.IsNullOrEmpty(_code))
                {
                    context.Response.Write("{msg:0, msgbox:\"对不起，请输入验证码！\"}");
                    return;
                }
                if (context.Session["DtCode"] == null)
                {
                    context.Response.Write("{msg:0, msgbox:\"对不起，系统找不到生成的验证码！\"}");
                    return;
                }
                if (_code.ToLower() != (context.Session["DtCode"].ToString()).ToLower())
                {
                    context.Response.Write("{msg:0, msgbox:\"您输入的验证码与系统的不一致！\"}");
                    return;
                }
                //检查网站标题
                if (string.IsNullOrEmpty(_title))
                {
                    context.Response.Write("{msg:0, msgbox:\"对不起，请输入您要链接的网站标题！\"}");
                    return;
                }
                //检查姓名
                if (string.IsNullOrEmpty(_username))
                {
                    context.Response.Write("{msg:0, msgbox:\"对不起，请输入您的姓名昵称！\"}");
                    return;
                }
                //检查联系电话
                if (string.IsNullOrEmpty(_usertel))
                {
                    context.Response.Write("{msg:0, msgbox:\"对不起，请输入您的联系电话！\"}");
                    return;
                }
                //检查网址
                if (string.IsNullOrEmpty(_weburl))
                {
                    context.Response.Write("{msg:0, msgbox:\"对不起，请输入您网站的网址！\"}");
                    return;
                }
                //检查链接类别
                if (!int.TryParse(context.Request.Form["rblIsImage"] as string, out _isimage))
                {
                    context.Response.Write("对不起，请选择要链接的类别！");
                    return;
                }
                //检查其它项
                if (string.IsNullOrEmpty(_usermail))
                    _usermail = "";
                if (string.IsNullOrEmpty(_imgurl))
                    _imgurl = "";
                //写入数据
                DtCms.Model.Links model = new DtCms.Model.Links();
                DtCms.BLL.Links bll = new DtCms.BLL.Links();
                model.Title = _title.Trim();
                model.UserName = _username.Trim();
                model.UserTel = _usertel.Trim();
                model.UserMail = _usermail.Trim();
                model.WebUrl = _weburl.Trim();
                model.ImgUrl = _imgurl.Trim();
                model.IsImage = _isimage;
                model.IsLock = 1;
                bll.Add(model);
                context.Response.Write("{msg:1, msgbox:\"您的链接请求已提交成功，请等待审核通过！\"}");
                return;
            }

            //===============================添加在线留言===============================
            if (action == "feedback")
            {
                string _code = context.Request.Form["txtCode"];
                string _username = context.Request.Form["txtUserName"];
                string _usertel = context.Request.Form["txtUserTel"];
                string _userqq = context.Request.Form["txtUserQQ"];
                string _title = context.Request.Form["txtTitle"];
                string _content = context.Request.Form["txtContent"];

                //校检验证码
                if (string.IsNullOrEmpty(_code))
                {
                    context.Response.Write("{msg:0, msgbox:\"对不起，请输入验证码！\"}");
                    return;
                }
                if (context.Session["DtCode"] == null)
                {
                    context.Response.Write("{msg:0, msgbox:\"对不起，系统找不到生成的验证码！\"}");
                    return;
                }
                if (_code.ToLower() != (context.Session["DtCode"].ToString()).ToLower())
                {
                    context.Response.Write("{msg:0, msgbox:\"您输入的验证码与系统的不一致！\"}");
                    return;
                }
                //检查姓名
                if (string.IsNullOrEmpty(_username))
                {
                    context.Response.Write("{msg:0, msgbox:\"对不起，请输入您的昵称！\"}");
                    return;
                }
                //检查输入的标题
                if (string.IsNullOrEmpty(_title))
                {
                    context.Response.Write("{msg:0, msgbox:\"请输入您要留言的标题！\"}");
                    return;
                }
                //检查输入的内容
                if (string.IsNullOrEmpty(_content))
                {
                    context.Response.Write("{msg:0, msgbox:\"请输入您要留言的信息内容！\"}");
                    return;
                }
                //检查其它项
                if (string.IsNullOrEmpty(_usertel))
                    _usertel = "";
                if (string.IsNullOrEmpty(_userqq))
                    _userqq = "";
                //写入数据
                DtCms.Model.Feedback model = new DtCms.Model.Feedback();
                DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback();
                model.UserName = _username.Trim();
                model.UserTel = _usertel.Trim();
                model.UserQQ = _userqq.Trim();
                model.Title = _title.Trim();
                model.Content = Utils.ToHtml(_content);
                model.IsLock = webset.IsCheckFeedback; //留言是否需要审核
                model.AddTime = DateTime.Now;
                bll.Add(model);
                context.Response.Write("{msg:1, msgbox:\"您的留言已提交成功，感谢您的支持！\"}");
                return;
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
