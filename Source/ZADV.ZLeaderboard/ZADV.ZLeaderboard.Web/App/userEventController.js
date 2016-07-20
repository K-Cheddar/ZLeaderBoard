angular.module('adminApp')
  .controller('userEventController', function ($scope, userEventService, $stateParams, $state, $location) {

      $scope.model = {
          event: {},
          eventId: $stateParams.eventId,
          voteAllowed: $state.current.data.showButton
      };

      if ($scope.model.eventId) {
          userEventService.get($scope.model.eventId).success(function (event) {
              $scope.model.event = event;
              $scope.model.participants = event.Participants;
          }).error(function (err) {
              alert("Error");
          })
      }

      $scope.vote = function (id) {

          var cookie = 

          userEventService.put(id).success(function () {

          }).error(function (err) {
              alert("Error");
          })
      }



  });