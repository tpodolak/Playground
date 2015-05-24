(function () {

    angular.module('app').factory('randomDataService', randomDataService);

    function randomDataService() {

        return {
            addData: addData
        };


        // Compute a random interval using an Exponential Distribution
        function randomInterval(avgSeconds) {
            return Math.floor(-Math.log(Math.random()) * 1000 * avgSeconds);
        }

        // Create or extend an array of increasing dates by adding
// a number of random seconds.
        function addData(data, numItems, avgSeconds) {
            // Compute the most recent time in the data array.
            var n = data.length,
                t = (n > 0) ? data[n - 1].date : new Date();

            // Append items with increasing times in the data array.
            for (var k = 0; k < numItems - 1; k += 1) {
                t = new Date(t.getTime() + randomInterval(avgSeconds));
                data.push({date: t});
            }

            return data;
        }
    }

}());