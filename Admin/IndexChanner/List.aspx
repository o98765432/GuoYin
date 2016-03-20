<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DtCms.Web.Admin.IndexChanner.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
    <div class="navigation">
    
    <span class="add"></span><b>您当前的位置：首页 &gt; 快捷通道信息 &gt; 快捷通道列表</b></div>
    <div class="spClear"></div>
    <table width="99%" border="0" cellspacing="0" cellpadding="0" class="msgtable" align="center">
      <tr> 
        <th align="left">标题</th>
        
        <th width="8%">操作</th>
      </tr>
      <tr> 
        <td><a href="Edit.aspx?id=1">学生服务</a></td>
        
        <td  align="center"><a href="Edit.aspx?id=1">查看</a></td>
      </tr>
      <tr> 
        <td><a href="Edit.aspx?id=2">家长关注</a></td>
        
        <td  align="center"><a href="Edit.aspx?id=2">查看</a></td>
      </tr>
      <tr> 
        <td><a href="Edit.aspx?id=3">教师</a></td>
        
        <td  align="center"><a href="Edit.aspx?id=3">查看</a></td>
      </tr>
      <tr> 
        <td><a href="Edit.aspx?id=4">校友</a></td>
        
        <td  align="center"><a href="Edit.aspx?id=4">查看</a></td>
      </tr>
       </table>
    </form>
</body>
</html>
