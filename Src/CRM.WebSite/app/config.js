var crm = angular.module('crm', [
	'ngResource',
	'ui.router',
	'ui.bootstrap',
	'angularFileUpload',
	'infinite-scroll',
	'crm.common',
	'crm.sales',
	'crm.supervision',
	'crm.administration'
]);

var crmCommon = angular.module('crm.common', ['ui.bootstrap']);
var crmSales = angular.module('crm.sales', ['crm.common', 'ui.bootstrap']);
var crmSupervision = angular.module('crm.supervision', ['crm.common']);
var crmAdministration = angular.module('crm.administration', ['crm.common']);

crm.config(function ($stateProvider, $urlRouterProvider) {

	var administrationRouting = function() {

		$urlRouterProvider.otherwise("/crm/administration");
		
		$stateProvider
			.state('crm.administration', {
				url: "/administration",
				templateUrl: "app/administration/views/home.html"
			});

	};

	var supervisionRouting = function() {

		$urlRouterProvider.otherwise("/crm/supervision/dashboard");
		
		$stateProvider
			.state('crm.supervision', {
				url: "/supervision",
				abstract: true,
				templateUrl: "app/supervision/views/home.html"
			})
			.state('crm.supervision.dashboard', {
				url: "/dashboard",
				templateUrl: "app/supervision/views/supervisorDashboard.html"
			});

	};

	var salesRouting = function() {

		$urlRouterProvider.otherwise("/crm/sales/dashboard");

		$stateProvider
			.state('crm.sales', {
				url: "/sales",
				templateUrl: "app/sales/views/home.html"
			})
			.state('crm.sales.dashboard', {
				url: "/dashboard",
				templateUrl: "app/sales/views/sellerDashboard.html"
			});
	};

	$stateProvider
		.state('crm', {
			url: "/crm",
			abstract: true,
			templateUrl: "app/views/main.html",
			resolve: {
				'sellersCatalogData': function(sellersCatalog) {
					return sellersCatalog.sellers;
				}
			}
		});

	switch(window.user.Role) {
		case "Administrator":
			administrationRouting();
			break;
		case "Supervisor":
			supervisionRouting();
			break;
		case "Seller":
			salesRouting();
			break;
	}
	
});