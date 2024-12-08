using MofidBudget.Application.BeneficiaryEmplyees.Commands.CreateBeneficiaryEmplyee;
using MofidBudget.Application.BeneficiaryEmplyees.Commands.DeleteBeneficiaryEmplyee;
using MofidBudget.Application.BeneficiaryEmplyees.Commands.UpdateBeneficiaryEmplyee;
using MofidBudget.Application.BeneficiaryEmplyees.Queries.GetBeneficiaryEmplyees;

namespace MofidBudget.Web.Endpoints;

public class BeneficiaryEmplyees : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetBeneficiaryEmplyees)
            .MapPost(CreateBeneficiaryEmplyee)
            .MapPut(UpdateBeneficiaryEmplyee, "{id}")
            .MapDelete(DeleteBeneficiaryEmplyee, "{id}");
    }

    public Task<IReadOnlyCollection<BeneficiaryEmplyeeDto>> GetBeneficiaryEmplyees(ISender sender)
    {
        return sender.Send(new GetBeneficiaryEmplyeesQuery());
    }

    public Task<int> CreateBeneficiaryEmplyee(ISender sender, CreateBeneficiaryEmplyeeCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateBeneficiaryEmplyee(ISender sender, int id, UpdateBeneficiaryEmplyeeCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteBeneficiaryEmplyee(ISender sender, int id)
    {
        await sender.Send(new DeleteBeneficiaryEmplyeeCommand(id));
        return Results.NoContent();
    }
}

