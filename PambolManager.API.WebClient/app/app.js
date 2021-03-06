﻿var app = angular.module('PambolManagerApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/torneos", {
        controller: "tournamentsController",
        templateUrl: "/app/views/tournaments.html"
    });

    $routeProvider.when("/torneos/crear", {
        controller: "tournamentsController",
        templateUrl: "/app/views/createTournament.html"
    });

    $routeProvider.when("/torneos/:selectedTournamentId", {
        controller: "tournamentsController",
        templateUrl: "/app/views/tournamentOptions.html"
    });

    $routeProvider.when("/torneos/:selectedTournamentId/editar", {
        controller: "tournamentsController",
        templateUrl: "/app/views/editTournament.html"
    });

    $routeProvider.when("/torneos/:selectedTournamentId/equipos", {
        controller: "teamsController",
        templateUrl: "/app/views/teams.html"
    });

    $routeProvider.when("/torneos/:selectedTournamentId/equipos/crear", {
        controller: "teamsController",
        templateUrl: "/app/views/createTeam.html"
    });

    $routeProvider.when("/equipos/:selectedTeamId", {
        controller: "teamsController",
        templateUrl: "/app/views/teamOptions.html"
    });

    $routeProvider.when("/equipos/:selectedTeamId/editar", {
        controller: "teamsController",
        templateUrl: "/app/views/editTeam.html"
    });

    $routeProvider.when("/equipos/:selectedTeamId/jugadores", {
        controller: "playersController",
        templateUrl: "/app/views/players.html"
    });

    $routeProvider.when("/equipos/:selectedTeamId/jugadores/crear", {
        controller: "playersController",
        templateUrl: "/app/views/createPlayer.html"
    });

    $routeProvider.when("/jugadores/:selectedPlayerId", {
        controller: "playersController",
        templateUrl: "/app/views/playerOptions.html"
    });

    $routeProvider.when("/jugadores/:selectedPlayerId/editar", {
        controller: "playersController",
        templateUrl: "/app/views/editPlayer.html"
    });

    $routeProvider.when("/torneos/:selectedTournamentId/calendario", {
        controller: "matchesController",
        templateUrl: "/app/views/matches.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

//Routing debugging code

//app.run(['$rootScope',  function($rootScope) {
//      // see what's going on when the route tries to change
//      $rootScope.$on('$routeChangeStart', function(event, next, current) {
//          // next is an object that is the route that we are starting to go to
//          // current is an object that is the route where we are currently
//          var currentPath = current.originalPath;
//          var nextPath = next.originalPath;

//          console.log('Starting to leave %s to go to %s', currentPath, nextPath);
//      });
//}]);

// Disable $http cache
app.config(['$httpProvider', function ($httpProvider) {
    //initialize get if not there
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }

    // Answer edited to include suggestions from comments
    // because previous version of code introduced browser-related errors

    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
}]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

var serviceBase = 'http://localhost:7247/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
});
