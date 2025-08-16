using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using System;

namespace DataverseFunctionsAsCustomAPIs
{
    public class GetAllTimeZonesWithDisplayName : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Default Output Parameters
            bool success = false;
            string errorMessage = "";

            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            // Input Parameters
            int localeId = context.InputParameterOrDefault<int>("LocaleId");

            // Output Parameters
            EntityCollection entityCollection = default;

            try
            {
                // Execute Dataverse Function
                GetAllTimeZonesWithDisplayNameRequest getAllTimeZonesWithDisplayNameRequest = new GetAllTimeZonesWithDisplayNameRequest { LocaleId = localeId };
                GetAllTimeZonesWithDisplayNameResponse getAllTimeZonesWithDisplayNameResponse = (GetAllTimeZonesWithDisplayNameResponse)service.Execute(getAllTimeZonesWithDisplayNameRequest);

                // Get Results
                entityCollection = getAllTimeZonesWithDisplayNameResponse.EntityCollection;

                success = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            // Assign Default Output Parameters
            context.OutputParameters["Success"] = success;
            context.OutputParameters["ErrorMessage"] = errorMessage;

            // Assign Output Parameters
            context.OutputParameters["EntityCollection"] = entityCollection;
        }
    }
}