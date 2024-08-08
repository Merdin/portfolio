#!/bin/bash
echo "Deploying $1-environment!"
if [ $1 == "test" ]; 
then
  docker-compose -f ".ci/docker-compose.base.yml" -f ".ci/docker-compose.$1.yml" -p "studym8-$1" up -d --build --remove-orphans
elif [ $1 == "dev" ];
then
  docker-compose -f ".ci/docker-compose.base.yml" -f ".ci/docker-compose.$1.yml" -p "studym8-$1" up -d --remove-orphans
elif [ $1 == "prod" ];
then
  docker-compose -f ".ci/docker-compose.base.yml" -f ".ci/docker-compose.$1.yml" -p "studym8-$1" up -d --build --remove-orphans
fi
