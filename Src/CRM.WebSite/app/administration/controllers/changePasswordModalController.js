crmAdministration.controller('changePasswordModalController', [

	'$scope', '$modalInstance', '$log', 'commandBus', 'user',
	function ($scope, $modalInstance, $log, commandBus, user) {

		$scope.Name = user.Name;
		$scope.Password = null;

		$scope.save = function () {

			var command = {
				UserId: user.Id,
				Password: $scope.Password
			};

			commandBus.send("ChangeUserPasswordCommand", command);

			$modalInstance.close();

		};

		$scope.cancel = function () {

			$modalInstance.dismiss('cancel');

		};

		var initialize = function () {

			$log.info('changePasswordModalController initialized');

		};

		initialize();
	}
]);