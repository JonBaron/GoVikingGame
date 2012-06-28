$(document).ready(function () {

    $(".BuildMenuClose").click(function (event) {

        CloseBuildMenus();
        return false;
    });


    $('table img').click(function (event) {


        CloseBuildMenus();

        var id = event.target.id;
        $("#CurrentTileId").val(id);

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
    
    // Get info from game server
    UpdateResources();
    
    window.setInterval(function () {
        UpdateCountDownTick();
    }, 1000);


});


var CloseBuildMenus = function() {
   
    $('.buildmenu').hide();
    
}

  
var Train = function(kind) {

    var service = '/Client/Train/?Kind=' + kind;

    $.ajax({
        cache: false,
        url: service,
        error: function (xhr, status, error) { console.log(xhr); console.log(status); console.log(error); },
        success: function (data, status, xhr) {

            var r = eval(data);
            
            if (r.Ok===true)
            {
                console.log('Training will take ' + r.TrainingTicks + ' ticks');
                CloseBuildMenus();
                UpdateResources();
                
            }
            else {
                console.log(r.ErrorMessage);
            }

        }
    });
    
}

var Build = function(kind) {


    var currentTile = $("#CurrentTileId").val();
    var service = '/Client/Build/?Kind=' + kind + '&TileId=' + currentTile;
    
    $.ajax({
        cache: false,
        url: service,
        error: function (xhr, status, error) { console.log(xhr); console.log(status); console.log(error); },
        success: function (data, status, xhr) {

            var r = eval(data);

            if (r.Ok === true) {
                console.log('Building will take ' + r.BuildTimeTicks + ' ticks');
                CloseBuildMenus();
                UpdateResources();
                $("#" + r.TileId).attr("src", "../../Content/Tiles/" + r.ImageFile);
                $("#" + r.TileId).attr("alt", r);
                
            }

        }
    });

}

var gamenexttick;

var UpdateResources = function()  {

    var service = '/Client/Resources';

    $.ajax({
        cache: false,
        url: service,
        error: function (xhr, status, error) { console.log(xhr); console.log(status); console.log(error); },
        success: function (data, status, xhr) {

            var r = eval(data);            
            
            $("#FoodResources").text(r.food + ' (+' + r.foodProduction + ')');
            $("#GoldResources").text(r.gold + ' (+' + r.goldProduction + ')');
            $("#StoneResources").text(r.stone + ' (+' + r.stoneProduction + ')');
            $("#WoodResources").text(r.wood + ' (+' + r.woodProduction + ')');
            $("#WorkersResources").text(r.workers + "/" + r.maxWorkers);

            gamenexttick = new Date(msDateToJSDate(r.nextTick));


        }
    });

}


var UpdateCountDownTick = function()
{
    if (gamenexttick) {
        var now = new Date();
        var displayTime = (gamenexttick - now) / 1000;
        
        if (displayTime < 0) {
            displayTime = 0;
            UpdateResources();
        }


        $("#GameNextTick").text("T:" + displayTime + "s");
    }
}