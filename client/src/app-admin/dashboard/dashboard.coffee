﻿angular.module('admin-dashboard',[])

.config(["$routeProvider", ($routeProvider) ->
  $routeProvider
    .when "/admin",
      templateUrl: "/app-admin/dashboard/dashboard.tpl.html"
      controller: 'DashboardCtrl'
])

.controller('DashboardCtrl',
["$scope","$http", 
($scope,$http) ->

])