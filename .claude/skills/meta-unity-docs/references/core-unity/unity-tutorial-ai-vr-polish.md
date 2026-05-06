# Unity Tutorial Ai Vr Polish

**Documentation Index:** Learn about unity tutorial ai vr polish in this documentation.

---

---
title: "Add AI-built shaders and spatial audio to a Unity Meta Quest VR game"
description: "Add dissolve shaders, energy blade effects, runtime mesh slicing, spatial 3D audio, and advanced haptics to your VR game."
last_updated: "2026-04-20"
---

Your game plays. Now make it look and feel polished. This tutorial uses AI to unlock techniques you'd normally skip: shader code, procedural mesh generation, and custom haptic waveforms. You add custom URP shaders (dissolve effect and energy blade), runtime mesh slicing so fruit splits along your actual swing, spatial 3D audio with per-fruit sounds, and advanced haptic patterns. Each step tackles something most Unity developers avoid because the implementation is complex—but describing what you want is easy.

## What you need before starting

- Completed [Build a fruit slicing Meta Quest VR game in Unity with AI](/documentation/unity/unity-tutorial-ai-vr-game), with Claude Code and Unity MCP already configured (from that tutorial).
- Quest connected through USB-C (you test on device after each visual step).
- (Optional) [hzdb CLI](/documentation/unity/ts-mqdh-mcp) installed (`npx @meta-quest/hzdb`) for live device logs while iterating on shaders and audio.

**Prompts:** Open a terminal in your Unity project folder and run `claude`. Send every prompt below in that session—Claude controls Unity directly through the MCP connection.

## Step 1: Dissolve shader for sliced fruit

Right now, sliced fruit just disappears. With AI, you can make it dissolve outward from the slice point, as if it's burning away. Implementing this manually requires Shader Graph node wiring or raw HLSL with noise textures, alpha cutoff, and emission edge glow—typically several minutes of tweaking.

### Prompt

```text
Make sliced fruit dissolve instead of just disappearing. It should burn away from the slice point over about 1.5s, with a realistic fire edge that is irregular, flickering, with a color gradient from white-hot to orange to charred black. The fruit should keep drifting gently while dissolving, not freeze in place. Keep the particle burst too with dissolve and particles together.
```

**Verify:** Sliced fruit dissolves over ~1.5s instead of vanishing instantly, and the orange edge glow is visible in the editor. Build to Quest (**File** > **Build and Run**) and confirm the dissolve looks right in VR—edge glow visible, timing that feels natural.

**If pink:** "The dissolve shader is pink. Rewrite it for URP."

**If no animation:** "The dissolve isn't triggering. Check that it is connected to the slice event."

**If unrealistic:** "The burn effect looks too clean. Make it more irregular and flickery."

## Step 2: Energy blade shader with animated weapon effect

Upgrade the existing blade into a glowing energy sword that pulses and reacts to your swing speed. Implementing animated shaders manually requires time-based UV scrolling, noise displacement, and script-driven emission—both C# and shader code working together.

### Prompt

```text
Replace the blade with a glowing energy sword. Flowing energy pattern along the blade, and it should react to how fast I am swinging—barely glowing when still, blazing bright during fast swings.
```

**Verify:** The blade glows with a flowing energy pattern in Play mode in the Unity Editor. Build to Quest (**File** > **Build and Run**) and check that the blade reacts to your swing speed (bright when fast, dim when still) with no pink materials.

**Tip:** Keep `npx @meta-quest/hzdb log --tag Unity` running in a separate terminal to catch runtime errors.

**If invisible or black:** "The energy blade shader isn't rendering. Check blend mode and culling."

## Step 3: Runtime mesh slicing for fruit splitting

This is the hardest feature in the tutorial. Instead of just dissolving, the fruit first splits into two halves along the exact plane of your blade swing, then each half dissolves—real geometry, with no pre-made halves. Implementing this manually is 300 to 500 lines of geometry code: plane-mesh intersection, vertex generation, cap face triangulation, and normal recalculation.

### Prompt

```text
When I slice a fruit, I want it to actually split into two halves along my swing angle with real geometry, not pre-made halves. Each piece should float apart briefly, then fall and dissolve. Also upgrade the slice particles to look juicier with soft splashes, streaks, and little juice droplets that arc with gravity.
```

