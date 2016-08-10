angular.module('adminApp')
  .controller('userHomeController', function ($scope, userEventService, $stateParams, $location) {

      $scope.model = {
          events: [],
          showUpcoming: false,
          showPast: false,
      };

      userEventService.all().success(function (events) {
          $scope.model.events = events;
      }).error(function (err) {
          //alert("Error");
      })

      $scope.showHide = function (state){
          if (state == 'Upcoming') {
              $scope.model.showUpcoming = !$scope.model.showUpcoming;
          }
          if (state == 'Past') {
              $scope.model.showPast = !$scope.model.showPast;
          }
      }

  });