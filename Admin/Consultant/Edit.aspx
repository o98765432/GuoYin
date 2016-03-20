<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.Consultant.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
      <span class="back"><a href="List.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 留言管理 &gt; 回复留言</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">回复留言信息</th>
        </tr>
        <tr>
            <td width="50" height="50" class="bgname" style="text-align:right">Full Name:</td>
            <td width="1572">
            <asp:TextBox ID="txtFullName" runat="server" CssClass="input w380 required" 
                    maxlength="250" minlength="3" HintTitle="名称" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Nationality:</td>
            <td width="756">
            <asp:TextBox ID="txtNationality" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="国籍" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Company Name:</td>
            <td width="756">
                 <asp:TextBox ID="txtCompanyName" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="公司名称" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Contact:</td>
            <td width="756">
                 <asp:TextBox ID="txtContact" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="联系方式" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="240" height="50" class="bgname" style="text-align:right">Email:</td>
            <td width="756">
                 <asp:TextBox ID="txtEmail" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="发布的文章标题" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Date:</td>
            <td width="756">
             <asp:TextBox ID="txtDate" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="时间" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100" height="50" class="bgname" style="text-align:right">Telephone NO:</td>
            <td width="756">
                 <asp:TextBox ID="txtTelephont" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="电话" HintInfo="控制在100个字数内，标题文本尽量不要太长。" Enabled="false"></asp:TextBox>
            </td>
        </tr>
    </table>

    <div class="tablefege" style="margin-left:100px">
	<div class="innertablefege">
    	<h2>(Better to Contact by: A. Email; B. Call; C. Both)</h2>
        <h3>1.How long have you stayed in China?</h3>
        <span>
            <asp:TextBox ID="txtStayChina" runat="server" width="670px" Height="122px" Enabled="false"></asp:TextBox>
        </span>
        <h3>2.Have you ever studied Mandarin before? If yes, where did you learn it? And how long?</h3>
        <span>
             <asp:TextBox ID="txtMandarin" runat="server"  width="670px" Height="122px" Enabled="false"></asp:TextBox>
        </span>
        <h3>3.How did you learn Mandarin? (if your answer is yes to question 2)</h3>

               <asp:RadioButton ID="TrainingSchool" runat="server"  Text="TrainingSchool" Enabled="false"/>
               <asp:RadioButton ID="University" runat="server" Text="University" Enabled="false"/> 
               <asp:RadioButton ID="PrivateTutor" runat="server" Text="PrivateTutor" Enabled="false"/> 
               <asp:RadioButton ID="SelfStudy" runat="server" Text="SelfStudy" Enabled="false"/> 
       <h3>4.Which district do you live in?</h3>
               <asp:RadioButton ID="Shekou" runat="server" Text="Shekou" Enabled="false"/>
               <asp:RadioButton ID="Nanshan" runat="server" Text="Nanshan" Enabled="false"/> 
               <asp:RadioButton ID="Futian" runat="server" Text="Futian" Enabled="false"/> 
               <asp:RadioButton ID="Luohu" runat="server" Text="Luohu" Enabled="false"/> 
               <asp:RadioButton ID="Baoan" runat="server" Text="Baoan" Enabled="false"/> 
               <asp:RadioButton ID="Longgang" runat="server" Text="Longgang" Enabled="false"/> 
        <h3>5.What's your goal of learning Mandarin? </h3>
               <asp:RadioButton ID="Business" runat="server" Text="Business" Enabled="false"/> 
               <asp:RadioButton ID="DailyLife" runat="server" Text="DailyLife" Enabled="false"/> 
               <asp:RadioButton ID="Selfdevelopment" runat="server" Text="Selfdevelopment" Enabled="false"/> 
               <asp:RadioButton ID="HSKtest" runat="server" Text="HSKtest" Enabled="false"/> 
               <asp:RadioButton ID="MandarinOthers" runat="server" Text="MandarinOthers" Enabled="false"/> 
        <h3>6.How many hours can you make for Mandarin courses per week?</h3>
               <asp:RadioButton ID="FourHours" runat="server" Text="FourHours" Enabled="false"/> 
               <asp:RadioButton ID="SixHours" runat="server" Text="SixHours" Enabled="false"/> 
               <asp:RadioButton ID="TenHours" runat="server" Text="TenHours" Enabled="false"/> 
               <asp:RadioButton ID="Everydayfordailycourses" runat="server" Text="Everydayfordailycourses" Enabled="false"/> 
               <asp:RadioButton ID="Weekendonly" runat="server" Text="Weekendonly" Enabled="false" /> 
               <asp:RadioButton ID="weekOthers" runat="server" Text="weekOthers" Enabled="false" /> 
        <h3>7.What's your best time for classes?</h3>
               <asp:RadioButton ID="Mornings" runat="server" Text="Mornings" Enabled="false"/> 
               <asp:RadioButton ID="Afternoons" runat="server" Text="Afternoons" Enabled="false"/> 
               <asp:RadioButton ID="Evenings" runat="server" Text="Evenings" Enabled="false"/> 
               <asp:RadioButton ID="Weekdays" runat="server" Text="Weekdays" Enabled="false"/> 
               <asp:RadioButton ID="Weekends" runat="server" Text="Weekends" Enabled="false"/> 
        <h3>8.What type of classes are you interested in?</h3>
               <asp:RadioButton ID="SmallGroupClass" runat="server" Text="SmallGroupClass" Enabled="false"/> 
               <asp:RadioButton ID="StandardGroupClass" runat="server" Text="StandardGroupClass" Enabled="false"/> 
               <asp:RadioButton ID="BigGroupClass" runat="server" Text="BigGroupClass" Enabled="false"/> 
               <asp:RadioButton ID="PrivateClass" runat="server" Text="PrivateClass" Enabled="false"/> 
               <asp:RadioButton ID="ImmersionClass" runat="server" Text="ImmersionClass" Enabled="false"/> 
               <asp:RadioButton ID="IntensiveClass" runat="server" Text="IntensiveClass" Enabled="false"/> 
        <h3>9.How did you know about us?</h3>
               <asp:RadioButton ID="WordofMouth" runat="server" Text="WordofMouth" Enabled="false"/> 
               <asp:RadioButton ID="BoothAdverts" runat="server" Text="BoothAdverts" Enabled="false"/> 
               <asp:RadioButton ID="Referral" runat="server" Text="Referral" Enabled="false"/> 
               <asp:RadioButton ID="Website" runat="server" Text="Website" Enabled="false"/> 
               <asp:RadioButton ID="aboutOthers" runat="server" Text="aboutOthers" Enabled="false"/> 
       <h3>10.What other information would you like to know? Please impart your requests.</h3>
        <span>
            <asp:TextBox ID="txtSomething" runat="server"  width="670px" Height="122px" Enabled="false"></asp:TextBox>
        </span>
    </div>
</div>
    </form>
</body>
</html>
