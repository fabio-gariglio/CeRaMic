'use strict';

crmCommon.service('sellersCatalog', [
	'api', function(api) {

		var sellers = api.query(api.sellers.all, {}, function(data) { sellers = data; });

		function getSellerById(id) {

			var sellerSelector = function(seller) { return seller.Id == id; };

			return Enumerable.From(sellers).FirstOrDefault(null, sellerSelector);

		};

		function getSellerNameById(id) {

			var seller = getSellerById(id);

			return seller? seller.Name: null;

		};

		return {
			sellers: sellers,
			getSellerById: getSellerById,
			getSellerNameById: getSellerNameById
		};

	}
]);