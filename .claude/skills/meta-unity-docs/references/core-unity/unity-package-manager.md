# Unity Package Manager

**Documentation Index:** Learn about unity package manager in this documentation.

---

---
title: "Meta XR UPM Packages"
description: "Import the Meta XR packages and SDKs from the Unity Package Manager, either as individual packages or an all-in-one SDK."
last_updated: "2025-06-01"
---

<oc-devui-note type="important" heading="Oculus Integration SDK Release Deprecation">Oculus Integration SDK releases have been deprecated since version 57. For more information, including instructions on migrating from Oculus Integration SDK to Meta XR SDKs, see <a href="/documentation/unity/unity-import">Import Oculus Integration SDK</a>.</oc-devui-note>

Meta develops and maintains the following Unity packages for extended reality (XR) application development:

| Name | Description |
| ---- | ----------- |
| [Meta XR SDKs](/documentation/unity/unity-sdks-overview/) | Provide functionality for XR applications built with virtual reality or mixed reality components for Meta Quest devices. These SDKs enable you to create immersive user experiences, facilitate social connection, and optimize display hardware. |
| Meta XR Development Tools | Packages that enable you to develop, build, and test XR applications quickly. |

For the full list of Meta XR packages offered as UPM packages, see the [Developer Center](/downloads/unity/), the [Unity Asset Store](https://assetstore.unity.com/lists/list-9071889420297), or the [Meta NPM Registry](https://npm.developer.oculus.com).

## Import packages

Meta XR packages are created and managed using Unity's package management system, [Unity Package Manager (UPM)](https://docs.unity3d.com/Manual/Packages.html).

We recommend downloading Meta XR packages from the [Unity Asset Store](https://assetstore.unity.com/publishers/25353), which contains all the Meta XR packages. Packages and dependencies are automatically managed, making it easy to get started with XR development. For instructions, see [Download and import packages from the Unity Asset Store](#download-and-import-packages-from-the-unity-asset-store).

For advanced workflows, Meta XR packages can be downloaded as raw tarball files from [Meta's NPM Registry](https://npm.developer.oculus.com/), then imported into Unity via the [Unity Package Manager UI](https://docs.unity3d.com/Manual/upm-ui-tarball.html). Note that you must manage package dependencies manually by downloading and importing them from the NPM registry first.

**Note:** When packages are imported via UPM, package content is read-only by default. This ensures that package installations are complete replacements, reducing the risk of harming the recipient project. If you need to customize this behavior, see [Make Local Changes to Packages](#make-local-changes-to-packages).

To set up a new Unity project for XR development, see [Set up Unity for XR development](/documentation/unity/unity-project-setup/).

### Download and import packages from the Unity Asset Store

To download the latest version of Meta XR Packages from the Unity Asset Store and import them into an existing project:

1. Open your project in the Unity Editor.
2. Go to the [Unity Asset Store](https://assetstore.unity.com/publishers/25353) and sign in using your Unity credentials.
3. Navigate to the SDK package that you want to install. Select **Add to My Assets**. You may choose to see all your assets on the My Asset page before selecting **Open in Unity**, which opens the **Package Manager** window in the Unity Editor. Enter Unity credentials if prompted.
4. On the **Package Manager** window, select the SDK you wish to download. Select the latest version, and then on the upper-right side of the window, select **Install** (labeled **Add** in some Unity versions).
5. If prompted to update **OVRPlugin** (a plugin that allows Unity to communicate with the VR Runtime of Meta Quest), follow the prompt to restart the Unity Editor.

To update or switch to different package versions on Unity Asset Store, see [Unity's documentation for your respective editor version](https://docs.unity3d.com/Manual/upm-ui-update.html). Note that the package must be "Added to My Assets" in order to browse package versions.

## Migrate to Meta XR SDKs from Oculus Integration SDK

Oculus Integration SDK releases have been deprecated since version 57. We recommend that you migrate your projects to Meta XR SDKs as soon as possible in order to access the latest features and improvements, including new SDKs such as [Mixed Reality Utility Kit](/downloads/package/meta-xr-mr-utility-kit-upm).

**Note**: Migrating from the Oculus Integration SDK to Meta XR SDKs requires the removal of existing content. We recommend creating a backup of your existing Unity project before upgrading.

To migrate to Meta XR SDKs:

1. Close Unity, if open.
2. On your computer, go to the folder where you've saved the project. For example, `/username/sample-project/`.
3. In your project, open the `Assets` folder, and then delete the `Oculus` folders.
4. In your project, open the `Library` folder, and then delete `/Library/PackageCache/`.
5. Re-open the project. Open in Safe Mode if prompted, as the project is likely to have compile errors.
6. Follow the [steps here](/documentation/unity/unity-package-manager#import-packages) to install Meta XR SDKs as UPM packages. We recommend starting with the [Meta XR All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/), as that package includes a similar feature set to the legacy Oculus Integrations SDK.
7. If your project has errors or missing assets, this is likely because larger sample assets (such as SampleFrameworks) have been moved to Github as [Unity StarterSamples](/documentation/unity/unity-starter-samples). Follow the [instructions here](/documentation/unity/unity-starter-samples#integrate-samples-to-your-own-project) to import the assets into your project.

## Common issues with migration

If you followed the migration steps and are still running into issues, try the following:

- If any custom modifications were made to existing Meta XR SDK files, these custom changes must be manually merged into the new UPM-distributed Meta XR SDK files. For more information, see [Import Meta XR Packages](/documentation/unity/unity-package-manager/#make-local-edits-to-a-upm-package).
- There may be legacy Oculus files being referenced outside of the `Assets/Oculus` folder. To clean these up, search for files beginning with the following text and manually remove them: `Oculus` and `OVR`. In addition, if you're building an Android app, search for filenames starting or matching with `AndroidManifest`, `vrapi`, `vrlib`, and `vrplatlib`. These files are usually located in different folders of your project so it's best to search by the filename and then remove them.

## Make local changes to packages

UPM Packages are read-only by default. To make local changes, you need to make a local copy or fork of the package. This can be done in two ways:

- Copy the existing installed read-only package from the `PackageCache` into the project's `Packages` folder as an embedded package.
For more information, see Unity's documentation on [Copying a Unity Package from the Cache](https://docs.unity3d.com/Manual/upm-embed.html#embed-cached).
- Download the package tarball from [Meta's NPM Registry](https://npm.developer.oculus.com/), extract the files, and import
the package as a "Local Package on Disk" via the Unity Package Manager window. See [Install a UPM package from a local folder](https://docs.unity3d.com/Manual/upm-ui-local.html)
for more information.

## Learn more

To learn more about Meta XR packages, see the following resources:

- [Developing apps for Horizon OS](/documentation/unity/unity-development-overview)
- [Meta Horizon Developer Center downloads](/downloads/unity/)
- [Meta on Unity Asset Store](https://assetstore.unity.com/lists/list-9071889420297)
- [Meta NPM Registry](https://npm.developer.oculus.com)
- [Unity Package Manager documentation](https://docs.unity3d.com/Manual/Packages.html)