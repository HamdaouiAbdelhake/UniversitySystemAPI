using System.Net;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using University.Core.DTOs;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers;

[ApiController]
[Route("api/v1/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public ApiResponse GetAllStudents()
    {
        return new ApiResponse(_service.GetAll(),200);
    }
    
    [HttpGet("{id}")]
    public ApiResponse GetStudentById(int id)
    {
        StudentDTO dto;
        try
        {
            dto = _service.GetById(id);
        }
        catch (KeyNotFoundException e)
        {
            return new ApiResponse(e.Message, null, 404);

        }
        
        return new ApiResponse(dto,200);
    }

    [HttpPost]
    public ApiResponse CreateStudent([FromBody] CreateStudentForm form)
    {
        try
        {
            _service.Create(form);
        }
        catch (InvalidDataException e)
        {
            return new ApiResponse(e.Message, null,400);

        }
        catch (ArgumentException e)
        {
            return new ApiResponse(e.Message, null, 400);
        }
        return new ApiResponse(HttpStatusCode.Created);
    }
    
    [HttpPut("{id}")]
    public ApiResponse UpdateStudent(int id,[FromBody] UpdateStudentForm form)
    {
        try
        {
            _service.Update(id,form);
        }
        catch (KeyNotFoundException e)
        {
            return new ApiResponse(e.Message, null, 404);

        }
        return new ApiResponse(HttpStatusCode.OK);
    }
    
    [HttpDelete("{id}")]
    public ApiResponse DeleteStudent(int id)
    {
        try
        {
            _service.Delete(id);
        }
        catch (KeyNotFoundException e)
        {
            return new ApiResponse(e.Message, null, 404);

        }
        return new ApiResponse(HttpStatusCode.OK);
    }
}