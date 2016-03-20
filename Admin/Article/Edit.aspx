<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.Article.Edit"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=strtitle %><%=channelmodel.Title %></title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script language="javascript" src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../css/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../KindEditor/kindeditor.js"></script>
    <link href="../css/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Js/multipleupload_add.js" type="text/javascript"></script>
    <script src="../Js/multipleupload_edit.js" type="text/javascript"></script>
    <script type="text/javascript">
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('#txtContent',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true,
            filterMode: false
        });

    });
       
      
        <%=DtCms.BLL.Chenadd.returnAllTable(1, classid)%>

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
</head>
<body style="padding: 10px;">
    <form id="form1" runat="server">
    <div class="navigation">
        <span class="back"><a href="List.aspx?classId=<%=this.classid %>&page=<%=Request.QueryString["page"] %>">
            返回列表</a></span><b>您当前的位置：首页 &gt; <%=channelmodel.Title %>管理 &gt; 编辑<%=channelmodel.Title %></b>
    </div>
    <div style="padding-bottom: 10px;">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">
                发布资讯
            </th>
        </tr>
        <tr>
            <td width="15%" align="right">
                 文章标题：
            </td>
            <td width="85%">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="input w380 required" MaxLength="250"
                    minlength="1" HintTitle="发布的文章标题" HintInfo="控制在100个字数内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none" >
            <td width="15%" align="right">
                国籍：
            </td>
            <td width="85%">   
                <asp:TextBox ID="txtSubTitle" runat="server" CssClass="input w380" MaxLength="250"
                    minlength="1" HintTitle="国籍" HintInfo="" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr <%if (classid == 8 || classid == 55 || classid == 31 || classid == 99)
              { %>style="display:none"<%} %> >
            <td align="right">
                来源：
            </td>
            <td>
                <asp:TextBox ID="txtIndexImgUrl" runat="server" CssClass="input w380 left" Text="国银租赁"></asp:TextBox>
            </td>
        </tr>
       <tr style="display:none">
            <td align="right">
                链接：
            </td>
            <td>
                <asp:RadioButton ID="radNeilians" Visible="false" runat="server" onclick="radNeilianClick();"
                    GroupName="linksinfo" />
                <asp:TextBox ID="neilians" runat="server" Visible="false" CssClass="input w380 left"
                    Style="display: none"></asp:TextBox>
                <asp:RadioButton ID="radWailians" Visible="false" runat="server" onclick="radWailianClick();"
                    GroupName="linksinfo" />
                <asp:TextBox ID="wailians" runat="server" CssClass="input w380 left"></asp:TextBox>
             
                 <font color="#ff0000">例：http://www.baidu.com</font>
            </td>
        </tr>
        <tr <%if (classid == 8 || classid == 55 || classid == 31 || classid == 99)
              { %>style="display:none"<%} %>>
            <td align="right"  >
              导读: 
            </td>
            <td style="display:none">
            <textarea id="txtzy" cols="100" rows="8" style="width: 60%; height: 70px;" runat="server"></textarea>
            </td>
              
            <td >
                <asp:TextBox ID="txtDaodu" runat="server" CssClass="textarea wh380" HintTitle="文章导读属性"
                    MaxLength="100" HintInfo="控制在100个字数内，纯文本，不填写将自动提取。" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none">
            <td align="right">
                图片：
            </td>
            <td>
                <asp:TextBox ID="txtXtImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files">
                    <input type="file" id="File1" name="XtFileUpload" onchange="SingleUpload('txtXtImgUrl','XtFileUpload')" /></a>
                <asp:Literal runat="server" ID="Literal3"></asp:Literal>
                <span class="uploading">正在上传，请稍候...</span> <a rel="<%=xtimg%>" href="<%=xtimg%>">
                    <img src="<%=xtimg %>" width="100px" /></a>
                <br />
                <font color="#ff0000">图片大小:<%if (classid == 29 || classid == 34 || classid == 35 || classid == 36 || classid == 37)
                                             { %>
                                             (234*175)
                                             <%}
                                             else
                                             { %>(110*83)<%} %></font>
            </td>
        </tr>
        <br />
        <tr <%if (classid != 8 && classid != 55)
              { %> style="display:none" <%} %>>
            <td align="right">
                阶段：
            </td>
            <td>
                <asp:TextBox ID="txtAuthor" runat="server" CssClass="input w250" MaxLength="50"
                    HintInfo="控制在50个字数内"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                日期：
            </td>
            <td>
                <asp:TextBox ID="txtAddTime" runat="server" onfocus="WdatePicker()" CssClass="input required"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Meta标题：
            </td>
            <td>
                <asp:TextBox ID="txtForm" runat="server" CssClass="input w250" MaxLength="100" HintTitle="Meta标题"
                    HintInfo="控制在100个字数内。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Meta关键字：
            </td>
            <td>
                <asp:TextBox ID="txtKeyword" runat="server" CssClass="input w250" MaxLength="100"
                    HintTitle="Meta关键字" HintInfo="用于搜索引擎，如有多个关健字请用英文的,号分隔，不填写将自动提取标题。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Meta描述：
            </td>
            <td>
                <asp:TextBox ID="txtZhaiyao" runat="server" CssClass="textarea wh380" MaxLength="250"
                    HintTitle="Meta描述" HintInfo="用于搜索引擎，控制在250个字数内，不填写将自动提取。" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <%=DtCms.BLL.Chenadd.returnAllTable(1, classid, Id)%>
        <tr>
            <td align="right">
                所属栏目：
            </td>
            <td>
                <asp:DropDownList ID="ddlClassId" CssClass=" required" runat="server" OnSelectedIndexChanged="ddlClassId_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="display:none" >
            <td align="right">
                图片：
            </td>
            <td>
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files">
                    <input type="file" id="FileUpload" name="FileUpload" onchange="SingleUpload('txtImgUrl','FileUpload')" /></a>
                <asp:Literal runat="server" ID="litSize"></asp:Literal>
                <span class="uploading">正在上传，请稍候...</span> <a rel="<%=img%>" href="<%=img%>">
                    <img src="<%=img %>" width="100px" /></a>
                <br />
                <font color="#ff0000">图片大小:(750*640)</font>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right">
                名字图片：
            </td>
            <td>
                <asp:TextBox ID="txtBigImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files">
                    <input type="file" id="FileUpload2" name="FileUpload2" onchange="SingleUpload('txtBigImgUrl','FileUpload2')" /></a>
                <asp:Literal runat="server" ID="Literal2"></asp:Literal>
                <span class="uploading">正在上传，请稍候...</span><font color="#ff0000">图片尺寸:( 高50，宽多定义)</font>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right">
                上传附件：
            </td>
            <td>
                <div class="uploadfile">
                    <asp:TextBox ID="txtVideoDownload" runat="server" CssClass="input w380 left"></asp:TextBox>
                    &nbsp;&nbsp;
                    <input id="file_upload" name="file_upload" type="file" runat="server" />
                    <div id="lbFileName">
                    </div>
                    <br />
                    <div id="fileQueue">
                    </div>
                </div>
            </td>
        </tr>
        <tr  style="display: none" >
            <td width="15%" align="right">
                上传视频：
            </td>
            <td width="85%">
                <asp:TextBox ID="txtFilepath" runat="server" CssClass="input w380 left"></asp:TextBox>
                (选择<span style="color:Red">.flv</span>格式本地视频文件上传)<a href="javascript:void(0);" class="files">
                    <input type="file" id="FileDownload" name="FileDownload" onchange="SingleUpload('txtFilepath','FileDownload')" />
                </a>
                <asp:Literal runat="server" ID="Literal1"></asp:Literal>
                <span class="uploading">正在上传，请稍候...</span>
            </td>
        </tr>
    
        <tr>
            <td align="right" valign="top">
                  文章内容：
            </td>
            <td>
                <textarea id="txtContent" cols="100" rows="8" style="width: 100%; height: 400px;
                    visibility: hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                文章属性：
            </td>
            <td>
                <asp:CheckBox ID="IsLock" runat="server" Checked="True" Text="可见性" />
                <asp:CheckBox ID="IsTop" runat="server" Visible="false" Text="置顶" />
                <asp:CheckBox ID="IsRed" runat="server" Visible="false" Text="推荐" />
                <asp:CheckBox ID="IsHot" runat="server" Text="首页" />
            </td>
        </tr>
        <tr>
            <td align="right">
                浏览次数：
            </td>
            <td>
                <asp:TextBox ID="txtClick" runat="server" CssClass="input required number" size="10"
                    MaxLength="10" HintTitle="文章的浏览次数" HintInfo="纯数字，本文章被阅读的次数。">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%" align="right">
                优先级别：
            </td>
            <td width="75%">
                <asp:TextBox ID="txtSortId" CssClass="input required number" Text="1" size="10" runat="server"
                    MaxLength="9" HintTitle="类别分类优先级别" HintInfo="纯数字，数字越少越向前。"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none" >
            <td align="right">
                上传多图(769 * 432)：
            </td>
            <td>
                <div id="swfu_container" style="margin: 0px 10px;">
                    <div>
                        <span id="spanButtonPlaceholder"></span>
                    </div>
                    <div class="imgItems_" style="width: 600px;">
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
        <tr style="display: none">
            <td align="right" valign="top">
                静态化模版：
            </td>
            <td>
                <asp:TextBox ID="txtDownload" runat="server" CssClass="input w380" HintTitle="静态化模版"
                    HintInfo="非专业人士指导请勿改变此处"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="margin-top: 10px; text-align: center;">
        <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" OnClick="btnSave_Click" />
        &nbsp;
        <input name="重置" type="reset" class="submit" value="重置" />
    </div>
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
    </form>
</body>
</html>
