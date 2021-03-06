﻿using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using OnlineLearningResourcesApp.Models;
using OnlineLearningResourcesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineLearningResourcesApp.Controllers.Api
{
    [Authorize]
    public class LearningPlanController : Controller
    {
        private ILogger _logger;
        private ILearningRepository _repository;

        public LearningPlanController(ILearningRepository repository, ILogger<LearningPlanController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Gets plans of user
        [HttpGet("api/plans")]
        public JsonResult Get()
        {
            var plans = _repository.GetUserPlansWithCourses(User.Identity.Name);
            var results = Mapper.Map<IEnumerable<LearningPlanViewModel>>(plans);
            return Json(results);
        }

        // Posts new plan for user
        [HttpPost("api/plans")]
        public JsonResult Post([FromBody]LearningPlanViewModel vm)
        {
            try {
                if (ModelState.IsValid)
                {
                    var newPlan = Mapper.Map<LearningPlan>(vm);
                    newPlan.UserName = User.Identity.Name;

                    // Save to the db
                    _logger.LogInformation("Attempting to save a new plan");
                    _repository.AddLearningPlan(newPlan);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<LearningPlanViewModel>(newPlan));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new plan", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message});
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }

       // Remove course from plan
        [HttpPut]
        [Route("api/plans/{planName}/courses/{id}")]
        public JsonResult RemoveCourseFromPlan(string planName, int id)
        {
            try
            {
                _repository.RemoveCourse(id, planName, User.Identity.Name);
                if (_repository.SaveAll())
                {
                    Response.StatusCode = (int)HttpStatusCode.Accepted;
                    return Json("course removed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to remove course", ex);
                return Json("Failed to remove course");
            }
            return Json("Failed to remove course. Cannot find course");
        }

        // Delete plan
        [HttpDelete]
        [Route("api/plans/{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                _repository.DeletePlan(id);
                if (_repository.SaveAll())
                {
                    Response.StatusCode = (int)HttpStatusCode.NoContent;
                    return Json("course deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete plan", ex);
                return Json("Failed to delete plan");
            }
            return Json("Failed to delete plan. Cannot find plan");
        }
    }
}

