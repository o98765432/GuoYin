<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Center.aspx.cs" Inherits="DtCms.Web.Admin.admin_center" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>管理中心首页</title>
    <link rel="stylesheet" type="text/css" href="images/style.css">
    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation"><span class="add"><a href="Config/admin_config.aspx">修改配置信息</a></span><b>您当前的位置：首页 &gt; 管理中心 &gt; 管理中心首页</b></div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th colspan="2" align="left">系统基本信息</th>
      </tr>
      <tr>
        <td align="right">生成网页：</td>
        <td>
          <asp:LinkButton ID="lbtnMakeHtml" runat="server" Visible="false" onclick="lbtnMakeHtml_Click" 
                    >生成首页</asp:LinkButton>
            <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" onclick="LinkButton1_Click" 
                    >生成整站静态化</asp:LinkButton>

            <asp:Button ID="Button1" runat="server" Text="生成首页"   onclick="lbtnMakeHtml_Click" />

            <asp:Button ID="Button2" runat="server" Text="生成整站静态化"   onclick="LinkButton1_Click" />
        </td>
      </tr>
      <tr>
        <td width="25%" align="right">网站名称：</td>
        <td width="75%">
          <%=showwebset!=null?showwebset.seoTitle:""%>
        </td>
      </tr>
      <tr>
        <td align="right">网站域名：</td>
        <td>
          <%=showwebset!=null?showwebset.other1:""%>
        </td>
      </tr>
      <tr>
        <td align="right">安装目录：</td>
        <td><%=webset.WebPath.ToString()%></td>
      </tr>
      <tr>
        <td align="right">后台目录：</td>
        <td>
          <%=webset.WebManagePath.ToString()%>
        </td>
      </tr>
      <tr>
        <td align="right">网站电话：</td>
        <td>
           <%=showwebset!=null?showwebset.tel:""%>
        </td>
      </tr>
      <tr style="display:none">
        <td align="right">传真号码：</td>
        <td>
           <%=showwebset!=null?showwebset.fax:""%>
        </td>
      </tr>
      
      <tr>
        <th colspan="2" align="left">服务器信息</th>
      </tr>
      <tr>
        <td align="right">服务器名称：</td>
        <td>
          <%=Server.MachineName%>
        </td>
      </tr>
      <tr>
        <td align="right">服务器IP：</td>
        <td>
          <%=Request.ServerVariables["LOCAL_ADDR"] %>
        </td>
      </tr>
      <tr>
        <td align="right">NET框架版本：</td>
        <td>
          <%=Environment.Version.ToString()%>
        </td>
      </tr>
      <tr>
        <td align="right">操作系统：</td>
        <td>
          <%=Environment.OSVersion.ToString()%>
        </td>
      </tr>
      <tr>
        <td align="right">IIS环境：</td>
        <td>
          <%=Request.ServerVariables["SERVER_SOFTWARE"]%>
        </td>
      </tr>
      <tr>
        <td align="right">服务器端口：</td>
        <td>
          <%=Request.ServerVariables["SERVER_PORT"]%>
        </td>
      </tr>
      <tr>
        <td align="right">虚拟目录绝对路径：</td>
        <td>
          <%=Request.ServerVariables["APPL_PHYSICAL_PATH"]%>
        </td>
      </tr>
      <tr>
        <td align="right">HTTPS支持：</td>
        <td>
          <%=Request.ServerVariables["HTTPS"]%>
        </td>
      </tr>
      <tr>
        <td align="right">seesion总数：</td>
        <td>
          <%=Session.Keys.Count.ToString()%>
        </td>
      </tr>
      
    </table>
    </form>
</body>
</html>
