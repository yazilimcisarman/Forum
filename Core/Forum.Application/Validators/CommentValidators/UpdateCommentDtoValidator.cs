using FluentValidation;
using Forum.Application.Dtos.CommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Validators.CommentValidators
{
    public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentDtoValidator()
        {
            RuleFor(x => x.Content)
           .NotEmpty().WithMessage("Yorum içeriği boş olamaz.")
           .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir.");

        }
    }
}
