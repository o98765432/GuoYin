<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="module_add.aspx.cs" Inherits="WEB.manage.Module.module_add" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>添加模块</title>
	<link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
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
        $(function () {
            $("#cbIsImage").bind("click", function () {
                if ($(this).attr("checked") == true) {
                    $(".upordown").show();
                } else {
                    $(".upordown").hide();
                }
            });
        });
    </script>
</head>
<body style="padding:10px;">
	<form id="form1" runat="server">
      <div class="navigation">
      <span class="back"><a href="module.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 模块管理 &gt; 增加模块</b>
    </div>
    <div style="padding-bottom:10px;"></div>
	<div>
		<table width="96%" border="0" cellpadding="0" cellspacing="0" class="msgtable">
			
						<tr>
                            <th colspan="2" align="left">增加模块</th>
                        </tr>
						<tr>
							<td class="c_tdleft">
								选择栏目:
							</td>
							<td class="c_td">
								<span class="c_td" style="height: 28px">
									<asp:DropDownList ID="ddlModule" runat="server">
									</asp:DropDownList>
								</span>
							</td>
						</tr>
						<tr>
							<td width="12%" class="c_tdleft">
								模块名称:
							</td>
							<td width="88%" class="c_td">
								<asp:TextBox ID="txtTitle" runat="server" Width="298px" CssClass="input required" size="30" 
            maxlength="100" minlength="3" HintTitle="网站标题名称" HintInfo="控制在100个字符内，标题文本尽量不要太长。"></asp:TextBox>
								
							</td>
						</tr>
						<tr>
							<td class="c_tdleft">
								模块链接:
							</td>
							<td class="c_td">
								<asp:TextBox ID="txtHref" runat="server" Width="298px" CssClass="input" size="30" 
            maxlength="100"  HintTitle="模块链接" HintInfo="控制在100个字符内"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="c_tdleft">
								优先级:
							</td>
							<td class="c_td">
								<asp:TextBox ID="txtAlias" runat="server" Width="298px" Text="0"  CssClass="input required number" size="10" 
            maxlength="10" HintTitle="该链接的优先级别" HintInfo="纯数字，数字越小越往前排列，可为负数。"></asp:TextBox>
                                请填入数字，数字越大，排序越靠前
                                
							</td>
						</tr>
						
					
		</table>

        <div style="margin-top:10px;text-align:center;">
  <asp:Button ID="btnAdd" runat="server" Text="确认保存" CssClass="submit" onclick="btnAdd_Click" 
        />
  &nbsp;
  <input name="重置" type="reset" class="submit" value="重置" />
	</div>
	</form>
</body>
</html>
