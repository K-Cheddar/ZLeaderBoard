angular.module('adminApp')
  .controller('eventController', function ($scope, eventService, $stateParams, $location, $state) {

      $scope.model = {
          eventId: $stateParams.eventId,
          event: {
          },
          participants: [],
          participantName: undefined,
          createEvent: $state.current.data.createEvent
          //participantImage: undefined
      };

      var dateTimePicker = function () {
          return {
              restrict: "A",
              require: "ngModel",
              link: function (scope, element, attrs, ngModelCtrl) {
                  var parent = $(element).parent();
                  var dtp = parent.datetimepicker({
                      format: "LL",
                      showTodayButton: true
                  });
                  dtp.on("dp.change", function (e) {
                      ngModelCtrl.$setViewValue(moment(e.date).format("LL"));
                      scope.$apply();
                  });
              }
          };
      };

      if ($scope.model.eventId) {
          eventService.get($scope.model.eventId).success(function (event) {
              event.StartAt = new Date(event.StartAt);
              event.EndAt = new Date(event.EndAt);
              $scope.model.event = event;
              $scope.model.participants = event.Participants;
          }).error(function (err) {
              alert("Error");
          })
      }
      else {
          $scope.model.event.StartAt = new Date();
          $scope.model.event.EndAt = new Date();
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

  });