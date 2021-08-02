#!/bin/bash

# total build time 429.462 seconds

CURRENT_BRANCH=$(git rev-parse --abbrev-ref HEAD)

if [[ "$CURRENT_BRANCH" != "master" ]];
then
    echo "Aborting script because you are not on the master branch.";
    exit 1
fi

echo "on master branch, building deployment artefacts...";

 
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
cat .dockerpass | docker login $REPO_LOCATION -u $REPO_USERNAME --password-stdin

# then we build and push each requested image
if [ $rebuild_client -eq 1 ] 
then

    cd client
    docker buildx build --rm \
        -t activity_client:latest \
        -f Dockerfile \
        .

    docker image tag activity_client:latest $REPO_LOCATION/activity_client:latest

    docker image push $REPO_LOCATION/activity_client:latest

    cd ..
fi

if [ $rebuild_api -eq 1 ] 
then
    cd api

    docker buildx build \
        --rm \
        -t activity_api:latest \
        -f Dockerfile \
        .

    docker image tag activity_api:latest $REPO_LOCATION/activity_api:latest

    docker image push $REPO_LOCATION/activity_api:latest

    cd ..

fi

if [ $rebuild_db -eq 1 ] 
then
    cd db

    docker buildx build \
        --rm \
        -t activity_db:latest \
        -f Dockerfile \
        .

    docker image tag activity_db:latest $REPO_LOCATION/activity_db:latest

    docker image push $REPO_LOCATION/activity_db:latest

    cd ..
fi


if [ $rebuild_proxy -eq 1 ] 
then
    cd proxy

    docker buildx build \
        --rm \
        -t activity_proxy:latest \
        -f Dockerfile \
        .

    docker image tag activity_proxy:latest $REPO_LOCATION/activity_proxy:latest

    docker image push $REPO_LOCATION/activity_proxy:latest

    cd ..
fi

docker logout $REPO_LOCATION

echo "total build time $(echo "scale=3;($(date +%s%N) - ${START})/(1*10^09)" | bc) seconds"