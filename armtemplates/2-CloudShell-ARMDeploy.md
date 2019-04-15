# ARM Deployment via Azure Cloud Shell
## Configure and clone GitHub  Repo
- <b>Azure Resource Manager Tools</b><br/>
[msazurermtools.azurerm-vscode-tools](https://marketplace.visualstudio.com/items?itemName=msazurermtools.azurerm-vscode-tools)<br/>
<i>Provides language support for Azure Resource Manager deployment templates and template language expressions.</i>

- <b>Azure Resource Manager Snippets</b><br/>
[samcogan.arm-snippets](https://marketplace.visualstudio.com/items?itemName=samcogan.arm-snippets)<br/>
<i/>Provides a single Azure sign-in and subscription filtering experience for all other Azure extensions. Azure Cloud Shell service is available in VS Code's integrated terminal.</i>

- <b>Azure Account and Sign-In</b><br/>
[wilfriedwoivre.arm-params-generator](https://marketplace.visualstudio.com/items?itemName=wilfriedwoivre.arm-params-generator)<br/>
<i/>Generates new and also consolidate existing parameters file parameter based on ARM templates.</i>

## Test and deploy ARM Templates
### Test ARM templates before deploying<br>
    Test-AzureRmResourceGroupDeployment
    -TemplateParameterFile .\keyvault.deploy.parameters.json
    -TemplateFile .\keyvault.deploy.json 

### Apply ARM template to resource group<br>

    New-AzureRmResourceGroupDeployment -TemplateParameterFile .\keyvaultd.deploy.parameters.json -TemplateFile .\keyvault.deploy.json -ResourceGroupName „azworkshop“ –DeploymentName „InitialDeployment“


## Appendix: Local PowerShell configuration
    Install-Module Az
    Enable-AzureRMAlias
    Connect-AzAccount

<i> Cloud Shell is still using AzureRM PowerShell modules there fore my example includes compatibility mode to add aliases for AzureRM.
