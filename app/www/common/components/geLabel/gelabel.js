(function () {
  'use strict';

  angular
    .module('greeneffect.common.components.geLabel', ['greeneffect.common.service.itemnamemanagement'])
    .directive('geLabel', function() {
        return {
            controller: Controller,
            templateUrl: 'common/components/geLabel/geLabel.html',
            bindings: {
                formId: '=',
                itemId: '='
            },
            required: {
                formId: true,
                itemId: true
            }
        }
    });

  Controller.$inject = ['ItemNameManagementService'];

  var ctrl;

  function Controller(ItemNameManagementService) {
    console.log('geLabel Controller Constructor');
    ctrl = this;
    ctrl.service = ItemNameManagementService;
  }

  Controller.prototype.$onInit = function() {
    console.log('geLabel  Controller $onInit');
    ctrl.service.setgeLabelName(ctrl);
    ctrl.onInit = 'Success';
  };
})();
