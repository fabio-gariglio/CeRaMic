crmCommon.controller('relationDetailController', [

	'$rootScope', '$scope', '$log', 'api', 'commandBus', 'sellersCatalog',
	function ($rootScope, $scope, $log, api, commandBus, sellersCatalog) {

		$scope.relation = {};
		$scope.isSupervisor = null;
		$scope.sellers = [];
		$scope.priority = 0;

		$scope.clearPriority = function() {
			$scope.priority = 0;
			$scope.Form.$dirty = true;
		};

		var initialize = function () {

			var onRelationLoaded = function (data) {

				data.Owner = sellersCatalog.getSellerById(data.OwnerId);
				data.Partner = sellersCatalog.getSellerById(data.PartnerId);
				$scope.priority = data.Priority;
				$scope.relation = data;

				$scope.setModalTitle(data.ReferentName, data.ClientName);
			};

			var parameter = { relationId: $scope.relationId };
			
			api.get(api.relations.relation, parameter, onRelationLoaded);

			var onSaveDelegate = function($form) {

				var command = {
					RelationId: $scope.relationId,
					Priority: $scope.priority
				};

				if ($form.Owner.$dirty) {
					command.OwnerId = $scope.relation.Owner.Id;
				}

				if ($form.Partner.$dirty) {
					command.PartnerId = $scope.relation.Partner.Id;
				}

				if ($form.Note.$dirty) {
					command.NoteId = $scope.relation.NoteId;
					command.NoteContent = $scope.relation.NoteContent;
				}

				commandBus.send("UpdateRelationCommand", command);

			};

			$scope.setOnSaveDelegate(onSaveDelegate);

			$scope.isSupervisor = $rootScope.isUserSupervisor;

			$scope.sellers = sellersCatalog.sellers;
			$scope.sellers.unshift({ Id: Consts.NullId, Name: "" });

			$log.info('relationDetailController initialized');

		};

		initialize();
	}
]);