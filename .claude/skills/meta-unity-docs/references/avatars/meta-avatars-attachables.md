# Meta Avatars Attachables

**Documentation Index:** Learn about meta avatars attachables in this documentation.

---

---
title: "Socket Attachables"
description: "A tutorial on attaching objects to Avatars using sockets in the Meta Avatars SDK."
---

<oc-devui-note type="warning" heading="Meta Avatars SDK has reached EOF" markdown="block">
The Meta Avatars SDK has reached End-of-Feature (EOF) status. **[Version 40.0.1](/downloads/package/meta-avatars-sdk/) is the final release.** While no further SDK versions, new features, or API additions are planned, all existing integrations continue to work and backend services remain fully operational. Developers can continue to build and submit apps with the current SDK.
</oc-devui-note>

## Attachables

The Attachables system allows objects to be attached to the Avatar and appropriately repositioned based on the actual Avatar's size. Common use cases include things like backpacks, equipment and items on the hip.

Historically, items were attached using [critical joints](/documentation/unity/meta-avatars-ovravatarentity/#critical-joint-types), which were replicated as Unity transforms in the scene. This allowed items to then be parented. However, with the introduction of Avatars 2.0, body sizes can be dynamically sized, so expanding the attachment system (and providing object scaling) was necessary.

The system now does 2 things:

1. Allows GameObjects to be attached to the Avatar
2. Dynamically adjust the position and scale of the objects to size of the Avatar. E.g. an item attached to the outside of the hip of an Avatar, will always stay on the outside of the hip, no matter the Avatar.

## OvrAvatarSocketDefinition

The `OvrAvatarSocketDefinition` is the main class used for Attachables. It does both the configuration and setup of the sockets.

## Scenes

The [joint attachments](/documentation/unity/meta-avatars-samples/#joint-attachments) scene provides an example of how to use the upgraded joint attachment system. It features a first-person and third-person avatar with objects that get attached to their head, chest, and hips. Each avatar has a `SampleAvatarAttachments` script, which uses an `OvrAvatarSocketDefinition` to define how objects attach to the avatar, and attaching prefabs to them once the sockets initialize. During avatar initialization, after the skeleton is loaded, `OvrAvatarSocketDefinition` will configure itself, and if configured, will instantiate socket `GameObjects` in the scene parented to the critical joints under the avatar's `GameObject` hierarchy.

The [attachables authoring](/documentation/unity/meta-avatars-samples/#attachables-authoring-scene) scene provides a way to preview what a socket will look like. This scene has code that automatically reloads the sockets, so that they can be previewed in real time. The sliders on the right can be used to control parameters of the socket. The code on the left shows the parameters that can be used to recreate the socket. The window can be copy/pasted from in the editor.

## How to use Attachables

1. Define the critical joint on the Avatar entity
2. Create the socket
3. Attach whatever GameObject you want to the socket

## Define the Critical Joint

Before you attach a socket to an object, you first need to define a [critical joint](/documentation/unity/meta-avatars-ovravatarentity/#critical-joint-types) on the avatar’s [OvrAvatarEntity](/documentation/unity/meta-avatars-ovravatarentity/). If the critical joint is not on the avatar entity, the socket will not be created and any invocations to `Attach()` become a no-op.

## Creating sockets

Once you have an avatar configured with a critical joint, you’ll need to create an `OvrAvatarSocketDefinition`. Either:

1. Via code `OvrAvatarEntity.CreateSocket()`
2. Via the AttachableSocket component (Meta Avatar SDK V33 or above)

We will first show off the code version, because the parameters are the same for both code and the component

### Minimum configuration

```cs
    // A reference to the Avatar entity you want to add a socket to
    OvrAvatarEntity avatarEntity = ...;

    OvrAvatarSocketDefinition chestSocket = avatarEntity.CreateSocket(
        "MY_CHEST_SOCKET_NAME",
        CAPI.ovrAvatar2JointType.Chest,
        position: new Vector3(0f, 0f, 0f),
        eulerAngles: new Vector3(-90, 0, -90),
    );
```

These 4 arguments are the minimum configuration needed to create a socket, but there are more options.

### CreateSocket Parameters

**string socketName** - Required - The Unique Name of the attachable socket. The name must be unique to the owning OvrAvatarEntity.

**CAPI.ovrAvatar2JointType jointType** - Required - The Joint Type to attach this socket to. The same Critical Joint must be exposed on the attached OvrAvatarEntity.
examples: CAPI.ovrAvatar2JointType.Chest, CAPI.ovrAvatar2JointType.Head

**Vector3 position** - Required - Local Position of the socket, relative to the Critical Joint. Essentially to offset off the object.

**Note**: The axis orientation is different from Unity.

**Vector3 eulerAngles** - Required - Local Rotation relative to the Critical Joint's rotation

**Vector3 baseScale** - Optional - Scaling applied uniformly to the attached object, regardless of Avatar size
Default Value: Vector3.one

**Note:** The following three values (`width`, `depth` and `height`) each adds a dynamically scaling aspect to the attached object. For example, width could be set to 0.2f and a hat is attached. The hat may scale larger for an Avatar with a larger head and scale less for a smaller head. Look at the AttachablesAuthoring Scene for a demonstration. The above `baseScale` is just a simple relative sizing, with no scaling compared to the Avatar.

**float? width** - Optional - In meters. Scales the object in relation to the Avatar size. If null, the attached object does not scale to Avatar size

**float? depth** - Optional - In meters. Scales the object in relation to the Avatar size. If null, the attached object does not scale to Avatar size

**float? height** - Optional - In meters. Scales the object in relation to the Avatar size. If null, the attached object does not scale to Avatar size

**bool createGameObject** - Default true. Generally should be kept true. When true, generates the GameObject that can actually be attached to. Read below for why you might want it false

**bool scaleGameObject** - Default false. If true, scale the socket based on the `width`, `depth`, `height` and `baseScale` parameters. If false, ignores those values.

#### Example of full configuration

```cs
OvrAvatarSocketDefinition chestSocket = entity.CreateSocket(
    "MY_CHEST_SOCKET_NAME",
    // Any critical joint defined above
    CAPI.ovrAvatar2JointType.Chest,
    // Canonical position relative to above critical joint
    position: new Vector3(0f, 0f, 0f),
    // Local Rotation relative to Critical Joint rotation
    eulerAngles: new Vector3(-90, 0, -90),
    baseScale: Vector3.one,
    // Canonical Sizes relative to Critical Joint (For scaling)
    depth: 0.25f,
    width: 0.4f,
    // a null size property prevents scaling on that axis
    height: null,
    createGameObject: true,
    scaleGameObject: false
);
```

```cs
// You can have multiple sockets for the same Joint, as long as the names are unique
// Both of these sockets are on the Hip joint
var holsterSocket1= entity.CreateSocket(
    "HolsterSocket1",
    CAPI.ovrAvatar2JointType.Hips,
    position: new Vector3(0.049f, 0, -0.157f),
    eulerAngles: new Vector3(0, 0, 90)
);

var holsterSocket2 = entity.CreateSocket(
    "HolsterSocket2",
    CAPI.ovrAvatar2JointType.Hips,
    position: new Vector3(0.80f, 0, -0.160f),
    eulerAngles: new Vector3(0, 0, 90)
);

// If the names aren't unique, CreateSocket will just return a reference to the first socket and not create the second one
var testSocket1 = entity.CreateSocket(
    "SameName",
    CAPI.ovrAvatar2JointType.Hips,
    position: new Vector3(0.049f, 0, -0.157f),
    eulerAngles: new Vector3(0, 0, 90)
);

var testSocket2 = entity.CreateSocket(
    "SameName",
    CAPI.ovrAvatar2JointType.Hips,
    position: new Vector3(0.80f, 0, -0.160f),
    eulerAngles: new Vector3(0, 0, 90)
);

testSocket1 == testSocket2 // true
```

#### AttachableSocket Component (Meta Avatar SDK V33 and above)

There is also an AttachableSocket Unity Component that can be added to an Avatar. On `Start()` it will create an `OvrAvatarSocketDefinition` that can be accessed via `Socket`

As a Component, it can also be accessed via code

```cs
AttachableSocket _generatedSocket;

_generatedSocket = entity.gameObject.AddComponent<AttachableSocket>();
_generatedSocket.SocketName = "SomeProgramaticSocket";
_generatedSocket.JointType = CAPI.ovrAvatar2JointType.Chest;
_generatedSocket.BaseScale = new Vector3(30f, 30f, 30f);
_generatedSocket.Position = new Vector3(0.7f, 0.7f, 0.7f);
_generatedSocket.EulerAngles = new Vector3(30f, 30f, 0f);

// Later on after the initialization
if (_generatedSocket.Socket.IsReady() && _generatedSocket.Socket.IsEmpty())
{
    _generatedSocket.Socket.Attach(/*GameObject to attach*/));
}
```

#### Socket Initialization

Once the socket has been defined either by `CreateSocket()` or the component, it needs to be initialized. The `OvrAvatarManager` in the scene will automatically initialize any uninitialzed objects in the scnee in its `Update()` call. This should take care of nearly all situations.

If it's required to do so manually, call `OvrAvatarEntity.OnUpdateAttachables()`.

`OvrAvatarSocketDefinition.IsReady()` returns `true` when the socket has initialized and is ready to be used (e.g. `Attach()` can be used).

Since initialization is asynchronous, it may take a few frames to happen and code may need to wait on `IsReady()` such as in an different `Update()` call or via coroutine.

## Attaching objects

Now to attach your equipment `GameObject` to your Socket. In this example, we will have it in an `Update()` function

```cs
    // This was saved or created previously
    OvrAvatarSocketDefinition chestSocket;
    GameObject someEquipmentGameObject;

    void Update() {
        if (chestSocket != null && chestSocket.IsReady()) {
            chestSocket.Attach(someEquipmentGameObject);
        }
    }
```

Each `OvrAvatarSocketDefinition` can have only a single `GameObject` attached to it. If you try to attach a different object to a socket that already contains a `GameObject`, it will automatically detach the first object.

```cs
    // Example GameObjects
    GameObject yellowBackpack;
    GameObject redBackpack;

    // Nothing is in the socket

    chestSocket.Attach(yellowBackpack);

    // A yellow backpack is now attached

    chestSocket.Attach(redBackpack);

    // The yellow backpack is removed and now the red backpack is attached
```

To check if an object is currently attached to a socket call `OvrAvatarSocketDefinition.IsEmpty()`. And to manually detach it use `OvrAvatarSocketDefinition.Detach()`.

```cs
    if (!chestSocket.IsEmpty())
    {
        chestSocket.Detach();
    }
    chestSocket.Attach(redBackpack);

```

`Attach()` assumes that `createGameObject` was set to `true` on the socket. If it's set to `false`, it is a no-op, because there would be no `GameObject` to attach to. Refer to the below section on `createGameObject` for an explanation on why that is.

And that's how to Attach to the Socket!

## Other Information

### `createGameObject` as `false`

The `createGameObject` flag is not referencing the `GameObject` that is attached to the socket such as equipment. It's referencing a `GameObject` that is created and attached to the avatar skeleton and would hold the attached items. In the below image, `[SOCKET] HatSocket` is created by `createGameObject` and the `Hat(Clone)` is what is `Attach()` to it.

There may be times when you do not want this `GameObject` to be made, but you still want information from the socket on how the object would be affected. So instead you can pass `createGameObject: false` and that `GameObject` won't be created. Then you can get the position or scale information from the socket definition post initialization.

```cs
chestSocket.localPosition
chestSocket.localEulerAngles
chestSocket.localScale
```

### Manually Parenting the object

You can also get the created `GameObject` manually and parent like you normally would in Unity:

```cs
var go = chestSocket.socketObj;
myEquipment.SetParent(go, false);
```