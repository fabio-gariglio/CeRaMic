crmCommon.directive('crmActionBar', function() {
	return {
		templateUrl: 'app/common/views/actionBar.html',
		scope: {
			relation: "="
		},
		link: function (scope, element, attrs) {

			scope.send = function (address) {

				window.location = "mailto:" + address;

			};

			scope.call = function (number) {

				window.location = "tel:+" + number;

			};
		}
};
});