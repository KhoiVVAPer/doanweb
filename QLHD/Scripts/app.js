$(function() {
	var cdSlideBanner = setInterval(function(){
		$('.next-btn').trigger('click');
	},3000);
	/* 
	    Xử lý hiệu ứng sub menu bên tay trái
	*/
	$('.slidebar-sub-menu').slideUp(0);

		// Xử lý khi click vào 1 slider item
	$('li.slidebar-menu-item>a').click(function(event) {

		// Nếu là có menu con thì hiện đầy đủ slide-bar
		if($(this).parent().hasClass('has-sub-menu')){
				$('.logo-section').removeClass('fixed');
				$('.page-content-wrapper').removeClass('fix-slide-bar');
		}
		// loại bỏ mọi phần tử li có class là clicked ( tô đậm )
		$('.slidebar-sub-menu li').removeClass('clicked');

		// class arrow-down để quay mủi tên chỉ về trái sang xuống đất
		var $ul = $(this).parent();
		$ul.toggleClass('arrow-down');

		// Nếu click vào phần tử đang được chọn có menu-con thì sẽ đóng menu con 
		if($ul.hasClass('clicked') && ($ul.children('ul').hasClass('slidebar-sub-menu'))){
			$('.slidebar-menu-item').removeClass('clicked');
		}else if(!$ul.hasClass('clicked')){

			// Nếu chưa tô đậm thì tô đậm
			$('.slidebar-menu-item').removeClass('clicked');
			$ul.addClass('clicked');	
		}
		$ul.children('ul').slideToggle();
	});
		// Xử lý khi click vào 1 sub-item
	$('.slidebar-sub-menu li').click(function(event) {
		$('.slidebar-sub-menu li').removeClass('clicked');
		$(this).addClass('clicked');
	});
		// Xử lý hiệu ứng khi click vào nút ẩn hiện slide bar
	$('.logo-section span').click(function(event){
		$('.logo-section').toggleClass('fixed');
		$('.page-content-wrapper').toggleClass('fix-slide-bar');
		$('.slidebar-sub-menu').slideUp();
	});

	/* Xử lí slide hinh anh */
	$('.next-btn').click(function(event) {
		var next = $('.slide-item.active').next();
		if(next.index() < 0){
			next = $('.slide-item:nth-child(1)');
		}
			$('.slide-item.active').addClass('movetoleft').one('webkitAnimationEnd', function(event) {
				$('.movetoleft').removeClass('movetoleft');
			});
			next.addClass('goinright').one('webkitAnimationEnd', function(event) {
				$('.active').removeClass('active');
				$('.goinright').addClass('active').removeClass('goinright');
			});;
	});
	$('.pre-btn').click(function(event) {
		var pre = $('.slide-item.active').prev();
		if(pre.index() < 0){
			pre = $('.slide-item:last-child');
		}
			$('.slide-item.active').addClass('movetoright').one('webkitAnimationEnd', function(event) {
				$('.movetoright').removeClass('movetoright');
			});
			pre.addClass('goinleft').one('webkitAnimationEnd', function(event) {
				$('.active').removeClass('active');
				$('.goinleft').addClass('active').removeClass('goinleft');
			});;
	});
	$('.next-btn img').click(function(){ 
		clearInterval(cdSlideBanner); 
	});
	$('.pre-btn img').click(function(){ 
		clearInterval(cdSlideBanner); 
	});
});
