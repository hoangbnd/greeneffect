(function() {
  'use strict';

  angular
    .module('greeneffect.common.service.urlcreator', [
      'greeneffect.constant'
    ])
    .factory('UrlCreatorService', function UrlCreatorService(Constant) {

        var urlcreatorService = {
            createUrl: function (apiId, method) {
                var url = Constant.URL.TEMPLATE;
                url = url.replace('%protocol%', Constant.URL.PROTOCOL);
                url = url.replace('%domain%', Constant.URL.DOMAIN);
                url = url.replace('%contentRoot%', Constant.URL.CONTENT_ROOT);
                url = url.replace('%apiId%', apiId);
                url = url.replace('%method%', method);
                return url;
            }
        };

        return urlcreatorService;
    });
})();
