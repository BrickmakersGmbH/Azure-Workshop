# ARM Deployment via Azure Cloud Shell

## Clone repository to cloud drive
### Change to local (personal) persistent cloud drive
    cd $clouddrive

### Git clone of repository
    git clone https://github.com/BrickmakersGmbH/Azure-Workshop.git

## Test and deploy ARM Templates
### Test ARM templates before deploying<br>
    Test-AzureRmResourceGroupDeployment `
    -TemplateParameterFile .\keyvault.deploy.parameters.json `
    -TemplateFile .\keyvault.deploy.json `
    -ResourceGroupName „AzWorkshop“

### Apply ARM template to resource group<br>
    New-AzureRmResourceGroupDeployment `
    -TemplateParameterFile .\keyvault.deploy.parameters.json `
    -TemplateFile .\keyvault.deploy.json `
    -ResourceGroupName „AzWorkshop“ `
    –DeploymentName „InitialDeployment“

## Appendix: Local PowerShell configuration
    Install-Module Az
    Enable-AzureRMAlias
    Connect-AzAccount

<i> Cloud Shell is still using AzureRM PowerShell module therefore my example includes compatibility mode to add aliases for AzureRM.
