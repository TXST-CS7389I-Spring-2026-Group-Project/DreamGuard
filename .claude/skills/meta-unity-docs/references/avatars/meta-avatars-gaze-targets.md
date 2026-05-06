# Meta Avatars Gaze Targets

**Documentation Index:** Learn about meta avatars gaze targets in this documentation.

---

---
title: "Gaze Targets for Meta Avatars SDK"
description: "Configure gaze targets so Meta Avatars look at specific objects and characters in your scene."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

## Targeting Avatar Gaze

Meta Avatars can be directed to focus their gaze on specific targets.

To target an avatar's gaze use the following process:

1. Add the `OvrAvatarGazeTarget` component to a game object that can be targeted.

1. Specify the Target Type.

This allows Meta Avatars to focus their gaze on targets they are nearby. Avatars do not gaze constantly at any one target, but will shift their focus naturally between nearby targets and glancing straight ahead.

When using GPU skinning, ensure that any joints which will be gaze targets are in the Critical Joint Types list. See [Critical Joint Types](/documentation/unity/meta-avatars-ovravatarentity/#critical-joint-types) for more information.

## Sample Scenes

### Gaze tracking

This scene demonstrates various gaze target scenarios. It also showcases how avatars' eyes simulates natural behaviors like temporarily glancing away from their current gaze target.
* Location: `/Avatar2/Example/Scenes/GazeTrackingExample/`

For details, go to [Gaze Tracking](/documentation/unity/meta-avatars-samples/#gaze-tracking) on samples page.

### Mirror

This scene places targets on the first-person Avatar’s hands and head so that users can observe the mirrored Avatar focusing on the gaze targets.
* Location: `/Avatar2/Example/Scenes/MirrorScene/`

For details, go to [Mirror Scene](/documentation/unity/meta-avatars-samples/#mirror-scene) on samples page.