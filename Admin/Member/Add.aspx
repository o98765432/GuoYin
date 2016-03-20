<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="DtCms.Web.Admin.Member.Add" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>增加会员</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript" src="../../KindEditor/kindeditor.js"></script>
    <script type="text/javascript">

        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('#txtContent',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });
        });

        /*
        KE.show({
            id : 'txtContent',
            imageUploadJson : '../../../Tools/upload_json.ashx',
            fileManagerJson : '../../../Tools/file_manager_json.ashx',
            allowFileManager : true
        });
      */  
        $(function() {
            //表单验证JS
            $("#form1").validate({
                //出错时添加的标签
                errorElement: "span",
                success: function(label) {
                    //正确时的样式
                    label.text(" ").addClass("success");
                }
            });
        });
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="List.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 会员模块 &gt; 增加会员</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">增加会员信息</th>
        </tr>
        <tr>
            <td width="15%" align="right">OA用户名：</td>
            <td width="85%">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input w380 required" 
            maxlength="250" HintTitle="会员名" HintInfo="控制在100个字数内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td align="right">密码：</td>
            <td>
                <asp:TextBox ID="txtUserPwd" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td align="right">真实姓名：</td>
            <td>
                <asp:TextBox ID="txtTrueName" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">性别：</td>
            <td>
                <asp:RadioButton runat="server" ID="rbMale" GroupName="sex" Checked="true" Text="男" />
                <asp:RadioButton runat="server" ID="rbFemale" GroupName="sex" Text="女" />
            </td>
        </tr>
        <tr>
            <td align="right">从属机构：</td>
            <td>
                <asp:TextBox ID="txtCongshujigou" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">邮箱：</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">所属栏目：</td>
            <td><asp:DropDownList id="ddlClassId" CssClass=" required" runat="server"></asp:DropDownList></td>
        </tr>
        <tr style="display:none">
            <td align="right">预览图：</td>
            <td>
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="FileUpload1" name="FileUpload1" onchange="SingleUpload('txtImgUrl','FileUpload1')" /></a>友情链接尺寸(176*95)|品牌荣耀尺寸(199*226)
                <span class="uploading">正在上传，请稍候...</span>
            </td>
        </tr>
       
        <tr>
            <td align="right" valign="top">详细介绍：</td>
            <td>
              <textarea id="txtContent" cols="100" rows="8" style="width:100%;height:400px;visibility:hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">信息属性：</td>
            <td>
                 
                <asp:CheckBoxList ID="cblItem" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  
                    <asp:ListItem Value="1">冻结</asp:ListItem>
                   
                </asp:CheckBoxList>
            </td>
        </tr>
        
       
    </table>
    <div style="margin-top:10px;text-align:center;">
      <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" onclick="btnSave_Click" />
      &nbsp;
      <input name="重置" type="reset" class="submit" value="重置" />
    </div>
    </form>
</body>
</html>
