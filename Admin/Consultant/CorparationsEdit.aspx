<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorparationsEdit.aspx.cs" Inherits="DtCms.Web.Admin.Consultant.CorparationsEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>管理</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.pagination.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="CorparationsList.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 留言管理 &gt; 回复留言</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">回复留言信息</th>
        </tr>
        <tr>
            <td width="50" height="50" class="bgname" style="text-align:right">Company Name:</td>
            <td width="1572">
            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="input w380 required" 
                    maxlength="250" minlength="3" HintTitle="名称" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Website:</td>
            <td width="756">
            <asp:TextBox ID="txtWebsite" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="国籍" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Industry:</td>
            <td width="756">
                 <asp:TextBox ID="txtIndustry" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="公司名称" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">The Number of Expats:</td>
            <td width="756">
                 <asp:TextBox ID="txtExpats" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="联系方式" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="240" height="50" class="bgname" style="text-align:right">Company Add:</td>
            <td width="756">
                 <asp:TextBox ID="txtCompayAdd" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="发布的文章标题" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Country:</td>
            <td width="756">
             <asp:TextBox ID="txtCountry" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="时间" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">city:</td>
            <td width="756">
                 <asp:TextBox ID="txtCity" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="电话" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">state:</td>
            <td width="756">
                 <asp:TextBox ID="txtState" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="电话" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Contactor Name:</td>
            <td width="756">
                 <asp:TextBox ID="txtContactorName" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="电话" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Contactor E-mail:</td>
            <td width="756">
                 <asp:TextBox ID="txtContactorEmail" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="电话" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Telephone NO:</td>
            <td width="756">
                 <asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="电话" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Your requests of learning:</td>
            <td width="756">
                 <asp:TextBox ID="txtLearning" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="电话" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
    </table>


    </form>
</body>
</html>

