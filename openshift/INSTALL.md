# Installing on OpenShift

## Part 1: Tools Project

1. Install the [scj-booking-build.yaml](./templates/scj-booking-build.yaml). Use the **default settings**.

```
oc process -f .\scj-booking-build.yaml | oc create -f -
```

2. Get images for the imagestreams

The steps below are written specifically for Windows Powershell. Some changes will be needed for other environments
The username and password for ARTIFACTORY (artifacts.gov.bc.ca) can be found here: https://console.apps.silver.devops.gov.bc.ca/k8s/ns/bc7c5c-tools/secrets/artifactory-creds

Log into the the docker image registries
```
Set-Variable ARTIFACTORY_USER [username]
Set-Variable ARTIFACTORY_PWD [password]

docker login -u unused -p $(oc whoami -t) image-registry.apps.silver.devops.gov.bc.ca
docker login -u $(Get-Variable -ValueOnly ARTIFACTORY_USER) -p $(Get-Variable -ValueOnly ARTIFACTORY_PWD) artifacts.developer.gov.bc.ca
```

pgbouncer image
```
oc project bc7c5c-tools
oc create imagestreamtag pgbouncer:1.22.1 --from-image=bitnami/pgbouncer:1.22.1
oc tag pgbouncer:1.22.1 pgbouncer:latest
```

backup container image
```
docker pull artifacts.developer.gov.bc.ca/bcgov-docker-local/backup-container-postgres
docker tag artifacts.developer.gov.bc.ca/bcgov-docker-local/backup-container-postgres:latest image-registry.apps.silver.devops.gov.bc.ca/bc7c5c-tools/backup-container:latest
docker push image-registry.apps.silver.devops.gov.bc.ca/bc7c5c-tools/backup-container:latest
```

patroni image
```
oc create imagestream patroni-postgres
docker pull artifacts.developer.gov.bc.ca/bcgov-docker-local/patroni-postgres:12.4-latest
docker tag artifacts.developer.gov.bc.ca/bcgov-docker-local/patroni-postgres:12.4-latest image-registry.apps.silver.devops.gov.bc.ca/bc7c5c-tools/patroni-postgres:12.4-latest
docker push image-registry.apps.silver.devops.gov.bc.ca/bc7c5c-tools/patroni-postgres:12.4-latest
oc tag patroni-postgres:12.4-latest patroni-postgres:latest
```


## Part 2: Dev, Test, Prod Projects

1. Run the appropriate [script](./scripts/permissions) to set permissions for each project.

2. Install the [Helm Charts](./helm) for each project.
