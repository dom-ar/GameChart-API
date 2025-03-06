using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.ModelBinders
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Check if IEnumerable type
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            // Extract a comma separated string (int ids)
            var providedValue = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName)
                .ToString();

            // Check if it's empty or not
            if (string.IsNullOrEmpty(providedValue))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            // Get what IEnumerable consists of (int (id))
            var genericType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            // converter to genericType type (int (id))
            var converter = TypeDescriptor.GetConverter(genericType);

            // Create an array from provided values from the api string to int type by copying all the values from objectArray to intArray
            //.Split(new[] ....)
            var objectArray = providedValue.Split([","], StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim()))
                .ToArray();

            var intArray = Array.CreateInstance(genericType, objectArray.Length);
            objectArray.CopyTo(intArray, 0);
            bindingContext.Model = intArray;

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
