<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.IndexChanner.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script src="../../Js/jquery-1.8.1.min.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".showallid").click(function () {

                if ($(".showallid").attr("checked") == "checked") {

                    $(".showid").attr("checked", true);

                } else {


                    $(".showid").attr("checked", false);

                }

            });

            $(".btn_all").click(function () {

                var allshowinfobyweb = "";

                for (i = 0; i < $(".showid").length; i++) {

                    if (allshowinfobyweb == "") {

                        if ($(".showid").eq(i).attr("checked") == "checked") {

                            allshowinfobyweb = $(".showid").eq(i).val() + ",1";

                        } else {

                            allshowinfobyweb = $(".showid").eq(i).val() + ",0";

                        }

                    } else {

                        if ($(".showid").eq(i).attr("checked") == "checked") {

                            allshowinfobyweb += "*" + $(".showid").eq(i).val() + ",1";

                        } else {

                            allshowinfobyweb += "*" + $(".showid").eq(i).val() + ",0";


                        }


                    }

                }
                location.href = "Edit.aspx?id=<%=id %>&showallinfo=" + allshowinfobyweb;


            });


        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="navigation">
        <span class="add"></span><b>您当前的位置：首页 &gt; 快捷通道信息 &gt; 快捷通道详细信息</b></div>
    <div class="spClear">
    </div>
    <table width="99%" border="0" cellspacing="0" cellpadding="0" class="msgtable" align="center">
        <tr>
            <th width="8%">
                选择<input type="checkbox" class="showallid" value="">
            </th>
            <th align="left">
                标题
            </th>
        </tr>
        <%= allinfo  %>
    </table>
    <div class="spClear"></div>
        <div style="line-height:30px;height:30px; margin-left:200px;">
            <div id="Pagination" class="right flickr"></div>
            <div class="left">
                <span class="btn_all">提交</span>
                 
            </div>
	</div>
    </form>
</body>
</html>
