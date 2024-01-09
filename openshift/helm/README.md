# Deploying and Upgrading

This is a quick overview on how to create deployments using the `scjob` Helm chart

## Prerequisite

Install `helm` CLI from https://helm.sh/docs/intro/install/

Log into OpenShift on your coonsole **with an admin account** (an admin account is needed to manage roles and role binding)

Grant the patroni service accounts permission to pull images from the tools project
```
oc policy add-role-to-user system:image-puller system:serviceaccount:bc7c5c-dev:scjob-patroni -n bc7c5c-tools
oc policy add-role-to-user system:image-puller system:serviceaccount:bc7c5c-test:scjob-patroni -n bc7c5c-tools
oc policy add-role-to-user system:image-puller system:serviceaccount:bc7c5c-prod:scjob-patroni -n bc7c5c-tools
```

## Deploying

The `install` command can be used when deploying to a namespace for the very first time.

Run the following commands from the `helm/scjob` directory.

### Dev

`helm -n bc7c5c-dev install scjob . -f values-dev.yaml`

### Test

`helm -n bc7c5c-test install scjob . -f values-test.yaml`

### Prod

`helm -n bc7c5c-prod install scjob . -f values-prod.yaml`

### After deploying

You will need to edit the secrets and the routes on OpenShift. The route whitelist annotations should match the eDivorce application for Dev/Test/Prod respectively. Both apps use justice.gov.bc.ca as a reverse proxy.

## Upgrading

The `upgrade` command can be used when updating existing deployments in a namespace.

Run the following commands from the `helm/scjob` directory.

### Dev

`helm -n bc7c5c-dev upgrade scjob . -f values-dev.yaml`

### Test

`helm -n bc7c5c-test upgrade scjob . -f values-test.yaml`

### Prod

`helm -n bc7c5c-prod upgrade scjob . -f values-prod.yaml`


## Teardown

The `uninstall` command ca be used to remove all resources defined by the Helm chart. Please note that secrets and PVCs created by the Helm chart are not automatically removed.

Run the following commands from the `helm/scjob` directory.

### Dev

`helm -n bc7c5c-dev uninstall scjob`

### Test

`helm -n bc7c5c-test uninstall scjob`

### Prod

`helm -n bc7c5c-prod uninstall scjob`
