# Check repo2 (S3) backups

Read-only. Checks that the backups on `repo2` (the S3 bucket on the BC Gov object store) are recent and healthy. Nothing changes and nothing goes down.

**Environments:** dev, test and prod. In dev, only while its S3 backups are enabled (`pgBackRest.s3.enabled` in `values-dev.yaml`; currently off).

**You need:** `oc` logged in (in the OpenShift web console, your name in the top right → **Copy login command**) and `jq`.

## 1. Set the variables

Paste the block for your environment.

### dev

```sh
NS=bc7c5c-dev; C=scjob-crunchy; REL=scjob
SEL=postgres-operator.crunchydata.com/cluster=$C
PRIMARY=$(oc -n $NS get pod -l $SEL,postgres-operator.crunchydata.com/role=master -o name)
REPOHOST=$(oc -n $NS get pod -l $SEL,postgres-operator.crunchydata.com/data=pgbackrest -o name)
echo "NS=$NS C=$C PRIMARY=$PRIMARY REPOHOST=$REPOHOST"
```

### test

```sh
NS=bc7c5c-test; C=scjob-crunchy; REL=scjob
SEL=postgres-operator.crunchydata.com/cluster=$C
PRIMARY=$(oc -n $NS get pod -l $SEL,postgres-operator.crunchydata.com/role=master -o name)
REPOHOST=$(oc -n $NS get pod -l $SEL,postgres-operator.crunchydata.com/data=pgbackrest -o name)
echo "NS=$NS C=$C PRIMARY=$PRIMARY REPOHOST=$REPOHOST"
```

### prod

```sh
NS=bc7c5c-prod; C=scjob-crunchy; REL=scjob
SEL=postgres-operator.crunchydata.com/cluster=$C
PRIMARY=$(oc -n $NS get pod -l $SEL,postgres-operator.crunchydata.com/role=master -o name)
REPOHOST=$(oc -n $NS get pod -l $SEL,postgres-operator.crunchydata.com/data=pgbackrest -o name)
echo "NS=$NS C=$C PRIMARY=$PRIMARY REPOHOST=$REPOHOST"
```

`PRIMARY` and `REPOHOST` must both be set.

## 2. Check that recent scheduled backups succeeded

```sh
oc -n $NS get postgrescluster $C -o jsonpath='{range .status.pgbackrest.scheduledBackups[*]}{.type}{"\t"}{.repo}{"\t"}{.completionTime}{"\t"}succeeded={.succeeded}{"\n"}{end}'
```

Every `repo2` line shows `succeeded=1`. The `repo1` lines are covered by [Check repo1 backups](check-backups-repo1.md).

## 3. List the backups in repo2

```sh
oc exec -n $NS $REPOHOST -- pgbackrest info --repo=2
```

Status is `ok`, and there's a full backup from the last day.

The earliest time you can restore to is the stop time of the oldest backup (the second time in its `timestamp start/stop` line). The `wal archive` min and max are WAL file names, not times.

The `repo2: backup set size` lines show how much space each backup uses. repo2 is an S3 bucket, so there's no disk to run `df` against.

## 4. Check that no `restore` or `manual` section is left over

```sh
oc -n $NS get postgrescluster $C -o jsonpath='{.spec.backups.pgbackrest}' | jq 'del(.repos, .global, .repoHost, .sidecars, .configuration)'
```

This should print `{}`.

If it prints anything else, follow [Clear leftover config](clear-leftover-config.md), then run this check again.
