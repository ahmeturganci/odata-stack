using odata_stack.BL;
using odata_stack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;
using System.Web.Http.Results;
using System.Diagnostics;

namespace odata_stack.Controllers
{
    public class ProjectController : ODataController
    {
        public IList<Project> Get()
        {
            return m_service.Projects;
        }
        public Project Get([FromODataUri] int key)
        {
            IEnumerable<Project> project = m_service.Projects.Where(m => m.Id == key);
            if (project.Count() == 0)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                return project.FirstOrDefault();
        }
        public IHttpActionResult Post([FromBody] Project project)
        {
            try
            {
                return Ok<Project>(m_service.Add(project));
            }
            catch (ArgumentNullException e)
            {
                Debugger.Log(1, "Error", e.Message);
                return BadRequest();
            }
            catch (ArgumentException e)
            {
                Debugger.Log(1, "Error", e.Message);
                return BadRequest();
            }
            catch (InvalidOperationException e)
            {
                Debugger.Log(1, "Error", e.Message);
                return Conflict();
            }
        }
        public IHttpActionResult Delete(int key)
        {
            if (m_service.Remove(key))
                return Ok();
            else
                return NotFound();
        }
        private DataService m_service = new DataService();
    }
}
