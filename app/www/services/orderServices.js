(function () {
    "use strict";

    angular.module("greeneffect.service.order", [
            "ngResource",
            "greeneffect.constant",
            "greeneffect.common.service.urlcreator"
    ])
        .factory("orderServices", [
            "$resource",
            "$q",
            "$exceptionHandler",
            "constant",
            "urlCreatorService",
            function ($resource, $q, $exceptionHandler, constant, urlCreatorService) {
                var orderServicesFactory = {
                    sendOrder: sendOrder
                };
                return orderServicesFactory;

                ///////////////////////////////////////////////

                function sendOrder() {
                    var orderInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.ORDER_INFO));
                    var userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                    console.log(userInfo);
                    var body = {};
                    body["userId"] = userInfo.Id;
                    body["customerId"] = orderInfo.customer.Id;
                    body["latitude"] = orderInfo.latitude;
                    body["longitude"] = orderInfo.longitude;
                    body["orderItems"] = orderInfo.orderItems;
                    

                    return $resource(urlCreatorService.createUrl("Order", "Create"), {
                        save: {
                            transformResponse: function (data) {
                                console.log(data);
                                return angular.fromJson(data);
                            }
                        }
                    }).save(body).$promise;
                }

            }]);
})();
