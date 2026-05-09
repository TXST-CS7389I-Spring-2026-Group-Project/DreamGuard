# DreamGuard: Comparing Mixed Reality Safety Systems in VR

**CS 7389I · Texas State University**
Andrew Scouten · Avery R Vanausdal

![Unity](https://img.shields.io/badge/Unity-6000.4.5f1-black?logo=unity) ![Python](https://img.shields.io/badge/Python-3.12-blue?logo=python&logoColor=white)

---

## Overview

DreamGuard is a within-subjects study platform comparing four passthrough safety techniques on Meta Quest. The core question is whether camera-aware or depth-aware passthrough systems can maintain safety awareness while disrupting immersion less than Meta's default Guardian grid.

The study runs on a dungeon maze task across four rooms, each using a different passthrough technique triggered at a fixed point in gameplay.

## Research Motivation

Current VR safety systems (e.g., Meta Guardian) are boundary-aware: they respond to where the user is relative to a predefined play area, not to what is actually in the physical space. DreamGuard treats this as a design space question: can object-aware or depth-aware passthrough techniques improve on the boundary-aware grid — preserving more immersion without sacrificing safety?

## Study Conditions

| Room | Condition | Trigger | Aware of |
|------|-----------|---------|----------|
| 1 | Guardian Grid (Meta default) | Proximity to play-area boundary | Boundary only |
| 2 | Window | Always-on, forward-facing | What's directly in front |
| 3 | Window + Detection | YOLO object recognition | Anything in camera view |
| 4 | Depth Bubble | Meta Depth API threshold | Anything in depth sensor range |

Passthrough implementations: `src/dreamguard/unity/Runtime/Passthrough/`

## Study Design

Four dungeon rooms, each with 4 collectible orbs. Collecting the 2nd orb activates the room's passthrough technique for 20 seconds, then deactivates. A HUD arrow guides the player to the next orb; a counter displays progress. The player cannot advance to the next room until all 4 orbs are collected.

**Conditions 3 and 4** use reactive sub-triggers (YOLO detection; depth threshold) within the 20-second window to determine when to show passthrough. **Condition 2** (Window) is always-on for the full window.

**Design note:** Conditions are presented in fixed order — not counterbalanced. Order effects (fatigue, practice, contrast) are the study pilot's primary internal validity limitation. Counterbalancing is identified as future work.

### Dependent Variables

- Safety awareness, system confidence — perceived protection
- Presence, disruption — immersion cost
- Trust, false alarm perception — reliability
- Trigger comprehension — whether the system's activation logic is intelligible
- Perceived trigger timing — correlates with objective `TRIGGER.ts − ROOM_HALFWAY.ts`
- Forced rankings and stated preference
- Qualitative (post-session interview)

## Platform

- **Device**: Meta Quest (USB, via Meta Quest Link for development)
- **SDK**: Meta XR/MR SDK, OpenXR, Unity OpenXR Meta
- **Rendering**: Passthrough via Meta's Passthrough API
- **Depth**: Meta's Depth API (active)
- **Engine**: Unity

## Study Logging

Session data is written to device storage by `StudyLogger` (`Runtime/Logging/StudyLogger.cs`):

```
/sdcard/Android/data/com.DefaultCompany.DreamGuard/files/experiments/study_N/
```

Pull after a session:

```bash
adb pull /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/experiments
```

See [`docs/LOGGING.md`](docs/LOGGING.md) for the full schema (event types, CSV columns, key timestamp formulas).

## Known Issues

See [`docs/KNOWN_ISSUES.md`](docs/KNOWN_ISSUES.md).

**"DreamGuard Not Responding" on launch:** The app requires the headset to be worn to initialize. If the Quest is on a desk when the app launches, it may show a not-responding dialog. Workaround: wear the headset before initiating Build and Run, or relaunch from the library.

## Third-Party Assets

See [`docs/THIRD_PARTY.md`](docs/THIRD_PARTY.md).

- **KayKit — Dungeon Remastered**: 3D dungeon tileset used for environment art
- **Unity PassthroughCameraApi Samples**: Meta's official passthrough camera API reference
