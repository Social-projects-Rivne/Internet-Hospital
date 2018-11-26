using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace InternetHospital.WebApi.Swagger.OperationFilters
{
	/// <summary>
    /// Add field for auth token input.
    /// </summary>
	public class AddAuthHeaderParam : IOperationFilter
	{
		public void Apply(Operation operation, OperationFilterContext context)
		{
			operation.Parameters = operation.Parameters ?? new List<IParameter>();

			operation.Parameters
				     .Add(new NonBodyParameter
						 {
							 Name = "Authorization",
						 	 In = "header",
						 	 Type = "string",
						 	 Description = "e.g.: bearer [token]"
						 });
		}
	}
}