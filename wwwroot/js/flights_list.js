var flights = [
    { flight_id: "1", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "2", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false },
    { flight_id: "3", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "4", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false },
    { flight_id: "5", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "6", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false },
    { flight_id: "7", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "8", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false },
    { flight_id: "9", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "10", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false },
    { flight_id: "11", company_name: "SwissAir", date_time: "16/05/20 17:20", longitude: 23.52, latitude: 34.21, passengers: 216, is_external: true },
    { flight_id: "12", company_name: "SwissAir", date_time: "16/05/20 17:35", longitude: 25.52, latitude: 34.71, passengers: 296, is_external: false }
];

tableHtml = "";
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
}
document.getElementById("internal_flights").innerHTML = tableHtml;