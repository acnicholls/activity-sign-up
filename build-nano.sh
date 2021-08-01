#!/bin/bash

eval $(docker-machine env dockernano)

docker builder prune -af

read -p "Have you incremented your version number? (yes/no)" EXECUTE

if [ $EXECUTE != "yes" ] 
then
    exit 1
fi

echo "Answer 1 for yes and 0 for no to the follow questions to refine your build."

read -p "Do you want to build the client image? " rebuild_client
read -p "Do you want to build the api image? " rebuild_api
read -p "Do you want to build the db image? " rebuild_db
read -p "Do you want to build the proxy image? " rebuild_proxy

START=$(date +%s%N)

#####  global params
REPO_LOCATION=acr.acnicholls.com
REPO_USERNAME=activity-acr

# login to the container registry
cat ./.dockerpass | docker login $REPO_LOCATION -u $REPO_USERNAME --password-stdin

# then we build and push each requested image
if [ $rebuild_client -eq 1 ] 
then

    cd client

    docker build \
        --rm \
        -t activity_client:nano \
        -f Dockerfile.nano \
        .

    docker image tag activity_client:nano $REPO_LOCATION/activity_client:nano

    docker image push $REPO_LOCATION/activity_client:nano

    cd ..
fi

if [ $rebuild_api -eq 1 ] 
then

    cd api

    docker build \
        --rm \
        -t activity_api:nano \
        -f Dockerfile.nano \
        .

    docker image tag activity_api:nano $REPO_LOCATION/activity_api:nano

    docker image push $REPO_LOCATION/activity_api:nano

    cd ..

fi

if [ $rebuild_db -eq 1 ] 
then

    cd db

    docker build \
        --rm \
        -t activity_db:nano \
        -f Dockerfile.nano \
        .

    docker image tag activity_db:nano $REPO_LOCATION/activity_db:nano

    docker image push $REPO_LOCATION/activity_db:nano

    cd ..

fi

if [ $rebuild_proxy -eq 1 ] 
then

    cd proxy

    docker build \
        --rm \
        -t activity_proxy:nano \
        -f Dockerfile.nano \
        .

    docker image tag activity_proxy:nano $REPO_LOCATION/activity_proxy:nano

    docker image push $REPO_LOCATION/activity_proxy:nano

    cd ..

fi
END_BUILD=$(date +%s+%N)

echo "total build time: $(echo "scale=3;($(date +%s%N) - ${START})/(1*10^09)" | bc) seconds"

# docker-compose -f docker-compose.nano.yml up

docker logout $REPO_LOCATION

# echo "total start time $(echo "scale=3;($(date +%s%N) - ${END_BUILD})/(1*10^09)" | bc) seconds"