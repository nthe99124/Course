//function handleWishList(elem) {

//    $.ajax({
//        url: 'https://4user.net/home/handleWishList',
//        type: 'POST',
//        data: {
//            course_id: elem.id
//        },
//        success: function (response) {
//            if (!response) {
//                window.location.replace("https://4user.net/login");
//            } else {
//                if ($(elem).hasClass('active')) {
//                    $(elem).removeClass('active')
//                } else {
//                    $(elem).addClass('active')
//                }
//                $('#wishlist_items').html(response);
//            }
//        }
//    });
//}

//function handleCartItems(elem) {
//    url1 = 'https://4user.net/home/handleCartItems';
//    url2 = 'https://4user.net/home/refreshWishList';
//    $.ajax({
//        url: url1,
//        type: 'POST',
//        data: {
//            course_id: elem.id
//        },
//        success: function (response) {
//            $('#cart_items').html(response);
//            if ($(elem).hasClass('addedToCart')) {
//                $('.big-cart-button-' + elem.id).removeClass('addedToCart')
//                $('.big-cart-button-' + elem.id).text("Thêm vào giỏ hàng");
//            } else {
//                $('.big-cart-button-' + elem.id).addClass('addedToCart')
//                $('.big-cart-button-' + elem.id).text("Đã thêm vào giỏ hàng");
//            }
//            $.ajax({
//                url: url2,
//                type: 'POST',
//                success: function (response) {
//                    $('#wishlist_items').html(response);
//                }
//            });
//        }
//    });
//}

//function handleEnrolledButton() {
//    $.ajax({
//        url: 'https://4user.net/home/isLoggedIn',
//        success: function (response) {
//            if (!response) {
//                window.location.replace("https://4user.net/login");
//            }
//        }
//    });
//}

//$(document).ready(function () {
//    if (!/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
//        //if ($(window).width() >= 840) {
//        //    $('a.has-popover').webuiPopover({
//        //        trigger: 'hover',
//        //        animation: 'pop',
//        //        placement: 'horizontal',
//        //        delay: {
//        //            show: 500,
//        //            hide: null
//        //        },
//        //        width: 330
//        //    });
//        //} else {
//        //    $('a.has-popover').webuiPopover({
//        //        trigger: 'hover',
//        //        animation: 'pop',
//        //        placement: 'vertical',
//        //        delay: {
//        //            show: 100,
//        //            hide: null
//        //        },
//        //        width: 335
//        //    });
//        //}
//    }

    

//    //if ($(".top-istructor-slick")[0]) {
//    //    $(".top-istructor-slick").slick({
//    //        dots: false
//    //    });
//    //}
//});

//function bindSlideCourse() {
//    if ($(".course-carousel")[0]) {
//        $(".course-carousel").slick({
//            dots: false,
//            speed: 300,
//            centerPadding: '40px',
//            slidesToShow: 5,
//            slidesToScroll: 5,
//            swipe: false,
//            touchMove: false,
//            responsive: [
//                { breakpoint: 1200, settings: { slidesToShow: 4, slidesToScroll: 4, }, },
//                { breakpoint: 840, settings: { slidesToShow: 3, slidesToScroll: 3, }, },
//                { breakpoint: 620, settings: { slidesToShow: 2, slidesToScroll: 2, }, },
//                { breakpoint: 480, settings: { slidesToShow: 1, slidesToScroll: 1, }, },
//            ],
//        });
//    }
//}

