<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DtCms.Web.Admin.ArticlePictures.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>文章多图片管理</title>
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
		    link_to:"?<%=CombUrlTxt(this.kindId, this.parentId, this.keywords, this.property) %>page=__id__"
           });
        });
        function pageselectCallback(page_id, jq) {
           //alert(page_id); 回调函数，进一步使用请参阅说明文档
        }
        //隔行变色
        $(function() {
            $(".msgtable tr:nth-child(odd)").addClass("tr_bg"); 
            $(".msgtable tr").hover(
			    function() {
			        $(this).addClass("tr_hover_col");
			    },
			    function() {
			        $(this).removeClass("tr_hover_col");
			    }
		    );
        });
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
 
     <div class="navigation">
     <%if (addflag == true)
       {%>
        <span class="add">
     <a href="Add.aspx?parentId=<%=Request.QueryString["ParentId"]%>&kindId=<%=Request.QueryString["kindId"] %>">发布文章图片</a></span>
     <%} %>
  
     <b>您当前的位置：首页 &gt; 文章图片管理 &gt; 图片列表</b></div>

    <div class="spClear"></div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="50" align="center">请选择：</td>
        <td>
            <asp:DropDownList ID="ddlKindId" runat="server" CssClass="select" onselectedindexchanged="ddlKindId_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>&nbsp;
            <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select" onselectedindexchanged="ddlProperty_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="">所有属性</asp:ListItem>
                <asp:ListItem Value="isLock">未审核</asp:ListItem>
                <asp:ListItem Value="unIsLock">已审核</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td width="50" align="right">关健字：</td>
        <td width="150"><asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword"></asp:TextBox></td>
        <td width="60" align="center"><asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="submit" onclick="btnSelect_Click" /></td>
        </tr>
    </table>
    <div class="spClear"></div>
      <asp:Repeater ID="rptList" runat="server" >
    <HeaderTemplate>
    <div class="pro_img_list">
      <ul>
      </HeaderTemplate>
        <ItemTemplate>
        <li>
           
            <asp:Label ID="hidId" runat="server" Text='<%#Eval("Id")%>' style="display:none;"></asp:Label>
            <div class="nTitle"><asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" style="vertical-align:middle;" /><asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>' style="display:none;"></asp:Label><%#DtCms.Common.Utils.CutString(Eval("Title").ToString(), 14)%></div>
            <div class="nImg"><a title="<%#Eval("Title")%>" href="Edit.aspx?id=<%#Eval("Id") %>&parentId=<%# Eval("ParentId") %>&kindId=<%# Eval("KindId") %>"><img src="<%#Eval("ImgUrl") %>" /></a></div>
            <div class="nBtm">
            <span class="right">
              <a href="Edit.aspx?id=<%#Eval("Id") %>&parentId=<%# Eval("ParentId") %>&kindId=<%# Eval("KindId") %>"><img src="../Images/ico-6.png" title="修改" /></a>
            </span>
          </div>
        </li>
        </ItemTemplate>
      <FooterTemplate>
      </ul>
    </div> 
    </FooterTemplate>
    </asp:Repeater>

    <div class="spClear"></div>
        <div style="line-height:30px;height:30px;">
            <div id="Pagination" class="right flickr"></div>
            <div class="left">
                <span class="btn_all" onclick="checkAll(this);">全选</span>
                <span class="btn_bg">
                  <asp:LinkButton ID="lbtnAudit" runat="server" onclick="lbtnAudit_Click">审 核</asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="lbtnDel" runat="server" 
                    OnClientClick="return confirm( '确定要删除这些记录吗？ ');" onclick="lbtnDel_Click">删 除</asp:LinkButton>
                </span>
            </div>
	</div>
    </form>
</body>
</html>
