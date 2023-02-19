using FluentValidation;
using Moor.Service.Models.Dto.MoorDto;

namespace Moor.Service.Validations
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            //RuleFor(x => x.Email).NotNull().WithMessage("E-Mail Boş olamaz.").NotEmpty().WithMessage("E-Mail Boş olamaz.");
            //RuleFor(x => x.Message).NotNull().WithMessage("Mesaj Boş olamaz.").NotEmpty().WithMessage("Mesaj Boş olamaz.");
            //RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Telefon No Boş olamaz.").NotEmpty().WithMessage("Telefon No Boş olamaz.");
        }
    }
}