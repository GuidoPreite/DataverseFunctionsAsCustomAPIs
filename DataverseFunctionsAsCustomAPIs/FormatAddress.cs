using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using System;

namespace DataverseFunctionsAsCustomAPIs
{
    public class FormatAddress : IPlugin
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
            string line1 = context.InputParameterOrDefault<string>("Line1");
            string city = context.InputParameterOrDefault<string>("City");
            string stateOrProvince = context.InputParameterOrDefault<string>("StateOrProvince");
            string postalCode = context.InputParameterOrDefault<string>("PostalCode");
            string country = context.InputParameterOrDefault<string>("Country");

            // Output Parameters
            string address = default;

            try
            {
                // Execute Dataverse Function
                FormatAddressRequest formatAddressRequest = new FormatAddressRequest { Line1 = line1, City = city, StateOrProvince = stateOrProvince, PostalCode = postalCode, Country = country };
                FormatAddressResponse formatAddressResponse = (FormatAddressResponse)service.Execute(formatAddressRequest);

                // Get Results
                address = formatAddressResponse.Address;
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
            context.OutputParameters["Address"] = address;
        }
    }
}