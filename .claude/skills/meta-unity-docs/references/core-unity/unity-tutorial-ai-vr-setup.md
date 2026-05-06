# Unity Tutorial Ai Vr Setup

**Documentation Index:** Learn about unity tutorial ai vr setup in this documentation.

---

---
title: "Set up Unity for AI-powered Meta Quest VR development"
description: "Build a complete VR game for Meta Quest using AI-assisted development. Start by setting up a Quest-ready Unity project with VR configuration, Unity MCP, and Meta XR Extension."
last_updated: "2026-04-20"
---

This tutorial series takes you from an empty Unity project to a VR game running on your Meta Quest, using AI at every step. You'll build a fruit slicing game from scratch and add custom dissolve shaders, runtime mesh slicing, an animated energy blade, spatial 3D audio, and haptic feedback.

The workflow is straightforward: you describe what you want in plain language, your AI assistant writes the code and applies it to your Unity project, and you test on Quest. Prompts in this series stay short because the AI inherits your platform rules from a project context file you set up in the next tutorial.

## Tutorial overview

| # | Tutorial | What you build |
|---|----------|----------------|
| 1 | Set up Unity for Meta Quest VR (this page) | Full AI-native VR development toolkit and Quest-ready Unity project |
| 2 | [Build a fruit slicing Meta Quest VR game in Unity with AI](/documentation/unity/unity-tutorial-ai-vr-game) | Fruit slicing slash game with spawning, blades, scoring, and difficulty |
| 3 | [Add AI-built shaders and spatial audio to a Unity Meta Quest VR game](/documentation/unity/unity-tutorial-ai-vr-polish) | Dissolve shaders, energy blade, mesh slicing, spatial audio, and haptics |

Follow the tutorials in order. Each one builds on the previous tutorial.

## What you need before starting

