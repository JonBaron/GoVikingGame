$(document).ready(function () {

    $(".BuildMenuClose").click(function (event) {
        
        $(event.target).parent().hide();

        return false;
    });


    $('table img').click(function (event) {


        $('.buildmenu').hide();

        var id = event.target.id;
        var tiletype = event.target.alt;

        var pos = $(event.target).position();

        pos.top = pos.top + 60;
        pos.left = pos.left + 60;
        
        var width = 0;

        $('#' + tiletype).css({
            position: "absolute",
            top: pos.top + "px",
            left: (pos.left + width) + "px"
        }).show();


        return false;
    });
});