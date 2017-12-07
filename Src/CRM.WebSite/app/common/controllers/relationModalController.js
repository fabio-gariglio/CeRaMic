crmSales.controller('relationModalController', [

	'$scope', '$modalInstance', '$log', 'api', 'commandBus', 'relationId',
	function ($scope, $modalInstance, $log, api, commandBus, relationId) {

		var onSaveDelegate = null;
		var templates = {
			relation: "app/common/views/relationDetail.html",
			referent: "app/common/views/referentDetail.html"
		};

		$scope.relationId = relationId;
		$scope.referentId = null;
		$scope.template = null;
		$scope.Headline = null;
		$scope.SubHeadline = null;
		
		$scope.setModalTitle = function(headline, subHeadline) {

			$scope.Headline = headline;
			$scope.SubHeadline = subHeadline;

		};

		$scope.showReferentInfo = function(referentId) {
			$scope.referentId = referentId;
			$scope.template = templates.referent;
		};

		$scope.setOnSaveDelegate = function(delegate) {

			onSaveDelegate = delegate;

		};

		$scope.save = function ($form) {

			if ($form.$dirty) onSaveDelegate($form);

			$modalInstance.close();

		};

		$scope.cancel = function() {

			$modalInstance.dismiss('cancel');

		};

		var initialize = function () {

			$scope.template = relationId
				? templates.relation
				: templates.referent;

			$log.info('relationModalController initialized');

		};

		initialize();
	}
]);