using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;
using StudentAPI.Extensions;
using StudentAPI.DataAccessLayer.Repository;
using StudentAPI.DataAccessLayer.Context;
using StudentAPI.Service;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentDetailsController : ControllerBase
    {
        private readonly ILogger<StudentDetailsController> _logger;
        //private readonly StudentContext _context;
        private IUserService _UserService;

        public StudentDetailsController(
            StudentContext context,
            ILogger<StudentDetailsController> logger,
            IUserService UserService
            )
        {
            //_context = context;
            _logger = logger;
            _UserService = UserService;
        }    
        // GET: api/StudentDetails
        [HttpGet]
        //[Produces("text/html")]
        //[FormatFilter]
        public async Task<ActionResult<IEnumerable<StudentDetail>>> GetStudentDetails()
        { 
            //return await _context.StudentDetails.ToListAsync();
            //if (_UserService.All() == null)
            //{
            //  return NotFound();
            //}

            var Detail = await _UserService.All();
            {
                var html = System.IO.File.ReadAllText(@"./HtmlRender/htmlpage.html");
                string html1 = String.Empty;
                string html2 = String.Empty;
                string html3 = String.Empty;
                {
                    //foreach (var studentDetail in Detail)
                    //{
                    //    int Id = studentDetail.Id;
                    //    string Name = studentDetail.Name;
                    //    string Address = studentDetail.Address;

                    //    string html3 = extendedSupportingHtml(Id, Name, Address);
                    //    html1 = html1 + html3;

                    //    html2 = WelcomeHTML(html1);
                    //}

                    foreach (var studentDetail in Detail)
                    {
                        html1 = html.extendedSupportingHtmlForString(studentDetail);
                        //string html = studentDetail.extendedSupportingHtml();

                        html3 += html1;
                        html2 = WelcomeHTML(html3);
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

        //private string supportingHTML(int Id, string Name, string Address)
        //{
        //    var html = System.IO.File.ReadAllText(@"./HtmlRender/htmlpage.html");
        //    html = html.Replace("{{Id}}", Id.ToString());
        //    html = html.Replace("{{Name}}", Name);
        //    html = html.Replace("{{Address}}", Address);
        //    // html = html.app
        //    return html;
        //}
        private string WelcomeHTML(string html1)
        {
            var html = System.IO.File.ReadAllText(@"./HtmlRender/index.html");
            html = html.Replace("{{htmlData}}", html1);
            return html;
        }

        //[HttpGet("GetClass")]
        //public async Task<ActionResult<IEnumerable<StudentDetail>>> GetDetails()
        //{
        //    Console.WriteLine("We are getting class , fields and its values");

        //    StudentDetail detailModel = new StudentDetail()
            
        //    { Name = "QWERTY", Address = "RemoteState" };
            
            
        //    detailModel.getClassFieldsValues();

        //    return Ok();
        //}

        // GET: api/StudentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDetail>> GetStudentDetail(int id)
        {
            if (_UserService.getStudentById == null)
            {
                return NotFound();
            }
            var studentDetail = await _UserService.getStudentById(id);
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
            
            await _UserService.update(id, studentDetail);
            await _UserService.CompleteAsync();
            return CreatedAtAction("GetStudentDetail", new { id = studentDetail.Id }, studentDetail);
        }

        // POST: api/StudentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentDetail>> PostStudentDetail(StudentDetail studentDetail)
        {
            if (_UserService.addStudent == null)
            {
                return Problem("Entity set 'StudentContext.StudentDetails'  is null.");
            }
            _UserService.addStudent(studentDetail);
            //await _context.SaveChangesAsync();
            _UserService.CompleteAsync();

            return CreatedAtAction("GetStudentDetail", new { id = studentDetail.Id }, studentDetail);
        }


        // DELETE: api/StudentDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentDetail>> DeleteStudentDetail(int id)
        {
            if (_UserService.deleteById(id) == null)
            {
                return NotFound();
            }
            var studentDetail = await _UserService.deleteById(id);
            await _UserService.CompleteAsync();
            return studentDetail;
        }
        //private bool StudentDetailExists(int id)
        //{
        //    return (_context.StudentDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
