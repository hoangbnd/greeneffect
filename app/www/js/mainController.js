angular.module('greeneffect.controller.main', [])

.controller('AppCtrl', function ($scope, $ionicModal, $timeout) {
    $scope.viewOnMap = function () {
        var routeInfo = {
            currentRoute: $scope.routeSelected,
            allRoutes: $scope.routes,
            allCustomer: $scope.allCustomer
        }
        sessionStorage.setItem(Constant.SS_KEY.ROUTE_INFO, angular.toJson(routeInfo));
        $location.path("#/app/map")
    };
    
})

.controller('PlaylistsCtrl', function ($scope) {
    $scope.playlists = [
      { title: 'Reggae', id: 1 },
      { title: 'Chill', id: 2 },
      { title: 'Dubstep', id: 3 },
      { title: 'Indie', id: 4 },
      { title: 'Rap', id: 5 },
      { title: 'Cowbell', id: 6 }
    ];
})

.controller('PlaylistCtrl', function ($scope, $stateParams) {
});
