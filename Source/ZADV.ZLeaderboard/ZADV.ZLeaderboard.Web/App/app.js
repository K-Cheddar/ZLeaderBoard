angular.module('adminApp', [
    'ui.router', 'ngCookies', 'ngRoute'
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
      .state('eventCreate', {
          url: "/eventCreate/",
          templateUrl: "/App/event.html",
          controller: "eventController",
          data: {
              createEvent: true
          }
      })
        .state('eventEdit', {
            url: "/eventEdit/:eventId",
            templateUrl: "/App/event.html",
            controller: "eventController",
            data: {
                createEvent: false
            }
        })
    .state('userHome', {
        url: "/userHome",
        templateUrl: "/App/userHome.html",
        controller: "userHomeController"
    })
    .state('userEventView', {
        url: "/userEventView/:eventId",
        templateUrl: "/App/userEvent.html",
        controller: "userEventController",
        data: {
            showButton: false
        }
    })
    .state('userEventVote', {
        url: "/userEventVote/:eventId",
        templateUrl: "/App/userEvent.html",
        controller: "userEventController",
        data: {
            showButton: true
        }
    })
})
    .constant("myConfig", {
        'apiUrl': 'http://localhost:60919/'
    });