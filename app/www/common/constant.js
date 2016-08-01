(function () {
    'use strict';

    angular
      .module('greeneffect.constant', [])
      .constant('Constant', {
          HTTP_PROVIDER_SETTINGS: {
              RETRY_COUNT: 3,
              RETRY_INTERVAL: 1000,
              TIMEOUT: 60000
          },
          POLLING_START: false,
          URL: {
              TEMPLATE: '%protocol%://%domain%/%contentRoot%/%apiId%/%method%',
              PROTOCOL: 'http',
              DOMAIN: 'localhost:17190',
              CONTENT_ROOT: 'api',
              METHOD: 'get'
          },
          HTML5_MODE: false,
          SS_KEY: {
              USER_INFO: 'geUserInfo',
              SYSTEM_EXCEPTION: 'geSystemException',
              HT0401F04_PARAM: 'ht0401f04_param'
          },
          GE_COOKIE_KEY: 'GEUSERINFO',
          GE_USER_INFO_KEY: {
              LANG: 'lang'
          },
          INDEXED_DB_SETTING: {
              NAME: 'greeneffect',
              VERSION: 1.0
          },          OFFLINE_STATUS: 1,
          DATA_SEPALATOR: '_',
          SPA_CALL_PARAM: {
              ON_DATA_RECIEVED: 'OnDataRecieved',
              ON_PLUS_KEY_PRESSED: 'OnPlusKeyPressed',
              ON_PLUS_KEY_REPEAT1: 'OnPlusKeyRepeat1',
              ON_PLUS_KEY_REPEAT2: 'OnPlusKeyRepeat2',
              ON_MINUS_KEY_PRESSED: 'OnMinusKeyPressed',
              ON_MINUS_KEY_REPEAT1: 'OnMinusKeyRepeat1',
              ON_MINUS_KEY_REPEAT2: 'OnMinusKeyRepeat2',
              ON_VOICE_AFTER: 'OnVoiceAfter',
              ON_GET_ACCESS_JSON: 'OnGetAccessJson',
              ON_GET_SHOHIN: 'OnGetShohin'
          }
      });
})();
