<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="DtCms.Web.Admin.AllHtml.list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模板管理</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.pagination.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
     
   <style type="text/css">
   p{ float:left;}
   </style>
</head>

<body style="padding:10px;">
    <form id="form1" runat="server">
   <div class="navigation"> 
  <b>您当前的位置：首页 &gt 模板管理</b></div>
   <div class="spClear"></div>
     
     <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="20%">名称</th>
        <th width="70%">路径</th>
        <th align="10%">操作</th> 
      </tr>
     
      <%=GetAllFilesInDirectory(Server.MapPath("~/Admin/Template"),1) %>
      </table>
    
    </form>
</body>
</html>
