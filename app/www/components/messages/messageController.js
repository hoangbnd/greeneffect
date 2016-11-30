(function () {
    "use strict";
    angular.module("greeneffect.controller.message",
        [ ])
        .controller("MessageCtrl",
           function ($scope) {
            //data demo
            sessionStorage.setItem("user", "anhnd");
            //get UserId
            $('#fromId').val(sessionStorage.getItem('user'));
            var data=[{id:1, text:'AnhNguyen', email:'anhnd1503@gmail.com'},{id:2, text:"HoangNguyen", email: "hoangbnd@gmail.com"}];
            $('#email').select2({
             /* data:data,*/
              ajax:{
                //get user from server
              }
            });
            // setdata to 
            //get Data Upload
            var subject;
            var userFrom;
            var userTo=[];
            var messageContent;

            $scope.uploadData = function(){
              alert("Call uploadData Function");

              //get Data Upload
                  subject = $('#subject').val();
                  userFrom = $('#fromId').val();

              //

              //alert (window.sessionStorage.getItem("user"));

              /* get UserTo */
              $('#email :selected').each(function(){
                userTo.push($(this).select2().data().data.email);
              });

              messageContent = $('#message-content').val();
              var message = {
                subject: subject,
                userFrom: userFrom,
                userTo: userTo,
                messageContent: messageContent
              }
              //call api upload to server
              uploadToServer(url, message).then(function(success){
                  //Upload to server success

              }, function(err){
                //upload to serve fail
              })
            };

            $scope.back = function(){
              alert("Back");
            }


            //userFuction
            var uploadToServer = function(url, message){
              $.ajax({
                url: url,
                type: 'post',
                dataType: 'json',
                success: function(data){
                  //get result post data to server are success or fail
                },
                data: message
              });
            }       

             
            });

})();



 