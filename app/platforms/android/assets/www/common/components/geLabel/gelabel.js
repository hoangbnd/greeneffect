(function () {
  "use strict";

  angular
    .module("greeneffect.common.components.geLabel", ["greeneffect.common.service.itemnamemanagement"])
    .directive("geLabel", function() {
        return {
            restrict: "E",
            controller: controller,
            templateUrl: "common/components/geLabel/geLabel.html",
            bindings: {
                formId: "=",
                itemId: "="
            },
            required: {
                formId: true,
                itemId: true
            }
        }
    });

  controller.$inject = ["ItemNameManagementService"];

  var ctrl;

  function controller(itemNameManagementService) {
    console.log("geLabel Controller Constructor");
    ctrl = this;
    ctrl.service = itemNameManagementService;
  }

  controller.prototype.$onInit = function() {
    console.log("geLabel  Controller $onInit");
    ctrl.service.setgeLabelName(ctrl);
    ctrl.onInit = "Success";
  };
})();
