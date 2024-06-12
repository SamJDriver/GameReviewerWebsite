Write-Host "Loading get-user-secret.ps1"

Function get-user-secret
{
    [CmdletBinding()]
    Param (
        [string]$SecretName
    )
    Process
    {
        $SecretsId = "34a2eb48-f55e-4322-8205-5f51e2572770"
        [string] $SecretValue = ''
        $SecretName = $SecretName + " *" # adding space and asterisk to find the full secret

        $listSecrets = dotnet user-secrets list --id $SecretsId

        if ($listSecrets.Length -gt 0)
        {
            Write-Host "Looking for secret '$SecretName'."

            $SecretValue = $listSecrets.where{$_ -like $SecretName}

            Write-Host "Secret '$SecretValue' found."
        }
    }
}