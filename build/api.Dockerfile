ARG DOTNET_IMAGE_BASE=5.0-alpine
ARG BUILD_INFO="No build info"
ARG VERSION=0.0.1

# =======================================================
# Stage 1 - Build backend API and server
# =======================================================
FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_IMAGE_BASE as server-build
WORKDIR /build

# Copy project source files
COPY api ./

# Restore, build & publish
WORKDIR /build
RUN dotnet publish --configuration Release

# =======================================================
# Stage 2 - Assemble runtime container
# =======================================================
FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_IMAGE_BASE
RUN apk add icu-libs

# Seems as good a place as any
WORKDIR /app

# Copy already published binaries (from build stage image)
COPY --from=server-build /build/bin/Release/net5.0/publish/ .

# Expose port 5000 from Kestrel webserver
EXPOSE 5000

# Tell Kestrel to listen on port 5000 and serve plain HTTP
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT Production
ENV VERSION $VERSION
ENV BUILD_INFO $BUILD_INFO

# Important, see https://github.com/dotnet/SqlClient/issues/220
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Run the ASP.NET Core app
ENTRYPOINT dotnet CommunityApi.dll