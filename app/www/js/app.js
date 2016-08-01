angular.module('greeneffect', [
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
.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider, $locationProvider, $httpProvider, Constant) {
    /*
      # Hashbang Mode
      http://www.example.com/#/aaa/
      # HTML5 Mode
      http://www.example.com/aaa/
    */
    $locationProvider.html5Mode(Constant.HTML5_MODE);

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

//.run(function ($ionicPlatform, $rootScope, $ionicPopup, $ionicModal, $timeout, $location) {
//    $ionicPlatform.ready(function () {
//        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
//        // for form inputs)
//        if (window.cordova && window.cordova.plugins.Keyboard) {
//            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
//        }
//        if (window.StatusBar) {
//            // org.apache.cordova.statusbar required
//            StatusBar.styleDefault();
//        }
//    });

//    $rootScope.list = [{ id: "1", img: "img/l1.png", title: "BUSTOP Brand Identity" }, { id: "2", img: "img/l6.png", title: "Washing Modern Car employee" }, { id: "3", img: "img/l3.png", title: "Designer Jobs available on Indeed" },
//	{ id: "1", img: "img/l4.png", title: "BUSTOP Brand Identity" }, { id: "2", img: "img/l2.png", title: "The emoji redesign project" }, { id: "3", img: "img/l5.png", title: "Designer Jobs available on Indeed" }, { id: "4", img: "img/l1.png", title: "The emoji redesign project" }]

//    $rootScope.cat = [{ id: "1", title: "Designs and arts", number: "215", checked: "true" }, { id: "2", title: "Handcrafts", number: "84" }, { id: "3", title: "Accounts", number: "43", checked: "true" }, { id: "4", title: "Medical jobs", number: "659", checked: "true" },
//	{ id: "5", title: "Programming", number: "521" }, { id: "6", title: "Communications", number: "145", checked: "true" }, { id: "7", title: "Public relations", number: "2" }, { id: "8", title: "House work", number: "0" }]

//    $rootScope.notificate = [{ id: "1", title: "Service Technician, Refrigeration and Appliance Repair", time: "5 minutes ago" },
//    { id: "2", title: "Regional Sales Director - Central", time: "1 hour ago" }, { id: "3", title: "Sales Manager needed", time: "June 04" }, { id: "4", title: "Restaurant General Manager - Assistant Manager - Miami", time: "June 06" }
//    , { id: "5", title: "Software Project Manager - Mobile and Web Apps", time: "October 22" }]

//    $rootScope.active_colorthem = 4;
//    $rootScope.color_them = function (index) {
//        $rootScope.active_colorthem = index
//    }

//    $rootScope.goto = function (url) {
//        $location.path(url)
//    }

//    $rootScope.active_icon = false
//    $rootScope.like_this = function () {
//        if ($rootScope.active_icon) {
//            $rootScope.active_icon = false
//        } else {
//            $rootScope.active_icon = true
//        }
//    }

//    $rootScope.forget_password = function () {
//        $ionicPopup.show({
//            template: 'Enter your email address below.<label class="item item-input" style="  height: 34px; margin-top: 10px;"><input  type="email"  /></label>',
//            title: 'Forget Password',
//            subTitle: ' ',
//            scope: $rootScope,
//            buttons: [
//            {
//                text: '<b>Send</b>',
//                type: 'button-positive'
//            },
//            {
//                text: 'Cancel',
//                type: 'button-positive main_bg'
//            }, ]
//        });
//    };


//    /*************************************setting_modal.html******************/
//    $ionicModal.fromTemplateUrl('templates/setting_modal.html', function (modal) {
//        $rootScope.setting_modal = modal;
//    }, {
//        scope: $rootScope,
//        animation: 'slide-in-up'
//    });

//    $rootScope.opensetting_modal = function () {
//        $rootScope.setting_modal.show();
//    };

//    $rootScope.closesetting_modal = function () {
//        $rootScope.setting_modal.hide();
//    };
//    $rootScope.$on('$destroy', function () {
//        $rootScope.setting_modal.remove();
//    });
//    $rootScope.$on('modal.hidden', function () {
//        // Execute action
//    });
//    /*************************************setting_modal.html******************/
//    /*************************************welcome_msg.html******************/
//    $ionicModal.fromTemplateUrl('templates/welcome.html', function (modal) {
//        $rootScope.welcome = modal;
//    }, {
//        scope: $rootScope,
//    });

//    $rootScope.openwelcome = function () {
//        $timeout(function () {
//            $rootScope.welcome.show();
//        }, 1000);
//    };

//    $rootScope.closewelcome = function () {
//        $rootScope.welcome.hide();
//    };
//    $rootScope.$on('$destroy', function () {
//        $rootScope.welcome.remove();
//    });
//    $rootScope.$on('modal.hidden', function () {
//        // Execute action
//    });
//    /*************************************welcome_msg.html******************/
//    /*************************************search_modal.html******************/
//    $ionicModal.fromTemplateUrl('templates/search_modal.html', function (modal) {
//        $rootScope.search_modal = modal;
//    }, {
//        scope: $rootScope,
//        animation: 'slide-in-up'
//    });

//    $rootScope.opensearch_modal = function () {
//        $rootScope.search_modal.show();
//    };

//    $rootScope.closesearch_modal = function () {
//        $rootScope.search_modal.hide();
//    };
//    $rootScope.$on('$destroy', function () {
//        $rootScope.search_modal.remove();
//    });
//    $rootScope.$on('modal.hidden', function () {
//        // Execute action
//    });
//    /*************************************search_modal.html******************/
//    /*************************************menu_modal.html******************/
//    $ionicModal.fromTemplateUrl('templates/menumodal.html', function (modal) {
//        $rootScope.menumodal = modal;
//    }, {
//        scope: $rootScope,
//        animation: 'slide-in-up'
//    });

//    $rootScope.openmenumodal = function () {
//        $rootScope.menumodal.show();
//    };


//    $rootScope.closemenumodal = function () {
//        $rootScope.menumodal.hide();
//    };
//    $rootScope.$on('$destroy', function () {
//        $rootScope.menumodal.remove();
//    });
//    $rootScope.$on('modal.hidden', function () {
//        // Execute action
//    });
//    /*************************************menu_modal.html******************/
//})


