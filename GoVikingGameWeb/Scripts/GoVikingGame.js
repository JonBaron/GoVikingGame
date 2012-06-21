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


  
function Train(kind) {


    var service = '/Player/Train/?kind=' + kind;

    $.ajax({
        cache: false,
        url: service,
        error: function (xhr, status, error) { console.log(xhr); console.log(status); console.log(error); },
        success: function (data, status, xhr) {

            var r = eval(data);
            
            if (r.ok===true)
            {
                console.log('Training will take ' + r.TrainingTicks + ' ticks');
                UpdateResources();
            }
            else {
                console.log(r.ErrorMessage);
            }

        }
    });

    $('.buildmenu').hide();
    
    return false;
}

function Build(kind) {


    var service = '/Player/Build/?kind=' + kind;

    
    $.ajax({
        cache: false,
        url: service,
        error: function (xhr, status, error) { console.log(xhr); console.log(status); console.log(error); },
        success: function (data, status, xhr) {

            var r = eval(data);

            if (r.Ok === true) {
                console.log('Building will take ' + r.BuildTimeTicks + ' ticks');
                UpdateResources();
            }
            else {
            }

        }
    });

    $('.buildmenu').hide();

    return false;
}

function UpdateResources() {


    var service = 'Player/Resources';


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
            $("#WorkersResources").text(r.workers);
            
            
            


        }
    });

    $('.buildmenu').hide();

    return false;
}