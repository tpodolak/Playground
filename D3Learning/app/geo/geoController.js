(function () {

    angular.module('app.geo', []).controller('geoCtrl', geoController);


    function geoController($scope) {
        $scope.title = "Geo chapter";
    }

}());