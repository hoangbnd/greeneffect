(function () {
    'use strict';

    angular
      .module('greeneffect.common.components.geAlert', [])
      .directive('geAlert', function() {
          return {
              controller: Controller,
              templateUrl: 'common/components/geAlert/geAlert.html',
              scope: {
                  type: '=',
                  message: '=',
                  display: '='
              },
              required: {
                  type: true,
                  message: true,
                  display: true
              },
              $canActivate: $canActivate
          };
      });

    Controller.$inject = [];

    var ctrl;

    function Controller() {
        ctrl = this;
    }

    function $canActivate() {
        return true;
    }

    Controller.prototype.$onInit = function () {
        if (angular.isUndefined(ctrl.display)) {
            ctrl.display = false;
        }
    };

    Controller.prototype.close = function () {
        ctrl.display = false;
    }
})();