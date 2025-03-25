using FluentValidation;
using Forum.Application.Dtos.PostStatusDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Validators.PostStatuesValidators
{
    public class CreatePostStatusDtoValidator : AbstractValidator<CreatePostStatusDto>
    {
        public CreatePostStatusDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Post Status adı boş olamaz.")
                .MaximumLength(20).WithMessage("Post Status adı en fazla 20 karakter olabilir.");
        }
    }
}
