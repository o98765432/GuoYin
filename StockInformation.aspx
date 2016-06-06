<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockInformation.aspx.cs" Inherits="DtCms.Web.StockInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="http://hq.sinajs.cn/list=hk02628" charset="gb2312"></script>
    
    <script src="/webjs/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        //$(document).ready(
        //    function () {
        //        reload();
        //    });
        var i = 1;
        function reload() {
            var elements = hq_str_hk02628.split(",");
            //document.write("current price:" + elements[3] + "  第" + i + "次");
            // $("#")
            $("#realtimeImg").attr("src", "http://image.sinajs.cn/newchart/hk_stock/min/02628.gif?"+i);
            $("#realtimeName").text(elements[0]);
            $("#realtimePic").text(elements[3]);
            i++
        }

       setInterval(reload, 3000);
    </script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
        股票名称:<div id="realtimeName"></div><br />
        
        当前价:<div id="realtimePic"></div>
        <img id="realtimeImg" src="" />
    </form>
</body>
</html>
