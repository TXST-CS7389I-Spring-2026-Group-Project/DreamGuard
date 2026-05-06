# Ps Save Game Best Practices

**Documentation Index:** Learn about ps save game best practices in this documentation.

---

---
title: "Save Game Best Practices"
description: "Best practices for implementing reliable save game systems in Meta Quest applications."
---

## Save Game Best Practices

Managing save data is harder than it seems. There is always the potential for something to go wrong and generate a bad save and cause an unpleasant experience for your users or even yourself during development. Care should be taken when creating a save, and in particular, when overwriting an existing save. The tips and best practices outlined below can help mitigate some of common pitfalls of handling save data before they arise.

### Version your save data schema

Your app save data schema should be versioned and you should have a plan on how to handle old versions of your save schema when loaded. You can do this by embedding a version code inside the save file directly or in the file name. When you load the data and find the schema version and game's current schema version do not match, you know that you should either:

 1. Migrate the save data to a newer schema version. (*Highly recommended*)
 2. Abort the load entirely and discard the out-of-version data. (Note: when possible it's better to migrate, but having fallbacks in place to abort loading bad data is helpful in preventing undesirable crashes, especially during development.).

The save data schema version does NOT have to be the same exact number as build version, but it does need to be incremented when *anything* changes in the save data structure. Otherwise you may load corrupted or incompatible data and/or have a good chance of crashing your app! A good way to approach this is to not just dump all the save data into a structure, but instead, set your data structure to some sensible default values and then overwrite it with your save data. Using JSON is a good option (or some similar format of key-value pairs).

### Put an integrity check into your save file

Have the last line of the save file (or anywhere in your save schema) store an integrity check. You can compare the checksum against the save data to ensure that it was saved/loaded correctly. If it's a json file, missing data (an incomplete file) is usually a good indication of an interrupted save or something else going wrong in the save process, but it's better to not only rely on this and use a checksum directly.

### Have a specialized autosave solution

Have at least two files that are alternated between and do not delete the old file, keep it as a backup in case the first file is corrupted or incomplete. (Do make sure there is some limit to how many are kept because this will take up unnecessary disk space and potentially bloat your Cloud Backup save bundle.) Don't let an autosave process start while an old one is still running. Use a semaphore, queue, or have a method to abort the old autosave cleanly.

### Do not directly overwrite an existing save solution

Name the save with a prefix indicating what slot it is (ex: SaveSlot3_TimeStamp) and do not delete the previous file of that save slot until the new one is correctly written out. When loading, check integrity of the save file and if it fails the integrity check, check the previous file instead and use that one if it is good. Afterwards, delete the bad save and load from the previous good one. Notify the user that a previous save had a problem and that an older version is available. Giving players the option to load from either the save slot or from the autosave can help mitigate these problems when they occur. For each save, display the date/time the save occurred at and the total play time. This lets the user make an informed decision as to which file is the right one to load.

### Allow saving anywhere

This is optional, but nice to have. Use frequent autosaves or allow the player to initiate a save anywhere in the game (even if it takes them back to the start of the level). It is almost always easier to save a limited amount of data at the start of a level and restore from there than reloading from a point in the middle. This is ultimately a game design decision, so if you want to make a more hardcore game, feel free to be more restrictive.

### Avoid zombie data problems during development

During development it's not uncommon to come across a variety of "zombie" data problems when bad/old data is loaded by your app that causes a variety of headaches (crashes, unexpected behavior, etc), especially when you're trying to iterate quickly. There are a few tips/tricks to help resolve this.

1. During development, when the app loads up data from a previous version, you can optionally discard it to prevent issues with incompatible saves between app versions. (Note: as suggested previously, this is not recommended for live apps! You should ultimately have a system in place to migrate data from an older save version to a new one.)
2. Implement save migrations from old to new versions. This is important for releasing your app! Have routines that migrate your save data up to the next version. If your save data is older by several versions, call each routine in succession until it's up-to-date (ex: save data version 4 needs to be migrated to version 6, first migrate to version 5, then from version 5 migrate to version 6). This reduces the amount of logic you need to migrate between large version gaps. Be careful though, if you are missing a migration between versions you will have to discard your data!
3. Defer loading save data. You probably won't remember to update your build number every time or make some other easy mistake and cause your app to black screen when loading as it tries to parse your bad save data. A good way to avoid this is to not load the save data when your app loads. Load and display your opening screen first, along with what version of the game you are running (even in released games, it's good to have this for bug reporting reasons). Once your opening screen loads, try to process the save, and display any issues to the user.
4. After reinstalling the app, you can pre-emptively clear application data by going to the Storage Panel and selecting "Clear application data" for your app. This can be useful if you know you have breaking changes in your latest version and you have not yet implemented save data migration logic yet.
5. On the headset, disable Cloud Backup by going to Settings > Cloud Backup and switching off the toggle. Cloud Backup is an incredibly important feature to have enabled in order to preserve user data across app installs, but during development it can accidentally cause you to load bad data that got backed up to the cloud. If you don't have any methods to handle this built into your app yet, it can cause a variety a problems. Disable it locally on your headset only, do NOT disable it on the Developer Dashboard as this will disable Cloud Backup for all your users!

### Understand and work with Cloud Backup

[Cloud Backup](/documentation/unity/ps-cloud-backup/) uploads your application data to the cloud so it can be restored the next time the app is installed. It's a helpful tool for users to have if they ever need to uninstall your app for any reason or if they upgrade to a new Quest device.

1. Keep save data size small. Cloud Backup supports uploading 100MiB in save data per backup, but it's always better to only require a much smaller set of data to restore your app state. You can use the file/folder exclusions feature on the Developer Dashboard under the Cloud Storage section to tweak what directories are included in your backup to limit its size. You can also follow the [Android Auto Backup including/excluding files documentation](https://developer.android.com/guide/topics/data/autobackup#IncludingFiles) to add custom rules to your Android Manifest.
2. Make sure your save data is saved to a supported directory under your app! (i.e. a custom folder under the root folder of your app's data directory won't be backed up! Putting data under the files/external files directories of your app is the easiest way to make sure it's included in the backup).
3. Exclude shader files from your app's backup. Shaders can potentially bloat your backup size, especially if they get pre-warmed when the app is first started and saved under your app's files directory (which can happen with certain game engines).