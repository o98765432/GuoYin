<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DtCms.Web.Admin.Video.edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=strtitle %><%=channelmodel.Title %></title>
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

     <%=DtCms.BLL.Chenadd.returnAllTable(3, classid)%>

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
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="List.aspx?classId=<%=this.classid %>&page=<%=Request.QueryString["page"] %>">返回列表</a></span><b>您当前的位置：首页 &gt; <%=channelmodel.Title %>管理 &gt; 编辑<%=channelmodel.Title %></b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left"><%=strtitle %><%=channelmodel.Title %></th>
        </tr>
        <tr>
            <td width="15%" align="right">标题：</td>
            <td width="85%">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="1" HintTitle="发布的文件标题" HintInfo="控制在100个字数内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
         <tr  <%if (classid!=47) {%>  style="display:none"  <% } %>>
            <td width="15%" align="right">标签：</td>
            <td width="85%">
           <asp:TextBox ID="txtSubTitle" runat="server" CssClass="input w380" 
            maxlength="250" minlength="3" HintTitle="标签" HintInfo="标签"></asp:TextBox>
            </td>
        </tr>
        
            <%=DtCms.BLL.Chenadd.returnAllTable(3, classid, Id)%>

        <tr>
            <td align="right">所属栏目：</td>
            <td><asp:DropDownList id="ddlClassId" CssClass=" required" runat="server"></asp:DropDownList></td>
        </tr>
        <tr  <%if (classid==2) {%>
                     style="display:none"
                <% } %>>
            <td align="right">预览图：</td>
            <td>
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="FileUpload1" name="FileUpload1" onchange="SingleUpload('txtImgUrl','FileUpload1')" /></a>
                <span class="uploading">正在上传，请稍候...</span>
                 <a rel="<%=img%>" href="<%=img%>"><img src="<%=img %>" width="100px"/></a>
                <br />
                  <font color="#ff0000">图片大小:(<%=channelmodel.width%>*<%=channelmodel.height%> )</font>
            </td>
        </tr>
        <tr  >
            <td align="right">上传文件：</td>
            <td>
                <asp:TextBox ID="txtFilePath" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files filesbg2"><input type="file" id="FileUpload2" name="FileUpload2" onchange="SingleUpload('txtFilePath','FileUpload2')" /></a>
                <span class="uploading">正在上传，请稍候...</span>
                <span><a href="<%=filepath %>">查看</a></span>
            </td>
        </tr>
       
        <tr style="display:none">
            <td align="right" valign="top">详细介绍：</td>
            <td>
              <textarea id="txtContent" cols="100" rows="8" style="width:100%;height:400px;visibility:hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">信息属性：</td>
            <td> 
                <asp:CheckBoxList ID="cblItem" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1" Selected="True">可见性</asp:ListItem>
                </asp:CheckBoxList>
                <asp:CheckBoxList ID="cbTop" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="0" Selected="True">置顶</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr  style="display:none">
            <td align="right">浏览次数：</td>
            <td>
            <asp:TextBox ID="txtClick" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="信息的浏览次数" HintInfo="纯数字，本下载信息被阅读的次数。">0</asp:TextBox>
            </td>
        </tr>
        <tr style="display:none">
            <td align="right">下载次数：</td>
            <td>
            <asp:TextBox ID="txtDownNum" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="文件被下载的次数" HintInfo="纯数字，本文件被下载的次数。">0</asp:TextBox>
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
