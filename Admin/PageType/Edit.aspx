<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.PageType.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=strtitle %>内容</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script> 
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

        KindEditor.ready(function (K) {
            editor = K.create('#txtImgUrl1',
        {
            uploadJson: '../../../KindEditor/asp.net/upload_json.ashx',
            fileManagerJson: '../../../KindEditor/asp.net/file_manager_json.ashx',
            allowFileManager: true
        });
        });

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
      <span class="back"><a href="List.aspx?classId=<%=this.classid %>">返回列表</a></span><b>您当前的位置：首页 &gt; 内容管理 &gt; <%=strtitle %>内容</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left"><%=strtitle %>内容</th>
        </tr>

        <tr>
            <td width="15%" align="right">页面形式名称：</td>
            <td width="85%">
            <asp:TextBox ID="txtPageTitle" runat="server" CssClass="input w380 required" 
            maxlength="100"  HintTitle="增加的内容信息标题" HintInfo="控制在100个字数内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>

        <tr >
            <td align="right" valign="top">页面形式描述：</td>
            <td>
            <asp:TextBox ID="txtPageContent" runat="server" CssClass="textarea wh380"  
            maxlength="250" HintTitle="Meta描述" 
                    HintInfo="用于搜索引擎，控制在250个字数内，不填写将自动提取。" TextMode="MultiLine"></asp:TextBox>
             
            </td>
        </tr>

       
      
        

        

      

        
        <tr  style="display:none">
            <td align="right" valign="top">添加连接：</td>
            <td>
                
                <asp:RadioButton ID="radNeilians" runat="server" onclick="radNeilianClick();"   GroupName="linksinfo"/>
                <asp:TextBox ID="neilians" runat="server" CssClass="input w380 left" style="display:none"></asp:TextBox>
                内链
                <asp:RadioButton ID="radWailians" runat="server" onclick="radWailianClick();"   GroupName="linksinfo"/>
                <asp:TextBox ID="wailians" runat="server" CssClass="input w380 left" style="display:none"></asp:TextBox>
                外链
            </td>
        </tr>
        
        <tr style="display:none">
            <td align="right">Meta标题：</td>
            <td>
            <asp:TextBox ID="txtSeoTitle" runat="server" CssClass="input w250" 
            maxlength="100" HintTitle="Meta关键字" HintInfo="用于搜索引擎，如有多个关健字请用英文的,号分隔，不填写将自动提取标题。"></asp:TextBox>
            </td>
        </tr>

        <tr style="display:none">
            <td align="right">Meta关键字：</td>
            <td>
            <asp:TextBox ID="txtSeoKeyword" runat="server" CssClass="textarea wh380"  
            maxlength="250" HintTitle="Meta描述" 
                    HintInfo="用于搜索引擎，控制在250个字数内，不填写将自动提取。" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

        <tr style="display:none">
            <td align="right">Meta描述：</td>
            <td>
            <asp:TextBox ID="txtSeoDescription" runat="server" CssClass="textarea wh380"  
            maxlength="250" HintTitle="Meta描述" 
                    HintInfo="用于搜索引擎，控制在250个字数内，不填写将自动提取。" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
      
        <tr>
            <td align="right">优先级别：</td>
            <td>
            <asp:TextBox ID="txtSortId" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="内容的优先级别" HintInfo="纯数字，数值越小越靠前。">0</asp:TextBox>

            <span id="upordown" class="up-02" style="display:none">显示高级选项</span>
            </td>
        </tr>

        <tr  style="display:none">
            <td align="right">产品属性：</td>
            <td>
               <asp:CheckBox ID="IsLock" runat="server" Checked="True" Text="可见性" />
               <asp:CheckBox ID="IsTop" runat="server" Text="置顶" /> 
               <asp:CheckBox ID="IsHot" runat="server" Text="首页" /> 
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

