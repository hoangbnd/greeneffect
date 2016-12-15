(function () {
    "use strict";
    angular.module("greeneffect.controller.customer",
        [
            "greeneffect.service.customer",
            "greeneffect.constant",
            "greeneffect.common.service.messagemanagement"
        ])
        .controller("LstCustomerCtrl",
            function ($scope, $ionicModal, $timeout, $location, messageManagementService, customerServices, constant, $state) {
                $scope.alertMsg = "";
                $scope.alertType = "warning";
                $scope.displayAlert = false;
                // Form data for the login modal
                //$scope.loginData = {};
                customerServices.getRoutes().then(function (data) {
                    if (!data.IsSuccessful) {
                        $scope.displayAlert = true;
                        $scope.alertType = "warning";
                        $scope.alertMsg = data.Message;

                        return;
                    }
                    $scope.routes = data.Data;
                    $scope.routeSelected = $scope.routes[0];
                    customerServices.getCustomers().then(function (dataCus) {
                        if (!dataCus.IsSuccessful) {
                            $scope.displayAlert = true;
                            $scope.alertType = "warning";
                            $scope.alertMsg = dataCus.Message;
                            return;
                        }
                        $scope.customers = dataCus.Data;
                        $scope.customers = $scope.customers.filter(function (item) {
                            if (angular.equals(item.RouteId, $scope.routeSelected.Id)) {
                                return true;
                            }
                            return false;
                        });
                        var routeInfo = {
                            currentRoute: $scope.routeSelected,
                            allRoutes: $scope.routes,
                            customers: $scope.customers
                        }
                        sessionStorage.setItem(constant.SS_KEY.ROUTE_INFO, angular.toJson(routeInfo));
                    }).catch(function (error) {
                        $scope.displayAlert = true;
                        $scope.alertType = "warning";
                        $scope.alertMsg = messageManagementService.getMessage("E001");
                        return;
                    });
                }).catch(function (error) {
                    $scope.displayAlert = true;
                    $scope.alertType = "warning";
                    $scope.alertMsg = messageManagementService.getMessage("E001");
                    return;
                });

                $scope.selectRoute = function (route) {
                    if (angular.isDefined($scope.customers) && $scope.customers != null && $scope.customers.length > 0) {
                        $scope.customers = $scope.customers.filter(function (item) {
                            if (angular.equals(item.RouteId, $scope.routeSelected.Id)) {
                                return true;
                            }
                            return false;
                        });

                        var routeInfo = {
                            currentRoute: $scope.routeSelected,
                            allRoutes: $scope.routes,
                            customers: $scope.customers
                        }
                        sessionStorage.setItem(constant.SS_KEY.ROUTE_INFO, angular.toJson(routeInfo));
                    }
                };

                $scope.closeAlertEvent = function () {
                    sessionStorage.removeItem(constant.SS_KEY.USER_INFO);
                    $state.go("login");
                };

                $scope.goToCreateOrder = function (customer) {
                    var orderInfo = {
                        customer: customer
                    }
                    sessionStorage.setItem(constant.SS_KEY.ORDER_INFO, angular.toJson(orderInfo));
                    $state.go("order.create");
                };
            })
        .controller("ViewOnMapCtrl",
            function ($scope, $ionicModal, $timeout, $location, $state,messageManagementService, customerServices, constant) {
                var routeInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.ROUTE_INFO));
                $scope.routes = routeInfo.allRoutes;
                $scope.routeSelected = routeInfo.currentRoute;
                $scope.customers = routeInfo.customers;
                customerServices.getLocations().then(function (data) {
                    if (data.IsSuccessful) {
                        $scope.locations = data.Data;
                    } else {
                        $scope.locations = [];
                    }
                });

                $scope.closeAlertEvent = function () {
                    $state.go("customer.list");
                }
            });

})();