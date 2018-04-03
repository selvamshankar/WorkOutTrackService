using System;
using System.Web.Http;
using System.Web.Http.Cors;
using WorkOutDBLayer;
using WorkOutDBModel.Model;

namespace WorkOutTrackService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("api/workout")]
    public class WorkOutTrackerController : ApiController
    {
        // GET: WorkOut
        WorkOutDb dao;
        public WorkOutTrackerController()
        {
            dao = new WorkOutDb();
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
        [System.Web.Http.Route("search/{title}")]
        public IHttpActionResult Get(string title)
        {
            try
            {
                return Ok(dao.GetAllByTitle(title));
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred" + ex.InnerException.ToString());
            }

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(dao.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred" + ex.InnerException.ToString());
            }

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Add/{WorkOutTitle}/{WorkOutNote}/{CaloriesBurnPerMin}/{CategoryId}")]
        public IHttpActionResult Post(string WorkOutTitle, string WorkOutNote, int CaloriesBurnPerMin, int CategoryId)
        {
            try
            {
                WorkOut model = new WorkOut()
                {
                    CaloriesBurnPerMin = CaloriesBurnPerMin,
                    CategoryId = CategoryId,
                    Created = DateTime.Now,
                    WorkOutNote = WorkOutNote,
                    WorkOutTitle = WorkOutTitle
                };
                if (dao.Add(model))
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
        [System.Web.Http.Route("Update/{Id}/{WorkOutTitle}/{WorkOutNote}/{CaloriesBurnPerMin}/{CategoryId}")]
        public IHttpActionResult Post(int Id, string WorkOutTitle, string WorkOutNote, int CaloriesBurnPerMin, int CategoryId)
        {
            try
            {


                WorkOut model = new WorkOut()
                {
                    CaloriesBurnPerMin = CaloriesBurnPerMin,
                    CategoryId = CategoryId,
                    Created = DateTime.Now,
                    WorkOutNote = WorkOutNote,
                    WorkOutTitle = WorkOutTitle,
                    WorkOutId = Id
                };
                if (dao.Update(model))
                    return Ok("Record Updated Successfully");
                else
                    return BadRequest("Error Occurred");
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred");
            }

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (dao.Delete(id))
                    return Ok("Record Deleted Successfully");
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
