
$(function () {
    $("a").each(function () {
        if ($(this).attr("href").match("javascript") != "javascript") {
            $(this).attr("href", $(this).attr("href") + "?#positon");
        }
    });
    $(".title_cent").attr("id", "positon");

    //$(".online_consultantfl_input").click(function () {
    //    $(this).find("input").select();
    //});
    Search_evn($(".input_padding input"));
});
function Search_evn(obj) {
    var Nulls = "";
    obj.parent().click(function () {
        if ($.trim($(this).val()) == Nulls) {
            $(this).parent().find("span").hide();

        }
        $(this).parent().find("input").select();
    });
    obj.blur(function () {
        if ($.trim($(this).val()) == Nulls) {
            $(this).parent().find("span").show();
        } else {
            $(this).parent().find("span").hide();
        }
    });
    obj.focus(function () {

        if ($.trim($(this).val()) == Nulls) {
            $(this).parent().find("span").hide();

        } else {
            $(this).parent().find("span").hide();
        }
        
    });
}
var winw, winh, cur_index = 0, curIndex = 0;
var delayTime = 100, timeHandle = null;
function Lg() {//语言选择
    var lgul = $("#lg_ul");
    var lgtext = $("#lg_text");
    $(document).delegate("#lg_text", "click", function () {
        lgul.stop(true, true).animate({ "height": (lgul.find("li").outerHeight() * lgul.find("li").length), "overflow": "visible" });

    });
    $(document).delegate("#lg_box", "hover", function () { }, function () {
        lgul.stop(true, true).animate({ "height": "0px", "overflow": "hidden" });
    });
    $(document).delegate("#lg_ul li", "click", function () {
        lgtext.html($(this).html());
        lgul.stop(true, true).animate({ "height": "0px", "overflow": "hidden" });
    });
}
function ListHoverTextColor() {
    $(document).delegate(".listbox_margin", "hover", function (e) {
        if (e.type == "mouseleave") {//离开
            $(this).removeClass("gyzp_textcolorred");
        }
        if (e.type == "mouseenter") {//
            $(this).addClass("gyzp_textcolorred");
        }
    });
    var timecc = 500;
    $(document).delegate(".gypz_cgan_boxabsolute", "hover", function (e) {
        if (e.type == "mouseleave") {//离开
            $(".gypz_cgan_boxabsolute").stop(true).animate({ "top": "25px" }, timecc);
            $(".gypz_cgan_frlist").stop(true).animate({ "background-position-y": "50%", "height": "30px" }, timecc);
        }
        if (e.type == "mouseenter") {//
            $(".gypz_cgan_boxabsolute").stop(true).animate({ "top": "-65px" }, timecc);
            $(".gypz_cgan_frlist").stop(true).animate({ "background-position-y": "90%", "height": "120px" }, timecc);
        }
    });
}
function MovesOver() {
    var gypztabbox = $(".gypz_tabbox");
    var gypztabcont1 = gypztabbox.eq(0).find(".gypz_tab_cont1");
    gypztabcont1.css({ "margin-top": -(gypztabcont1.outerHeight() / 2) });
    $(document).delegate("#gypz_tabul1 li", "hover", function (e) {
        var curevent = $("#gypz_tabul1 li");
        var gypztabbox = $(".gypz_tabbox");
        var curindex = $(this).index();
        var gypztabcont1 = gypztabbox.eq(curindex).find(".gypz_tab_cont1");
        if (e.type == "mouseleave") {//离开
            //$(this).removeClass("select");
        }
        if (e.type == "mouseenter") {//
            curevent.removeClass("select");
            $(this).addClass("select");
            gypztabbox.hide().eq(curindex).show();
            gypztabcont1.css({ "margin-top": -(gypztabcont1.outerHeight() / 2) });
        }

    }); $("#gypz_tabul1 li:eq(0)").addClass("select");
}
function BusinessTabButton() {
    $(document).delegate("#business_tab_button span", "hover", function (e) {
        //if (e.type == "mouseleave") {//离开
        //    $(this).removeClass("select1");
        //}
        //if (e.type == "mouseenter") {//
        //    $(this).addClass("select1");
        //}
    }).delegate("#business_tab_button span", "hover", function (e) {
        if (e.type == "mouseenter") {
            $("#business_tab_button span").removeClass("select");
            $(this).addClass("select");
            $(".business_tab_box").hide().eq($(this).index()).show();
        }
    });

    $("#partner_links_ul li").gyzy({ more: "#gyzp_hzhb_more",number:12 });
}
function PopEvent() {
    var MaskDiv = $(".mask");
    var Popbox = $(".pop");
    var poppositonbox = $(".pop_positonbox");
    var liitem = $("#main_honors li");
    var liitemImg = $("#main_honors li .main_honors_img img");
    var  popind=0;
    var masksizes = function () {
        var winheight = $(window).height();
        var winwidth = $(window).width();
        var Popwidth = Popbox.width();
        var Popheight = Popbox.height();
        var popimgwidth = Popbox.find(".pop_imgbox_re img").width();
        var popimgheight = Popbox.find(".pop_imgbox_re img").height();
        MaskDiv.css({ "opacity": 0.5, "height": winheight, "width": winwidth });
        $(".pop_fl_top_top,.pop_fl_top_bottom").css("width", popimgwidth+45);
        $(".pop_fl_top_left,.pop_fl_top_right").css("height", Popheight - 5);
        Popbox.css({ "top": (winheight / 2) - ((popimgheight+40) / 2), "left": (winwidth / 2) - ((popimgwidth+40) / 2) });
        Popbox.find(".pop_positonbox").css({ "width": popimgwidth });

        
    }
    liitemImg.click(function () {
        MaskDiv.show();
        Popbox.show();
        popind = $(this).parents("li").index();
        Popbox.find("img").attr("src", $(this).parents("li").find("img").attr("src"));
        Popbox.find(".imgdetail_text").html($(this).parents("li").find(".main_honors_txt").html());
        masksizes();
    });
    $(".pop_colse").click(function () {

        MaskDiv.hide();
        Popbox.hide();
    });
    $(".pop_prev").click(function () {

        
        if (popind <= 0) {
            popind = 0;
        } else {
            $(this).parents(".pop_box").find("img").attr("src", liitem.eq(popind).prev().find("img").attr("src"));
            $(this).parents(".pop_box").find(".imgdetail_text").html(liitem.eq(popind).prev().find(".main_honors_txt").html());
            masksizes();popind--;
        } 
    });
    $(".pop_next").click(function () {
        if (popind >= (liitem.length - 1)) {
            popind = liitem.length - 1;
        } else {
            $(this).parents(".pop_box").find("img").attr("src", liitem.eq(popind).next().find("img").attr("src"));
            $(this).parents(".pop_box").find(".imgdetail_text").html(liitem.eq(popind).next().find(".main_honors_txt").html());
            masksizes();popind++;
        }
    });
    $(window).resize(function () {
        masksizes();
    });
}
function MenuAboutUs() {
    var cur_position, curleft;
    var winw = $(window).width();
    var curover = function (curthis, curpositon) {
        //console.log(curpositon.left + "...." + Math.ceil(curpositon.left))
        curthis.find(".about_us_nav").css({ "width": winw, "height": curthis.find(".about_us_nav").height(), "left": -(curpositon.left), "top": (curthis.outerHeight() / 2), "padding-top": (curthis.outerHeight() / 2) }).show();
        curthis.find("a:eq(0)").addClass("select_1");
    }
    var curout = function (curthis) {
            curthis.find(".about_us_nav").hide();
            curthis.find("a:eq(0)").removeClass("select_1");
    }
    $(".aboutusli").hover(function () {
        cur_position = $(this).offset();
        curover($(this), cur_position);
       
    }, function () {
        curout($(this));
    });

}

