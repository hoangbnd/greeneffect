(function () {
    "use strict";
    angular.module("greeneffect.controller.product",
        [
            "greeneffect.service.product",
            "greeneffect.constant",
            "greeneffect.common.service.messagemanagement"
        ])
        .controller("ProductCtrl",
            function ($scope, $ionicModal, $timeout, $location, messageManagementService, productServices, selectedProduct) {

                $scope.searchProduct = function (keyword) {
                    if (keyword.length < 3) return;
                    productServices.getProducts(keyword, false)
                            .then(function (data) {
                                if (!data.IsSuccessful) {
                                    $scope.displayAlert = true;
                                    $scope.alertType = "warning";
                                    $scope.alertMsg = data.Message;
                                    return;
                                }
                                $scope.products = data.Data.Products;
                            })
                            .catch(function (error) {
                                $scope.displayAlert = true;
                                $scope.alertType = "warning";
                                $scope.alertMsg = messageManagementService.getMessage("E001");
                                return;
                            });
                }

                $scope.chooseProduct = function(item) {
                    selectedProduct.setProduct(item);
                }
            });

})();