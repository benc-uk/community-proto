# Common variables
VERSION := 0.0.1
BUILD_INFO := Manual build 
SPA_DIR := frontend
API_DIR := api

# Most likely want to override these when calling `make image`
IMAGE_REG ?= ghcr.io
IMAGE_REPO ?= benc-uk/community-proto
IMAGE_TAG ?= latest
IMAGE_PREFIX := $(IMAGE_REG)/$(IMAGE_REPO)

# Things you don't want to change
REPO_DIR := $(abspath $(dir $(lastword $(MAKEFILE_LIST))))

.PHONY: help image push build run lint lint-fix run run-api run-frontend
.DEFAULT_GOAL := help

help: ## This help message :)
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-20s\033[0m %s\n", $$1, $$2}'

lint: ## Lint & format, will not fix but sets exit code on error
	cd $(API_DIR); dotnet format --check
	cd $(SPA_DIR); npm run lint

image-frontend: ## Build container image for frontend
	docker build --file ./build/frontend.Dockerfile \
	--build-arg BUILD_INFO="$(BUILD_INFO)" \
	--build-arg VERSION="$(VERSION)" \
	--tag $(IMAGE_PREFIX)/frontend:$(IMAGE_TAG) . 

image-api: ## Build container image for API server
	docker build --file ./build/api.Dockerfile \
	--build-arg BUILD_INFO="$(BUILD_INFO)" \
	--build-arg VERSION="$(VERSION)" \
	--tag $(IMAGE_PREFIX)/api:$(IMAGE_TAG) . 

push: ## Push container image to registry
	docker push $(IMAGE_PREFIX)/api:$(IMAGE_TAG)
	docker push $(IMAGE_PREFIX)/frontend:$(IMAGE_TAG)

build: ## Run a local build without a container
	cd $(SPA_DIR); npm run build
	cd $(API_DIR); dotnet publish --configuration Release

run:  run-api run-frontend ## Run both API and frontend server locally, *MUST* -j2 to the make command

run-api: ## Run with hotreload the API, used for local development
	cd $(API_DIR); ASPNETCORE_ENVIRONMENT=Development dotnet watch run

run-frontend: ## Run with hotreload the frontend server, used for local development
	cd $(SPA_DIR); npm run dev