<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.Banner.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=strtitle %>Banner</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript" src="../js/multiple_productupload_edit.js"></script>
    <script type="text/javascript" src="../SWFUpload/swfupload.js"></script>
    <script type="text/javascript" src="../SWFUpload/js/handlers.js"></script>
    <script type="text/javascript" src="../../KindEditor/kindeditor.js"></script>
    <script type="text/javascript">

        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('#txtContent2',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });
        });
        KindEditor.ready(function (K) {
            editor = K.create('#txtContent3',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });
        });
        KindEditor.ready(function (K) {
            editor = K.create('#txtContent4',
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
<body style="padding: 10px;">
    <form id="form1" runat="server" method="post">
    <div class="navigation">
        <span class="back"><a href="List.aspx?classId=<%=this.classid %>">返回列表</a></span><b>您当前的位置：首页
            &gt;<%=channelmodel.Title %>管理 &gt;
            <%=strtitle %>Banner</b>
    </div>
    <div class="spClear">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="3" align="left">
                <%=strtitle %><%=channelmodel.Title %>信息
            </th>
        </tr>
        <tr>
            <td>
                <script type="text/javascript" src="../SWFUpload/swfupload.js"></script>
                <script type="text/javascript" src="../SWFUpload/js/handlers.js"></script>
                <script type="text/javascript">
                    var swfu;
                    window.onload = function () {
                        swfu = new SWFUpload({
                            // Backend Settings
                            upload_url: "../SWFUpload/upload.aspx",
                            post_params: {
                                "ASPSESSID": "<%=Session.SessionID %>"
                            },

                            // File Upload Settings
                            file_size_limit: "2 MB",
                            file_types: "*.jpg",
                            file_types_description: "JPG Images",
                            file_upload_limit: "0",    // Zero means unlimited

                            // Event Handler Settings - these functions as defined in Handlers.js
                            //  The handlers are not part of SWFUpload but are part of my website and control how
                            //  my website reacts to the SWFUpload events.
                            file_queue_error_handler: fileQueueError,
                            file_dialog_complete_handler: fileDialogComplete,
                            upload_progress_handler: uploadProgress,
                            upload_error_handler: uploadError,
                            upload_success_handler: uploadSuccess,
                            upload_complete_handler: uploadComplete,

                            // Button settings
                            button_image_url: "../SWFUpload/images/XPButtonNoText_160x22.png",
                            button_placeholder_id: "spanButtonPlaceholder",
                            button_width: 160,
                            button_height: 22,
                            button_text: '<span class="button">上传图片</span></span>',
                            button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
                            button_text_top_padding: 1,
                            button_text_left_padding: 5,

                            // Flash Settings
                            flash_url: "../SWFUpload/swfupload.swf", // Relative to this file

                            custom_settings: {
                                upload_target: "divFileProgressContainer"
                            },

                            // Debug Settings
                            debug: false
                        });
                    }
                </script>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                标题：
            </td>
            <td>
                <asp:TextBox ID="txtBigTitle" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right">
                小图片：
            </td>
            <td>
                <asp:TextBox ID="txtImgUrl2" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files">
                    <input type="file" id="FileUploadImg2" name="FileUploadImg2" onchange="SingleUpload('txtImgUrl2','FileUploadImg2')" /></a>
                <span class="uploading">正在上传，请稍候...</span> <font color="#ff0000">图片大小:(125*100)
                </font>
            </td>
        </tr>
        <tr>
            <td align="right">
                <font>图片:</font>：
            </td>
            <td>
                <asp:TextBox ID="txtBigImgurl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files">
                    <input type="file" id="File1" name="FileUploadImg" onchange="SingleUpload('txtBigImgurl','FileUploadImg')" /></a>
                <span class="uploading">正在上传，请稍候...</span> <font color="#ff0000">图片大小:<%if (classid == 46)
                                                                                        { %>(1920*622)<%}
                                                                                        else if (classid == 12)
                                                                                        {%>(598*401)<%}
                                                                                        else if (classid == 27)
                                                                                        {%>(1920*878)<%}
                                                                                        else if (classid == 51||classid==93)
                                                                                        {%>(229*142)<%}
                                                                                        else { %>(1862*390)<%} %></font>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right">
                上传文件：
            </td>
            <td>
                <asp:TextBox ID="txtSmallImagurl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files">
                    <input type="file" id="FileUploadImg" name="FileUploadImg" onchange="SingleUpload('txtSmallImagurl','FileUploadImg')" /></a>
                <span class="uploading">正在上传，请稍候...</span>
            </td>
        </tr>
        <tr>
            <td align="right" class="style1">
                类别栏目：
            </td>
            <td>
                <asp:DropDownList ID="ddlClassId" CssClass=" required" runat="server" OnSelectedIndexChanged="ddlClassId_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="display: none;">
            <td align="right" valign="top">
                展厅号：
            </td>
            <td>
                <asp:TextBox ID="txtContent" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none;">
            <td align="right" valign="top">
                展位号：
            </td>
            <td>
                <asp:TextBox ID="txtContent1" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none;">
            <td align="right" valign="top">
                展会时间段：
            </td>
            <td>
                <asp:TextBox ID="txtSmallTitle" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
        <tr <%if(classid != 51&&classid!=93&&classid!=108){%> style="display: none"<%} %>>
            <td align="right" valign="top">
                描述：
            </td>
            <td>
                <textarea id="txtContent2" cols="100" rows="8" style="width: 100%; height: 400px;
                    visibility: hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr <%if (classid != 27&&classid!=107){ %>style="display: none"<%}%>>
            <td align="right" valign="top">
                添加连接：
            </td>
            <td>
                <asp:RadioButton ID="radNeilians" runat="server" GroupName="linksinfo" onclick="radNeilianClick();" />
                <asp:TextBox ID="neilians" runat="server" CssClass="input w380 left" Style="display: none"></asp:TextBox>
                内链
                <asp:RadioButton ID="radWailians" runat="server" GroupName="linksinfo" onclick="radWailianClick();" />
                <asp:TextBox ID="wailians" runat="server" CssClass="input w380 left" Style="display: none"></asp:TextBox>
                外链
            </td>
        </tr>
        <tr>
            <td align="right">
                产品属性：
            </td>
            <td>
                <asp:CheckBox ID="IsLock" runat="server" Checked="True" Text="可见性" />
                <asp:CheckBox ID="IsTop" runat="server" Text="置顶" />
            </td>
        </tr>
        <tr>
            <td align="right">
                优先级别：
            </td>
            <td>
                <asp:TextBox ID="txtSortId" runat="server" CssClass="input required number" size="10"
                    MaxLength="10" HintTitle="产品的排序数字" HintInfo="纯数字，越小越向前。">0</asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right">
                静态模块：
            </td>
            <td>
                <asp:TextBox ID="txtHtmlPath" runat="server" CssClass="input w380 left"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td colspan="2">
                <div id="swfu_container" style="margin: 0px 10px;">
                    <div>
                        <span id="spanButtonPlaceholder"></span>
                    </div>
                    <div class="imgItems_" style="position: absolute; width: 600px;">
                        <ul style="width: 590px;">
                            <asp:HiddenField ID="showaddinfo" runat="server" />
                        </ul>
                        <div class="clear">
                        </div>
                    </div>
                </div>
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
