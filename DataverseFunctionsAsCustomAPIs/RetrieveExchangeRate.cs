using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using System;

namespace DataverseFunctionsAsCustomAPIs
{
    public class RetrieveExchangeRate : IPlugin
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
            Guid transactionCurrencyId = context.InputParameterOrDefault<Guid>("TransactionCurrencyId");

            // Output Parameters
            decimal exchangeRate = default;

            try
            {
                // Execute Dataverse Function
                RetrieveExchangeRateRequest retrieveExchangeRateRequest = new RetrieveExchangeRateRequest { TransactionCurrencyId = transactionCurrencyId };
                RetrieveExchangeRateResponse retrieveExchangeRateResponse = (RetrieveExchangeRateResponse)service.Execute(retrieveExchangeRateRequest);

                // Get Results
                exchangeRate = retrieveExchangeRateResponse.ExchangeRate;

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
            context.OutputParameters["ExchangeRate"] = exchangeRate;
        }
    }
}