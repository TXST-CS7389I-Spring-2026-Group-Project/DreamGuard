# Ts Mqdh Xr Projects

**Documentation Index:** Learn about ts mqdh xr projects in this documentation.

---

---
title: "Create new XR projects in MQDH"
description: "Generate Unity projects preconfigured for Meta Quest device development by using Meta Quest Developer Hub."
last_updated: "2025-11-20"
---

Meta Quest Developer Hub (MQDH) offers a convenient way to start a new XR project, preconfigured for Meta Quest
device development. To download MQDH, follow the instructions in [Get started with Meta Quest Developer Hub](/documentation/unity/ts-mqdh-getting-started/).

To get started, follow the steps below to generate and open a new project.

<box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
  <section>
    <embed-video width="100%">
      <video-source handle="GJSbUB0UNVMpwCoCALSLSFz4MxR8bosWAAAF" />
    </embed-video>
  </section>
  <text display="block" color="secondary">
      Unity XR project creation flow in MQDH
  </text>
</box>

## Step 1: Open MQDH and navigate to XR projects

1. Launch **Meta Quest Developer Hub**.

2. In the left sidebar, click **XR Projects**.

  

3.  If the app cannot find Unity installed in the default location, follow the instructions to download and install it.

3. Click **Create project** in the top right corner.

## Step 2: Fill in your project details

1. You should see a dialog titled **Create Unity XR project**.

2. Select values for the following fields:

| Field | Description |
|-------|-------------|
| <b>Name</b> | A descriptive name for your project. Limit: 100 characters. |
| <b>Location</b> | The folder on your computer where you want to save the new project. |
| <b>Version</b> | The Unity Editor version 6.1, which starts with "6000.1", or a later version. |

## Step 3: Choose a configuration

In the **Configuration** section, set the following fields:

| Field | Description |
|-------|-------------|
| <b>XR Plugin</b> | Provider plugin package. Select <b>OpenXR</b>. |
| <b>SDKs</b> | The Meta XR SDKs to install with the project. Select **All** to install all Meta XR packages found in the [All-in-One SDK](/downloads/package/meta-xr-sdk-all-in-one-upm/) |

<oc-devui-note type="note" heading="OculusXR plugin deprecation notice">
  The <b>OculusXR</b> plugin is deprecated and no longer receives updates or support for the latest Meta Quest features.
</oc-devui-note>

The following screenshot shows the XR Plugin selection:

The following screenshot shows the SDKs selection:

## Create the project

1. Click **Create** at the bottom of the dialog to start generating the project. You should see a loading status, such as *Processing · 15%*.

   

2. Once the project generation completes, your project automatically opens in Unity Editor.

## New project contents

Your new project comes preconfigured with:

- Meta Quest-compatible settings for VR development
- SDKs and plugins based on your choices
- Default folder structure

## Next steps

After the project is created, you can:

- Create and preview an interaction in [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr)
- Add Building Blocks to your scene by following [Explore Meta Quest features with Building Blocks](/documentation/unity/bb-overview)

- In MQDH, manage your headset using [Device Manager](/documentation/unity/ts-mqdh-basic-usage/) and [Deploy a Build to your headset](/documentation/unity/ts-mqdh-deploy-build/)

## See also

* [Meta Quest Developer Hub](/documentation/unity/ts-mqdh/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)