**Verify:** Press Play in the editor to confirm the halves appear, then build to Quest (**File** > **Build and Run**). Slice fruits at different angles—try slicing diagonally. Two pieces should fly apart along the cut line, showing "flesh" inside, then dissolve mid-air.

**If stuttering:** "Add object pooling and limit simultaneous sliced pairs."

**If halves look wrong:** "Recalculate normals and check triangle winding order."

**If too complex:** "Fall back to the simpler approach. Spawn two pre-made half-sphere meshes oriented along the blade's swing plane. Apply the dissolve shader to both halves."

## Step 4: Spatial audio with 3D sound positioning

Make the audio exist in 3D space so you can hear where fruits are before you see them. Implementing this manually requires configuring spatial blend, rolloff curves, Doppler, parameterized procedural audio, and custom directional audio scripting—lots of trial and error to get right at room-scale VR distances.

### Prompt

```text
Make all the audio spatial. Each fruit type should sound different when sliced: watermelon is a big wet splash, coconut is a dry crack, banana is a soft squish. Add a directional chime when fruits spawn so I can tell where they are coming from with my eyes closed. A whoosh when a fruit flies past me unsliced. And a special chime that circles around my head on combo milestones.
```

**Verify:** Spatial audio works correctly only on Quest, so build and test there (**File** > **Build and Run**) and play with headphones for the best effect. Close your eyes—you should be able to point to where a fruit is spawning before you see it, just from the audio cue.

**Note:** If you used colored spheres instead of Kenney Food Kit models, adjust the prompt to describe sounds by color ("red is a sharp pop, blue is a deep thud") instead of fruit names. The spatial audio features work the same either way.

**If silent on Quest:** "Check that AudioListener is on centerEyeAnchor and AudioSources aren't destroyed before they finish playing."

## Step 5: Advanced haptic patterns

Replace basic vibration with custom haptic waveforms that communicate game state through your hands. Most developers settle for a single buzz; designing distinct patterns for different events takes iterative tuning.

### Prompt

```text
Add custom haptic feedback so every game event feels different through the controllers. Slicing should feel like cutting through something with a quick double-tap at high frequency. Missing is a dull low rumble. Combo milestones feel like power building up with a few pulses getting stronger. A near miss (fruit within arm's reach) is a brief light buzz in whichever hand was closer. Game over feels like energy draining away with pulses getting weaker and slower. Make sure patterns can't step on each other.
```

**Verify:** Haptics only work on Quest, so build to Quest (**File** > **Build and Run**) and confirm each event feels distinctly different—slice is a buzz, miss is a low rumble, combo is multiple bursts, and game over is an energy drain.

**If nothing:** "Haptics aren't firing. Check that the pre-built waveform buffer is being played, and that nothing else is blocking the haptic channel."

## Troubleshooting

| Problem | Fix |
|---------|-----|
| Shader appears pink on Quest | Ask Claude to rewrite for URP. Run **Edit** > **Rendering** > **Materials** > **Convert Built-in Materials to URP**. |
| Dissolve doesn't start from slice point | Check `_SlicePoint` is in world space, not local space. |
| Mesh slicing causes frame spike | Add object pooling. Limit active sliced pairs. |
| Spatial audio sounds wrong | `AudioListener` must be on `centerEyeAnchor`, not a separate camera. |
| Haptics don't play | Haptics require Oculus runtime. Test on Quest only, not editor or XR Simulator. |
| Energy blade too bright | Reduce emission intensity. Adjust URP Volume bloom threshold. |
| Cap faces have wrong normals | Run `RecalculateNormals()` and check triangle winding order. |
| Need device logs | `adb logcat -s Unity` for live logs, `npx @meta-quest/hzdb log --tag Unity` for recent logs. |

You added five features that would each take an experienced Unity developer hours to build from scratch: a dissolve shader with HLSL and a C# driver, an animated energy blade shader, runtime mesh slicing with real geometry, spatial 3D audio with per-fruit sounds, and custom haptic patterns. With AI, you described what you wanted and had it running in minutes. Features that were previously "not worth the time" become trivial to add—shaders, mesh manipulation, and spatial audio go from specialist skills to describe-and-deploy.