(function () {

    angular.module('app').controller('sidebarCtrl', sidebarController);

    function sidebarController($scope, $rootScope, $location) {

        $scope.items = [
            {
                location: 'introduction',
                title: 'Introduction'
            },
            {
                location: 'geo',
                title: 'Geo'
            }
        ];

        $scope.handleSidebarItemClick = handleSidebarItemClick;

        activate();

        //

        function handleSidebarItemClick($event, item) {
            $event.preventDefault();
            $location.path(item.location);
        }

        function wireUpEvents() {
            $rootScope.$on('$routeChangeSuccess', handleRootChangeSuccess);
        }

        function handleRootChangeSuccess(current) {
            console.log(current);
        }

        function activate() {
            wireUpEvents();
        }
    }

}());