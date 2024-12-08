using MofidBudget.Application.CostCategories.Commands.CreateCostCategory;
using MofidBudget.Application.CostCategories.Commands.DeleteCostCategory;
using MofidBudget.Application.CostCategories.Commands.UpdateCostCategory;
using MofidBudget.Application.CostCategories.Queries.GetCostCategories;

namespace MofidBudget.Web.Endpoints;

public class CostCategories : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetCostCategories)
            .MapPost(CreateCostCategory)
            .MapPut(UpdateCostCategory, "{id}")
            .MapDelete(DeleteCostCategory, "{id}");
    }

    public Task<IReadOnlyCollection<CostCategoryDto>> GetCostCategories(ISender sender)
    {
        return sender.Send(new GetCostCategoriesQuery());
    }

    public Task<int> CreateCostCategory(ISender sender, CreateCostCategoryCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateCostCategory(ISender sender, int id, UpdateCostCategoryCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteCostCategory(ISender sender, int id)
    {
        await sender.Send(new DeleteCostCategoryCommand(id));
        return Results.NoContent();
    }
}

