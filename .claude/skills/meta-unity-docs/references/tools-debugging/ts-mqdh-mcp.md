# Ts Mqdh Mcp

**Documentation Index:** Learn about ts mqdh mcp in this documentation.

---

---
title: "Enable AI tools with MCP for Meta Horizon OS developers"
description: "Connect AI assistants to Meta Quest development tools using Horizon Debug Bridge and MCP."
last_updated: "2026-02-27"
---

Horizon Debug Bridge (hzdb) is a combined CLI and Model Context Protocol (MCP) server that connects AI assistants and developers to Meta Quest development tools. It gives both you and your LLM access to documentation, device debugging, performance trace analysis, and Meta's 3D asset library. The same tools available through the MCP server are also accessible directly from the command line, so you can use them with or without an AI agent.

When you use hzdb through an AI agent, the MCP client requests the list of available tools from the server and forwards your query, along with tool metadata, to the LLM. The LLM determines which tools to call, the client invokes them through the server, and the results are sent back to the LLM to generate a response.

You can install and use hzdb in multiple ways, through:

- **Your editor (VS Code, Cursor, or Antigravity)** — install the Meta Horizon extension.
- **Meta Quest Developer Hub (MQDH)** — a standalone desktop app with a guided setup UI.
- **The `agentic-tools` plugin (Claude Code, Cursor, GitHub Copilot, and more)** — installs the hzdb MCP server and agentic skills in one step.
- **Node.js (`npx`)** — manual setup that works with any MCP-compatible agent.

<!-- VERIFIED: duplicate MCP server risk — SRC: xplat/horizon/plugins/vs-code/src/extension.ts:114, Task: T261477718 -->
**Note:** Each option registers its own MCP server. Using more than one at the same time will result in duplicate tool registrations in your AI agent. Choose one method that fits your workflow.

