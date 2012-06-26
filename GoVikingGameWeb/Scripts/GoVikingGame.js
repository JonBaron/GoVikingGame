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
    

});


function CloseBuildMenus() {
   
    $('.buildmenu').hide();
    
}


  
function Train(kind) {

    var service = '/Client/Train/?ImageFile=' + kind;

    $.ajax({
        cache: false,
        url: service,
        error: function (xhr, status, error) { console.log(xhr); console.log(status); console.log(error); },
        success: function (data, status, xhr) {

            var r = eval(data);
            
            if (r.ok===true)
            {
                console.log('Training will take ' + r.TrainingTicks + ' ticks');
                CloseBuildMenus();
                UpdateResources();
                return false;
            }
            else {
                console.log(r.ErrorMessage);
            }

        }
    });
    
    
    return false;
}

function Build(kind) {


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
                
            }

            return false;
        }
    });

    return false;
}

function UpdateResources() {


    var service = '/Client/Resources';


    $.ajax({
        cache: false,
        url: service,
        error: function (xhr, status, error) { console.log(xhr); console.log(status); console.log(error); },
        success: function (data, status, xhr) {

            var r = eval(data);            
            
            $("#FoodResources").text(r.food + '(' + r.foodProduction + ')');
            $("#GoldResources").text(r.gold + '(' + r.goldProduction + ')');
            $("#StoneResources").text(r.stone + '(' + r.stoneProduction + ')');
            $("#WoodResources").text(r.wood + '(' + r.woodProduction + ')');
            $("#WorkersResources").text(r.workers + "/" + r.maxWorkers);

            $("#GameNextTick").text("T:" + r.nextTick);

            return false;
        }
    });
    return false;
}