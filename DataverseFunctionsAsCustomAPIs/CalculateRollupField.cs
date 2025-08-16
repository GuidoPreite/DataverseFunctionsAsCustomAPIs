using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using System;

namespace DataverseFunctionsAsCustomAPIs
{
    public class CalculateRollupField : IPlugin
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
            EntityReference target = context.InputParameterOrDefault<EntityReference>("Target");
            string fieldName = context.InputParameterOrDefault<string>("FieldName");

            // Output Parameters
            Entity entity = default;

            try
            {
                // Execute Dataverse Function
                CalculateRollupFieldRequest calculateRollupFieldRequest = new CalculateRollupFieldRequest { FieldName = fieldName, Target = target };
                CalculateRollupFieldResponse calculateRollupFieldResponse = (CalculateRollupFieldResponse)service.Execute(calculateRollupFieldRequest);

                // Get Results
                entity = calculateRollupFieldResponse.Entity;

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
            context.OutputParameters["Entity"] = entity;
        }
    }
}