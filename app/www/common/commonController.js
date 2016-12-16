(function () {
    "use strict";
    angular.module("greeneffect.controller.common",
        [
            "greeneffect.constant",
            "greeneffect.common.service.messagemanagement"
        ])
        
        .controller("CommonCtrl",
        function ($scope, $ionicModal, $state, constant) {
            var userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
            $scope.username = userInfo.username;

            $ionicModal.fromTemplateUrl('common/components/geMenu/geMenu.html', function (modal) {
                $scope.menumodal = modal;
            }, {
                scope: $scope,
                animation: 'slide-in-up'
            });
            $scope.openmenumodal = function () {
                $scope.menumodal.show();
            };


            $scope.closemenumodal = function () {
                $scope.menumodal.hide();
            };
            $scope.$on('$destroy', function () {
                $scope.menumodal.remove();
            });
            $scope.$on('modal.hidden', function () {
                // Execute action
            });

            $scope.signOut = function () {
                sessionStorage.removeItem(constant.SS_KEY.USER_INFO);
                $scope.menumodal.hide();
                $state.go("login");

            }

            
        });
})();