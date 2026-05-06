# Ps Sampleapp

**Documentation Index:** Learn about ps sampleapp in this documentation.

---

---
title: "Sample Apps"
description: "Explore sample code demonstrating Platform SDK features for Unity, Unreal, and native development."
last_updated: "2026-03-02"
---

The Meta Horizon platform SDK sample apps are provided examples of how to initialize the SDK, perform the entitlement check, and implement some of the platform features.

These sample apps can be used either as a reference for integrating the Platform SDK in your app, or as a foundation that you can use to build your VR experience.

The features implementations are similar with native and Unity apps, so review all of the apps below, even if they are using a different development platform.

## Prerequisites

To run the samples you will need the following:

- A [developer team](/resources/publish-account-management-intro/) in the Meta Horizon Developer Dashboard.
- An application created and associated with the developer team. Make sure to retrieve the App Id from the **Development** > **API** tab of the [Developer Dashboard](/manage). See [Creating and Managing Apps](/resources/publish-create-app/) for information about how to create a new application.
- A correctly configured development environment. Please see the [Getting Started Guide](/documentation/unity/ps-setup) for information about configuring your development environment.

## App Groupings

<oc-devui-note type="important" heading="Platform service app grouping deprecation" markdown="block">
Beginning in January 2026, certain platform services will no longer use
groupings to handle cross-app sharing. See [Platform Services](/documentation/unity/ps-platform-services/)
for new app sharing instructions. Refer to the "All platform services" page
in the developer dashboard for an up-to-date list of which services are still
using groupings.
</oc-devui-note>

If you're creating both a PC VR and mobile application, you can move the mobile application into the PC VR application's [App Grouping](/resources/publish-create-app/#grouping-apps-optional). This will allow cross-platform interactions between PC VR and mobile device users.

Then, copy the OSIG files for the mobile devices you are testing to `Assets\Plugins\Android\Assets`.

## Unity Sample Apps

The Platform SDK samples for Unity are available inside the [Meta XR Platform SDK UPM package](/downloads/package/meta-xr-platform-sdk/).

For information on how to import the SDK package and samples, see [Import the Unity Package](/documentation/unity/unity-package-manager).

Once you have imported the package and samples, find the samples in the Unity Project Window, under Assets &gt; Samples &gt; Meta XR Platform SDK &gt; (version number).
The following image shows an example:

As the Platform SDK does not have a dependency to the [Core SDK package](/downloads/package/meta-xr-core-sdk/), the samples have to be modified in order to support a VR camera. See the page [Add Camera Rig Using OVRCameraRig](/documentation/unity/unity-ovrcamerarig/) for more information.