# Migrating from Patroni to CrunchyDB

1. Install the `crunchy-postgres` helm charts for the environment you are setting up. Wait about 10 minutes.

1. Back up the existing database

   Run this from the terminal on the current patroni leader (e.g. scjob-patroni-X)

   ```
   pg_dump --no-owner --no-privileges "scj_booking" > /tmp/scjob-backup.sql
   ```

1. Copy the backup to your local machine
   - Log into OpenShift with `oc login` first
   - Change `bc7c5c-dev` to the current environment being migrated

   ```
   cd ~
   oc project bc7c5c-dev
   oc cp scjob-patroni-0:/tmp/scjob-backup.sql ./scjob-backup.sql
   ```

1. Create the `scj_booking` database on the new crunchy leader

   ```
   psql -U postgres

   CREATE database "scj_booking" OWNER "scjob-crunchy";
   ```

1. Copy the backup onto the crunchy primary with `oc cp`

   Run this in the leader pod

   ```
   mkdir -p /pgdata/tmp_backup
   chmod 700 /pgdata/tmp_backup
   ```

   Run this from your mac terminal

   ```
   oc cp ./scjob-backup.sql scjob-crunchy-postgres-spz8-0:/pgdata/tmp_backup/scjob-backup.sql
   ```

1. Restore the DB

   You will need the password for the app user!
   password secret is in `scjob-crunchy-pguser-scjob-crunchy`

   ```
   \q

   psql -h scjob-crunchy-primary -U scjob-crunchy -d scj_booking < /pgdata/tmp_backup/scjob-backup.sql
   ```

   **You can delete the backup when you're done.**

1. Run the `upgrade` command in the `deployment` helm chart for the environment being migrated
