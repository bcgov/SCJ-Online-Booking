# Verify repo1 backup files (optional)

Read-only. Checks that every backup file in `repo1` is intact. Nothing changes and nothing goes down.

**Environments:** dev only. Dev keeps 30 full backups and this hasn't been timed here yet, so it may be slow.

> [!WARNING]
> Don't run this on test or prod. They also keep 30 full backups, and the pgBackRest pods have very little CPU: in bcparks, which keeps only 15, it ran on test for more than 7 minutes and was stopped. Verifying repo2 (S3) is even slower, because it downloads every backup.

**You need:** `oc` logged in (in the OpenShift web console, your name in the top right → **Copy login command**).

## 1. Set the variables

Paste the block for your environment.

### dev

```sh
NS=bc7c5c-dev; C=scjob-crunchy; REL=scjob
SEL=postgres-operator.crunchydata.com/cluster=$C
REPOHOST=$(oc -n $NS get pod -l $SEL,postgres-operator.crunchydata.com/data=pgbackrest -o name)
echo "NS=$NS C=$C REPOHOST=$REPOHOST"
```

`REPOHOST` must be set.

## 2. Verify the backups

```sh
oc exec -n $NS $REPOHOST -- pgbackrest verify --stanza=db --repo=1 --output=text --verbose
```

Each backup shows `status: valid` and `checksum invalid: 0`.

This checks that the files are intact. It doesn't prove that a restore to a particular time will work: valid backups and zero checksum errors don't prove that the archived WAL is continuous up to that time.
