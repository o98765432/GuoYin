<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="DtCms.Web.Admin.FriendshipLink.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>增加内容</title>
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

            //显示关闭高级选项
            $("#upordown").toggle(function () {
                $(this).text("关闭高级选项");
                $(this).removeClass();
                $(this).addClass("up-01");
                $(".upordown").show();
            }, function () {
                $(this).text("显示高级选项");
                $(this).removeClass();
                $(this).addClass("up-02");
                $(".upordown").hide();
            });
        });
    </script>

     <script type="text/javascript">
         function radNeilianClick() {
             $("#radWailians").attr("checked", false);
             $("#neilians").css("display", "block");
             $("#wailians").css("display", "none");
         }


         function radWailianClick() {
             $("#radNeilians").attr("checked", false);
             $("#neilians").css("display", "none");
             $("#wailians").css("display", "block");
         }
    </script>

</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="List.aspx?classId=<%=this.classid %>">返回列表</a></span><b>您当前的位置：首页 &gt; 内容管理 &gt; 增加内容</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">增加内容</th>
        </tr>
        <tr>
            <td width="15%" align="right">友情链接名称：</td>
            <td width="85%">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input w380 required" 
            maxlength="100" minlength="3" HintTitle="增加的内容信息标题" HintInfo="控制在100个字数内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
  
         <tr>
            <td align="right" class="style1">类别栏目：</td>
            <td><asp:DropDownList id="ddlClassId" CssClass=" required" runat="server" 
                    onselectedindexchanged="ddlClassId_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
        </tr>

        <tr>
            <td align="right">优先级别：</td>
            <td>
            <asp:TextBox ID="txtSortId" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="内容的优先级别" HintInfo="纯数字，数值越小越靠前。">0</asp:TextBox>

            <span id="upordown" class="up-02">显示高级选项</span>
            </td>
        </tr>

        <tr>
            <td align="right">可见性：</td>
            <td>
            <asp:TextBox ID="txtIslock" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="内容的优先级别" HintInfo="纯数字，数值越小越靠前。">0</asp:TextBox>
            </td>
        </tr>
           <tr>
            <td  width="25%"  align="right">友情链接图片：</td>
            <td width="75%">
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="FileUpload1" name="FileUpload1" onchange="SingleUpload('txtImgUrl','FileUpload1')" /></a>
                <asp:Literal runat="server" ID="litSize"></asp:Literal>
                <span class="uploading">正在上传，请稍候...</span>
            </td>
        </tr>
        <tr>
             <td  width="25%"  align="right">友情链接地址：</td>
            <td width="75%">
                <asp:TextBox ID="txtHref" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
          <tr>
            <td align="right" valign="top">静态化模版：</td>
            <td>
              <asp:TextBox ID="txtFilepath" runat="server" CssClass="input w380" 
             HintTitle="静态化模版" HintInfo="非专业人士指导请勿改变此处"></asp:TextBox>
            </td>
        </tr>
        
    </table>
    <div style="margin-top:10px;text-align:center;">

  &nbsp;<asp:Button ID="btnSave" runat="server" Text="保存" 
            onclick="btnSave_Click" CssClass="submit" />
  <input name="重置" type="reset" class="submit" value="重置" />
</div>
    </form>
</body>
</html>

