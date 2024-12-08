using MofidBudget.Application.BeneficiaryRelationShips.Queries.GetBeneficiaryRelationShips;
using MofidBudget.Domain.Entities;

namespace MofidBudget.Application.BeneficiaryEmplyees.Queries.GetBeneficiaryEmplyees;
public class BeneficiaryEmplyeeDto
{
    public int Id { get; init; }
    public int BeneficiaryId { get; init; }
    public DateTime? Date { get; set; }
    public int EmployeeCount { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<BeneficiaryEmplyee, BeneficiaryEmplyeeDto>();
        }
    }
}
