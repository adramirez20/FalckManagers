param(
    [string] $project = "src/EmployeeManagement.API"
)

Write-Host "Adding initial migration and updating database for project: $project"

Push-Location $project

if (-Not (Test-Path "./Migrations")) {
    dotnet ef migrations add InitialCreate -o Migrations
} else {
    dotnet ef migrations add InitialCreate -o Migrations
}

dotnet ef database update

Pop-Location
