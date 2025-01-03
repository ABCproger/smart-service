namespace SmartService.Core.Models.Dto.CreateEquipmentPlacementContract;

using FluentValidation;

public class CreateEquipmentPlacementContractRequestDtoValidator  : AbstractValidator<CreateEquipmentPlacementContractRequestDto>
{
    public CreateEquipmentPlacementContractRequestDtoValidator()
    {
        RuleFor(dto => dto.ProductionFacilityCode)
            .NotEmpty()
            .WithMessage("Production facility code is required.");

        RuleFor(dto => dto.ProccessQuipmentCode)
            .NotEmpty()
            .WithMessage("Process equipment code is required.");

        RuleFor(dto => dto.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");
    }
}