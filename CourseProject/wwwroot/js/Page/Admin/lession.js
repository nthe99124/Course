function handleShowHideContentLession(indexChap) {
    if (indexChap != null && indexChap != undefined) {
        if ($('.lesson-content') && $('.lesson-content').length && $('.lesson-content')[indexChap]) {
            // kiểm tra trạng thái ẩn hay hiện
            let lectureGroupWrapper = $('.lesson-content')[indexChap]
                lisLession = $(lectureGroupWrapper).find('.childs-lession'),
                btnAddLession = $(lectureGroupWrapper).find('.add-lession');
            if ((lisLession && lisLession.is(":visible")) || (btnAddLession && btnAddLession.is(":visible"))) {
                // đang hiển thị thì ẩn
                lisLession.hide();
                btnAddLession.hide();
                $(lectureGroupWrapper).find('.js-change-show-hide i').removeClass("fa-chevron-down").addClass("fa-chevron-right");

            }
            else {
                lisLession.show();
                btnAddLession.show();
                $(lectureGroupWrapper).find('.js-change-show-hide i').removeClass("fa-chevron-right").addClass("fa-chevron-down");
            }
        }
    }

}