function in_box_height() {
    $(".body_box").each(function () {
        $(this).css("height", winh);
    });
    var inrightbtnbox = $(".in_right_btnbox span");
    var bodys = $(".bodys");
    var inbtn = $(".in_btn");
    var BodysLen = bodys.find(".body_box").length;
    var allclassname = "in_right_buttoncur1 in_right_buttoncur2 in_right_buttoncur3 in_right_buttoncur4 in_right_buttoncur5";
    
    var returntop = function () {//返回顶部
        inrightbtnbox.removeClass(allclassname).eq(BodysLen - 1).addClass("in_right_buttoncur" + BodysLen);
        bodys.stop(true).animate({ "top": "0px" }, 1000, "easeOutExpo", function () {
            inbtn.show();
            inrightbtnbox.removeClass(allclassname);
            cur_index = 0;
        });
    }
    var BtnNext = function () {//向下翻
       cur_index++;
       if (cur_index == BodysLen-1) {
           inbtn.hide();
       }
       if (cur_index >= BodysLen) {
           cur_index = BodysLen - 1;
           returntop();
       } else{
           var nowtop = cur_index * winh;
           bodys.stop(true).animate({ "top": -nowtop }, 1000, "easeOutExpo");
           inrightbtnbox.removeClass(allclassname).eq(cur_index - 1).addClass("in_right_buttoncur" + cur_index);
           divanimate();
       }
   }
   var BtnPrev = function () {//向上翻
       cur_index--;
       if (cur_index <0) {
           cur_index = 0;
       } else {
           var nowtop = cur_index * winh;
           bodys.stop(true).animate({ "top": -nowtop }, 1000, "easeOutCubic");
           inrightbtnbox.removeClass(allclassname).eq(cur_index-1).addClass("in_right_buttoncur" + cur_index);
           divanimate();
       }
       inbtn.show();
   }
    //easeOutBounce，easeOutCubic，easeOutSine，easeOutExpo，easeOutElastic，easeInOutBounce，
   var divanimate = function () {//当前屏下的动画
       var curdiv = bodys.find(".body_box").eq(cur_index);
       setTimeout(function () {
           curdiv.find("div.in_animatbox_1").animate({ "left": "0px", "opacity": 1 }, 1200, "easeOutExpo");
       }, 200);
       setTimeout(function () {
           curdiv.find("div.in_animatbox_2").animate({ "right": "0px", "opacity": 1 }, 1200, "easeOutExpo");
       }, 200);
       $(".in_animatbox_1").css({ "left": -(winw), "opacity": 0 });
       $(".in_animatbox_2").css({ "right": -(winw), "opacity": 0 });
       curdiv.find(".in_animatbox_1").css({ "left": -(winw), "opacity": 0 });
       curdiv.find(".in_animatbox_2").css({ "right": -(winw), "opacity": 0 });
   }

   var resizebodytop = function () {//浏览器变化时
       var nowtop = cur_index * winh;
       bodys.stop(true).css({ "top": -nowtop });
   }
   var RgButton = function (curindex) {//点击右边菜单判断是点击上还是下
       if (curindex == (BodysLen - 1)) {
           returntop();
       } else {
               BtnNext();
       }
   }
   inbtn.click(function () {//点击中间按钮向下翻
       //向下
       BtnNext();
   });
   inrightbtnbox.click(function () {//点击右边菜单
       cur_index = $(this).index();
       if (cur_index <= 2) {
           inbtn.show();
       }
       RgButton(cur_index);
   });

       
       $(document).mousewheel(function (e, d) {
           clearTimeout(timeHandle);
           timeHandle = setTimeout(function () {
                   if (d ==1) {
                       //向上
                       BtnPrev();
                   } else if(d==-1) {
                       //向下
                           BtnNext();
                   }
           }, delayTime);
        return false;
    });
    resizebodytop();
}
function in_banner() {
    var sWidth, IndexBanner, Bannerbtnbg, index, ulbox, curlen = 0, len, libox;
    var bannerheight;
    var cssname = "curs";
    var topbox = $("#top").height();
    
    IndexBanner = $("#in_banner");
   var initev = function () {
        winw = $(window).width();
        winh = $(window).height();
        bannerheight = parseInt(winh - topbox);
        IndexBanner.css({ "height": bannerheight, "width": winw });
        IndexBanner.find("img").css({ "height": bannerheight, "width": winw });
        IndexBanner.find("ul li").css({ "height": bannerheight });
     sWidth = IndexBanner.width();
     
        in_box_height();
    }
    initev();
    $(window).resize(function () {
       initev();
    });

        
        IndexBanner.find("ul li").each(function (i) {
            //if (i > 0) {
            //$(this).css("width", winw);
            //}
            $("<span></span>").appendTo(IndexBanner.find("div"));
        });

        
        Bannerbtnbg = IndexBanner.find("div span");
        sWidth = IndexBanner.width(); //获取焦点图的宽度（显示面积）

        index = 0;
        var picTimer;
        Bannerbtnbg.mouseover(function () {
            index = $(this).index();
            showPics(index);
        });
        Bannerbtnbg.eq(0).addClass(cssname);
        len = IndexBanner.find("ul li").length; //获取焦点图个数
        //IndexBanner.find("ul").css("width", sWidth * (len + 1));
        //鼠标滑上焦点图时停止自动播放，滑出时开始自动播放
        //IndexBanner.hover(function () {
            //clearInterval(picTimer);
        //}, function () {
            picTimer = setInterval(function () {
                if (index == len) { //如果索引值等于li元素个数，说明最后一张图播放完毕，接下来要显示第一张图，即调用showFirPic()，然后将索引值清零
                    showFirPic();
                    index = 0;
                } else { //如果索引值不等于li元素个数，按普通状态切换，调用showPics()
                    showPics(index);
                }
                index++;

            }, 4000); //此4000代表自动播放的间隔，单位：毫秒
        //}).trigger("mouseleave");
    //}
    function showPics(index) { //普通切换
        var nowLeft = -index * sWidth; //根据index值计算ul元素的left值

        IndexBanner.find("ul").stop(true).animate({ "left": nowLeft }, 900, "easeOutExpo"); //通过animate()调整ul元素滚动到计算出的position
        Bannerbtnbg.removeClass(cssname).eq(index).addClass(cssname); //为当前的按钮切换到选中的效果

    }
    function showFirPic() {
        var nowLeft = -len * sWidth;
        IndexBanner.find("ul").append(IndexBanner.find("ul li:first").clone());
        IndexBanner.find("ul").animate({ "left": nowLeft }, 1000, "easeOutExpo", function () {
            IndexBanner.find("ul").hide().css("left", "0").show();
            IndexBanner.find("ul li:last").remove();
        });
        Bannerbtnbg.removeClass(cssname).eq(0).addClass(cssname); //为当前的按钮切换到选中的效果
    }
}
function IndexTab() {
    var innewsfltab = $("#in_news_fl_tab a");
    var innewsfltabbox = $(".in_news_fl_tabbox");
    innewsfltab.each(function (i) {
        $(this).attr("index", i);
    })
    $(document).delegate("#in_news_fl_tab a", "hover", function (e) {
        
        if (e.type == "mouseenter") {
            innewsfltab.removeClass("curr");
            $(this).addClass("curr");
            innewsfltabbox.hide().eq($(this).attr("index")).show();
        }
        //if (e.type == "mouseleave") {

        //}
        
    });
}
function PrimaryService() {
    //$("#listbox_1 li:odd").addClass("cont_box_bg");
    $("#listbox_1 li:even").addClass("cont_box_bg");
}
function PageLoad() {//静态页面加载页面
    $("#top").load("top.html");
    $("#footer").load("bottom.html");
    TestEvent();
   
}
function TestEvent() {
    setTimeout(function () {
        var hrf = window.location.href;
        var menua = $("#top .menu_ul a");//导航菜单
        var newsa1 = $(".title_box a");//当前导航菜单下的子级a标签跳转
        var newsa2 = $(".newbox_ul a");//当前导航菜单下的子级a标签跳转
        var newsa3 = $(".list_ul a");//当前导航菜单下的子级a标签跳转
        var newsa4 = $(".business_tab a");//当前导航菜单下的子级a标签跳转
        var newsa5 = $(".in_newfr a");//当前导航菜单下的子级a标签跳转
        var NavCss = function (classNameDef, num) {
            menua.removeClass(classNameDef);
            menua.eq(num).addClass(classNameDef);

        }
        var ev = function (obj, num) {
            obj.each(function () {
                $(this).attr("href", $(this).attr("href") + "?n=" + num);
            }); 
        }
        NavCss("select", 0);
        menua.each(function (i) {
            var ap = $(this);
            var ahref = ap.attr("href");
            $(this).attr("href", ahref + "?n=" + i);
            if (hrf.match("n=" + i + "") == "n=" + i + "") {
                NavCss("select", i);
                ev(newsa1, i); ev(newsa2, i); ev(newsa3, i);
                ev(newsa4, i); 
                ev(newsa5, i);
            }

        }); 
    }, 500);
}
function businessUl() {
    var BoxLi = $("#business_list li");
    var BoxLiWidth = BoxLi.width();
    var UlWidth = BoxLiWidth * BoxLi.length;
    var liwidth = BoxLi.parent().parent().outerWidth();
    var BoxLiLen = Math.ceil(UlWidth / liwidth);

    var Liwidth = 0, ind = 0;
    BoxLi.parent().css("width", UlWidth);
    $(".business_next").click(function () {
        ind++;
        if (ind >= BoxLiLen) {
            ind = BoxLiLen - 1;
        } else {
            var UlLeft = liwidth * ind;
            BoxLi.parent().stop(true, true).animate({ "left": "-" + UlLeft });
        }
    });
    $(".business_prev").click(function () {
        ind--;
        if (ind < 0) {
            ind = 0;
        } else {
            var UlLeft = -liwidth * ind;
            BoxLi.parent().stop(true, true).animate({ "left": UlLeft });
        }
    });
}
//function businessUl() {
//    var BoxLi = $("#business_list li");
//    var BoxLiWidth = BoxLi.width();
//    var UlWidth = BoxLiWidth * BoxLi.length;
//    var liwidth = BoxLi.parent().parent().outerWidth();
//    var BoxLiLen = Math.ceil(UlWidth / liwidth);

