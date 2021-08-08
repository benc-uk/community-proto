ARG DOTNET_IMAGE_BASE=5.0-alpine
ARG NODE_IMAGE_BASE=14-alpine
ARG BUILD_INFO="No build info"
ARG VERSION=0.0.1

# =======================================================
# Stage 1 - Build/bundle frontend SPA
# =======================================================
FROM node:$NODE_IMAGE_BASE AS spa-build
WORKDIR /build

COPY frontend/package*.json ./
RUN npm install

COPY frontend/ .
RUN npm run build

# =======================================================
# Stage 2 - Build backend API and server
# =======================================================
FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_IMAGE_BASE as server-build
WORKDIR /build

# Copy project source files
COPY api ./

# Restore, build & publish
WORKDIR /build
RUN dotnet publish --configuration Release

# =======================================================
# Stage 3 - Assemble runtime container
# =======================================================
FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_IMAGE_BASE

# Seems as good a place as any
WORKDIR /app

# Copy already published binaries (from build stage image)
COPY --from=server-build /build/bin/Release/net5.0/publish/ .
# Copy SPA bundle to wwwroot for static serving
COPY --from=spa-build /build/dist ./wwwroot

# Expose port 5000 from Kestrel webserver
EXPOSE 5000

# Tell Kestrel to listen on port 5000 and serve plain HTTP
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT Production
ENV VERSION $VERSION
ENV BUILD_INFO $BUILD_INFO

# Run the ASP.NET Core app
ENTRYPOINT dotnet CommunityApi.dll