using Features.Events.Data;
using Features.Events.Domain;
using FluentValidation;

namespace EventService.Events.Validators;

public class AddEventCommandValidator : AbstractValidator<Event>
{
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
        FakeData fakeData = new();
        foreach (var item in fakeData.Image)
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
        FakeData fakeData = new();
        foreach(var item in fakeData.Room) 
        {
            if(item == id)
            {
                return true;
            }
        }
        return false;
    }
}
