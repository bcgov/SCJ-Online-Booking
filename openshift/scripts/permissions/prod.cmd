::
:: Before you run this script, you should be logged into the OpenShift console 
:: (login using the oc login command, copied from the OpenShift console)
::
:: NOTE:
:: This script assumes that you have ADMIN access to the tools project and the prod project
::

oc project bc7c5c-prod

oc policy add-role-to-user system:image-puller system:serviceaccount:bc7c5c-prod:default -n bc7c5c-tools

oc policy add-role-to-user edit system:serviceaccount:bc7c5c-tools:default -n bc7c5c-prod


@echo off

echo:
echo SUCCESS! The prod project has been configured to pull images from the tools project.
echo:

pause
