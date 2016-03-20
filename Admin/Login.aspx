<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DtCms.Web.Admin.login" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ismarta信息管理系统</title>
    <link rel="stylesheet" type="text/css" href="images/css.css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="js/function.js"></script>
</head>
<body>
<form id="login_form" runat="server">


<div class="bodyBj">
	<div id="header">沙漠风，为您提供一体化的互联网品牌营销推广整合方案</div>
    <div class="logincon">
        <div class="login">
        <ul>
            <li><span class="user">用户名</span> <asp:TextBox ID="txtUserName" runat="server" CssClass="loginInput"
                            HintTitle="请输入登录帐号" HintInfo="用户名必须是字母或数字，不能包含空格或其它非法字符，不区分大小写。"></asp:TextBox>
                           
                     </li>
            <li><span class="user">密&nbsp;&nbsp;&nbsp;码</span><asp:TextBox ID="txtUserPwd" runat="server" CssClass="loginInput"
                            HintTitle="请输入登录密码" HintInfo="登录密码必须>=6位且是字母或数字，不能包含空格或其它非法字符，不区分大小写。" 
                            TextMode="Password"></asp:TextBox>
                           </li>
            <li>
                <span class="user">版&nbsp;&nbsp;&nbsp;本</span>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="cn" Selected="True">中文</asp:ListItem>
                    <asp:ListItem Value="en">英文</asp:ListItem>
                    <asp:ListItem Value="cn-tw">繁体中文</asp:ListItem>
                </asp:RadioButtonList>
            </li>
             <li><span class="user">验证码</span>

             <asp:TextBox ID="tboxValidator" runat="server" CssClass="loginInput1"></asp:TextBox>
				 
				<asp:Label ID="lblValidator" __designer:dtid="844476469739535" runat="server" ForeColor="Red"></asp:Label>

             <img runat="server" id="checkimg" style="cursor:pointer" src="checkCode.aspx" alt="点击更换"
					onclick="changecode()" />
                    
                    </li>
             <li> <asp:Button ID="loginsubmit" runat="server" Text="" CssClass="denglu margin" 
                     onclick="loginsubmit_Click" /></li>
             <li><br /><asp:Label ID="lbMsg"  Visible="false"
                          runat="server" Text="登录失败3次，需关闭后才能重新登录"></asp:Label></li>
        </ul>
        </div>
    	 
    </div>
    <div class="copy">Copyright Right &copy; <%=DateTime.Now.ToString("yyyy") %> 深圳市沙漠风网络科技有限公司</div>
    
    <div class="cloud">
	 <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
                width="100%" height="400">
                <param name="movie" value="images/bg.swf" />
                <param name="quality" value="high" />
                <param name="wmode" value="transparent" />
                <embed src="images/bg.swf" wmode="transparent" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                    type="application/x-shockwave-flash" width="100%" height="400"></embed>
            </object>  	
</div>
    
</div> 

<script type="text/javascript" language="javascript">

    function changecode() { 
        document.getElementById("checkimg").src = "../images/tb.gif";
        document.getElementById("checkimg").src = "checkCode.aspx"; 
    }
		 
	</script>

</form>
</body>
</html>
