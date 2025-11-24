# ==========================
# Makefile for .NET Clean Architecture
# ==========================

SOLUTION = dotNET-CleanArchitecture-Boilerplate.slnx
API_PROJECT = API/API.csproj
INFRASTRUCTURE_PROJECT = Infrastructure/Infrastructure.csproj

.PHONY: help setup restore build build-debug test clean run-api format migrate migration watch

# --------------------------
# Help
# --------------------------
help:
	@echo "Available commands:"
	@echo "  make setup         - First time setup: tools + restore + hooks"
	@echo "  make restore       - Restore NuGet packages"
	@echo "  make build         - Build solution (Release)"
	@echo "  make build-debug   - Build solution (Debug)"
	@echo "  make test          - Run all tests"
	@echo "  make test-watch    - Run tests in watch mode"
	@echo "  make run-api       - Run API project"
	@echo "  make watch         - Run API with hot reload"
	@echo "  make clean         - Clean bin/obj folders"
	@echo "  make format        - Format code with dotnet format"
	@echo "  make format-check  - Check code formatting"
	@echo "  make migration     - Create migration (use: make migration name=MigrationName)"
	@echo "  make migrate       - Apply pending migrations"
	@echo "  make migrate-down  - Rollback last migration"

# --------------------------
# Setup
# --------------------------
setup:
	@echo "Setting up development environment..."
	restore
	dotnet tool restore
	@if [ -d .githooks ]; then \
		echo "📎 Installing git hooks..."; \
		cp .githooks/* .git/hooks/ 2>/dev/null || true; \
		chmod +x .git/hooks/* 2>/dev/null || true; \
	fi
	@echo "✅ Setup complete!"

restore:
	dotnet restore $(SOLUTION)

# --------------------------
# Build
# --------------------------
build:
	dotnet build $(SOLUTION) --configuration Release --no-restore

build-debug:
	dotnet build $(SOLUTION) --configuration Debug --no-restore

# --------------------------
# Test
# --------------------------
test:
	dotnet test $(SOLUTION) --no-build --verbosity normal

test-watch:
	dotnet watch test --project Tests/Application.Tests/Application.Tests.csproj

# --------------------------
# Run
# --------------------------
run-api:
	dotnet run --project $(API_PROJECT)

watch:
	dotnet watch run --project $(API_PROJECT)

# --------------------------
# Clean
# --------------------------
clean:
	@echo "🧹 Cleaning solution..."
	dotnet clean $(SOLUTION)
	@find . -type d -name "bin" -o -name "obj" | xargs rm -rf
	@echo "✅ Clean complete!"

# --------------------------
# Format
# --------------------------
format:
	dotnet format $(SOLUTION)

format-check:
	dotnet format $(SOLUTION) --verify-no-changes

# --------------------------
# Database Migrations
# --------------------------
ifndef name
	@echo "❌ Error: Migration name required. Usage: make migration name=MigrationName"
	@exit 1
endif
	dotnet ef migrations add $(name) \
		--project $(INFRASTRUCTURE_PROJECT) \
		--startup-project $(API_PROJECT)

migrate:
	dotnet ef database update \
		--project $(INFRASTRUCTURE_PROJECT) \
		--startup-project $(API_PROJECT)

migrate-down:
	dotnet ef migrations remove \
		--project $(INFRASTRUCTURE_PROJECT) \
		--startup-project $(API_PROJECT)