(function () {
    'use strict';

    angular.module('greeneffect.service.user', [
            'ngResource',
            'greeneffect.constant',
            'greeneffect.common.service.urlcreator'
    ])
        .factory('UserServices', [
            '$resource',
            '$q',
            '$exceptionHandler',
            'Constant', 
            'UrlCreatorService',
            function UserServices($resource, $q, $exceptionHandler, Constant, UrlCreatorService) {
                var UserServicesFactory = {
                    login: login
                };
                return UserServicesFactory;

                ///////////////////////////////////////////////

                function login() {
                    var deferred = $q.defer();
                    $q.all([
                        validateUser()
                    ]).then(function (data) {
                        deferred.resolve(data[0]);
                        return data[0];
                    }).catch(function (error) {
                        deferred.reject(error);
                        return error;
                    });

                    return deferred.promise;
                }

                function validateUser() {
                    var sessionData = angular.fromJson(sessionStorage.getItem(Constant.SS_KEY.USER_INFO));
                    var body = {};
                    body['username'] = sessionData['username'];
                    body['password'] = sessionData['password'];

                    return $resource(UrlCreatorService.createUrl('user', 'login'), {
                        save: {
                            transformResponse: function (data) {
                                return angular.fromJson(data);
                            }
                        }
                    }).save(body).$promise;
                }

            }]);
})();
