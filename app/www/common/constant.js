(function() {
  'use strict';

  angular
    .module('greeneffect.constant', [])
    .constant('Constant', {
      HTML5_MODE: false,
      SS_KEY: {
        USER_INFO: 'geUserInfo',
        SYSTEM_EXCEPTION: 'geSystemException',
        HT0401F04_PARAM : 'ht0401f04_param'
      },
      GE_COOKIE_KEY : 'GEUSERINFO',
      GE_USER_INFO_KEY : {
        LANG : 'lang'
      },
      ITEM_GET : {
        URL : 'json/:itemfile',
        JSON_FILE : '%formid%.%lang%.json'
      },
      MESSAGE_GET : {
        URL : 'json/:messagefile',
        JSON_FILE : 'message.%lang%.json'
      },
      DEFAULT_LANG : 'JP',
      LANG_FILE : {
        JP : 'ja-JP',
        US : 'en-US',
        CN : 'zh-CN',
        TW : 'zh-TW'
      },
      LANG_COOKIE : {
        JP : 'JP',
        US : 'US',
        CN : 'CN',
        TW : 'TW'
      },
      INDEXED_DB_SETTING : {
        NAME : 'greeneffect',
        VERSION : 1.0
      },
      SOSHIN_STATUS: {
        MISOSHIN: 0,
        SOHINSUMI: 1,
        SOSHINSUMIERROR: 9
      },
      POLLING_STATUS: {
        // 未処理
        MISHORI: '0',
        // 処理中
        SHORICHU: '1'
      },
      OFFLINE_STATUS: 1,
      DATA_SEPALATOR: '_',
      UWP_CALL_PARAM : {
        ON_VOICE_BTN : 'OnVoiceBtn',
        ON_PRINT_BTN : 'OnPrintBtn',
        ON_SET_ACCESS_POINT : 'OnSetAccessPoint',
        ON_SAVE_ACCESS_JSON : 'OnSaveAccessJson',
        ON_SET_SHOHIN : 'OnSetShohin'
      },
      // SPA->UWP連携時の処理名
      SPA_CALL_PARAM : {
        ON_DATA_RECIEVED : 'OnDataRecieved',
        ON_PLUS_KEY_PRESSED : 'OnPlusKeyPressed',
        ON_PLUS_KEY_REPEAT1 : 'OnPlusKeyRepeat1',
        ON_PLUS_KEY_REPEAT2 : 'OnPlusKeyRepeat2',
        ON_MINUS_KEY_PRESSED : 'OnMinusKeyPressed',
        ON_MINUS_KEY_REPEAT1 : 'OnMinusKeyRepeat1',
        ON_MINUS_KEY_REPEAT2 : 'OnMinusKeyRepeat2',
        ON_VOICE_AFTER : 'OnVoiceAfter',
        ON_GET_ACCESS_JSON : 'OnGetAccessJson',
        ON_GET_SHOHIN : 'OnGetShohin'
      }
    });
})();
