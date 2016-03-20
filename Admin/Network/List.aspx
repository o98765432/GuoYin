<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DtCms.Web.Admin.Network.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>友情销售网络管理</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.pagination.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
        $(function() {
            //分页参数设置
            $("#Pagination").pagination(<%=pcount %>, {
            callback: pageselectCallback,
            prev_text: "« 上一页",
            next_text: "下一页 »",
            items_per_page:<%=pagesize %>,
		    num_display_entries:3,
		    current_page:<%=page %>,
		    num_edge_entries:2,
		    link_to:"?page=__id__"
           });
        });
        function pageselectCallback(page_id, jq) {
           //alert(page_id); 回调函数，进一步使用请参阅说明文档
        }
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation"><span class="add"><a href="Add.aspx">增加销售网络</a></span><b>您当前的位置：首页 &gt; 销售网络管理 &gt; 销售网络列表</b></div>
    <div class="spClear"></div>
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="50" align="center">请选择：</td>
        <td>
            <asp:DropDownList ID="ddlClassId" runat="server" CssClass="select" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlClassId_SelectedIndexChanged">
                 <asp:ListItem Value="__">全部地区</asp:ListItem>
                   <asp:ListItem Value="dongbei">东北地区</asp:ListItem>
                    <asp:ListItem Value="huabei">华北地区</asp:ListItem>
                     <asp:ListItem Value="xibei">西北地区</asp:ListItem>
                        <asp:ListItem Value="xinan">西南地区</asp:ListItem>
                      <asp:ListItem Value="huazhong">华中地区</asp:ListItem>
                  
                    <asp:ListItem Value="huadong">华东地区</asp:ListItem>
                   
                    <asp:ListItem Value="huanan">华南地区</asp:ListItem>
                    <asp:ListItem Value="taiwan">台湾地区</asp:ListItem>
            </asp:DropDownList>&nbsp;
        </tr>
    </table>

    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="6%">编号</th>
        <th align="left">地区名</th>
        <th width="16%">电话</th>
        <th width="90">优先级别</th>
        <th width="16%">添加时间</th>
       
        <th width="8%">操作</th>
      </tr>
      </HeaderTemplate>
      <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
        <td align="center"><asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>'></asp:Label></td>
        <td><%# Convert.ToInt32(Eval("isLock")) == 1 ? "<img src=\"../Images/wsh01.gif\" title=\"未审核\" />" : ""%> <a href="Edit.aspx?id=<%#Eval("Id") %>"><%#Eval("Title")%></a></td>
        <td align="center"><%# Eval("UserTel")%></td>
        <td align="center"><%#Eval("SortId") %></td>
        <td ><%#string.Format("{0:g}",Eval("AddTime"))%></td>
        
        <td align="center"><span><a href="edit.aspx?id=<%#Eval("Id") %>">修改</a></span></td>
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
                  <asp:LinkButton ID="lbtnAudit" runat="server" onclick="lbtnAudit_Click">生成动画</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="lbtnDel" runat="server" 
                    OnClientClick="return confirm( '确定要删除这些记录吗？ ');" onclick="lbtnDel_Click">删 除</asp:LinkButton>
                </span>
            </div>
	</div>
    </form>
</body>
</html>
