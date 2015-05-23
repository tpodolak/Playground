(function () {

    angular.module('app.reusableCharts', []);
    angular.module('app.noSvgVisualisation', []);

    angular.module('app', [
        'ngRoute',
        'app.reusableCharts',
        'app.noSvgVisualisation'
    ]).config(configureRoutes);


    function configureRoutes($routeProvider) {
        $routeProvider.when('/reusableCharts', {
            url: '/reusableCharts',
            controller: 'reusableChartsCtrl',
            templateUrl: 'app/reusableCharts/reusableChartsView.html'
        }).when('/creatingElements', {
            url: '/creatingElements',
            controller: 'creatingElementsCtrl',
            templateUrl: 'app/reusableCharts/creatingElementsView.html'
        }).when('/noSvg', {
            url: '/noSvg',
            controller: 'noSvgVisualisationCtrl',
            templateUrl: 'app/noSvgVisualisation/noSvgVisualisationView.html'
        }).otherwise({
            redirectTo: '/reusableCharts'
        });
    }
})();


