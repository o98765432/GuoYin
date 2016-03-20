<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="DtCms.Web.Admin.ProductKey.list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>详细参数管理</title>
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
		    link_to:"?classid=<%=this.classId %>&returnclassid=<%=this.returnclassid%>&shownowid=<%=this.shownowid%>&page=__id__"
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
        $(document).ready(function(){
        
          $("#updateAllInfo").click(function(){
          
          var allinfo ="";

          for(i=0;i<$(".shownowid").length;i++)
          {
          
          if(allinfo ==""){

          allinfo=$(".shownowid").eq(i).html()+","+$(".shownowsortid").eq(i).val();

          }
          else{
          
          allinfo+="*"+$(".shownowid").eq(i).html()+","+$(".shownowsortid").eq(i).val();
          
          }
            
          
          }

          location.href="list.aspx?classid=<%=this.classId %>&returnclassid=<%=this.returnclassid%>&shownowid=<%=this.shownowid%>&allinfo="+allinfo;
            
          
          });
        
        });
    </script>

     
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
   <div class="navigation">
   <%if(addflag == true){%>
   <span class="add"><a href="Edit.aspx?classid=<%=this.classId %>&returnclassid=<%=this.returnclassid%>&shownowid=<%=this.shownowid%>">发布详细参数</a></span>
   <%} %>
   <b>您当前的位置：首页 &gt; 详细参数管理 &gt; 详细参数列表</b></div>
   <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="display:none">
      <tr>
        <td width="50" align="center">请选择：</td>
        <td>
            <asp:DropDownList ID="ddlClassId" runat="server" CssClass="select" 
                AutoPostBack="True" onselectedindexchanged="ddlClassId_SelectedIndexChanged"></asp:DropDownList>&nbsp;
           
        </td>
        <td width="50" align="right">关健字：</td>
        <td width="150"><asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword"></asp:TextBox></td>
        <td width="60" align="center"><asp:Button ID="btnSelect" runat="server" Text="查询" 
                CssClass="submit" onclick="btnSelect_Click" /></td>
        </tr>
    </table>
    <div class="spClear"></div>

    <!--列表展示开始-->
    <asp:Repeater ID="rptBanner" runat="server" onitemcommand="rptList_ItemCommand">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="6%">编号</th>
        <th align="left">名称</th>
        <th width="13%">图片</th> 
        <th width="16%">发布时间</th>
        <th width="90">排序</th>

        <th width="110" style="display:none">属性</th>
        
      </tr>
      </HeaderTemplate>
      <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
        <td align="center"><asp:Label ID="lb_id" runat="server" class="shownowid" Text='<%#Eval("Id")%>'></asp:Label></td>
        <td><a href="Edit.aspx?id=<%#Eval("Id") %>&classid=<%=this.classId %>&returnclassid=<%=Request.QueryString["returnclassid"] %>&shownowid=<%=shownowid %>"><%#Eval("subject")%></a></td>
        <td align="center">
            <asp:Image ID="Image1" src='<%# Eval("blinfo1")%> ' runat="server" Width="50px" Height="50px" /></td>
        <td align="center"><%#string.Format("{0:g}",Eval("AddTime"))%></td> 
        <td align="center">
            <input type="text" name ="paixuSortId"  id="<%# Eval("Id")%>" class="shownowsortid"  value='<%# Eval("SortId")%>'  style="width: 50px; height: 16px; "  onblur ="getPaixu(this);"  /> </td>
      
       
       
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
                    OnClientClick="return confirm( '确定要删除这些记录吗？ ');" onclick="lbtnDel_Click">删 除</asp:LinkButton>
                </span>
                <span class="btn_all" >
                    <linkbutton  id="updateAllInfo"  >修改</linkbutton>
                </span>&nbsp;
                <span class="btn_all" onclick="location.href='../Article/list.aspx?classid=<%=Request.QueryString["returnclassid"] %>&showmatypeid=<%=Request.QueryString["returnclassid"] %>'">返回上一级列表</span>
            </div>
	</div>
    </form>
</body>
</html>
