# Azure demo saas marketplace webhook implementation

Demo webhook as an example for SaaS Azure marketplace offering. This is sample implementation in [.NET](https://dot.net) for the purpose of hackathon and integration into Azure SaaS solution
on the [Azure commercial marketplace](https://docs.microsoft.com/en-us/azure/marketplace/). Guide on how to implement webhook is available [here](https://docs.microsoft.com/en-us/azure/marketplace/partner-center-portal/pc-saas-fulfillment-webhook).

## Structure

Solution is built on [.NET Core 6](https://dot.net) as [Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview) and is structured in the following way:
1. **SaaSFunctions** - contains code for sending email via SendGrid provider and handling webhook calls from Azure Marketplace based on documentation.
2. **SaasFunctionsTests** - Unit tests to check the validity of requests to webhook to understand functionality and ways to test it out without Azure Commercial marketplace.

## Run instructions

TBD

# Additional links and credits

1. [Azure Functions documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview)
2. [Azure commercial marketplace](https://docs.microsoft.com/en-us/azure/marketplace/)
3. [Azure SaaS Webhook Integration and documentation](https://docs.microsoft.com/en-us/azure/marketplace/partner-center-portal/pc-saas-fulfillment-webhook)
4. [Azure Commercial Marketplace SaaS Accelerator](https://github.com/Azure/Commercial-Marketplace-SaaS-Accelerator)