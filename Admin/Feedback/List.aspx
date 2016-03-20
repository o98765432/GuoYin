<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DtCms.Web.Admin.Feedback.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>留言管理</title>
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
		    link_to:"?classid=<%=this.classId %>&<%=CombUrlTxt(this.property, this.keywords) %>page=__id__"
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
<body style="padding: 10px;">
    <form id="form1" runat="server">
    <div class="navigation">
        <b>您当前的位置：首页 &gt; 留言管理 &gt; 留言列表</b></div>
    <div class="spClear">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="0"  style="display:none">
                请选择：
            </td>
            <td style="display:none">
                <asp:DropDownList ID="ddlClassId" runat="server" Visible="false" CssClass="select"
                    OnSelectedIndexChanged="ddlClassId_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProperty_SelectedIndexChanged">
                    <asp:ListItem Value="">所有属性</asp:ListItem>
                    <asp:ListItem Value="isLock">待审核</asp:ListItem>
                    <asp:ListItem Value="unIsLock">已审核</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="50">
                关健字：
            </td>
            <td width="10" >
                <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword"></asp:TextBox>
            </td>
            <td >
                <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="submit" OnClick="btnSelect_Click" />
            </td>
            <td width="400"></td>
        </tr>
    </table>
    <div class="spClear">
    </div>
    <asp:Repeater ID="rptList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                <tr>
                    <th width="6%">
                        选择
                    </th>
                    <th width="6%">
                        编号
                    </th>
                    <%
                        if (showda != null && showda.Tables[0].Rows.Count > 0)
                        {

                            for (int i = 0; i < (showda.Tables[0].Rows.Count > 2 ? 2 : showda.Tables[0].Rows.Count); i++)
                            {
                    %>
                    <th width="25%">
                        <%=showda.Tables[0].Rows[i]["Title"].ToString()%>
                    </th>
                    <%}
                        } %>
                    <th width="10%">
                        发布时间
                    </th>
                     <th width="20%">
                        留言主题：
                    </th>
                    <th width="10">
                        联系人
                    </th>
                     <th width="10%">
                        E-mail
                    </th>
                    <%if (classId == 42)
                      { %>
                      <th width="10%">
                        业务类型
                    </th>
                          <% } %>
                           <th width="10%">
                        电话
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" />
                </td>
                <td align="center">
                  <a href="edit.aspx?id=<%#Eval("Id") %>&classId=<%=this.classId %>">    <asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>'></asp:Label></a>
                </td>
                <%
                    if (strnames.Count > 0)
                    {
                %>
                <td align="center">
                    <%# DtCms.Common.Utils.CutString((strnames.Count > 0?Eval(strnames[0]):"").ToString(),40)%>
                </td>
                <%
                    } %>
                <%
                    if (strnames.Count > 1)
                    {
                %>
                <td align="center">
                    <%#(strnames.Count > 1?Eval(strnames[1]):"")%>
                </td>
                <%
                    } %>
                <%
                    if (strnames.Count > 2)
                    {
                %>
                <td align="center">
                    <%#(strnames.Count > 2?Eval(strnames[2]):"")%>
                </td>
                <%
                    } %>
                <td align="center">
                    <%#string.Format("{0:g}",Eval("AddTime"))%>
                </td>
                <td align="center">
                   <a href="edit.aspx?id=<%#Eval("Id") %>&classId=<%=this.classId %>">  <%#string.Format("{0:g}",Eval("Title"))%></a>
                </td>
                 <td align="center">
                    <%#string.Format("{0:g}",Eval("UserName"))%>
                </td>
                 <td align="center">
                    <%#string.Format("{0:g}", Eval("MailBox"))%>
                </td>
                 <%if (classId==42)
                      {%>
                           <td align="center">
                   <a href="/Admin/Product/edit.aspx?id=<%#Eval("OrderNumber")%>&classid=41">  <%#string.Format("{0:g}", Eval("UserSex"))%></a>
                </td> 
                     <% } %>
                     <td align="center">
                    <%#string.Format("{0:g}", Eval("UserTel"))%>
                </td> 
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div class="spClear">
    </div>
    <div style="line-height: 30px; height: 30px;">
        <div id="Pagination" class="right flickr">
        </div>
        <div class="left">
            <span class="btn_all" onclick="checkAll(this);">全选</span> <span class="btn_bg">
                <asp:LinkButton ID="lbtnAudit" Visible="false" runat="server" OnClick="lbtnAudit_Click">审 核</asp:LinkButton>
                &nbsp;<asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="return confirm( '确定要删除这些记录吗？ ');"
                    OnClick="lbtnDel_Click">删 除</asp:LinkButton>
            </span>
        </div>
    </div>
    </form>
</body>
</html>
