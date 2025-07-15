using System.Net;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using University.API.Filters;
using University.Core.DTOs;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers;

[ApiController]
[Route("api/v1/students")]
[TypeFilter(typeof(ApiExceptionFilter))]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ApiResponse GetAllStudents()
    {
        return new ApiResponse(_service.GetAll(),200);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ApiResponse GetStudentById(int id)
    {
        StudentDTO dto = _service.GetById(id);
        return new ApiResponse(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ApiResponse CreateStudent([FromBody] CreateStudentForm form)
    {
        _service.Create(form);
        return new ApiResponse(HttpStatusCode.Created);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ApiResponse UpdateStudent(int id,[FromBody] UpdateStudentForm form)
    {
        _service.Update(id,form);
        return new ApiResponse(HttpStatusCode.OK);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ApiResponse DeleteStudent(int id)
    {
        _service.Delete(id);
        return new ApiResponse(HttpStatusCode.OK);
    }
}