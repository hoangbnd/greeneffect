(function () {
    "use strict";
    angular.module("greeneffect.controller.order", [
        "ngCordova",
        "greeneffect.constant",
        "greeneffect.service.order",
        "greeneffect.common.service.messagemanagement",
        "greeneffect.common.service.urlcreator"])

    .controller("CreateOrderCtrl", function ($scope, $ionicModal, $cordovaGeolocation, $http,
        constant, selectedProduct, messageManagementService, urlCreatorService, orderServices) {
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
            // capture callback
            var captureSuccess = function (mediaFiles) {
                var i, len;
                for (i = 0, len = mediaFiles.length; i < len; i += 1) {
                    orderInfo.images.push(mediaFiles[i].fullPath);
                }
                $scope.images = orderInfo.images;
            };

            // capture error callback
            var captureError = function (error) {

                $scope.displayAlert = true;
                $scope.alertType = "warning";
                switch (error.code) {
                    case CaptureError.CAPTURE_NO_MEDIA_FILES:
                        $scope.alertMsg = messageManagementService.getMessage("E001");
                        break;
                    case CaptureError.CAPTURE_INTERNAL_ERR:
                        $scope.alertMsg = messageManagementService.getMessage("E001");
                        break;
                    case CaptureError.CAPTURE_APPLICATION_BUSY:
                        $scope.alertMsg = messageManagementService.getMessage("E001");
                        break;
                    case CaptureError.CAPTURE_INVALID_ARGUMENT:
                        $scope.alertMsg = messageManagementService.getMessage("E001");
                        break;
                    case CaptureError.CAPTURE_NO_MEDIA_FILES:
                        $scope.alertMsg = messageManagementService.getMessage("E001");
                        break;
                    case CaptureError.CAPTURE_PERMISSION_DENIED:
                        $scope.alertMsg = messageManagementService.getMessage("E001");
                        break;
                    case CaptureError.CAPTURE_NOT_SUPPORTED:
                        $scope.alertMsg = messageManagementService.getMessage("E001");
                        break;
                }

                return;
            };

            // start image capture
            navigator.device.capture.captureImage(captureSuccess, captureError, { limit: 3 });
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
                //sessionStorage.setItem(constant.SS_KEY.ORDER_INFO, angular.toJson(orderInfo));
                var userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                orderInfo.userId = userInfo.Id;
                //var data = new FormData();
                //data.append("userId", userInfo.Id);
                //data.append("customerId", orderInfo.customer.Id);
                //data.append("latitude", orderInfo.latitude);
                //data.append("longitude", orderInfo.longitude);
                //data.append("orderItems", orderInfo.orderItems);
                var files = [];
                for (var i in $scope.images) {
                    window.requestLocalFileSystemURL(i,
                        function (fileEntry) {
                            fileEntry.file(function (file) {
                                files.push(file);
                            });
                        });
                }
                //data.append("files", files);
                //var xhr = new XMLHttpRequest();
                //xhr.open("POST", urlCreatorService.createUrl("Order", "Create2"));
                ////xhr.setRequestHeader("Accept", "application/json");
                ////xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

                //xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
                ////xhr.setRequestHeader("X-File-Name", encodeURIComponent(name));
                //xhr.setRequestHeader("Content-Type", "multipart/form-data");
                //xhr.send(data);


                orderServices.sendOrder().then(function (res) {
                    if (res.IsSuccessful) {
                        $scope.alertMsg = messageManagementService.getMessage("S001");
                    } else {
                        $scope.displayAlert = true;
                        $scope.alertType = "warning";
                        $scope.alertMsg = res.Message;
                    }
                });
                //var body = {};
                //body["userId"] = userInfo.Id;
                //body["customerId"] = orderInfo.customer.Id;
                //body["latitude"] = orderInfo.latitude;
                //body["longitude"] = orderInfo.longitude;
                //body["orderItems"] = orderInfo.orderItems;
                //$http({
                //    method: 'POST',
                //    url: urlCreatorService.createUrl("Order", "Create"),

                //    headers: {
                //        'Content-Type': false,
                //        'Accept': 'application/json'
                //    },
                //    transformRequest: function (data) {
                //        var formData = new FormData();
                //        //need to convert our json object to a string version of json otherwise
                //        // the browser will do a 'toString()' on the object which will result 
                //        // in the value '[Object object]' on the server.
                //        formData.append("model", angular.toJson(data.model));
                //        //now add all of the assigned files
                //        for (var i = 0; i < data.files; i++) {
                //            //add each file to the form data and iteratively name them
                //            formData.append("file" + i, data.files[i]);
                //        }
                //        return formData;
                //    },
                //    //Create an object that contains the model and files which will be transformed
                //    // in the above transformRequest method
                //    data: { model: body, files: files }
                //}).success(function (data, status, headers, config) {
                //    alert("success!");
                //}).error(function (data, status, headers, config) {
                //    alert("failed!");
                //});
            }, function (err) {
                $scope.displayAlert = true;
                $scope.alertType = "warning";
                $scope.alertMsg = "Hãy chắc chắn bạn đã bật GPS.";
            });
        }

    });

})();

