# --- Generate C# client ---
kiota generate `
    --openapi "./resources/exactonline-openapi.json" `
    --clean-output `
    --language CSharp `
    --output "./src/ExactOnline.Api.Client/Generated" `
    --namespace-name "ExactOnline.Api.Client" `
    --class-name "ExactOnlineServiceClient" `

Write-Output "✅ C# client code generated"