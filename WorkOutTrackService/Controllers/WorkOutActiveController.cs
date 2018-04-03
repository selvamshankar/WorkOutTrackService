using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WorkOutDBLayer;

namespace WorkOutTrackService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("api/workoutactive")]
    public class WorkoutActiveController : ApiController
    {
        WorkOutActiveDb dao;
        public WorkoutActiveController()
        {
            dao = new WorkOutActiveDb();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(dao.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred" + ex.InnerException.ToString());
            }

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("start/{Id}")]
        public IHttpActionResult Get(int Id)
        {
            try
            {
                return Ok(dao.StartWorkOut(Id));
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred" + ex.InnerException.ToString());
            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("End/{Id}/{WorkId}")]
        public IHttpActionResult Get(int Id, int WorkId)
        {
            try
            {
                return Ok(dao.EndWorkOut(Id, WorkId));
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred" + ex.InnerException.ToString());
            }

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Add/{Id}/{comments}/{startdate}/{starttime}")]
        public IHttpActionResult Post(int Id, string comments, string startdate, string starttime)
        {
            try
            {
                startdate = startdate.Replace("_", "/");
                starttime = starttime.Replace("_", ":");
                WorkOutDBModel.Model.Workout_Active model = new WorkOutDBModel.Model.Workout_Active()
                {

                    Comment = comments,
                    Start_Date = Convert.ToDateTime(startdate),
                    Start_Time = starttime,
                    Status = true,
                    WorkOutId = Id
                };
                if (dao.AddStartWorkOut(model))
                    return Ok("Record Created Successfully");
                else
                    return BadRequest("Error Occurred");
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred");
            }

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Update/{Id}/{workoutid}/{comments}/{enddate}/{endtime}")]
        public IHttpActionResult Post(int Id, int workoutid, string comments, string enddate, string endtime)
        {
            try
            {
                enddate = enddate.Replace("_", "/");
                endtime = endtime.Replace("_", ":");
                WorkOutDBModel.Model.Workout_Active model = new WorkOutDBModel.Model.Workout_Active()
                {

                    Comment = comments,
                    End_Date = Convert.ToDateTime(enddate),
                    End_time = endtime,
                    Status = true,
                    Id = Id,
                    WorkOutId = workoutid
                };
                if (dao.AddStartWorkOut(model))
                    return Ok("Record Saved Successfully");
                else
                    return BadRequest("Error Occurred");
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred");
            }

        }

    }
}
