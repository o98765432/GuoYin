<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.Job.Edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=strtitle %><%=channelmodel.Title %></title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script> 
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript" src="../../KindEditor/kindeditor.js"></script>

    <script language="javascript" src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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

        KindEditor.ready(function (K) {
            editor = K.create('#txtContent1',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });
        });
        KindEditor.ready(function (K) {
            editor = K.create('#txtContent2',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });
    }); 

     <%=DtCms.BLL.Chenadd.returnAllTable(5, classid)%>

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
            $("#upordown").toggle(function() {
                $(this).text("关闭高级选项");
                $(this).removeClass();
                $(this).addClass("up-01");
                $(".upordown").show();
            }, function() {
                $(this).text("显示高级选项");
                $(this).removeClass();
                $(this).addClass("up-02");
                $(".upordown").hide();
            });
        });
    </script>
    <style type="text/css">
        .style1
        {
            width: 15%;
        }
    </style>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="List.aspx?classId=<%=this.classid %>&page=<%=Request.QueryString["page"] %>">返回列表</a></span><b>您当前的位置：首页 &gt; <%=strtitle %>管理 &gt; 编辑<%=strtitle %></b>
    </div>
    <div style="padding-bottom:10px;"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">发布<%=strtitle %></th>
        </tr>
       <tr>
            <td align="right" class="style1">职位名称：</td>
            <td width="85%">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="1" HintTitle="发布的职位名称" HintInfo="控制在100个字数内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right" class="style1">招聘人数：</td>
            <td>
            <asp:TextBox ID="txtPeople" runat="server" CssClass="input w250" 
            maxlength="50" HintTitle="招聘人数" HintInfo="控制在50个字数内。"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right" class="style1">工作地点：</td>
            <td>
            <asp:TextBox ID="txtAddr" runat="server" CssClass="input w250 " 
            maxlength="50" HintTitle="工作地点" HintInfo="控制在50个字数内。"></asp:TextBox>
            </td>
        </tr>




        <tr >
            <td align="right" class="style1">薪资报酬：</td>
            <td>
            <asp:TextBox ID="txtAge" runat="server" CssClass="input w250" 
            maxlength="50" HintTitle="薪资报酬" HintInfo="控制在50个字数内。"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right" class="style1">学历：</td>
            <td>
            <asp:TextBox ID="txtEducation" runat="server" CssClass="input w250" 
            maxlength="50" HintTitle="学历" HintInfo="控制在50个字数内。"></asp:TextBox>
            </td>
        </tr>

        <tr >
            <td align="right" class="style1">专业要求：</td>
            <td>
            <asp:TextBox ID="txtYear" runat="server" CssClass="input w250" 
            maxlength="50" HintTitle="工作年限" HintInfo="控制在50个字数内。"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right">链接信息：</td>
            <td>
            <asp:TextBox ID="txtCompany" runat="server" CssClass="input w250 " 
            maxlength="100" HintTitle="链接信息" HintInfo="控制在50个字数内。"></asp:TextBox>
            <font color="#ff0000">例：http://www.51job.com</font>
            </td>
        </tr>
        <tr  >
            <td align="right">Meta关键字：</td>
            <td>
            <asp:TextBox ID="txtKeyword" runat="server" CssClass="input w250" 
            maxlength="100" HintTitle="Meta关键字" HintInfo="用于搜索引擎，如有多个关健字请用英文的,号分隔，不填写将自动提取标题。"></asp:TextBox>
            </td>
        </tr>
        <tr  >
            <td align="right">Meta描述：</td>
            <td>
            <asp:TextBox ID="txtDescription" runat="server" CssClass="textarea wh380"  
            maxlength="250" HintTitle="Meta描述" 
                    HintInfo="用于搜索引擎，控制在250个字数内，不填写将自动提取。" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

         <tr>
            <td align="right" class="style1">工作经验：</td>
            <td>
            <asp:TextBox ID="txtSex" runat="server" CssClass="input w250" 
            maxlength="50" HintTitle="工作经验" HintInfo="控制在50个字数内。"></asp:TextBox>
            </td>
        </tr>

          <tr  style="display:none">
            <td align="right" class="style1">联系人：</td>
            <td>
            <asp:TextBox ID="txtcontact" runat="server" CssClass="input w250" 
            maxlength="50" HintTitle="联系人" HintInfo="控制在50个字数内。"></asp:TextBox>
            </td>
        </tr>

          <tr  style="display:none" >
            <td align="right" class="style1">联系邮箱：</td>
            <td>
            <asp:TextBox ID="txtshowemail" runat="server" CssClass="input w250" 
            maxlength="50" HintTitle="联系邮箱" HintInfo="控制在50个字数内。"></asp:TextBox>
            </td>
        </tr>

          <tr  style="display:none">
            <td align="right" class="style1">联系电话：</td>
            <td>
            <asp:TextBox ID="txttel" runat="server" CssClass="input w250" 
            maxlength="50" HintTitle="联系电话" HintInfo="控制在50个字数内。"></asp:TextBox>
            </td>
        </tr> 
        
            <%=DtCms.BLL.Chenadd.returnAllTable(5, classid, Id)%>

        <tr>
            <td align="right" class="style1">所属栏目：</td>
            <td><asp:DropDownList id="ddlClassId" CssClass=" required" runat="server"  ></asp:DropDownList></td>
        </tr>
        <tr >
            <td align="right" class="style1">发布日期：</td>
            <td>
                <asp:TextBox ID="txtBeginTime" runat="server" onfocus="WdatePicker()"  CssClass=""></asp:TextBox>
            </td>
        </tr>
        <tr  style="display:none">
            <td align="right" class="style1">有效日期：</td>
            <td>
                <asp:TextBox ID="txtEndTime" runat="server" onfocus="WdatePicker()" CssClass=""></asp:TextBox>
            </td>
        </tr>
        <tr  style="display:none">
            <td align="right" valign="top" class="style1">工作性质：</td>
            <td>
                <textarea id="txtContent" cols="100" rows="8" style="width:100%;height:400px;visibility:hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top" class="style1">岗位职责：</td>
            <td>
                <textarea id="txtContent1" cols="100" rows="8" style="width:100%;height:400px;visibility:hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr >
            <td align="right" valign="top" class="style1">岗位要求：</td>
            <td>
                <textarea id="txtContent2" cols="100" rows="8" style="width:100%;height:400px;visibility:hidden;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right" class="style1">属性：</td>
            <td>
                  <asp:CheckBox ID="IsLock" runat="server" Checked="True" Text="可见性" />
            </td>
        </tr>
        <tr style="display:none">
            <td align="right" class="style1">浏览次数：</td>
            <td>
            <asp:TextBox ID="txtClick" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="招聘的浏览次数" HintInfo="纯数字，本招聘被阅读的次数。" Enabled="false">0</asp:TextBox>
            </td>
        </tr>

        <tr>
         <td align="right" class="style1">优先级别：</td>
         <td width="75%">
            <asp:TextBox ID="txtSortId" CssClass="input required number" size="10" Text="0" runat="server" maxlength="9" HintTitle="类别分类优先级别" HintInfo="纯数字，数字越少越向前。"></asp:TextBox>
         </td>
       </tr>
       <tr style="display:none">
            <td align="right" valign="top" class="style1">静态化模版：</td>
            <td>
              <asp:TextBox ID="txtFilepath" runat="server" CssClass="input w380" 
             HintTitle="静态化模版" HintInfo="非专业人士指导请勿改变此处"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="margin-top:10px;text-align:center;">
  <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" onclick="btnSave_Click" 
        />
  &nbsp;
  <input name="重置" type="reset" class="submit" value="重置" />
</div>
    </form>
</body>
</html>
