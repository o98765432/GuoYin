<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DtCms.Web.Admin.ChenAdd.edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>扩展字段</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script> 
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
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
    <form id="form1" runat="server" method="post">
    <div class="navigation">
        <span class="back"><a href="List.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 扩展字段管理 &gt;
            扩展字段编辑</b>
    </div>
    <div class="spClear">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="3" align="left">
                扩展字段信息
            </th>
        </tr>
        <tr>
            <td align="right" valign="top">
                标题：
            </td>
            <td>
                <asp:TextBox ID="txttitle" runat="server" CssClass="input w380 required" MaxLength="250"
                    minlength="2" HintTitle="标题" HintInfo="标题"></asp:TextBox>
                <asp:HiddenField ID="txtid" Value="0" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                字段名：
            </td>
            <td>
                <asp:TextBox ID="txtziduan" runat="server" CssClass="input w380 required" MaxLength="250"
                    minlength="1" HintTitle="字段名" HintInfo="字段名,字段名不能重复"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                描述：
            </td>
            <td>
                <asp:TextBox ID="txtmemo" runat="server" CssClass="input w380 required" MaxLength="250"
                    minlength="1" HintTitle="描述" HintInfo="描述"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                选项信息：
            </td>
            <td>
                <asp:TextBox ID="txtfenge" runat="server" CssClass="input w380" MaxLength="250" minlength="2"
                    HintTitle="选项信息" HintInfo="分割，多个选项的时候用/分割"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                图片宽：
            </td>
            <td>
                <asp:TextBox ID="txtwidth" runat="server" CssClass="input w380" Text="0" MaxLength="250"
                    minlength="1" HintTitle="图片宽" HintInfo="图片宽"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                图片高：
            </td>
            <td>
                <asp:TextBox ID="txtheight" runat="server" CssClass="input w380" Text="0" MaxLength="250"
                    minlength="1" HintTitle="图片高" HintInfo="图片高"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                类别：
            </td>
            <td>
                <asp:DropDownList ID="txttypeid" runat="server">
                    <asp:ListItem Value="1">新闻</asp:ListItem>
                    <asp:ListItem Value="2">产品</asp:ListItem>
                    <asp:ListItem Value="3">视频</asp:ListItem>
                    <asp:ListItem Value="4">下载</asp:ListItem>
                    <asp:ListItem Value="5">人才招聘</asp:ListItem>
                    <asp:ListItem Value="6">在线留言</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                类型：
            </td>
            <td>
                <asp:DropDownList ID="txtclassid" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                类别：
            </td>
            <td>
                <asp:DropDownList ID="txttype" runat="server">
                    <asp:ListItem Value="1">单行文本框</asp:ListItem>
                    <asp:ListItem Value="2">多行文本框</asp:ListItem>
                    <asp:ListItem Value="3">HTML编辑框</asp:ListItem>
                    <asp:ListItem Value="4">下拉框</asp:ListItem>
                    <asp:ListItem Value="5">单选框</asp:ListItem>
                    <asp:ListItem Value="6">复选框</asp:ListItem>
                    <asp:ListItem Value="7">上传图片</asp:ListItem>
                    <asp:ListItem Value="8">上传文件</asp:ListItem>
                    <asp:ListItem Value="9">数字框</asp:ListItem>
                    <asp:ListItem Value="10">日期框</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td align="right" valign="top">
                是否必填（前台在线留言）：
            </td>
            <td>
                <asp:DropDownList ID="txtbitian" runat="server">
                    <asp:ListItem Value="0">否</asp:ListItem>
                    <asp:ListItem Value="1">是</asp:ListItem> 
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div style="margin-top: 100px; text-align: center;">
        <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" OnClick="btnSave_Click" />
        &nbsp;
        <input name="重置" type="reset" class="submit" value="重置" />
    </div>
    </form>
</body>
</html>
