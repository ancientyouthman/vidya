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
        $location.url('/GameSearch?searchTerm=' + $scope.searchForm.searchTerm, false);
        $http({
            method: 'POST',
            url: '/GameSearch/SearchGames?Name=' + $scope.searchForm.searchTerm + '&Page=' + $scope.searchForm.page
        }).then(function successCallback(response) {
            $scope.results = response.data.results;
            $scope.totalResults = response.data.number_of_total_results;

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

    $scope.loadMore = function () {
        $scope.searchForm.page++;
        $scope.loading = true;
        $http({
            method: 'POST',
            url: '/GameSearch/SearchGames?Name=' + $scope.searchForm.searchTerm + '&Page=' + $scope.searchForm.page
        }).then(function successCallback(response) {
            $scope.results =   $scope.results.concat(response.data.results);
            $scope.totalResults = response.data.number_of_total_results;
           
            $scope.loading = false;
        }, function errorCallback(response) {
            alert('error');
            $scope.loading = false;
        });
    }
    $scope.addToCollection = function (id, adding, list) {
        $scope.inCollection = adding;

        $http({
            method: 'POST',
            url: '/Collection/AddToCollection?id=' + id + '&adding=' + adding + '&listType=' + list
        }).then(function successCallback(response) {
            $scope.$apply();
        }, function errorCallback(response) {
            alert('error ' + response);
        });
    }
    if (term) $scope.search();
}

searchController.$inject = ['$scope', '$http', '$location'];