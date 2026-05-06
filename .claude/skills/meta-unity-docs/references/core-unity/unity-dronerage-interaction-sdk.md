# Unity Dronerage Interaction Sdk

**Documentation Index:** Learn about unity dronerage interaction sdk in this documentation.

---

---
title: "Interaction SDK integration in DroneRage"
description: "Interaction SDK integration in DroneRage shows how to add poke, grab, and ray interactions to a mixed reality game."
---

The [Interaction SDK](/documentation/unity/unity-isdk-getting-started/) is used to add specific interactions like pokes or grabs to Meta Quest inputs like controllers or hands.

In Discover, the `AppInteractionController` script is added to each user’s camera rig. This class initializes interactors for Grab, Pinch, and Release from the Interaction SDK.

In DroneRage, the Interaction SDK is used in two major areas: the laser gun trigger pull and menus.

## Grabbing the gun

The guns are grabbable objects that are oriented into the hands by the Interaction SDK.

As a design consideration, for hand tracking, the guns could be automatically attached to the user’s hands. This would mean, however, that trying to pull the trigger would open a menu instead of triggering a shot.

To work around this and get the trigger working, a `currentGrabInteractor` is defined based on whether the user is using hands or not. This logic is defined in `WeaponInputHandler` class in `/Assets/Discover/DroneRage/Scripts/Weapons/WeaponInputHandler.cs`:

```
        private void UpdateInteractor(bool useHands)

        {
            CleanupInteractor();
            m_usingHands = useHands;
            m_currentGrabInteractor = m_usingHands ? m_handGrabInteractor : m_controllerGrabInteractor;
            m_currentActiveState = m_usingHands ? m_handActiveState : m_controllerActiveState;
            var interactable = m_weapon.HandGrabInteractable;
            if (interactable != null && m_currentGrabInteractor != null && m_currentActiveState.Active)
            {
                Debug.Log($"Selecting {interactable} with {m_currentGrabInteractor}", this);
                m_currentGrabInteractor.ForceSelect(interactable);
            }
            m_isCurrentInteractorActive = m_currentActiveState?.Active ?? false;
        }
```

The [following line](https://github.com/oculus-samples/Unity-Discover/blob/1938d235351723ef31c21b8f60b115a12467373c/Assets/Discover/DroneRage/Scripts/Weapons/WeaponInputHandler.cs#L109) gets the current `GrabInteractor` which is either the hand or the controller `GrabInteractor`.

```
m_currentGrabInteractor = m_usingHands ? m_handGrabInteractor : m_controllerGrabInteractor;
```

Using the Interaction SDK’s `ForceSelect()` function on the current `GrabController` results in the player automatically grabbing the object with the following line:

```
m_currentGrabInteractor.ForceSelect(interactable);
```

Then, in `Update()`, in every frame, it checks whether the user is using hands:

```
            if (useHands != m_usingHands || activeStateChanged)
            {
                UpdateInteractor(useHands);
            }
```

If the weapon is not grabbed, the player automatically grabs it.

## Pulling the gun's trigger

For the user to pull the weapon trigger, the [`HandGrabAPI`](/documentation/unity/unity-isdk-hand-grab-interaction/#handgrabapi) on the interactor is called in [`Update()`](https://github.com/oculus-samples/Unity-Discover/blob/1938d235351723ef31c21b8f60b115a12467373c/Assets/Discover/DroneRage/Scripts/Weapons/WeaponInputHandler.cs#L126). This captures the `FingerPalmStrength` of the index finger.

```

                var triggerStrength = m_currentGrabInteractor.HandGrabApi.GetFingerPalmStrength(HandFinger.Index);

```

If the trigger strength is above a set threshold, the trigger is held and the gun will fire. This works so that if the user holds the trigger down, the gun will continuously fire. If they hold it down then let it go, the gun only fires once. This is true for both the machine gun and the pistol that the player may be holding.

```
                isTriggerHeldThisFrame = triggerStrength > m_triggerStrengthThreshold;
```

Because this uses the `HandGrabAPI`, it works on both hand grab and the controller through different `Interactors`.

```
            if (m_weapon.HandGrabInteractable != null && m_currentGrabInteractor != null &&
                m_currentGrabInteractor.HasSelectedInteractable)
            {
                var triggerStrength = m_currentGrabInteractor.HandGrabApi.GetFingerPalmStrength(HandFinger.Index);
                isTriggerHeldThisFrame = triggerStrength > m_triggerStrengthThreshold;
```

## UI and Menus

The UI and Menus use direct touch and `RayInteractors` using the Interaction SDK.

The [`RayInteractor`](/documentation/unity/unity-isdk-ray-interaction/) is set up on left and right controllers or hands in the `AppInteractionController` class, defined in `/Assets/Discover/Scripts/AppInteractionController.cs`. These interactors are initialized and the controller meshes are loaded.

The [`IconController`](https://github.com/oculus-samples/Unity-Discover/blob/1938d235351723ef31c21b8f60b115a12467373c/Assets/Discover/Scripts/Icons/IconController.cs#L12) class, defined in `/Assets/Discover/Scripts/Icons/IconController.cs` manages highlighting icons and haptics when hovering over or selecting icons based on whether the ray is colliding with those icons.

For debugging purposes, the surfaces of all Scene objects like walls and desks have Ray interactables, so that a cursor appears when the input is moving over them. This can be used to check that the app knows where the real-life wall is.

### App placement flow

When selecting an app from the menu, the app must be placed in the player’s space. The [`AppIconPlacementController`](https://github.com/oculus-samples/Unity-Discover/blob/1938d235351723ef31c21b8f60b115a12467373c/Assets/Discover/Scripts/AppIconPlacementController.cs#L12) class, defined in `/Assets/Discover/Scripts/AppIconPlacementController.cs` controls the placement flow.

This is also where app placement is managed, using the `StartPlacement` function:

```
        public void StartPlacement(AppManifest appManifest, Handedness handedness)

        {
            m_isPlacing = true;
            m_isOnValidPlacement = false;
            m_handedness = handedness;
            m_selectedAppManifest = appManifest;

            m_iconRotation = appManifest.IconStartRotation;
            m_pinchAnchored = false;

            if (appManifest.DropIndicator != null)
            {
                m_indicator = Instantiate(appManifest.DropIndicator, m_appPlacementVisual.transform);
                m_indicator.SetActive(false);
            }

            m_appPlacementVisual.gameObject.SetActive(true);
        }
```

To create the indicator on the Scene elements, an `Indicator` is added:

```

                m_indicator = Instantiate(appManifest.DropIndicator, m_appPlacementVisual.transform);
```

This uses a `RayInteractor` to create a ray that has a filter on. It checks the Scene element to figure out what type of element the input is moving over.

For example, certain apps have to be on a desk. This is disabled in DroneRage, but it is useful when you want to query what type of object each one is.

## Locomotion

[Locomotion](/documentation/unity/unity-isdk-locomotion-interactions) for both controllers and hands has been reduced because Mixed Reality apps occur within a user’s space, unlike in larger virtual reality scenes where there is more space.

Discover uses standard controller locomotion, where using the joystick allows the user to move around and teleport. Hands use a gesture that is built using built-in prefabs from the Interaction SDK.