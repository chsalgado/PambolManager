'use strict';
app.controller('tournamentsController', ['$scope', '$location', '$timeout', '$routeParams', 'tournamentsService',
    function ($scope, $location, $timeout, $routeParams, tournamentsService) {

    //Tournaments list
    $scope.tournaments = [];

    //Add-update tournament
    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.requestModel = {
        TournamentName: "",
        MaxTeams: "",
        Description: "",
        FieldManagerId: ""
    };
    //Add-update tournament

    //Tournament options
    $scope.selectedTournamentId = $routeParams.selectedTournamentId;

    $scope.getTournaments = function () {

        tournamentsService.getTournaments().then(function (results) {

            $scope.tournaments = results.data.Items;

        }, function (error) {
            alert(error.data.message);
        });
    }

    $scope.getSelectedTournament = function () {

        tournamentsService.getSelectedTournament($scope.selectedTournamentId).then(function (results) {

            $scope.requestModel.TournamentName = results.data.TournamentName;
            $scope.requestModel.MaxTeams = results.data.MaxTeams;
            $scope.requestModel.Description = results.data.Description;

        }, function (error) {
            alert(error.data.message);
        });
    }

    $scope.createTournament = function () {
        tournamentsService.createTournament($scope.requestModel).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "Registro exitoso, redirigiendo...";
            startTimer();
        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Error!" + errors.join(' ');
         });

    };

    $scope.editTournament = function () {
        tournamentsService.editTournament($scope.selectedTournamentId, $scope.requestModel).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "Edición exitosa, redirigiendo...";
            startTimer();
        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Error!" + errors.join(' ');
         });

    };

    $scope.deleteTournament = function () {
        tournamentsService.deleteTournament($scope.selectedTournamentId).then(function (response) {

            $location.path('/torneos');
        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Error!" + errors.join(' ');
         });

    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/torneos');
        }, 2000);
    }

}]);
