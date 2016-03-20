<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Index.aspx.cs" Inherits="DtCms.Web.Admin.admin_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=showwebset!=null?showwebset.seoTitle:""%> - 后台管理</title>
   <link href="../css/custom-theme/jquery-ui-1.7.2.custom.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="images/style.css">
    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui-1.7.2.custom.min.js"></script>
    <script type="text/javascript" src="js/function.js"></script>

</head>
<body>
<form id="form1" runat="server">

<table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%" style="background:#EBF5FC;">
<tbody>
  <tr>
    <td height="70" colspan="4" style="background:url(images/head_bg.gif);"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="24%" height="70"><div style="padding-left:20px"><img src="images/TopLogo.gif"></div>
       
        </td>
        <td width="76%" valign="bottom"> 
          <div id="tabs">

         
           
          </div>

        </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="30" colspan="4" style="padding:0px 10px;font-size:12px;background:url(images/navsub_bg.gif) repeat-x;">
    <div style="float:right;line-height:20px;"><a href="admin_center.aspx" target="sysMain">管理中心</a> | 
        <a target="_blank" href="../">预览网站</a> |  <asp:Button runat="server" ID="shownewbutton"  Text="安全退出"  onclick="lbtnExit_Click"/> 
        </div>
    <div style="padding-left:20px;line-height:20px;background:url(images/siteico.gif) 0px 0px no-repeat;">当前登录用户：<font color="#FF0000"><asp:Label
        ID="lblAdminName" runat="server" Text="Label"></asp:Label></font>您好，欢迎光临。</div>
    </td>
  </tr>

  <tr>
    <td width="200px">
       <div style="width:200px; height:100%; overflow:hidden;" >
         <iframe src="Left.aspx" height="100%" frameborder="0" scrolling="auto" width="200px" t></iframe>
        </div>  

        
    </td>
   
  
    <td align="middle" id="mainLeft" valign="top" style="background:#FFF;">
	  <div style="text-align:left;width:185px;height:100%;font-size:12px;display:none" >
         
      </div>
      
	</td>
 
	<td valign="middle" style="width:8px;background:url(images/main_cen_bg.gif) repeat-x;">
      <div id="sysBar" style="cursor:pointer;"><img id="barImg" src="images/butClose.gif" alt="关闭/打开左栏" /></div>
	</td>
    
	<td style="width:100%" valign="top">
      <iframe frameborder="0" id="sysMain" name="sysMain" scrolling="yes" src="admin_center.aspx" style="height:100%;visibility:inherit; width:100%;z-index:1;"></iframe>
	</td>
  </tr>
  <tr>
    <td height="28" colspan="4" bgcolor="#EBF5FC" style="padding:0px 10px;font-size:10px;color:#2C89AD;background:url(images/foot_bg.gif) repeat-x;">Copyright Right © <%=DateTime.Now.ToString("yyyy") %>  <a href="http://www.szweb.cn/" target="_blank">沙漠风</a></td>
  </tr>
  </tbody>
</table>

</form>
</body>
</html>