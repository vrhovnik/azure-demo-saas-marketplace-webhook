# Azure demo saas marketplace webhook implementation

Demo webhook as an example for SaaS Azure marketplace offering. This is sample implementation in [.NET](https://dot.net) for the purpose of hackathon and integration into Azure SaaS solution
on the [Azure commercial marketplace](https://docs.microsoft.com/en-us/azure/marketplace/). Guide on how to implement webhook is available [here](https://docs.microsoft.com/en-us/azure/marketplace/partner-center-portal/pc-saas-fulfillment-webhook).

## Structure

Solution is built on [.NET Core 6](https://dot.net) as [Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview) and is structured in the following way:
1. **SaaSFunctions** - contains code for sending email via SendGrid provider and handling webhook calls from Azure Marketplace based on documentation.
2. **SaasFunctionsTests** - Unit tests to check the validity of requests to webhook to understand functionality and ways to test it out without Azure Commercial marketplace.

![Solution Structure](https://webeudatastorage.blob.core.windows.net/web/saas-hackathon-webhook-solution-structure.png)

## Run instructions

If you want to run it locally, you will need to setup local environment. [Here](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-local) are the instructions for different platforms and tools to work with. You will need to have [.NET](https://dot.net) installed.

To deploy Azure function in production or Azure environment, you can use different deployment options, described [here](https://docs.microsoft.com/en-us/azure/azure-functions/functions-deployment-technologies). If you don't have tools
you can use [zip deploy](https://docs.microsoft.com/en-us/azure/azure-functions/functions-deployment-technologies#zip-deploy) option. In that case, you will need [Azure](https://azure.com) subscription. Check [options here](https://azure.microsoft.com/en-us/pricing/purchase-options/).

# Additional links and credits

1. [Azure Subscription Free Account](https://azure.microsoft.com/en-us/free/) and other [Azure available options](https://azure.microsoft.com/en-us/pricing/purchase-options/)
2. [Azure Functions documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview)
3. [Azure commercial marketplace](https://docs.microsoft.com/en-us/azure/marketplace/)
4. [Azure SaaS Webhook Integration and documentation](https://docs.microsoft.com/en-us/azure/marketplace/partner-center-portal/pc-saas-fulfillment-webhook)
5. [Azure Commercial Marketplace SaaS Accelerator](https://github.com/Azure/Commercial-Marketplace-SaaS-Accelerator)