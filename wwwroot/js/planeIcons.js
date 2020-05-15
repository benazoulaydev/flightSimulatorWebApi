var map = L.map('map').invalidateSize();

var blackIcon = L.icon({
    iconUrl: 'lib/images/flight-icon-no-rotate.jpg',
    iconSize: [38, 38], // size of the icon
    iconAnchor: [22, 94], // point of the icon which will correspond to marker's location
    popupAnchor: [-19, -38] // point from which the popup should open relative to the iconAnchor
});
L.marker([51.5, -0.09], { icon: blackIcon }).addTo(map);

var LeafIcon = L.Icon.extend({
    options: {
        iconSize: [38, 95],
        iconAnchor: [22, 94],
        popupAnchor: [-3, -76]
    }
});