# Deploying and Upgrading

This is a quick overview on how to create deployments using the `scjob` Helm chart

## Prerequisite

Install `helm` CLI from https://helm.sh/docs/intro/install/

## Deploying

The `install` command can be used when deploying to a namespace for the very first time.

Run the following commands from the `helm/scjob` directory.

### Dev

`helm -n bc7c5c-dev install scjob . -f values-dev.yaml`

## Upgrading

The `upgrade` command can be used when updating existing deployments in a namespace.

Run the following commands from the `helm/scjob` directory.

### Dev

`helm -n bc7c5c-dev upgrade scjob . -f values-dev.yaml`

## Teardown

The `uninstall` command ca be used to remove all resources defined by the Helm chart. Please note that secrets and PVCs created by the Helm chart are not automatically removed.

Run the following commands from the `helm/scjob` directory.

### Dev

`helm -n bc7c5c-dev uninstall scjob`
