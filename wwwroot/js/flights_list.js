var flights = [
    { flight_id: "fsd423red", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "fshjgf423red", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false },
    { flight_id: "fsd423red", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "fshjgf423red", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false },
    { flight_id: "fsd423red", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "fshjgf423red", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false },
    { flight_id: "fsd423red", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "fshjgf423red", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false },
    { flight_id: "fsd423red", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "fshjgf423red", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false },
    { flight_id: "fsd423red", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "fshjgf423red", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false }
];

tableHtml = "";
for (i = 0; i < flights.length; i++) {
    if (!flights[i].is_external) {
        tableHtml += "<a href='#' class='list-group-item list-group-item-action flex-column align-items-start'>" +
            "<div class='d-flex w-100 justify-content-between' >" +
            "<h5 class='mb-1'>" + flights[i].company_name + " " + flights[i].date_time + "</h5>" +
            "</div >" +
            "<small>(" + flights[i].longitude + ", " + flights[i].latitude + ")</small>" +
            "</a >";
        /*tableHtml += "<a class='list-group-item list-group-item-action' id='" + flights[i].flight_id
            + "' data-toggle='list' href='#list-home' role='tab' aria-controls='home'>" +
            flights[i].company_name + " " + flights[i].date_time + "<br>(" + flights[i].longitude + ", " + flights[i].latitude + ")" + flights[i].passengers + "</a>";*/
    }
}
tableHtml += "<a href='#' class='list-group-item list-group-item-action flex-column align-items-start'>" +
    "<div class='d-flex w-100 justify-content-between' >" +
    "<h8 class='mb-1'>Externals</h8>" +
    "</div >" +
    "</a >";
for (i = 0; i < flights.length; i++) {
    if (flights[i].is_external) {
        tableHtml += "<a href='#' class='list-group-item list-group-item-action flex-column align-items-start'>" +
            "<div class='d-flex w-100 justify-content-between' >" +
            "<h5 class='mb-1'>" + flights[i].company_name + " " + flights[i].date_time + "</h5>" +
            "</div >" +
            "<small>(" + flights[i].longitude + ", " + flights[i].latitude + ")</small>" +
            "</a >";
        /*tableHtml += "<a class='list-group-item list-group-item-action' id='" + flights[i].flight_id
            + "' data-toggle='list' href='#list-home' role='tab' aria-controls='home'>" +
            flights[i].company_name + " " + flights[i].date_time + "<br>(" + flights[i].longitude + ", " + flights[i].latitude + ")" + flights[i].passengers + "</a>";*/
    }
}
document.getElementById("internal_flights").innerHTML = tableHtml;