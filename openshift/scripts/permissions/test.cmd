::
:: Before you run this script, you should be logged into the OpenShift console 
:: (login using the oc login command, copied from the OpenShift console)
::
:: NOTE:
:: This script assumes that you have ADMIN access to the tools project and the test project
::

oc project 9xrnfn-test

oc policy add-role-to-user system:image-puller system:serviceaccount:9xrnfn-test:default -n 9xrnfn-tools

oc policy add-role-to-user edit system:serviceaccount:9xrnfn-tools:default -n 9xrnfn-test


@echo off

echo:
echo SUCCESS! The test project has been configured to pull images from the tools project.
echo:

pause
