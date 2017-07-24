var searchController = function ($scope, $http, $location) {
    var term = $location.search().searchTerm;
    $scope.searchForm = {
        searchTerm: term ? term : 'enter a term...',
        page: 1
    };

    $scope.results = [];

    $scope.search = function () {
        $scope.results = [];
        $scope.loading = true;
        $location.url('/Search?searchTerm=' + $scope.searchForm.searchTerm, false);
        $http({
            method: 'POST',
            url: '/Search/SearchGames?Name=' + $scope.searchForm.searchTerm + '&Page=' + $scope.searchForm.page
        }).then(function successCallback(response) {
            debugger;
            $scope.results = response.data.Results;
            $scope.loading = false;
        }, function errorCallback(response) {
            alert('error');
            $scope.loading = false;
        });
    }

    $scope.nextPage = function(){
        $scope.searchForm.page++;
        $scope.search();
    }

    if (term) $scope.search();
}

searchController.$inject = ['$scope', '$http', '$location'];