(function () {
    'use strict';
    angular
      .module('greeneffect.common.components.geMap', ['ngCordova'])
      .directive('geMap', function () {
          return {
              restrict: 'E',
              controller: controller,
              templateUrl: 'common/components/geMap/geMap.html',
              scope: {
                  locations: '=',
                  type: '='
              },
              required: {
                  locations: true
              },
              transclude: true,
          };
      });

    var controller = function ($scope, $cordovaGeolocation, $ionicLoading) {
        ionic.Platform.ready(function () {
            //$ionicLoading.show({
            //    template: '<ion-spinner icon="bubbles"></ion-spinner><br/>Acquiring location!'
            //});

            var posOptions = {
                enableHighAccuracy: true,
                timeout: 20000,
                maximumAge: 0
            };

            $cordovaGeolocation.getCurrentPosition(posOptions).then(function (position) {
                var lat = position.coords.latitude;
                var long = position.coords.longitude;

                //var myLatlng = new google.maps.LatLng(lat, long);

                //var mapOptions = {
                //    center: myLatlng,
                //    zoom: 16,
                //    mapTypeId: google.maps.MapTypeId.ROADMAP
                //};

                //var map = new google.maps.Map(document.getElementById("map"), mapOptions);

                //$scope.map = map;
                createHomepageGoogleMap(lat, long);

                //$ionicLoading.hide();

            }, function (err) {
                $ionicLoading.hide();
                console.log(err);
            });
        });
        var mapStyles = [{ featureType: 'water', elementType: 'all', stylers: [{ hue: '#d7ebef' }, { saturation: -5 }, { lightness: 54 }, { visibility: 'on' }] }, { featureType: 'landscape', elementType: 'all', stylers: [{ hue: '#eceae6' }, { saturation: -49 }, { lightness: 22 }, { visibility: 'on' }] }, { featureType: 'poi.park', elementType: 'all', stylers: [{ hue: '#dddbd7' }, { saturation: -81 }, { lightness: 34 }, { visibility: 'on' }] }, { featureType: 'poi.medical', elementType: 'all', stylers: [{ hue: '#dddbd7' }, { saturation: -80 }, { lightness: -2 }, { visibility: 'on' }] }, { featureType: 'poi.school', elementType: 'all', stylers: [{ hue: '#c8c6c3' }, { saturation: -91 }, { lightness: -7 }, { visibility: 'on' }] }, { featureType: 'landscape.natural', elementType: 'all', stylers: [{ hue: '#c8c6c3' }, { saturation: -71 }, { lightness: -18 }, { visibility: 'on' }] }, { featureType: 'road.highway', elementType: 'all', stylers: [{ hue: '#dddbd7' }, { saturation: -92 }, { lightness: 60 }, { visibility: 'on' }] }, { featureType: 'poi', elementType: 'all', stylers: [{ hue: '#dddbd7' }, { saturation: -81 }, { lightness: 34 }, { visibility: 'on' }] }, { featureType: 'road.arterial', elementType: 'all', stylers: [{ hue: '#dddbd7' }, { saturation: -92 }, { lightness: 37 }, { visibility: 'on' }] }, { featureType: 'transit', elementType: 'geometry', stylers: [{ hue: '#c8c6c3' }, { saturation: 4 }, { lightness: 10 }, { visibility: 'on' }] }];

        $.ajaxSetup({
            cache: true
        });

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Google Map - Homepage
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        function createHomepageGoogleMap(_latitude, _longitude) {
            setMapHeight();
            if (document.getElementById('map') != null) {

                var map = new google.maps.Map(document.getElementById('map'), {
                    zoom: 14,
                    scrollwheel: false,
                    center: new google.maps.LatLng(_latitude, _longitude),
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    mapTypeControl: true,
                    mapTypeControlOptions: {
                        style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                        
                        position: google.maps.ControlPosition.LEFT_BOTTOM
                    },

                    styles: mapStyles
                });
                var i;
                var newMarkers = [];
                for (i = 0; i < $scope.locations.length; i++) {
                    var pictureLabel = document.createElement("img");
                    pictureLabel.src = $scope.locations[i][7];
                    var boxText = document.createElement("div");
                    var infoboxOptions = {
                        content: boxText,
                        disableAutoPan: false,
                        //maxWidth: 150,
                        pixelOffset: new google.maps.Size(-100, 0),
                        zIndex: null,
                        alignBottom: true,
                        boxClass: "infobox-wrapper",
                        enableEventPropagation: true,
                        closeBoxMargin: "0px 0px -8px 0px",
                        closeBoxURL: "img/close-btn.png",
                        infoBoxClearance: new google.maps.Size(1, 1)
                    };
                    var marker = new MarkerWithLabel({
                        title: $scope.locations[i][0],
                        position: new google.maps.LatLng($scope.locations[i][3], $scope.locations[i][4]),
                        map: map,
                        icon: 'img/marker.png',
                        labelContent: pictureLabel,
                        labelAnchor: new google.maps.Point(50, 0),
                        labelClass: "marker-style"
                    });
                    newMarkers.push(marker);
                    boxText.innerHTML =
                        '<div class="infobox-inner">' +
                            '<a href="' + $scope.locations[i][5] + '">' +
                            '<div class="infobox-image" style="position: relative">' +
                            '<img src="' + $scope.locations[i][6] + '">' + '<div><span class="infobox-price">' + $scope.locations[i][2] + '</span></div>' +
                            '</div>' +
                            '</a>' +
                            '<div class="infobox-description">' +
                            '<div class="infobox-title"><a href="' + $scope.locations[i][5] + '">' + $scope.locations[i][0] + '</a></div>' +
                            '<div class="infobox-location">' + $scope.locations[i][1] + '</div>' +
                            '</div>' +
                            '</div>';
                    //Define the infobox
                    newMarkers[i].infobox = new InfoBox(infoboxOptions);
                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            for (var h = 0; h < newMarkers.length; h++) {
                                newMarkers[h].infobox.close();
                            }
                            newMarkers[i].infobox.open(map, this);
                        }
                    })(marker, i));

                }
                var clusterStyles = [
                    {
                        url: 'img/cluster.png',
                        height: 37,
                        width: 37
                    }
                ];
                var markerCluster = new MarkerClusterer(map, newMarkers, { styles: clusterStyles, maxZoom: 15 });
                $('body').addClass('loaded');
                setTimeout(function () {
                    $('body').removeClass('has-fullscreen-map');
                }, 1000);
                $('#map').removeClass('fade-map');

                //  Dynamically show/hide markers --------------------------------------------------------------

                google.maps.event.addListener(map, 'idle', function () {

                    for (var i = 0; i < $scope.locations.length; i++) {
                        if (map.getBounds().contains(newMarkers[i].getPosition())) {
                            newMarkers[i].setVisible(true); // <- Uncomment this line to use dynamic displaying of markers

                            newMarkers[i].setMap(map);
                            markerCluster.setMap(map);
                        } else {
                            newMarkers[i].setVisible(false); // <- Uncomment this line to use dynamic displaying of markers

                            newMarkers[i].setMap(null);
                            markerCluster.setMap(null);
                        }
                    }
                });

                // Function which set marker to the user position
                function success(position) {
                    var center = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                    map.panTo(center);
                    $('#map').removeClass('fade-map');
                }

                // Enable Geo Location on button click
                $('.geo-location').on("click", function () {
                    if (navigator.geolocation) {
                        $('#map').addClass('fade-map');
                        navigator.geolocation.getCurrentPosition(success);
                    } else {
                        error('Geo Location is not supported');
                    }
                });
            }
        }

        function setMapHeight() {
            var $body = $('body');
            var mapHeight = $('.view-container').height() - 50;
            $('#map').height(mapHeight);
            $(window).on('resize', function () {
                $('#map').height($('.view-container').height() - 50);
            });
        }
    };
})();

