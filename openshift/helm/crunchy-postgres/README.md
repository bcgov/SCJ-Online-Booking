# Crunchy Postgres Installation

## Prerequisites

- Install `helm` CLI from https://helm.sh/docs/intro/install/

## Installing

### Dev

```sh
helm -n bc7c5c-dev install crunchy . -f values-dev.yaml
```

### Test

```sh
helm -n bc7c5c-test install crunchy . -f values-test.yaml
```

### Prod

```sh
helm -n bc7c5c-prod install crunchy . -f values-prod.yaml
```

### Create a network policy

The crunchy pods need a network policy that allows them to talk to each other. The policy below works, but something more secure might be possible.

```sh
apiVersion: networking.k8s.io/v1
kind: NetworkPolicy
metadata:
  name: allow-same-namespace
spec:
  podSelector: {}
  ingress:
    - from:
        - podSelector: {}
  policyTypes:
    - Ingress
```

## Upgrading

### Dev

```sh
helm -n bc7c5c-dev upgrade crunchy . -f values-dev.yaml
```

### Test

```sh
helm -n bc7c5c-test upgrade crunchy . -f values-test.yaml
```

### Prod

```sh
helm -n bc7c5c-prod upgrade crunchy . -f values-prod.yaml
```

## Teardown

Don't run `uninstall crunchy` unless you really want to lose all your data!

### Dev

```sh
helm -n bc7c5c-dev uninstall crunchy
```

### Test

```sh
helm -n bc7c5c-test uninstall crunchy
```

### Prod

intentionally omitted

## S3 backup secret

Test and prod also back up to S3 object storage (`pgBackRest.s3.enabled`). Dev has a bucket too, but its S3 backups are currently disabled. The S3 credentials are in the `s3-pgbackrest` secret, which the Crunchy operator reads. `createS3Secret` is `false` in the values files, so Helm never creates, overwrites or deletes this secret.

The bucket and endpoint are in the values files. The keys are **only** in the `s3-pgbackrest` secret in each namespace, so keep a safe copy outside OpenShift: if the namespace is lost, the secret goes with it, and you need the keys to [rebuild from S3](../../../docs/crunchy-postgres-backup-and-restore/rebuild-from-s3.md).

To see the current keys:

```sh
oc -n bc7c5c-test get secret s3-pgbackrest -o jsonpath='{.data.s3\.conf}' | base64 -d
```

To create or update the secret, edit the two key lines in the block for your environment, then run it.

### Dev

```sh
(
  KEY_ID='<access key id>'
  KEY_SECRET='<secret access key>'
  if [ -z "$KEY_ID" ] || [ -z "$KEY_SECRET" ] || [ "$KEY_ID" = '<access key id>' ]; then
    echo "Set KEY_ID and KEY_SECRET first; s3-pgbackrest not changed" >&2
    exit 1
  fi

  oc -n bc7c5c-dev create secret generic s3-pgbackrest \
    --from-file=s3.conf=<(printf '[global]\nrepo2-s3-key=%s\nrepo2-s3-key-secret=%s\n' \
      "$KEY_ID" "$KEY_SECRET") \
    --dry-run=client -o yaml | oc apply --server-side --force-conflicts -f -
)
```

### Test

```sh
(
  KEY_ID='<access key id>'
  KEY_SECRET='<secret access key>'
  if [ -z "$KEY_ID" ] || [ -z "$KEY_SECRET" ] || [ "$KEY_ID" = '<access key id>' ]; then
    echo "Set KEY_ID and KEY_SECRET first; s3-pgbackrest not changed" >&2
    exit 1
  fi

  oc -n bc7c5c-test create secret generic s3-pgbackrest \
    --from-file=s3.conf=<(printf '[global]\nrepo2-s3-key=%s\nrepo2-s3-key-secret=%s\n' \
      "$KEY_ID" "$KEY_SECRET") \
    --dry-run=client -o yaml | oc apply --server-side --force-conflicts -f -
)
```

### Prod

```sh
(
  KEY_ID='<access key id>'
  KEY_SECRET='<secret access key>'
  if [ -z "$KEY_ID" ] || [ -z "$KEY_SECRET" ] || [ "$KEY_ID" = '<access key id>' ]; then
    echo "Set KEY_ID and KEY_SECRET first; s3-pgbackrest not changed" >&2
    exit 1
  fi

  oc -n bc7c5c-prod create secret generic s3-pgbackrest \
    --from-file=s3.conf=<(printf '[global]\nrepo2-s3-key=%s\nrepo2-s3-key-secret=%s\n' \
      "$KEY_ID" "$KEY_SECRET") \
    --dry-run=client -o yaml | oc apply --server-side --force-conflicts -f -
)
```

