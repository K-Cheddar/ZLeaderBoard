angular.module('adminApp')
  .controller('adminLoginController', function ($scope, authService, $state, $rootScope) {

      $scope.model = {
          username: "",
          password: "",
          loginError: false,
          loading: false
      };

      $scope.login = function () {
          $scope.model.loading = true;
          authService
                .login($scope.model.username, $scope.model.password)
                .success(function () {
                    $scope.model.loading = false;
                })
                .error(function () {
                    $scope.model.username = "";
                    $scope.model.password = "";
                    $scope.model.loginError = true;
                    $scope.model.loading = false;
                });
      }
  });