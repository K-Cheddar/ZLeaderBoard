angular.module('adminApp')
  .controller('userEventController', function ($cookies, $scope, userEventService, $stateParams, $state, $location, $route, $timeout, $interval) {

      $scope.model = {
          event: {},
          email: $cookies.get('user'),
          emailTmp: undefined,
          cookieTest: $cookies.get('user'),
          eventId: $stateParams.eventId,
          voteAllowed: $state.current.data.showButton,
          alreadyVoted: false
      };

      if (!$scope.model.voteAllowed) {
          $interval(reload, 5000);
      }

      function reload() {
          if ($scope.model.eventId) {
              userEventService.get($scope.model.eventId).success(function (event) {
                  $scope.model.event = event;
                  $scope.model.participants = event.Participants;
              }).error(function (err) {
                  alert("Error");
              })
          }
      };

      reload();


      $scope.saveEmail = function () {
          $cookies.put('user', $scope.model.emailTmp);
          $scope.model.email = $scope.model.emailTmp;

      }

      $scope.vote = function (participant) {

          userEventService.put(participant.Id).success(function (voted) {
              participant.VoteCount = voted.VoteCount;
              $scope.model.alreadyVoted = voted.Voted;
              $timeout(function () {
                  $scope.model.alreadyVoted = false;
              }, 1000)

          }).error(function (err) {
              alert("Error");
          })
      }

      $scope.getTotal = function () {
          var total = 0;
          for (var i = 0; i < $scope.model.participants.length; i++){
              var count = $scope.model.participants[i];
              total += count.VoteCount;
          }
          return total;
      }

      $scope.getMax = function () {
          var max = 0;
          for (var i = 0; i < $scope.model.participants.length; i++) {
              var count = $scope.model.participants[i];
              if (count.VoteCount > max) {
                  max = count.VoteCount;
              }
          }
          return max;
      }

      $scope.getPercent = function (voteCount) {
          return (voteCount / $scope.getMax())*100;
      }
  });