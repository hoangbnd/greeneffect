(function () {
    'use strict';

    angular
      .module('greeneffect.common.service.messagemanagement', ['greeneffect.constant'])
      .factory('MessageManagementService', MessageManagementService);

    MessageManagementService.$inject = ['Constant'];

    function MessageManagementService(Constant) {

        var MessageManagementService = {
            getMessage: function (messageId, messageParams) {
                var msg = Constant.MSG[messageId];
                if (angular.isUndefined(msg)) {
                    return null;
                }
                if (angular.isDefined(messageParams) && messageParams.length > 0) {
                    msg = messageReplace(msg, messageParams);
                }
                return msg;
            }
        };

        function messageReplace(msg, messageParams) {
            for (var i = 0; i < messageParams.length; i++) {
                msg = msg.replace('{' + i + '}', messageParams[i]);
            }
            return msg;
        }
        return MessageManagementService;
    }

})();
