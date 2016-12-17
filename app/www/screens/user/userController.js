(function () {
    "use strict";
    angular.module("greeneffect.controller.user", [
        "greeneffect.service.user",
        "greeneffect.constant",
        "greeneffect.common.service.messagemanagement"
    ])

    .controller("LoginCtrl", function ($scope, $ionicModal, $timeout, $location, $state, userServices, messageManagementService, constant) {

        $scope.alertMsg = "";
        $scope.alertType = constant.MSG_TYPE.WARNING;
        $scope.displayAlert = false;

        // Perform the login action when the user submits the login form
        $scope.doLogin = function () {
            if (angular.isUndefined($scope.loginData) ||
                angular.isUndefined($scope.loginData.username) ||
                angular.equals($scope.loginData.username, "")) {
                $scope.displayAlert = true;
                $scope.alertType = constant.MSG_TYPE.WARNING;
                $scope.alertMsg = messageManagementService.getMessage("E101", ["tên đăng nhập"]);
                return;
            }
            if (angular.isUndefined($scope.loginData.password) ||
                angular.equals($scope.loginData.password, "")) {
                $scope.displayAlert = true;
                $scope.alertType = constant.MSG_TYPE.WARNING;
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
                    $scope.alertType = constant.MSG_TYPE.WARNING;
                    $scope.alertMsg = data.Message;
                    return;
                }
                userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                userInfo.Id = data.Data.Id;
                userInfo.Op = data.Data.Op;
                userInfo.IdenObj = data.Data.IdenObj;
                userInfo.UserCode = data.Data.UserCode;
                sessionStorage.setItem(constant.SS_KEY.USER_INFO, angular.toJson(userInfo));
                $state.go("customer.list");
            }).catch(function (e) {
                $scope.displayAlert = true;
                $scope.alertType = constant.MSG_TYPE.WARNING;
                console.log(e)
                if (e.statusText && e.statusText == "timeout") {
                    $scope.alertMsg = "Không có kết nối đến máy chủ. Vui lòng thử lại sau.";
                    return;
                }
                if (e.statusText && e.statusText == "offline") {
                    $scope.alertMsg = "Không có kết nối internet. Vui lòng thử lại sau.";
                    return;
                }
                $scope.displayAlert = true;
                $scope.alertType = constant.MSG_TYPE.WARNING;
                $scope.alertMsg = messageManagementService.getMessage("E001");
                return;
            });
        };
        $scope.closeAlertEvent = function () {

        }
    })

})();