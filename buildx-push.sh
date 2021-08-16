#!/bin/bash

# this script will build arm64 images for linux os. 

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
read -p "Do you want to build the proxy image? " rebuild_proxy

START=$(date +%s%N)

echo building arm64 release.

#####  global params
REPO_LOCATION=acr.acnicholls.com
REPO_USERNAME=activity-acr
BUILDX_NAME=antsle

IMAGE_TAG=arm64-latest

# create a new buildx builder
docker buildx create --use --driver docker-container --name $BUILDX_NAME --platform linux/amd64,linux/arm64 
# the above isn't working for some reason.  The build fails, so each build statement specifies only one os type.  


# login to the container registry
cat .dockerpass | docker login $REPO_LOCATION -u $REPO_USERNAME --password-stdin

# then we build and push each requested image
if [ $rebuild_client -eq 1 ] 
then
    cd client
    echo building client...
    docker buildx build --rm --load --platform linux/arm64 \
        -t activity_client:$IMAGE_TAG \
        -f Dockerfile.arm64 \
        .

    docker image tag activity_client:$IMAGE_TAG $REPO_LOCATION/activity_client:$IMAGE_TAG

    docker image push $REPO_LOCATION/activity_client:$IMAGE_TAG        

    cd ..
fi

if [ $rebuild_api -eq 1 ] 
then
    cd api
    echo building api...
    docker buildx build --rm --load --platform linux/arm64 \
        -t activity_api:$IMAGE_TAG \
        -f Dockerfile.arm64 \
        .

    docker image tag activity_api:$IMAGE_TAG $REPO_LOCATION/activity_api:$IMAGE_TAG

    docker image push $REPO_LOCATION/activity_api:$IMAGE_TAG        

    cd ..
fi

if [ $rebuild_proxy -eq 1 ] 
then
    cd proxy
    echo building proxy...
    docker buildx build --rm --load --platform linux/arm64 \
        -t activity_proxy:$IMAGE_TAG \
        -f Dockerfile.arm64 \
        .

    docker image tag activity_proxy:$IMAGE_TAG $REPO_LOCATION/activity_proxy:$IMAGE_TAG

    docker image push $REPO_LOCATION/activity_proxy:$IMAGE_TAG        

    cd ..
fi

docker logout $REPO_LOCATION

docker buildx rm $BUILDX_NAME

echo "total build time $(echo "scale=3;($(date +%s%N) - ${START})/(1*10^09)" | bc) seconds"