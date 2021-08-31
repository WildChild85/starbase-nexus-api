using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

// TODO: find reference and put it in here to credit the author
namespace starbase_nexus_api.Filters.Api
{
    public class JsonPatchDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Remove irrelevent schemas
            List<KeyValuePair<string, OpenApiSchema>> schemas = swaggerDoc.Components.Schemas.ToList();
            foreach (KeyValuePair<string, OpenApiSchema> item in schemas)
            {
                if (item.Key.StartsWith("OperationOf") || item.Key.StartsWith("JsonPatchDocumentOf"))
                    swaggerDoc.Components.Schemas.Remove(item.Key);
            }

            // Add accurate PatchDocument schema
            swaggerDoc.Components.Schemas.Add("Operation", new OpenApiSchema
            {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    {"op", new OpenApiSchema{ Type = "string" } },
                    {"value", new OpenApiSchema{ Type = "object", Nullable = true } },
                    {"path", new OpenApiSchema{ Type = "string" } }
                }
            });
            swaggerDoc.Components.Schemas.Add("JsonPatchDocument", new OpenApiSchema
            {
                Type = "array",
                Items = new OpenApiSchema
                {
                    Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "Operation" }
                },
                Description = "Array of operations to perform"
            });

            // Fix up the patch references
            foreach (KeyValuePair<OperationType, OpenApiOperation> path in swaggerDoc.Paths.SelectMany(p => p.Value.Operations)
                .Where(p => p.Key == OperationType.Patch))
            {
                if (path.Value.RequestBody != null)
                {
                    foreach (KeyValuePair<string, OpenApiMediaType> item in path.Value.RequestBody.Content.Where(c => c.Key != "application/json-patch+json"))
                    {
                        path.Value.RequestBody.Content.Remove(item.Key);
                    }
                    KeyValuePair<string, OpenApiMediaType> response = path.Value.RequestBody.Content.Single(c => c.Key == "application/json-patch+json");
                    response.Value.Schema = new OpenApiSchema
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "JsonPatchDocument" }
                    };
                }
            }
        }
    }
}
