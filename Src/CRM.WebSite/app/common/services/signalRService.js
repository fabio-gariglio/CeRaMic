'use strict';

crmCommon.service('signalR', [

		'$rootScope', '$timeout', '$log',
		function ($rootScope, $timeout, $log) {

			var hubConnection = $.connection.hub;
			var crmHub = $.connection.crmHub;

			//crmHub.stateChanged(function (change) {

			//	$log.log("connection.stateChanged:" + change.newState);
			//	$rootScope.$emit("signalR.stateChanged", change.newState);

			//});

			crmHub.client.onEventRaised = function (event) {

				$log.log("Notification Received: ", event);

				//$log.log("signalRService - onEventRaised", obj);
				$rootScope.$emit(event.Name, event.Data);

			};

			var bootstrap = function() {
				hubConnection.start().done(function () {
					$log.log('signalRService - bootstrapped');
				});
			};

			return {
				bootstrap: bootstrap
			};

		}]);

