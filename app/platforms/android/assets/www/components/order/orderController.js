//global variable
var imagelist = [];
var imgUrl;
var imgGalleryList ;
var folderPath;
var Folder;
//angular controller and fuction
(function () {
    "use strict";
    angular.module("greeneffect.controller.order", ["ngCordova"])

    .controller("takePhotoCtrl", function ($scope, $state, $cordovaCamera, $cordovaFile, $window) {


        $scope.openGallery = function () {
             
            $state.go("gallery");
        };
        $scope.uploadImg2 = function(){
              uploadPhoto();
            };

        $scope.takePhoto = function () {

            folderPath = cordova.file.applicationStorageDirectory;
            var sourceFile;
            var sourceDir;
            var date = new Date();
            var y = date.getFullYear();
            var m = date.getUTCMonth() + 1;
            var d = date.getDate();
            Folder = d + "_" + m + "_" + y;
            
            
            // take photo image//

             $cordovaCamera.getPicture({
                destinationType: Camera.DestinationType.FILE_URI
            }).then(function(success){
                var imgURI = success;
                imgUrl = success;
                $scope.imgUrl = imgUrl;
                //  alert(success);
              $cordovaFile.checkDir(folderPath, Folder).then(function(success){
                    //alert("Forlder was created!");
                    saveImg(imgURI);
              }, function(err){
                    //alert("Don't find folder");
                    $cordovaFile.createDir(folderPath, Folder, false).then(function(success){
                        alert("Folder was created!");
                    }, function(err){
                        alert("Folder not created");
                    });
              });
                saveImg(success);
                $('.img-take-upload').show('slow');
             }, function(err){
                alert("Take picture err");
             });


                    
          

    function saveImg(data) {

                var time = date.getTime();
                var newFile = time + ".jpg";
                var datafile = "thum.dat";

                sourceFile = data.replace(/^.*[\\\/]/, "");
                //alert("Source file: "+sourceFile);
                sourceDir = data.substr(0, data.lastIndexOf("/") + 1);
                // alert("dir path: "+sourceDir);
                $cordovaFile.moveFile(sourceDir, sourceFile, folderPath+Folder+"/", newFile)
                .then(function (success) {
                    alert("Picture Save in " + folderPath);
                   }, function (err) {
                    console.log("Has err when coppy Image " + err);
                });
            };
        }
    })
    .controller("gelleryCtrl", function ($scope, $cordovaFile, $window) {
        $scope.getNumber = function (num) {
            return new Array(num);
        };
        
        $scope.loadGellery = function () {

            if(typeof folderPath == 'undefined')
                folderPath = cordova.file.applicationStorageDirectory;
            if(typeof Folder == 'undefined')
                {
                    //alert("Folder not setup");
                    var date = new Date();
                    var y = date.getFullYear();
                    var m = date.getUTCMonth() + 1;
                    var d = date.getDate();
                    Folder = d + "_" + m + "_" + y;
                }
           
           getFiles(folderPath+Folder+'/', function(entries){
                alert(typeof entries[0]);
                createItem(entries);
               
                

            });
         };

    })
    .controller("headerCtrl", function ($scope, $state) {
        $scope.back = function (page) {
            $state.go(page);
        };

        $scope.uploadImg = function(){
            //alert(imgGalleryList.length);
                if(imagelist.length>3)
                    alert("Ban khong the chon qua 3 anh");
                else
                    alert("Call Upload to server function");
        }
    });
})();



var endcodeImg = function (file, callback) {
    file.file(function (file) {

        var reader = new FileReader();
        reader.onload = function (e) {
            var content = this.result;
            // imageData = content;
            callback(content);


        };
        reader.readAsDataURL(file); // or the way you want to read it


    });


};

 var  getFiles =  function(dir, callback){
        window.resolveLocalFileSystemURL(dir, function(fileSystem){
                    var reader = fileSystem.createReader();
                        reader.readEntries(function(entries){
                            
                        callback(entries);                    

                    }, function(err){
                        alert("err When reade entries");
                    });
                }, function(err){
                    alert("err when read folder");
                });

};
var createItem = function(imglist){
         var content = $('#gallery-content');
                    content = $(content);
                    for(var i = 0; i<imglist.length; i++){
                        var item = document.createElement('div');
                        var img = document.createElement('img');
                        
                        item = $(item);
                        img = $(img);

                        item.addClass('gallery-item');
                        if(i % 2 == 0)
                            item.addClass('gallery-item-left');
                        else
                            item.addClass('gallery-item-right');
                        if (typeof imglist[i] == 'object')
                            img.prop('src', imglist[i].fullPath)
                        else
                            img.prop('src', imglist[i]);
                        item.append(img);
                        item.append("<input type='checkbox'/>");
                        content.append(item);
                        
                    }
        $('.gallery-item').click(function(){
            var img = $(this).find('img');
            //alert(img.prop("src"));
            var index;
            var checkbox = $(this).find('input[type="checkbox"]');
            if(checkbox.prop('checked')== false){
                checkbox.prop('checked', true);
                img.css('opacity', '0.5');
                checkbox.css('display','block');
                imagelist.push(img.prop('src'));
                //console.log(imglist);
                //$(this).css('border','5px solid white');
                }
            else
            {
                checkbox.prop('checked', false);
                img.css('opacity','1.0');
                //img.css('margin','0');
                checkbox.css('display','none');
                index = imagelist.indexOf(img.prop('src'));
                if(index >-1)
                    imagelist.splice(index,1);
                //console.log(imglist);
                //$(this).css('border','none');
            }
            //console.log(imagelist);
            });

};

var uploadPhoto = function(){
    alert("Upload Image to server");
}