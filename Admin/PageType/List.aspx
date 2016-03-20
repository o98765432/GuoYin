<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DtCms.Web.Admin.PageType.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>内容管理</title>
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
		    link_to:"?<%=CombUrlTxt(this.classId, this.keywords) %>page=__id__"
           });
        });
        function pageselectCallback(page_id, jq) {
           //alert(page_id); 回调函数，进一步使用请参阅说明文档
        }
        ////隔行变色
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
 
    <span class="add"><a href="Edit.aspx?classid=<%=this.classId %>">增加页面模式</a></span>
    
    <b>您当前的位置：首页 &gt; 内容管理 &gt; 内容列表</b></div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr style="display:none">
        <td width="50" align="center">请选择：</td>
        <td>
        <asp:DropDownList ID="ddlClassId" runat="server" CssClass="select" AutoPostBack="True" onselectedindexchanged="ddlClassId_SelectedIndexChanged">
            <asp:ListItem Value="请选择">请选择</asp:ListItem>
                    <asp:ListItem Value="新闻">新闻</asp:ListItem>
                    <asp:ListItem Value="产品">产品</asp:ListItem>
                    <asp:ListItem Value="单页面">单页面</asp:ListItem>
                    <asp:ListItem Value="人才招聘">人才招聘</asp:ListItem>
                    <asp:ListItem Value="下载">下载</asp:ListItem>
                    <asp:ListItem Value="友情链接">友情链接</asp:ListItem>
                    <asp:ListItem Value="在线留言">在线留言</asp:ListItem>
                    <asp:ListItem Value="广告图">广告图</asp:ListItem>
        </asp:DropDownList></td>
        <td width="50" align="right">关健字：</td>
        <td width="150"><asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword"></asp:TextBox></td>
        <td width="60" align="center"><asp:Button ID="btnSelect" runat="server" Text="查询" 
                CssClass="submit" onclick="btnSelect_Click" /></td>
        </tr>
    </table>
    <div class="spClear"></div>
    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="6%">编号</th>
        <th width="13%">页面形式名称</th>
        <th width="13%">页面形式描述</th>
        <th width="13%">时间</th>
        <th width="13%">排序</th>
      </tr>
      </HeaderTemplate>
      <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
        <td align="center"><asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>'></asp:Label></td>
        <td align="center"><a href="Edit.aspx?id=<%#Eval("Id") %>"><%#Eval("Title")%></a></td>
        <td align="center"><asp:Label ID="Label1" runat="server" Text='<%#Eval("ContentInfo")%>'></asp:Label></td>
        <td align="center"><asp:Label ID="Label2" runat="server" Text='<%#Eval("AddTime")%>'></asp:Label></td>
        <td align="center"><asp:TextBox ID="txt_sortId" runat="server"  text='<%# Eval("SortId")%>' Width="50px"></asp:TextBox></td>
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
                     <asp:LinkButton ID="lbtnMakeHtml" runat="server" onclick="lbtnMakeHtml_Click"   style="display:none"
                    >生成静态网页</asp:LinkButton>
                </span>
                
                  
            </div>
	</div>
    </form>
</body>
</html>

