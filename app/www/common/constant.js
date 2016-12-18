(function () {
    "use strict";

    angular
        .module("greeneffect.constant", [])
        .constant("constant",
        {
            HTTP_PROVIDER_SETTINGS: {
                RETRY_COUNT: 3,
                RETRY_INTERVAL: 1000,
                TIMEOUT: 60000
            },
            POLLING_START: false,
            URL: {
                TEMPLATE: "%protocol%://%domain%/%contentRoot%/%apiId%/%method%",
                PROTOCOL: "http",
                //DOMAIN: "localhost:17190",
                DOMAIN: "hoangnh.somee.com",
                CONTENT_ROOT: "api",
                METHOD: "post"
            },
            HTML5_MODE: false,
            SS_KEY: {
                USER_INFO: "geUserInfo",
                SYSTEM_EXCEPTION: "geSystemException",
                HT0401F04_PARAM: "ht0401f04_param",
                ROUTE_INFO: "geRouteInfo",
                ORDER_INFO: "geOrderInfo"
            },
            GE_COOKIE_KEY: "GEUSERINFO",
            GE_USER_INFO_KEY: {
                LANG: "lang"
            },
            OFFLINE_STATUS: 1,
            NOTICE_RETRY_INTERVAL: 1000 * 60 * 10,
            DATA_SEPALATOR: "_",
            MSG: {
                E001: "Có lỗi xảy ra trong hệ thống. Xin vui lòng thử lại sau.",
                E101: "Thông tin {0} không được để trống.",
                S001: "Gửi đơn hàng thành công."
            },
            MSG_TYPE: {
                WARNING: "warning",
                INFO: "info",
                SUCCESS: "success"
            }

        });
})();
