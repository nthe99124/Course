
function ajax_confirm_modal(delete_url, elem_id) {
    jQuery('#ajax-alert-modal').modal('show', { backdrop: 'static' });
    $("#appent_link_a").bind("click", function () {
        delete_by_ajax_calling(delete_url, elem_id);
    });
}

function delete_by_ajax_calling(delete_url, elem_id) {
    $.ajax({
        url: delete_url,
        success: function (response) {
            var response = JSON.parse(response);
            if (response.status == 'success') {
                $('#' + elem_id).fadeOut(600);
                $.NotificationApp.send("Thành công!", response.message, "top-right", "rgba(0,0,0,0.2)", "success");
            } else {
                $.NotificationApp.send("Ôi gãy!", response.message, "top-right", "rgba(0,0,0,0.2)", "error");
            }
        }
    });
}
function showRightModal(url, header) {
    // SHOWING AJAX PRELOADER IMAGE
    jQuery('#right-modal .modal-body').html('<div style="width: 100px; height: 100px; line-height: 100px; padding: 0px; text-align: center; margin-left: auto; margin-right: auto;"><div class="spinner-border text-secondary" role="status"></div></div>');
    jQuery('#right-modal .modal-title').html('...');
    // LOADING THE AJAX MODAL
    jQuery('#right-modal').modal('show', { backdrop: 'true' });

    // SHOW AJAX RESPONSE ON REQUEST SUCCESS
    $.ajax({
        url: url,
        success: function (response) {
            jQuery('#right-modal .modal-title').html(header);
            jQuery('#right-modal .modal-body').html(response);

        }
    });
}

function AIModal(url, header) {
    jQuery('#ai-modal .modal-title').html('...');
    // LOADING THE AJAX MODAL
    jQuery('#ai-modal').modal('show', { backdrop: 'true' });

    // SHOW AJAX RESPONSE ON REQUEST SUCCESS
    $.ajax({
        url: url,
        success: function (response) {
            jQuery('#ai-modal .modal-title').html(header);
            console.log($('#ai-modal .modal-body').html())
            if ($('#ai-modal .modal-body').html() == '') {
                jQuery('#ai-modal .modal-body').html(response);
            }
        }
    });
}

function set_js_flashdata(url) {
    $.ajax({
        url: url,
        success: function () { }
    });
}

function togglePriceFields(elem) {
    if ($("#" + elem).is(':checked')) {
        $('.paid-course-stuffs').slideUp();
    } else
        $('.paid-course-stuffs').slideDown();
}
function refreshServersideTable(tableId) {
    $('#' + tableId).DataTable().ajax.reload();
}


function div_add() {
    $.NotificationApp.send("Thành công!", 'Div được thêm vào cuối', "top-right", "rgba(0,0,0,0.2)", "info");

}

function div_remove() {
    $.NotificationApp.send("Thành công!", 'Div đã bị xóa ', "top-right", "rgba(0,0,0,0.2)", "error");

}

