(function() {
  "use strict";

  angular
    .module("greeneffect.common.service.languagemanagement", ["greeneffect.constant", "greeneffect.common.service.cookiemanagement"])
    .factory("languageManagementService", languageManagementService);

  languageManagementService.$inject = ["constant", "cookieManagementService"];

  function languageManagementService(constant, cookieManagementService) {

    var LanguageManagementService = {
      getLanguage: function () {
        var lang = cookieManagementService.getCookieValue(constant.GE_USER_INFO_KEY.LANG);
        if (angular.isUndefined(lang)) {
          lang = constant.DEFAULT_LANG;
        }
        return lang;
      },
      setLanguage: function (lang) {
        cookieManagementService.setCookieValue(constant.GE_USER_INFO_KEY.LANG, lang);
        return;
      }
    };

    return LanguageManagementService;
  }

})();
