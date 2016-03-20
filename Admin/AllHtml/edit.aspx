<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DtCms.Web.Admin.AllHtml.edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script> 
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
   
    <script type="text/javascript">

        $(function () {
            //表单验证JS
            $("#form1").validate({
                //出错时添加的标签
                errorElement: "span",
                success: function (label) {
                    //正确时的样式
                    label.text(" ").addClass("success");
                }
            });

        });
    </script>
</head>
 
<body style="padding: 10px;">
    <form id="form1" runat="server" method="post">
    <div class="navigation">
        <span class="back"><a href="List.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 模板管理 &gt;
            模板编辑</b>
    </div>
    <div class="spClear">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="3" align="left">
                模板信息
            </th>
        </tr>
        <tr>
            <td align="right" valign="top">
                路径：
            </td>
            <td>
                <asp:TextBox ID="txttitle" runat="server" ReadOnly="true"  CssClass="input w380 required" MaxLength="250"
                    minlength="2" HintTitle="标题" HintInfo="标题"></asp:TextBox> 
            </td>
        </tr>
          

         <tr>
            <td align="right" valign="top">
                详细信息：
            </td>
            <td>
                <textarea id="txtContent" cols="100" rows="8" style="width: 100%; height: 500px;" runat="server"></textarea>
            </td>
        </tr>
    </table>
    <div style="margin-top: 100px; text-align: center;">
        <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" OnClick="btnSave_Click" />
        &nbsp;
        <input name="重置" type="reset" class="submit" value="重置" />
    </div>
    </form>
</body>
</html>
