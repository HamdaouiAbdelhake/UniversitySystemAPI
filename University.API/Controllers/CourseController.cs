using System.Net;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.API.Filters;
using University.Core.DTOs;
using University.Core.Forms;
using University.Core.Services;
using University.Data.Repositories;

namespace University.API.Controllers;

[ApiController]
[Route("api/v1/courses")]
[TypeFilter(typeof(ApiExceptionFilter))]
[Authorize(Roles = "Teacher")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;

    public CoursesController(ICourseService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ApiResponse GetAllCourses()
    {
        return new ApiResponse(_service.GetAll());
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ApiResponse GetCourseById(int id)
    {
        CourseDTO dto = _service.GetById(id);
        return new ApiResponse(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ApiResponse CreateCourse([FromBody] CreateCourseForm form)
    {
        _service.Create(form);
        return new ApiResponse(HttpStatusCode.Created,201);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ApiResponse UpdateCourse(int id,[FromBody] UpdateCourseForm form)
    {
        _service.Update(id,form);
        return new ApiResponse(HttpStatusCode.OK);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public ApiResponse DeleteCourse(int id)
    {
        _service.Delete(id);
        return new ApiResponse(HttpStatusCode.OK);
    }
}