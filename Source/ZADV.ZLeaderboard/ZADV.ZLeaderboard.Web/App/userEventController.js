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
          $interval(reload, 3000);
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

          //var cookie = 


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
  });