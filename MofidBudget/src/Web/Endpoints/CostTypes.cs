using Microsoft.AspNetCore.Mvc;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;
using MofidBudget.Application.CostTypes.Commands.CreateCostType;
using MofidBudget.Application.CostTypes.Commands.DeleteCostType;
using MofidBudget.Application.CostTypes.Commands.UpdateCostType;
using MofidBudget.Application.CostTypes.Queries.GetCostTypes;

namespace MofidBudget.Web.Endpoints;

public class CostTypes : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetCostTypes)
            .MapPost(CreateCostType)
            .MapPut(UpdateCostType, "{id}")
            .MapDelete(DeleteCostType, "{id}");
    }

    public Task<PaginatedList<CostTypeDto>> GetCostTypes(ISender sender, [FromBody]GetCostTypesWithPaginationQuery command)
    {
        return sender.Send(command);
    }

    public Task<int> CreateCostType(ISender sender, CreateCostTypeCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateCostType(ISender sender, int id, UpdateCostTypeCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteCostType(ISender sender, int id)
    {
        await sender.Send(new DeleteCostTypeCommand(id));
        return Results.NoContent();
    }
}

