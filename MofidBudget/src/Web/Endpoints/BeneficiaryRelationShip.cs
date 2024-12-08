using Microsoft.AspNetCore.Mvc;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.BeneficiaryRelationShips.Commands.CreateBeneficiaryRelationShip;
using MofidBudget.Application.BeneficiaryRelationShips.Commands.DeleteBeneficiaryRelationShip;
using MofidBudget.Application.BeneficiaryRelationShips.Commands.UpdateBeneficiaryRelationShip;
using MofidBudget.Application.BeneficiaryRelationShips.Queries.GetBeneficiaryRelationShips;

namespace MofidBudget.Web.Endpoints;

public class BeneficiaryRelationShips : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetBeneficiaryRelationShips)
            .MapPost(CreateBeneficiaryRelationShip)
            .MapPut(UpdateBeneficiaryRelationShip, "{id}")
            .MapDelete(DeleteBeneficiaryRelationShip, "{id}");
    }

    public Task<PaginatedList<BeneficiaryRelationShipDto>> GetBeneficiaryRelationShips(ISender sender, [FromBody]GetBeneficiaryRelationShipsWithPaginationQuery command)
    {
        return sender.Send(command);
    }

    public Task<int> CreateBeneficiaryRelationShip(ISender sender, CreateBeneficiaryRelationShipCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateBeneficiaryRelationShip(ISender sender, int id, UpdateBeneficiaryRelationShipCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteBeneficiaryRelationShip(ISender sender, int id)
    {
        await sender.Send(new DeleteBeneficiaryRelationShipCommand(id));
        return Results.NoContent();
    }
}

