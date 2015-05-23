(function () {

    angular.module('app').controller('sidebarCtrl', sidebarController);

    function sidebarController($scope, $rootScope, $location) {

        $scope.tree = [
            {
                location: '/reusableCharts',
                title: 'Reusable charts',
                nodes: [
                    {
                        location: '/creatingElements',
                        title: 'Creating elements'
                    }
                ]

            },
            {
                location: '/noSvg',
                title: 'No SVG',
                nodes: [
                    {
                        location: '/noSvg',
                        title: 'No SVG'
                    }
                ]

            }
        ];

        $scope.handleSidebarItemClick = handleSidebarItemClick;

        activate();

        //

        function handleSidebarItemClick(event, item) {
            event.preventDefault();
            $location.path(item.location);
        }

        function wireUpEvents() {
            $rootScope.$on('$routeChangeSuccess', handleRootChangeSuccess);
        }

        function handleRootChangeSuccess() {
            selectCurrentItem($scope.tree,$location.path());
        }

        function selectCurrentItem(items, currentPath) {
            var currentItem;
            for (var i = 0, length = items.length; i < length; i++) {
                currentItem = items[i];
                currentItem.selected = currentItem.location === currentPath;
                if (currentItem.nodes && currentItem.nodes.length) {
                    selectCurrentItem(currentItem.nodes, currentPath);
                }
            }
        }

        function activate() {
            wireUpEvents();
        }
    }

}());