var gameController = function ($scope, $http, $location) {
    $scope.loading = true;
    var gameId = $location.search().id;
    $scope.model = {};
    $http({
        method: 'POST',
        url: '/Game/GetGame?id=' + gameId
    }).then(function successCallback(response) {
        $scope.model = response.data;
        $scope.loading = false;
    }, function errorCallback(response) {
        alert('error ' + response);
    });

    $scope.addToCollection = function (id, adding, list) {
        // TODO: return a new instance of the game instead? 
        $http({
            method: 'POST',
            url: '/Collection/AddToCollection?id=' + id + '&adding=' + adding + '&listType=' + list
        }).then(function successCallback(response) {
            $scope.model.Have = response.data.Have;
            $scope.model.Want = response.data.Want;
            $scope.$apply();
        }, function errorCallback(response) {
            alert('error ' + response);
        });
    }

    $scope.addPlatform = function (platformId, adding) {
        $http({
            method: 'POST',
            url: '/Collection/AddPlatformForGame?gameId=' + gameId + '&platFormId=' + platformId + '&adding=' + adding
        }).then(function successCallback(response) {
            var ids = response.data;
            for (var i = 0; i < ids.length; i++) {
                if ($scope.model.Platforms[i].Id == ids[i]) {
                    $scope.model.Platforms[i].Owned = true;
                }
            }
     
        }, function errorCallback(response) {
            alert('error ' + response);
        });
        $scope.$evalAsync();
    }

}

gameController.$inject = ['$scope', '$http', '$location'];