﻿(function () {
    'use strict';
    angular.module('greeneffect.controller.customer', [
        'greeneffect.service.customer',
        'greeneffect.constant',
        'greeneffect.common.service.messagemanagement'
    ])

    .controller('LstCustomerCtrl', function ($scope, $ionicModal, $timeout, $location, MessageManagementService, customerServices, Constant) {
        $scope.allCustomer;
        $scope.alertMsg = '';
        $scope.alertType = 'warning';
        $scope.displayAlert = false;
        // Form data for the login modal
        //$scope.loginData = {};
        customerServices.getRoutes().then(function (data) {
            if (!data.IsSuccessful) {
                $scope.displayAlert = true;
                $scope.alertType = 'warning';
                $scope.alertMsg = data.Messenger;
                return;
            }
            $scope.routes = data.Data;
            $scope.routeSelected = $scope.routes[0];

            customerServices.getCustomers().then(function (dataCus) {
                if (!dataCus.IsSuccessful) {
                    $scope.displayAlert = true;
                    $scope.alertType = 'warning';
                    $scope.alertMsg = dataCus.Messenger;
                    return;
                }
                $scope.allCustomer = dataCus.Data;
                $scope.customers = $scope.allCustomer.filter(function (item) {
                    if (angular.equals(item.IdenRoute, $scope.routeSelected.IdenRoute)) {
                        return true;
                    }
                    return false;
                });
                var routeInfo = {
                    currentRoute: $scope.routeSelected,
                    allRoutes: $scope.routes,
                    allCustomer: $scope.allCustomer
                }
                sessionStorage.setItem(Constant.SS_KEY.ROUTE_INFO, angular.toJson(routeInfo));
            }).catch(function (error) {
                $scope.displayAlert = true;
                $scope.alertType = 'warning';
                $scope.alertMsg = MessageManagementService.getMessage('E001');
                return;
            });
        }).catch(function (error) {
            $scope.displayAlert = true;
            $scope.alertType = 'warning';
            $scope.alertMsg = MessageManagementService.getMessage('E001');
            return;
        });

        $scope.closeAlertEvent = function () {

        };

        $scope.selectRoute = function (route) {
            if (angular.isDefined($scope.allCustomer) && $scope.allCustomer != null && $scope.allCustomer.length > 0) {
                $scope.customers = $scope.allCustomer.filter(function (item) {
                    if (angular.equals(item.IdenRoute, $scope.routeSelected.IdenRoute)) {
                        return true;
                    }
                    return false;
                });

                var routeInfo = {
                    currentRoute: $scope.routeSelected,
                    allRoutes: $scope.routes,
                    allCustomer: $scope.allCustomer
                }
                sessionStorage.setItem(Constant.SS_KEY.ROUTE_INFO, angular.toJson(routeInfo));
            }
        };

        $scope.goToCreateOrder = function (customer) {
            var orderInfo = {
                currentCustomer: customer
            }
            sessionStorage.setItem(Constant.SS_KEY.ORDER_INFO, angular.toJson(orderInfo));
        };
    })
    .controller('ViewOnMapCtrl', function ($scope, $ionicModal, $timeout, $location, MessageManagementService, customerServices, Constant) {
        var routeInfo = angular.fromJson(sessionStorage.getItem(Constant.SS_KEY.ROUTE_INFO));
        $scope.routes = routeInfo.allRoutes;
        $scope.routeSelected = routeInfo.currentRoute;
        $scope.allCustomer = routeInfo.allCustomer;

        $scope.locations = [
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.87, 2.29, "property-detail.html", "img/properties/property-01.jpg", "img/property-types/apartment.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.866876, 2.309639, "property-detail.html", "img/properties/property-02.jpg", "img/property-types/apartment.png"],

    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.874796, 2.299275, "property-detail.html", "img/properties/property-03.jpg", "img/property-types/construction-site.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.864194, 2.288868, "property-detail.html", "img/properties/property-04.jpg", "img/property-types/cottage.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.881187, 2.276938, "property-detail.html", "img/properties/property-06.jpg", "img/property-types/garage.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.859098, 2.423515, "property-detail.html", "img/properties/property-08.jpg", "img/property-types/houseboat.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.872296, 2.287796, "property-detail.html", "img/properties/property-07.jpg", "img/property-types/land.png"],

    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.874457, 2.254386, "property-detail.html", "img/properties/property-09.jpg", "img/property-types/single-family.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.875191, 2.252412, "property-detail.html", "img/properties/property-10.jpg", "img/property-types/villa.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.864352, 2.257218, "property-detail.html", "img/properties/property-11.jpg", "img/property-types/vineyard.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.858648, 2.273526, "property-detail.html", "img/properties/property-12.jpg", "img/property-types/warehouse.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.856277, 2.264256, "property-detail.html", "img/properties/property-13.jpg", "img/property-types/industrial-site.png"],

    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.859988, 2.252991, "property-detail.html", "img/properties/property-01.jpg", "img/property-types/apartment.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.856954, 2.283912, "property-detail.html", "img/properties/property-05.jpg", "img/property-types/condominium.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.879268, 2.270672, "property-detail.html", "img/properties/property-06.jpg", "img/property-types/construction-site.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.875925, 2.3239098, "property-detail.html", "img/properties/property-03.jpg", "img/property-types/cottage.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.870393, 2.327771, "property-detail.html", "img/properties/property-04.jpg", "img/property-types/houseboat.png"],

    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.880328, 2.307258, "property-detail.html", "img/properties/property-08.jpg", "img/property-types/land.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.880284, 2.306721, "property-detail.html", "img/properties/property-09.jpg", "img/property-types/single-family.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.860342, 2.304597, "property-detail.html", "img/properties/property-10.jpg", "img/property-types/vineyard.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.852549, 2.329574, "property-detail.html", "img/properties/property-11.jpg", "img/property-types/warehouse.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.857124, 2.316699, "property-detail.html", "img/properties/property-12.jpg", "img/property-types/empty.png"],

    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.869433, 2.315068, "property-detail.html", "img/properties/property-13.jpg", "img/property-types/apartment.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.885916, 2.297130, "property-detail.html", "img/properties/property-01.jpg", "img/property-types/industrial-site.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.893534, 2.276616, "property-detail.html", "img/properties/property-02.jpg", "img/property-types/construction-site.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.872570, 2.237349, "property-detail.html", "img/properties/property-03.jpg", "img/property-types/cottage.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.879344, 2.226191, "property-detail.html", "img/properties/property-04.jpg", "img/property-types/garage.png"],
     /*
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.860374, 2.222242, "property-detail.html", "img/properties/property-05.jpg", "img/property-types/condominium.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.845917, 2.265673, "property-detail.html", "img/properties/property-06.jpg", "img/property-types/cottage.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.843092, 2.306013, "property-detail.html", "img/properties/property-07.jpg", "img/property-types/warehouse.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.887697, 2.332277, "property-detail.html", "img/properties/property-08.jpg", "img/property-types/apartment.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.871441, 2.347555, "property-detail.html", "img/properties/property-07.jpg", "img/property-types/empty.png"],

    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.832438, 2.369270, "property-detail.html", "img/properties/property-09.jpg", "img/property-types/apartment.png"],
    ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.803954, 2.275200, "property-detail.html", "img/properties/property-10.jpg", "img/property-types/apartment.png"],
    ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.879183, 2.252133, "property-detail.html", "img/properties/property-11.jpg", "img/property-types/construction-site.png"],

     ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.845092, 2.187996, "property-detail.html", "img/properties/property-06.jpg", "img/property-types/cottage.png"],
     ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.909218, 2.179756, "property-detail.html", "img/properties/property-07.jpg", "img/property-types/single-family.png"],

     ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.910120, 2.352104, "property-detail.html", "img/properties/property-01.jpg", "img/property-types/warehouse.png"],
     ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.867681, 2.396736, "property-detail.html", "img/properties/property-05.jpg", "img/property-types/empty.png"],
     ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.826109, 2.350731, "property-detail.html", "img/properties/property-06.jpg", "img/property-types/industrial-site.png"],
     ['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.794908, 2.353477, "property-detail.html", "img/properties/property-03.jpg", "img/property-types/warehouse.png"],
     ['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.859098, 2.423515, "property-detail.html", "img/properties/property-04.jpg", "img/property-types/empty.png"]

     */
        ];
    })

})();