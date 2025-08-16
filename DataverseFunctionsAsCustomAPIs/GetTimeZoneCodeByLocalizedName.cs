using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using System;

namespace DataverseFunctionsAsCustomAPIs
{
    public class GetTimeZoneCodeByLocalizedName : IPlugin
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
            string localizedStandardName = context.InputParameterOrDefault<string>("LocalizedStandardName");
            int localeId = context.InputParameterOrDefault<int>("LocaleId");

            // Output Parameters
            int timeZoneCode = default;

            try
            {
                // Execute Dataverse Function
                GetTimeZoneCodeByLocalizedNameRequest getTimeZoneCodeByLocalizedNameRequest = new GetTimeZoneCodeByLocalizedNameRequest { LocalizedStandardName = localizedStandardName, LocaleId = localeId };
                GetTimeZoneCodeByLocalizedNameResponse getTimeZoneCodeByLocalizedNameResponse = (GetTimeZoneCodeByLocalizedNameResponse)service.Execute(getTimeZoneCodeByLocalizedNameRequest);

                // Get Results
                timeZoneCode = getTimeZoneCodeByLocalizedNameResponse.TimeZoneCode;

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
            context.OutputParameters["TimeZoneCode"] = timeZoneCode;
        }
    }
}