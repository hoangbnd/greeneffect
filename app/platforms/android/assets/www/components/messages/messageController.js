(function () {
    "use strict";
    angular.module("greeneffect.controller.message",
        ["ngCordova" ])
        .controller("MessageCtrl",
           function ($scope,  $cordovaEmailComposer, $ionicPlatform) {
            var data=[{id:1, text:'AnhNguyen', email:'anhnd1503@gmail.com'},{id:2, text:"HoangNguyen", email: "hoangbnd@gmail.com"}];
            $ionicPlatform.ready(function(){

                            
                 $("#email").select2({
                placeholder: "Nhap email",
                data: data,
                tags: true,
                tokenSeparators: [',',' '] 
              });
                 $scope.sendMessage = function(){
                    alert($cordovaEmailComposer);
                 }

            });
            /*var data=[{id:1, text:'AnhNguyen', email:'anhnd1503@gmail.com'},{id:2, text:"HoangNguyen", email: "hoangbnd@gmail.com"}];
            $("#email").select2({
              placeholder: "Nhap email",
              data: data,
              tags: true,
              tokenSeparators: [',',' '] 
            });
            $scope.sendMessage = function($cordovaEmailComposer){
            var email = $('#email');
            var subject = $('#subject');
            var messageContent = $('#message-content');
            var maillist = [];
            $('#email :selected').each(function(){
              maillist.push($(this).select2().data().data.email);
            });
              
          
             
              //send email

              document.addEventListener('deviceready', function(){
                alert($cordovaEmailComposer);
              }, false);


            }*/
                 /*      var data=[{id:1, text:'AnhNguyen', email:'anhnd1503@gmail.com'},{id:2, text:"HoangNguyen", email: "hoangbnd@gmail.com"}];
             var emailadd = $('.emailadd').find('select');
             emailadd.css({"width": "100%","background-color":"gray"});
             emailadd.addClass('email');
             console.log(emailadd);
             emailadd.select2({
              data:data,
              tokenSeparators: [',',' '],
              tags: true,
              allowClear: true

             });*/
             
            });

})();



 
