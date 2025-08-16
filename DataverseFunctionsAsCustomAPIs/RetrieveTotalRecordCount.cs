using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using System;
using System.Text.Json;

namespace DataverseFunctionsAsCustomAPIs
{
    public class RetrieveTotalRecordCount : IPlugin
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
            string[] entityNames = context.InputParameterOrDefault<string[]>("EntityNames");
            // Output Parameters
            string entityRecordCountCollection = default;

            try
            {
                // Execute Dataverse Function
                RetrieveTotalRecordCountRequest retrieveTotalRecordCountRequest = new RetrieveTotalRecordCountRequest { EntityNames = entityNames };
                RetrieveTotalRecordCountResponse retrieveTotalRecordCountResponse = (RetrieveTotalRecordCountResponse)service.Execute(retrieveTotalRecordCountRequest);

                // Get Results
                entityRecordCountCollection = JsonSerializer.Serialize(retrieveTotalRecordCountResponse.EntityRecordCountCollection);
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
            context.OutputParameters["EntityRecordCountCollection"] = entityRecordCountCollection;
        }
    }
}