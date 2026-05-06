# Unity Ai Providers

**Documentation Index:** Learn about unity ai providers in this documentation.

---

---
title: "Providers and Inference Types"
description: "Configure cloud, local server, and on-device providers to control where AI inference runs in your app."
last_updated: "2025-12-11"
---

<box height="5px"></box>

## Learning Objectives

- Understand how **Providers** define _where_ AI inference runs (Cloud, Local,
  or On-Device).
- Learn how to configure and authenticate **Cloud Providers** like OpenAI, Llama
  API, Hugging Face, and ElevenLabs.
- Connect **Local Providers** such as Ollama for LAN-based, private inference.
- Set up **On-Device Providers** with the Unity Inference Engine for offline
  on-device execution.
- Learn about the **Provider Installation Routine** and
  **RemoteProviderProfileRegistry** to import Providers during setup.

<box height="10px"></box>

**AI Building Blocks** separate _what you run_ (**the Agent**) from _where it
runs_ (**the Provider**). A **Provider** is a Unity `ScriptableObject` that
performs inference through a **Cloud**, **Local Server**, or **On-Device**
backend using the **Unity Inference Engine**.

A **Provider** handles **provider-specific input/output handling and
formatting** to support the **Agent**'s focus on core logic. **Agents**
therefore should be Provider-agnostic and work with any Provider that supports
the same task through a task interface, for example `IObjectDetectionTask`.

<box height="10px"></box>

## Inference Types

| Inference Type | Description                                                                                           | Typical Use                                                                              |
| -------------- | ----------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------- |
| **Cloud**      | Sends text, audio, or image payloads to a hosted model over HTTPS and returns results.                | Fastest way to prototype using the latest models (LLMs, TTS/STT, DETR).                  |
| **Local**      | Communicates with a model running on a local machine in the same Wi-Fi network (for example, Ollama). | Low latency, private demos, exhibitions.                                                 |
| **On-Device**  | Runs the model directly on the headset via Unity Inference Engine.                                    | Lowest latency, full privacy, and no network dependency. Significant performance impact. |

<box height="10px"></box>

## Cloud Providers

Cloud Providers are the easiest way to get started: Just create a **Provider
asset**, paste your **API key**, and enter the **endpoint/model** you want to
use.

<box height="5px"></box>
<oc-devui-note type="important" heading="Always check provider and model availability">
Providers and models may not always be available on the provider's servers.
Always check provider and model availability before using them in your
experience. </oc-devui-note> <box height="5px"></box>

| Provider (Asset)        | Common Models / Capabilities                                                          | Editor Features                                                             |
| ----------------------- | ------------------------------------------------------------------------------------- | --------------------------------------------------------------------------- |
| **LlamaApiProvider**    | Official Meta Llama family (chat and multimodal variants)                             | Curated model list with automatic vision toggling.                          |
| **OpenAIProvider**      | `gpt-5`, `gpt-4o` (chat/vision), `whisper-1` (STT), `tts-1`, `tts-1-hd` (TTS)         | Model picker, Chat/Vision toggle, STT/TTS configuration foldouts.           |
| **HuggingFaceProvider** | Any Hugging Face-hosted model (for example, `facebook/detr-resnet-101`, Llama family) | Token validator, endpoint health checker, image inlining options.           |
| **ReplicateProvider**   | Community-hosted models (`owner/model[:version]`)                                     | Endpoint override, base64/data URI support, inline byte cap.                |
| **ElevenLabsProvider**  | Text-to-Speech and Speech-to-Text (Scribe)                                            | Fetches voices, models, and metadata directly from your ElevenLabs account. |

<box height="10px"></box>

### Setting Up a Cloud Provider

1. **Create a Provider Asset**:
   `Create → Meta → AI → Provider Assets → <Cloud>/<Your Provider>`
2. **Enter API Key**: Use the **Get Key…** button in the Inspector to open your
   provider’s developer portal.
3. **Set Endpoint and Model**: Copy these from the provider's example `curl`
   request.
4. **Click Validate / Check**: Confirm authentication and connectivity on your
   provider's website or the asset itself.
5. **(Optional) Configure Vision Options**: Adjust settings like _Inline Remote
   Images_, _Resolve Redirects_, and _Max Inline Bytes_.

<box height="5px"></box>
<img src="/images/unity-ai-building-blocks-create-cloud-provider-asset.png" alt="Create AI Cloud Provider" width="100%" border-radius="12px" />

<box height="10px"></box>

## Local Server Providers

| Provider (Asset)   | Backend                                  | Description                                                                                                                  |
| ------------------ | ---------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- |
| **OllamaProvider** | Ollama daemon (`http://localhost:11434`) | Discovers installed models via `/api/tags` and lets you select local tags (for example, `llama3`, `llava:latest`, `gemma3`). |

### Connecting to Ollama

1. **Run Ollama** on your local machine:
   - `ollama pull llama3`
   - `ollama serve`
2. In Unity, open your `OllamaProvider` asset and configure:
   - **Model
     Endpoint:**&nbsp;<a href="http://localhost:11434/api/generate">http://localhost:11434/api/generate</a>
   - **Model:**&nbsp;`llama3` or another installed tag
3. Click **Refresh Models** to fetch available tags, then press **Play** to test
   the connection.

<box height="10px"></box>

## On-Device Providers

| Provider (Asset)                 | Runtime                         | Highlights                                                                                                                      |
| -------------------------------- | ------------------------------- | ------------------------------------------------------------------------------------------------------------------------------- |
| **UnityInferenceEngineProvider** | Unity Inference Engine (Sentis) | Supports GPU/CPU backends, optional _Split Over Frames_ for smoother performance, and GPU-based NMS compute shader integration. |

### Quick Setup

1. **Create** →
   `Create → Meta → AI → Provider Assets → On-Device → Unity Inference Engine`
2. **Assign your model file** (`.onnx` or `.sentis`)
3. **Configure Backend:**&nbsp;`GPUCompute` or `CPU`
4. **Adjust Split Over Frames / Layers Per Frame** for performance tuning
5. _(Optional)_ Add **Class Labels** via a `.txt` file
6. _(Optional)_ Enable **GPU Non-Max Suppression (NMS)** using the provided
   compute shader

For model conversion and performance tuning, see
[Unity Inference Engine](/documentation/unity/unity-ai-unity-inference-engine/).
For more information about Unity Inference Engine, see
[Unity's official documentation](https://docs.unity3d.com/Packages/com.unity.ai.inference@2.3/manual/index.html).

<box height="10px"></box>

## Provider Selection During Building Block Installation

<box height="5px"></box>
<img src="/images/unity-ai-building-blocks-create-install-object-detection.png" alt="Object Detection Block" width="100%" border-radius="12px" />

The **RemoteProviderProfileRegistry** automatically retrieves configuration
files from Meta's CDN containing official Provider profiles and defaults
(endpoints, model names, and so on). When adding a new AI Building Block from
**Meta Hub → Building Blocks**:

- The installer detects all available inference types for that block.
- It loads compatible Providers from the **RemoteProviderProfileRegistry**.
- You choose your preferred inference type (Cloud, Local, or On-Device).
- The selected Provider asset is saved with your prefab or component.
- You can later edit it directly in the Inspector to update models, endpoints,
  or keys.

<box height="20px"></box>

<p>
  → <strong>Next: </strong>
  <a href="/documentation/unity/unity-ai-unity-inference-engine/">Unity Inference Engine</a>
</p>