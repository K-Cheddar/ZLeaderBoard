angular.module('adminApp')
  .controller('eventController', function ($scope, eventService, $stateParams, $location, $state, $filter, $interval) {

      $scope.model = {
          eventId: $stateParams.eventId,
          event: {
          },
          participants: [],
          participantName: undefined,
          createEvent: $state.current.data.createEvent,
          participantClicked: false,
          eventNameClicked: false,
          submitValid: true,
          datesValid: true,
          startDateTooEarly: false,
          endDateTooEarly: false,
          incorrectStartDateFormat: false,
          incorrectEndDateFormat: false,
          //participantImage: undefined
      };

      var init = function () {
          if (!$scope.$parent.model.authenticated) {
              $state.go("userHome");
          }
      }

      init();

      $scope.clicked = function (element) {
          if (element == 'participant') {
              $scope.model.participantClicked = true;
          }
          if (element == 'eventName') {
              $scope.model.eventNameClicked = true;
          }

      }

      $interval(timeCheck, 60000);

      function timeCheck (){
          $scope.timeValidate();
      }

      if ($scope.model.eventId) {
          eventService.get($scope.model.eventId).success(function (event) {
              event.StartAt = new Date(event.StartAt);
              event.EndAt = new Date(event.EndAt);
              $scope.model.event = event;
              $scope.model.participants = event.Participants;
          }).error(function (err) {
              alert("You don't have access to that.");
          })
      }
      else {

          $scope.model.event.StartAt = new Date();
          $scope.model.event.EndAt = new Date();
          $scope.model.event.IsActive = true;
      }

      $scope.save = function () {
          $scope.model.submitValid = false;
          $scope.timeValidate();
          var checked;
          if ($scope.model.event.IsActive == null || $scope.model.event.IsActive == false) {
              checked = false;
          }
          else {
              checked = true;
          }

          var newEvent = {
              name: $scope.model.event.Name,
              startAt: $filter('date')(new Date($scope.model.event.StartAt), 'EEE MMM dd, yyyy hh:mm a'),
              endAt: $filter('date')(new Date($scope.model.event.EndAt), 'EEE MMM dd, yyyy hh:mm a'),
              isActive: checked,
              description: $scope.model.event.Description,
              participants: $scope.model.participants
          }

          if ($scope.model.eventId == null || $scope.model.eventId == 0) {
              eventService.post(newEvent).success(function () {
                  $scope.model.submitValid = true;
                  $location.path("/main");

              }).error(function (err) {
                  alert("You don't have access to that.");
              })
          }
          else {
              eventService.put($scope.model.eventId, newEvent).success(function () {
                  $scope.model.submitValid = true;
                  $location.path("/main");

              }).error(function (err) {
                  alert("You don't have access to that.");
              })
          }

      }

      $scope.addParticipant = function () {
          $scope.model.participants.push({
              Name: $scope.model.participantName,
              // image: $scope.model.participantImage
          });

          $scope.model.participantName = "";
          $scope.model.participantClicked = false;
          // $scope.model.participantImage = "";
      }

      $scope.removeParticipant = function (index) {
          $scope.model.participants.splice(index, 1);
      }
      $scope.ActiveBox = function (){
          $scope.model.event.IsActive = !$scope.model.event.IsActive;
      }

      $scope.timeValidate = function(){
          var s = new Date($scope.model.event.StartAt);
          var e = new Date($scope.model.event.EndAt);
          var c = new Date();
          if (s.getTime() > e.getTime() ) {
              $scope.model.datesValid = false;
          }
          else {
              $scope.model.datesValid = true;
          }
          if ($scope.model.createEvent) {
              if (s.getTime() < c.getTime()) {
                  $scope.model.startDateTooEarly = true;
              }
              else {
                  $scope.model.startDateTooEarly = false;
              }
          }

          if ( e.getTime() < c.getTime()) {
              $scope.model.endDateTooEarly = true;
          }
          else {
              $scope.model.endDateTooEarly = false;
          }
          if (s.toString() == "Invalid Date") {
              $scope.model.incorrectStartDateFormat = true;
          }
          else {
              $scope.model.incorrectStartDateFormat = false;
          }
          if (e.toString() == "Invalid Date") {
              $scope.model.incorrectEndDateFormat = true;
          }
          else {
              $scope.model.incorrectEndDateFormat = false;
          }
      }

      $scope.onTimeSet = function (time) {
          if (time == 's') {
              $scope.model.event.StartAt = $filter('date')(new Date($scope.model.event.StartAt), 'EEE MMM dd, yyyy hh:mm a');
          }
          else {
              $scope.model.event.EndAt = $filter('date')(new Date($scope.model.event.EndAt), 'EEE MMM dd, yyyy hh:mm a');
          }
          $scope.timeValidate();
      }
      $scope.beforeRender = function (time) {
          $scope.timeValidate();
          if (time == 's') {
              $scope.model.event.StartAt = $filter('date')(new Date($scope.model.event.StartAt), 'EEE MMM dd, yyyy hh:mm a');
          }
          else {
              $scope.model.event.EndAt = $filter('date')(new Date($scope.model.event.EndAt), 'EEE MMM dd, yyyy hh:mm a');
          }

      }
  });