(function () {
    "use strict";
    angular.module("greeneffect.controller.order", [
        "ngCordova",
        "greeneffect.constant",
        "greeneffect.common.service.messagemanagement"])

    .controller("CreateOrderCtrl", function ($scope, $ionicModal, constant, selectedProduct, messageManagementService) {
        $scope.orderItems = [];
        var orderInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.ORDER_INFO));
        $scope.customer = orderInfo.customer;
        $ionicModal.fromTemplateUrl("components/product/searchProduct.html", function (modal) {
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
            $scope.search_modal.hide();
            var isExisted = false;
            console.log(newValue)
            for (var i = 0; i < $scope.orderItems.length; i ++) {
                if ($scope.orderItems[i].productId === newValue.Id) {
                    isExisted = true;
                }
            }
            if (!isExisted) 
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
        //$rootScope.opensearch_modal = function () {
        //    $rootScope.search_modal.show();
        //};

        //$rootScope.closesearch_modal = function () {
        //    $rootScope.search_modal.hide();
        //};
        //$rootScope.$on('$destroy', function () {
        //    $rootScope.search_modal.remove();
        //});
        //$rootScope.$on('modal.hidden', function () {
        //    // Execute action
        //});
    });

})();



var endcodeImg = function (file, callback) {
    file.file(function (file) {

        var reader = new FileReader();
        reader.onload = function (e) {
            var content = this.result;
            // imageData = content;
            callback(content);


        };
        reader.readAsDataURL(file); // or the way you want to read it


    });

};

/*function encodeImageFileAsURL(path) {
            
                var file f = new file("path");
                var reader  = new FileReader();
                reader.onloadend = function () {
                    alert(reader.result);
                }
                reader.readAsDataURL(f);
            
        };*/
