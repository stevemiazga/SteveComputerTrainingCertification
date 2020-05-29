using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator) => _mediator = mediator;

        [HttpGet("students")]
        public async Task<IActionResult> GetStudentRecords()
        {
            var getStudentRecordQuery = new GetStudentRecordsQuery();

            var response = await _mediator.Send(getStudentRecordQuery);
            return Ok(response);
        }

        [HttpGet("notices")]
        public async Task<IActionResult> GetObtainCertificationNotices()
        {
            var getObtainCertificationNoticesQuery = new GetObtainCertificationNoticesQuery(System.DateTime.Now);

            var response = await _mediator.Send(getObtainCertificationNoticesQuery);
            return Ok(response);
        }

        [HttpPost("student")]
        public async Task<IActionResult> CreateStudentRecord([FromBody] StudentRecordViewModel studentRecordViewModel)
        {
            List<int> courseIds = new List<int>();
            foreach (var item in studentRecordViewModel.Courses)
            {
                courseIds.Add(item.CourseId);
            }
            List<int> examIds = new List<int>();
            foreach (var item in studentRecordViewModel.Exams)
            {
                examIds.Add(item.ExamId);
            }
            var createStudentRecordCommand = new CreateStudentRecordCommand(studentRecordViewModel.FirstName, studentRecordViewModel.LastName, studentRecordViewModel.Email, courseIds, examIds);
            var response = await _mediator.Send(createStudentRecordCommand);
            return Ok(response);
        }


        [HttpPut("student")]
        public async Task<IActionResult> EditStudentRecord([FromBody] StudentRecordViewModel studentRecordViewModel)
        {
            List<int> courseIds = new List<int>();
            foreach (var item in studentRecordViewModel.Courses)
            {
                courseIds.Add(item.CourseId);
            }
            List<int> examIds = new List<int>();
            foreach (var item in studentRecordViewModel.Exams)
            {
                examIds.Add(item.ExamId);
            }

            var editStudentRecordCommand = new EditStudentRecordCommand(studentRecordViewModel.StudentId, studentRecordViewModel.FirstName, studentRecordViewModel.LastName, studentRecordViewModel.Email, courseIds, examIds);
            var response = await _mediator.Send(editStudentRecordCommand);
            return Ok(response);
        }

        [HttpDelete("student/{studentId:int}")]
        public async Task<IActionResult> DeleteStudentRecord(int studentId)
        {
            var deleteStudentRecordCommand = new DeleteStudentRecordCommand(studentId);
            var response = await _mediator.Send(deleteStudentRecordCommand);

            return Ok(response);
        }

        [HttpGet("student/{studentId:int}")]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            var getStudentById = new GetStudentByIdQuery(studentId);

            var response = await _mediator.Send(getStudentById);
            return Ok(response);
        }
    }
}
