$(document).ready(function () {
    $(".course-carousel").slick({
        dots: false,
        speed: 300,
        centerPadding: '40px',
        slidesToShow: 4,
        slidesToScroll: 4,
        swipe: false,
        touchMove: false,
        responsive: [
            { breakpoint: 1200, settings: { slidesToShow: 4, slidesToScroll: 4, }, },
            { breakpoint: 840, settings: { slidesToShow: 3, slidesToScroll: 3, }, },
            { breakpoint: 620, settings: { slidesToShow: 2, slidesToScroll: 2, }, },
            { breakpoint: 480, settings: { slidesToShow: 1, slidesToScroll: 1, }, },
        ],
    });
});
function handleCartItems(elem) {
    url1 = 'https://4user.net/home/handleCartItems';
    url2 = 'https://4user.net/home/refreshWishList';
    $.ajax({
        url: url1,
        type: 'POST',
        data: {
            course_id: elem.id
        },
        success: function (response) {
            $('#cart_items').html(response);
            if ($(elem).hasClass('active')) {
                $(elem).removeClass('active')
                $(elem).text("Thêm vào giỏ hàng");
            } else {
                $(elem).addClass('active');
                $(elem).addClass('active');
                $(elem).text("Đã thêm vào giỏ hàng");
            }
            $.ajax({
                url: url2,
                type: 'POST',
                success: function (response) {
                    $('#wishlist_items').html(response);
                }
            });
        }
    });
}

function handleBuyNow(elem) {

    url1 = 'https://4user.net/home/handleCartItemForBuyNowButton';
    url2 = 'https://4user.net/home/refreshWishList';
    urlToRedirect = 'https://4user.net/home/shopping_cart';
    var explodedArray = elem.id.split("_");
    var course_id = explodedArray[1];

    $.ajax({
        url: url1,
        type: 'POST',
        data: {
            course_id: course_id
        },
        success: function (response) {
            $('#cart_items').html(response);
            $.ajax({
                url: url2,
                type: 'POST',
                success: function (response) {
                    $('#wishlist_items').html(response);
                    toastr.success('Xin vui lòng đợi....');
                    setTimeout(
                        function () {
                            window.location.replace(urlToRedirect);
                        }, 1000);
                }
            });
        }
    });
}

function handleEnrolledButton() {
    $.ajax({
        url: 'https://4user.net/home/isLoggedIn?url_history=aHR0cHM6Ly80dXNlci5uZXQvaG9tZS9jb3Vyc2UvbCVFMSVCQSVBRHAtdHIlQzMlQUNuaC1qYXZhLXdlYi1wcmozMDEvMzQ=',
        success: function (response) {
            if (!response) {
                window.location.replace("https://4user.net/login");
            }
        }
    });
}

function handleAddToWishlist(elem) {
    $.ajax({
        url: 'https://4user.net/home/isLoggedIn?url_history=aHR0cHM6Ly80dXNlci5uZXQvaG9tZS9jb3Vyc2UvbCVFMSVCQSVBRHAtdHIlQzMlQUNuaC1qYXZhLXdlYi1wcmozMDEvMzQ=',
        success: function (response) {
            if (!response) {
                window.location.replace("https://4user.net/login");
            } else {
                $.ajax({
                    url: 'https://4user.net/home/handleWishList',

                    data: {
                        course_id: elem.id
                    },
                    success: function (response) {
                        if ($(elem).hasClass('active')) {
                            $(elem).removeClass('active');
                            $(elem).text("Thêm vào danh sách yêu thích");
                        } else {
                            $(elem).addClass('active');
                            $(elem).text("Đã thêm vào danh sách yêu thích");
                        }
                        $('#wishlist_items').html(response);
                    }
                });
            }
        }
    });
}

function pausePreview() {
    player.pause();
}

$('.course-compare').click(function (e) {
    e.preventDefault()
    var redirect_to = $(this).attr('redirect_to');
    window.location.replace(redirect_to);
});

function go_course_playing_page(course_id, lesson_id) {
    var course_playing_url = "https://4user.net/home/lesson/lập-trình-java-web-prj301/" + course_id + '/' + lesson_id;

    $.ajax({
        url: 'https://4user.net/home/go_course_playing_page/' + course_id,
        type: 'POST',
        success: function (response) {
            if (response == 1) {
                window.location.replace(course_playing_url);
            }
        }
    });
}