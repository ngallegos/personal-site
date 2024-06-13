FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
WORKDIR ./PersonalSite.Web
RUN dotnet restore
RUN dotnet publish -c release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
ENV ASPNETCORE_ENVIRONMENT=local
EXPOSE 80
#EXPOSE 443
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet","PersonalSite.Web.dll"]

