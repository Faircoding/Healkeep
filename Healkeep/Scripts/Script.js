/// <reference path="angular.min.js" />

var demoapp = angular.module("demoApp", [])
                    .controller("diseaseController", function ($scope, $http, $location, $anchorScroll) {
                        $http.get("HealkeepService.asmx/GetData")
                        .then(function (response) {
                            $scope.diseases = response.data;
                        });
                        $scope.scrollTo = function (diseaseName) {
                            $location.hash(diseaseName);
                            $anchorScroll();
                        }
                    });
                    

var app = angular
            .module("myModule2", [])
            .controller("myController", function ($scope) {
                var technologies = [
                    {name:"C#", likes:0, dislikes:0},
                ];

                $scope.technologies = technologies;

                $scope.incrementLikes = function (technology) {
                    technology.likes++;
                }

                $scope.incrementDislikes = function (technology) {
                    technology.dislikes++;
                }
            });


var app = angular
            .module("myModule",[])
            .controller("myController2", function ($scope, $http) {
                $http.get("HealkeepService.asmx/GetLikes")
                .then(function (response) {
                    $scope.comments = response.data;
                });
            });