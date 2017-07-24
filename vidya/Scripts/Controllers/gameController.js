var gameController = function ($scope, $http, $location) {
    debugger;
    $scope.loading = true;

    $scope.model = {
    };
    $http({
        method: 'POST',
        url: '/Game/GetGame?id=' + $location.search().id
    }).then(function successCallback(response) {
        debugger;
        $scope.model = response.data;
        $scope.loading = false;
    }, function errorCallback(response) {
        debugger;
        alert('error ' + response);
    });

    $scope.addToCollection = function (id) {
        $http({
            method: 'POST',
            url: '/Collection/AddToCollection?id=' + id
        }).then(function successCallback(response) {

            $scope.addSuccess = response.data;
            $scope.$apply();
        }, function errorCallback(response) {
            debugger;
            alert('error ' + response);
        });
    }
}

gameController.$inject = ['$scope', '$http', '$location'];