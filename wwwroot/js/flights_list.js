function display_flights(flights) {

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
    document.getElementById("internal_flights").innerHTML = tableHtml;

}
}