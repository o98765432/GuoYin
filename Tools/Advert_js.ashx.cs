using System;
using System.Text;
using System.Data;
using System.Web;
using DtCms.Common;

namespace DtCms.Web.Tools
{
    public class Advert_js : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int aid;

            //获得广告位的ID
            if (!int.TryParse(context.Request.Params["id"] as string, out aid))
            {
                context.Response.Write("document.write('错误提示，请勿提交非法字符！');");
                return;
            }

            //检查广告位是否存在
            DtCms.BLL.Advertising abll = new DtCms.BLL.Advertising();
            if (!abll.Exists(aid))
            {
                context.Response.Write("document.write('错误提示，该广告位不存在！');");
                return;
            }

            //取得该广告位详细信息
            DtCms.Model.Advertising aModel = abll.GetModel(aid);

            //输出该广告位下的广告条,不显示未开始、过期、暂停广告
            DtCms.BLL.Adbanner bbll = new DtCms.BLL.Adbanner();
            DataSet ds = bbll.GetList("IsLock=0 and datediff(d,StartTime,getdate())>=0 and datediff(d,EndTime,getdate())<=0 and Aid=" + aid);
            if (ds.Tables[0].Rows.Count < 1)
            {
                context.Response.Write("document.write('该广告位下暂无广告内容');");
                return;
            }

            //=================判断广告位类别,输出广告条======================

            //新增，取得站点配置信息
            DtCms.Model.WebSet webset = new DtCms.BLL.WebSet().loadConfig(Utils.GetXmlMapPath("Configpath"));

