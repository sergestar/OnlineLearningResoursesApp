!function(){"use strict";angular.module("app-courses",["simpleControls","ngRoute"]).config(["$routeProvider",function(e){e.when("/",{controller:"coursesController",controllerAs:"vm",templateUrl:"/views/coursesView.html"}),e.when("/addNewCourse",{controller:"coursesController",controllerAs:"vm",templateUrl:"/views/addNewCourseView.html"}),e.otherwise({redirectTo:"/"})}])}();