(function () {
    "use strict";
    angular.module("greeneffect.controller.user", [
        "greeneffect.service.user",
        "greeneffect.constant",
        "greeneffect.common.service.messagemanagement"
    ])

    .controller("LoginCtrl", function ($scope, $ionicModal, $timeout, $location, $state, userServices, messageManagementService, constant) {

        $scope.alertMsg = "";
        $scope.alertType = "warning";
        $scope.displayAlert = false;
        // Form data for the login modal
        //$scope.loginData = {};

        //// Create the login modal that we will use later
        $ionicModal.fromTemplateUrl("components/user/menumodal.html", {
            scope: $scope
        }).then(function (modal) {
            $scope.modal = modal;
        });

        // Perform the login action when the user submits the login form
        $scope.doLogin = function () {
            if (angular.isUndefined($scope.loginData) ||
                angular.isUndefined($scope.loginData.username) ||
                angular.equals($scope.loginData.username, "")) {
                $scope.displayAlert = true;
                $scope.alertType = "warning";
                $scope.alertMsg = messageManagementService.getMessage("E101", ["tên đăng nhập"]);
                return;
            }
            if (angular.isUndefined($scope.loginData.password) ||
                angular.equals($scope.loginData.password, "")) {
                $scope.displayAlert = true;
                $scope.alertType = "warning";
                $scope.alertMsg = messageManagementService.getMessage("E101", ["mật khẩu"]);
                return;
            }
            var userInfo = {
                username: $scope.loginData.username,
                password: $scope.loginData.password
            };
            sessionStorage.setItem(constant.SS_KEY.USER_INFO, angular.toJson(userInfo));
            userServices.login().then(function (data) {
                if (!data.IsSuccessful) {
                    $scope.displayAlert = true;
                    $scope.alertType = "warning";
                    $scope.alertMsg = data.Message;
                    return;
                }
                userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                userInfo.Id = data.Data.Id;
                userInfo.Op = data.Data.Op;
                userInfo.IdenObj = data.Data.IdenObj;
                userInfo.UserCode = data.Data.UserCode;
                sessionStorage.setItem(constant.SS_KEY.USER_INFO, angular.toJson(userInfo));
                //$location.path("/order/create");
                $state.go("order.create");
            }).catch(function (error) {
                $scope.displayAlert = true;
                $scope.alertType = "warning";
                $scope.alertMsg = messageManagementService.getMessage("E001");
                return;
            });
        };
        $scope.closeAlertEvent = function () {

        }
    })

})();