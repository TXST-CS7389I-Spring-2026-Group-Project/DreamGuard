# Ts Ai Tooling Overview

**Documentation Index:** Learn about ts ai tooling overview in this documentation.

---

---
title: "AI tooling overview"
description: "Explore AI-powered development tools available for building Meta Horizon OS applications."
last_updated: "2026-02-27"
---

AI-powered tooling is transforming how developers build for Meta Horizon OS. By integrating large language models (LLMs) directly into your development workflow, you can query documentation instantly, debug device issues in real time, search Meta's 3D asset library with natural language, and analyze performance traces all without leaving your editor.

## Why accelerate with AI?

Building for spatial computing involves working across multiple SDKs, device-specific debugging workflows, and performance-sensitive rendering pipelines. Traditional development cycles require context-switching between documentation portals, ADB commands, trace analysis tools, and asset pipelines.

AI tooling collapses these steps. Instead of navigating docs manually, you describe what you need. Instead of writing ADB commands from memory, you ask. Instead of browsing asset catalogs, you search with plain language. The result is less friction, faster iteration, and more time spent building.

## What's available

Two components work together to bring AI-powered development to Meta Quest:

### hzdb (Horizon Debug Bridge)

A CLI and MCP server that gives you and your AI agent direct access to Quest development tools:

* **Documentation retrieval** — Query Unity, Unreal Engine, Meta Spatial SDK, Android, Native C++, and Web development docs for Meta Quest through natural language.
* **Device debugging** — Pull logcat logs, stream real-time device output, capture screenshots, and filter by tag, level, package, or PID on connected Meta Quest devices.
* **Performance analysis** — Load Perfetto traces, query thread state, run SQL against trace data, and extract GPU frame metrics for profiling sessions.
* **3D asset discovery** — Search Meta's asset library for existing 3D models using text descriptions to accelerate prototyping and content creation.
* **Timestamp conversion** — Convert hexadecimal timestamps from trace data to human-readable dates and times. This is useful for correlating events across different logs and traces.

## Agentic tools and skills

The [meta-quest/agentic-tools](https://github.com/meta-quest/agentic-tools) GitHub repository provides a collection of ready-made skills that onboard your AI agent to build for Meta Horizon OS. These skills contain distilled knowledge from Meta's developer documentation and from Meta engineers who have solved common problems developers encounter when building for the platform — from converting Android apps to Horizon OS to diagnosing jank with Perfetto.

Skills are structured prompts that teach your AI agent how to perform Quest-specific workflows. They go beyond individual MCP tools by guiding multi-step processes:

* **Performance debugging** (`perfetto-debug`) — Walks your agent through capturing, loading, and analyzing a Perfetto trace.
* **Code review** (`unity-code-review`) — Flags VR-specific issues like single-pass rendering settings, fixed foveated rendering, and common performance pitfalls.
* **Store readiness** (`vrc-check`) — Validates Quest Store requirements before submission.
* **Project creation** (`new-project-creation`) — Scaffolds a new project for Unity, Unreal, Web, or Android.
* **XR Simulator setup** (`xr-simulator-setup`) — Configures the simulator for testing without a headset.
* **Spatial SDK** (`spatial-sdk`) — Guides development with the Meta Spatial SDK.
* **VR debugging** (`vr-debug`) — Systematic approach to diagnosing common VR issues.

## Getting started

The fastest way to get both hzdb and agentic skills is the [meta-quest/agentic-tools](https://github.com/meta-quest/agentic-tools) plugin. It bundles the hzdb MCP server configuration and skills in one install — no separate setup needed.

It works with Claude Code, Cursor, GitHub Copilot, Gemini CLI, and other MCP-compatible agents. See the [repository README](https://github.com/meta-quest/agentic-tools) for the full list and agent-specific installation instructions.

You can add the skills to your project with a single command using your tool's built-in skills support. See the [repository README](https://github.com/meta-quest/agentic-tools) for setup instructions specific to your tool.