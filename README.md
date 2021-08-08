# Community Prototype

A prototype community and discussion system with API written in .NET Core and frontend in Vue.js.  
Datastore is CosmosDB

![](https://img.shields.io/github/license/benc-uk/community-proto)
![](https://img.shields.io/github/last-commit/benc-uk/community-proto)
![](https://img.shields.io/github/release/benc-uk/community-proto)
![](https://img.shields.io/github/checks-status/benc-uk/community-proto/main)
![](https://img.shields.io/github/workflow/status/benc-uk/community-proto/CI%20Build?label=ci-build)
![](https://img.shields.io/github/workflow/status/benc-uk/community-proto/Release%20Assets?label=release)

# Getting Started

## Running as Container

```bash
make image
```

> Note. Set IMAGE_REG, IMAGE_REPO and IMAGE_TAG when calling `make image` to override the defaults

```bash
docker run --rm -it -p 5000:5000 \
-e CosmosDB__Account="https://__CHANGE_ME__.documents.azure.com:443/" \
-e CosmosDB__Key="__CHANGE_ME__" \
ghcr.io/benc-uk/community-proto:latest
```

## Running Locally

Requires:

- Dotnet 5.0 SDK
- Node.js 14+
- CosmosDB account
- Bash
- Make

Copy `appsettings.Development.json.sample` to `appsettings.Development.json` and set the CosmosDB account and key.

```bash
make run
```

# Architecture

Optional. Diagram or description of the overall system architecture, only where applicable.

# Screenshots

Optional. Screenshots can help convey what the project looks like when running and what it's purpose and use is.

# Configuration

Environmental variables for the API server

| Setting / Variable  | Purpose                                     | Default |
| ------------------- | ------------------------------------------- | ------- |
| `CosmosDB__Account` | CosmosDB account URL **_Required_**             | _None_  |
| `CosmosDB__Key`     | CosmosDB key **_Required_** | _None_  |

# Repository Structure

A brief description of the top-level directories of this project is as follows:

```text
/api        - Backend REST API and serving host, written in .NET Core 5.0
/build      - Build configuration e.g. Dockerfiles
/frontend   - Frontend SPA written in VUe.js
```

# API

See the [API documentation](./api/) for full infomration about the API(s).  
Optional. Delete this section if project as no API.

# Known Issues

List any known bugs or gotchas.

# Change Log

See [complete change log](./CHANGELOG.md)

# License

This project uses the MIT software license. See [full license file](./LICENSE)

# Acknowledgements

Optional. Put acknowledgements and credits here, if any
