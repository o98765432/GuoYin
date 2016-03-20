<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Control.aspx.cs" Inherits="DtCms.Web.Admin.Control" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       远程控制硕士猫网站，关闭硕士猫网站
        <asp:RadioButton runat="server" ID="rbControlYes" GroupName="shuoshimao" />是
        <asp:RadioButton runat="server" ID="rbControlNo" GroupName="shuoshimao" />否
        <asp:Button runat="server" ID="Send" Text="确定" onclick="Send_Click" />
    </div>
    </form>
</body>
</html>
