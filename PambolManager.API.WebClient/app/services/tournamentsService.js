'use strict';
app.factory('tournamentsService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var tournamentsServiceFactory = {};

    var _getTournaments = function () {

        return $http.get(serviceBase + 'api/tournaments?page=1&take=20').then(function (results) {
            return results;
        });
    };

    var _getSelectedTournament = function (tournamentId) {

        return $http.get(serviceBase + 'api/tournaments?id=' +tournamentId).then(function (results) {
            return results;
        });
    };

    var _createTournament = function (requestModel) {

        return $http.post(serviceBase + 'api/tournaments', requestModel).then(function (response) {
            return response;
        });
    };

    var _editTournament = function (tournamentId, requestModel) {

        return $http.put(serviceBase + 'api/tournaments?id=' +tournamentId, requestModel).then(function (response) {
            return response;
        });
    };

    var _deleteTournament = function (tournamentId) {

        return $http.delete(serviceBase + 'api/tournaments?id=' + tournamentId).then(function (response) {
            return response;
        });
    };

    tournamentsServiceFactory.getTournaments = _getTournaments;
    tournamentsServiceFactory.getSelectedTournament = _getSelectedTournament;
    tournamentsServiceFactory.createTournament = _createTournament;
    tournamentsServiceFactory.editTournament = _editTournament;
    tournamentsServiceFactory.deleteTournament = _deleteTournament;

    return tournamentsServiceFactory;
}]);

