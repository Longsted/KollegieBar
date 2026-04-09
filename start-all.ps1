docker compose up -d
Write-Host "Started docker"
Start-Sleep -Seconds 2
write-Host "Starting App"
dotnet run --project App