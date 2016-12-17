(function () {
    "use strict";
    var controller;
    angular
      .module("greeneffect.common.components.geMap", [
          "ngCordova",
          "greeneffect.constant"
      ])
      .directive("geMap", function () {
          return {
              restrict: "E",
              controller: controller,
              templateUrl: "common/components/geMap/geMap.html",
              scope: {
                  locations: "=",
                  type: "="
              },
              required: {
                  locations: true
              },
              transclude: false
          };
      });
    controller = function ($scope, $cordovaGeolocation, $ionicLoading, $state, constant) {
        $scope.alertMsg = "";
        $scope.alertType = constant.MSG_TYPE.WARNING;
        $scope.displayAlert = false;

        var myLocation;

        var map = initMap();
        var newMarkers = [];
        var markerCluster;
        $scope.$watch("locations", function () {
            addMarker(map);
        });

        function initMap() {
            var mapStyles = [
                {
                    "featureType": "administrative.land_parcel", "elementType": "labels", "stylers": [{ "visibility": "off" }]
                },
                {
                    "featureType": "poi", "elementType": "labels.text", "stylers": [{ "visibility": "off" }]
                },
                {
                    "featureType": "poi.business", "stylers": [{ "visibility": "off" }]
                },
                {
                    "featureType": "poi.park", "elementType": "labels.text", "stylers": [{ "visibility": "off" }]
                },
                {
                    "featureType": "road.arterial", "elementType": "labels", "stylers": [{ "visibility": "off" }]
                },
                {
                    "featureType": "road.highway", "elementType": "labels", "stylers": [{ "visibility": "off" }]
                },
                {
                    "featureType": "road.local", "stylers": [{ "visibility": "off" }]
                },
                {
                    "featureType": "road.local", "elementType": "labels", "stylers": [{ "visibility": "off" }]
                }
            ];
            setMapHeight();
            if (document.getElementById("map") != null) {

                var map = new google.maps.Map(document.getElementById("map"), {
                    zoom: 12,
                    scrollwheel: false,
                    //center: new google.maps.LatLng(latitude, longitude),
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    mapTypeControl: true,
                    mapTypeControlOptions: {
                        style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                        position: google.maps.ControlPosition.LEFT_BOTTOM
                    },
                    styles: mapStyles
                });
                findMyLocation(map);
                // Enable Geo Location on button click
                $(".geo-location").on("click", function () {
                    if (navigator.geolocation) {
                        $("#map").addClass("fade-map");
                        findMyLocation(map);
                    } else {
                        $scope.displayAlert = true;
                        $scope.alertType = constant.MSG_TYPE.WARNING;
                        $scope.alertMsg = "Hãy chắc chắn bạn đã bật GPS";
                    }
                });
                return map;
            }
            return null;
        }

        function setMapHeight() {
            var mapHeight = $(".view-container").height() - 50;
            $("#map").height(mapHeight);
            $(window).on("resize", function () {
                $("#map").height($(".view-container").height() - 50);
            });
        }

        function addMarker(map) {
            clearOverlays();
            if (angular.isUndefined($scope.locations)) return;
            var i;
            for (i = 0; i < $scope.locations.length; i++) {
                var pictureLabel = document.createElement("img");
                pictureLabel.src = "img/property-types/single-family.png";
                var boxText = document.createElement("div");
                var infoboxOptions = {
                    content: boxText,
                    disableAutoPan: false,
                    pixelOffset: new google.maps.Size(-100, 0),
                    zIndex: null,
                    alignBottom: true,
                    boxClass: "infobox-wrapper",
                    enableEventPropagation: true,
                    closeBoxMargin: "12px 6px 0px 0px",
                    closeBoxURL: "img/close-btn.png",
                    infoBoxClearance: new google.maps.Size(1, 1)
                };
                var marker = new MarkerWithLabel({
                    title: $scope.locations[i].CustomerName,
                    position: new google.maps.LatLng($scope.locations[i].Latitude, $scope.locations[i].Longitude),
                    map: map,
                    icon: "img/marker.png",
                    labelContent: pictureLabel,
                    labelAnchor: new google.maps.Point(13, 42),
                    labelClass: "marker-style"
                });
                newMarkers.push(marker);
                boxText.innerHTML =
                    "<div class='infobox-inner'>" +
                        "<div class='infobox-description'>" +
                            "<div class='infobox-title'><a href='#/order/create'>" + $scope.locations[i].CustomerName + "</a></div>" +
                            "<div class='infobox-location'>" + $scope.locations[i].CustomerAddress + "</div>" +
                            "<div class='infobox-phone'>" + $scope.locations[i].CustomerPhone + "</div>" +
                        "</div>" +
                    "</div>";
                //Define the infobox
                newMarkers[i].infobox = new InfoBox(infoboxOptions);
                google.maps.event.addListener(marker, "click", (function (marker, i) {
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
                    url: "img/cluster.png",
                    height: 37,
                    width: 37
                }
            ];
            markerCluster = new MarkerClusterer(map, newMarkers, { styles: clusterStyles, maxZoom: 15 });
            $("body").addClass("loaded");
            setTimeout(function () {
                $("body").removeClass("has-fullscreen-map");
            }, 1000);
            $("#map").removeClass("fade-map");

            //  Dynamically show/hide markers --------------------------------------------------------------
            google.maps.event.addListener(map, "idle", function () {

                for (var i = 0; i < $scope.locations.length; i++) {
                    if (!map.getBounds()) continue;
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

           function clearOverlays() {
               var i;
               for (i = 0; i < newMarkers.length; i++) {
                   newMarkers[i].setVisible(false);
                   newMarkers[i].setMap(null);
                   markerCluster[i].setMap(null);
                }
               newMarkers.length = 0;
               newMarkers = [];
           }
        }

        function findMyLocation(map) {
            $ionicLoading.show({
                template: "<ion-spinner icon='bubbles'></ion-spinner><br/>Đang tìm vị trí!"
            });
            var posOptions = {
                enableHighAccuracy: true,
                timeout: 20000,
                maximumAge: 0
            };

            var permissions = cordova.plugins.permissions;
            permissions.hasPermission(permissions.ACCESS_FINE_LOCATION, checkPermissionCallback, null);

            function checkPermissionCallback(status) {
                
                if (!status.hasPermission) {
                    var errorCallback = function() {
                        $scope.displayAlert = true;
                        $scope.alertType = constant.MSG_TYPE.WARNING;
                        $scope.alertMsg = "Hãy chắc chắn bạn đã bật GPS.";
                    }
                    permissions.requestPermission(permissions.ACCESS_FINE_LOCATION,
                        function(status) {
                            if (!status.hasPermission) {
                                errorCallback();
                                $state.goBack();
                            } else {
                                getCurrentPosition();
                            }
                            
                        },
                        errorCallback);
                } else {
                    getCurrentPosition();
                }
            }

            function getCurrentPosition() {
                $cordovaGeolocation.getCurrentPosition(posOptions).then(function (position) {
                    var myLatLng = { lat: position.coords.latitude, lng: position.coords.longitude };
                    myLocation = new google.maps.Marker({ map: map, position: myLatLng, title: "Bạn đang ở đây" });
                    var center = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                    map.panTo(center);
                    $("#map").removeClass("fade-map");
                    $ionicLoading.hide();
                }, function (err) {
                    console.log(err);
                    $ionicLoading.hide();
                    $scope.displayAlert = true;
                    $scope.alertType = constant.MSG_TYPE.WARNING;
                    $scope.alertMsg = "Hãy chắc chắn bạn đã bật GPS.";
                });
            }
        }
    };
})();

