var map;
var markers = [];
var lastinfowindow;
var locIndex;
var defaultLat = -6.258092209473512;
var defaultLng = 106.79673067700197;

 $(document).ready(function() {
     $.post(window.addressLink, { lat: defaultLat, logt: defaultLng }, function (data) {
            if (!data || data.data.length == 0) {
                $("#regionMap").remove();
                return;
            }
            //innitSelect2();
            //initialize(data);
        })
})

if (!Array.prototype.forEach) {
    Array.prototype.forEach = function (fn, scope) {
        for (var i = 0, len = this.length; i < len; ++i) {
            fn.call(scope, this[i], i, this);
        }
    }
}
function initialize() {
    var latlng = new google.maps.LatLng(-6.258092209473512, 106.79673067700197);
    var myOptions = {
        zoom: 13,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
    geocoder = new google.maps.Geocoder();


    setupEvents();
    centerChanged();
}