crm.controller('mainController', [

	'$rootScope', '$scope', '$log', '$modal', 'signalR',
	function ($rootScope, $scope, $log, $modal, signalR) {

		$scope.view = 'app/views/main.html';

		$rootScope.user = window.user;
		$rootScope.isUserSeller = window.user.Role == "Seller";
		$rootScope.isUserSupervisor = window.user.Role == "Supervisor";
		$rootScope.isUserAdministrator = window.user.Role == "Administrator";
		//$scope.events = [];
		//$scope.newEvents = false;

		function initialize() {

			signalR.bootstrap();
		
			$log.info('mainController initialized');

		};

		//function appendEvent(event) {
			
		//	if ($scope.events.length > 3) {
		//		$scope.events.splice(0, $scope.events.length - 3);
		//	}

		//	$scope.events.push(event);
		//	$scope.newEvents = true;

		//};

		$rootScope.$on('CommandFailed', function(event, data) {
			$modal.open({
				templateUrl: "app/common/views/commandFailedModal.html",
				controller: 'commandFailedModalController',
				size: "sm",
				resolve: {
					data: function () {
						return data;
					}
				}
			});
		});

		$scope.export = function() {

			$modal.open({
				templateUrl: "app/supervision/views/exportModal.html",
				controller: 'exportModalController',
				size: "sm"
			});

		};

		initialize();
	}
]);