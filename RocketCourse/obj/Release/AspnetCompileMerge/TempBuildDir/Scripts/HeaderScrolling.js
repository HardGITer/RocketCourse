var zero = 0;
$(document).ready(function () {
    $(window).on('scroll', function () {
        $('.mainHeader').toggleClass('hide', $(window).scrollTop() > zero);
        zero = $(window).scrollTop();
    })
})