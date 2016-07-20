'use strict';
angular.module('adminApp').factory('userEventService', ['$http', 'myConfig', function ($http, myConfig) {

    var serviceBase = myConfig.apiUrl;

    var serviceFactory = {};



    serviceFactory = {
        get: function (id) {
            return $http.get(serviceBase + 'api/userEvent?id=' + id);
        },
        all: function () {
            return $http.get(serviceBase + 'api/userEvent');
        },
        post: function (event) {
            return $http.post(serviceBase + 'api/userEvent', event);
        },
        put: function (id) {
            return $http.put(serviceBase + 'api/userEvent?id=' + id);
        }

    }

    return serviceFactory;

}]);