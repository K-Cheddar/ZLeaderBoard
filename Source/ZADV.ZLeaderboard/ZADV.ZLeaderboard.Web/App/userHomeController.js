angular.module('adminApp')
  .controller('userHomeController', function ($scope, userEventService, $stateParams, $location) {

      $scope.model = {
          events: []
      };

      userEventService.all().success(function (events) {
          $scope.model.events = events;
      }).error(function (err) {
          alert("Error");
      })

  });