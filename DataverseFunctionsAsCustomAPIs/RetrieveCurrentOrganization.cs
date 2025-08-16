using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using System;
using System.Text.Json;

namespace DataverseFunctionsAsCustomAPIs
{
    public class RetrieveCurrentOrganization : IPlugin
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
            int accessType = context.InputParameterOrDefault<int>("AccessType");

            // Output Parameters
            Guid dataCenterId = default;
            string endpoints = default;
            string environmentId = default;
            string friendlyName = default;
            string geo = default;
            Guid organizationId = default;
            int organizationType = default;
            string organizationVersion = default;
            string schemaType = default;
            int state = default;
            string tenantId = default;
            string uniqueName = default;
            string urlName = default;

            try
            {
                // Execute Dataverse Function
                RetrieveCurrentOrganizationRequest retrieveCurrentOrganizationRequest = new RetrieveCurrentOrganizationRequest { AccessType = (Microsoft.Xrm.Sdk.Organization.EndpointAccessType)accessType };
                RetrieveCurrentOrganizationResponse retrieveCurrentOrganizationResponse = (RetrieveCurrentOrganizationResponse)service.Execute(retrieveCurrentOrganizationRequest);

                // Get Results
                dataCenterId = retrieveCurrentOrganizationResponse.Detail.DatacenterId;
                endpoints = JsonSerializer.Serialize(retrieveCurrentOrganizationResponse.Detail.Endpoints);
                environmentId = retrieveCurrentOrganizationResponse.Detail.EnvironmentId;
                friendlyName = retrieveCurrentOrganizationResponse.Detail.FriendlyName;
                geo = retrieveCurrentOrganizationResponse.Detail.Geo;
                organizationId = retrieveCurrentOrganizationResponse.Detail.OrganizationId;
                organizationType = (int)retrieveCurrentOrganizationResponse.Detail.OrganizationType;
                organizationVersion = retrieveCurrentOrganizationResponse.Detail.OrganizationVersion;
                schemaType = retrieveCurrentOrganizationResponse.Detail.SchemaType;
                state = (int)retrieveCurrentOrganizationResponse.Detail.State;
                tenantId = retrieveCurrentOrganizationResponse.Detail.TenantId;
                uniqueName = retrieveCurrentOrganizationResponse.Detail.UniqueName;
                urlName = retrieveCurrentOrganizationResponse.Detail.UrlName;

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
            context.OutputParameters["DataCenterId"] = dataCenterId;
            context.OutputParameters["Endpoints"] = endpoints;
            context.OutputParameters["EnvironmentId"] = environmentId;
            context.OutputParameters["FriendlyName"] = friendlyName;
            context.OutputParameters["Geo"] = geo;
            context.OutputParameters["OrganizationId"] = organizationId;
            context.OutputParameters["OrganizationType"] = organizationType;
            context.OutputParameters["OrganizationVersion"] = organizationVersion;
            context.OutputParameters["SchemaType"] = schemaType;
            context.OutputParameters["State"] = state;
            context.OutputParameters["TenantId"] = tenantId;
            context.OutputParameters["UniqueName"] = uniqueName;
            context.OutputParameters["UrlName"] = urlName;
        }
    }
}