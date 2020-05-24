function display_flights(flights) {
    tableHtml = "";
    document.getElementById("internal_flights").innerHTML = tableHtml;
    for (i = 0; i < flights.length; i++) {
        if (!flights[i].is_external) {
            tableHtml += "<a id='" + flights[i].flight_id + "' href='#' onclick='chooseLine(this.id)' class='list-group-item list-group-item-action flex-column align-items-start'>" +
                "<div class='d-flex w-100 justify-content-between' >" +
                "<h5 class='mb-1'>" + flights[i].company_name + " " + flights[i].date_time + "</h5>" +
                "</div >" +
                "<small>(" + flights[i].longitude + ", " + flights[i].latitude + ")</small>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "</a >" +
                "<label id='" + flights[i].flight_id + "' onclick='deleteLine(this.id)'>X</label>";
        }
    }
    tableHtml += "<div class='d-flex w-100 justify-content-between'><h5 class='mb-0 text-center'>Externals</h5></div>";

    for (i = 0; i < flights.length; i++) {
        if (flights[i].is_external) {
            tableHtml += "<a id='" + flights[i].flight_id + "' href='#' onclick='chooseLine(this.id)' class='list-group-item list-group-item-action flex-column align-items-start'>" +
                "<div class='d-flex w-100 justify-content-between' >" +
                "<h5 class='mb-1'>" + flights[i].company_name + " " + flights[i].date_time + "</h5>" +
                "</div >" +
                "<small>(" + flights[i].longitude + ", " + flights[i].latitude + ")</small>" +
                "</a >";
        }
        document.getElementById("internal_flights").innerHTML = tableHtml;
    }
}

$('.file-upload').file_upload();

function allowDrop(ev) {
    ev.preventDefault();
}

function changeFillBack(ev) {
    document.getElementById("afterFill").innerHTML = '<div class="list-group overflow-auto p-3 mb-3 mb-md-0 mr-md-3 bg-light" style="max-width: 300px; max-height: 580px;" id="internal_flights" role="tablist"></div>';
    var newScript = document.createElement("script");
    newScript.src = "../js/flights_list.js";
    document.getElementById("afterFill").appendChild(newScript);
    document.getElementById("afterFill").id = "beforeFill";

}

function changeFill(ev) {
    document.getElementById("beforeFill").innerHTML = '<label for="fileInput" id="dropLabel" ondrop="drop(event); changeFillBack(event)" ondragover="changeWhenDrag(event)" ondragenter="changeWhenDrag(event)" ondragleave="changeWhenDrag(event)" ><img id="arrow" src="images/Arrrow.png" /><br />Drag the "Flight Plan"</label>';
    document.getElementById("beforeFill").id = "afterFill";
}

function changeWhenDrag(ev) {
    ev.preventDefault();
    ev.stopPropagation();
}

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    json_data = JSON.parse(JSON.stringify(data));
    changeFillBack(ev);
    post_flightplan(json_data);
}

function chooseLine(id) {
    get_flightplan(id);
}

function deleteLine(id) {
    delete_flightplan(id);
    document.getElementById("identifer").innerHTML = id + " has deleted";
    document.getElementById("start").innerHTML = "";
    document.getElementById("end").innerHTML = "";
}

function setFlightPlanBox(flightPlan, id) { // get FlightPlan object/jason and id flight
    document.getElementById("identifer").innerHTML = id + " for " + flightPlan.company_name;
    document.getElementById("start").innerHTML = "Begining at " + flightPlan.initial_location.time + " in (" +
        flightPlan.initial_location.longitude + ", " + flightPlan.initial_location.latitude + ")";
    var endTime = flightPlan.initial_location.time;
    /*for (var i = 0; flightPlan.lenght; i++) {
        endTime.AddSeconds((flightPlan.segments[i].timeSpan));
    }*/
    /*var endLongitude = flightPlan.segments[flightPlan.segments.lenght - 1].longitude;
    var endLatitude = flightPlan.segments[flightPlan.segments.lenght - 1].latitude;
    document.getElementById("end").innerHTML = "Ending at " + endTime + " in (" +
        endLongitude + ", " + endLatitude + ")";*/
}