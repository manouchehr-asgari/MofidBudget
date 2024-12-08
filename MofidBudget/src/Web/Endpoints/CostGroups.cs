using MofidBudget.Application.CostCategories.Queries.GetCostCategories;
using MofidBudget.Application.CostGroups.Commands.CreateCostGroup;
using MofidBudget.Application.CostGroups.Commands.DeleteCostGroup;
using MofidBudget.Application.CostGroups.Commands.UpdateCostGroup;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;

namespace MofidBudget.Web.Endpoints;

public class CostGroups : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetCostGroups)
            .MapPost(CreateCostGroup)
            .MapPut(UpdateCostGroup, "{id}")
            .MapDelete(DeleteCostGroup, "{id}");
    }

    public Task<IReadOnlyCollection<CostGroupDto>> GetCostGroups(ISender sender)
    {
        return sender.Send(new GetCostGroupsQuery());
    }

    public Task<int> CreateCostGroup(ISender sender, CreateCostGroupCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateCostGroup(ISender sender, int id, UpdateCostGroupCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteCostGroup(ISender sender, int id)
    {
        await sender.Send(new DeleteCostGroupCommand(id));
        return Results.NoContent();
    }
}

