<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.ProductKey.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=strtitle %>详细参数</title>
   <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript" src="../js/multiple_productupload_edit.js"></script>

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
         

        $(function () {

            $("#btnSave").click(function () {


                showinfoby();

            });

            function showinfoby() {

                var allinfo = "";

                for (i = 0; i < $(".myclassinfo").length; i++) {


                    if (allinfo == "") {
                        allinfo = $(".myclassinfo").eq(i).val();
                    } else {
                        allinfo += "," + $(".myclassinfo").eq(i).val();
                    }

                }

                $("#showaddinfo").val(allinfo);

            }

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

       <script type="text/javascript">
           function radNeilianClick() {

               $("#wailians").css("display", "none");
           }


           function radWailianClick() {

               $("#wailians").css("display", "block");
           }
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server"  method="post">
    <div class="navigation">
      <span class="back"><a href="List.aspx?classId=<%=Request.QueryString["classId"] %>&returnclassid=<%=Request.QueryString["returnclassid"] %>&shownowid=<%=Request.QueryString["shownowid"] %>">返回列表</a></span><b>您当前的位置：首页 &gt;详细参数管理 &gt; <%=strtitle %>详细参数</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="3" align="left"><%=strtitle %>详细参数</th>
        </tr>
     
       <tr>
        <td>
       

        </td>
       </tr>
        
        <tr>
            <td align="right" valign="top">标题：</td>
            <td>
                <asp:TextBox ID="txtBigTitle" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right">图片(226 * 126)：</td>
            <td>
                <asp:TextBox ID="txtblinfo1" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="File1" name="FileUploadImg" onchange="SingleUpload('txtblinfo1','FileUploadImg')" /></a>
                <span class="uploading">正在上传，请稍候...</span>
            </td>
        </tr>
        
         <tr style="display:none">
            <td align="right">上传文件：</td>
            <td>
                <asp:TextBox ID="txtFilePath" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files filesbg2"><input type="file" id="FileUpload2" name="FileUpload2" onchange="SingleUpload('txtFilePath','FileUpload2')" /></a>
                <span class="uploading">正在上传，请稍候...</span>
            </td>
        </tr>
         <tr style="display:none">
            <td align="right" valign="top">内容：</td>
            <td>
            
                <textarea id="txtContent" cols="100" rows="8" style="width:100%;height:400px;visibility:hidden;" runat="server"></textarea>
            </td>
        </tr>
          
        
        <tr>
            <td align="right">优先级别：</td>
            <td>
            <asp:TextBox ID="txtSortId" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="产品的排序数字" HintInfo="纯数字，越小越向前。">0</asp:TextBox>
            </td>
        </tr>
        
        <tr style="display:none">
            <td colspan = "2">
            <div id="swfu_container" style="margin: 0px 10px;">
		         <div>
				    <span id="spanButtonPlaceholder"></span>
		         </div>
                 <div class="imgItems_" style="position:absolute; width:600px;">
                    <ul style=" width:590px;">
                  
                   
                    <asp:HiddenField ID="showaddinfo" runat="server" />
                    </ul>
                    <div class="clear">
                    </div>
                </div>
	       </div>
            </td>
        </tr>
    </table>
    <div style="margin-top:100px;text-align:center;">
      <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" onclick="btnSave_Click" />
  &nbsp;
  <input name="重置" type="reset" class="submit" value="重置" />
</div>
    </form>
</body>
</html>

