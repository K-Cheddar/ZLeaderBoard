﻿angular.module('adminApp')
  .controller('userEventController', function ($cookies, $scope, userEventService, $stateParams,
                                                $state, $location, $route, $timeout, $interval, $filter) {

      $scope.model = {
          event: {},
          email: $cookies.get('user'),
          emailTmp: undefined,
          cookieTest: $cookies.get('user'),
          eventId: $stateParams.eventId,
          voteAllowed: $state.current.data.showButton,
          alreadyVoted: false,
          participants: {}
      };


      var repeat = $interval(reload, 5000);

      function reload() {
          if ($state.current.name == "userEventVote" || $state.current.name == "userEventView" || $state.current.name == "eventWinner") {
              if ($scope.model.eventId) {
                  userEventService.get($scope.model.eventId).success(function (event) {

                      var end = $filter('date')(new Date(event.EndAt), 'EEE MMM dd, yyyy hh:mm a');;
                      var c = $filter('date')(new Date(), 'EEE MMM dd, yyyy hh:mm a');
                      //alert(c + " > " + end);
                      if (end > c) {
                          $scope.model.voteAllowed = false;
                      }
                      $scope.model.event = event;
                      $scope.model.participants = event.Participants;

                  }).error(function (err) {
                      //alert("Error");
                  })
              }
          }
          else {
              $interval.cancel(repeat);
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
              participant.VotedFor = true;
              $scope.model.alreadyVoted = voted.Voted;
              $timeout(function () {
                  $scope.model.alreadyVoted = false;
              }, 1000)

          }).error(function (err) {
              //alert("Error");
          })
      }

      $scope.getTotal = function () {
          var total = 0;
          for (var i = 0; i < $scope.model.participants.length; i++) {
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
          return (voteCount / $scope.getMax()) * 100;
      }
  });