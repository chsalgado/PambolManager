'use strict';
app.controller('playersController', ['$scope', '$location', '$timeout', '$routeParams', 'playersService',
    function ($scope, $location, $timeout, $routeParams, playersService) {

        //Players list
        $scope.players = [];
        $scope.selectedTeamId = $routeParams.selectedTeamId;

        //Add-update player
        $scope.savedSuccessfully = false;
        $scope.message = "";

        $scope.requestModel = {
            Name: "",
            LastName: "",
            Age: "",
            PhoneNumber: "",
            Email: "",
            TeamId: ""
        };
        //Add-update player

        //Player options
        $scope.selectedPlayerId = $routeParams.selectedPlayerId;

        $scope.getPlayers = function () {

            playersService.getPlayers($scope.selectedTeamId).then(function (results) {

                $scope.players = results.data.Items;

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.getSelectedPlayer = function () {

            playersService.getSelectedPlayer($scope.selectedPlayerId).then(function (results) {

                $scope.requestModel.Name = results.data.Name;
                $scope.requestModel.LastName = results.data.LastName;
                $scope.requestModel.Age = results.data.Age;
                $scope.requestModel.PhoneNumber = results.data.PhoneNumber;
                $scope.requestModel.Email = results.data.Email;
                $scope.requestModel.TeamId = results.data.TeamId;

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.createPlayer = function () {
            $scope.requestModel.TeamId = $scope.selectedTeamId;
            playersService.createPlayer($scope.requestModel).then(function (response) {

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

        $scope.editPlayer = function () {
            playersService.editPlayer($scope.selectedPlayerId, $scope.requestModel).then(function (response) {

                $scope.savedSuccessfully = true;
                $scope.selectedTeamId = $scope.requestModel.TeamId;
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

        $scope.deletePlayer = function () {
            $scope.getSelectedPlayer($scope.selectedPlayerId);
            playersService.deletePlayer($scope.selectedPlayerId).then(function (response) {
                $scope.selectedTeamId = $scope.requestModel.TeamId;
                $location.path('/equipos/' + $scope.selectedTeamId + '/jugadores');
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
                $location.path('/equipos/' + $scope.selectedTeamId + '/jugadores');
            }, 2000);
        }

    }]);
