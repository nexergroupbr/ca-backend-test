using FluentValidation.Resources;

namespace Services.Validators;

public class CustomLanguageManager
     : LanguageManager
{
    public CustomLanguageManager()
    {
        AddTranslation("en", "NotEmptyValidator", "cannot be empty!");
        AddTranslation("en", "MaximumLengthValidator", "maximum {MaxLength} character(s)!");
        AddTranslation("en", "ExactLengthValidator", "must have {MaxLength} character(s)!");
        AddTranslation("en", "EnumValidator", "invalid value!");
        AddTranslation("en", "EmailValidator", "invalid email address!");
    }
}
