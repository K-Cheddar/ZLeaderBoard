'use strict';
angular.module('adminApp').factory('eventService', ['$http', 'myConfig', function ($http, myConfig) {

    var serviceBase = myConfig.apiUrl;

    var serviceFactory = {};



    serviceFactory = {
        get: function (id) {
            return $http.get(serviceBase + 'api/event?id=' + id);
        },
        all: function () {
            return $http.get(serviceBase + 'api/event');
        },
        post: function (event) {
            return $http.post(serviceBase + 'api/event', event);
        }

    }

    return serviceFactory;

}]);