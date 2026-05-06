# Ps Cloud Backup

**Documentation Index:** Learn about ps cloud backup in this documentation.

---

---
title: "Cloud Backup"
description: "Save and restore user progress across devices with the Cloud Backup feature for Meta Quest apps."
last_updated: "2024-09-19"
---

Cloud Backup is a system to backup device app data. This includes backing up progress and settings for participating apps to the cloud, so people can pick up where they left off in an app. This makes it easier to use apps on new headsets, reinstall apps, or reset a device. Cloud Backup uses [Android Auto Backup](https://developer.android.com/guide/topics/data/autobackup).

Cloud Backup works at the filesystem level, with no coding required. Apps are opted-in by default, and you can opt out or make adjustments through the Developer Dashboard. To keep Cloud Backup data safe, app data is encrypted on the individual device and remains encrypted on our servers.

Users can choose to opt-out of Cloud Backup in their headset settings.

## Use cases for app users

### How can I view and delete Cloud Backups for an app I own?

To view existing backups across all of your apps, navigate to the [Cloud Backups](https://secure.oculus.com/my/cloud-backup/) tab on the Meta Quest website.

On this page you'll find a list of owned applications, and a filter to search by name. If an application has an existing backup in the cloud, there will be a timestamp, size, and an option to delete the backup.

### When does an app I own perform a backup?

**Backups** happen automatically at the following times:

- Once per night, when the device is idle, charging, and connected to WiFi, similar to when the system will update the OS.
- When the app is uninstalled, if it was installed from the store
- Each time the app is exited, up to once every six hours

 Each user can have multiple backup 'slots' per purchased app, per headset, and each backup run will not overwrite the previous slot. In addition to automated backups, users can manually request backups by going to the Cloud Backup tab located in device settings.

### When does an app I own restore its Cloud Backup data?

Cloud Backup automatically restores the latest backup for an app when it's installed. Users can determine which backup will be restored by navigating to the [Cloud Backups](https://secure.oculus.com/my/cloud-backup/) web page. Here users can also request to manually restore a specified backup at any time.

### Does my Cloud Backup data generated on one device transfer to another device?

Yes. If Cloud Backup cannot find data for a specific app+device pairing, then it will restore the most recent backup for that app across all owned devices.

Consider the example of a user who has Cloud Backup data generated for an app on their Meta Quest 2, who has just purchased a Meta Quest 3. Upon installing the app on their new Meta Quest 3, the Cloud Backup system **will restore backed-up data from the Meta Quest 2**, due to being unable to find an instance of backed-up data for that app associated with their Meta Quest 3.

Once Cloud Backup is performed on the Meta Quest 3, that data will be backed up under the 'slot' for their purchased app, **for their Meta Quest 3 headset**. Beyond that point, the Cloud Backups for Meta Quest 2 and for Meta Quest 3 will diverge.

A user who wishes to copy Cloud Backup data between two headsets can navigate to the [Cloud Backups](https://secure.oculus.com/my/cloud-backup/) tab on the Meta Quest website and perform a restore of the Cloud Backup data targeting the headset they want to transfer the data to. A new slot for the transferred Cloud Backup data will automatically be created for the targeted headset.

Cloud Backup data may be transferred from newer generation headsets (i.e. Quest 3) to older generation headsets (i.e. Quest 2), and vice versa.

## Use cases for app developers

### How do I enable Cloud Backup for my title when our title previously used Cloud Saves V2?

In the [Developer Dashboard](/manage), click **Development** > **Cloud Storage**. Select your app in the dropdown. On the Cloud Storage page, ensure **Enable Automatic Cloud Backup** is toggled on.

See more about how to correctly store data in [Technical Implementation](#technical-implementation).

### How do I turn off Cloud Backup for my title if I don't want it to be used?

In the [Developer Dashboard](/manage), under **Development** > **Cloud Storage**, toggle off **Enable Automatic Cloud Backup**.

## Technical implementation

Store app data only in supported directories, don't exceed the 100 MB limit, and have enabled Cloud Backup for your app on the Developer Dashboard.

If these conditions are met, the app will backup automatically.

### Directories that will be backed up

[Auto Backup](https://developer.android.com/guide/topics/data/autobackup) includes files in most of the directories that are assigned to your app by the system. You can find these file locations by accessing  these through a [Context object](https://developer.android.com/reference/android/content/Context):

* Shared preferences files
* Files saved to your app's internal storage, accessed by [`getFilesDir()`](https://developer.android.com/reference/android/content/Context#getFilesDir()) or [getDir(String, int)](https://developer.android.com/reference/android/content/Context#getDir(java.lang.String,%20int))
* Files in the directory returned by [getDatabasePath(String)](https://developer.android.com/reference/android/content/Context#getDatabasePath(java.lang.String)) which also includes files created with the [SQLiteOpenHelper](https://developer.android.com/reference/android/database/sqlite/SQLiteOpenHelper) class
* Files on external storage in the directory returned by [getExternalFilesDir(String)](https://developer.android.com/reference/android/content/Context#getExternalFilesDir(java.lang.String))
* The above locations provided by a device-protected storage context

Beware that custom folders made in the app's root directory are not backed up.  If you want to create custom folders, they should be within the directory returned by [`getFilesDir()`](https://developer.android.com/reference/android/content/Context#getFilesDir()).

#### Backed-up folders
```
/data/data/<package-name>/files/
/data/data/<package-name>/<custom-folder>/
/sdcard/Android/data/<package-name>/files/
```

#### Not backed-up folders
```
/data/data/<package-name>/cache/
/data/data/<package-name>/no_backup/
/sdcard/Android/data/<package-name>/cache/
/sdcard/Android/data/<package-name>/<custom-folder>
```

### Directories that won't be backed up

Android Auto Backup excludes files in directories returned by [getCacheDir()](https://developer.android.com/reference/android/content/Context#getCacheDir()), [getCodeCacheDir()](https://developer.android.com/reference/android/content/Context#getCodeCacheDir()), and [getNoBackupFilesDir()](https://developer.android.com/reference/android/content/Context#getNoBackupFilesDir()). The files saved in these locations are needed only temporarily and are intentionally excluded from backup operations.

In addition, any folder within [`getFilesDir()`](https://developer.android.com/reference/android/content/Context#getFilesDir()) named `UnityCache` will not be backed up.

By default, these excluded directories are `cache/` and `UnityCache/`

You can also customize excluded files/folders that do not need to be backed up in the [Developer Dashboard](/manage), under **Development** > **Cloud Storage**. When specifying custom excluded files, please keep the following details in mind:

* You can only specify paths relative to the package's internal/external files directories (these are the directories returned by [`getFilesDir()`](https://developer.android.com/reference/android/content/Context#getFilesDir()) or [`getExternalFilesDir()`](https://developer.android.com/reference/android/content/Context#getExternalFilesDir(java.lang.String))).
  * Example: if the data you want to exclude is stored under `/storage/emulated/0/Android/data/com.company.somegame/files/UE4Game/SomeGame/SomeGame/BigFiles/SomeFile.txt` then you would specify `UE4Game/SomeGame/SomeGame/BigFiles/SomeFile.txt` as the exclude path.
* If you specify a custom file exclusion, all matching files/folders will be excluded from both the internal/external files directory. You cannot specify which of the two directories to target.
* You cannot use wildcards. Paths must reference either exact files or folders precisely.

For more fine grained control over file/folder inclusion and exclusion, please refer to the [Android Auto Backup Including Files documentation](https://developer.android.com/guide/topics/data/autobackup#IncludingFiles) for more information.

### Should DLC files be stored in directories that will be included in the backup?

Put DLC save game files and config files in one of the directories that will be backed up.

Do not put DLC asset files in a backup directory.

Assets should be put into an excluded directory so that they don't get backed up. If the total size goes over 100 MB, this would cause a backup failure. Even barring that, it could worsen the user experience. For one thing the user will be uploading a lot of wasted info. Furthermore, if they delete the app and then reinstall after an update to the app was made, they will likely waste time downloading old versions of files that will then be almost immediately updated. The DLC entitlement will allow them to download those files once again as normal.

### How can a developer check when the last time an app backup occurred?

You can check the last time an app was backed up by navigating to the [Cloud Backups](https://secure.oculus.com/my/cloud-backup/) page on the Meta Quest website.

You can also check by navigating to the Cloud Backup tab located within device settings on your Meta Quest device.

### How can a developer check what files were included in a backup?

You can validate what files are included in your backup by monitoring logcat while a backup is executing. Logs should be outputted which indicate which files and their respective sizes were included in a backup. This is useful to check if your app goes over the 100 MB backup limit and need to check which files are taking up space in your app's backup or if you want to validate any recently implemented file exclusion/inclusion rules are working as intended.

You can run the following `adb` command to filter logcat to get only backup related logs:

```
adb logcat | grep Backup
```

## Testing

### Testing Cloud Backup

The android shell `bmgr` command can be used to test the backup system. Use the following command to force backup to run for a specified package.

```
bmgr backupnow --monitor-verbose <package-name>
```

The command may be run directly from your local host with `adb shell` or from an adb shell session on your device.

You can learn about more `bmgr` by running the command without options.

*Important:* Forcing a restore via ADB is not supported. Restores happen automatically when you install an app. To transfer backup data to another headset, uninstall and reinstall the app on that device to trigger the restore.

> **Note**: this is NOT the same "adb backup", which is not supported.

### Toggling Cloud Backup on a live app for testing purposes

> **Note**: We recommend being VERY careful if you plan to do this, and it is not advised on a live app.

You can turn off the backup and restore feature in the device settings with the Cloud Backup toggle. By default it is on for all users. You can clear a specific app's data with a new button in the storage setting panel.

You can also turn off Cloud Backup for your app in the [Developer Dashboard](/manage). Under **Development** > **Cloud Storage**, toggle off **Enable Automatic Cloud Backup**.

## Troubleshooting

### What are the common reasons a directory is not backed up?

The common cases for non-conforming app data:
- The app data is in a non supported directory (such as a custom folder in the root directory)
- The app data for the app is over 100 MB size
- A directory that contained save data was accidentally excluded by the developer

### Does the app need to update or be opened in order for the backup to occur?

Generally no -- assuming the app is eligible for backup and the backup conforms to the requirements (specified above), it will happen all on its own.

### How does a user manually trigger a backup (for example right before they do a factory reset)?

Ad-hoc backups are supported for both users and developers in the Cloud Backup panel in device settings. Developers can also test them using the bmgr command over ADB.

### If a user has an older version of the OS and hasn't turned their Meta Quest on, what happens? Can they turn it on and have it backup their saves?

Cloud Backup requires Meta Horizon OS v35 or later. The usual requirements for OS updates apply.

### How do I delete an individual save?

Go to the [Cloud Backups](https://secure.oculus.com/my/cloud-backup/) profile. A delete option appears next to each save. Click that to delete save data for that app from the cloud.  Please note that deletions are **permanent**. If you delete ALL backed up data for that app it will be gone from the Cloud. The app may have backup data on multiple devices, to fully delete all cloud data you may need to delete the backups for that app across all owned devices.

### What backup is stored when a user has app data on multiple devices?

Each device has its own backup history. A backup generated on one device would not conflict or otherwise overwrite backups generated on other devices.

### How many backups are stored for each app?

Each app stores at most five backups per device. Additionally, the two oldest backups for an app are retained up to a week after they've been generated before overwriting them with a newer backup.

### Whenever I uninstall an app, it says my data may be permanently lost. Does this mean Cloud Backup isn't working?

No, it is just there to warn you. Uninstalling an application removes the local data, but does not delete any Cloud Backups.  Check your profile's [Cloud Backups](https://secure.oculus.com/my/cloud-backup/) or navigate to the Cloud Backup tab in device settings to confirm that the backup is safe on the cloud.