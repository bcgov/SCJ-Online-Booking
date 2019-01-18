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

//Test for numberic values
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    return !(charCode > 31 && (charCode < 48 || charCode > 57));
}
