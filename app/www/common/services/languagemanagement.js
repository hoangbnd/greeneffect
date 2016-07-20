(function() {
  'use strict';

  angular
    .module('greeneffect.common.service.languagemanagement', ['greeneffect.constant', 'greeneffect.common.service.cookiemanagement'])
    .factory('LanguageManagementService', LanguageManagementService);

  LanguageManagementService.$inject = ['Constant', 'CookieManagementService'];

  function LanguageManagementService(Constant, CookieManagementService) {

    var LanguageManagementService = {
      getLanguage: function () {
        var lang = CookieManagementService.getCookieValue(Constant.GE_USER_INFO_KEY.LANG);
        if (angular.isUndefined(lang)) {
          lang = Constant.DEFAULT_LANG;
        }
        return lang;
      },
      setLanguage: function (lang) {
        CookieManagementService.setCookieValue(Constant.GE_USER_INFO_KEY.LANG, lang);
        return;
      }
    };

    return LanguageManagementService;
  }

})();
