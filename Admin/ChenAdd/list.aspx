﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="DtCms.Web.Admin.ChenAdd.list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
		    link_to:"?classid=<%=this.classId %>&<%=CombUrlTxt(this.classId, this.keywords, this.property) %>page=__id__"
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
<body style="padding: 10px;">
    <form id="form1" runat="server">
    <div class="navigation">
        <span class="add"><a href="Edit.aspx?classid=<%=this.classId %>">发布扩展字段</a></span><b>您当前的位置：首页
            &gt; 扩展字段管理 &gt; 扩展字段列表</b></div>
    <div class="spClear">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="50" align="right">
                关健字：
            </td>
            <td width="150">
                <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword"></asp:TextBox>
            </td>
            <td width="60" align="center">
                <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="submit" OnClick="btnSelect_Click" />
            </td>
        </tr>
    </table>
    <div class="spClear">
    </div>
    <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                <tr>
                    <th width="6%">
                        选择
                    </th>
                    <th width="6%">
                        编号
                    </th>
                    <th align="left">
                        标题
                    </th>
                    <th width="15%">
                        字段
                    </th>
                    <th width="10%">
                        描述
                    </th>
                    <th width="10%">
                        类别
                    </th>
                    <th width="13%">
                        类型
                    </th>
                    <th width="10%">
                        字段类型
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" />
                </td>
                <td align="center">
                    <asp:Label ID="lb_id" runat="server" Text='<%#Eval("id")%>'></asp:Label>
                </td>
                <td>
                    <a href="edit.aspx?id=<%#Eval("id") %>">
                        <%#Eval("title")%></a>
                </td>
                <td align="center">
                    <%#Eval("ziduan")%>
                </td>
                <td align="center">
                    <%#Eval("memo")%>
                </td>
                <td align="center">
                      <%# DtCms.BLL.Chenadd.returnTypeid(Convert.ToInt32(Eval("typeid")))%>
                </td>
                <td align="center">
                     <%# DtCms.BLL.Chenadd.returnType(Convert.ToInt32(Eval("type")))%>
                </td>
                <td align="center">
                    <%# new DtCms.BLL.Channel().GetChannelTitle(Convert.ToInt32(Eval("classid")),"cn")%>
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
                <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="return confirm( '确定要删除这些记录吗？ ');"
                    OnClick="lbtnDel_Click">删 除</asp:LinkButton>&nbsp; </span>
        </div>
    </div>
    </form>
</body>
</html>
