using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

namespace DataverseFunctionsAsCustomAPIs
{
    public class RetrieveAvailableLanguages : IPlugin
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
            string[] localeIds = default;

            try
            {
                // Execute Dataverse Function
                RetrieveAvailableLanguagesRequest retrieveAvailableLanguagesRequest = new RetrieveAvailableLanguagesRequest { };
                RetrieveAvailableLanguagesResponse retrieveAvailableLanguagesResponse = (RetrieveAvailableLanguagesResponse)service.Execute(retrieveAvailableLanguagesRequest);

                // Get Results
                localeIds = retrieveAvailableLanguagesResponse.LocaleIds.Select(t => t.ToString()).ToArray();
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
            context.OutputParameters["LocaleIds"] = localeIds;
        }
    }
}