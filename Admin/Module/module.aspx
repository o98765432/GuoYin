<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="module.aspx.cs" Inherits="DtCms.Web.Admin.Module.module" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head2" runat="server">
    <title>模块管理</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.pagination.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    
</head>
<body style="padding:10px;">
    <form id="form2" runat="server">
    <div class="navigation"><span class="add"><a href="module_add.aspx?id=<%=this.ddlClassId.SelectedValue %>">增加模块</a></span><b>您当前的位置：首页 &gt; 模块管理 &gt; 模块列表</b></div>
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="50" align="center">请选择：</td>
            <td>
                <asp:DropDownList ID="ddlClassId" runat="server" CssClass="select" onselectedindexchanged="ddlClassId_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>&nbsp;
            </td>
        </tr>
    </table>
    
    <div class="spClear"></div>
    
    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="6%">编号</th>
        <th align="left">模块标题</th>
        <th width="36%">模块链接</th>
        <th width="90">优先级别</th>
        <th width="8%">操作</th>
      </tr>
      </HeaderTemplate>
      <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
        <td align="center"><asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>'></asp:Label></td>
        <td><a href="module_update.aspx?id=<%#Eval("Id") %>"><%#Eval("ModuleName")%></a></td>
        <td align="center"><%# Eval("ModuleHref")%></td>
        <td align="center"><%#Eval("Order_Id")%></td>
        <td align="center"><span><a href="module_update.aspx?id=<%#Eval("Id") %>&typeid=<%=ddlClassId.SelectedValue %>">修改</a></span></td>
      </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
      </FooterTemplate>
      </asp:Repeater>

    <div class="spClear"></div>
        <div style="line-height:30px;height:30px;">
            <div id="Pagination" class="right flickr"></div>
            <div class="left">
                <span class="btn_all" onclick="checkAll(this);">全选</span>
                <span class="btn_bg">
                  
                    &nbsp;<asp:LinkButton ID="lbtnDel" runat="server" 
                    OnClientClick="return confirm( '确定要删除这些记录吗？ ');" onclick="lbtnDel_Click">删 除</asp:LinkButton>
                </span>
            </div>
	</div>
    </form>
</body>
</html>

