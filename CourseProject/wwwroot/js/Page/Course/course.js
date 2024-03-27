//function get_url() {
//    var urlPrefix = 'https://4user.net/home/courses?'
//    var urlSuffix = "";
//    var slectedCategory = "";
//    var selectedPrice = "";
//    var selectedLevel = "";
//    var selectedLanguage = "";
//    var selectedRating = "";
//    var sortBy = "";
//    var searchString = "";


//    // Get selected category
//    $('.categories:checked').each(function () {
//        slectedCategory = $(this).attr('value');
//    });

//    // Get selected price
//    $('.prices:checked').each(function () {
//        selectedPrice = $(this).attr('value');
//    });

//    // Get selected difficulty Level
//    $('.level:checked').each(function () {
//        selectedLevel = $(this).attr('value');
//    });

//    // Get selected difficulty Level
//    $('.languages:checked').each(function () {
//        selectedLanguage = $(this).attr('value');
//    });

//    // Get selected rating
//    $('.ratings:checked').each(function () {
//        selectedRating = $(this).attr('value');
//    });

//    //sorting value
//    sortBy = $('#sortByValue').val();

//    //sorting value
//    searchString = $('[name=query]').val();

//    if (searchString != "") {
//        searchString = "query=" + searchString.split(' ').join('+') + '&';
//    }


//    urlSuffix = searchString + "category=" + slectedCategory + "&price=" + selectedPrice + "&level=" + selectedLevel + "&language=" + selectedLanguage + "&rating=" + selectedRating + "&sort_by=" + sortBy;
//    var url = urlPrefix + urlSuffix;
//    return url;
//}

//function filter() {
//    var url = get_url();
//    window.location.replace(url);
//}

//function toggleLayout(layout) {
//    $.ajax({
//        type: 'POST',
//        url: 'https://4user.net/home/set_layout_to_session',
//        data: {
//            layout: layout
//        },
//        success: function (response) {
//            location.reload();
//        }
//    });
//}

//function showToggle(elem, selector) {
//    $('.' + selector).slideToggle(50);
//    $('.' + selector).toggleClass("d-flex");
//    if ($(elem).text() === "Cho xem nhiều hơn") {
//        $(elem).text('Hiện ít hơn');
//    } else {
//        $(elem).text('Cho xem nhiều hơn');
//    }
//}

//$('.course-compare').click(function (e) {
//    e.preventDefault()
//    var redirect_to = $(this).attr('redirect_to');
//    window.location.replace(redirect_to);
//});

//function courseSorting($sortByValue = "") {
//    $('#sortByValue').val($sortByValue);
//    filter();
//}