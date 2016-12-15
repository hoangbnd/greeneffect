(function() {
  "use strict";

  angular
    .module("greeneffect.common.service.itemnamemanagement", ["greeneffect.constant", "greeneffect.common.service.languagemanagement", "ngResource"])
    .factory("itemNameManagementService", itemNameManagementService);

  itemNameManagementService.$inject = ["constant", "languageManagementService", "$resource", "$q"];

  function itemNameManagementService(constant, languageManagementService, $resource, $q) {
      var getItemNameProcess;
      var ItemNameManagementService = {
      getItemName : function (formId, itemId) {
        var deffered = $q.defer();
        var lang = languageManagementService.getLanguage();
        getItemNameProcess(lang, formId, itemId)
          .then(function (result) {
            deffered.resolve(result);
          });
        return deffered.promise;
      },
      setGeLabelName: function(ctrl) {
        var lang = languageManagementService.getLanguage();
        getItemNameProcess(lang, ctrl.formId, ctrl.itemId)
        .then(function (result) {
          ctrl.item = result;
        });
      }
    };
      getItemNameProcess = function (lang, formId, itemId) {
          var deffered = $q.defer();
          $resource(constant.ITEM_GET.URL).get({itemfile: jsonFile(formId, lang)}).$promise
              .then (function (result) {
                  var name = result[itemId];
                  if (angular.isUndefined(name)) {
                      deffered.resolve("");
                  }
                  deffered.resolve(name);
              })
              .catch (function (error) {
                  deffered.resolve("");
              });
          return deffered.promise;
      };
      var jsonFile = function (formId, lang) {
      return constant.ITEM_GET.JSON_FILE
        .replace("%formid%", formId)
        .replace("%lang%", constant.LANG_FILE[lang])
    };

    return ItemNameManagementService;
  }
})();
