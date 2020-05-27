﻿
//call function 4 time per sec (every 250 milisec)
setInterval(get_flight_time, 1000);

function get_flight_time() {

    var today = new Date();
    if ((today.getMonth() + 1) < 10) { var mounth = "0" + (today.getMonth() + 1); } else { var mounth = (today.getMonth() + 1); }
    if (today.getDate() < 10) { var day = "0" + today.getDate(); } else { var day = today.getDate(); }
    if (today.getHours() < 10) { var hour = "0" + today.getHours(); } else { var hour = today.getHours(); }
    if (today.getMinutes() < 10) { var min = "0" + today.getMinutes(); } else { var min = today.getMinutes(); }
    if (today.getSeconds() < 10) { var sec = "0" + today.getSeconds(); } else { var sec = today.getSeconds(); }
    var time = today.getFullYear() + "-" + mounth + "-" + day + "T" + hour + ":" + min + ":" + sec;

    //var today2 = new Date().toISOString();
    //var time2 = today2.substring(0, today2.length - 5);

    $.ajax({
        type: "GET",
        url: "/api/Flights?relative_to=" + time + "&sync_all", // Using our resources.json file to serve results
        dataType: 'json',
        success: function (result) {
            display_flights(result);
            display_planes_icons(result);
            // call function with parameter result
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //document.getElementById('alrt').innerHTML = '<b>Error</b>' + textStatus;
            //setTimeout(function () { document.getElementById('alrt').innerHTML = ''; }, 5000);
            //var flights = [];
            //display_flights(flights);
            //display_planes_icons(flights);
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
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            let al = document.getElementById('alrt');
            al.innerHTML = '<b>Error </b>' + XMLHttpRequest.responseJSON.status + ", " + XMLHttpRequest.responseJSON.title;
            al.style.opacity = "1.0";
            setTimeout(function () { document.getElementById('alrt').style.opacity = "0.0"; }, 5000);
        }
    });
}

function post_flightplan(flightPlan) {
    $.ajax({
        type: "POST",
        url: "/api/FlightPlan", // Using our resources.json file to serve results
        dataType: "json",
        data: flightPlan,
        success: function (result) {
            // call function with parameter result
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            let al = document.getElementById('alrt');
            al.innerHTML = '<b>Error </b>' + XMLHttpRequest.responseJSON.status + ", " + XMLHttpRequest.responseJSON.title;
            al.style.opacity = "1.0";
            setTimeout(function () { document.getElementById('alrt').style.opacity = "0.0"; }, 5000);
        }
    });
}

function delete_flightplan(id) {
    $.ajax({
        type: "DELETE",
        url: "/api/Flights/" + id, // Using our resources.json file to serve results
        success: function (result) {
            // call function with no parameter
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            let al = document.getElementById('alrt');
            al.innerHTML = '<b>Error </b>' + XMLHttpRequest.responseJSON.status + ", " + XMLHttpRequest.responseJSON.title;
            al.style.opacity = "1.0";
            setTimeout(function () { document.getElementById('alrt').style.opacity = "0.0"; }, 5000);
        }
    });
}