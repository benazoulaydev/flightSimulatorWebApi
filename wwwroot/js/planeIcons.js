//import {get_flightplan} from "ajaxFlight.js"
let oldPlaneIcons = [];
let selectedId = -1;
let oldLine;

var blackIcon = L.icon({
    iconUrl: 'images/flight-icon-no-rotate.png',
    iconSize: [38, 38], // size of the icon
    iconAnchor: [19, 38], // point of the icon which will correspond to marker's location
    popupAnchor: [0, -36] // point from which the popup should open relative to the iconAnchor
});
var selectedIcon = L.icon({
    iconUrl: 'images/flight-icon-no-rotate-select.png',
    iconSize: [38, 38], // size of the icon
    iconAnchor: [19, 38], // point of the icon which will correspond to marker's location
    popupAnchor: [0, -36] // point from which the popup should open relative to the iconAnchor
});

function display_planes_icons(flights) {
    for(let oldIcon of oldPlaneIcons){
        map.removeLayer(oldIcon);
    }
    let selected;
    let planeIcons = [];
    for (let i = 0; i < flights.length; i++) {
        if(flights[i].flight_id === selectedId){
            planeIcons[i] = L.marker([flights[i].latitude,flights[i].longitude], {icon: selectedIcon, id: flights[i].flight_id});
        } else{
            planeIcons[i] = L.marker([flights[i].latitude,flights[i].longitude], {icon: blackIcon, id: flights[i].flight_id});
        }
        planeIcons[i].on('click', function(e) {
            if (typeof selected !== 'undefined') {
                selected.target.setIcon(blackIcon);
            }
            selected = e;
            selectedId = this.id;
            e.target.setIcon(selectedIcon);
            get_flightplan(this.id);
        });
        map.addLayer(planeIcons[i]);
    }
    oldPlaneIcons = planeIcons;
}

function drawFlightPlanLine(flightPlan, id){
    map.removeLayer(oldLine);
    let line = [];
    for( let segment in flightPlan.segments){
        line+=[segment.longitude,segment.latitude];
    }
    // create a red polyline from an array of LatLng points
    let polyline = L.polyline(line, {color: 'red'});
    polyline.addTo(map);
    // zoom the map to the polyline
    //map.fitBounds(polyline.getBounds());
    oldLine = polyline;
}
