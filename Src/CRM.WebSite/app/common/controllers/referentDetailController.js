crmSales.controller('referentDetailController', [

	'$scope', '$log', 'api', 'commandBus', 
	function ($scope, $log, api, commandBus) {

		$scope.referent = {};
		$scope.showOtherInfo = false;

		$scope.getClient = function (name) {

			var result = api
				.query(api.clients.search, { name: name })
				.$promise
				.then(function (data) {
					return data.slice(0, 6);
				});

			$log.log(result);

			return result;

		};

		var initialize = function () {

			var onClientLoaded = function(client) {

				$scope.referent.Client = client;

			};

			var onReferentLoaded = function(referent) {

				$scope.setModalTitle(referent.Name);

				api.get(api.clients.client, { clientId: referent.ClientId }, onClientLoaded);

				$scope.referent = referent;

			};

			if($scope.referentId){
				api.get(api.referents.referent, { referentId: $scope.referentId }, onReferentLoaded);
			}

			var onSaveDelegate = function () {

				var command = {
					FirstName: $scope.referent.FirstName,
					LastName: $scope.referent.LastName,
					ClientName: $scope.referent.Client.Name || $scope.referent.Client,
					Area: $scope.referent.Area,
					Secretary: $scope.referent.Secretary,
					EmailAddress: $scope.referent.EmailAddress,
					MobilePhone: $scope.referent.MobilePhone,
					LandlineNumber: $scope.referent.LandlineNumber
				};

				if ($scope.referentId) {
					command.Id = $scope.referentId;
					commandBus.send("UpdateReferentCommand", command);
				} else {
					commandBus.send("CreateReferentCommand", command);
				}

			};

			$scope.setOnSaveDelegate(onSaveDelegate);

			$log.info('referentDetailController initialized');

		};

		initialize();
	}
]);