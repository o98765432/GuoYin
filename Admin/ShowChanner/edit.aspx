<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DtCms.Web.Admin.ShowChanner.edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改类别</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script language="javascript" src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../css/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../KindEditor/kindeditor.js"></script>
    <link href="../css/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('#txtContent',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });
        });
        $(function () {
            //表单验证JS
            $("#form1").validate({
                //出错时添加的标签
                errorElement: "span",
                success: function (label) {
                    //正确时的样式
                    label.text(" ").addClass("success");
                }
            });

            //显示关闭高级选项
            $("#upordown").toggle(function () {
                $(this).text("关闭高级选项");
                $(this).removeClass();
                $(this).addClass("up-01");
                $(".upordown").show();
            }, function () {
                $(this).text("显示高级选项");
                $(this).removeClass();
                $(this).addClass("up-02");
                $(".upordown").hide();
            });
        });
    </script>
</head>
<body style="padding: 10px;">
    <form id="form1" runat="server">
    <div class="navigation">
        <span class="back"><a href="list.aspx?kindId=<%=kindId %>&path=<%=path %>">返回列表</a></span><b>您当前的位置：首页
            &gt; 类别管理 &gt; 修改类别</b>
    </div>
    <div style="padding-bottom: 10px;">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">
                修改类别信息
            </th>
        </tr>
        <tr>
            <td width="25%" align="right">
                所属父类别：
            </td>
            <td width="75%">
                <asp:DropDownList ID="ddlClassId" CssClass="" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                类别名称：
            </td>
            <td width="75%">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="input required" size="30" MaxLength="50"
                    HintTitle="类别名称" HintInfo="请填写该类别的名称，至少1个字符，小于50个字符。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                SEO标题：
            </td>
            <td width="75%">
                <asp:TextBox ID="txtPageUrl" runat="server" CssClass="input required" size="30" MaxLength="250"
                    HintTitle="SEO标题" HintInfo="请填写SEO标题，至少1个字符，小于250个字符。如：深圳网站建设"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Meta关键字：
            </td>
            <td>
                <asp:TextBox ID="txtKeyword" runat="server" CssClass="input w250" MaxLength="100"
                    HintTitle="Meta关键字" HintInfo="用于搜索引擎，如有多个关健字请用英文的,号分隔，不填写将自动提取标题。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Meta描述：
            </td>
            <td>
                <asp:TextBox ID="txtZhaiyao" runat="server" CssClass="textarea wh380" MaxLength="250"
                    HintTitle="Meta描述" HintInfo="用于搜索引擎，控制在250个字数内，不填写将自动提取。" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                分类描述：
            </td>
            <td width="75%">
                <textarea id="txtContent" cols="100" rows="8" style="width: 100%; height: 400px;
                    visibility: hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                分类背景图片：
            </td>
            <td width="75%">
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files">
                    <input type="file" id="FileUpload1" name="FileUpload1" onchange="SingleUpload('txtImgUrl','FileUpload1')" /></a>
                <span class="uploading">正在上传，请稍候...</span>
                 <font color="#ff0000">图片大小:  (300*214)
                 </font>
            </td>
        </tr>
        <tr  style="display: none">
            <td width="15%" align="right">
                上传右侧视频：
            </td>
            <td width="85%">
                <asp:TextBox ID="txtFilepath" runat="server" CssClass="input w380 left"></asp:TextBox>
                (选择<span style="color:Red">.flv</span>格式本地视频文件上传)<a href="javascript:void(0);" class="files">
                    <input type="file" id="FileDownload" name="FileDownload" onchange="SingleUpload('txtFilepath','FileDownload')" />
                </a>
                <asp:Literal runat="server" ID="Literal1"></asp:Literal>
                <span class="uploading">正在上传，请稍候...</span>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                引导页的信息：（如不需要，可以为空）
            </td>
            <td width="75%">
                <asp:TextBox ID="txtWebIndex" runat="server" CssClass="input w380" size="30" MaxLength="250"
                    HintTitle="引导页的信息" HintInfo="此处为专业人员所用，请勿随意修改"></asp:TextBox>
            </td>
        </tr>
        <tr  >
            <td width="25%" align="right">
                优先级别：
            </td>
            <td width="75%">
                <asp:TextBox ID="txtSortId" CssClass="input required number" Text="10" size="10"
                    runat="server" MaxLength="9" HintTitle="类别分类优先级别" HintInfo="纯数字，数字越少越向前。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                Html路径
            </td>
            <td width="75%">
                <asp:TextBox ID="txtWebPath" runat="server" CssClass="input w380" size="30" MaxLength="250"
                    HintTitle="Html路径" HintInfo="此处为专业人员所用，请勿随意修改"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                分页数量
            </td>
            <td width="75%">
                <asp:TextBox ID="txtHtmlSize" runat="server" CssClass="input w50" size="30" MaxLength="250"
                    HintTitle="分页数量" HintInfo="此处为专业人员所用，请勿随意修改"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                图片宽度：
            </td>
            <td width="75%">
                <asp:TextBox Text="0" ID="txtwidth" CssClass="input required number" size="10" runat="server"
                    MaxLength="9" HintTitle="图片宽度" HintInfo="纯数字，图片宽度。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                图片高度：
            </td>
            <td width="75%">
                <asp:TextBox Text="0" ID="txtheight" CssClass="input required number" size="10" runat="server"
                    MaxLength="9" HintTitle="图片高度" HintInfo="纯数字，图片高度。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                页面类型：
            </td>
            <td>
                <asp:DropDownList ID="DrpPageType" CssClass="" runat="server">
                    <asp:ListItem Value="13">单页面</asp:ListItem>
                    <asp:ListItem Value="3">列表单页面</asp:ListItem>
                    <asp:ListItem Value="12">视频展示</asp:ListItem>
                    <asp:ListItem Value="11">解决方案</asp:ListItem>
                    <asp:ListItem Value="10">常见问题</asp:ListItem>
                    <asp:ListItem Value="9">广告图</asp:ListItem>
                    <asp:ListItem Value="8">在线留言</asp:ListItem>
                    <asp:ListItem Value="7">友情链接</asp:ListItem>
                    <asp:ListItem Value="6">下载</asp:ListItem>
                    <asp:ListItem Value="5">人才招聘</asp:ListItem>
                    <asp:ListItem Value="4">产品</asp:ListItem>
                    <asp:ListItem Value="2">新闻</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr >
            <td width="25%" align="right">
                其它属性：
            </td>
            <td width="75%">
                <asp:CheckBox ID="FrontDeskDisplay" runat="server" Checked="True" Text="前台可见性" />
                <asp:CheckBox ID="LeftDisplay" runat="server" Checked="True" Text="左侧栏目可见性" />
            </td>
        </tr>
    </table>
    <div style="margin-top: 10px; text-align: center;">
        <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" OnClick="btnSave_Click" />
        &nbsp;&nbsp;
        <input type="reset" name="button" id="btnReset" value="重 置" class="submit" />
    </div>
    </form>
</body>
</html>
