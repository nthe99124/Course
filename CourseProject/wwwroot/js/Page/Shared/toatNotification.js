$(function () {
    $('[data-bs-toggle="tooltip"]').tooltip()
});
if ($('.tagify').height()) {
    $('.tagify').tagify();
}

function lesson_preview(url, title) {
    // SHOWING AJAX PRELOADER IMAGE
    jQuery('.lesson_preview_header').html(title);
    jQuery('#lesson_preview .modal-body').html('<div class="w-100 text-center pt-5"><img class="mt-5 mb-5" width="80px" src="https://4user.net/assets/global/gif/page-loader-2.gif"></div>');

    // LOADING THE AJAX MODAL
    jQuery('#lesson_preview').modal('show', { backdrop: 'true' });

    // SHOW AJAX RESPONSE ON REQUEST SUCCESS
    $.ajax({
        url: url,
        success: function (response) {
            jQuery('#lesson_preview .modal-body').html(response);
        }
    });
}