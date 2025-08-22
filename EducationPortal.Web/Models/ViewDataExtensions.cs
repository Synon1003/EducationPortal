using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EducationPortal.Web.Models;

public static class TempDataExtensions
{
    public static T? Get<T>(this ITempDataDictionary tempData, string key) where T : class
    {
        tempData.TryGetValue(key, out object? o);
        return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
    }

    public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
    {
        tempData[key] = JsonSerializer.Serialize(value);
    }

    public static void CreateFlash(this ITempDataDictionary tempData, string message, string type)
    {
        tempData.Put("flash", new FlashViewModel()
        {
            Message = message,
            Type = type
        });
    }
}