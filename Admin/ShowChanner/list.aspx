<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="DtCms.Web.Admin.ShowChanner.list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>类别管理</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".msgtable tr:nth-child(odd)").addClass("tr_bg"); //隔行变色
            $(".msgtable tr").hover(
			    function () {
			        $(this).addClass("tr_hover_col");
			    },
			    function () {
			        $(this).removeClass("tr_hover_col");
			    }
		    );
        });
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
        <div class="navigation">
            <span>
                <asp:LinkButton ID="lbtAddTopChanner" CssClass="add" runat="server">增加顶级类别</asp:LinkButton>
       &nbsp;&nbsp;
            <asp:LinkButton ID="lbtBack" runat="server">返回列表</asp:LinkButton>
            </span>    
            <b>您当前的位置：首页 &gt; 类别管理 &gt; 类别列表</b></div>
            <div class="spClear"></div>
         <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                    <tr>
                        <th width="7%">编号</th>
                        <th align="left"">类别名称</th>
                        <th width="90">优先级别</th>
                        <th width="150">管理操作</th>
                    </tr>
         <%=new DtCms.BLL.Channel().returnAllMenu(kindId, showkindid)%>
        </table>
     </form>
</body>
</html>
