# Unity Tutorial Ai Vr Game

**Documentation Index:** Learn about unity tutorial ai vr game in this documentation.

---

---
title: "Build a fruit slicing Meta Quest VR game in Unity with AI"
description: "Build a VR fruit slicing game with spawning, blades, scoring, and difficulty using AI-assisted development in Unity."
last_updated: "2026-04-20"
---

With your development environment set up, it's time to build the game. In this tutorial, you build a fruit slicing VR game: objects arc upward, you swing the controllers to slice them, particles burst on contact, and your score tracks combos and lives. Miss three fruits and the game ends.

**Note:** If you finished Tutorial 1 with the test cube still in your scene, ask Claude to remove it and reset the scene to empty before starting this tutorial.

## What you need before starting

- Completed [Set up Unity for AI-powered Meta Quest VR development](/documentation/unity/unity-tutorial-ai-vr-setup) with tools installed, Unity project Quest-ready, and build cache warmed up.
- (Optional) [Kenney Food Kit](https://kenney.nl/assets/food-kit) downloaded. If skipped, the tutorial can use colored spheres.
- (Optional) [Meta XR Simulator](https://developers.meta.com/horizon/documentation/unity/xrsim-getting-started/) installed to play and test without the headset for quick iteration.

**Note:** VR input (swings, triggers, and haptics) only works on Quest hardware or the Meta XR Simulator. When you press Play in the Unity Editor, check that the code compiles and the objects appear. Full gameplay testing happens later in the tutorial.

## How prompts work in these tutorials

Each prompt below is text you send to Claude. To set it up:

1. Open a terminal (**Terminal** on Mac, **PowerShell** on Windows).
2. Navigate to your Unity project folder.
3. Run `claude` to start a Claude Code session.
4. Paste the prompt text into Claude Code.

With Unity MCP configured (from [Tutorial 1: Set up Unity for AI-powered Meta Quest VR development](/documentation/unity/unity-tutorial-ai-vr-setup#step-3-set-up-unity-mcp)), Claude generates code and applies it directly to your open Unity project. Keep Claude Code running in this terminal for the entire tutorial; each prompt builds on the previous one.

## Step 1 (optional): Import assets

In the Unity Editor, drag the unzipped folder you downloaded from [Kenney Food Kit](https://kenney.nl/assets/food-kit) into the Assets panel. If you skip this step, the prompts below work with primitive shapes.

## Step 2: Set up project context for your AI tool

Before writing any game code, give your AI tool context about this VR project: the rendering pipeline, the camera rig, and shader rules. Every prompt then inherits the same constraints automatically, so you can say "add a glowing blade" instead of "add a glowing blade using a URP-compatible shader with emission."

It also keeps Claude consistent across steps. Without project rules, you might get a Standard shader in one step and URP in another, leaving you to debug pink materials. Think of it as onboarding a teammate: you explain the project setup once, and every conversation after that focuses on the work itself.

### Claude Code

Claude Code uses a `CLAUDE.md` file at your project root as a context file. It reads this file automatically at the start of every session, so every prompt inherits the project rules without restating them each time. Set it up:

1. Download our recommended <file-link handle="GLO8wh2SpBCVKyYFAH5zeUsA4wQ9bosWAADk">CLAUDE.md</file-link> template for this VR project.
2. Place the file at the root of your Unity project (for example, `~/VRSlashGame/CLAUDE.md`).
3. Restart your Claude Code session so it picks up the new file.

**Verify:** In your Claude Code session, ask "What rendering pipeline does this project use?" Claude should answer URP without prompting.

### Other AI tools

Most AI coding tools support project-level context files. Create an equivalent file with the same content. Use `.cursor/rules/*.mdc` for Cursor, `agents.md` for Codex, `.github/copilot-instructions.md` for GitHub Copilot, `.windsurfrules` for Windsurf, or `.clinerules` for Cline.

## Step 3: Set up game architecture

Before building any mechanics, set up the core architecture. This first prompt creates the skeleton—event system, managers, and input tracking—so every mechanic you add next plugs into a clean foundation.

**Note:** Why architecture first? Without this step, you'd build the spawner, blade, and scoring as isolated scripts, then have to wire them together later. With the event system in place, each mechanic you add automatically communicates with the others: the spawner listens for `OnGameStart`, the slash detector fires `OnFruitSliced`, and the score UI reacts to `OnComboChanged`. Cleaner code, fewer bugs.

### Prompt

```text
Set up the game architecture for a fruit slicing-style VR slash game. I need a GameManager that tracks score, lives, and combo, an event system so components can talk to each other (game start, game over, fruit sliced, fruit missed, combo changed), and an InputManager that tracks controller velocity for slash detection. No gameplay yet, just the foundation. Create the GameObjects in the scene and attach the components.
```

**Verify:** Scripts compile with no errors. GameManager, GameEvents, and InputManager exist in the scene hierarchy.

## Step 4: Fruit spawner

This is the most specific prompt in the series because spawning physics is where AI tools tend to guess wrong—arc height, spawn position, and drift range. Spelling out explicit constraints ("arm height," "below the player," "5 seconds") up front saves you from debugging later.

### Prompt

```text
Create a FruitSpawner. Spawn fruit in front of the player every 1.5 seconds in a wide arc. If Kenney Food Kit models are in Assets, use those, otherwise colored spheres. Fruits launch upward from below the player with a little random drift, then fall back down with gravity. Fruits should arc up to arm height before falling back down. Each fruit should take a slightly different path. Unsliced fruits disappear after 5 seconds and fire OnFruitMissed. The spawner starts and stops with OnGameStart/OnGameOver. Start spawning immediately in Play mode so I can test before the full game flow is wired up. Each fruit needs a trigger collider and kinematic Rigidbody for slash detection.
```

**Verify:** Press Play. Fruits spawn, arc upward, fall back down, and disappear after 5 seconds.

**If wrong:** Tell Claude what is off, such as "fruits spawn behind me," "arc is too low," or "the fruits are too tiny and too far away."

## Step 5: Blade on controllers

Claude already knows the architecture from Step 3 and the input system from the `GameManager`. You just need to tell it what to attach and where. The URP shader reminder is included because AI tools tend to default to the Standard shader otherwise.

### Prompt

```text
Add a glowing blade to each controller. Each blade needs a trigger collider for slash detection and a kinematic Rigidbody. Use the InputManager's velocity tracking to decide if a swing is fast enough to count as a slash. Make sure all materials use URP shaders, no Shader.Find(), no Standard shader.
```

**Verify:** Press Play. Blade components should appear on both hand anchors, and scripts should compile. Blade visuals and swing detection require VR input, so test on Quest.

**If wrong:** "No blades visible" usually means a missing material. "Blades on wrong objects" usually means a hand anchor path is wrong. Ask Claude to fix it.

## Step 6: Slash detection and effects

This prompt connects two things that already exist (blades and fruits) and adds feedback. The `isSliced` flag callout prevents a common bug where one swing registers multiple hits on the same fruit.

### Prompt

```text
Add slash detection. When a blade hits a fruit at speed, fire OnFruitSliced, spawn a burst of colorful particles at the hit point that match the color of the fruit that is slashed, trigger a quick haptic tap on that controller, and destroy the fruit. Prevent double-hits with the isSliced flag.
```

**Verify:** Scripts compile and the particle prefab exists. Full slash testing requires controller velocity, so test on Quest.

**If wrong:** "Trigger not firing" usually means a collider/Rigidbody mismatch. "Fruits are hard to hit" usually means colliders are too small. Ask Claude to increase them.

## Step 7: Score UI and game flow

The event system from Step 3 does the heavy lifting here. Claude just needs to know what to display and how to start and restart. This is the payoff of architecture-first: the UI "just works" by listening to events.

### Prompt

```text
Add a floating score UI in front of the player. Show a title screen "VR SLASH" with press A or X to start. Display score, lives, and combo multiplier. The UI listens to GameManager events. Same button press to restart on game over.
```

**Verify:** The UI canvas appears with title text, score, lives, and combo elements. Button input requires a controller, so test on Quest.

**If wrong:** "UI not visible" usually means the canvas is out of view. Ask Claude to reposition it. "Text too small" usually means fonts are too small. Ask Claude to increase them.

## Step 8: Difficulty and polish

This prompt stacks several changes at once: difficulty curve, environment, audio, and blade visual. Each one is small, so bundling them is fine. If Claude's output feels overwhelming, break the prompt into separate ones.

### Prompt

```text
Add difficulty, environment, and audio. Fruits should spawn faster over time and multiple at once after 30 seconds. Combos multiply your score: 2x at 5 combo, 3x at 10+, and reset on miss. Build a simple environment around the player with a ground plane and some background scenery. Add a satisfying slash sound when you cut a fruit, with each fruit type sounding a little different based on what kind of fruit it is. Add gentle calming melodies as background music. Replace the plain blade with something that looks like an actual weapon.
```

**Verify:** The game gets harder over time, the combo counter works, the environment is visible, music plays, and the slash sound triggers.

**If wrong:** "Ground is too high" usually means the ground is above eye level. Ask Claude to lower it. "Fruits are all below me and not random" usually means the spawn positions need adjusting. "The slash sound is metallic" usually means the audio needs more depth. Ask Claude to make it juicier.

**Note:** If you're using colored spheres instead of Kenney Food Kit models, Claude generates different sounds per color rather than per fruit type. The result is the same: variety in slash feedback.

## Step 9: Build and play

### Build

**Option A: Unity Build and Run**

1. In the Unity Editor, press **Ctrl+S** (Mac: **Cmd+S**) to save.
2. Go to **File** > **Build and Run**.

**Option B: Build with the hzdb CLI**

**Note:** This option requires the [hzdb CLI](/documentation/unity/ts-mqdh-mcp). Install it with `npx @meta-quest/hzdb` if you have not already.

1. In the Unity Editor, press **Ctrl+S** (Mac: **Cmd+S**) to save.
2. Go to **File** > **Build Settings** > **Build**. Unity prompts for a save location—create a `Builds` folder in your project and save the file as `SlashGame.apk`.
3. In your terminal, deploy the build: `npx @meta-quest/hzdb app install ./Builds/SlashGame.apk`.
4. Launch the app: `npx @meta-quest/hzdb app launch <package name>`.

Find your package name in **Edit** > **Project Settings** > **Player** > **Other Settings** > **Package Name**.

### Play

Put on your Quest headset. Press A or X to start. Slash fruit. Beat your high score.

**Bonus:** In your terminal, explore the docs (requires the [hzdb CLI](/documentation/unity/ts-mqdh-mcp) from earlier):

```bash
npx @meta-quest/hzdb docs search "Quest controller input"
```

The hzdb CLI exposes the full Meta Quest docs from the command line. It's useful when you want to extend the game or understand an API without leaving your workflow.

## Quick-fix prompts

- "Fruits are too hard to hit": increase fruit and blade collider sizes.
- "Game is too easy": start spawn interval at 1.0s, minimum 0.3s.
- "UI is hard to read": double the font sizes and add a dark background panel behind the text.
- "I want sound effects": add slash and miss sound effects using AudioSource.

## Troubleshooting

| Problem | Fix |
|---------|-----|
| Can't hit fruits | Ask Claude to increase collider sizes. |
| No blades visible | Check blade mesh has a material with emission. |
| UI not visible | Ask Claude to reposition canvas in front of player. |
| Button press not working | Ask Claude to check OVRInput button mapping. |
| Build fails | Ensure Android Build Support is installed in Unity Hub. |
| Black screen on Quest | Re-run Simplified VR setup. |
| Haptics not working | Normal. Only works on Quest hardware, not in editor. |
| Low frame rate | Ask Claude to reduce particle count. |
| Pink materials | Ask Claude to convert materials to URP. |

You've built a complete VR game: an architecture-first foundation, physics-based spawning, motion-tracked blades, collision detection with particles and haptics, event-driven scoring with combos, and progressive difficulty—all running on Quest.

In the [next tutorial](/documentation/unity/unity-tutorial-ai-vr-polish), you add features that would normally take hours to build by hand: dissolve shaders, runtime mesh slicing, spatial audio, and advanced haptics.