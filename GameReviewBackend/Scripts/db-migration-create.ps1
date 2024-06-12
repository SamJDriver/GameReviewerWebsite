param (
    [Parameter(
        Mandatory = $true,
        HelpMessage = "Unique name for the migration to be created")]
        [string]$MigrationName
    )
    
. $PSScriptRoot\get-user-secret.ps1