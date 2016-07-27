angular.module('adminApp')
  .controller('authController', function ($scope, $state, authDefaults, authService) {

      authDefaults.authenticateUrl = '/api/event';
      authService.addEndpoint();
      $scope.model = {
          authenticated: authService.username()
      };

      $scope.$on("login", function () {
          $scope.model.authenticated = true;
          $state.go("main");
      })

      $scope.$on("logout", function () {
          $scope.model.authenticated = false;
      })

      $scope.logout = function () {
          $scope.model.authenticated = false;
          authService.logout();
          $state.go("userHome");
      }

  });