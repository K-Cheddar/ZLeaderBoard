angular.module('adminApp', [
    'ui.router', 'ngCookies', 'ngRoute', 'angularBasicAuth', 'truncate', 'ui.bootstrap.datetimepicker'
])
.config(function ($stateProvider, $urlRouterProvider) {
    
    //
    // For any unmatched url, redirect to /state1
    $urlRouterProvider.otherwise("/userHome");
    //
    // Now set up the states
    $stateProvider
      .state('main', {
          url: "/main",
          templateUrl: "/App/admin/main.html",
          controller: "mainController"
      })
      .state('eventCreate', {
          url: "/eventCreate/",
          templateUrl: "/App/admin/event.html",
          controller: "eventController",
          data: {
              createEvent: true
          }
      })
        .state('eventEdit', {
            url: "/eventEdit/:eventId",
            templateUrl: "/App/admin/event.html",
            controller: "eventController",
            data: {
                createEvent: false
            }
        })
    .state('userHome', {
        url: "/userHome",
        templateUrl: "/App/user/userHome.html",
        controller: "userHomeController"
    })
    .state('userEventView', {
        url: "/userEventView/:eventId",
        templateUrl: "/App/user/userEvent.html",
        controller: "userEventController",
        data: {
            showButton: false
        }
    })
    .state('eventWinner', {
        url: "/eventWinner/:eventId",
        templateUrl: "/App/user/eventWinner.html",
        controller: "userEventController",
        data: {
            showButton: true
        }
    })
    .state('userEventVote', {
        url: "/userEventVote/:eventId",
        templateUrl: "/App/user/userEvent.html",
        controller: "userEventController",
        data: {
            showButton: true
        }
    })
    .state('adminLogin', {
        url: "/adminLogin",
        templateUrl: "/App/admin/adminLogin.html",
        controller: "adminLoginController"
    })

    //data: {
    //    authorizedRoles: [USER_ROLES.admin]
    //}
})
    .constant("myConfig", {
        'apiUrl': 'http://localhost:60919/'
    });