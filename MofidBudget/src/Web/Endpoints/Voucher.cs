using Microsoft.AspNetCore.Mvc;
using MofidBudget.Application.Common.Models;
using MofidBudget.Application.CostGroups.Queries.GetCostGroups;
using MofidBudget.Application.Vouchers.Commands.CreateVoucher;
using MofidBudget.Application.Vouchers.Commands.DeleteVoucher;
using MofidBudget.Application.Vouchers.Commands.UpdateVoucher;
using MofidBudget.Application.Vouchers.Queries.GetBeneficiary;
using MofidBudget.Application.Vouchers.Queries.GetVouchers;


namespace MofidBudget.Web.Endpoints;

public class Vouchers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetVouchers)
           .MapGet(GetVoucherByBeneficiary, "VoucherByBeneficiary")
           .MapGet(GetVoucherByCostType, "VoucherByCostType")
            .MapPost(CreateVoucher)
            .MapPut(UpdateVoucher, "{id}")
            .MapDelete(DeleteVoucher, "{id}");
    }

    public Task<PaginatedList<VoucherDto>> GetVouchers(ISender sender, [FromBody]GetVouchersWithPaginationQuery command)
    {
        return sender.Send(command);
    }

    public Task<PaginatedList<VoucherDto>> GetVoucherByBeneficiary(ISender sender, [FromBody] GetVouchersByBeneficiayWithPaginationQuery command)
    {
        return sender.Send(command);
    }

    public Task<PaginatedList<VoucherDto>> GetVoucherByCostType(ISender sender, [FromBody] GetVouchersByCostTypeWithPaginationQuery command)
    {
        return sender.Send(command);
    }
    public Task<int> CreateVoucher(ISender sender, CreateVoucherCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateVoucher(ISender sender, int id, UpdateVoucherCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteVoucher(ISender sender, int id)
    {
        await sender.Send(new DeleteVoucherCommand(id));
        return Results.NoContent();
    }
}

