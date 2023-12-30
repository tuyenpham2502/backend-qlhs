#!/usr/bin/env bash

while getopts "e:v:h" opt
do
  # shellcheck disable=SC2220
  case "$opt" in
    e) env="$OPTARG" ;;
    v) version="$OPTARG" ;;
  esac
done


# Export to system env for docker compose 
export version=$env-v$version
case "$env" in
  development)
    export env=dev
    export COMPOSE_PROJECT_NAME="qlhs-dev-server-services" 
    ;;
#    uat)
#       export env=uat
#       export COMPOSE_PROJECT_NAME="qs-prod-biz-services"
#       export command='yarn start:uat'
#       source ~/.zshrc
#       ;;
  production)
    export env=prod
    export COMPOSE_PROJECT_NAME="qlhs-prod-server-services"
    source ~/.zshrc
    ;;
#   uat)
#     export env=UAT
#     export COMPOSE_PROJECT_NAME="qs-uat-biz-services"
#     export command='yarn start'
#     source ~/.zshrc
#     ;;
esac

echo "Building version [$version] service on {$env} environment"
docker-compose -f docker-compose.$env.yml down
docker-compose -f docker-compose.$env.yml config
docker-compose -f docker-compose.$env.yml build --no-cache --progress=plain
docker-compose -f docker-compose.$env.yml up -d