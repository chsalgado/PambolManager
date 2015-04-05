'use strict';
app.factory('matchesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var matchesServiceFactory = {};

    var _getMatches = function (tournamentId, roundNumber) {

        return $http.get(serviceBase + 'api/matches?tournamentId=' + tournamentId + '&roundNumber=' +roundNumber).then(function (results) {
            return results;
        });
    };

    var _editScore = function (matchId, requestModel) {

        return $http.put(serviceBase + 'api/matches?id=' + matchId, requestModel).then(function (response) {
            return response;
        });
    };

    var _deleteScore = function (matchId) {

        return $http.delete(serviceBase + 'api/matches?id=' + matchId).then(function (response) {
            return response;
        });
    };

    var _getSelectedTournament = function (tournamentId) {

        return $http.get(serviceBase + 'api/tournaments?id=' + tournamentId).then(function (results) {
            return results;
        });
    };

    matchesServiceFactory.getMatches = _getMatches;
    matchesServiceFactory.editScore = _editScore;
    matchesServiceFactory.deleteScore = _deleteScore;
    matchesServiceFactory.getSelectedTournament = _getSelectedTournament;

    return matchesServiceFactory;
}]);

