# ==========================
# Makefile for .NET Clean Architecture
# ==========================

SOLUTION = dotNET-CleanArchitecture-Boilerplate.slnx

.PHONY: help build test clean run-api migrate migration

# --------------------------
# Help
# --------------------------

help:
	@echo "Available commands:"
	@echo "  make init          - First time: install tools + restore"
	@echo "  make restore       - dotnet restore"
	@echo "  make build         - Build solution (Release)"
	@echo "  make build-debug   - Build (Debug)"
	@echo "  make test          - Run all tests"
	@echo "  make run-api       - Run API-project"
	@echo "  make clean         - Clear bin/obj"
	@echo "  make format        - Run dotnet format (if installed)"

build:
	dotnet build $(SOLUTION)

test:
	dotnet test $(SOLUTION) --logger "console;verbosity=detailed"

clean:
	dotnet clean $(SOLUTION)
	find . -type d -name "bin" -exec rm -rf {} +
	find . -type d -name "obj" -exec rm -rf {} +

run-api:
	dotnet run --project API\API.csproj

migrate:
	dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project API/API.csproj

migration:
	dotnet ef migrations add $(name) --project Infrastructure/Infrastructure.csproj --startup-project API/API.csproj