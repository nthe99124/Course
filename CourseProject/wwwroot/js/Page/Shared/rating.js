function toggleRatingView(course_id) {
    $('#course_info_view_' + course_id).toggle();
    $('#course_rating_view_' + course_id).toggle();
    $('#edit_rating_btn_' + course_id).toggle();
    $('#cancel_rating_btn_' + course_id).toggle();
}
function publishRating(course_id) {
    var review = $('#review_of_a_course_' + course_id).val();
    var starRating = 0;
    starRating = $('#star_rating_of_course_' + course_id).val();
    if (starRating > 0) {
        $.ajax({
            type: 'POST',
            url: 'https://4user.net/home/rate_course',
            data: { course_id: course_id, review: review, starRating: starRating },
            success: function (response) {
                location.reload();
            }
        });
    } else {

    }
}
