# Community Prototype

A prototype community and discussion system with API written in .NET Core and frontend in Next.js the backend state is held in SQL Server.

![](https://img.shields.io/github/license/benc-uk/community-proto)
![](https://img.shields.io/github/last-commit/benc-uk/community-proto)
![](https://img.shields.io/github/release/benc-uk/community-proto)
![](https://img.shields.io/github/checks-status/benc-uk/community-proto/main)
![](https://img.shields.io/github/workflow/status/benc-uk/community-proto/CI%20Build?label=ci-build)
![](https://img.shields.io/github/workflow/status/benc-uk/community-proto/Release%20Assets?label=release)

# Getting Started

## Running Locally

Requires:

- Dotnet 5.0 SDK
- Node.js 14+
- SQL Server instance (use Azure SQL or run in [Docker](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-bash))
- Bash
- Make
- Entity Framework CLI tools for Dotnet `dotnet tool install --global dotnet-ef`

Copy `appsettings.Development.json.sample` to `appsettings.Development.json` and set the CommunityContext connection string.

The connection string when running SQL Server on Docker looks like:
```bash
"Server=tcp:127.0.0.1,1433;Initial Catalog=communityDb;User ID=SA;Password=__CHANGE_ME__;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;",
```

> !NOTE! The -j2 after make run is important to allow make to run the frontend and API processes side-by-side

```bash
cd api
dotnet ef database update
cd ..
make run -j2
```

Open http://localhost:3000 in your browser

## Running as Container

```bash
make image-api
make image-frontend
```

> Note. Set IMAGE_REG, IMAGE_REPO and IMAGE_TAG when calling `make image` to override the defaults

Run the API server

```bash
docker run --rm -it --network host \
-e ConnectionStrings__CommunityContext="__CHANGE_ME__" \
ghcr.io/benc-uk/community-proto-api:latest
```

Run the frontend server

```bash
docker run --rm -it --network host \
ghcr.io/benc-uk/community-proto-frontend:latest
```

Open http://localhost:3000 in your browser

# Architecture

```text
[ Browser ] -> HTML & JS (port 3000) -> [ Frontend Server ] -> REST (port 5000) -> [ API Server ] -> ( SQL Server DB )
```

# Configuration

Environmental variables for the API server

| Setting / Variable                    | Purpose                                     | Default |
| ------------------------------------- | ------------------------------------------- | ------- |
| `ConnectionStrings__CommunityContext` | SQL Server connection string **_Required_** | _None_  |

Environmental variables for the frontend server


| Setting / Variable | Purpose    | Default  |
| ------------------ | --------------------------------------------------------- | ------------------------- |
| `API_ENDPOINT`     | Endpoint of API, e.g. `https://myserver.foo.com:5000/api` | http://localhost:5000/api | _None_ |


# Repository Structure

A brief description of the top-level directories of this project is as follows:

```text
/api        - Backend REST API and serving host, written in .NET Core 5.0
/build      - Build configuration e.g. Dockerfiles
/frontend   - Frontend SPA written in VUe.js
```

# API

See the Swagger from the API when running http://localhost:5000/swagger/index.html

# Known Issues

It's not finished!

# Change Log

See [complete change log](./CHANGELOG.md)

# License

This project uses the MIT software license. See [full license file](./LICENSE)

# Acknowledgements

Optional. Put acknowledgements and credits here, if any
