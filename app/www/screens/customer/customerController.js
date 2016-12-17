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
                $scope.alertType = constant.MSG_TYPE.WARNING;
                $scope.displayAlert = false;

                $scope.routes = [];
                $scope.customers = [];
                $scope.customersShow = [];
                $scope.routeSelected = null;
                $scope.showMap = false;

                customerServices.getRoutes().then(function (data) {
                    if (!data.IsSuccessful) {
                        $scope.displayAlert = true;
                        $scope.alertType = constant.MSG_TYPE.WARNING;
                        $scope.alertMsg = data.Message;

                        return;
                    }
                    $scope.routes = data.Data;
                    $scope.routeSelected = $scope.routes[0];
                    customerServices.getCustomers().then(function (dataCus) {
                        if (!dataCus.IsSuccessful) {
                            $scope.displayAlert = true;
                            $scope.alertType = constant.MSG_TYPE.WARNING;
                            $scope.alertMsg = dataCus.Message;
                            return;
                        }
                        $scope.customers = dataCus.Data;
                        $scope.customersShow = $scope.customers.filter(function (item) {
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
                        $scope.alertType = constant.MSG_TYPE.WARNING;
                        $scope.alertMsg = messageManagementService.getMessage("E001");
                        return;
                    });
                }).catch(function (error) {
                    $scope.displayAlert = true;
                    $scope.alertType = constant.MSG_TYPE.WARNING;
                    $scope.alertMsg = messageManagementService.getMessage("E001");
                    return;
                });

                $scope.selectRoute = function () {
                    if (angular.isDefined($scope.customers) && $scope.customers != null && $scope.customers.length > 0) {
                        $scope.customersShow = $scope.customers.filter(function (item) {
                            return angular.equals(item.RouteId, $scope.routeSelected.Id);
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
            function ($scope, $ionicModal, $timeout, $location, $state, messageManagementService, customerServices, constant) {
                $scope.alertMsg = "";
                $scope.alertType = constant.MSG_TYPE.WARNING;
                $scope.displayAlert = false;

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