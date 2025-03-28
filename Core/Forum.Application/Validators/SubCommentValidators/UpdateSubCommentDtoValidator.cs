using FluentValidation;
using Forum.Application.Dtos.SubCommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Validators.SubCommentValidators
{
    public class UpdateSubCommentDtoValidator : AbstractValidator<UpdateSubCommentDto>
    {
        public UpdateSubCommentDtoValidator()
        {
            RuleFor(x => x.Content)
           .NotEmpty().WithMessage("Yorum içeriği boş olamaz.")
           .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID girilmelidir.");

        }
    }
}
