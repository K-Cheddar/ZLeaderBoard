angular.module('adminApp')
  .controller('mainController', function ($scope, eventService, $state) {

      $scope.model = {
          name: "",
          events: [],
          event: undefined,
          sort: undefined
      };

      $scope.sorter = function (sort) {

          if ($scope.model.sort == sort) {
              $scope.model.sort = '-' + sort;
          }
          else {
              $scope.model.sort = sort;
          }
      }

      var init = function () {
          if (!$scope.$parent.model.authenticated) {
              $state.go("userHome");
          }
      }

      init();

      eventService.all().success(function (events) {
          $scope.model.events = events;
      }).error(function (err) {
          //alert("Error");
      })

      $scope.editEvent = function (eventId) {

          eventService.get(eventId).success(function(event){
              $scope.model.event = event;
              })
          
      };

  });