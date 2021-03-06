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
          singleVoteProcess: false,
          participants: {},
          loading: true,
          voting: false
      };


      var repeat = $interval(reload, 5000);

      function reload() {
          if ($state.current.name == "userEventVote" || $state.current.name == "userEventView" || $state.current.name == "eventWinner") {
              if ($scope.model.eventId) {
                  userEventService.get($scope.model.eventId).success(function (event) {
                      var end = $filter('date')(new Date(event.EndAt), 'EEE MMM dd, yyyy hh:mm a');
                      var start = $filter('date')(new Date(event.StartAt), 'EEE MMM dd, yyyy hh:mm a');
                      var c = $filter('date')(new Date(), 'EEE MMM dd, yyyy hh:mm a');
                      end = new Date(end);
                      start = new Date(start);
                      c = new Date(c);
                      if (end < c || start > c) {
                          $scope.model.voteAllowed = false;
                      }
                      else if ($state.current.name != "userEventView") {
                          $scope.model.voteAllowed = true;
                      }
                      $scope.model.event = event;
                      $scope.model.participants = event.Participants;
                      $scope.model.loading = false;
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
          var expireDate = new Date();
          expireDate.setFullYear(expireDate.getFullYear() + 1);
          $cookies.put('user', $scope.model.emailTmp, { 'expires': expireDate });
          $scope.model.email = $scope.model.emailTmp;
      }

      $scope.vote = function (participant) {
          if (!$scope.model.event.MultipleVotes) {
              $scope.model.singleVoteProcess = true;
              for (var i = 0; i < $scope.model.participants.length; i++) {
                  if ($scope.model.participants[i].VotedFor) {

                      if ($scope.model.participants[i].VoteCount > 0) {
                          $scope.model.participants[i].VoteCount--;
                      }
                      else {
                          $scope.model.participants[i].VoteCount = 0;
                      }

                      $scope.model.participants[i].VotedFor = false;
                  }
              }
          }
          userEventService.put(participant.Id).success(function (voted) {
              participant.VoteCount = voted.VoteCount;
              participant.VotedFor = true;
              $scope.model.alreadyVoted = voted.Voted;
              $timeout(function () {
                  $scope.model.alreadyVoted = false;
              }, 1000)
              $scope.model.singleVoteProcess = false;

          }).error(function (err) {
              //alert("Error");
          })
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