﻿(function () {
    "use strict";
    angular.module("greeneffect.controller.order", [
        "ngCordova",
        "greeneffect.constant",
        "greeneffect.service.order",
        "greeneffect.common.service.messagemanagement",
        "greeneffect.common.service.urlcreator"])

    .controller("OrderCtrl", function ($scope, $ionicModal, $cordovaGeolocation, $http, $state, $ionicPopup,
        constant, selectedProduct, messageManagementService, urlCreatorService, orderServices) {
        $scope.alertMsg = "";
        $scope.alertType = constant.MSG_TYPE.WARNING;
        $scope.displayAlert = false;
        $scope.orderItems = [];
        var orderInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.ORDER_INFO));
        $scope.customer = orderInfo.customer;
        $ionicModal.fromTemplateUrl("screens/product/searchProduct.html", function (modal) {
            $scope.search_modal = modal;
        }, {
            scope: $scope,
            animation: "slide-in-up"
        });
        $scope.searchProduct = function () {
            $scope.search_modal.show();
        }

        $scope.$watch(function () { return selectedProduct.getProduct(); }, function (newValue, oldValue) {
            selectedProduct.setProduct(newValue);
            if ($scope.search_modal) $scope.search_modal.hide();
            var isExisted = false;

            for (var i = 0; i < $scope.orderItems.length; i++) {
                if ($scope.orderItems[i].productId === newValue.Id) {
                    isExisted = true;
                }
            }

            if (!isExisted && newValue.Id)
                $scope.orderItems.push({
                    productId: newValue.Id,
                    productName: newValue.ProductName,
                    quantity: 0
                });
        });

        $scope.takePhoto = function () {
            orderInfo.images = [];
            $ionicPopup.confirm({
                title: "Tạo đơn hàng",
                content: "Vui lòng chụp 3 ảnh để đưa vào đơn hàng."
            }).then(function (result) {
                if (result) {
                    // start image capture
                    navigator.device.capture.captureImage(captureSuccess, captureError, { limit: 3 });
                }
            });

            // capture callback
            function captureSuccess(mediaFiles) {
                var i, len;
                for (i = 0, len = mediaFiles.length; i < len; i += 1) {
                    orderInfo.images.push(mediaFiles[i].fullPath);
                }
                $scope.images = orderInfo.images;
            };

            // capture error callback
            function captureError(error) {

                $scope.displayAlert = true;
                $scope.alertType = constant.MSG_TYPE.WARNING;
                switch (error.code) {
                    case CaptureError.CAPTURE_NO_MEDIA_FILES:
                        $scope.alertMsg = "Chưa chụp ảnh nào";
                        break;
                    case CaptureError.CAPTURE_INTERNAL_ERR:
                        $scope.alertMsg = "Có lỗi trong quá trình chụp ảnh";
                        break;
                    case CaptureError.CAPTURE_APPLICATION_BUSY:
                        $scope.alertMsg = "Ứng dụng chụp ảnh đang được sử dụng bởi phần mềm khác.";
                        break;
                    case CaptureError.CAPTURE_PERMISSION_DENIED:
                        $scope.alertMsg = "Vui lòng kiểm tra lại quyền sử dụng camera của phần mềm";
                        break;
                    case CaptureError.CAPTURE_NOT_SUPPORTED:
                        $scope.alertMsg = "Thiết bị không được hỗ trợ chụp ảnh";
                        break;
                }

                return;
            };

        }

        $scope.sendOrder = function () {

            orderInfo.orderItems = $scope.orderItems;
            var posOptions = {
                enableHighAccuracy: true,
                timeout: 20000,
                maximumAge: 0
            };
            $cordovaGeolocation.getCurrentPosition(posOptions).then(function (position) {
                orderInfo.latitude = position.coords.latitude;
                orderInfo.longitude = position.coords.longitude;
                sessionStorage.setItem(constant.SS_KEY.ORDER_INFO, angular.toJson(orderInfo));
                var userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                orderInfo.userId = userInfo.Id;
                
                var files = [];
                //for (var i = 0; i < $scope.images.length; i++) {
                //    window.resolveLocalFileSystemURL($scope.images[i],
                //        function (fileEntry) {
                //            fileEntry.file(function (file) {
                //                files.push(file);
                //            });
                //        });
                //}
                //data.append("files", files);
                //var xhr = new XMLHttpRequest();
                //xhr.open("POST", urlCreatorService.createUrl("Order", "Create2"));
                ////xhr.setRequestHeader("Accept", "application/json");
                ////xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

                //xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
                ////xhr.setRequestHeader("X-File-Name", encodeURIComponent(name));
                //xhr.setRequestHeader("Content-Type", "multipart/form-data");
                //xhr.send(data);


                //orderServices.sendOrder().then(function (res) {
                //    if (res.IsSuccessful) {
                //        $scope.displayAlert = true;
                //        $scope.alertType = constant.MSG_TYPE.SUCCESS;
                //        $scope.alertMsg = messageManagementService.getMessage("S001");
                //        $state.go("customer.list");
                //    } else {
                //        $scope.displayAlert = true;
                //        $scope.alertType = constant.MSG_TYPE.WARNING;
                //        $scope.alertMsg = res.Message;
                //    }
                //}).catch(function (e) {
                //    $scope.displayAlert = true;
                //    $scope.alertType = constant.MSG_TYPE.WARNING;
                //    $scope.alertMsg = messageManagementService.getMessage("E001");
                //});
                var data = new FormData();
                data.append("userId", userInfo.Id);
                data.append("customerId", orderInfo.customer.Id);
                data.append("latitude", orderInfo.latitude);
                data.append("longitude", orderInfo.longitude);
                data.append("orderItems", orderInfo.orderItems);
                //data.append("files", files[0]);
                //var body = {};
                //body["userId"] = userInfo.Id;
                //body["customerId"] = orderInfo.customer.Id;
                //body["latitude"] = orderInfo.latitude;
                //body["longitude"] = orderInfo.longitude;
                //body["orderItems"] = orderInfo.orderItems;
                $http({
                    method: 'POST',
                    url: urlCreatorService.createUrl("Order", "Create2"),

                    headers: {
                        'Content-Type': "application/x-www-form-urlencoded",
                        'Accept': "application/json"
                    },
                    //transformRequest: function (data) {
                    //    var formData = new FormData();
                    //    //need to convert our json object to a string version of json otherwise
                    //    // the browser will do a 'toString()' on the object which will result 
                    //    // in the value '[Object object]' on the server.
                    //    formData.append("model", angular.toJson(data.model));
                    //    //now add all of the assigned files
                    //    for (var i = 0; i < data.files; i++) {
                    //        //add each file to the form data and iteratively name them
                    //        formData.append("file" + i, data.files[i]);
                    //    }
                    //    return formData;
                    //},
                    //Create an object that contains the model and files which will be transformed
                    // in the above transformRequest method
                    data: data
                }).success(function (data, status, headers, config) {
                    alert("success!");
                }).error(function (data, status, headers, config) {
                    alert("failed!");
                });
            }, function (err) {
                $scope.displayAlert = true;
                $scope.alertType = constant.MSG_TYPE.WARNING;
                $scope.alertMsg = "Hãy chắc chắn bạn đã bật GPS.";
            });
        }

    });

})();

