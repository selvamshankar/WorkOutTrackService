using System;
using System.Web.Http;
using System.Web.Http.Cors;
using WorkOutDBLayer;
using WorkOutDBModel.Model;

namespace WorkOutTrackService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        CateogryDb dao;
        public CategoryController()
        {
            dao = new CateogryDb();
        }
        // GET: api/Category

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
        [System.Web.Http.Route("search/{categoryName}")]
        public IHttpActionResult Get(string categoryName)
        {
            try
            {
                return Ok(dao.GetByName(categoryName));
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred");
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
                return BadRequest("Error Occurred");
            }
        }

        // POST: api/Category
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Add/{categoryName}/{categoryId}")]
        public IHttpActionResult Post(string categoryName, int categoryId)
        {
            try
            {
                Category model = new Category() { CategoryId = categoryId, CategoryName = categoryName };
                if (dao.Add(model))
                    return Ok("Record Created Successfully");
                else
                    return BadRequest("Record Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred");
            }

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Update/{categoryId}/{categoryName}")]
        public IHttpActionResult Post(int Id, string categoryName)
        {
            try
            {
                Category model = new Category()
                {
                    CategoryName = categoryName,
                    Created = DateTime.Now
                };
                if (dao.Add(model))
                    return Ok("Record Created Successfully");
                else
                    return BadRequest("Record Created Successfully");
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
