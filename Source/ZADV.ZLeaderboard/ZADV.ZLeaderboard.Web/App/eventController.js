angular.module('adminApp')
  .controller('eventController', function ($scope, eventService, $stateParams) {

      $scope.model = {
          eventId: $stateParams.eventId,
          event: {
          },
          participants: [],
          participantName: undefined,
          participantImage: undefined
      };

      if ($scope.model.eventId) {
          eventService.get($scope.model.eventId).success(function (event) {
              $scope.model.event = event;
              participants = event.Participants;
          }).error(function (err) {
              alert("Error");
          })
      }

      $scope.save = function (newEvent) {
          eventService.post(newEvent).success(function () {
              //$location.url("/main");
              
          }).error(function (err) {
              alert("Error");
          })
      }

      $scope.addParticipant = function () {
          $scope.model.participants.push({
              name: $scope.model.participantName,
              image: $scope.model.participantImage
          });

          function readURL(input) {
              if (input.files && input.files[0]) {
                  var reader = new FileReader();
                  reader.onload = function (e) {
                      $('#pImage').attr('src', e.target.result);
                  }
                  reader.readAsDataURL(input.files[0]);
              }
          }

          $("#participantName").change(function () {
              readURL(this);
          });

          $scope.model.participantName = "";
          $scope.model.participantImage = "";
      }

      $scope.removeParticipant = function (index) {
          $scope.model.participants.splice(index, 1);
      }

      $scope.add = function ($form) {
          if ($form.$valid) {
              $form.commit();
          }
      };

  });