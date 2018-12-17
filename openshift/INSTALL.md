# Installing on OpenShift

## Part 1: Tools Project

1. Install the "BC Gov Pathfinder Jenkins (Persistent)" template into the "tools" project with **default settings** (find it under "Browse Catalog" in the OpenShift console).

2. Install the [scj-booking-build.json](./templates/scj-booking-build.json) template into the tools project using the "Import YAML/JSON" feature.  Use the **default settings**.


## Part 2: Dev, Test, Prod Projects

1. Run the appropriate [script](./scripts/permissions) to set permissions for each project.

2. Install the [scj-booking-deploy.json](./templates/scj-booking-deploy.json) template into each project using the "Import YAML/JSON" feature.  Use the **default settings** for **everything except 'Environment TAG name'**, where you need to specify `test` or `prod` for Test and Prod.
