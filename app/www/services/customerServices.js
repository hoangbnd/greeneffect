(function () {
    "use strict";

    angular.module("greeneffect.service.customer", [
            "ngResource",
            "greeneffect.constant",
            "greeneffect.common.service.urlcreator"
    ])
        .factory("customerServices", [
            "$resource",
            "$q",
            "$exceptionHandler",
            "constant", 
            "urlCreatorService",
            function($resource, $q, $exceptionHandler, constant, urlCreatorService) {
                var customerServicesFactory = {
                    getRoutes: getRoutes,
                    getCustomers: getCustomers,
                    getLocations: getLocations
            };
                return customerServicesFactory;

                ///////////////////////////////////////////////

                function getCustomers() {
                    var userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                    var body = {};
                    body["UserId"] = userInfo.Id;

                    return $resource(urlCreatorService.createUrl("Customer", "GetByUser"), {
                        save: {
                            transformResponse: function (data) {
                                return angular.fromJson(data);
                            }
                        }
                    }).save(body).$promise;
                }

                function getRoutes() {
                    var userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                    var body = {};
                    body["UserId"] = userInfo.Id;
                    
                    return $resource(urlCreatorService.createUrl("Route", "GetByUser"), {
                        save: {
                            transformResponse: function (data) {
                                return angular.fromJson(data);
                            }
                        }
                    }).save(body).$promise;
                }

                function getLocations() {
                    var userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                    var body = {};
                    body["UserId"] = userInfo.Id;

                    return $resource(urlCreatorService.createUrl("Location", "GetByUser"), {
                        save: {
                            transformResponse: function (data) {
                                return angular.fromJson(data);
                            }
                        }
                    }).save(body).$promise;
                }
            }]);
})();
