using Microsoft.AspNetCore.Mvc;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;
using MofidBudget.Application.Beneficiaries.Commands.CreateBeneficiary;
using MofidBudget.Application.Beneficiaries.Commands.DeleteBeneficiary;
using MofidBudget.Application.Beneficiaries.Commands.UpdateBeneficiary;
using MofidBudget.Application.Beneficiaries.Queries.GetBeneficiaries;
using MofidBudget.Application.BeneficiaryType.Queries.GetBeneficiaryTypes;
using MofidBudget.Application.CostTypes.Queries.GetCostTypes;

namespace MofidBudget.Web.Endpoints;

public class Beneficiaries : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetBeneficiaries)
           .MapGet(GetBeneficiaryByGroup, "BeneficiaryByGroup")
           .MapGet(GetBeneficiaryByType, "BeneficiaryByType")
            .MapPost(CreateBeneficiary)
            .MapPut(UpdateBeneficiary, "{id}")
            .MapDelete(DeleteBeneficiary, "{id}");
    }

    public Task<PaginatedList<BeneficiaryDto>> GetBeneficiaries(ISender sender, int pageSize=50,int pageNumber = 1)
    {
        GetBeneficiariesWithPaginationQuery command=new () { PageNumber=pageNumber,PageSize=pageSize};
        return sender.Send(command);
    }

    public Task<PaginatedList<BeneficiaryDto>> GetBeneficiaryByType(ISender sender, [FromBody] GetBeneficiariesByTypeWithPaginationQuery command)
    {
        return sender.Send(command);
    }

    public Task<PaginatedList<BeneficiaryDto>> GetBeneficiaryByGroup(ISender sender, [FromBody] GetBeneficiariesByGroupWithPaginationQuery command)
    {
        return sender.Send(command);
    }
    public Task<int> CreateBeneficiary(ISender sender, CreateBeneficiaryCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateBeneficiary(ISender sender, int id, UpdateBeneficiaryCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteBeneficiary(ISender sender, int id)
    {
        await sender.Send(new DeleteBeneficiaryCommand(id));
        return Results.NoContent();
    }
}

