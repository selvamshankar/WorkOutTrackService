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
    [System.Web.Http.RoutePrefix("api/report")]
    public class ReportController : ApiController
    {
        WorkOutActiveDb dao;
        public ReportController()
        {
            dao = new WorkOutActiveDb();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("workoutsummary")]
        public IHttpActionResult Get()
        {
            try
            {

                return Ok(dao.GetWorkOutSummary());
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred" + ex.InnerException.ToString());
            }

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("weeklyChart")]
        public IHttpActionResult GetChart()
        {
            try
            {

                return Ok(dao.GetWeeklyChart());
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred" + ex.InnerException.ToString());
            }

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("monthlyChart")]
        public IHttpActionResult GetMonthChart()
        {
            try
            {

                return Ok(dao.GetMonthlyChart());
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred" + ex.InnerException.ToString());
            }

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("yearlyChart")]
        public IHttpActionResult GetYearlyChart()
        {
            try
            {

                return Ok(dao.GetYearlyChart());
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred" + ex.InnerException.ToString());
            }

        }

    }
}
