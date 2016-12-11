(function () {
    "use strict";
    angular.module("greeneffect.controller.message",
         ["greeneffect.service.message"])
        .controller("MessageCtrl",
            function ($scope, messageServices) {
                //data demo
                sessionStorage.setItem("user", "anhnd");
                //prepare data
                $scope.currentUser = sessionStorage.getItem('user');
                $scope.loading = false;
                $scope.messageObj = {
                    userFrom: $scope.currentUser,
                    userTo: [],
                    subject: '',
                    messageContent: ''
                };

                // get list Receivers
                var $selectBox = $('#email').select2({
                    //data:data
                    placeholder: 'Receivers',
                    minimumInputLength: 0,
                    escapeMarkup: function (m) {
                        return m;
                    },
                    ajax: {
                        dataType: 'json',
                        url: "dummy/users.json",//TODO: dummy data
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
                                results = data.Data;

                            } else {
                                // TODO: show messgage
                            }
                            return {
                                results: results,
                                // TODO: pagination
                                // pagination: {
                                //     more: (params.page * 30) < data.TotalCount
                                // }
                            };
                        },
                    }
                });
                // triger focus on input
                $('#email').focus(function () {
                    $selectBox.select2('open');
                });


                $scope.uploadData = function () {
                    $scope.loading = true;
                    $scope.messageObj.userTo = [];
                    /* get UserTo */
                    $('#email :selected').each(function () {
                        $scope.messageObj.userTo.push($(this).select2().data().data.email);
                    });
                    if ($scope.messageObj.userTo.length == 0) {
                        //show message
                        return;
                    }
                    console.log($scope.messageObj);

                    //call api upload to server
                    messageServices.sendMessage($scope.messageObj).then(function (response) {
                        //Upload to server success
                        $scope.loading = false;
                        $scope.messageObj.subject = '';
                        $scope.messageObj.userTo = [];
                        $scope.messageObj.messageContent = "";
                        $scope.message = "Message send success";

                    }, function (err) {
                        $scope.message = "Message send failure";
                        $scope.loading = false;
                        //upload to serve fail
                    });
                };

                $scope.back = function () {
                    alert("Back");
                }

            }
        )
        .controller("NotificationCtrl",
            function ($scope, messageServices) {
                $scope.notificate = [];
                //messageServices.getMessages().then(function (response) {
                //    if (response.IsSuccessful) {
                //        $scope.notificate = response.Data;
                //    } else {
                //        $scope.message = response.Message;
                //    }
                //});

                //TODO: dummy
                $.ajax({
                    method: 'GET',
                    url: "dummy/notificate.json",
                    dataType: 'json',
                    cache: true,
                    success: function (response) {
                        if (response.IsSuccessful) {
                            $scope.notificate = response.Data;
                        } else {
                            $scope.message = response.Message;
                        }
                    },

                });
            }
       );
})();



