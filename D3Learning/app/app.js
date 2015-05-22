(function () {
    angular.module('app', [
        'ngRoute',
        'app.geo',
        'app.introduction'
    ]).config(configureRoutes);


    function configureRoutes($routeProvider) {
        $routeProvider.when('/introduction', {
            url: '/introduction',
            controller: 'introductionCtrl',
            templateUrl: 'app/introduction/introductionView.html'
        }).when('/geo', {
            url: '/geo',
            controller: 'geoCtrl',
            templateUrl: 'app/geo/geoView.html'
        }).otherwise({
            redirectTo: '/introduction'
        });
    }
})();


