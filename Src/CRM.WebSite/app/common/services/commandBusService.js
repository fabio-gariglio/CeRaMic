'use strict';

crmCommon.service('commandBus', [

		'$rootScope', '$http', '$log',
		function ($rootScope, $http, $log) {

			var send = function(name, command) {

				command.CommandId = uuid.v4();

				var parameter = {
					Name: name,
					Body: angular.toJson(command)
				};

				$log.log("Command Sending: ", parameter);

				$http.post('/crm/api/commands/send', parameter)
					.error(function(data) {
						$rootScope.$emit("CommandFailed", { Name: name, Command: command, Response: data });
					});
				
				return command.CommandId;

			};

			return {
				send: send
			};

		}
	]);