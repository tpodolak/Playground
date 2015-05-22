(function () {

    angular.module('app.introduction',[]).controller('introductionCtrl', introductionController);


    function introductionController($scope) {
        $scope.title = "Introduction";
    }
}());