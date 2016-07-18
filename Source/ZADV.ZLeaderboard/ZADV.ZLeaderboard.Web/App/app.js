angular.module('adminApp', [
    'ui.router'
])
.config(function ($stateProvider, $urlRouterProvider) {
    //
    // For any unmatched url, redirect to /state1
    $urlRouterProvider.otherwise("/main");
    //
    // Now set up the states
    $stateProvider
      .state('main', {
          url: "/main",
          templateUrl: "/App/main.html",
          controller: "mainController"
      })
      .state('event', {
          url: "/event/:eventId",
          templateUrl: "/App/event.html",
          controller: "eventController"
      })
})
    .constant("myConfig", {
        'apiUrl': 'http://localhost:60919/'
    });