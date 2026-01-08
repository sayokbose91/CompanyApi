using CompanyApi.Application.Companies.Commands.CreateCompany;
using CompanyApi.Application.Companies.Commands.DeleteCompany;
using CompanyApi.Application.Companies.Commands.UpdateCompany;
using CompanyApi.Application.Companies.Queries.GetAllCompanies;
using CompanyApi.Application.Companies.Queries.GetCompanyById;
using CompanyApi.Application.Companies.Queries.SearchCompanies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new company
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetCompanyById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Gets a company by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompanyById(string id, CancellationToken cancellationToken)
    {
        var query = new GetCompanyByIdQuery { Id = id };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound(new { Message = $"Company with ID {id} not found" });
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all companies with pagination and filtering
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCompanies(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool sortAscending = true,
        [FromQuery] bool? isActive = null,
        [FromQuery] string? industry = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllCompaniesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SortBy = sortBy,
            SortAscending = sortAscending,
            IsActive = isActive,
            Industry = industry
        };

        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Searches companies by text
    /// </summary>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchCompanies([FromQuery] string query, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest(new { Message = "Search query is required" });
        }

        var searchQuery = new SearchCompaniesQuery { SearchTerm = query };
        var result = await _mediator.Send(searchQuery, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Updates a company (full update)
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCompany(string id, [FromBody] UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest(new { Message = "ID in URL does not match ID in request body" });
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Partially updates a company
    /// </summary>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchCompany(string id, [FromBody] UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest(new { Message = "ID in URL does not match ID in request body" });
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes a company
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCompany(string id, CancellationToken cancellationToken)
    {
        var command = new DeleteCompanyCommand { Id = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound(new { Message = $"Company with ID {id} not found" });
        }

        return NoContent();
    }
}
