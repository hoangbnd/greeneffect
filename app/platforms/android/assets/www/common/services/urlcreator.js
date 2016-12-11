(function() {
  "use strict";

  angular
    .module("greeneffect.common.service.urlcreator", [
      "greeneffect.constant"
    ])
    .factory("urlCreatorService", function urlCreatorService(constant) {

        var urlcreatorService = {
            createUrl: function (apiId, method) {
                var url = constant.URL.TEMPLATE;
                url = url.replace("%protocol%", constant.URL.PROTOCOL);
                url = url.replace("%domain%", constant.URL.DOMAIN);
                url = url.replace("%contentRoot%", constant.URL.CONTENT_ROOT);
                url = url.replace("%apiId%", apiId);
                url = url.replace("%method%", method);
                return url;
            }
        };

        return urlcreatorService;
    });
})();
