<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorparationsList.aspx.cs" Inherits="DtCms.Web.Admin.Consultant.CorparationsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.pagination.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
</head>
<body >
<form id="form1" runat="server">
  <div class="navigation"> <span class="back"><a href="">返回列表</a></span><b>您当前的位置：首页 &gt; 资讯管理 &gt; 编辑资讯</b></div>
     <div class="spClear"></div>
 
        <asp:Repeater ID="rptList" runat="server" >
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="6%">编号</th>
        <th align="left">公司名称</th>
        <th width="13%">行业</th>
        <th width="16%">发布时间</th>
        <th width="110">联系方式</th>
 
      </tr>
      </HeaderTemplate>
      <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
        <td align="center"><asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>'></asp:Label></td>
        <td><a href="CorparationsEdit.aspx?id=<%#Eval("Id") %>"><%#Eval("Company")%></a></td>
        <td align="center"><a href="CorparationsEdit.aspx?id=<%#Eval("Id") %>"><%#Eval("industry")%></a></td>
        <td align="center"><%#Eval("addTime")%></td>
        <td align="center"><%#Eval("telePhone")%></td>
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
                  <asp:LinkButton ID="lbtnDel" runat="server" 
                    OnClientClick="return confirm( '确定要删除这些记录吗？ ');" onclick="lbtnDel_Click">删 除</asp:LinkButton>&nbsp;
                     <asp:LinkButton ID="lbtnMakeHtml" runat="server" onclick="lbtnMakeHtml_Click" Visible="false"
                    >生成网页静态化</asp:LinkButton>
                </span>
                <span class="btn_all" >
                    <linkbutton onclick ="changePaixuAndIslock();"  >修改</linkbutton>
                </span>
            </div>
	</div>

      </form>
</body>
</html>
