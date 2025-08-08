FROM mcr.microsoft.com/dotnet/sdk:6.0.428-1-alpine3.20 AS sdk

ENV PATH="/root/.dotnet/tools:${PATH}"
RUN dotnet tool install --global Microsoft.OData.Cli

ENTRYPOINT [ "odata-cli" ]

# docker build . -t odata-cli 