<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.ArticlePictures.Edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑资讯</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>

    <script language="javascript" src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>

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
            //显示关闭高级选项
//            $("#upordown").toggle(function() {
//                $(this).text("关闭高级选项");
//                $(this).removeClass();
//                $(this).addClass("up-01");
//                $(".upordown").show();
//            }, function() {
//                $(this).text("显示高级选项");
//                $(this).removeClass();
//                $(this).addClass("up-02");
//                $(".upordown").hide();
//            });
        });
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="List.aspx?parentId=<%=Request.QueryString["ParentId"]%>&kindId=<%=Request.QueryString["kindId"] %>">返回列表</a></span><b>您当前的位置：首页 &gt; 资讯管理 &gt; 编辑资讯</b>
    </div>
    <div style="padding-bottom:10px;"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">发布资讯</th>
        </tr>
        <tr>
            <td width="15%" align="right">文章图片标题：</td>
            <td width="85%">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input w380" 
            maxlength="250" minlength="3" HintTitle="发布的文章标题" HintInfo="控制在100个字数内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">日期：</td>
            <td>
           <asp:TextBox ID="txtAddTime" runat="server" onfocus="WdatePicker()" CssClass="input required"></asp:TextBox>
          
            </td>
        </tr>
       
        <tr class="upordown hide">
            <td align="right">Meta关键字：</td>
            <td>
            <asp:TextBox ID="txtKeyword" runat="server" CssClass="input w250" 
            maxlength="100" HintTitle="Meta关键字" HintInfo="用于搜索引擎，如有多个关健字请用英文的,号分隔，不填写将自动提取标题。"></asp:TextBox>
            </td>
        </tr>
        <tr class="upordown hide">
            <td align="right">Meta描述：</td>
            <td>
            <asp:TextBox ID="txtZhaiyao" runat="server" CssClass="textarea wh380"  
            maxlength="250" HintTitle="Meta描述" 
                    HintInfo="用于搜索引擎，控制在250个字数内，不填写将自动提取。" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none">
            <td align="right">文章图片：</td>
            <td>
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="FileUpload" name="FileUpload" onchange="SingleUpload('txtImgUrl','FileUpload')" /></a>
                <asp:Literal runat="server" ID="litSize"></asp:Literal>
                <span class="uploading">正在上传，请稍候...</span>

                  <a rel="<%=img%>" href="<%=img%>"><img src="<%=img %>" width="100px" /></a>
            </td>
        </tr>
         <tr>
            <td align="right">文章大图：</td>
            <td>
                <asp:TextBox ID="txtBigImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="FileUpload2" name="FileUpload2" onchange="SingleUpload('txtBigImgUrl','FileUpload2')" /></a>
                <asp:Literal runat="server" ID="Literal2"></asp:Literal>
                <span class="uploading">正在上传，请稍候...</span>

                 <a rel="<%=bigimg%>" href="<%=bigimg%>"><img src="<%=bigimg %>" width="100px" /></a>
                 图片尺寸：900px
            </td>
        </tr>
        
        <tr>
            <td align="right" valign="top">图片内容内容：</td>
            <td>
                <textarea id="txtContent" cols="100" rows="8" style="width:100%;height:400px;visibility:hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">文章属性：</td>
            <td>
                <asp:CheckBoxList ID="cblItem" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                
                    <asp:ListItem Value="1">隐藏</asp:ListItem>
                    
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="right">浏览次数：</td>
            <td>
            <asp:TextBox ID="txtClick" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="文章的浏览次数" HintInfo="纯数字，本文章被阅读的次数。">0</asp:TextBox>
            </td>
        </tr>
         
        <tr>
         <td width="25%" align="right">优先级别：</td>
         <td width="75%">
            <asp:TextBox ID="txtSortId" CssClass="input required number" size="10" runat="server" maxlength="9" HintTitle="类别分类优先级别" HintInfo="纯数字，数字越少越向前。" Text="0"></asp:TextBox>
         </td>
       </tr>
       
    </table>

    <div style="margin-top:10px;text-align:center;">
  <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" onclick="btnSave_Click" 
        />
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
