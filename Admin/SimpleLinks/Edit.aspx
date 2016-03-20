<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.SimpleLinks.Edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑图文链接</title>
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
      <span class="back"><a href="List.aspx?classId=<%=drpClassId %>">返回列表</a></span><b>您当前的位置：首页 &gt; 图文链接管理 &gt; 编辑图文链接</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">编辑图文链接信息</th>
        </tr>
        <tr>
            <td width="15%" align="right">文件标题：</td>
            <td width="85%">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="发布的标题" HintInfo="控制在100个字数内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
         <tr >
            <td width="15%" align="right">副标题：</td>
            <td width="85%">
            <asp:TextBox ID="txtSubTitle" runat="server" CssClass="input w380" 
            maxlength="250" minlength="3" HintTitle="发布副标题" HintInfo="控制在500个字数内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">所属栏目：</td>
            <td><asp:DropDownList id="ddlClassId" CssClass=" required" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right">预览图：</td>
            <td>
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="FileUpload1" name="FileUpload1" onchange="SingleUpload('txtImgUrl','FileUpload1')" /></a>
                <span class="uploading">正在上传，请稍候...</span>
                <a rel="<%=img%>" href="<%=img%>"><img src="<%=img %>" width="100px"/></a>
                <br />
                首页大图:707px*487px|小图:233px*161px|各页大图:963px*192px
            </td>
        </tr>
        <tr>
            <td align="right">链接地址：</td>
            <td>
                <asp:TextBox ID="txtFilePath" runat="server" CssClass="input w380 left"></asp:TextBox>
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
         <td width="25%" align="right">优先级别：</td>
         <td width="75%">
            <asp:TextBox ID="txtSortId" CssClass="input required number" size="10" runat="server" maxlength="9" HintTitle="类别分类优先级别" HintInfo="纯数字，数字越少越向前。" ></asp:TextBox>
         </td>
       </tr>
        <tr>
            <td align="right">信息属性：</td>
            <td>
                 
                <asp:CheckBoxList ID="cblItem" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                   <asp:ListItem Value="1" Enabled="false">允许评论</asp:ListItem>
                    <asp:ListItem Value="1" Enabled="false">推荐</asp:ListItem>
                    <asp:ListItem Value="1">隐藏</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        
       
    </table>
    <div style="margin-top:10px;text-align:center;">
      <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" onclick="btnSave_Click" />
      &nbsp;
      <input name="重置" type="reset" class="submit" value="重置" />
    </div>

    <link href="../css/jquery.lightbox-0.5.css" rel="stylesheet" type="text/css" />
<script src="../js/jquery.lightbox-0.5.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () { $('a[rel]').lightBox(); });
</script>

    </form>
</body>
</html>
