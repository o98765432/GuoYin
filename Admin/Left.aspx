<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="DtCms.Web.Admin.Left" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1"><meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><title>
</title><link rel="stylesheet" type="text/css" href="images/Admin_Menu.css" /></head>

<script type="text/javascript">
    var prevMenu = null;

    function $(o) {
        return document.getElementById(o);
    }
    document.getElementsByClassName = function (sClassName) {
        var parent = document;
        if (arguments.length > 1) parent = arguments[1];

        var tags = parent.getElementsByTagName("*");
        var objs = [];
        for (var i = 0; i < tags.length; i++) {
            if (tags[i].className && tags[i].className == sClassName) {
                objs.push(tags[i]);
            }
        }
        return objs;
    };
    window.onload = function () {
        var menus = document.getElementsByClassName("menuTitle");
        for (var i = 0; i < menus.length; i++) {
            //alert(menus[i].id);
            menus[i].isShow = false;
            menus[i].onclick = function () {
                showMenu(this);
            };
        }
    };
    function showMenu(obj) {
        var submenu = obj.nextSibling;
        var title = obj.getElementsByTagName("li")[0];

        if (submenu.nodeType != 1) {
            submenu = submenu.nextSibling;
        }

        if (obj.isShow) {
            obj.isShow = false;
            submenu.style.display = "none";
            title.className = "title";
        } else {
            //hide previous
            if (prevMenu && prevMenu.isShow) {
                prevMenu.isShow = false;
                sm = prevMenu.nextSibling;
                if (sm.nodeType != 1) sm = sm.nextSibling;
                sm.style.display = "none";
                prevMenu.getElementsByTagName("li")[0].className = "title";
            }

            obj.isShow = true;
            submenu.style.display = "block";
            title.className = "currentTitle";
            prevMenu = obj;
        }
    }
</script>
<script  type="text/javascript">
    function switchStatus(para) {
        var mydiv = document.getElementById(para);
        if (mydiv.style.display == "none") {
            mydiv.style.display = "";
        }
        else {
            mydiv.style.display = "none";
        }

    }
    function clickinfo(typeid) {

        if (typeid == 1) {

            document.getElementById("container").style.display = "none";
            document.getElementById("container1").style.display = "block";

        } else {

            document.getElementById("container").style.display = "block";
            document.getElementById("container1").style.display = "none";

        }

    }
</script>
<script language="javascript">
    function switchStatus(para) {
        var mydiv = document.getElementById(para);
        if (mydiv.style.display == "none") {
            mydiv.style.display = "";
        }
        else {
            mydiv.style.display = "none";
        }

    }
</script>

<body>
    
   <div class="container" id="container" >

		 <div class="header">
			<ul>
				<li class="title">国银租赁后台管理系统</li>
			</ul>
		</div>
					    
         	<%=load(0) %>
			
	      <div class="menu">
					<div class="menuTitle">
						<ul>
							<li class="title">
								系统管理
                            </li>
							<li class="right">
								<img src='images/menu_title_down.gif' alt="" border="0" />
                            </li>
						</ul>

					</div>
					<div class="subMenu">
						<ul> 
                            <li><a href="ShowChanner/List.aspx?kindId=0" target="sysMain">栏目管理</a> </li> 
                            <li><a href="Manage/List.aspx?showmatypeid=81" target="sysMain">管理员管理</a> </li> 
                            <li><a href="Admin_Center.aspx" target="sysMain">系统信息</a> </li> 
						</ul>
					</div>
				</div>
	</div>

    <div class="container" style="display:none"  id="container1">

		 <div class="header">
			<ul>
				<li class="version"  onclick="clickinfo(2)">回到主网站</li>
			</ul>
		</div>
					    
         	<%=load(1) %>
			 
	</div> 
</body>
</html>

