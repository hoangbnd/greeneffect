﻿(function () {
    "use strict";
    angular.module("greeneffect.controller.message",
         [
             "greeneffect.service.message",
             "greeneffect.constant",
              "greeneffect.common.service.urlcreator"
         ])
        .controller("MessageCtrl",
            function ($scope, constant, messageServices, urlCreatorService) {
                //prepare data
                $scope.currentUser = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                $scope.loading = false;
                $scope.messageObj = {
                    fromId: $scope.currentUser.Id,
                    toIds: [],
                    title: "",
                    content: ""
                };

                // get list Receivers
                var $selectBox = $("#email").select2({
                    placeholder: "Người nhận",
                    minimumInputLength: 0,
                    escapeMarkup: function (m) {
                        return m;
                    },
                    ajax: {
                        method: "get",
                        dataType: "json",
                        url: urlCreatorService.createUrl("user", "GetUsers"),
                        data: function (params) {
                            return {
                                q: params.term, // search term
                                page: params.page
                            };
                        },
                        processResults: function (data, params) {
                            params.page = params.page || 1;
                            var results = [];
                            if (data.IsSuccessful) {
                                data.Data.forEach(function(item) {
                                    item.text = item.UserName;
                                    item.id = item.Id;
                                    results.push(item);
                                });
                            } else {
                                results = [];
                            }
                            return {
                                results: results
                            };
                        }
                    }
                });
                // triger focus on input
                $("#email").focus(function () {
                    $selectBox.select2('open');
                });


                $scope.uploadData = function () {
                    $scope.loading = true;
                    $scope.messageObj.toIds = [];
                    /* get UserTo */
                    $("#email :selected").each(function () {
                        $scope.messageObj.toIds.push($(this).select2().data().data.id);
                    });
                    if ($scope.messageObj.toIds.length == 0) {
                        //show message
                        return;
                    }
                    console.log($scope.messageObj);
                    sessionStorage.setItem(constant.SS_KEY.MSG_INFO, angular.toJson($scope.messageObj));
                    //call api upload to server
                    messageServices.sendMessage().then(function (response) {
                        //Upload to server success
                        $scope.loading = false;
                        $scope.messageObj.title = '';
                        $scope.messageObj.toIds = [];
                        $scope.messageObj.content = "";
                        $scope.message = "Message send success";

                    }, function (err) {
                        $scope.message = "Message send failure";
                        $scope.loading = false;
                        //upload to serve fail
                    });
                };

            }
        )
        .controller("ListNotificateCtrl",
            function ($scope, messageServices) {
                $scope.notificate = [];
                messageServices.getMessages().then(function (response) {
                    if (response.IsSuccessful) {
                        $scope.notificate = response.Data;
                    } else {
                        $scope.message = response.Message;
                    }
                });

                $scope.showNotificate = function(item) {
                    item.show = !item.show;
                    if (!item.IsRead) {
                        item.IsRead = true;
                    }
                    messageServices.readMessage(item.Id);
                }

                $scope.delete = function (item) {
                    $scope.notificate.splice($scope.notificate.indexOf(item), 1);
                    messageServices.deleteMessage(item.Id);
                }
            }
       );
})();



