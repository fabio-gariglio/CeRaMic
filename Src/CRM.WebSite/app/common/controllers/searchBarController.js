crmSales.controller('searchBarController', [

	'$scope', '$rootScope', '$log',
	function ($scope, $rootScope, $log) {

		$scope.query = null;

		$scope.onSearchClick = function() {
			$rootScope.$emit("search", $scope.query);
		};

		$scope.onAddClick = function () {
			$rootScope.$emit("add");
		};

		$rootScope.$on("forceSearch", function(event, data) {

			$scope.query = data;
			$scope.onSearchClick();

		});

		var initialize = function () {

			$log.info('searchBarController initialized');

		};

		initialize();
	}
]);