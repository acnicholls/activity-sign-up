#####  global params
REPO_LOCATION=https://acr.acnicholls.com
REPO_USERNAME=activity-acr

# login to the container registry
cat .dockerpass | docker login $REPO_LOCATION -u $REPO_USERNAME --password-stdin