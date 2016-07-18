angular.module('adminApp')
  .controller('mainController', function ($scope, eventService) {

      $scope.model = {
          name: "",
          events: [],
          event: undefined
      };

      eventService.all().success(function (events) {
          $scope.model.events = events;
      }).error(function (err) {
          alert("Error");
      })

      $scope.editEvent = function (eventId) {

          eventService.get(eventId).success(function(event){
              $scope.model.event = event;
              })
          
      };

      $scope.message = function () {
          return "Hello " + ($scope.model.name || "");
      }
  });