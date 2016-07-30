(function () {
    'use strict';
    angular.module('greeneffect.controller.user', [])

    .controller('LoginCtrl', function ($scope, $ionicModal, $timeout) {

        $scope.alertMsg = '';
        $scope.alertType = 'warning';
        $scope.displayAlert = false;
        // Form data for the login modal
        //$scope.loginData = {};

        //// Create the login modal that we will use later
        $ionicModal.fromTemplateUrl('templates/menumodal.html', {
            scope: $scope
        }).then(function (modal) {
            $scope.modal = modal;
        });

        // Perform the login action when the user submits the login form
        $scope.doLogin = function () {
            if (angular.isUndefined($scope.loginData) || angular.isUndefined($scope.loginData.username) ||
               angular.isUndefined($scope.loginData.password) || angular.equals($scope.loginData.username, '') || 
                 angular.equals($scope.loginData.password, '')) {
                $scope.displayAlert = true;
                $scope.alertType = 'warning';
                $scope.alertMsg = 'Hãy nhập đầy đủ tên đăng nhập và mật khẩu.'
            }
        };

        $scope.closeAlertEvent = function () {
            alert("close");
        }
    })

})();