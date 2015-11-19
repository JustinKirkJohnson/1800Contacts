'use strict';

var classControllers = angular.module('classControllers', []);

classControllers.controller('ClassScheduleController', ['$scope', '$http',

    function ($scope, $http) {

    	$scope.classList = "Test class 1:\r\n" +
    						"Test class 2:Test class 1\r\n" +
    						"Test class 3:Test class 1\r\n" +
    						"Test class 4:Test class 2";

	    $scope.processClasses = function () {

	    	var jsonFormatedRequestBody = "[\"" + $scope.classList.replace(/[\r\n]+/g, '","') + "\"]";

	    	$http({
	    		method: 'POST',
	    		url: 'api/classes/sortByPrerequisite',
	    		data: jsonFormatedRequestBody
	    	}).then(function successCallback(response) {

			    $scope.showSuccess = true;
				$scope.showError = false;
			    $scope.sortedClasses = response.data;

		    }, function errorCallback(response) {

	    		$scope.showSuccess = false;
	    		$scope.sortedClasses = "";

	    		if (!isJsonResponse(response.data)) {
	    			$scope.processingError = "An error occured! " + response.data;
				    return;
	    		}

	    		$scope.errorMessage = response.data.ExceptionMessage;
	    		$scope.showError = true;

	    	});

	    };
    }

]);

function isJsonResponse(object) {
	var objectConstructor = {}.constructor;

	if (object === null || object === undefined) {
		return false;
	}

	if (object.constructor === objectConstructor) {
		return true;
	}
	else {
		return false;
	}
}

