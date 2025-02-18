var app = angular.module('CurrencyConverterApp', []);

app.controller('CurrencyController', function ($scope, $http) {
    // List of common currencies
    $scope.currencies = ["USD", "EUR", "GBP", "INR", "JPY", "AUD", "CAD"];
    $scope.fromCurrency = "USD";
    $scope.toCurrency = "EUR";
    $scope.amount = 1;
    $scope.result = null;

    $scope.convertCurrency = function () {
        // API call to ASP.NET Web API backend
        var apiUrl = `http://localhost:44395/api/currency/convert?from=${$scope.fromCurrency}&to=${$scope.toCurrency}&amount=${$scope.amount}`;

        
        $http.get(apiUrl)
            .then(function (response) {
                $scope.result = response.data.convertedAmount;
            }, function (error) {
                console.error("Error fetching the currency data:", error);
                $scope.result = "Error fetching data";
            });
    };
});
