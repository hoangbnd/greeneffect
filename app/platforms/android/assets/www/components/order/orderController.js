(function () {
    "use strict";
    angular.module("greeneffect.controller.order", ["ngCordova"])

    .controller("takePhotoCtrl", function ($scope, $state, $cordovaCamera, $cordovaFile, $window) {
        $scope.openGallery = function(){           
            $state.go("gallery");            
        };
        $scope.imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT4AhEoPhG5BaW_b1_CxxpiqXl-tsVTk21u2tqN2i_OrS1pN66-";
           
        $scope.takePhoto = function(){

            var dataDir = cordova.file.applicationStorageDirectory;
            var sourceDir;
            var sourceFile;
            var date = new Date();
            var y  = date.getFullYear();
            var m = date.getUTCMonth() +1;
            var d = date.getDate();
            var Folder = d+"_"+m+"_"+y;
                
            var folderPath = dataDir+Folder;
            //  alert ("NEw folder: "+ dataDir+Folder);

		
            var options = {
                quality : 100,
                sourceType : Camera.PictureSourceType.CAMERA,
                destinationType : Camera.DestinationType.FILE_URI,
                targetWidth: 300,
                targetHeight:300
          				
            };

            var imageData;
            var imgFile;
            $cordovaCamera.getPicture(options)
            .then(function(imgURI){
                //saveImg
                saveImg(imgURI);
                $cordovaFile.checkDir(dataDir, Folder)
                .then(function(success){
                    //alert("finded folder");
                }, function(err){
                    alert("dont find folder");
                    $cordovaFile.createDir(dataDir, Folder)
                  .then(function(success){
                      alert("Created folder");
                  }, function(err){
                      alert("Create folder err"+err);
                  });
                });

                

                
              

                $window.resolveLocalFileSystemURL(imgURI, function success(fileEntry) {                    
                    endcodeImg(fileEntry, function(content){
                        $scope.imgUrl = content;
                    });
                        
                }, function(err){
                    alert("Err file");
                });




            }, function(err){

                alert("Tack Picture Err");
            });



            function saveImg(data){

                var time = date.getTime();
                var newFile = time+".jpg";
                sourceFile = data.replace(/^.*[\\\/]/, "");
                //alert("Source file: "+sourceFile);
                sourceDir = data.substr(0,data.lastIndexOf("/")+1);
                // alert("dir path: "+sourceDir);
                $cordovaFile.moveFile(sourceDir, sourceFile, folderPath, newFile)
                .then(function(success){
                    alert("Picture Save in "+ folderPath);
                    // alert("Coppy Image Success");
                }, function(err){
                    console.log("Has err when coppy Image "+err);
                }); 
            };

	
        }
    })
    .controller("gelleryCtrl", function($scope, $cordovaFile, $window){
        $scope.getNumber = function(num){
            return new Array(num);
        };

        $scope.loadGellery = function (){

            var gallery_content = $("#gallery_content");

            var screenWidth = window.screen.width/3;
            $scope.w = screenWidth;
            $scope.filelist = [];
            /*//////////////////////////////////////////////
            //////////reade Forlder Image//////////////////
            ////////////////////////////////////////////*/
            var dataDir = cordova.file.applicationStorageDirectory;
            var date = new Date();
            var y  = date.getFullYear();
            var m = date.getUTCMonth() +1;
            var d = date.getDate();
            var Folder = d+"_"+m+"_"+y;
            var folderPath = dataDir+Folder;
           
           

            $window.resolveLocalFileSystemURL(folderPath,function(dirEntry){
                var dirReader = dirEntry.createReader();
                dirReader.readEntries(
                  function(entries){
                      var filepath;
                      var id;
                      for(var i = 0; i<entries.length; i++)
                      { 
                      
                          var img = document.createElement("img");                     
                          filepath = entries[i].nativeURL;
                          id = entries[i].name;
                          id = id.substr(0,id.indexOf("."));
                          //alert(id);
                          $scope.filelist.push(filepath);
                          endcodeImg(entries[i], function(content){
                              img.src = content;
                              gallery_content.append("<div  class='gallery_item' ng-click='compentClick'> " +
                                  "<input class='pic_check' value ='"+filepath+"'type='checkbox'/>  " +
                                  "<img  id='"+id+"' width='100' heigth='100'  src='"+content+"'> </div> ");
                        
                          });
                      }

                      // alert($scope.filelist.length);         
                  }, function(err){
                      alert("Has Err when show entries");
                  });
            }, function(err){
                alert("Error when read Folder Image");
            } );

            $scope.compentClick = function(){
                alert("click");
            };
          

           
        }
    })
    .controller("headerCtrl", function($scope,$state){
        $scope.back = function (page){
            $state.go(page);
        }
    });



})();
  


var endcodeImg = function(file, callback){
    file.file(function(file) {
                                     
        var reader = new FileReader();
        reader.onload = function(e) {
            var content = this.result;
            // imageData = content;
            callback(content);
                                    
                                    
        };
        reader.readAsDataURL(file); // or the way you want to read it
                                

    });
                  
};
                
/*function encodeImageFileAsURL(path) {
            
                var file f = new file("path");
                var reader  = new FileReader();
                reader.onloadend = function () {
                    alert(reader.result);
                }
                reader.readAsDataURL(f);
            
        };*/
