!function(){"use strict";function e(e,o,n){var s=this;s.planName=e.planName,s.duration,s.courseName=e.name,s.courses=[],s.errorMessage="",s.isBusy=!0,s.newCourse={},s.showInfoText=!1;var a="/api/plans/"+s.planName+"/courses";o.get(a).then(function(e){angular.copy(e.data,s.courses,s.isActive);var o=new Array,n=0;for(var a in e.data)o.push(e.data[a]);for(var r in o)n+=o[r].duration;s.infoTextMessage="Total duration of plan:  "+n+" days",s.showInfoText=!0},function(e){s.errorMessage="Failed to load courses: "+e})["finally"](function(){s.isBusy=!1}),s.addCourse=function(){s.isBusy=!0,o.post(a,s.newCourse,s.planName).then(function(e){s.courses.push(e.data),s.newCourse={},n.reload()},function(e){s.errorMessage="Failed to add new course"})["finally"](function(){s.isBusy=!1})},s.removeCourse=function(e){s.errorMessage="",s.isBusy=!0,o.put(a+"/"+e).then(function(o){s.courses.splice(e,1),s.isBusy=!1,n.reload()},function(e){s.errorMessage="Failed to remove course from plan"})["finally"](function(){s.isBusy=!1})},s.startCourse=function(e){o.put(a+"/"+e).then(function(e){n.reload()},function(e){s.errorMessage="Failed to start course"})["finally"](function(){s.isBusy=!1})}}e.$inject=["$routeParams","$http","$route"],angular.module("app-plans").controller("planEditorController",e)}();