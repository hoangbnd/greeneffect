(function () {
    "use strict";

    angular.module("greeneffect.service.product", [
            "ngResource",
            "greeneffect.constant",
            "greeneffect.common.service.urlcreator"
    ])
        .factory("productServices", [
            "$resource",
            "$q",
            "$exceptionHandler",
            "constant",
            "urlCreatorService",
            function ($resource, $q, $exceptionHandler, constant, urlCreatorService) {
                var productServicesFactory = {
                    getProducts: getProducts
                };
                return productServicesFactory;

                ///////////////////////////////////////////////

                function getProducts(keyword, searchByGroup) {
                    
                    var body = {};
                    body["Keyword"] = keyword;
                    body["ByGroup"] = searchByGroup;
                    body["PageIndex"] = 1;

                    return $resource(urlCreatorService.createUrl("product", "GetProduct"), {
                        save: {
                            transformResponse: function (data) {
                                return angular.fromJson(data);
                            }
                        }
                    }).save(body).$promise;
                }

               
            }])
        .factory("selectedProduct", function () {
        // I know this doesn't work, but what will?
            var data = {};

            return {
                getProduct: function () {
                    return data;
                },
                setProduct: function (product) {
                    data = product;
                }
            };
    });
})();
