﻿using System;
using System.Linq;
using BulbaCourses.PracticalMaterialsTests.Logic.Models.Test.Questions;
using FluentValidation;

namespace BulbaCourses.PracticalMaterialsTests.Logic.Validators.Test.Questions
{
    public class Validator_Question_SetOrder : AbstractValidator<MQuestion_SetOrder>
    {
        public Validator_Question_SetOrder()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleSet("Validation_MQuestion_SetOrder", () => {
                RuleFor(x => x.QuestionText)
                    .Length(20, 50).WithMessage("Длина QuestionText должна быть от 20 до 50 символов")
                    .Must(x => !x.All(Char.IsDigit)).WithMessage("Наименование не может быть только из цифр")
                    .Must(x => !x.All(Char.IsSymbol)).WithMessage("Наименование не может быть только из символов")
                    .Must(x => !String.IsNullOrWhiteSpace(x)).WithMessage("Наименование не может быть только из пробелов");
            });
        }
    }
}
