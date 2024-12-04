@echo off
docker compose -f ../Docker/docker-compose.postgres.yaml down --volumes
docker compose -f ../Docker/docker-compose.postgres.yaml up -d
echo(
pause