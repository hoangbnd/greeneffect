(function () {
    "use strict";
    var controller;
    angular
      .module("greeneffect.common.components.geAlert", [])
      .directive("geAlert", function() {
          return {
              restrict: "E",
              controller: controller,
              templateUrl: "common/components/geAlert/geAlert.html",
              scope: {
                  ngModel: "=",
                  type: "=", //waring, info, success
                  message: "=",
                  display: "=",
                  closeEvent: "&"
              },
              required: {
                  type: true,
                  message: true,
                  display: true
              },
              transclude: true,
          };
      });
    controller = ["$scope", function ($scope) {
        $scope.close = function () {
            if (angular.isDefined($scope.closeEvent) || angular.isFunction($scope.closeEvent)) {
                $scope.closeEvent();
            }
            $scope.display = false;
        }
    }];
})();