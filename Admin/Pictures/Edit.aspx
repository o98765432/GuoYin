<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.Pictures.Edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=strtitle %><%=channelmodel.Title %></title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript" src="../js/multipleupload_edit.js"></script>
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

    <script type="text/javascript">
        function radNeilianClick() {
            $("#wailians").hide();
        }


        function radWailianClick() {

            $("#wailians").show();
        }
    </script>

</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="List.aspx?classId=<%=this.classid %>">返回列表</a></span><b>您当前的位置：首页 &gt; <%=channelmodel.Title %>管理 &gt; 编辑<%=channelmodel.Title %></b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="3" align="left"><%=strtitle %><%=channelmodel.Title %>信息</th>
        </tr>
        <tr>
            <td width="100" align="right"><%=channelmodel.Title %>标题：</td>
            <td>
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input w250 required" 
            maxlength="250" minlength="3" HintTitle="发布的图文标题名称" HintInfo="控制在100个字符内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
            <td width="204" rowspan="8" valign="top" style="display:none">
              <div class="imgbox"></div>
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
                <div class="clear"></div>
              </div>
              <div class="line5"></div>
              <div class="filebtn">
                <a class="upfiles"><input type="file" name="FileUpload" id="FileUpload" /></a> <img src="../images/loading2.gif" style="display:none;" id="idProcess" />
              </div>
              <input name="album_parent_id" type="hidden" value="<%=this.Id %>" />
            </td>
        </tr>
        
        <asp:Repeater ID="rptField" runat="server">
        <ItemTemplate>
        <tr style="display:none">
            <td align="right"><%#Eval("Title") %>：</td>
            <td>
                <asp:HiddenField ID="hideExtensionId" runat="server" />
                <asp:HiddenField ID="hideFieldId" Value='<%#Eval("Id") %>' runat="server" />
                <asp:HiddenField ID="hideFieldTitle" Value='<%#Eval("Title") %>' runat="server" /><%--style="<%# Eval("FieldType").ToString().Trim()=="LongText"?"width:300px":"" %>" --%>
                <asp:TextBox ID="txtFieldContent" runat="server" Visible='<%# Eval("FieldType").ToString().Trim() == "Label"?false:true%>' style='<%# Eval("FieldType").ToString().Trim()=="LongText"?"width:500px":"" %>' CssClass='<%# WriteCss(Convert.ToBoolean(Eval("IsNull")), Eval("FieldType").ToString())%>' maxlength="250" HintTitle='<%#Eval("Title") %>' HintInfo='<%#Eval("FieldRemark") %>'></asp:TextBox>
                <%# Eval("FieldType").ToString().Trim() == "Label"?Eval("FieldRemark"):""%>
            </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>

        <tr style="display:none">
            <td align="right">价格：</td>
            <td>
            <asp:TextBox ID="txtPrice" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="图文所涉及的价格" HintInfo="货币格式如“150.5”,单位为元，0代表暂无价格。">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">所属类别：</td>
            <td><asp:DropDownList id="ddlClassId" CssClass="required" runat="server"></asp:DropDownList></td>
        </tr>
         <tr style="display:none">
            <td align="right">列表图片：</td>
            <td>
                <asp:TextBox ID="txtListImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="FileUpload1" name="FileUpload1" onchange="SingleUpload('txtListImgUrl','FileUpload1')" /></a>
                <span class="uploading">正在上传，请稍候...</span>&nbsp;&nbsp;
                <a href="<%=this.txtListImgUrl.Text %>" target="_blank">查看图片</a>
            </td>
        </tr>
        <tr >
            <td align="right"><%=channelmodel.Title %>图片：</td>
            <td>
                <asp:TextBox ID="txtBigImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="File1" name="FileUpload2" onchange="SingleUpload('txtBigImgUrl','FileUpload2')" /></a>
                <span class="uploading">正在上传，请稍候...</span>&nbsp;&nbsp;
                 <font color="#ff0000">图片大小:(192*42)</font>
            </td>
        </tr>
        <tr style="display:none">
            <td align="right" valign="top">图文详细：</td>
            <td>
                <textarea id="txtContent" cols="100" rows="8" style="width:100%;height:400px;visibility:hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr style="display:none">
            <td align="right">图文属性：</td>
            <td>
                <asp:CheckBox ID="IsLock" runat="server" Checked="True" Text="可见性" />
               <asp:CheckBox ID="IsTop" runat="server" Text="置顶" /> 
               <asp:CheckBox ID="IsHot" runat="server" Text="首页" />
            </td>
        </tr>
        <tr style="display:none">
            <td align="right">浏览次数：</td>
            <td>
            <asp:TextBox ID="txtClick" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="图文的浏览次数" HintInfo="纯数字，本图文被浏览的次数。">0</asp:TextBox>
            </td>
        </tr>
        <tr >
            <td align="right">优先级别：</td>
            <td>
            <asp:TextBox ID="txtSortId" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="图文的排序数字" HintInfo="纯数字，越小越向前。">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">添加连接：</td>
            <td>
               <div  style="display:none">
                <asp:RadioButton ID="radNeilians" runat="server" onclick="radNeilianClick();"  GroupName="linksinfo"  />
                <asp:TextBox ID="neilians" runat="server" CssClass="input w380 left" style="display:none"></asp:TextBox>
                内链
                <asp:RadioButton ID="radWailians" runat="server" onclick="radWailianClick();"  style="display:none"  GroupName="linksinfo"  />
                </div>
                <asp:TextBox ID="wailians" runat="server" CssClass="input w380 left" ></asp:TextBox>
                 
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
