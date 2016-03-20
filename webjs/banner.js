(function () {
            $.fn.extend({
                "touchBanner": function (opt) {
                    var istouch = $.support.touch,
                    touchstart = istouch ? "touchstart" : "mousedown",
                    touchmove = istouch ? "touchmove" : "mousemove",
                    touchend = istouch ? "touchend" : "mouseup";
                    return this.each(function () {
                        var left = true, right = true, $ul = $(this).find("ul"), $li = $("li", $ul), len = $li.length, i = 0, w = $li.width(), timer,x, ismove = false;;
                        $li.first().clone().appendTo($ul);
                                $li.eq(len - 1).clone().prependTo($ul);
                                $ul.css({ marginLeft: -w });
                                clearInterval(timer);
                                timer = setInterval(function () {
                                    opt.left();
                                }, 3000);
								
                        opt = $.extend({
                            "touch": function (event) { },
                            "left": function () {
                                i += 1;
								opt.minbtn.removeClass("cur").eq(i).addClass("cur");
								if (i >= len)
								opt.minbtn.removeClass("cur").eq(0).addClass("cur");
                                $ul.stop(true, true).animate({ marginLeft: -w * (i + 1) }, function () {
                                    if (i >= len)
                                        i = 0;
                                    $ul.css({ marginLeft: -w * (i + 1) });
                                });
                            },
                            "right": function () {
                                i -= 1;
								opt.minbtn.removeClass("cur").eq(i).addClass("cur");
								if (i < 0)
								opt.minbtn.removeClass("cur").eq(len-1).addClass("cur");
                                $ul.stop(true, true).animate({ marginLeft: -w * (i + 1) }, function () {
                                    if (i < 0)
                                        i = len - 1;
                                    $ul.css({ marginLeft: -w * (i + 1) });
                                });
                            },
                            "leftbtn": null,
                            "rightbtn": null,
                            "minbtn": null,
                            "minbtncur": "cur"
                        }, opt);

                        if (opt.leftbtn) {
                            opt.leftbtn.on("tap", function () {
                                clearInterval(timer);
                                opt.right();
                                timer = setInterval(function () {
                                    opt.right();
                                }, 3000);
                            });
                        }
                        if (opt.rightbtn) {
                            opt.rightbtn.on("tap", function () {
                                clearInterval(timer);
                                opt.left();
                                timer = setInterval(function () {
                                    opt.left();
                                }, 3000);
                            });
                        }
                        if (opt.minbtn) {
                            opt.minbtn.on("tap", function () {
                                clearInterval(timer);
                                var i1 = $(this).index();
                                var temp;
                                if (i > i1) {
                                    i = i1;
                                    temp = opt.right;
                                }
                                else if (i < i1) {
                                    i = i1;
                                    temp = opt.left;
                                }
                                if (temp) {
                                    temp();
                                }
                                timer = setInterval(function () {
                                    if (temp) temp(); else opt.left();
                                }, 3000);
                            });
                        }

                        $(this).on(touchstart, function (event) {
                            event.preventDefault();
                            clearInterval(timer)
                            x = event.originalEvent.touches ? event.originalEvent.touches[0].pageX : event.pageX;
                            ismove = true;
							
                        });

                        $(this).on(touchmove, function (event) {
                            if (ismove) {
                                event.preventDefault();
                                var x1 = event.originalEvent.touches ? event.originalEvent.touches[0].pageX : event.pageX;
                                if (x != 0) {
                                    $ul.css({ marginLeft: "+=" + (x1 - x) });
                                    if (x > x1)
                                        left = true, right = false;
                                    else if (x < x1)
                                        left = false, right = true;
                                    else
                                        left = false, right = false;
                                    x = x1;
                                }
                            }
                        });

                        $(this).on(touchend, function (event) {
                            event.preventDefault();
                            if (left)
                                opt.left();
                            else if (right)
                                opt.right();
                            ismove = false;
                            clearInterval(timer)
							timer = setInterval(function () {
							   opt.left();
							}, 3000);
                        })
                    })
                },
                "touchrolling": function (opt) {
                    var istouch = $.support.touch,
                    touchstart = istouch ? "touchstart" : "mousedown",
                    touchmove = istouch ? "touchmove" : "mousemove",
                    touchend = istouch ? "touchend" : "mouseup";

                    var t = this, x, ismove = false;
                    return this.each(function () {

                        var left = true, right = true, $ul = $(this).find("ul"), $li = $("li", $ul), len = $li.length, i = 0, w = $li.outerWidth(true);
                        
                        opt = $.extend({
                            "init": function ($ul) {
                                $li.first().clone().appendTo($ul);
                                $li.eq(len - 1).clone().prependTo($ul);
                                $ul.css({ marginLeft: -w });
                            },
                            "touch": function (event) { },
                            "left": function () {
                                i += 1;
                                $ul.stop(true, true).animate({ marginLeft: -w * (i + 1) }, function () {
                                    if (i >= len)
                                        i = 0;
                                    $ul.css({ marginLeft: -w * (i + 1) });
                                });

                            },
                            "right": function () {
                                i -= 1;
                                $ul.stop(true, true).animate({ marginLeft: -w * (i + 1) }, function () {
                                    if (i < 0)
                                        i = len - 1;
                                    $ul.css({ marginLeft: -w * (i + 1) });
                                });
                            },
                            "leftbtn": null,
                            "rightbtn": null,
                            "minbtn": null,
                            "minbtncur": "cur"
                        }, opt);

                        if (opt.leftbtn) {
                            opt.leftbtn.on("tap", function () {
                                opt.right();
                            });
                        }
                        if (opt.rightbtn) {
                            opt.rightbtn.on("tap", function () {
                                opt.left();
                            });
                        }
                        if (opt.minbtn) {
                            opt.minbtn.on("tap", function () {
                                var i1 = $(this).index();
                                var temp;
                                if (i > i1) {
                                    i = i1;
                                    temp = opt.right;
                                }
                                else if (i < i1) {
                                    i = i1;
                                    temp = opt.left;
                                }
                                if (temp) {
                                    temp();
                                }
                               
                            });
                        }

                        opt.init($ul);

                        $(this).on(touchstart, function (event) {
                            event.preventDefault();
                            x = event.originalEvent.touches ? event.originalEvent.touches[0].pageX : event.pageX;
                            ismove = true;
                        });

                        $(this).on(touchmove, function (event) {
                            if (ismove) {
                                event.preventDefault();
                                var x1 = event.originalEvent.touches ? event.originalEvent.touches[0].pageX : event.pageX;
                                if (x != 0) {
                                    $ul.css({ marginLeft: "+=" + (x1 - x) });
                                    if (x > x1)
                                        left = true, right = false;
                                    else if (x < x1)
                                        left = false, right = true;
                                    else
                                        left = false, right = false;
                                    x = x1;
                                }
                            }
                        });

                        $(this).on(touchend, function (event) {
                            event.preventDefault();
                            if (left)
                                opt.left();
                            else if (right)
                                opt.right();
                            opt.touch(event);
                            ismove = false;
                        })
                    })
                }            
		})
   })
(jQuery);





$(function(){
		
    //$("#index_bannerbox li").css({ width: $(window).width() });
		
		//$(".Popupbg").css("height",$(document).height())
		//$(".Footerapply").click(function(){
		//	$(".Popupbg").fadeIn(300);
		//	$(".Shut").fadeIn(300);
		//	$(".Onlineapplication").fadeIn(300);
		//})
		
		//$(".Shut").click(function(){
		//	$(".Popupbg").hide();
		//	$(".Onlineapplication").hide();
		//	$(".Shut").hide();
		//})
		
		//$(".SubmitFormShut").click(function(){
		//	$(".Popupbg").hide();
		//	$(".SubmitForm").hide();
		//})
		
		//$(".gaoxingneng").click(function(){
		//	$(".Popupbg").fadeIn(300);
		//	$(".Shut").hide();
		//	$(".Onlineapplication").hide();
		//	$(".SubmitForm").fadeIn(300);
		//})
		
    //$("#index_bannerbox").touchBanner({
	//		 "minbtn": $(".dot a"),
	//	});
		
		
		
	})




