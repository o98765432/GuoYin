<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DtCms.Web.Admin.Article.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=channelmodel.Title %>管理</title>
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
		    link_to:"?classid=<%=this.classId %>&<%=CombUrlTxt(Convert.ToInt32(this.ddlClassId.SelectedValue), this.keywords, this.property) %>page=__id__"
           });
        });
        function pageselectCallback(page_id, jq) {
           //alert(page_id); 回调函数，进一步使用请参阅说明文档
        }
        
        $(function() {
            $(".msgtable tr:nth-child(odd)").addClass("tr_bg"); //隔行变色
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
  <div class="navigation"><span class="add"><a href="Edit.aspx?classid=<%=this.classId %>">发布<%=channelmodel.Title %></a></span><b>您当前的位置：首页 &gt; <%=channelmodel.Title %>管理 &gt; <%=channelmodel.Title %>列表</b></div>
     <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="50" align="center">请选择：</td>
        <td>
            <asp:DropDownList ID="ddlClassId" runat="server" CssClass="select" 
                onselectedindexchanged="ddlClassId_SelectedIndexChanged" 
                AutoPostBack="True"></asp:DropDownList>&nbsp;
            <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select" 
                onselectedindexchanged="ddlProperty_SelectedIndexChanged" 
                AutoPostBack="True">
                <asp:ListItem Value="">所有属性</asp:ListItem> 
                <asp:ListItem Value="isTop">置顶</asp:ListItem>
                <asp:ListItem Value="isRed">推荐</asp:ListItem>
                <asp:ListItem Value="isHot">首页</asp:ListItem>
                 <asp:ListItem Value="isSlide">热点</asp:ListItem>
                <asp:ListItem Value="isLock">暂不发布</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td width="50" align="right">关健字：</td>
        <td width="150"><asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword"></asp:TextBox></td>
        <td width="60" align="center"><asp:Button ID="btnSelect" runat="server" Text="查询" 
                CssClass="submit" onclick="btnSelect_Click" /></td>
        </tr>
    </table>
    <div class="spClear"></div>
    <asp:Repeater ID="rptList" runat="server" onitemcommand="rptList_ItemCommand">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="6%">编号</th>
        <th align="left">文章标题</th>
        <th width="13%">所属类别</th>
        <th width="6%" <%if(classId!=99) {%> style="display:none"<%} %>>图片列表</th>
        <th width="16%">发布时间</th>
        <th width="110">属性排序</th>
        <th width="10%">操作可见性</th>
      </tr>
      </HeaderTemplate>
      <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
        <td align="center"><asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>'></asp:Label></td>
        <td><a href="edit.aspx?id=<%#Eval("Id") %>&classId=<%=this.classId %>&page=<%=page %>"><%#Eval("Title")%></a></td>
        <td align="center"><%# new DtCms.BLL.Channel().GetChannelTitle(Convert.ToInt32(Eval("ClassId")),ver)%></td>
        <td align="center"  <%if(classId!=99) {%> style="display:none"<%} %>><a href="../ProductKey/list.aspx?classId=<%#Eval("Id") %>&returnclassid=<%=this.classId %>&shownowid=1">图片列表</a></td>
        <td align="center"><%#string.Format("{0:g}", Eval("AddTime"))%></td>
        <td align="center">  
            
            <asp:TextBox ID="paixuSortId"  style="width: 50px; height: 16px; " text='<%# Eval("SortId")%>' runat="server"></asp:TextBox>
             
             
             </td>
        <td align="center">
        <asp:CheckBox checked='<%# Eval("isLock").ToString()=="1"%>' id="IsLock" runat="server" />
       </td>
        
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
                <span class="btn_all" CssClass="btn_all"  onclick="checkAll(this);">全选</span>
                <span class="btn_bg">
                  <asp:LinkButton ID="lbtnDel" runat="server" CssClass="btn_all" 
                    OnClientClick="return confirm( '确定要删除这些记录吗？ ');" onclick="lbtnDel_Click">删 除</asp:LinkButton>&nbsp;
                    
                  
                </span>
                <span class="btn_bg"  >
                 <asp:Button ID="Button1" runat="server"  onclick="lbtnMakeHtml_Click"  CssClass="btn_all"  Text="生成整个栏目" />

                 </span>
                   <span class="btn_bg"   >
                 <asp:Button ID="Button2" runat="server"  CssClass="btn_all"  Text="生成列表页" 
                    onclick="Button2_Click" />

                 </span>
                  <span class="btn_bg"  >
               
                 <asp:Button ID="Button3" runat="server"  CssClass="btn_all"  Text="生成单页" 
                    onclick="Button3_Click" />

                 </span>
                  <span class="btn_bg" >
                 <asp:Button ID="Button4" onclick="updateInfo_Click" runat="server" CssClass="btn_all"  Text="修改" />

                 </span> 
                &nbsp;
                 <span class="btn_all"  onclick="location.href='../ShowChanner/List.aspx?kindId=<%=classId %>&path=Article'">管理菜单</span>
            </div>
	</div>
    </form>
</body>
</html>
