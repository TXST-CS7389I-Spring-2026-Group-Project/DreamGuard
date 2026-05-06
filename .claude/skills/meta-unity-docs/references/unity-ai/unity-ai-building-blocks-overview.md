# Unity Ai Building Blocks Overview

**Documentation Index:** Learn about unity ai building blocks overview in this documentation.

---

---
title: "AI Building Blocks - Overview"
description: "AI Building Blocks provide drag-and-drop AI components for adding voice, vision, and language features to Unity apps on Meta Quest."
last_updated: "2025-12-11"
---

<box height="10px"></box>
<img src="/images/unity-ai-building-block-banner.png" alt="AI Building Blocks Banner" width="100%" border-radius="12px" />
<box height="10px"></box>

**AI Building Blocks** bring plug-and-play machine-learning capabilities
directly into Unity-based XR projects for Meta Quest. Each block combines a
Unity **Agent** (runtime logic) with a configurable **Provider** (defining where
inference runs, A. in the cloud, B. on a local machine, or C. on the Meta Quest
device). Providers from options A and B currently connect through HTTP requests
due to a lack of native Websocket/WebRTC support in Unity.

Also, working with HTTP is simpler for prototyping and testing, but it is less
performant than native Websocket/WebRTC, so be aware of increased latency when
trying to build real-time experiences using cloud or local inference through
HTTP ports. For this reason we will focus more on building out on-device
inference support with the
[Unity Inference Engine](/documentation/unity/unity-ai-unity-inference-engine)
in the future.

<box height="10px"></box>

## Core AI Building Blocks

AI Building Blocks offer modular functionality across four key categories:

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" border-radius="12px">
    <img src="/images/unity-ai-building-block-object-detection.png" alt="Object Detection" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Object Detection</heading>
    <p>Detect and label real-world objects in passthrough or camera textures using on-device or cloud models.</p>
    <a href="/documentation/unity/unity-ai-agents#object-detection">View Documentation</a>
  </box>

  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" border-radius="12px">
    <img src="/images/unity-ai-building-block-llm.png" alt="Large Language Models" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Large Language Models</heading>
    <p>Integrate contextual or multimodal AI using Llama, or custom models through any Provider.</p>
    <a href="/documentation/unity/unity-ai-agents#large-language-model-llm">View Documentation</a>
  </box>
</box>

<box display="flex" flex-direction="row" justify-content="space-between" margin-top="24">
  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" border-radius="12px">
    <img src="/images/unity-ai-building-block-stt.png" alt="Speech to Text" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Speech to Text (STT)</heading>
    <p>Transcribe microphone or audio-clip input in real time using state-of-the-art cloud models.</p>
    <a href="/documentation/unity/unity-ai-agents#speech-to-text-stt">View Documentation</a>
  </box>

  <box width="48%" display="flex" flex-direction="column" align-items="flex-start" border-radius="12px">
    <img src="/images/unity-ai-building-block-tts.png" alt="Text to Speech" border-radius="12px" width="100%" />
    <box height="16px" />
    <heading type="title-small-emphasized">Text to Speech (TTS)</heading>
    <p>Generate natural-sounding voice output using state-of-the-art ElevenLabs or OpenAI models.</p>
    <a href="/documentation/unity/unity-ai-agents#text-to-speech-tts">View Documentation</a>
  </box>
</box>

<box height="10px"></box>

## System Recommendations

- **Unity 6 or newer**
- **Meta Quest 3 or 3s**
- **Meta XR Core SDK v83+** and **Meta XR MR UtilityKit v83+** (for Passthrough
  Camera support)
- Stable internet connection when running cloud providers

<box height="10px"></box>

## Typical Use Cases

<box height="5px"></box>

<oc-devui-note type="important" heading="Always check provider and model availability">
  We do our best to provide you with state-of-the-art providers and up-to-date models, but especially for cloud providers, models may not always be available on the provider's servers. Therefore, always make sure to check the provider and model availability before using them in your experience.
</oc-devui-note>

| Category     | Example                                   | Recommended Provider                                  |
| ------------ | ----------------------------------------- | ----------------------------------------------------- |
| **Vision**   | Real-time object detection                | Unity Inference Engine / HuggingFace                  |
| **Language** | Language and Vision Requests to LLMs/VLMs | Llama API / OpenAI / Ollama / HuggingFace / Replicate |
| **Speech**   | Voice commands or narration (TTS / STT)   | OpenAI / ElevenLabs                                   |

<box height="10px"></box>

## Architecture Overview

Each AI Building Block consists of two core layers:

| Layer        | Role                                                                          | Examples                                                                                  |
| ------------ | ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- |
| **Agent**    | Unity runtime component managing input/output and inference calls.            | `ObjectDetectionAgent`, `LlmAgent`, `SpeechToTextAgent`, `TextToSpeechAgent`              |
| **Provider** | `ScriptableObject` defining the inference backend and input/output structure. | `OpenAIProvider`, `HuggingFaceProvider`, `OllamaProvider`, `UnityInferenceEngineProvider` |

<oc-devui-note type="note" heading="Example">
<p>Prototype using <strong>Llama 4 Maverick</strong> (Llama API), then switch to <strong>Llama 3.3</strong> running on <strong>Ollama</strong>, or an on-device model, without changing your experience’s logic.</p>
</oc-devui-note>

<box height="10px"></box>

## Continue Learning

- [Providers and Inference Types](/documentation/unity/unity-ai-providers):
  Configure Cloud, Local, and On-Device inference.
- [Unity Inference Engine](/documentation/unity/unity-ai-unity-inference-engine):
  Run optimized models directly on Meta Quest hardware.
- [Agents and Building Blocks](/documentation/unity/unity-ai-agents): Use Object
  Detection, LLM, STT, and TTS components.
- [Adding New Providers](/documentation/unity/unity-ai-add-new-provider): Extend
  the system with custom backends and Editors.
- [Troubleshooting and FAQ](/documentation/unity/unity-ai-faq): Resolve setup
  issues and see answers to frequently asked questions.

<box height="20px"></box>

<p>
  → <strong>Next: </strong>
  <a href="/documentation/unity/unity-ai-providers/">Providers and Inference Types</a>
</p>