//    var Liwidth = 0, ind = 0;
//    BoxLi.parent().css("width", UlWidth);
//    $(".business_next").click(function () {
//        ind++;
//        if (ind >= (BoxLi.length - 3)) {
//            ind = BoxLi.length - 4;
//        } else {
//            var UlLeft = BoxLiWidth * ind;
//            BoxLi.parent().stop(true, true).animate({ "left": -UlLeft });
//        }
//    });
//    $(".business_prev").click(function () {
//        ind--;
//        if (ind < 0) {
//            ind = 0;
//        } else {
//            var UlLeft = BoxLiWidth * ind;
//            BoxLi.parent().stop(true, true).animate({ "left": -UlLeft });
//        }
//    });
//}
//
$.parallax = {

    instance: [],

    id: 0

}

$.fn.parallax = function (options) {

    if (this.length > 1) {
        this.each(function () {
            $(this).parallax();
        });
        return;
    }

    var left = parseInt(this.css('left')) || 0;
    var top = parseInt(this.css('top')) || 0;
    var right = parseInt(this.css('right')) || 0;
    var bottom = parseInt(this.css('bottom')) || 0;

    var def = {

        start: {
            left: left,
            top: top,
            opacity: 1,
            right: 0,
            bottom: 0
        },

        end: {
            left: left,
            top: top,
            opacity: 1,
            right: right,
            bottom: bottom
        },

        speed: 400,

        easing: 'easeOutExpo',
        end_callback: $.noop
    }


    var opt = $.extend(true, def, options);

    if (opt.start.right) {
        opt.start.left = "auto";
        opt.end.left = "auto";
    } else {
        opt.start.right = "auto";
        opt.end.right = "auto";
    }
    if (opt.start.bottom) {
        opt.start.top = "auto";
    } else {
        opt.start.bottom = "auto";
    }
    var status = 0;

    var self = this;
    $.parallax.instance.push({
        id: $.parallax.id++,
        dom: self,
        start: opt.start,
        end: opt.end,
        speed: opt.speed,
        easing: opt.easing,
        status: status,
        end_callback: opt.end_callback
    })

}
$(function () {

    $(".inbannerbox img").eq(0).parallax({ start: { top: 100, opacity: 0 }, speed: 500 });

    var winHeight = $(window).height();

    var inWindow = function (dom) {
        var d = dom.get(0);
        var p = d.getBoundingClientRect();
        winHeight = $(window).height();


        return p.top < winHeight / 1.1;
    }


    var init = function () {
        for (var i = 0 ; i < $.parallax.instance.length; i++) {
            var d = $.parallax.instance[i];
            if (d.status < 1) {
                d.dom.css(d.start);
                d.status = 1;
            }
        }
    }


    var move = function () {
        for (var i = 0 ; i < $.parallax.instance.length; i++) {
            var d = $.parallax.instance[i];
            if (d.dom.length < 1) return;
            if (d.status > 0 && inWindow(d.dom)) {
                var f = d.end_callback;
                d.dom.animate(d.end, d.speed, d.easing, function () {
                    f();
                });
                d.status = 0;
            }
        }
    }

    init();
    move();
});
(function ($) {
    $.fn.gyzy = function (opt) {
        var defaults = {
            more: null,
            number:null
        }
        $.extend(defaults, opt);
        opt.number = parseInt(opt.number-1);
        $(this).each(function (i) {
            if (i > opt.number) {
                $(this).hide().addClass("curstate");
                $(opt.more).parent().show();
            }
            if (i < opt.number) {
                $(opt.more).parent().hide();
            }
        });
        $(opt.more).click(function () {

            $(".curstate").each(function (i) {
                if (i <= opt.number) {
                    $(this).removeAttr("class").removeAttr("style");
                }

            });
            if ($(".curstate").length == 0) {
                $(this).parent().hide();
            }
        });
    }
})(jQuery);