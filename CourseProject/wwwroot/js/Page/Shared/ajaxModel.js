function confirm_modal(delete_url) {
    jQuery('#modal-4').modal('show', { backdrop: 'static' });
    document.getElementById('delete_link').setAttribute('href', delete_url);
}

function showAjaxModal(url) {
    // SHOWING AJAX PRELOADER IMAGE
    jQuery('#modal_ajax .modal-body').html('<div class="w-100 text-center pt-5"><img class="mt-5 mb-5" width="80px" src="https://4user.net/assets/global/gif/page-loader-2.gif"></div>');

    // LOADING THE AJAX MODAL
    jQuery('#modal_ajax').modal('show', { backdrop: 'true' });

    // SHOW AJAX RESPONSE ON REQUEST SUCCESS
    $.ajax({
        url: url,
        success: function (response) {
            jQuery('#modal_ajax .modal-body').html(response);
        }
    });
}

function async_modal() {
    const asyncModal = new Promise(function (resolve, reject) {
        $('#modal-4').modal('show');
        $('#modal-4 .btn-yes').click(function () {
            resolve(true);
        });
        $('#modal-4 .btn-cancel').click(function () {
            resolve(false);
        });
    });
    return asyncModal;
}

function viewMore(element, visibility) {
    if (visibility == "hide") {
        $(element).parent(".view-more-parent").addClass("expanded");
        $(element).remove();
    } else if ($(element).hasClass("view-more")) {
        $(element).parent(".view-more-parent").addClass("expanded has-hide");
        $(element)
            .removeClass("view-more")
            .addClass("view-less")
            .html("- Xem ít hơn");
    } else if ($(element).hasClass("view-less")) {
        $(element).parent(".view-more-parent").removeClass("expanded has-hide");
        $(element)
            .removeClass("view-less")
            .addClass("view-more")
            .html("+ Xem thêm");
    }
}

