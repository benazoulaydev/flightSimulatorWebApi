//draws the plane icons

var locations = [[32,32],[33.244, 40.12],[34,34]];
var blackIcon = L.icon({
    iconUrl: 'lib/images/flight-icon-no-rotate.png',
    iconSize: [38, 38], // size of the icon
    iconAnchor: [19, 38], // point of the icon which will correspond to marker's location
    popupAnchor: [0, -36] // point from which the popup should open relative to the iconAnchor
});
var selectedIcon = L.icon({
    iconUrl: 'lib/images/flight-icon-no-rotate-select.png',
    iconSize: [38, 38], // size of the icon
    iconAnchor: [19, 38], // point of the icon which will correspond to marker's location
    popupAnchor: [0, -36] // point from which the popup should open relative to the iconAnchor
});
// for(let location of locations){
//     L.marker(location, { icon: blackIcon }).addTo(map);
// }
var selected;
for (i = 0; i < locations.length; i++) {
    let testmarker = [];
    console.log([locations[i][0],locations[i][1]]);
    testmarker[i] = L.marker([locations[i][0],locations[i][1]], {icon: blackIcon, id: i});
    testmarker[i].on('click', function(e) {
        if (typeof selected !== 'undefined') {
            selected.target.setIcon(blackIcon);
        }
        selected = e;
        e.target.setIcon(selectedIcon);
        //document.getElementById('someDiv').innerHTML = points[e.target.options.id][2];
    });
    map.addLayer(testmarker[i]);
}

// create a red polyline from an array of LatLng points
var latlngs = [
    [45.51, -122.68],
    [37.77, -122.43],
    [34.04, -118.2]
];
var polyline = L.polyline(locations, {color: 'red'}).addTo(map);
// zoom the map to the polyline
map.fitBounds(polyline.getBounds());