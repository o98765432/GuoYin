<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.Feedback.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>回复留言</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
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
<body style="padding: 10px;">
    <form id="form1" runat="server">
    <div class="navigation">
        <span class="back"><a href="List.aspx?classid=<%=Request.QueryString["classid"] %>">
            返回列表</a></span><b>您当前的位置：首页 &gt; 留言管理 &gt; </b>
    </div>
    <div class="spClear">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr style="display: none">
            <th colspan="2" align="left">
                回复留言信息
            </th>
        </tr>
        <%
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (showda != null && showda.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < showda.Tables[0].Rows.Count; i++)
                    {
        %>
        <tr>
            <td align="right">
                <%=showda.Tables[0].Rows[i]["Title"].ToString()%>：
            </td>
            <td>
                <%=ds.Tables[0].Rows[0][showda.Tables[0].Rows[i]["ziduan"].ToString()]%>
            </td>
        </tr>
        <%}
                }
            } %>
        <tr>
            <td align="right">
                留言时间：
            </td>
            <td>
                <%=model.AddTime.ToString() %>
            </td>
        </tr>
        <tr>
            <td align="right">
                留言人：
            </td>
            <td>
                <%=model.UserName.ToString() %>
            </td>
        </tr>
        <tr>
            <td align="right">
                邮箱：
            </td>
            <td>
                <%=model.mailBox.ToString() %>
            </td>
        </tr>
         <tr>
            <td align="right">
                电话：
            </td>
            <td>
                <%=model.UserTel.ToString() %>
            </td>
        </tr>
        <%if (classId == 42)
          {
        %>
        <tr>
            <td align="right">
                业务类型：
            </td>
            <td>
                <a href="/Admin/Product/edit.aspx?id=<%=model.orderNum%>&classid=41"><%=model.sex%></a>
            </td>
        </tr>
        <%
            } %>
        <tr>
            <td align="right">
                内容：
            </td>
            <td>
                <%=model.Content.ToString() %>
            </td>
        </tr>
        <tr style="display: none">
            <td align="right">
                回复内容：
            </td>
            <td>
                <asp:TextBox ID="txtReContent" runat="server" TextMode="MultiLine" CssClass="textarea"
                    Style="width: 300px; height: 80px;" HintTitle="回复留言内容" HintInfo="请填写将要回复的内容，字符不限。"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="margin-top: 10px; display: none; text-align: center;">
        <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" OnClick="btnSave_Click" />&nbsp;<input
            name="重置" type="reset" class="submit" value="重置" />
    </div>
    </form>
</body>
</html>
