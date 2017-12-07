crmAdministration.controller('adminController', [
	'$scope', '$upload', '$log', '$modal', 'api', 'commandBus',
	function ($scope, $upload, $log, $modal, api, commandBus) {

		$scope.users = [];
		$scope.rebuildProjections = function() {

			commandBus.send("RebuildAllProjectionsCommand", {});

		};

		$scope.onFileSelect = function($files) {

			var file = $files[0];

			if (file) {
				var reader = new FileReader();

				reader.onload = function (e) {

					var command = { csvContent: e.target.result };

					commandBus.send("ImportCsvCommand", command);
					
				};

				reader.readAsText(file);

			};

		};

		$scope.onUserFileSelect = function ($files) {

			var file = $files[0];

			if (file) {
				var reader = new FileReader();

				reader.onload = function (e) {

					var command = { usersJsonContent: e.target.result };

					commandBus.send("ImportUsersCommand", command);

				};

				reader.readAsText(file);

			};

		};

		$scope.changePassword = function(user) {

			$modal.open({
				templateUrl: "app/administration/views/changePasswordModal.html",
				controller: 'changePasswordModalController',
				resolve: {
					user: function () {
						return user;
					}
				}
			});

		};

		var initialize = function() {

			$scope.users = api.query(api.administration.users);

			$log.info('adminController initialized');

		};

		initialize();
	}
]);