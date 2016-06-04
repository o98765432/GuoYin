<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockInformation.aspx.cs" Inherits="DtCms.Web.StockInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="http://hq.sinajs.cn/list=sh601006" charset="gb2312"></script>
    
    <script src="/webjs/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var i = 1;
        function reload() {
            var elements = hq_str_sh601006.split(",");
            //document.write("current price:" + elements[3] + "  第" + i + "次");
           // $("#")
            $("#realtimeImg").attr("src", "http://image.sinajs.cn/newchart/daily/n/sh601006.gif");
            i++;
        }
       setInterval(reload, 3000);
    </script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
        <div id="realtimeName"></div>
        <img id="realtimeImg" src="" />
    </form>
</body>
</html>
