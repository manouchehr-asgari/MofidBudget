using MofidBudget.Application.BeneficiaryGroups.Commands.BeneficiaryGroup;
using MofidBudget.Application.BeneficiaryGroups.Commands.CreateBeneficiaryGroup;
using MofidBudget.Application.BeneficiaryGroups.Commands.DeleteBeneficiaryGroup;
using MofidBudget.Application.BeneficiaryGroups.Commands.UpdateBeneficiaryGroup;
using MofidBudget.Application.BeneficiaryGroups.Queries.GetBeneficiaryGroups;

namespace MofidBudget.Web.Endpoints;

public class BeneficiaryGroup : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetBeneficiaryGroups)
            .MapPost(CreateBeneficiaryGroup)
            .MapPut(UpdateBeneficiaryGroup, "{id}")
            .MapDelete(DeleteBeneficiaryGroup, "{id}");
    }

    public Task<IReadOnlyCollection<BeneficiaryGroupDto>> GetBeneficiaryGroups(ISender sender)
    {
        return sender.Send(new GetBeneficiaryGroupsQuery());
    }

    public Task<int> CreateBeneficiaryGroup(ISender sender, CreateBeneficiaryGroupCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateBeneficiaryGroup(ISender sender, int id, UpdateBeneficiaryGroupCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteBeneficiaryGroup(ISender sender, int id)
    {
        await sender.Send(new DeleteBeneficiaryGroupCommand(id));
        return Results.NoContent();
    }
}

