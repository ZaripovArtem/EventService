using Features.Events.Data;
using Features.Events.Domain;
using FluentValidation;

namespace Features.Events.Validators;

/// <summary>
/// Validator
/// </summary>
// ReSharper disable once UnusedMember.Global Валидатор
public class AddEventCommandValidator : AbstractValidator<Event>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public AddEventCommandValidator()
    {
        RuleFor(x => x.EventName).NotEmpty()
            .WithMessage("Введите название мероприятия");
        RuleFor(x => x.PlaceId).Must(BeAValidPlaceId)
            .WithMessage("Id пространства не существует");
        RuleFor(x => x.PhotoId).Must(BeAValidPhotoId)
            .When(x => x.PhotoId.HasValue).WithMessage("Id фото не существует");
    }

    private bool BeAValidPhotoId(Guid? id)
    {
        Repository data = new();
        foreach (var item in data.Image)
        {
            if (item == id)
            {
                return true;
            }
        }
        return false;
    }

    private bool BeAValidPlaceId(Guid id)
    {
        Repository data = new();
        foreach (var item in data.Room) 
        {
            if(item == id)
            {
                return true;
            }
        }
        return false;
    }
}
