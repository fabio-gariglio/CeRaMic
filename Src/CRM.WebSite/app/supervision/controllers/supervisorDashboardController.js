crmSupervision.controller('supervisorDashboardController', [

	'$scope', '$rootScope', '$log', '$window', '$modal', 'api', 'sellersCatalog',
	function ($scope, $rootScope, $log, $window, $modal, api, sellersCatalog) {

		$scope.relations = [];
		$scope.query = null;
		$scope.loading = false;

		$scope.loadRelations = function() {

			if ($scope.loading) return;

			$scope.loading = true;

			var onRelationsLoaded = function(data) {

				for (var i = 0, len = data.length; i < len; i++) {
					$scope.relations.push(data[i]);
				}

				$scope.loading = false;

			};

			var parameter = {
				limit: Math.round($window.innerHeight / 60),
				skip: $scope.relations.length,
				fragment: $scope.query || ""
			};

			api.query(api.relations.search, parameter, onRelationsLoaded);

		};

		$scope.search = function() {

			$scope.relations = [];
			$scope.loadRelations();

		};

		$scope.createReferent = function () {

			$modal.open({
				templateUrl: "app/common/views/relationModal.html",
				controller: 'relationModalController',
				size: "lg",
				resolve: {
					relationId: function () {
						return null;
					}
				}
			});

		};

		$scope.showRelation = function(relationId) {

			$modal.open({
				templateUrl: "app/common/views/relationModal.html",
				controller: 'relationModalController',
				size: "lg",
				resolve: {
					relationId: function () {
						return relationId;
					}
				}
			});

		};

		var initialize = function() {

			$log.info('supervisorDashboardController initialized');

		};

		function getRelationById(id) {

			var relationSelector = function(relation) {
				return relation.Id == id;
			};

			return Enumerable.From($scope.relations).FirstOrDefault(null, relationSelector);

		};

		function onRelationOwnerApproved(event, data) {

			var relation;

			if ((relation = getRelationById(data.AggregateId)) == null) return;

			relation.OwnerName = sellersCatalog.getSellerNameById(data.OwnerId);

		};

		function onRelationOwnerRejected(event, data) {

			var relation;

			if ((relation = getRelationById(data.AggregateId)) == null) return;

			relation.OwnerName = null;

		};

		function onRelationPartnerApproved(event, data) {

			var relation;

			if ((relation = getRelationById(data.AggregateId)) == null) return;

			relation.PartnerName = sellersCatalog.getSellerNameById(data.PartnerId);

		};

		function onRelationPartnerRejected(event, data) {

			var relation;

			if ((relation = getRelationById(data.AggregateId)) == null) return;

			relation.PartnerName = null;

		};

		$rootScope.$on("RelationOwnerApproved", onRelationOwnerApproved);
		$rootScope.$on("RelationOwnerRejected", onRelationOwnerRejected);
		$rootScope.$on("RelationPartnerApproved", onRelationPartnerApproved);
		$rootScope.$on("RelationPartnerRejected", onRelationPartnerRejected);

		initialize();
	}
]);