## Backup and restore

Runbooks for checking backups, manual backups, point-in-time restore and rebuilding from S3 are in [docs/crunchy-postgres-backup-and-restore](../../../docs/crunchy-postgres-backup-and-restore/README.md).

---

# Crunchy Postgres chart

A chart to provision a [Crunchy Postgres](https://www.crunchydata.com/) cluster.

## Configuration

### Crunchy Options

| Parameter          | Description            | Default            |
| ------------------ | ---------------------- | ------------------ |
| `fullnameOverride` | Override release name  | `crunchy-postgres` |
| `crunchyImage`     | Crunchy Postgres image |                    |
| `postgresVersion`  | Postgres version       | `14`               |

---

### Instances

| Parameter                                   | Description                    | Default                  |
| ------------------------------------------- | ------------------------------ | ------------------------ |
| `instances.name`                            | Instance name                  | `ha` (high availability) |
| `instances.replicas`                        | Number of replicas             | `2`                      |
| `instances.dataVolumeClaimSpec.storage`     | Amount of storage for each PVC | `480Mi`                  |
| `instances.requests.cpu`                    | CPU requests                   | `1m`                     |
| `instances.requests.memory`                 | Memory requests                | `256Mi`                  |
| `instances.limits.cpu`                      | CPU limits                     | `100m`                   |
| `instances.limits.memory`                   | Memory limits                  | `512Mi`                  |
| `instances.replicaCertCopy.requests.cpu`    | replicaCertCopy CPU requests   | `1m`                     |
| `instances.replicaCertCopy.requests.memory` | replicaCertCopyMemory requests | `32Mi`                   |
| `instances.replicaCertCopy.limits.cpu`      | replicaCertCopyCPU limits      | `50m`                    |
| `instances.replicaCertCopy.limits.memory`   | replicaCertCopy Memory limits  | `64Mi`                   |

---

### pgBackRest - Reliable PostgreSQL Backup & Restore

[pgBackRest site](https://pgbackrest.org/)
[Crunchy pgBackRest docs](https://access.crunchydata.com/documentation/pgbackrest/latest/)

| Parameter                                            | Description                                                                                                                                  | Default                |
| ---------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------- |
| `pgBackRest.image`                                   | Crunchy pgBackRest                                                                                                                           |                        |
| `pgBackRest.retention`                               | Number of backups/days to keep depending on retentionFullType                                                                                | `2`                    |
| `pgBackRest.retentionFullType`                       | Either 'count' or 'time'                                                                                                                     | `count`                |
| `pgBackRest.repos.schedules.full`                    | Full backup schedule                                                                                                                         | `0 8 * * *`            |
| `pgBackRest.repos.schedules.incremental`             | Incremental backup schedule                                                                                                                  | `0 0,4,12,16,20 * * *` |
| `pgBackRest.repos.schedules.volume.accessModes`      | Access modes                                                                                                                                 | `ReadWriteOnce`        |
| `pgBackRest.repos.schedules.volume.storage`          | Access modes                                                                                                                                 | `64Mi`                 |
| `pgBackRest.repos.schedules.volume.storageClassName` | Storage class name modes                                                                                                                     | `netapp-file-backup`   |
| `pgBackRest.repoHost.requests.cpu`                   | CPU requests                                                                                                                                 | `1m`                   |
| `pgBackRest.repoHost.requests.memory`                | Memory requests                                                                                                                              | `64Mi`                 |
| `pgBackRest.repoHost.limits.cpu`                     | CPU limits                                                                                                                                   | `50m`                  |
| `pgBackRest.repoHost.limits.memory`                  | Memory limits                                                                                                                                | `128Mi`                |
| `pgBackRest.sidecars.requests.cpu`                   | sidecars CPU requests                                                                                                                        | `1m`                   |
| `pgBackRest.sidecars.requests.memory`                | sidecars Memory requests                                                                                                                     | `64Mi`                 |
| `pgBackRest.sidecars.limits.cpu`                     | sidecars CPU limits                                                                                                                          | `50m`                  |
| `pgBackRest.sidecars.limits.memory`                  | sidecars Memory limits                                                                                                                       | `128Mi`                |
| `pgBackRest.s3.enabled`                              | Enables the s3 repo backups                                                                                                                  | `false`                |
| `pgBackRest.s3.createS3Secret`                       | Creates the s3 secret based on key and keySecret (`false` in dev, test and prod; see [S3 backup secret](#s3-backup-secret))                  | `true`                 |
| `pgBackRest.s3.s3Secret`                             | The secret name to be created or read from                                                                                                   | `s3-pgbackrest`        |
| `pgBackRest.s3.s3Path`                               | The path inside the bucket where the backups will be saved to, set it to `/` to use the root of the bucket.                                  | `/dbbackup`            |
| `pgBackRest.s3.s3UriStyle`                           | Style of URL to use for S3 communication. [More Info](https://pgbackrest.org/configuration.html#section-repository/option-repo-s3-uri-style) | `path`                 |
| `pgBackRest.s3.bucket`                               | The bucket to use for backups                                                                                                                | `bucketName`           |
| `pgBackRest.s3.endpoint`                             | The endpoint to use, for example s3.ca-central-1.amazonaws.com                                                                               | `endpointName`         |
| `pgBackRest.s3.region`                               | The region to use, not necessary if your S3 system does not specify one                                                                      | `ca-central-1`         |
| `pgBackRest.s3.key`                                  | The key to use to access the bucket. MUST BE KEPT SECRET                                                                                     | `s3KeyValue`           |
| `pgBackRest.s3.keySecret`                            | The key secret for the key set above. MUST BE KEPT SECRET                                                                                    | `s3SecretValue`        |

---

### Patroni

[Patroni docs](https://patroni.readthedocs.io/en/latest/)
[Crunchy Patroni docs](https://access.crunchydata.com/documentation/patroni/latest/)

| Parameter                                   | Description                                                         | Default                           |
| ------------------------------------------- | ------------------------------------------------------------------- | --------------------------------- |
| `patroni.postgresql.pg_hba`                 | pg_hba permissions                                                  | `"host all all 0.0.0.0/0 md5"`    |
| `crunchyImage`                              | Crunchy Postgres image                                              | `...crunchy-postgres:ubi8-14.7-0` |
| `patroni.parameters.shared_buffers`         | The number of shared memory buffers used by the server              | `16MB`                            |
| `patroni.parameters.wal_buffers`            | The number of disk-page buffers in shared memory for WAL            | `64KB`                            |
| `patroni.parameters.min_wal_size`           | The minimum size to shrink the WAL to                               | `32MB`                            |
| `patroni.parameters.max_wal_size`           | Sets the WAL size that triggers a checkpoint                        | `64MB`                            |
| `patroni.parameters.max_slot_wal_keep_size` | Sets the maximum WAL size that can be reserved by replication slots | `128MB`                           |

---

### pgBouncer

A lightweight connection pooler for PostgreSQL

[pgBouncer site](https://www.pgbouncer.org/)
[Crunchy Postgres pgBouncer docs](https://access.crunchydata.com/documentation/pgbouncer/latest/)

| Parameter                         | Description             | Default |
| --------------------------------- | ----------------------- | ------- |
| `proxy.pgBouncer.image`           | Crunchy pgBouncer image |         |
| `proxy.pgBouncer.replicas`        | Number of replicas      | `2`     |
| `proxy.pgBouncer.requests.cpu`    | CPU requests            | `1m`    |
| `proxy.pgBouncer.requests.memory` | Memory requests         | `64Mi`  |
| `proxy.pgBouncer.limits.cpu`      | CPU limits              | `50m`   |
| `proxy.pgBouncer.limits.memory`   | Memory limits           | `128Mi` |

---

## PG Monitor

[Crunchy Postgres PG Monitor docs](https://access.crunchydata.com/documentation/pgmonitor/latest/)

| Parameter                            | Description                                    | Default |
| ------------------------------------ | ---------------------------------------------- | ------- |
| `pgmonitor.enabled`                  | Enable PG Monitor (currently only PG exporter) | `false` |
| `pgmonitor.exporter.requests.cpu`    | PG Monitor CPU requests                        | `1m`    |
| `pgmonitor.exporter.requests.memory` | PG Monitor Memory requests                     | `64Mi`  |
| `pgmonitor.exporter.limits.cpu`      | PG Monitor CPU limits                          | `50m`   |
| `pgmonitor.exporter.limits.memory`   | PG Monitor Memory limits                       | `128Mi` |

#### Postgres Exporter

A [Prometheus](https://prometheus.io/) exporter for PostgreSQL

[Postgres Exporter](https://github.com/prometheus-community/postgres_exporter)

| Parameter                            | Description               | Default |
| ------------------------------------ | ------------------------- | ------- |
| `pgmonitor.exporter.image`           | Crunchy PG Exporter image |         |
| `pgmonitor.exporter.requests.cpu`    | CPU requests              | `1m`    |
| `pgmonitor.exporter.requests.memory` | Memory requests           | `64Mi`  |
| `pgmonitor.exporter.limits.cpu`      | CPU limits                | `50m`   |
| `pgmonitor.exporter.limits.memory`   | Memory limits             | `128Mi` |

---
