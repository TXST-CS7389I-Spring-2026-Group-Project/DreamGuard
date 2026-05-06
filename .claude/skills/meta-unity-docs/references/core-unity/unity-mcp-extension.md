# Unity Mcp Extension

**Documentation Index:** Learn about unity mcp extension in this documentation.

---

---
title: "Meta XR Unity MCP Extension"
description: "Set up the Meta XR Unity MCP Extension to modify Unity scenes by sending text prompts through your LLM."
last_updated: "2026-04-01"
---

<oc-devui-note type="note" markdown="block">
  This feature uses the Unity AI Gateway and Unity's MCP Server.
  The MCP Server, developed in collaboration with Meta, is available through [Unity's AI Gateway Early Access Beta](https://create.unity.com/UnityAIGatewayBeta).

  To complete this guide, you must be a member of Unity's AI Gateway Early Access Beta program to access the required downloads and documentation.
</oc-devui-note>

This guide summarizes the steps to set up the Unity MCP Server and Meta XR Unity MCP Extension so you can create and
modify Unity scenes using text prompts and AI agents.

Model Context Protocol (MCP) lets you connect AI agents, such as large language models (LLMs), with external services
and data sources. The Meta XR Unity MCP Extension is an MCP Server containing tools that control your Unity Editor and
project. AI services use an agentic shell with an MCP Client to access MCP Server tools.

The Unity MCP Server acts as a backend server that connects AI agents to Unity.

The Meta XR Unity MCP Extension lets Unity receive and run commands from the MCP Server in Meta XR projects.

After configuring the extension, you can submit text prompts through your AI agent to command the MCP Server to
perform tasks such as:

- Creating, updating, or deleting a GameObject
- Moving or rotating GameObjects using relative transforms
- Adding an interaction to make a GameObject grabbable
- Creating teleport hotspots

After completing this step-by-step guide, you should be able to:

- Add the Meta XR Unity MCP Extension to your project
- Set up Unity MCP Server with one of the supported AI agents
- Modify your scene by sending text prompts through your AI agent

## Prerequisites

Before proceeding with this tutorial, complete the setup steps outlined in [Unity Hello World for Meta Quest VR headsets](/documentation/unity/unity-tutorial-hello-vr/)
to create a project with the necessary dependencies.

### Account requirements

- Membership to Unity's AI Gateway Early Access program: Apply on [Unity's AI Gateway Beta sign-up form](https://create.unity.com/UnityAIGatewayBeta).
- An account on an AI service that supports MCP Servers. For a list of compatible services, see [Unity's
  AI Gateway documentation](https://docs.unity3d.com/Packages/com.unity.ai.assistant@2.0/manual/ai-gateway-get-started.html).

### Software requirements

<!-- vale off -->
- Unity Editor 2022.3.15f1 or later (6.1 or later recommended)
  
  
  

<!-- vale on -->

- Meta XR SDK packages v78 or later

## Instructions

### Step 1: Install the Unity MCP package

---

<oc-devui-note type="warning" markdown="block">
  Without access to Unity's AI Gateway beta program, you cannot access the documentation and files necessary to complete this step and guide.

  For instructions on how to install the Unity MCP package, see [Unity's AI Gateway documentation](https://docs.unity3d.com/Packages/com.unity.ai.assistant@2.0/manual/ai-gateway-get-started.html).
</oc-devui-note>

### Step 2: Install the Meta XR Unity MCP Extension

---

<oc-devui-note type="note">
  The Meta XR Unity MCP Extension is currently only available on GitHub. Functionality might vary by version.
</oc-devui-note>

To install the extension from GitHub:

1. Open the Package Manager by selecting **Window** > **Package Management** > **Package Manager**.

2. Click the **+** (plus sign) to show install options and then select **Install package from git URL...** from the top left.

   {:width="416px"}

3. In the text input area, enter `https://github.com/meta-quest/Unity-MCP-Extensions.git` and then click **Install**.

In the Package Manager, select **In Project** on the left and verify that the Meta XR Unity MCP Extension appears in the list.

{:width="600px"}

### Step 3: Install and start the agentic shell

---

**Note:** Without access to Unity's AI Gateway beta program, you cannot access the documentation and files necessary
to complete this step and guide.

For instructions on installing an AI agent's agentic shell with the Unity MCP server, see [Unity's AI Gateway documentation](https://docs.unity3d.com/Packages/com.unity.ai.assistant@2.0/manual/ai-gateway-get-started.html).

After installing the agentic shell and adding the Unity MCP server to it, click **Accept** in the Connection Request
dialog that appears in Unity Editor, as shown below:

{:width="600px"}

**Note:** If the dialog does not appear, see [Troubleshooting](#troubleshooting) for steps to manually accept the connection.

### Step 4: Send text prompt commands

---

From the MCP Server provider UI, try submitting the following text prompts to
update your scene:

- Add a cube with distance grab enabled
- Set up ray and poke interactions for all UI elements
- Add a teleport hotspot at the center of the scene

## Troubleshooting

### The MCP Connection Request dialog does not appear after starting the LLM MCP server or the LLM throws an error when attempting to update the project.

Navigate to **Project Settings** > **MCP Server** > **Other Connections**. This displays a list of all the attempted
connections and their acceptance or rejection status. For any rejected connection, expand the **Connection Details**
section, verify the details match your connection attempt, and then click **Accept**.

### The extension crashes Unity or behaves unexpectedly.

To view or leave feedback about unexpected behavior of the extension, navigate to the [Meta Quest Developer Feedback Center](/feedback/)
and click **View investigations** in the VR section.