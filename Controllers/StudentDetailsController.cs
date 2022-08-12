using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentDetailsController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentDetailsController(StudentContext context)
        {
            _context = context;
        }
        //public class HtmlOutputFormatter : StringOutputFormatter
        //{
        //    public HtmlOutputFormatter()
        //    {
        //        SupportedMediaTypes.Add("text/html");
        //    }
        //}

        // GET: api/StudentDetails
        [HttpGet]
        //[Produces("text/html")]
        //[FormatFilter]
        public async Task<ActionResult<IEnumerable<StudentDetail>>> GetStudentDetails()
        {
            //return await _context.StudentDetails.ToListAsync();
            if (_context.StudentDetails == null)
            {
              return NotFound();
            }
            
            List<StudentDetail> Detail = await _context.StudentDetails.ToListAsync();
            {

                string html = String.Empty;
                string html1 = String.Empty;
                string html2 = String.Empty;
                {
                    
                    foreach (var studentDetail in Detail)
                    {
                        
                        int Id = studentDetail.Id;
                        string Name = studentDetail.Name;
                        string Address = studentDetail.Address;
                        
                        html = supportingHTML(Id, Name, Address);
                        html1 = html1 + html;

                        html2 = WelcomeHTML(html1); // No use of supportingHTML is happening here
                    }
                }

                return new ContentResult
                {
                    //Content = htmltitle,
                    Content = html2 ,
                    ContentType = MediaTypeNames.Text.Html,
                    StatusCode = 200                  
                };
                //return base.Content(html, "text/html");
            }
        }

        private string supportingHTML(int Id, string Name, string Address)
        {
            var html = System.IO.File.ReadAllText(@"./HtmlRender/htmlpage.html");
            html = html.Replace("{{Id}}", Id.ToString());
            html = html.Replace("{{Name}}", Name);
            html = html.Replace("{{Address}}", Address);
            // html = html.app
            return html;
        }
        private string WelcomeHTML(string html1)
        {
            var html = System.IO.File.ReadAllText(@"./HtmlRender/index.html");
            html = html.Replace("{{htmlData}}", html1);
            
           // html = html.app
            return html;
        }
        
        [HttpGet("fffff")]
        //public ActionResult DisplayWebPage()
        //{
        //    return Content("<html><p><i>Hello! You are trying to view <u>something!</u></i></p></html>", "text/html");
        //}
        public ContentResult Index()
        {
            return Content("<h3>Here's a custom content header</h3>", "text/html", System.Text.Encoding.UTF8);
        }

        // GET: api/StudentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDetail>> GetStudentDetail(int id)
        {
          if (_context.StudentDetails == null)
          {
              return NotFound();
          }
            var studentDetail = await _context.StudentDetails.FindAsync(id);

            if (studentDetail == null)
            {
                return NotFound();
            }

            return studentDetail;
        }

        // PUT: api/StudentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentDetail(int id, StudentDetail studentDetail)
        {
            if (id != studentDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentDetail>> PostStudentDetail(StudentDetail studentDetail)
        {
            if (_context.StudentDetails == null)
            {
                return Problem("Entity set 'StudentContext.StudentDetails'  is null.");
            }
            _context.StudentDetails.Add(studentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentDetail", new { id = studentDetail.Id }, studentDetail);
        }

        // DELETE: api/StudentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentDetail(int id)
        {
            if (_context.StudentDetails == null)
            {
                return NotFound();
            }
            var studentDetail345 = await _context.StudentDetails.FindAsync(id);
            if (studentDetail345 == null)
            {
                return NotFound();
            }

            _context.StudentDetails.Remove(studentDetail345);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentDetailExists(int id)
        {
            return (_context.StudentDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