[Option D](#option-d-install-through-nodejs-npx) always pulls the latest version of hzdb. The other options bundle specific builds that may not include the newest tools yet.

<!-- VERIFIED: Options A-C don't add hzdb to PATH — SRC: xplat/horizon/plugins/vs-code/src/extension.ts:24-42 (bundled binary), agentic-tools .mcp.json uses npx at runtime -->
Options A, B, and C configure the MCP server for your AI agent but do not add hzdb to your terminal PATH. To use hzdb directly from the command line, see [Option D](#option-d-install-through-nodejs-npx).

No authentication is required to install or run the MCP server. Most tools work without credentials. Tools that require authentication will prompt you with instructions when you invoke them.

## Option A: Install hzdb through your editor

Open VS Code, Cursor, or Antigravity, go to the **Extensions** view and search for **Meta Horizon**. You can also install it directly from the [VS Code Marketplace](https://marketplace.visualstudio.com/items?itemName=meta.meta-vr-dev).

The extension bundles hzdb and registers the MCP server automatically. No additional configuration is needed.

## Option B: Install through MQDH

Meta Quest Developer Hub (MQDH) bundles the MCP server and provides a settings UI to configure your AI agent connection.

### 1. Download MQDH

If you don't have MQDH installed, see [Get started with Meta Quest Developer Hub](/documentation/unity/ts-mqdh-getting-started/), or download MQDH v6.2.1 or later for [Mac](https://developers.meta.com/horizon/downloads/package/oculus-developer-hub-mac/) or [Windows](https://developers.meta.com/horizon/downloads/package/oculus-developer-hub-win/).

To check your version, navigate to **Settings** > **About**.

### 2. Connect your agent

MQDH includes guided setup instructions for popular agents:

- Android Studio
- Augment Code
- Claude Code
- Claude Desktop
- Cline
- Cursor
- Gemini CLI
- JetBrains AI Assistant
- OpenAI Codex
- Roo Code
- Zed

Open the **AI Tools** tab in MQDH settings, select your agent from the dropdown, and follow the instructions. For agents not listed, select **Other** and consult your agent's MCP configuration docs.

#### VS Code, Cursor, and Antigravity quick configure

For VS Code, Cursor, and Antigravity users, MQDH provides a one-click install link that automatically configures the MCP server.

1. Go to **Settings** > **AI tools**.
2. Click **Add to VS Code**.

## Option C: Install through the `agentic-tools` plugin

The [meta-quest/agentic-tools](https://github.com/meta-quest/agentic-tools) plugin configures the hzdb MCP server and installs [agentic skills](#agentic-skills) in one step. No separate hzdb install is needed.

### Claude Code

```bash
claude plugin marketplace add meta-quest/agentic-tools
claude plugin install agentic-tools@meta-quest
```

This installs the plugin at the user level, so it's available across all your projects. No cloning needed.

To verify, run `claude plugin list` and confirm `agentic-tools@meta-quest` appears with status enabled. Restart Claude Code, then run `/mcp` and confirm `hzos-agentic-tools:hzdb` is connected.

### Cursor, GitHub Copilot, Gemini, Augment Code, and Roo Code

The same plugin works with Cursor, GitHub Copilot, Gemini, Augment Code, and Roo Code. See the [repository README](https://github.com/meta-quest/agentic-tools) for agent-specific installation instructions.

You can also use the [Vercel Skills CLI](https://github.com/vercel-labs/skills) to install the plugin into other compatible agents:

```bash
npx skills add meta-quest/agentic-tools
```

## Option D: Install through Node.js (`npx`)

If you prefer a lightweight setup, you can run the MCP server directly through Node.js.

### 1. Install hzdb

#### Prerequisites

- [Node.js](https://nodejs.org/) v18 or later
- npm (included with Node.js)
- [Optional] [ADB](https://developer.android.com/tools/adb). hzdb includes a built-in device communication library, so ADB is not required. Install ADB separately if you experience device connectivity issues.

#### Install

In your terminal, run:

```bash
npx @meta-quest/hzdb
```

This will download and run the hzdb CLI.

**Note:** Running `npx @meta-quest/hzdb` does not install hzdb globally. To use hzdb, prefix all commands with `npx` (for example, `npx @meta-quest/hzdb device list`). If you prefer a global install, run `npm install -g @meta-quest/hzdb` instead.

To verify the installation, run:

```bash
npx @meta-quest/hzdb --version
```

You should see the version number of the latest release.

### 2. Configure your agent

After installing hzdb, configure your AI agent to connect to the MCP server.

#### VS Code

1. In your project root, create the file `.vscode/mcp.json` (or open it if it already exists).
2. Paste the following configuration:

    ```json
    {
      "servers": {
        "meta-horizon-mcp": {
          "command": "npx",
          "args": ["-y", "@meta-quest/hzdb", "mcp", "server"]
        }
      }
    }
    ```

3. Restart VS Code or reload the window (`Cmd+Shift+P` > **Developer: Reload Window**) so VS Code picks up the new MCP server.

To verify the server can start, run this command in your terminal:

```bash
npx -y @meta-quest/hzdb mcp server
```

You should see the server start without errors. Press `Ctrl+C` to stop it.

#### Claude Desktop

Add the following to your Claude Desktop config file (`claude_desktop_config.json`):

```json
{
  "mcpServers": {
    "meta-horizon-mcp": {
      "command": "npx",
      "args": ["-y", "@meta-quest/hzdb", "mcp", "server"]
    }
  }
}
```

#### Other agents

For any MCP-compatible agent, configure a stdio transport with the command:

```bash
npx -y @meta-quest/hzdb mcp server
```

Consult your agent's documentation for the specific configuration format.

## Using hzdb as a CLI

You don't need an AI agent to use hzdb. The same tools available through the MCP server are accessible directly from the command line.

```bash
# Search documentation
npx @meta-quest/hzdb search-doc "passthrough rendering"

# Pull device logs
npx @meta-quest/hzdb logcat --lines 100 --level error

# Capture a screenshot from a connected Quest
npx @meta-quest/hzdb screenshot

# Search Meta's 3D asset library
npx @meta-quest/hzdb asset-search "realistic indoor furniture"
```

Run `npx @meta-quest/hzdb --help` to see all available commands.

## Verify your setup

After installing, try these prompts to confirm the MCP server is connected and working. Many tools work without a Quest device.

### Without a device

1. **Browse documentation** — "Search the documentation for passthrough rendering." You should receive a list of relevant documentation pages.
2. **Fetch a specific page** — Pick a page from the results and ask: "Fetch the documentation at [URL]." You should see the full page content returned.
3. **Search 3D assets** — "Find 5 realistic indoor furniture models." The MCP server returns download URLs and preview images from Meta's asset library.

### With a connected Quest

Connect your Meta Quest device via USB, then try:

4. **Pull device logs** — "Get 50 lines of logcat filtered to error level." This confirms ADB and device communication are working.
5. **Capture a screenshot** — "Take a screenshot from my connected Quest device."

If your agent returns tool results for these prompts, the MCP server is connected and ready to use.

<!-- NOTE: SRC shows 46+ actual MCP tools (54 with UI). This table is a curated subset. Missing: device mgmt (10), app mgmt (7), file mgmt (5), API refs (3), system (2), input (1), UI (8, feature-gated). Doc hedges with "may evolve". -->
## Available tools and capabilities

Once the MCP server is connected, you can use the following tools. Your AI agent discovers available tools and their parameters automatically through the MCP protocol, so the exact set of tools may evolve over time.

| **Tool Name**                        | **Description**                                                                           | **Key Parameters / Usage**                                                                                                                                                                        | **More Details / Links**                                           |
| ------------------------------------ | ----------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------ |
| fetch_meta_quest_doc                 | Retrieve the full content of a specific documentation page from Meta's Horizon platforms. | `url` (required): Full URL to the documentation page (should start with `https://developers.meta.com/horizon/llmstxt/documentation/`) | Use with `search_doc` to find and fetch specific pages.            |
| search_doc                           | Search across Meta Horizon OS documentation for relevant pages.                           | `query` (required): Search query describing the topic or feature you're looking for                                                                                                               | Use with `fetch_meta_quest_doc` to retrieve full page content.     |
| get_adb_path                         | Return the preferred ADB binary path for Meta Quest development.                          | None                                                                                                                                                                                              |                                                                    |
| get_device_logcat                    | Retrieve Android logcat logs from a connected Meta Quest device via ADB.                  | `lines`, `tag`, `level`, `package`, `pid`, `clear`                                                                                                                                                | Useful for debugging app crashes, performance, system events, and more. |
| stream_device_logcat                 | Stream real-time Android logcat logs from a connected Meta Quest device via ADB.          | `duration_seconds`, `tag`, `level`, `package`, `pid`                                                                                                                                              | Real-time device monitoring.                                       |
| take_screenshot                      | Capture a screenshot from a connected Meta Quest device via ADB.                          | `width`, `height`, `method`                                                                                                                                                                       | For debugging or documentation.                                    |
| hex_to_datetime                      | Convert a hexadecimal timestamp string to a datetime object (UTC).                        | `hex_str` (required): Hexadecimal string representing a Unix timestamp                                                                                                                            | Useful for interpreting trace timestamps.                          |
| load_trace_for_analysis              | Load the content of a Perfetto trace or processed JSON for analysis.                      | `session_id` (required): The file name of the trace                                                                                                                                               | For in-depth performance session analysis.                         |
| trace_thread_state                   | Retrieve thread state information from a Perfetto trace for performance analysis.          | `session_id` (required), `utid` (required), `start_ts`, `end_ts`                                                                                                                                  | Diagnose expensive operations or CPU activity.                     |
| run_sql_query                        | Run SQL query on the loaded trace to retrieve information in JSON format.                 | `session_id` (required), `query` (required)                                                                                                                                                       | Flexible trace data interrogation.                                 |
| get_counter_for_gpu_frames           | Return all GPU metric counters for given arrays of GPU frame start/end times.             | `session_id` (required), `start_ts` (array, required), `end_ts` (array, required)                                                                                                                 | At least 20 start/end times recommended for accurate metrics.      |
| get_perfetto_context                 | Retrieve the context needed for Perfetto or performance analysis workflows.               | None                                                                                                                                                                                              | Must be called before most Perfetto-related analysis.              |
| meta-assets-search                   | Search for existing 3D models in Meta's asset library using text descriptions.            | `prompt` (required), `number_of_models`                                                                                                                                                           | Returns download URLs for FBX/GLB and preview images.              |

### Using the performance trace tools

The Perfetto tools (`get_perfetto_context`, `load_trace_for_analysis`, `trace_thread_state`, `run_sql_query`, `get_counter_for_gpu_frames`) let you analyze performance traces captured from a Quest device. If you're new to Perfetto tracing, see [How to Take Perfetto Traces with Meta Quest Developer Hub](/documentation/unity/ts-perfettoguide/) to learn how to capture a trace before using these tools.

The typical workflow is:

1. Call `get_perfetto_context` to initialize the performance analysis context.
2. Call `load_trace_for_analysis` with the `session_id` (the trace file name) to load a captured trace.
3. Use `trace_thread_state`, `run_sql_query`, or `get_counter_for_gpu_frames` to query the loaded trace.

## Agentic skills

Skills are structured prompts that teach your AI agent how to perform Quest-specific tasks. They go beyond MCP tools. For example, `perfetto-debug` walks your agent through performance trace analysis, `unity-code-review` flags VR-specific code issues, and `vrc-check` validates Quest Store readiness requirements.

Skills work alongside the MCP server. The MCP server provides the tools (device access, documentation search, performance capture), and skills provide the workflows that use those tools effectively.

Skills are included in the [meta-quest/agentic-tools](https://github.com/meta-quest/agentic-tools) plugin. If you installed hzdb through [Option C](#option-c-install-through-the-agentic-tools-plugin), you already have them.

## Troubleshooting

### MQDH "Add to VS Code" button does nothing

- **Check that VS Code is installed.** The button opens a `vscode://` URI link. If VS Code is not installed, your system won't have a handler for this URI and nothing will happen. Install [VS Code](https://code.visualstudio.com/) first, then try again.
- **Check if VS Code opened in the background.** VS Code may launch but not appear on top of other windows. Check your taskbar or dock for the VS Code icon after clicking the button.
- **Try installing the extension manually.** Open VS Code, go to **Extensions** (`Cmd+Shift+X` on Mac, `Ctrl+Shift+X` on Windows), and search for **Meta Horizon**, or install it directly from the [VS Code Marketplace](https://marketplace.visualstudio.com/items?itemName=meta.meta-vr-dev). If the extension is not listed, use [Option D: Install through Node.js (`npx`)](#option-d-install-through-nodejs-npx) instead.

### MCP server won't start

- **Check your Node.js version.** The MCP server requires Node.js v18 or later. Run `node --version` to verify.
- **Check for npm errors.** If you see `npm error 404 Not Found` when running `npx -y @meta-quest/hzdb mcp server`, the package may not be available yet. Check the [@meta-quest/hzdb npm page](https://www.npmjs.com/package/@meta-quest/hzdb) to confirm it is published.
- **Validate your JSON configuration.** A missing comma or extra bracket in your agent's MCP config file will prevent the server from starting. Paste your config into a JSON validator if you're unsure.
- **Check the command path.** Make sure `npx` is available on your system PATH. Run `npx --version` to confirm.

### Skills not found

- **Check that the plugin is installed.** Verify the agentic-tools plugin is installed and enabled in your agent. For Claude Code, run `claude plugin list` and confirm `agentic-tools@meta-quest` appears with status enabled. For other agents, consult the [repository README](https://github.com/meta-quest/agentic-tools) for verification steps.
- **Restart your AI agent.** After installing or updating plugins, restart your agent so it picks up the new skills.

### Agent connects but device tools fail

- **Enable developer mode** on your Quest headset. Device tools require ADB access, which requires developer mode to be turned on.
- **Enable USB debugging.** In the headset, navigate to **Settings** > **System** > **Developer** and enable USB debugging.
- **Check your USB cable.** Use a USB-C data cable, not a charge-only cable. If the device isn't recognized, try a different cable or port.
- **Allow the connection.** When you first connect via USB, the headset prompts you to allow the computer. Put on the headset and accept the prompt.

### Documentation tools return errors

- **Check the URL format.** When using `fetch_meta_quest_doc`, the URL should start with `https://developers.meta.com/horizon/llmstxt/documentation/`. Use the `search_doc` tool first to find valid page URLs.

### Perfetto tools return errors

- **Call `get_perfetto_context` first.** This tool must be called before using other performance analysis tools like `load_trace_for_analysis`, `trace_thread_state`, or `run_sql_query`.