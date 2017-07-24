var collectionController = function ($scope, $http, $location) {
   

    $scope.model = {};
    $http({
        method: 'POST',
        url: '/Collection/GameCollection?id='
    }).then(function successCallback(response) {
        debugger;
        $scope.model.collection = response.data;
        $scope.loading = false;
    }, function errorCallback(response) {
        debugger;
        alert('error ' + response);
    });

}

collectionController.$inject = ['$scope', '$http', '$location'];