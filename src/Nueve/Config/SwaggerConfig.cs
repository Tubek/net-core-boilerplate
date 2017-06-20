using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Nueve.Config
{
    /// <summary>
    /// Swagger config
    /// </summary>
    public class SwaggerConfig
    {
        private static string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return System.IO.Path.Combine(app.ApplicationBasePath, "Nueve.xml");
        }

        /// <summary>
        /// Register 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var pathToDoc = GetXmlCommentsPath();

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.IncludeXmlComments(pathToDoc);
                options.DescribeAllEnumsAsStrings();
                options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }


        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUi("api/docs");
        }

        /// <summary>
        /// 
        /// </summary>
        public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="operation"></param>
            /// <param name="context"></param>
            public void Apply(Operation operation, OperationFilterContext context)
            {
                var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
                var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
                var allowAnonymous = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);

                if (isAuthorized && !allowAnonymous)
                {
                    if (operation.Parameters == null)
                        operation.Parameters = new List<IParameter>();

                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Name = "Authorization",
                        In = "header",
                        Description = "access token",
                        Required = false,
                        Type = "string"
                    });
                }
            }
        }

    }
}
