'use strict';
app.controller('teamsController', ['$scope', '$location', '$timeout', '$routeParams', 'teamsService',
    function ($scope, $location, $timeout, $routeParams, teamsService) {

        //Teams list
        $scope.teams = [];
        $scope.selectedTournamentId = $routeParams.selectedTournamentId;

        //Add-update team
        $scope.savedSuccessfully = false;
        $scope.message = "";

        $scope.requestModel = {
            TeamName: "",
            TournamentId: ""
        };
        //Add-update team

        //Team options
        $scope.selectedTeamId = $routeParams.selectedTeamId;

        $scope.getTeams = function () {

            teamsService.getTeams($scope.selectedTournamentId).then(function (results) {

                $scope.teams = results.data.Items;

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.getSelectedTeam = function () {

            teamsService.getSelectedTeam($scope.selectedTeamId).then(function (results) {

                $scope.requestModel.TeamName = results.data.TeamName;
                $scope.requestModel.TournamentId = results.data.TournamentId;

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.createTeam = function () {
            $scope.requestModel.TournamentId = $scope.selectedTournamentId;
            teamsService.createTeam($scope.requestModel).then(function (response) {

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

        $scope.editTeam = function () {
            teamsService.editTeam($scope.selectedTeamId, $scope.requestModel).then(function (response) {

                $scope.savedSuccessfully = true;
                $scope.selectedTournamentId = $scope.requestModel.TournamentId;
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

        $scope.deleteTeam = function () {
            $scope.getSelectedTeam($scope.selectedTeamId);
            teamsService.deleteTeam($scope.selectedTeamId).then(function (response) {
                $scope.selectedTournamentId = $scope.requestModel.TournamentId;
                $location.path('/torneos/' +$scope.selectedTournamentId +'/equipos');
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
                $location.path('/torneos/' + $scope.selectedTournamentId + '/equipos');
            }, 2000);
        }

    }]);
