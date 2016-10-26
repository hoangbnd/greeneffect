(function() {
  'use strict';

  angular
    .module('greeneffect.common.service.cookiemanagement', ['greeneffect.constant'])
    .factory('CookieManagementService', CookieManagementService);

  CookieManagementService.$inject = ['Constant', '$document'];

  function CookieManagementService(Constant, $document) {

    var getCookieArray = function () {
      var result = {};
      var allCookies = $document.cookie;
      if (angular.isUndefined(allCookies)) {
        return result;
      }
      var cookies = allCookies.split('; ');
      for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].split('=');
        result[cookie[0]] = cookie[1];
      }
      return result;
    };

    var isCookieEmpty = function (sysCookieVal) {
      return angular.isUndefined(sysCookieVal) ||
        sysCookieVal === null ||
        sysCookieVal === '';
    }

    var CookieManagementService = {
      getCookieValue : function (key) {
        var sysInfo = getCookieArray()[Constant.GE_COOKIE_KEY];
        var returnVal;
        if (isCookieEmpty(sysInfo)) {
          returnVal = undefined;
        } else {
          returnVal = angular.fromJson(sysInfo)[key];
        }
        return returnVal;
      },
      setCookieValue : function (key, val) {
        var sysInfo = getCookieArray()[Constant.GE_COOKIE_KEY];
        if (angular.isUndefined(sysInfo)) {
          sysInfo = {};
        }
        var putVal = angular.fromJson(sysInfo);
        putVal[key] = val;
        $document.cookie = (Constant.GE_COOKIE_KEY + '=' + angular.toJson(putVal));
      }
    };

    return CookieManagementService;
  }

})();
