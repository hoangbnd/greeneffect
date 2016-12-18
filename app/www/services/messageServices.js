(function () {
    "use strict";
    angular.module("greeneffect.service.message", ["ngResource", "greeneffect.constant", "greeneffect.common.service.urlcreator"]).factory("messageServices",
        ["$resource", "$q", "$exceptionHandler", "constant", "urlCreatorService",
        function ($resource, $q, $exceptionHandler, constant, urlCreatorService) {
            var messageServicesFactory = {
                sendMessage: sendMessage,
                getMessages: getMessages,
                getNewNotice: getNewNotice,
                readMessage: readMessage,
                deleteMessage: deleteMessage
            };
            return messageServicesFactory;
            ///////////////////////////////////////////////

            function getMessages() {
                var userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                var body = {};
                body["UserId"] = userInfo.Id;
                return $resource(urlCreatorService.createUrl("Message", "GetMessages"), {
                    save: {
                        transformResponse: function (data) {
                            return angular.fromJson(data);
                        }
                    }
                }).save(body).$promise;
            }


            function sendMessage(data) {
                return $resource(urlCreatorService.createUrl("Message", "Send"), {
                    save: {
                        transformResponse: function (body) {
                            return angular.fromJson(body);
                        }
                    }
                }).save(data).$promise;
            }

            function getNewNotice() {
                var body = {};
                var userInfo = angular.fromJson(sessionStorage.getItem(constant.SS_KEY.USER_INFO));
                body["userId"] = userInfo.Id;

                return $resource(urlCreatorService.createUrl("Message", "Get"), {
                    save: {
                        transformResponse: function (data) {
                            return angular.fromJson(data);
                        }
                    }
                }).save(body).$promise;
            }

            function readMessage(msgId) {
                var body = {};
                body["Id"] = msgId;

                return $resource(urlCreatorService.createUrl("Message", "Read"), {
                    save: {
                        transformResponse: function (data) {
                            return angular.fromJson(data);
                        }
                    }
                }).save(body).$promise;
            }

            function deleteMessage(msgId) {
                var body = {};
                body["Id"] = msgId;

                return $resource(urlCreatorService.createUrl("Message", "Delete"), {
                    save: {
                        transformResponse: function (data) {
                            return angular.fromJson(data);
                        }
                    }
                }).save(body).$promise;
            }
        }
    ]);
})();