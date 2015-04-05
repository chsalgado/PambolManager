'use strict';
app.factory('playersService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var playersServiceFactory = {};

    var _getPlayers = function (teamId) {

        return $http.get(serviceBase + 'api/players?teamId=' + teamId + '&page=1&take=30').then(function (results) {
            return results;
        });
    };

    var _getSelectedPlayer = function (playerId) {

        return $http.get(serviceBase + 'api/players?id=' + playerId).then(function (results) {
            return results;
        });
    };

    var _createPlayer = function (requestModel) {

        return $http.post(serviceBase + 'api/players', requestModel).then(function (response) {
            return response;
        });
    };

    var _editPlayer = function (playerId, requestModel) {

        return $http.put(serviceBase + 'api/players?id=' + playerId, requestModel).then(function (response) {
            return response;
        });
    };

    var _deletePlayer = function (playerId) {

        return $http.delete(serviceBase + 'api/players?id=' + playerId).then(function (response) {
            return response;
        });
    };

    playersServiceFactory.getPlayers = _getPlayers;
    playersServiceFactory.getSelectedPlayer = _getSelectedPlayer;
    playersServiceFactory.createPlayer = _createPlayer;
    playersServiceFactory.editPlayer = _editPlayer;
    playersServiceFactory.deletePlayer = _deletePlayer;

    return playersServiceFactory;
}]);

