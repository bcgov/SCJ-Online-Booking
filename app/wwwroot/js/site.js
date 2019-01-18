// Write your JavaScript code.
$(function () {
    //Init Tooltips
    $('[data-toggle="tooltip"]').tooltip();
})


function validateCaseDate(containerId, bookingDate) {
    //set hidden fields based on the swiper selected dates
    $('#hidContainerId').val(containerId);
    $('#hidDate').val(bookingDate);

    //simulate form submit
    $('#btnSearch').trigger('click');
}
