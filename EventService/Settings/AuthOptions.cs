using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Features.Settings;
/// <summary>
/// Настройка получения JWT-Token
/// </summary>
public class AuthOptions
{
    /// <summary>
    /// Издатель токена
    /// </summary>
    public const string Issuer = "MyAuthServer";
    /// <summary>
    /// Потребитель токена
    /// </summary>
    public const string Audience = "MyAuthClient";

    private const string Key = "mysupersecret_secretkey!123";
    /// <summary>
    /// Получение ключа
    /// </summary>
    /// <returns></returns>
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        // ReSharper disable once ArrangeObjectCreationWhenTypeEvident
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}

