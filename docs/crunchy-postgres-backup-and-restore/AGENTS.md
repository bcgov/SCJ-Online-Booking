# Crunchy Postgres backup and restore runbooks

Developers use these runbooks, often under pressure during an incident. They copy commands from top to bottom without reading much. Keep every change in that spirit.

## Keep in sync with the bcparks runbooks

These runbooks are adapted from the copies in two other repos, at `docs/crunchy-postgres-backup-and-restore/` in each:

- bcparks-staff-portal (the closest match, because it also uses `REL`)
- bcparks.ca

The checkouts are usually side by side, for example `~/Work/SCJ-Online-Booking`, `~/Work/bcparks-staff-portal` and `~/Work/bcparks.ca`.

When a procedure changes in those repos, port the change here, and port procedural fixes made here back to them, unless the change only concerns one app's values.

The only intended differences from bcparks-staff-portal:

|                                    | bcparks-staff-portal                                       | SCJ-Online-Booking                                                                   |
| ---------------------------------- | ---------------------------------------------------------- | ------------------------------------------------------------------------------------ |
| Namespaces                         | `a7dd13-*`                                                 | `bc7c5c-*`                                                                           |
| Environments                       | alpha-dev, alpha-test, dev, test, training, prod           | dev, test, prod; repo2 runbooks include dev, "only while its S3 backups are enabled" |
| Cluster                            | `crunchy-postgres` (and `-alpha`, `-training`)             | `scjob-crunchy`                                                                      |
| App variable in step 1             | `REL=main` (Helm release of the app)                       | `REL=scjob`                                                                          |
| Instance pods                      | `<cluster>-ha-*`                                           | `<cluster>-postgres-*`                                                               |
| App database                       | same name as the cluster                                   | `scj_booking`                                                                        |
| S3 object store                    | NRS, `nrs.objectstore.gov.bc.ca`                           | BC Gov, `ag-pssg-sharedservices.objectstore.gov.bc.ca`                               |
| S3 buckets                         | `doot$ENV`                                                 | `ag-pssg-bccourts-scjob-$ENV-bkt`                                                    |
| S3 keys                            | Vault                                                      | only in the live `s3-pgbackrest` secret (no Vault)                                   |
| Full backups kept                  | 2 (non-prod), 15 (test, prod)                              | 30 everywhere                                                                        |
| Schedules                          | on the hour                                                | 17 minutes past the hour                                                             |
| OpenShift cluster                  | Gold                                                       | Silver                                                                               |
| App restart                        | `deploy/$REL-backend`                                      | `deploy/$REL-app deploy/$REL-taskrunner`                                             |
| Damaged-data check after a restore | Staff Portal, tables in `backend/migrations`               | booking app, `scj_booking`, EF migrations in `database/Migrations`                   |
| README "Connections" bullet        | backend straight to the primary                            | app and taskrunner through pgBouncer                                                 |
| Chart path                         | `helm/crunchy-postgres`                                    | `openshift/helm/crunchy-postgres`                                                    |
| Helm prerequisite                  | Helm 4 + helm-secrets, `../../helm/README.md#prerequisite` | Helm only, `../../openshift/helm/README.md#prerequisite`                             |
| Rebuild from S3                    | uninstall step notes the alpha-test and training releases  | no such note                                                                         |
| `verify-backup-files-repo1.md`     | alpha-dev, alpha-test, dev, training                       | dev only, not timed                                                                  |
| Prettier                           | `frontend/node_modules/.bin/prettier`                      | `npx prettier`                                                                       |

To check, run `diff -r ~/Work/bcparks-staff-portal/docs/crunchy-postgres-backup-and-restore docs/crunchy-postgres-backup-and-restore`. Every difference should be in this table.

## Layout

- `README.md` is the index: the "which runbook do I need?" table, How it works (concepts: repos, backups, WAL, point-in-time restore), environments and references.
- Each task has its own runbook, with separate files for repo1 and repo2. repo2 (S3) runs in test and prod. Dev has a bucket too, but its S3 backups are currently disabled, so the repo2 runbooks list dev with that caveat.
- `clear-leftover-config.md` is never started on its own. Other runbooks link to it from every leftover check (`jq 'del(.repos, .global, .repoHost, .sidecars, .configuration)'`, which should print `{}`). If you add a runbook with that check, add the link.
- The restore and rebuild runbooks handle rehearsals with steps marked **Rehearsal only**, not with separate files.

## Rules for runbooks

- **Self-contained.** Never make a runbook depend on finishing steps in another file. Write shared steps out in full in every runbook that needs them, such as setting variables, finding `PRIMARY` and `REPOHOST` again, manual backups and backup checks. The only allowed links to other runbooks are:
  - exception branches: a leftover check that fails, or a leftover `dataSource` (→ step 9 of `rebuild-from-s3.md`)
  - background reading in `README.md`
  - the rebuild rehearsal's lifecycle check
- **Step numbers are referenced elsewhere.** If you renumber, update the references. For example, `pitr-repo*.md` and `clear-leftover-config.md` point to step 9 of `rebuild-from-s3.md`, and `rebuild-from-s3.md` points to step 19 of `pitr-repo2.md`.
- **Step 1 sets the variables.** It has one paste-able block per applicable environment, and each block sets `NS`, `C`, the app variable, `SEL` (plus `PRIMARY` and `REPOHOST` when the runbook needs them) and echoes them. Only list environments the runbook applies to.
- **No prompts or choices in commands.** The repo is written into the commands (`--repo=1`, `"repoName":"repo1"`), with no `REPO` variable. T is set by one editable line (`T='YYYY-MM-DD HH:MM:SS+00'`), followed by a regex check that works in both bash and zsh. Don't add shell-specific blocks such as zsh `vared`, bash `read -e`, or macOS or GNU `date`.
- **Use `$NS` everywhere**, not `<namespace-prefix>-$ENV`. The rebuild runbook also sets `ENV`, for the Helm values file and the bucket name.
- **Each step:** one command block, then one line saying what you should see. Keep warnings (point of no return, don't restart a restore, never uninstall in prod, remove `dataSource` before `restore`) next to the step they apply to.
- **Keep cautions short.** Put explanations in How it works in `README.md`, not in the runbooks.

## Wording

- Readers are developers, not DBAs. Don't assume they know Postgres or pgBackRest terms. Explain concepts once, in `README.md`.
- The first time a file uses "repo" or "repos" on its own, not as `repo1` or `repo2`, write "repo (backup device)" or "repos (backup devices)". Use the plain word after that. "Repo host" (the pgBackRest pod) is left as is.
- T is the target time of a point-in-time restore. Define it at the top of each runbook that uses it.
- WAL is the write-ahead log, Postgres's transaction log. It's copied to every repo all the time, separately from the scheduled backups. A point-in-time restore restores the last backup before T (the full backup plus any incrementals after it), then replays the WAL up to T.

## Checks after editing

- Format with Prettier, using the path from the table above, for example `<prettier> --write docs/crunchy-postgres-backup-and-restore/*.md`. It turns example JSON into multi-line, so example `oc` output goes in a `text` block to stay on one line.
- Check that every relative link resolves.
- If you change the T check, test it in both `bash` and `zsh`.
- Run `diff -r` against the bcparks-staff-portal folder, as described above.
