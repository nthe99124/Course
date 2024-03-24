function getMyWishListsBySearchString(search_string) {
    $('#my_wishlists_area').html('<div class="animated-loader"><div class="spinner-border text-secondary" role="status"></div></div>');
    $.ajax({
        type: 'POST',
        url: 'https://4user.net/home/get_my_wishlists_by_search_string',
        data: {
            search_string: search_string
        },
        success: function (response) {
            $('#my_wishlists_area').html(response);
        }
    });
}

async function handleWishList(elem) {


    try {
        var result = await async_modal();
        if (result) {
            $.ajax({
                url: 'https://4user.net/home/handleWishList',
                type: 'POST',
                data: {
                    course_id: elem.id
                },
                success: function (response) {
                    if ($(elem).hasClass('active')) {
                        $(elem).removeClass('active')
                    } else {
                        $(elem).addClass('active')
                    }
                    $('#wishlist_items').html(response);
                    $.ajax({
                        url: 'https://4user.net/home/reload_my_wishlists',
                        type: 'POST',
                        success: function (response) {
                            $('#modal-4').modal('toggle');
                            $('#my_wishlists_area').html(response);
                        }
                    });
                }
            });
        }
    } catch (e) {
        console.log("Error occured", e.message);
    }
}
