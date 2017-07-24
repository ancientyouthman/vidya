var app = angular.module('app', ['ngRoute', 'ngSanitize']);

app.controller('searchController', searchController);
app.controller('gameController', gameController);
app.controller('collectionController', collectionController);


app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false,
        rewriteLinks: false
    });
}]);
