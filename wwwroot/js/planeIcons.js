let oldPlaneIcons = [];

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

    var selected;
    let planeIcons = [];
    for (i = 0; i < flights.length; i++) {
        planeIcons[i] = L.marker([flights[i].latitude,flights[i].longitude], {icon: blackIcon, id: i});
        planeIcons[i].on('click', function(e) {
            if (typeof selected !== 'undefined') {
                selected.target.setIcon(blackIcon);
            }
            selected = e;
            e.target.setIcon(selectedIcon);
        });
        map.addLayer(planeIcons[i]);
    }
    oldPlaneIcons = planeIcons;
}

// create a red polyline from an array of LatLng points
var latlngs = [
    [45.51, -122.68],
    [37.77, -122.43],
    [34.04, -118.2]
];
var polyline = L.polyline(latlngs, {color: 'red'}).addTo(map);
// zoom the map to the polyline
map.fitBounds(polyline.getBounds());