using System.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Lingonberry.Api.UseCases.Handlers;

/// <summary>
/// Class for deserialize collection from query.
/// </summary>
/// <typeparam name="T">ICollection.</typeparam>
public class SwaggerCollection<T> : IModelBinder where T : ICollection
{
    /// <summary>
    /// Bind model.
    /// </summary>
    /// <param name="bindingContext">ModelBindingContext.</param>
    /// <returns>Tas.</returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var val = bindingContext.ValueProvider.GetValue(bindingContext.FieldName).ToString();
        var first = string.Concat("[", string.Join(",", val.ToList())
            .Replace("\n", ""), "]");
        var model = JsonConvert.DeserializeObject<T>(first);
        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}
