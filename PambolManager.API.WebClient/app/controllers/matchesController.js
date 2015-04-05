'use strict';
app.controller('matchesController', ['$scope', '$location', '$routeParams', 'matchesService',
    function ($scope, $location, $routeParams, matchesService) {

        //Matches list
        $scope.matches = [];

        //Add-update tournament

        $scope.selectedTournamentId = $routeParams.selectedTournamentId;
        $scope.totalRounds = undefined;

        $scope.getMatches = function (roundNumber) {

            matchesService.getMatches($scope.selectedTournamentId, roundNumber).then(function (results) {

                $scope.matches = results.data;

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.editScore = function (index) {
            $scope.matches[index].IsScoreSet = true;
            matchesService.editScore($scope.matches[index].Id, $scope.matches[index]).then(function (response) {

                
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

        $scope.deleteScore = function (index) {
            matchesService.deleteScore($scope.matches[index].Id).then(function (response) {

                $scope.matches[index].HomeGoals = 0;
                $scope.matches[index].AwayGoals = 0;
                $scope.matches[index].IsScoreSet = false;
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

        $scope.setTotalRounds = function () {

            matchesService.getSelectedTournament($scope.selectedTournamentId).then(function (results) {

                $scope.totalRounds = results.data.TotalRounds;

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.getTotalRounds = function (num) {
            return new Array(num);
        }
    }]);
