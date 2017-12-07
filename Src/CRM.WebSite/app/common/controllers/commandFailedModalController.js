crmCommon.controller('commandFailedModalController', [

	'$scope', '$modalInstance', '$log', 'data',
	function ($scope, $modalInstance, $log, data) {

		$scope.Data = data;

		$scope.cancel = function() {

			$modalInstance.dismiss('cancel');

		};

		var initialize = function () {

			$log.info('commandFailedModalController initialized');

		};

		initialize();
	}
]);