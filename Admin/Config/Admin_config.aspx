<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_config.aspx.cs" Inherits="DtCms.Web.Admin.Config.admin_config" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统参数设置</title>
    <link rel="stylesheet" href="../images/style.css" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
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
        });
    </script>
</head>
<body style="padding: 10px;">
    <form id="form1" runat="server">
    <div class="navigation">
        <b>您当前的位置：首页 &gt; 系统管理 &gt; 系统参数设置</b></div>
    <div style="padding-bottom: 10px;">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tbody>
            <tr>
                <th colspan="2" align="left">
                    系统基本设置（注意：如果你不是专业人员请勿改动，只有开放文件的读写权限才能修改）
                </th>
            </tr>
            <tr>
                <td width="25%" align="right">
                    网站标题：
                </td>
                <td width="75%">
                    <asp:TextBox ID="txtWebName" runat="server" CssClass="input required" size="48" MaxLength="50"
                        HintTitle="系统的名称" HintInfo="给你的系统起个有意义的名字哦，长度不能超过50个字符。"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    网站域名：
                </td>
                <td>
                    <asp:TextBox ID="txtWebUrl" runat="server" CssClass="input required" size="48" MaxLength="100"
                        HintTitle="网站的域名" HintInfo="请以http://为开头填写，长度不能超过100个字符。"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    网站电话：
                </td>
                <td>
                    <asp:TextBox ID="txtWebTel" runat="server" CssClass="input required" size="25" MaxLength="50"
                        HintTitle="办公电话号码" HintInfo="格式如：0757-22228888。"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td align="right">
                    传真号码：
                </td>
                <td>
                    <asp:TextBox ID="txtWebFax" runat="server" CssClass="input" size="25" MaxLength="50"
                        HintTitle="传真号码" HintInfo="格式如：0757-22228888。"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td align="right">
                    商城链接：<br />
                </td>
                <td>
                    <asp:TextBox ID="txtWebEmail" runat="server" CssClass="input" size="25" MaxLength="50"
                        HintTitle="商城链接" HintInfo="商城链接。"></asp:TextBox>
                </td>
            </tr>
            <tr >
                <td align="right">
                    系统版权信息：
                </td>
                <td>
                    <asp:TextBox ID="txtWebCrod" runat="server" CssClass="input" size="25" MaxLength="100" Style="width: 300px;"
                        HintTitle="系统版权信息" HintInfo="系统版权信息。"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    网站关健字：
                </td>
                <td>
                    <asp:TextBox ID="txtWebKeywords" runat="server" CssClass="input" Style="width: 300px;"
                        MaxLength="250" HintTitle="网站关健字" HintInfo="搜索引擎可根据网站设置的关健字，以“,”号分隔开。"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    网站描述：
                </td>
                <td>
                    <asp:TextBox ID="txtWebDescription" runat="server" CssClass="textarea" Style="width: 300px;
                        height: 45px;" MaxLength="250" HintTitle="网站描述" HintInfo="搜索引擎可根据网站设置的描述信息，字符小于等于250位字符。"
                        TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr style="display: none">
                <td align="right">
                    系统版权信息：<br />
                    （支持HTML）
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="txtWebCopyright">
                        <asp:ListItem Value="0">无网站地图</asp:ListItem>
                        <asp:ListItem Value="1">网站地图－001</asp:ListItem>
                        <asp:ListItem Value="2">网站地图－002</asp:ListItem>
                        <asp:ListItem Value="3">网站地图－003</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="txtid" runat="server" />
                    <asp:HiddenField ID="txtver" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <div class="spClear">
    </div>
    <div style="margin-top: 10px; text-align: center;">
        <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" OnClick="btnSave_Click" />
        &nbsp;
        <input name="重置" type="reset" class="submit" value="重置" />
    </div>
    <div>
        <img src="<%=(!webmapinfo.Equals("0")?"/admin/WebTemplate/Map/" + webmapinfo + ".jpg":"") %>" id="showmapinfo"/>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#txtWebCopyright").change(function () {
                
                if ($("#txtWebCopyright").val() == "0") {

                    $("#showmapinfo").attr("src", "");


                } else {

                    $("#showmapinfo").attr("src", "/admin/WebTemplate/Map/" + $("#txtWebCopyright").val() + ".jpg");

                }

            });
        });
    </script>
</body>
</html>
