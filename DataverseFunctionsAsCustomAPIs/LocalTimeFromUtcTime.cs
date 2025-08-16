using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using System;

namespace DataverseFunctionsAsCustomAPIs
{
    public class LocalTimeFromUtcTime : IPlugin
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
            int timeZoneCode = context.InputParameterOrDefault<int>("TimeZoneCode");
            DateTime utcTime = context.InputParameterOrDefault<DateTime>("UtcTime");

            // Output Parameters
            DateTime localTime = default;

            try
            {
                // Execute Dataverse Function
                LocalTimeFromUtcTimeRequest localTimeFromUtcTimeRequest = new LocalTimeFromUtcTimeRequest { TimeZoneCode = timeZoneCode, UtcTime = utcTime };
                LocalTimeFromUtcTimeResponse localTimeFromUtcTimeResponse = (LocalTimeFromUtcTimeResponse)service.Execute(localTimeFromUtcTimeRequest);

                // Get Results
                localTime = localTimeFromUtcTimeResponse.LocalTime;

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
            context.OutputParameters["LocalTime"] = localTime;
        }
    }
}