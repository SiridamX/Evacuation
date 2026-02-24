# ตัวอย่างเนื้อหา Makefile
PROJECT_PATH=Evacuation.Infrastructure
API_PATH=Evacuation.Api

.PHONY: create-migration

m-add:
	dotnet ef migrations add $(name) --project $(PROJECT_PATH)

m-del:
	dotnet ef migrations remove --project $(PROJECT_PATH)

up:
	dotnet ef database update --project $(PROJECT_PATH)

rmi:
	dotnet ef database update 0 --project $(PROJECT_PATH)

run:
	dotnet run --project $(API_PATH)

start:
	docker compose up db redis -d