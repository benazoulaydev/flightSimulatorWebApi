
//call function 4 time per sec (every 250 milisec)
setInterval(get_flight_time, 1000);

function get_flight_time() {

    var today = new Date().toISOString();
    var time = today.substring(0, today.length - 5);


    //var today = new Date();
    //var time = today.getFullYear() + "-" + String(today.getMonth()).padStart(2, "0") + "-" + String(today.getDay()).padStart(2, "0") + "T" + String(today.getHours()).padStart(2, "0") + ":" + String(today.getMinutes()).padStart(2, "0") + ":" + String(today.getSeconds()).padStart(2, "0");
    //console.log(time);
    $.ajax({
        type: "GET",
        url: "/api/Flights/?relative_to=" + time + "&sync_all", // Using our resources.json file to serve results
        dataType: 'json',
        success: function (result) {
            display_flights(result);
            display_planes_icons(result);
            // call function with parameter result
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var flights = [];
            display_flights(flights);
            display_planes_icons(flights);
        }
    });
}

function get_flightplan(id) {
    //document.getElementById("identifer").innerHTML = id;
    $.ajax({
        type: "GET",
        url: "/api/FlightPlan/" + id, // Using our resources.json file to serve results
        dataType: 'json',
        success: function (result) {
            // call function with parameter result
            setFlightPlanBox(result, id);
            drawFlightPlanLine(result, id);
        },
        // error: function (XMLHttpRequest, textStatus, errorThrown) {
        //     var flights = [];
        //     display_flights(flights);
        // }
    });
}

function post_flightplan(json) {
    $.ajax({
        type: "POST",
        url: "/api/FlightPlan", // Using our resources.json file to serve results
        dataType: 'json',
        success: function (result) {
            var a = result.data;
            // call function with parameter result
        }
    });
}

function delete_flightplan(id) {
    $.ajax({
        type: "DELETE",
        url: "/api/Flights/" + id, // Using our resources.json file to serve results
        success: function (result) {
            // call function with no parameter
        }
    });
}