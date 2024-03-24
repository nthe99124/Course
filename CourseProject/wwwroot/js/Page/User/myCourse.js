function getCoursesByCategoryId(category_id) {
    $('#my_courses_area').html('<div class="animated-loader"><div class="spinner-border text-secondary" role="status"></div></div>');
    $.ajax({
        type: 'POST',
        url: 'https://4user.net/home/my_courses_by_category',
        data: { category_id: category_id },
        success: function (response) {
            $('#my_courses_area').html(response);
        }
    });
}

function getCoursesBySearchString(search_string) {
    $('#my_courses_area').html('<div class="animated-loader"><div class="spinner-border text-secondary" role="status"></div></div>');
    $.ajax({
        type: 'POST',
        url: 'https://4user.net/home/my_courses_by_search_string',
        data: { search_string: search_string },
        success: function (response) {
            $('#my_courses_area').html(response);
        }
    });
}