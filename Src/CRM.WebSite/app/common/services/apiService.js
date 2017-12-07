'use strict';

crmCommon.service('api', [

		'$resource',
		function($resource) {

			var get = function(resource, data, onSuccess, onFailure) {

				return resource.get(data || {}, onSuccess || angular.noop, onFailure || angular.noop);

			};

			var query = function(resource, data, onSuccess, onFailure) {

				return resource.query(data || {}, onSuccess || angular.noop, onFailure || angular.noop);

			};

			return {
				get: get,
				query: query,
				clients: {
					all: $resource('crm/api/clients/all'),
					client: $resource('crm/api/clients/id/:clientId', { clientId: '@clientId' }),
					search: $resource('crm/api/clients/search', { name: '@name' }),
				},
				referents: {
					all: $resource('crm/api/referents/all'),
					referent: $resource('crm/api/referents/id/:referentId', { referentId: '@referentId' }),
					search: $resource('crm/api/referents/search', { name: '@name' }),
				},
				relations: {
					search: $resource('crm/api/relations/search'),
					relation: $resource('crm/api/relations/id/:relationId', { relationId: '@relationId' }),
				},
				sellers: {
					all: $resource('crm/api/sellers/all')
				},
				administration: {
					users: $resource('crm/api/administration/users')
				}
			};

		}
	]);