<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="DtCms.Web.Admin.Member.Show" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑会员</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript" src="../../KindEditor/kindeditor.js"></script>
    
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="List.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 会员管理 &gt; 编辑会员</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">报名信息</th>
        </tr>
          <tr>
            <td width="15%" align="right">姓名：</td>
            <td width="85%">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input w380 required" 
            maxlength="250" HintTitle="姓名"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">年龄：</td>
            <td>
                <asp:TextBox ID="txtAge" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">电话：</td>
            <td>
                <asp:TextBox ID="txtTel" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">所属栏目：</td>
            <td><asp:DropDownList id="ddlClassId" CssClass=" required" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right">信息属性：</td>
            <td>
                 
                <asp:CheckBoxList ID="cblItem" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  
                    <asp:ListItem Value="1">冻结</asp:ListItem>
                   
                </asp:CheckBoxList>
            </td>
        </tr>
        
       
    </table>
    <div style="margin-top:10px;text-align:center;">
      <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" onclick="btnSave_Click" />
      &nbsp;
      <input name="重置" type="reset" class="submit" value="重置" />
    </div>
    </form>
</body>
</html>
