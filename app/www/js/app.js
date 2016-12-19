angular.module("greeneffect", [
    "ngComponentRouter",
    "ngSanitize",
    "ngCordova",
    "ionic",
    "angularMoment",
    "greeneffect.constant",
    "greeneffect.service.user",
    "greeneffect.service.customer",
    "greeneffect.service.product",
    "greeneffect.service.order",
    "greeneffect.controller.main",
    "greeneffect.controller.user",
    "greeneffect.controller.order",
    "greeneffect.controller.customer",
    "greeneffect.controller.product",
    "greeneffect.controller.common",
    "greeneffect.common.components.geAlert",
    "greeneffect.common.components.geMap",
    "greeneffect.common.service.messagemanagement",
    "greeneffect.controller.message"])
.run(function ($ionicPlatform, $ionicPopup, $cordovaNetwork, amMoment, $window ) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)

        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        }

        if ($cordovaNetwork.isOffline()) {
            $ionicPopup.confirm({
                    title: "Mất kết nối internet",
                    content: "Thiết bị của bạn không được kết nối internet. Vui lòng kiểm tra lại."
                })
                .then(function(result) {
                    if (!result) {
                        ionic.platform.exitapp();
                    }
                });
        } else {
           
        }
        amMoment.changeLocale('vi');
    });

    $window.addEventListener("offline", function () {
       alert("Vui long kiểm tra lại kết nối internet.");
    }, false);

    $window.addEventListener("online", function () {
        //loadScript("http://maps.google.com/maps/api/js?key=AIzaSyCPjIZpNGuzSxOiBa9UhW5cS3qBnNIQXe0",
        //    function() {
        //        loadScript("js/infobox.js");
        //        loadScript("js/markerwithlabel_packed.js");
        //        loadScript("js/markerclusterer_packed.js");
        //    });
    }, false);

    function loadScript(url, callback) {
        // Adding the script tag to the head as suggested before
        var head = document.getElementsByTagName('head')[0];
        var script = document.createElement('script');
        script.type = 'text/javascript';
        script.src = url;

        // Then bind the event to the callback function.
        // There are several events for cross browser compatibility.
        script.onreadystatechange = callback;
        script.onload = callback;

        // Fire the loading
        head.appendChild(script);
    }
})
.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider, $locationProvider, $httpProvider, $resourceProvider, constant, $compileProvider) {
    /*
      # Hashbang Mode
      http://www.example.com/#/aaa/
      # HTML5 Mode
      http://www.example.com/aaa/
    */
    var canRetryUnknownError;
    var canRetryAnotherError;
    var writeResponseLog;
    var canRetry;

    $locationProvider.html5Mode(constant.HTML5_MODE);
    delete $httpProvider.defaults.headers["X-Requested-With"];
    $httpProvider.defaults.headers["Content-Type"] = "application/x-www-form-urlencoded;charset=utf-8";
    $httpProvider.defaults.headers["Accept"] = "application/x-www-form-urlencoded, application/json, text/javascript";
    $httpProvider.defaults.headers["Access-Control-Allow-Methods"] = "GET, PUT, POST, DELETE, OPTION";
    $httpProvider.defaults.headers["Access-Control-Allow-Origin"] = "*";
    $httpProvider.defaults.headers["Access-Control-Allow-Headers"] = "Origin, X-Requested-With, Content-Type, Accept, Authorization, X-File-Name";
    $httpProvider.defaults.useXDomain = true;
    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|ftp|file|cdvfile):|data:image\//);
    
    $httpProvider.interceptors.push(["$q", "$injector", "$timeout", function ($q, $injector, $timeout) {
        return {
            "request": function (request) {
                request.timeout = constant.HTTP_PROVIDER_SETTINGS.TIMEOUT;
                if (angular.isUndefined(request.retryCount)) {
                    request.retryCount = 0;
                }
                request.startDate = 0;
                request.endDate = 0;
                request.startDate = new Date().getTime();
                return request;
            },
            "response": function (response) {

                response.config.endDate = new Date().getTime();
                writeResponseLog(response);
                if (angular.isDefined(response.data.systemerror)) {
                    if (constant.HTTP_PROVIDER_SETTINGS.RETRY_COUNT <= response.config.retryCount) {
                        response.statusText = angular.toJson(response.data.systemerror);
                        return $q.reject(response);
                    }
                    response.config.retryCount++;
                    return $timeout(function () {
                        var $http = $injector.get("$http");
                        return $http(response.config);
                    }, constant.HTTP_PROVIDER_SETTINGS.RETRY_INTERVAL);
                }
                return response;
            },
            "responseError": function (rejection) {
                rejection.config.endDate = new Date().getTime();
                writeResponseLog(rejection);
                if (canRetry(rejection)) {
                    rejection.config.retryCount++;
                    return $timeout(function () {
                        var $http = $injector.get("$http");
                        return $http(rejection.config);
                    }, constant.HTTP_PROVIDER_SETTINGS.RETRY_INTERVAL);
                }
                return $q.reject(rejection);
            }
        }
    }]);

    canRetry = function (rejection) {
        if (rejection.status === -1) {
            return canRetryUnknownError(rejection);
        }
        return canRetryAnotherError(rejection);
    };
    canRetryUnknownError = function (rejection) {
        if (constant.HTTP_PROVIDER_SETTINGS.TIMEOUT <= (rejection.config.endDate - rejection.config.startDate)) {
            rejection.statusText = "timeout";
            return false;
        }
        rejection.status = constant.OFFLINE_STATUS;
        rejection.statusText = "offLine";
        if (constant.HTTP_PROVIDER_SETTINGS.RETRY_COUNT <= rejection.config.retryCount) {
            return false;
        }
        return true;
    };
    canRetryAnotherError = function (rejection) {
        if (rejection.status === 408) {
            return false;
        }
        if (constant.HTTP_PROVIDER_SETTINGS.RETRY_COUNT <= rejection.config.retryCount) {
            return false;
        }
        return true;
    };
    writeResponseLog = function (response) {
        console.log(response.config.url + ":" + (response.config.endDate - response.config.startDate) + "ms");
    };
    $ionicConfigProvider.navBar.alignTitle("left");
    $ionicConfigProvider.backButton.text("").previousTitleText("");
    $stateProvider
        .state("login",
        {
            url: "/login",
            templateUrl: "screens/user/login.html",
            controller: "LoginCtrl"
        })

        .state("customer",
        {
            url: "/customer",
            abstract: true,
            templateUrl: "screens/customer/menu.html",
            controller: "CommonCtrl"
        })
        .state("customer.list",
        {
            url: "/list",
            views: {
                "menuContent": {
                    templateUrl: "screens/customer/customerList.html"
                }
            }
        })
        .state("customer.map",
        {
            url: "/map",
            views: {
                "menuContent": {
                    templateUrl: "screens/customer/viewOnMap.html"
                }
            }
        })

        .state("order",
        {
            url: "/order",
            abstract: true,
            templateUrl: "screens/order/menu.html",
            controller: "CommonCtrl"
        })
        .state("order.create",
        {
            url: "/create",
            views: {
                "menuContent": {
                    templateUrl: "screens/order/createOrder.html"
                }
            }
        })

        .state("message",
        {
            url: "/message",
            abstract: true,
            templateUrl: "screens/message/menu.html",
            controller: "CommonCtrl"
        })
        .state("message.send",
        {
            url: "/send",
            views: {
                "menuContent": {
                    templateUrl: "screens/message/message.html"
                }
            }
            
        })
        .state("message.list",
        {
            url: "/list",
            views: {
                "menuContent": {
                    templateUrl: "screens/message/listNotificate.html"
                }
            }
        });

    $urlRouterProvider.otherwise("login");

});