- Unity 6.3 (6000.3.x+) with Android Build Support (SDK, NDK, OpenJDK).
- Meta Quest with [developer mode enabled](https://developers.meta.com/horizon/documentation/native/android/mobile-device-setup/).
- Node.js 18+ installed (check with `node --version`; if not installed, download from nodejs.org).
- Access to Claude Code (or another AI tool of your choice).
- (Optional) [hzdb](/documentation/unity/ts-mqdh-mcp) and the [meta-quest/agentic-tools](https://github.com/meta-quest/agentic-tools) repo set up.

**Note:** This tutorial series uses Claude Code as the AI assistant for every example, but the same workflow works with any AI coding tool (Cursor, Codex, GitHub Copilot, Windsurf, Cline, and others). Substitute your agent of choice when you see Claude Code referenced; the prompts work with any agent.

## What if Claude gets it wrong

AI tools sometimes get things wrong. When that happens, paste the error message into Claude and describe what you saw: "The shader is pink," "Fruits spawn behind me," or "It doesn't compile." Each tutorial in the series includes troubleshooting tips for common issues, and the workflow is always the same: prompt, check, and adjust.

## Step 1: Create a Unity 6.3 project

1. Open **Unity Hub** > **New Project**.
2. Select **Universal 3D template**. URP (Universal Render Pipeline) is [recommended for Quest development](https://developers.meta.com/horizon/documentation/unity/unity-project-setup/).
3. Name it (for example, `VRSlashGame`) and click **Create project**.

Wait for the project to fully open before continuing.

**Verify:** The Unity 6 Editor opens with a default URP scene.

## Step 2: Simplified VR setup

With your Unity project open:

1. Download <file-link handle="GIKbUB20iWBsXpAEANzQH6TZyskMbosWAACi">Simplified VR Setup Package</file-link>.
2. Go to **Assets** > **Import Package** > **Custom Package** > select the file > **Import All** (Mac) or **Open** (Windows). If Unity warns that the script needs to be updated for the latest 6.4 versions, click **Yes, for these and other files that might be found later**.
3. Go to **Meta** > **Simplified VR Setup** > **Run Setup**.

This package automates the [Unity VR project setup guide](https://developers.meta.com/horizon/documentation/unity/unity-project-setup/), including the platform switch, XR packages, OpenXR configuration, and scene setup. It also installs the Meta XR SDK that the Meta XR MCP Extension depends on. During setup, Unity prompts you to make changes to your scene, switch platforms, accept manifest changes, and import packages—accept each dialog to continue. On Windows, you may need to restart your computer to switch the build platform; the setup resumes automatically.

When the setup finishes, an optional installation window appears for additional Meta SDKs.

**Note:** This tutorial does not require you to install any additional SDKs from the optional SDKs window.

**Verify:** Meta Quest platform active, OVRCameraRig in hierarchy, no console errors.

## Step 3: Set up Unity MCP

Unity MCP gives Claude direct control of the Unity Editor: it can read the scene, modify objects, run play mode, and inspect components. This is the foundation of the "vibe coding" workflow—you describe what you want in plain language, and Claude builds it directly in your Unity project. Without it, you'd have to copy and paste Claude's code manually. Install the Unity AI Assistant first, then add the Meta XR Extension.

### Unity AI Assistant (official beta)

1. Request access to [Unity's AI Gateway Early Access program](https://create.unity.com/UnityAIGatewayBeta). Without this access, you cannot use the Unity MCP.
2. Install the Unity MCP package by following [Unity's AI Gateway documentation](https://docs.unity3d.com/Packages/com.unity.ai.assistant@2.0/manual/unity-mcp-get-started.html).
3. Enable the Claude Code integration: in the Unity Editor, go to **Edit** > **Project Settings** > **AI** > **Unity MCP** and click **Configure** next to Claude Code. For a different AI coding tool, choose the appropriate integration option from the list. If you have trouble configuring your client, see the [Unity documentation](https://docs.unity3d.com/Packages/com.unity.ai.assistant@2.0/manual/unity-mcp-get-started.html) on manual configuration.
4. Start Claude Code from your project folder: `cd ~/VRSlashGame && claude`.
5. If a pop-up appears to accept a pending MCP connection, click **Accept**. If it does not appear automatically, open Unity and go to **Edit** > **Project Settings** > **AI** > **Unity MCP**, then click **Accept** on the pending connection.

**Note:** If Unity MCP gives you trouble, you can still complete the tutorials. Skip the Unity MCP commands and use Claude Code directly for code generation. The prompts work the same way, but you paste Claude's output into Unity manually instead of letting Claude control the editor.

### Meta XR MCP Extension

**Note:** This section builds on the Meta XR SDK installed during Step 2: Simplified VR setup.

1. In the Unity Editor, go to **Window** > **Package Management** > **Package Manager** > **+** > **Install package from git URL**.
2. Paste the following URL and click **Install**:

   ```text
   https://github.com/meta-quest/Unity-MCP-Extensions.git
   ```

3. Open a terminal, navigate to your Unity project folder, and run `claude`.

The Meta XR Extension adds Quest-specific tools on top of Unity MCP, including OVR components, Meta XR SDK integration, and Quest build settings.

**Verify:** Ask Claude, "Can you see my Unity scene?" Claude should describe the scene hierarchy. Then ask Claude, "Can you create a cube in front of the camera and make it change color between red and blue when I press play?" If you don't see the cube when you press Play, ask, "Can you play and test it? I don't see the cube." Claude will iterate live—changing the code, replaying the scene, and fixing the issue.

## Step 4: Build to Quest

This first build caches the Android toolchain so subsequent builds are fast. In the Unity Editor, go to **File** > **Build and Run**, name your `.apk` file, and save.

The first build takes a few minutes (IL2CPP compilation, Gradle setup, signing keys). Subsequent builds are incremental and finish in around one to two minutes. Unity locks the editor during builds, so it's best to run the first build now, before you start adding game logic.

**Verify:** A VR scene loads on your Quest, you can see the cube changing color, and you can look around with the headset.

## Troubleshooting

| Problem | Fix |
|---------|-----|
| hzdb command not found | Run `npx @meta-quest/hzdb` again, or add to PATH. |
| `npx @meta-quest/hzdb device list` shows nothing | Check USB-C connection, developer mode enabled, and Quest is awake. If device shows as "unauthorized," put on the headset and accept the USB debugging prompt. Select "Always allow from this computer." |
| MCP server won't connect | Restart Claude Code after config changes. |
| Unity MCP not connecting or tools missing | Check the relay binary exists at `~/.unity/relay/`. Restart Claude Code. Accept pending connection in Unity: **Edit** > **Project Settings** > **AI** > **Unity MCP**. If two MCP entries appear in `/mcp`, disable the one without tools. |
| Skills not found | Run `claude plugin list` to check if agentic-tools is installed and enabled. If not, re-run `claude plugin install agentic-tools@meta-quest`. |
| Simplified setup errors | Check Unity console. You may need to close and reopen Unity after package import. |
| Build fails | Ensure Android Build Support is installed in Unity Hub (SDK, NDK, and OpenJDK all checked). |
| Black screen on Quest | Re-run Simplified VR setup, then go to **Meta** > **Tools** > **Project Setup Tool** > **Fix All**. |

Your toolkit is ready. Head to [Build a fruit slicing Meta Quest VR game in Unity with AI](/documentation/unity/unity-tutorial-ai-vr-game) to start building.