using FluentValidation;
using Moor.Model.Dtos.MoorDto.CarDto;

namespace Moor.Service.Validations
{
    public class CarDtoValidator : AbstractValidator<CarDto>
    {
        public CarDtoValidator()
        {
            RuleFor(x => x.CarParameterId).NotNull().WithMessage("Araca ait marka model bilgisi boş olamaz.").NotEmpty().WithMessage("Araca ait marka model bilgisi boş olamaz.");
            RuleFor(x => x.NumberPlate).NotNull().WithMessage("Plaka bilgisi boş olamaz.").NotEmpty().WithMessage("Plaka bilgisi boş olamaz.");
        }
    }
}