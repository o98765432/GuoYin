;(function($){
	
	$.extend( jQuery.easing,
	{
		fx :function (x, t, b, c, d) {
			return -c * ((t=t/d-1)*t*t*t - 1) + b;
		}
	});
	
	$.fn.fullscreenScroll = function(options){
		
		var def = {
			index : 0,
			speed : 1000,
			fx : 'fx',
			callback_start : $.noop,
			callback : $.noop,
			pager : null,
			className : 'cur',
			events : 'click'	
		}
		
		var setting = $.extend(def,options);
		
		var self = this;
		
		var len,height,curIndex,list;
		
		curIndex = setting.index;
		
		this.init = function(){
			list = $(this).children();
			console.log(list.attr("class"))
			len = list.length;
			height = $(window).height();
			
			if(setting.pager){
				if($(setting.pager).children().length > 0){
					
				}else{
					for(var i = 0,s = '' ; i < len; i++){
						if(i === setting.index){
							s += '<a class="'+setting.className+'" href="javascript:;">'+'</a>';
						}else{
							s += '<a href="javascript:;">'+'</a>';
						}
						
					}
					$(setting.pager).empty().append(s);
				}
			}
		}
		
		this.skip = function(num){
			num < 0 ? num = 0 : num > len - 1 ? num = len - 1 : true;
			if(num == curIndex) return;
			//callback_start
			setting.callback_start(num);
			
			self.stop(true).animate({top : -height * num},setting.speed,setting.fx,function(){
				setting.callback.call(self,num);	
			});
			if(setting.pager){
				$(setting.pager).children().eq(num).addClass(setting.className).siblings().removeClass(setting.className);
			}
			curIndex = num;
		}
		
		// 初始化
		this.init();
		this.skip(setting.index);
		
		//events   
		if($(document).mousewheel){
			var delayTime = 50, timeHandle = null;
			$(this).mousewheel(function(e,d){
				clearTimeout(delayTime);
				timeHandle = setTimeout(function(){
					self.skip(curIndex-d);
				},delayTime);
			});
		}else{
			//alert('缺少滚轮事件支持！');
		}
		
		if(setting.pager){
			$(setting.pager).delegate('a',setting.events,function(){
				self.skip($(this).index());
			});
		}
		
		$(window).resize(function(){
			self.init();
			self.skip(curIndex);
		});
		
		return this;
	}
})(jQuery);


$(function(){

	
	
})