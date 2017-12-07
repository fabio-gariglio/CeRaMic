crmCommon.controller('exportModalController', [

	'$scope', '$modalInstance', '$log', 'api', 'commandBus',
	function ($scope, $modalInstance, $log, api, commandBus) {

		$scope.allSelected = false;
		$scope.sellers = api.query(api.sellers.all, {});

		$scope.toggleAll = function() {

			angular.forEach($scope.sellers, function(seller) {
				seller.Selected = $scope.allSelected;
			});

		};

		$scope.cancel = function() {

			$modalInstance.dismiss('cancel');

		};

		$scope.export = function() {

			var command = { ExportId: uuid.v4() };

			if (!$scope.allSelected) {

				command.SellerIds = Enumerable.From($scope.sellers)
					.Where(function(seller) { return seller.Selected; })
					.Select(function(seller) { return seller.Id; })
					.ToArray();

			}

			commandBus.send("ExportCommand", command);

		};

		var initialize = function () {

			$log.info('exportModalController initialized');

		};

		initialize();
	}
]);