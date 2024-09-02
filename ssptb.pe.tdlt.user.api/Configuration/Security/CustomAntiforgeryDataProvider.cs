using Microsoft.AspNetCore.Antiforgery;
using ssptb.pe.tdlt.user.common.Enums;
using ssptb.pe.tdlt.user.common.Exceptions;
using ssptb.pe.tdlt.user.redis.Services;

namespace ssptb.pe.tdlt.user.api.Configuration.Security;

public class CustomAntiforgeryDataProvider(IRedisService redisService) : IAntiforgeryAdditionalDataProvider
{
    private readonly IRedisService _redisService = redisService;

    public string GetAdditionalData(HttpContext context)
    {
        string guid = Guid.NewGuid().ToString();
        string key = $"SSPTB_User_FT_{guid}";

        _redisService.SaveInformation(key, guid, TimeSpan.FromMinutes(1));

        return guid;
    }

    public bool ValidateAdditionalData(HttpContext context, string additionalData)
    {
        string key = $"SSPTB_User_FT_{additionalData}";
        string guid = _redisService.GetInformation(key);
        bool resultValidation = guid == additionalData;

        if (resultValidation)
            _redisService.DeleteInformation(key);
        else
            throw new CustomException("Error en Forgery Token", ApiErrorCode.ValidationError);

        return resultValidation;
    }
}