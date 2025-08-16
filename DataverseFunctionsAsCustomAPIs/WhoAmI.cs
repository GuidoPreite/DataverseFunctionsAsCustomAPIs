using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

namespace DataverseFunctionsAsCustomAPIs
{
    public class WhoAmI : IPlugin
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
            Guid businessUnitId = default;
            Guid organizationId = default;
            Guid userId = default;

            try
            {
                // Execute Dataverse Function
                WhoAmIRequest whoAmIRequest = new WhoAmIRequest { };
                WhoAmIResponse whoAmIResponse = (WhoAmIResponse)service.Execute(whoAmIRequest);

                // Get Results
                businessUnitId = whoAmIResponse.BusinessUnitId;
                organizationId = whoAmIResponse.OrganizationId;
                userId = whoAmIResponse.UserId;

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
            context.OutputParameters["BusinessUnitId"] = businessUnitId;
            context.OutputParameters["OrganizationId"] = organizationId;
            context.OutputParameters["UserId"] = userId;
        }
    }
}