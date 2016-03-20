﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="DtCms.Web.Admin.Manage_Advanced.add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>增加管理员</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
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
            //分配管理权限
            $("#rblUserType input").eq(0).click(function() {
                $("#tbList1").hide();
                $("#tbList2").hide();
            });
            $("#rblUserType input").eq(1).click(function() {
                $("#tbList1").show();
                $("#tbList2").show();
            });
            $("#rblUserType input").eq(2).click(function() {
                $("#tbList1").hide();
                $("#tbList2").show();
            });
        });
   </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="list.aspx">返回管理列表</a></span><b>您当前的位置：首页 &gt; 系统管理 &gt; 增加管理员</b>
    </div>
    <div style="padding-bottom:10px;"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
              <tr>
                <th colspan="2" align="left">基本信息设置</th>
              </tr>
              <tr>
                <td width="25%" align="right">登录帐号：</td>
                <td width="75%">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="input required" size="25" 
            maxlength="50" minlength="3" HintTitle="登录用户名（帐号）" HintInfo="必须以字母开头，大于3个字符，小于20个字符，字母或数字或下划线的组合。"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td align="right">登录密码：</td>
                <td>
                <asp:TextBox ID="txtUserPwd" runat="server" CssClass="input required" size="25" minLength="6" 
            maxlength="50" HintTitle="登录密码" HintInfo="大于6个字符，小于50个字符，必须是字母或数字或下划线的组合。" 
                        TextMode="Password"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td align="right">确认密码：</td>
                <td>
                <asp:TextBox ID="txtUserPwd1" runat="server" CssClass="input required" size="25" minLength="6" 
            maxlength="50" equalTo="#txtUserPwd" HintTitle="再次输入密码" HintInfo="请再次输入密码，大于6个字符，小于50个字符，必须与登录密码一致。" 
                        TextMode="Password"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td align="right">是否禁用：</td>
                <td>
                    <asp:RadioButtonList ID="rblIsLock" runat="server" RepeatDirection="Horizontal" 
                        RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">正常</asp:ListItem>
                        <asp:ListItem Value="1">锁定</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
              </tr>
              <tr>
                <td align="right">真实姓名：</td>
                <td>
                <asp:TextBox ID="txtReadName" runat="server" CssClass="input required" size="25" 
            maxlength="50" HintTitle="管理员真实姓名" HintInfo="请输入该管理员的真实姓名，必须为中文汉字。"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td align="right">邮箱地址：</td>
                <td>
                <asp:TextBox ID="txtUserEmail" runat="server" CssClass="input email" size="25" 
            maxlength="50" HintTitle="管理员的邮箱地址" HintInfo="请输入该管理员的邮箱地址，以便日后工作联系。"></asp:TextBox>
                </td>
              </tr>
              
              <tr>
                <td align="right">管理类型：</td>
                <td>
                    <asp:RadioButtonList ID="rblUserType" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">超级管理员</asp:ListItem>
                        <asp:ListItem Value="2">系统管理员</asp:ListItem>
                        <asp:ListItem Value="3">内容管理员</asp:ListItem>
                    </asp:RadioButtonList>
                
                </td>
              </tr>
            </table>
            
           <table id="tbList1" width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable" style="margin-top:5px;display:none;">
              <tr>
                <th colspan="2" align="left">系统权限设置</th>
              </tr>
              <tr>
                <td width="25%" align="right">系统参数设置：</td>
                <td width="75%">
                  <input name="cbLevel" type="checkbox" value="editConfig" />修改
                </td>
              </tr>
              <tr>
                <td align="right">系统模板管理：</td>
                <td>
                  <input name="cbLevel" type="checkbox" value="viewTemplates" />查看
                  <input name="cbLevel" type="checkbox" value="markTemplates" />生成
                </td>
              </tr>
              <tr>
                <td align="right">系统日志管理：</td>
                <td>
                  <input name="cbLevel" type="checkbox" value="viewSystemLog" />查看
                  <input name="cbLevel" type="checkbox" value="delSystemLog" />删除
                </td>
              </tr>
              <tr>
                <td align="right">管理员管理：</td>
                <td>
                  <input name="cbLevel" type="checkbox" value="viewManage" />查看
                  <input name="cbLevel" type="checkbox" value="addManage" />添加 
                  <input name="cbLevel" type="checkbox" value="editManage" />修改
                  <input name="cbLevel" type="checkbox" value="delManage" />删除
                </td>
              </tr>
          </table>
          
          <table id="tbList2" width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable" style="margin-top:5px; display:none">
              <tr>
                <th colspan="2" align="left">内容权限设置</th>
              </tr>
             
              <tr>
                <td align="right">权限管理：</td>
                <td>
                    
								<asp:Repeater ID="rptModule" runat="server" OnItemDataBound="rptModule_ItemDataBound">
									<ItemTemplate>
										<%# Eval("ModuleName") %><br />
										<%--<asp:CheckBoxList ID="cblModule" runat="server" RepeatColumns="4">
										</asp:CheckBoxList>--%>

                                        <asp:Repeater runat="server" ID="RptModuleList">
                                            <ItemTemplate>
                                                <input type="checkbox" name="cbLevel" value="level<%# Eval("Id") %>" /><%# Eval("ModuleName")%>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <br />
                                        <br />
									</ItemTemplate>
								</asp:Repeater>
								<input onclick="ChooseAll()" type="checkbox" name="checkbox" id="checkbox" />
								全选
							
                </td>
              </tr>
              

          </table>
          
          <div style="margin-top:10px; text-align:center;">
            <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" 
                  onclick="btnSave_Click" />
&nbsp;&nbsp; 
            <input type="reset" name="button" id="button" value="重 置" class="submit" />
          </div>
    </form>
</body>
</html>
