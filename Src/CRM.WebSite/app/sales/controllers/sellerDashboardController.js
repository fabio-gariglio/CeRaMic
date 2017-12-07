crmSales.controller('sellerDashboardController', [

	'$scope', '$rootScope', '$log', '$window', '$modal', 'api',
	function ($scope, $rootScope, $log, $window, $modal, api) {

		$scope.relations = [];
		$scope.loading = false;
		$scope.query = null;

		$scope.getRelationType = function(relation) {

			return Enums.Parse(Enums.RelationType, relation.Type);

		};

		$scope.loadRelations = function () {

			if ($scope.loading) return;

			$scope.loading = true;

			var onRelationsLoaded = function(data) {

				for (var i = 0, len = data.length; i < len; i++) {
					$scope.relations.push(data[i]);
				}

				$scope.loading = false;

			};

			var parameter = {
				limit: Math.round($window.innerHeight / 40),
				skip: $scope.relations.length,
				fragment: $scope.query || ""
			};

			api.query(api.relations.search, parameter, onRelationsLoaded);

		};

		$scope.showRelation = function (relationId) {

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

		$scope.forceSearch = function(query) {
			$rootScope.$emit("forceSearch", query);			
		};

		$rootScope.$on("search", function(event, data) {

			$scope.query = data;
			$scope.relations = [];
			$scope.loadRelations();
			
		});

		$rootScope.$on("add", function () {

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

		});

		$rootScope.$on("RelationPriorityChanged", function (event, data) {

			var getRelationById = function (relationId) {

				return Enumerable.From($scope.relations)
					.FirstOrDefault(null, function (x) {
						return x.Id == relationId;
					});

			};

			var relation = getRelationById(data.AggregateId);

			if (relation) relation.Priority = data.Priority;

			$scope.$apply();

		});

		var initialize = function() {

			$log.info('sellerDashboardController initialized');

		};

		initialize();
	}
]);