using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

public class SwaggerExcludeFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema?.Properties == null || context == null)
            return;

        // Obtém todas as propriedades marcadas com SwaggerExcludeAttribute
        var excludedProperties = context.Type.GetProperties()
            .Where(prop => prop.GetCustomAttribute<SwaggerExcludeAttribute>() != null);

        foreach (var excludedProperty in excludedProperties)
        {
            // Converte o nome da propriedade para o formato que aparece no schema (camelCase)
            var propertyName = char.ToLowerInvariant(excludedProperty.Name[0]) + excludedProperty.Name.Substring(1);
            if (schema.Properties.ContainsKey(propertyName))
                schema.Properties.Remove(propertyName);
        }
    }
}
