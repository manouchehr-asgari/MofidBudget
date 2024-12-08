using MofidBudget.Domain.Entities;

namespace MofidBudget.Application.BeneficiaryType.Queries.GetBeneficiaryTypes;
public class BeneficiaryTypeDto
{
    public BeneficiaryTypeDto()
    {
        Items = Array.Empty<BeneficiaryDto>();
    }

    public int Id { get; init; }

    public string? Title { get; init; }

    public IReadOnlyCollection<BeneficiaryDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<MofidBudget.Domain.Entities.BeneficiaryType, BeneficiaryTypeDto>();
        }
    }
}