            switch (aModel.AdType)
            {
                case 1: //文字
                    context.Response.Write("document.write('<ul>');\n");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //如果超出限制广告数量，则退出循环
                        if (i >= aModel.AdNum)
                            break;
                        DataRow dr = ds.Tables[0].Rows[i];
                        context.Response.Write("document.write('<li>');");
                        context.Response.Write("document.write('<a title=\"" + dr["Title"] + "\" target=\"" + aModel.AdTarget + "\" href=\"" + dr["LinkUrl"] + "\">" + dr["Title"] + "</a>');");
                        context.Response.Write("document.write('</li>');\n");
                    }
                    context.Response.Write("document.write('</ul>');\n");
                    break;
                case 2: //图片
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        context.Response.Write("document.write('<a title=\"" + dr["Title"] + "\" target=\"" + aModel.AdTarget + "\" href=\"" + dr["LinkUrl"] + "\">');");
                        context.Response.Write("document.write('<img src=\"" + dr["AdUrl"] + "\" width=" + aModel.AdWidth + " height=" + aModel.AdHeight + " border=0 />');");
                        context.Response.Write("document.write('</a>');");
                    }
                    else
                    {
                        context.Response.Write("document.write('<ul>');\n");
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //如果超出限制广告数量，则退出循环
                            if (i >= aModel.AdNum)
                                break;
                            DataRow dr = ds.Tables[0].Rows[i];
                            context.Response.Write("document.write('<li>');");
                            context.Response.Write("document.write('<a title=\"" + dr["Title"] + "\" target=\"" + aModel.AdTarget + "\" href=\"" + dr["LinkUrl"] + "\">');");
                            context.Response.Write("document.write('<img src=\"" + dr["AdUrl"] + "\" width=" + aModel.AdWidth + " height=" + aModel.AdHeight + " border=0 />');");
                            context.Response.Write("document.write('</a>');\n");
                            context.Response.Write("document.write('</li>');\n");
                        }
                        context.Response.Write("document.write('</ul>');\n");
                    }
                    break;
                case 3: //幻灯片
                    StringBuilder picTitle = new StringBuilder();
                    StringBuilder picUrl = new StringBuilder();
                    StringBuilder picLink = new StringBuilder();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //如果超出限制广告数量，则退出循环
                        if (i >= aModel.AdNum)
                            break;
                        DataRow dr = ds.Tables[0].Rows[i];
                        picUrl.Append(dr["AdUrl"].ToString());
                        picLink.Append(dr["LinkUrl"].ToString());
                        if (i < ds.Tables[0].Rows.Count - 1)
                        {
                            picUrl.Append("|");
                            picLink.Append("|");
                        }
                    }
                    context.Response.Write("document.write('<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" d=scriptmain name=scriptmain codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" width=\"" + aModel.AdWidth + "\" height=\"" + aModel.AdHeight + "\">');\n");
                    context.Response.Write("document.write('<param name=\"movie\" value=\"" + webset.WebPath + "images/focus.swf?width=" + aModel.AdWidth + "&height=" + aModel.AdHeight + "&bigSrc=" + picUrl + "&href=" + picLink + "\">');\n");
                    context.Response.Write("document.write('<param name=\"quality\" value=\"high\">');\n");
                    context.Response.Write("document.write('<param name=\"loop\" value=\"false\">');\n");
                    context.Response.Write("document.write('<param name=\"menu\" value=\"false\">');\n");
                    context.Response.Write("document.write('<param name=\"wmode\" value=\"transparent\">');\n");
                    context.Response.Write("document.write('<embed src=\"" + webset.WebPath + "images/focus.swf?width=" + aModel.AdWidth + "&height=" + aModel.AdHeight + "&bigSrc=" + picUrl + "&href=" + picLink + "\" width=\"" + aModel.AdWidth + "\" height=\"" + aModel.AdHeight + "\" loop=\"false\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" type=\"application/x-shockwave-flash\" wmode=\"transparent\" menu=\"false\"></embed>');\n");
                    context.Response.Write("document.write('</object>');\n");
                    break;
                case 4: //动画
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        context.Response.Write("document.write('<object classid=\"clsid:D27CDB6E-AE6D-11CF-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" width=\"" + aModel.AdWidth + "\" height=\"" + aModel.AdHeight + "\">');\n");
                        context.Response.Write("document.write('<param name=\"movie\" value=\"" + dr["AdUrl"] + "\">');\n");
                        context.Response.Write("document.write('<param name=\"quality\" value=\"high\">');\n");
                        context.Response.Write("document.write('<param name=\"wmode\" value=\"transparent\">');\n");
                        context.Response.Write("document.write('<param name=\"menu\" value=\"false\">');\n");
                        context.Response.Write("document.write('<embed src=\"" + dr["AdUrl"] + "\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"" + aModel.AdWidth + "\" height=\"" + aModel.AdHeight + "\" quality=\"High\" wmode=\"transparent\">');\n");
                        context.Response.Write("document.write('</embed>');\n");
                        context.Response.Write("document.write('</object>');\n");
                    }
                    else
                    {
                        context.Response.Write("document.write('<ul>');\n");
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //如果超出限制广告数量，则退出循环
                            if (i >= aModel.AdNum)
                                break;
                            DataRow dr = ds.Tables[0].Rows[i];
                            context.Response.Write("document.write('<li>');");
                            context.Response.Write("document.write('<object classid=\"clsid:D27CDB6E-AE6D-11CF-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" width=\"" + aModel.AdWidth + "\" height=\"" + aModel.AdHeight + "\">');\n");
                            context.Response.Write("document.write('<param name=\"movie\" value=\"" + dr["AdUrl"] + "\">');\n");
                            context.Response.Write("document.write('<param name=\"quality\" value=\"high\">');\n");
                            context.Response.Write("document.write('<param name=\"wmode\" value=\"transparent\">');\n");
                            context.Response.Write("document.write('<param name=\"menu\" value=\"false\">');\n");
                            context.Response.Write("document.write('<embed src=\"" + dr["AdUrl"] + "\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"" + aModel.AdWidth + "\" height=\"" + aModel.AdHeight + "\" quality=\"High\" wmode=\"transparent\">');\n");
                            context.Response.Write("document.write('</embed>');\n");
                            context.Response.Write("document.write('</object>');\n");
                            context.Response.Write("document.write('</li>');\n");
                        }
                        context.Response.Write("document.write('</ul>');\n");
                    }
                    break;
                case 5://视频
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //如果超出限制广告数量，则退出循环
                        if (i >= 1)
                            break;
                        DataRow dr = ds.Tables[0].Rows[i];
                        context.Response.Write("document.write('<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" width=" + aModel.AdWidth + " height=" + aModel.AdHeight + " viewastext>');\n");
                        context.Response.Write("document.write('<param name=\"movie\" value=\"" + webset.WebPath + "images/Player.swf\" />');\n");
                        context.Response.Write("document.write('<param name=\"quality\" value=\"high\" />');\n");
                        context.Response.Write("document.write('<param name=\"allowFullScreen\" value=\"true\" />');\n");
                        context.Response.Write("document.write('<param name=\"FlashVars\" value=\"vcastr_file=" + dr["AdUrl"].ToString() + "&LogoText=www.auto.cn&BarTransparent=30&BarColor=0xffffff&IsAutoPlay=1&IsContinue=1\" />');\n");
                        context.Response.Write("document.write('</object>');\n");
                    }
                    break;
                case 6://代码
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //如果超出限制广告数量，则退出循环
                        if (i >= 1)
                            break;
                        DataRow dr = ds.Tables[0].Rows[i];
                        StringBuilder sb = new StringBuilder(dr["AdRemark"].ToString());
                        sb.Replace("&lt;", "<");
                        sb.Replace("&gt;", ">");
                        sb.Replace("\"", "'");
                        context.Response.Write("document.write(\"" + sb.ToString() + "\")");
                    }
                    break;
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
