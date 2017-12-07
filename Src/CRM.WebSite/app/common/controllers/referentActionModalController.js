crmSales.controller('referentActionModalController', [

	'$scope', '$modalInstance', '$log', 'api', 'referentId',
	function ($scope, $modalInstance, $log, api, referentId) {

		$scope.referent = null;
		$scope.actions = [];

		$scope.cancel = function() {

			$modalInstance.dismiss('cancel');

		};

		$scope.send = function(address) {

			window.location = "mailto:" + address;

		};

		$scope.call = function (number) {

			window.location = "tel:+" + number;

		};

		var initialize = function () {

			var onReferentLoaded = function(referent) {

				$scope.referent = referent;
				var action;

				if (referent.EmailAddress) {

					action = {
						Icon: "envelope",
						Label: referent.EmailAddress,
						Link: "mailto:" + referent.EmailAddress
					};

					$scope.actions.push(action);

				}

				if (referent.MobilePhone) {

					action = {
						Icon: "mobile-phone",
						Label: referent.MobilePhone,
						Link: "tel:+" + referent.MobilePhone
					};

					$scope.actions.push(action);

				}

				if (referent.LandlineNumber) {

					action = {
						Icon: "phone",
						Label: referent.LandlineNumber,
						Link: "tel:+" + referent.LandlineNumber
					};

					$scope.actions.push(action);

				}

			};

			api.get(api.referents.referent, { referentId: referentId }, onReferentLoaded);

			$log.info('referentActionModalController initialized');

		};

		initialize();
	}
]);