//$(document).ready(function () {
//    //$("#popcorn").css("display","none");
//    $("#mainicon").mouseenter(function () {
//        $("#mainicon").attr('src', '/images/pizza.png');

//    })
//    $("#mainicon").mouseleave(function () {
//        $("#mainicon").attr('src', '/images/fries.png');

//    })






//});

$(".data").keydown(function () {
    $(this).css({
        "background-color": "grey",
        "color": "white",
    })
})
$(".data").keyup(function () {
    $(this).css({
        "background-color": "white",
        "color": "black",
    })
})

$(".data").focus(function () {
    $("#milk").css("display", "inline").fadeOut(5000);
})