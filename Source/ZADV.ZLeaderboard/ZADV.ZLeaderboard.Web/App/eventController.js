angular.module('adminApp')
  .controller('eventController', function ($scope, eventService, $stateParams, $location, $state, $filter) {

      $scope.model = {
          eventId: $stateParams.eventId,
          event: {
          },
          participants: [],
          participantName: undefined,
          createEvent: $state.current.data.createEvent
          //participantImage: undefined
      };


      if ($scope.model.eventId) {
          eventService.get($scope.model.eventId).success(function (event) {
              event.StartAt = new Date(event.StartAt);
              event.EndAt = new Date(event.EndAt);
              //var _date = $filter('date')(new Date(event.StartAt), 'MMM dd, yyyy hh:mm a');
              //event.StartAt = _date;
              //var _date = $filter('date')(new Date(event.EndAt), 'MMM dd, yyyy hh:mm a');
              //event.EndAt = _date;
              $scope.model.event = event;
              $scope.model.participants = event.Participants;
          }).error(function (err) {
              alert("Error");
          })
      }
      else {

          $scope.model.event.StartAt = new Date();
          $scope.model.event.EndAt = new Date();
          $scope.model.event.IsActive = true;
      }

      $scope.save = function () {

          var checked;
          if ($scope.model.event.IsActive == null || $scope.model.event.IsActive == false) {
              checked = false;
          }
          else {
              checked = true;
          }

          var newEvent = {
              name: $scope.model.event.Name,
              startAt: $scope.model.event.StartAt,
              endAt: $scope.model.event.EndAt,
              isActive: checked,
              description: $scope.model.event.Description,
              participants: $scope.model.participants
          }

          if ($scope.model.eventId == null || $scope.model.eventId == 0) {
              eventService.post(newEvent).success(function () {

                  $location.path("/main");

              }).error(function (err) {
                  alert("Error");
              })
          }
          else {
              eventService.put($scope.model.eventId, newEvent).success(function () {

                  $location.path("/main");

              }).error(function (err) {
                  alert("Error");
              })
          }

      }
      $scope.inputOnTimeSet = function (newDate, oldDate) {
          console.log(newDate);
          console.log(oldDate);
      }
      $scope.addParticipant = function () {
          $scope.model.participants.push({
              Name: $scope.model.participantName,
              // image: $scope.model.participantImage
          });

          $scope.model.participantName = "";
          // $scope.model.participantImage = "";
      }

      $scope.removeParticipant = function (index) {
          $scope.model.participants.splice(index, 1);
      }
      $scope.ActiveBox = function (){
          $scope.model.event.IsActive = !$scope.model.event.IsActive;
      }
  });