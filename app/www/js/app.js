﻿angular.module('greeneffect', [
    'ngComponentRouter',
    'ngSanitize',
    'ionic',
    'greeneffect.constant',
    'greeneffect.service.user',
    'greeneffect.controller.main',
    'greeneffect.controller.user',
    'greeneffect.common.components.geAlert'])
.run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (window.cordova && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        }
    });
})
.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider, $locationProvider, $httpProvider,$resourceProvider, Constant) {
    /*
      # Hashbang Mode
      http://www.example.com/#/aaa/
      # HTML5 Mode
      http://www.example.com/aaa/
    */
    $locationProvider.html5Mode(Constant.HTML5_MODE);
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
    $httpProvider.defaults.transformRequest = [function (data) {
        /**
         * The workhorse; converts an object to x-www-form-urlencoded serialization.
         * @param {Object} obj
         * @return {String}
         */
        var param = function (obj) {
            var query = '';
            var name, value, fullSubName, subName, subValue, innerObj, i;

            for (name in obj) {
                value = obj[name];

                if (value instanceof Array) {
                    for (i = 0; i < value.length; ++i) {
                        subValue = value[i];
                        fullSubName = name + '[' + i + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                }
                else if (value instanceof Object) {
                    for (subName in value) {
                        subValue = value[subName];
                        fullSubName = name + '[' + subName + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                }
                else if (value !== undefined && value !== null) {
                    query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
                }
            }

            return query.length ? query.substr(0, query.length - 1) : query;
        };

        return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
    }];
    $httpProvider.interceptors.push(['$q', '$injector', '$timeout', function ($q, $injector, $timeout) {
        return {
            'request': function (request) {
                request.timeout = Constant.HTTP_PROVIDER_SETTINGS.TIMEOUT;
                if (angular.isUndefined(request.retryCount)) {
                    request.retryCount = 0;
                }
                request.startDate = 0;
                request.endDate = 0;
                request.startDate = new Date().getTime();
                return request;
            },
            'response': function (response) {
                response.config.endDate = new Date().getTime();
                writeResponseLog(response);
                if (angular.isDefined(response.data.systemerror)) {
                    // systemerrorが存在した場合はステータス200でもエラー扱い
                    if (Constant.HTTP_PROVIDER_SETTINGS.RETRY_COUNT <= response.config.retryCount) {
                        // リトライ回数の上限に達している
                        response.statusText = angular.toJson(response.data.systemerror);
                        return $q.reject(response);
                    }
                    response.config.retryCount++;
                    return $timeout(function () {
                        var $http = $injector.get('$http');
                        return $http(response.config);
                    }, Constant.HTTP_PROVIDER_SETTINGS.RETRY_INTERVAL);
                }
                return response;
            },
            'responseError': function (rejection) {
                rejection.config.endDate = new Date().getTime();
                writeResponseLog(rejection);
                if (canRetry(rejection)) {
                    rejection.config.retryCount++;
                    return $timeout(function () {
                        var $http = $injector.get('$http');
                        return $http(rejection.config);
                    }, Constant.HTTP_PROVIDER_SETTINGS.RETRY_INTERVAL);
                }
                return $q.reject(rejection);
            }
        }
    }]);

    var canRetry = function (rejection) {
        if (rejection.status === -1) {
            return canRetryUnknownError(rejection);
        }
        return canRetryAnotherError(rejection);
    };

    var canRetryUnknownError = function (rejection) {
        if (Constant.HTTP_PROVIDER_SETTINGS.TIMEOUT <= (rejection.config.endDate - rejection.config.startDate)) {
            rejection.statusText = 'timeout';
            return false;
        }
        rejection.status = Constant.OFFLINE_STATUS;
        rejection.statusText = 'offLine';
        if (Constant.HTTP_PROVIDER_SETTINGS.RETRY_COUNT <= rejection.config.retryCount) {
            return false;
        }
        return true;
    };

    var canRetryAnotherError = function (rejection) {
        if (rejection.status === 408) {
            return false;
        }
        if (Constant.HTTP_PROVIDER_SETTINGS.RETRY_COUNT <= rejection.config.retryCount) {
            return false;
        }
        return true;
    };

    var writeResponseLog = function (response) {
        console.log(response.config.url + ':' + (response.config.endDate - response.config.startDate) + 'ms');
    };
    $ionicConfigProvider.navBar.alignTitle('left');
    $ionicConfigProvider.backButton.text('').previousTitleText('');
    $stateProvider

     .state('login', {
         url: "/login",
         templateUrl: "components/user/login.html"
     })

    //.state('register', {
    //    url: "/register",
    //    templateUrl: "templates/register.html"
    //})

    //.state('app', {
    //    url: "/app",
    //    abstract: true,
    //    templateUrl: "templates/menu.html",
    //    controller: 'AppCtrl'
    //})

    //.state('app.home', {
    //    url: "/home",
    //    views: {
    //        'menuContent': {
    //            templateUrl: "templates/home.html"
    //        }
    //    }
    //})
    // .state('app.notification', {
    //     url: "/notification",
    //     views: {
    //         'menuContent': {
    //             templateUrl: "templates/notification.html"
    //         }
    //     }
    // })

    // .state('app.category', {
    //     url: "/category",
    //     views: {
    //         'menuContent': {
    //             templateUrl: "templates/category.html"
    //         }
    //     }
    // })
    // .state('app.detail', {
    //     url: "/detail",
    //     views: {
    //         'menuContent': {
    //             templateUrl: "templates/detail.html"
    //         }
    //     }
    // })
    //  .state('app.contact', {
    //      url: "/contact",
    //      views: {
    //          'menuContent': {
    //              templateUrl: "templates/contact.html"
    //          }
    //      }
    //  })
    ;
    $urlRouterProvider.otherwise('login');
});


