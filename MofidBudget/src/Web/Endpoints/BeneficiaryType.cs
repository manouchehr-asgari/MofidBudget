using MofidBudget.Application.BeneficiaryType.Commands.CreateBeneficiaryType;
using MofidBudget.Application.BeneficiaryType.Commands.DeleteBeneficiaryType;
using MofidBudget.Application.BeneficiaryType.Commands.UpdateBeneficiaryType;
using MofidBudget.Application.BeneficiaryType.Queries.GetBeneficiaryTypes;

namespace MofidBudget.Web.Endpoints;

public class BeneficiaryType : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetBeneficiaryType)
            .MapPost(CreateBeneficiaryType)
            .MapPut(UpdateBeneficiaryType, "{id}")
            .MapDelete(DeleteBeneficiaryType, "{id}");
    }

    public Task<IReadOnlyCollection<BeneficiaryTypeDto>> GetBeneficiaryType(ISender sender)
    {
        return sender.Send(new GetBeneficiaryTypesQuery());
    }

    public Task<int> CreateBeneficiaryType(ISender sender, CreateBeneficiaryTypeCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateBeneficiaryType(ISender sender, int id, UpdateBeneficiaryTypeCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteBeneficiaryType(ISender sender, int id)
    {
        await sender.Send(new DeleteBeneficiaryTypeCommand(id));
        return Results.NoContent();
    }
}

