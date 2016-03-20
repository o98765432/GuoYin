<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.Product.Edit"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=strtitle %><%=channelmodel.Title %></title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript" src="../js/multiple_productupload_edit.js"></script>
    <script type="text/javascript" src="../../KindEditor/kindeditor.js"></script>
    <style type="text/css">
        .spanButtonPlaceholder
        {
            width: 80px;
        }
    </style>
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
 
   <%=DtCms.BLL.Chenadd.returnAllTable(2, classid)%>


    var editor8;
    KindEditor.ready(function (K) {
        editor1 = K.create('#txtContent5',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });


    });


    var editor3;
    KindEditor.ready(function (K) {
        editor1 = K.create('#txtContent6',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });


    });
      var editor14;
    KindEditor.ready(function (K) {
        editor14 = K.create('#txtContent4',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });


    });

    
    var editor4;
    KindEditor.ready(function (K) {
        editor1 = K.create('#txtContent7',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });


    });



    var editor5
    KindEditor.ready(function (K) {
        editor1 = K.create('#txtContent9',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });


    });

//    var editor11
//    KindEditor.ready(function (K) {
//        editor11 = K.create('#txtContent11',
//        {
//            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
//            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
//            allowFileManager: true
//        });


//    });


    var editor9;
    KindEditor.ready(function (K) {
        editor1 = K.create('#txtContent10',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });


    });
        $(function () {

            $("#btnSave").click(function () {


                showinfoby();
                showinfobyShow();
            });

            function showinfobyShow() {

                var allinfo = "";

                for (i = 0; i < $(".showtypeidinfo").length; i++) {

                    if ($(".showtypeidinfo").eq(i).attr("checked")) {
                        if (allinfo == "") {
                            allinfo = $(".showtypeidinfo").eq(i).val();
                        } else {
                            allinfo += "," + $(".showtypeidinfo").eq(i).val();
                        }
                    }
                }

                $("#txtFilePath").val(allinfo);

            }

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
            $("#wailians").hide();
        }


        function radWailianClick() {

            $("#wailians").show();
        }
    </script>
</head>
<body style="padding: 10px;">
    <form id="form1" runat="server">
    <div class="navigation">
        <span class="back"><a href="List.aspx?classId=<%=this.classid %>&page=<%=Request.QueryString["page"] %>">
            返回列表</a></span><b>您当前的位置：首页 &gt;
                <%=channelmodel.Title %>管理 &gt;
                <%=strtitle %><%=channelmodel.Title %></b>
    </div>
    <div class="spClear">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="3" align="left">
                编辑信息
            </th>
        </tr>
        <tr>
            <td width="100" align="right">
                标题：
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="input w250 required" MaxLength="250"
                    minlength="1" HintTitle="发布的学生姓名" HintInfo="控制在100个字符内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
            <td width="204" rowspan="8" valign="top" style="display: none;">
                <div class="imgbox">
                </div>
                <div class="imgItems">
                    <ul>
                        <asp:Repeater ID="rptAlbums" runat="server">
                            <ItemTemplate>
                                <li>
                                    <img src="<%#Eval("ImgUrl") %>" onmouseover="ChangePreview('<%#Eval("ImgUrl") %>');" />
                                    <a onclick="dlstItems_Command(this,'<%#Eval("Id") %>','<%#Eval("ImgUrl") %>');">删除</a>
                                    <input name="hideFiles" type="hidden" value="<%#Eval("ImgUrl") %>">
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
                <div class="line5">
                </div>
                <div class="filebtn">
                    <a class="upfiles">
                        <input type="file" name="FileUpload" id="FileUpload" /></a>
                    <img src="../images/loading2.gif" style="display: none;" id="idProcess" />
                </div>
                <br />
                <center>
                    此功能暂不使用</center>
                <input name="album_parent_id" type="hidden" value="<%=this.Id %>" />
            </td>
        </tr>
         <tr>
            <td align="right" valign="top">
                产品简述：
            </td>
            <td>
                <textarea id="txtContent11" cols="100" rows="3" style="width: 50%; height: 150px;
                    " runat="server"></textarea>
            </td>
        </tr>
        <tr  style="display: none">
            <td align="right" valign="top">
                小标题：
            </td>
            <td>
                <asp:TextBox ID="txtsubTitle" runat="server" CssClass="textarea wh380" MaxLength="400"
                    minlength="1" HintTitle="发布的产品简述" HintInfo="控制在200个字符内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
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
                            file_upload_limit: "0",
                            file_queue_error_handler: fileQueueError,
                            file_dialog_complete_handler: fileDialogComplete,
                            upload_progress_handler: uploadProgress,
                            upload_error_handler: uploadError,
                            upload_success_handler: uploadSuccess,
                            upload_complete_handler: uploadComplete,

                            // Button settings
                            button_image_url: "../SWFUpload/images/XPButtonNoText_160x22.png",
                            button_placeholder_id: "spanButtonPlaceholder",
                            button_width: 100,
                            button_height: 22,
                            button_text: '<span class="button">多图片上传</span></span>',
                            button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 12pt;width:80px; } .buttonSmall { font-size: 10pt; }',
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
        <asp:Repeater ID="rptField" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="right">
                        <%#Eval("Title") %>：
                    </td>
                    <td>
                        <asp:HiddenField ID="hideExtensionId" runat="server" />
                        <asp:HiddenField ID="hideFieldId" Value='<%#Eval("Id") %>' runat="server" />
                        <asp:HiddenField ID="hideFieldTitle" Value='<%#Eval("Title") %>' runat="server" />
                        <asp:TextBox ID="txtFieldContent" runat="server" Visible='<%# Eval("FieldType").ToString().Trim() == "Label"?false:true%>'
                            Style='<%# Eval("FieldType").ToString().Trim()=="LongText"?"width:500px": "" %>'
                            CssClass='<%# WriteCss(Convert.ToBoolean(Eval("IsNull")), Eval("FieldType").ToString())%>'
                            MaxLength="250" HintTitle='<%#Eval("Title") %>' HintInfo='<%#Eval("FieldRemark") %>'></asp:TextBox>
                        <%# Eval("FieldType").ToString().Trim() == "Label"?Eval("FieldRemark"):""%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr style="display: none">
            <td align="right">
                上传文件：
            </td>
            <td>
                <asp:TextBox ID="txtFilePath" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files filesbg2">
                    <input type="file" id="FileUpload2" name="FileUpload2" onchange="SingleUpload('txtFilePath','FileUpload2')" /></a>
                <span class="uploading">正在上传，请稍候...</span>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right">
                价格：
            </td>
            <td>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="input required number" size="10"
                    MaxLength="10" HintTitle="产品所涉及的价格" HintInfo="货币格式如“150.5”,单位为元，0代表暂无价格。">0</asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right">
                下拉小图片：
            </td>
            <td>
                <asp:TextBox ID="txtContent8" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files">
                    <input type="file" id="showFileUploadImg" name="showFileUploadImg" onchange="SingleUpload('txtContent8','showFileUploadImg')" /></a>
                <span class="uploading">正在上传，请稍候...</span> <font color="#ff0000">图片大小(150 * 110)</font>
            </td>
        </tr>
        <tr>
            <td align="right">
                列表图：
            </td>
            <td>
                <asp:TextBox ID="txtImgUrl1" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files">
                    <input type="file" id="FileUploadImg" name="FileUploadImg" onchange="SingleUpload('txtImgUrl1','FileUploadImg')" /></a>
                <span class="uploading">正在上传，请稍候...</span> <font color="#ff0000">图片大小(555 * 503)
                </font>
            </td>
        </tr>
        <tr>
            <td align="right">
                内页大图：
            </td>
            <td>
                <asp:TextBox ID="txtImgUrl4" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files">
                    <input type="file" id="txtImgUrlBanner" name="txtImgUrlBanner" onchange="SingleUpload('txtImgUrl4','txtImgUrlBanner')" /></a>
                <span class="uploading">正在上传，请稍候...</span> <font color="#ff0000">图片大小(978 * 214)</font>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right">
                型号：
            </td>
            <td>
                <asp:TextBox ID="txtImgUrl2" runat="server" CssClass="input w250 left"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right" valign="top">
                添加连接：
            </td>
            <td>
                <asp:RadioButton ID="radNeilians" runat="server" onclick="radNeilianClick();" GroupName="linksinfo" />
                <asp:TextBox ID="neilians" runat="server" CssClass="input w380 left" Style="display: none"></asp:TextBox>
                内链
                <asp:RadioButton ID="radWailians" runat="server" onclick="radWailianClick();" GroupName="linksinfo" />
                <asp:TextBox ID="wailians" runat="server" CssClass="input w380 left" Style="display: none"></asp:TextBox>
                外链
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                Meta标题：
            </td>
            <td>
                <asp:TextBox ID="txtContent1" runat="server" MaxLength="250" minlength="3" HintTitle="发布的Meta标题"
                    HintInfo="控制在100个字符内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                Meta关键字：
            </td>
            <td>
                <textarea id="txtContent2" cols="100" rows="8" style="width: 400px; height: 100px;"
                    runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                Meta描述：
            </td>
            <td>
                <textarea id="txtContent3" cols="100" rows="8" style="width: 400px; height: 100px;"
                    runat="server"></textarea>
            </td>
        </tr>
        <%=DtCms.BLL.Chenadd.returnAllTable(2, classid, Id)%>
        <tr>
            <td align="right">
                所属类别：
            </td>
            <td>
                <asp:DropDownList ID="ddlClassId" CssClass="required" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right">
                所属其它栏目：
            </td>
            <td>
                <%=allhtypeid %>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                业务介绍：
            </td>
            <td>
                <textarea id="txtContent4" cols="100" rows="8" style="width: 200; height: 400px;"
                    runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                业务优势：
            </td>
            <td>
                <textarea id="txtContent" cols="100" rows="8" style="width: 100%; height: 400px;
                    visibility: hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                业务流程:
            </td>
            <td>
                <textarea id="txtContent5" cols="100" rows="8" style="width: 100%; height: 400px;
                    visibility: hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                成功案例:
            </td>
            <td>
                <textarea id="txtContent6" cols="100" rows="8" style="width: 100%; height: 400px;
                    visibility: hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr style="display: none;">
            <td align="right" valign="top">
                如何购买:
            </td>
            <td>
                <textarea id="txtContent7" cols="100" rows="8" style="width: 100%; height: 400px;
                    visibility: hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right" valign="top">
                产品选型
            </td>
            <td>
                <textarea id="txtContent9" cols="100" rows="8" style="width: 100%; height: 400px;
                    visibility: hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right" valign="top">
                技术优势
            </td>
            <td>
                <textarea id="txtContent10" cols="100" rows="8" style="width: 100%; height: 400px;
                    visibility: hidden;" runat="server"></textarea>
            </td>
        </tr>
       
        <tr>
            <td align="right">
                产品属性：
            </td>
            <td>
                <asp:CheckBox ID="IsLock" runat="server" Checked="True" Text="可见性" />
                <asp:CheckBox ID="IsTop" runat="server" Text="置顶" />
                <asp:CheckBox ID="IsHot" runat="server" Text="首页" />
            </td>
        </tr>
        <tr>
            <td align="right">
                浏览次数：
            </td>
            <td>
                <asp:TextBox ID="txtClick" runat="server" CssClass="input required number" size="10"
                    MaxLength="10" HintTitle="产品的浏览次数" HintInfo="纯数字，本产品被浏览的次数。">0</asp:TextBox>
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
                更多图片： <font color="#ff0000">(446*355) </font>
            </td>
            <td>
                <div id="swfu_container" style="margin: 0px 10px;">
                    <div>
                        <span id="spanButtonPlaceholder"></span>
                    </div>
                    <div class="imgItems_" style="position: absolute; width: 600px;">
                        <ul style="width: 590px;">
                            <%=itemImgs%>
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
