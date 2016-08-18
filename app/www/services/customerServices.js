(function () {
    'use strict';

    angular.module('greeneffect.service.customer', [
            'ngResource',
            'greeneffect.constant',
            'greeneffect.common.service.urlcreator'
    ])
        .factory('customerServices', [
            '$resource',
            '$q',
            '$exceptionHandler',
            'Constant', 
            'UrlCreatorService',
            function CustomerServices($resource, $q, $exceptionHandler, Constant, UrlCreatorService) {
                var CustomerServicesFactory = {
                    getRoutes: getRoutes,
                    getCustomers: getCustomers,
                };
                return CustomerServicesFactory;

                ///////////////////////////////////////////////

                //function getRoute() {
                //    var deferred = $q.defer();
                //    $q.all([
                //        getRoutes()
                //    ]).then(function (data) {
                //        deferred.resolve(data[0]);
                //        return data[0];
                //    }).catch(function (error) {
                //        deferred.reject(error);
                //        return error;
                //    });

                //    return deferred.promise;
                //}

                function getCustomers() {
                    //var sessionData = angular.fromJson(sessionStorage.getItem(Constant.SS_KEY.USER_INFO));
                    var body = {};
                    //body['idenUser'] = sessionData['id'];
                    body['idenUser'] = '1';

                    return $resource(UrlCreatorService.createUrl('customers', 'GetByUser'), {
                        save: {
                            transformResponse: function (data) {
                                return angular.fromJson(data);
                            }
                        }
                    }).save(body).$promise;
                }

                function getRoutes() {
                    //var sessionData = angular.fromJson(sessionStorage.getItem(Constant.SS_KEY.USER_INFO));
                    var body = {};
                    //body['idenUser'] = sessionData['id'];
                    body['idenUser'] = '1';

                    return $resource(UrlCreatorService.createUrl('route', 'GetByUser'), {
                        save: {
                            transformResponse: function (data) {
                                return angular.fromJson(data);
                            }
                        }
                    }).save(body).$promise;
                }

            }]);
})();
