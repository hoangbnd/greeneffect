(function () {
    "use strict";
    angular.module("greeneffect.controller.common",
        [
            "greeneffect.constant",
            "greeneffect.common.service.messagemanagement",
            "greeneffect.service.message"
        ])

        .controller("CommonCtrl",
        function ($scope, $ionicModal, $state, $interval, constant, messageServices) {
            $scope.newNotice = 0;
            var userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
            $scope.username = userInfo.username;

            $ionicModal.fromTemplateUrl('common/components/geMenu/geMenu.html', function (modal) {
                $scope.menumodal = modal;
            }, {
                scope: $scope,
                animation: 'slide-in-up',
                backdropClickToClose: true
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

            $interval(getNotification, constant.NOTICE_RETRY_INTERVAL);

            function getNotification() {
                //messageServices.getNewNotice().then(function (response) {
                //    if (response.IsSuccessful) {
                //        $scope.newNotice = response.Data;
                //        if ($scope.newNotice > 0 && !$scope.menumodal.isShown()) {
                //            $scope.menumodal.show();
                //        }
                //    } else {
                //        $scope.newNotice = 0;
                //    }
                //});

                $scope.newNotice = Math.floor(Math.random() * 10) + 1;
            }

        });
})();