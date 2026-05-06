# Unity Isdk Get Bone Position

**Documentation Index:** Learn about unity isdk get bone position in this documentation.

---

---
title: "Get Hand Bone Position"
description: "Retrieve the world space position of any hand bone using the Interaction SDK GetJointPose method."
last_updated: "2025-11-06"
---

In this tutorial, you learn how to get the current world space position of a hand bone in Interaction SDK. Hands in Interaction SDK consist of multiple bones. The `enum HandJointId` in the **OVRHandPrimitives** script lists all of the bones in each hand. Since fingers have multiple bones, the bones use a number suffix. The number suffix increases the closer the bone is to the end of the finger. For example, the **HandThumb0** bone is at the root of the thumb, whereas **HandThumb3** is at the end of the thumb.

## Before you begin

* Complete [Getting Started with Interaction SDK](/documentation/unity/unity-isdk-getting-started/).

## Choose bone to track

1. Open your Unity scene.

1. Under **Project**, search for _OVRHandPrimitives_.

    The **OVRHandPrimitives** script appears.

1. Open the **OVRHandPrimitives** script and choose a bone from the `enum HandJointId`.

## Create script to call GetJointPose

`GetJointPose` is one of multiple methods provided by the `IHand` interface. It returns the world space transform of a given bone. You can access `IHand` in your project by referencing a GameObject that implements `IHand`, like **HandDataLeft**, **HandDataRight**, **LeftHand**, or **RightHand**.

1. Under **Project**, in **Assets** > **Scripts** (or wherever you store your custom scripts), create a new script called **LogBoneLocation**.

2. Open the **LogBoneLocation** script and paste this code.

    ```
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using Oculus.Interaction.Input;

    public class LogBoneLocation : MonoBehaviour
    {
        [SerializeField]
        private Hand hand;
        private Pose currentPose;
        private HandJointId handJointId = HandJointId.HandIndex3; // TO DO: Change this to your bone.

        void Update()
        {
            hand.GetJointPose(handJointId, out currentPose);
        }
    }
    ```

3. In the pasted code, change the value of `HandJointId handJointId` to the bone you want to track.

4. Add your own logic to access `currentPose`, which contains the current world transform of the selected bone.

## Learn more

### Design guidelines

- [Hands design](/design/hands/): Learn about design for using hands in your app.
- [Hands technology](/design/hands-technology/): Learn about the technology behind hands.
- [Hands best practices](/design/hands-best-practices/): Learn about the best practices, limitations and mitigations for using hands in your app.
- [Hands examples](/design/hands-examples/): Learn about hands interaction examples.
- [Hands quickstart prototype](/design/hands-quickstart-guide/): Get started quickly with a hands prototype.
- [Hand representation](/design/hand-representation/): Learn about hand representation.