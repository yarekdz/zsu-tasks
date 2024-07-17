﻿using FluentValidation;
using Tasks.Domain.Shared;

namespace Tasks.Application.Validation
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule,
            Error error)
        {
            return rule
                .WithErrorCode(error.Code)
                .WithMessage(error.Message);
        }
    }
}
