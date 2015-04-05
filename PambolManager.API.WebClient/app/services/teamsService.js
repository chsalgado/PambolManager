'use strict';
app.factory('teamsService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var teamsServiceFactory = {};

    var _getTeams = function (tournamentId) {

        return $http.get(serviceBase + 'api/teams?tournamentId=' +tournamentId +'&page=1&take=30').then(function (results) {
            return results;
        });
    };

    var _getSelectedTeam = function (teamId) {

        return $http.get(serviceBase + 'api/teams?id=' + teamId).then(function (results) {
            return results;
        });
    };

    var _createTeam = function (requestModel) {

        return $http.post(serviceBase + 'api/teams', requestModel).then(function (response) {
            return response;
        });
    };

    var _editTeam = function (teamId, requestModel) {

        return $http.put(serviceBase + 'api/teams?id=' + teamId, requestModel).then(function (response) {
            return response;
        });
    };

    var _deleteTeam = function (teamId) {

        return $http.delete(serviceBase + 'api/teams?id=' + teamId).then(function (response) {
            return response;
        });
    };

    teamsServiceFactory.getTeams = _getTeams;
    teamsServiceFactory.getSelectedTeam = _getSelectedTeam;
    teamsServiceFactory.createTeam = _createTeam;
    teamsServiceFactory.editTeam = _editTeam;
    teamsServiceFactory.deleteTeam = _deleteTeam;

    return teamsServiceFactory;
}]);

