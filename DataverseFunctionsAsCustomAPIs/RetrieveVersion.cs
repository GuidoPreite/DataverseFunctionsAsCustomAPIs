using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

namespace DataverseFunctionsAsCustomAPIs
{
    public class RetrieveVersion : IPlugin
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

            // Output Parameters
            string version = default;

            try
            {
                // Execute Dataverse Function
                RetrieveVersionRequest retrieveVersionRequest = new RetrieveVersionRequest { };
                RetrieveVersionResponse retrieveVersionResponse = (RetrieveVersionResponse)service.Execute(retrieveVersionRequest);

                // Get Results
                version = retrieveVersionResponse.Version;

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
            context.OutputParameters["Version"] = version;
        }
    }
}