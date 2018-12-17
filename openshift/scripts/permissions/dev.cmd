::
:: Before you run this script, you should be logged into the OpenShift console 
:: (login using the oc login command, copied from the OpenShift console)
::
:: NOTE:
:: This script assumes that you have ADMIN access to the tools project and the dev project
::

oc project 9xrnfn-dev

oc policy add-role-to-user system:image-puller system:serviceaccount:9xrnfn-dev:default -n 9xrnfn-tools

oc policy add-role-to-user edit system:serviceaccount:9xrnfn-tools:default -n 9xrnfn-dev


@echo off

echo:
echo SUCCESS! The dev project has been configured to pull images from the tools project.
echo:

pause
