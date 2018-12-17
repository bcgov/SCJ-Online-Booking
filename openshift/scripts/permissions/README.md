# Permissions Scripts

Run these 3 scripts in a new OpenShift environment to set up the permissions needed for `tools` project to interact with the `dev`, `test` and `prod` projects. 


### PREREQUISITES:

In order to run these scripts

* You need to have the `oc` binary installed
* You need to be logged into OpenShift (using the `oc login` command)
* You must have admin `access` to both the `tools` project and the corresponding deployment environment for each script (`dev`, `test` or `prod`) 
* These are cmd files, so they only run on Windows.  If you are using Linux or Mac feel free to convert them to your favorite shell scripting language.
* Make sure you run these from the Command Prompt or PowerShell, not from Windows Explorer (you need to use a command line so you can `oc login` first)


### WHAT DOES THIS DO?:

After applying these 3 scripts:

* The `dev`, `test` and `prod` projects can pull tagged images from the ImageStreams in the `tools` project
* The `tools` project can control pipeline builds in the `dev`, `test` and `prod` projects