# Ts Ai Prompting Best Practices

**Documentation Index:** Learn about ts ai prompting best practices in this documentation.

---

---
title: "Prompting best practices"
description: "Best practices for crafting effective prompts when using AI-assisted development tools for Meta Quest."
last_updated: "2026-02-27"
---

This guide helps you write effective prompts when using AI tools connected through the [MCP (Model Context Protocol)](/documentation/unity/ts-mqdh-mcp/) for Meta Horizon OS developers. Better prompts lead to faster, more accurate results, whether you're querying documentation, debugging a device, analyzing performance, or searching for 3D assets.

## Core principles

### State your platform, SDK, and context upfront
The MCP server supports multiple SDKs and development targets. Specifying yours eliminates ambiguity and gets you relevant answers on the first try.

* Instead of: "How do I render passthrough?" \
Write: "How do I enable passthrough rendering in Unity using the Meta XR SDK on Quest 3?"
* Instead of: "Set up hand tracking" \
Write: "Walk me through implementing hand tracking with the Meta Spatial SDK for an Android-native Quest app."
* Instead of: "Fix my shader" \
Write: "My custom URP shader isn't rendering correctly on Quest 2. Here's the shader code: ..."

### Include the right amount of context
Give the LLM enough information to act without overloading it. Include what's relevant, leave out what isn't.

For debugging, include:
* The error message or logcat output
* What you were doing when it occurred
* Your target device and OS version
* The SDK and engine version you're using

For documentation discovery, include:
*  The specific feature or API you're asking about
*  Your development framework (Unity, Unreal, Native)

For performance analysis, include:
* The session ID or trace file name
* The time range or frame range you care about
* What metric or behavior you're investigating

### Use direct, imperative language
Tell the LLM what to do. Avoid hedging or open-ended phrasing when you know what you need.

* Instead of: "Can you maybe check the logs?" \
Write: "Pull the last 500 lines of logcat filtered to tag Unity at warning level and above."
* Instead of: "I was wondering about the docs" \
Write: "Search the documentation for scene anchors in Unity."
* Instead of: "Something seems slow" \
Write: "Get Perfetto context, then load trace session_42, query thread states between timestamps 1000000 and 5000000, and identify which threads spent the most time in runnable state."

##  Quick reference
* **Find docs:** "Search the documentation for passthrough in Unity."
* **Fetch a specific doc page:** "Fetch the documentation at https://developers.meta.com/horizon/llmstxt/documentation/unity/..."
* **Pull device logs:** "Get 200 lines of logcat filtered to package com.example.app at error level."
* **Stream live logs:** "Stream logcat for 20 seconds filtered to tag VrApi at debug level."
* **Capture a screenshot:** "Take a screenshot from my connected Quest device."
* **Initialize Perfetto:** "Get Perfetto context for performance analysis."
* **Load a trace:** "Load trace session_abc for analysis."
* **Query trace data:** "Run a SQL query on session_abc to find the longest slices on the main thread."
* **Analyze GPU frames:** "Get GPU counters for 30 frames starting at timestamp X in session_abc."
* **Search 3D assets:** "Find 5 realistic indoor furniture models."
* **Convert a timestamp:** "Convert hex timestamp 65a8b3c0 to a readable UTC datetime."