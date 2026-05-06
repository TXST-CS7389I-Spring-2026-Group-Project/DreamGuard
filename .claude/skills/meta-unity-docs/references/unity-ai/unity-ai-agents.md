# Unity Ai Agents

**Documentation Index:** Learn about unity ai agents in this documentation.

---

---
title: "Agents and Building Blocks"
description: "Learn how each AI Building Block Agent works and how to connect them into modular, multimodal AI workflows."
last_updated: "2025-12-16"
---

<box height="5px"></box>

## Learning Objectives

- Understand how **Agents** coordinate runtime data flow and events in AI
  Building Blocks.
- Learn how **Providers** handle model inference across Cloud, Local, and
  On-Device backends.
- Connect multiple Agents to build **modular**, **event-driven**, and
  **multimodal** AI pipelines.
- Combine vision, speech, and language Agents to create interactive XR
  experiences.
- Extend or customize existing Agents using inheritance and UnityEvents without
  modifying Providers.

<box height="10px"></box>

**Agents** are Unity components that bring core AI capabilities — such as object
detection, natural language processing, and speech synthesis — into XR
applications. Each Agent coordinates **runtime data flow** between your scene
and an **inference Provider** (Cloud, Local, or On-Device).

Providers define _how_ inference runs and how input/output is formatted, while
Agents handle _when and where_ data is captured, processed, and dispatched in
Unity.

<box height="10px"></box>

## Overview of Available Building Blocks

| Building Block                 | Agent(s)                                            | Purpose                                                               |
| ------------------------------ | --------------------------------------------------- | --------------------------------------------------------------------- |
| **Object Detection**           | `ObjectDetectionAgent`, `ObjectDetectionVisualizer` | Detects and tracks objects in passthrough or camera textures.         |
| **Large Language Model (LLM)** | `LlmAgent`, `LlmAgentHelper`                        | Manages text and multimodal chat using GPT, Llama, or similar models. |
| **Speech-to-Text (STT)**       | `SpeechToTextAgent`                                 | Converts user speech or audio clips into text.                        |
| **Text-to-Speech (TTS)**       | `TextToSpeechAgent`                                 | Generates natural-sounding voice from text.                           |

All four Building Blocks, which are made up of four agents and their helper
classes, share a consistent interface and can be combined to form complex,
multimodal pipelines.

<box height="10px"></box>

## Object Detection

<box height="5px"></box>
<img src="/images/unity-ai-building-block-object-detection.png" alt="Object Detection" width="100%" border-radius="12px" />
<box height="5px"></box>

<oc-devui-note type="important" heading="Possible Bounding Box Misalignment">We
have a fix ready for the upcoming release that will take into account different
resolutions and aspect ratios of the Passthrough Camera. If you are currently
experiencing an offset on your bounding boxes, please set the resolution on your
<b>PassthroughCameraAccess</b> component to <b>x: 1280, y: 960</b>, which was
previously the default resolution.</oc-devui-note>

### Components

- **ObjectDetectionAgent**: Runs inference and returns structured detection
  data.
- **ObjectDetectionVisualizer**: Renders bounding boxes or 3D meshes in the
  scene.

### Data Flow

1. The Agent captures a frame from `PassthroughCamera`.
2. It sends the texture to the assigned Provider (for example,
   `UnityInferenceEngineProvider` or `HuggingFaceProvider`).
3. The Provider returns detection results (bounding boxes, class labels,
   confidence scores).
4. The Visualizer draws the results in world space.

### How to place object detections in 3D space

When importing the Object Detection Building Block with visualizer, the
visualizer will also import the **DepthTextureAccess** class. This provides the
visualizer with depth data to turn 2D detections into real-world 3D results.

1. **EnvironmentDepthManager**: Retrieves per-eye environment depth textures
   from the Meta Quest system and makes them available globally for other
   components.
2. **DepthTextureAccess**: Reads these depth textures from the GPU (via
   `EnvironmentDepthManager`), copies them into a CPU-accessible array, and
   exposes per-frame depth data.
3. **ObjectDetectionAgent**: Uses the 2D bounding boxes from its model output
   and maps them to world-space using the latest depth information from
   `DepthTextureAccess`.
4. **ObjectDetectionVisualizer**: Renders 3D bounding boxes or markers in the
   scene at the world-space positions provided by `ObjectDetectionAgent`.

An alternative and still valid solution is to use the
**EnvironmentRaycastManager** component to shoot a ray from the camera pixel
into world space to find the distance to the object. However, the AI Building
Blocks do not use this solution as it can get imprecise and expensive when done
each frame and for each point of the bounding box. Furthermore, if the user's
head moves between inference and output retrieval, the raycast may hit the wrong
point, as the user might not have the object in their frustum anymore.

### Example

<box height="5px"></box>

<pre><code class="language-csharp">agent.OnDetectionsUpdated += (detections) =>
{
    foreach (var d in detections)
        Debug.Log($"Detected {d.label} at {d.box}");
};
</code></pre>

<box height="10px"></box>

## Large Language Model (LLM)

<box height="5px"></box>
<img src="/images/unity-ai-building-block-llm.png" alt="Large Language Models" width="100%" border-radius="12px" />
<box height="5px"></box>

### Components

- **LlmAgent**: Manages text or multimodal (image and text) conversation flow.
- **LlmAgentHelper**: Connects LLMs/VLMs with speech agents (STT/TTS) or other
  custom logic.

### Data Flow

1. `SendPrompt()` forwards user input to the Provider (Llama API, OpenAI,
   Ollama, and so on).
2. The Provider streams text tokens asynchronously.
3. The Agent emits `OnResponseReceived` and `OnStreamUpdate` events.
4. Other Agents (like Text to Speech) can subscribe to stream updates for
   immediate feedback.

### Example

<box height="5px"></box>

<pre><code class="language-csharp">speechToTextAgent.OnTranscript += llmAgentHelper.SendPrompt;
llmAgent.OnResponseReceived += textToSpeechAgent.SpeakText;
</code></pre>

<box height="5px"></box>

### Multimodal Support

Providers such as **Llama 4 Maverick**, **OpenAI GPT-4o**, or **Claude 4 Sonnet
on Replicate** models support image input. When a Provider implements
`IChatTask`, textures are automatically encoded and sent alongside text prompts.

<box height="10px"></box>

## Speech-to-Text (STT)

<box height="5px"></box>
<img src="/images/unity-ai-building-block-stt.png" alt="Speech to Text" width="100%" border-radius="12px" />
<box height="5px"></box>

### Components

- **SpeechToTextAgent**: Captures microphone or audio clip input.
- Works with Providers implementing `ISpeechToTextTask`.

### Data Flow

1. The Agent records audio or receives an `AudioClip`.
2. Audio is encoded and sent to the Provider (`OpenAI`, `ElevenLabs`).
3. The Provider transcribes it to text and triggers `OnTranscript`.

### Example

<box height="5px"></box>

<pre><code class="language-csharp">speechToTextAgent.OnTranscript += (string transcript) =>
{
    Debug.Log($"User said: {transcript}");
};
</code></pre>

<box height="5px"></box>

### Features

- Supports **real-time** or **clip-based** transcription.
- Some Providers offer **streaming** recognition for live captions.
- Optional **language code** field for multilingual setups.

<box height="10px"></box>

## Text-to-Speech (TTS)

<box height="5px"></box>
<img src="/images/unity-ai-building-block-tts.png" alt="Text to Speech" width="100%" border-radius="12px" />
<box height="5px"></box>

### Components

- **TextToSpeechAgent**: Converts text into audio clips using
  `ITextToSpeechTask` Providers.

### Data Flow

1. The Agent receives text via `SpeakText()`.
2. It sends the request to the Provider (for example, ElevenLabs, OpenAI TTS).
3. The Provider returns or streams an `AudioClip`.
4. The Agent plays it automatically or exposes it for custom playback.

### Example

<box height="5px"></box>

<pre><code class="language-csharp">llmAgent.OnResponseReceived += textToSpeechAgent.SpeakText;
</code></pre>

<box height="10px"></box>

## Agent Events Summary

| Event                             | Raised By              | Description                                                                   |
| --------------------------------- | ---------------------- | ----------------------------------------------------------------------------- |
| `onDetectionResponseReceived`     | `ObjectDetectionAgent` | Invoked after a detection pass with the processed `BoxData` list.             |
| `OnBoxesUpdated`                  | `ObjectDetectionAgent` | C# event fired when boxes are updated; used by `ObjectDetectionVisualizer`.   |
| `onPromptSent`                    | `LlmAgent`             | Raised when a user prompt is dispatched to the provider.                      |
| `onResponseReceived`              | `LlmAgent`             | Raised when a full assistant response is received.                            |
| `onImageCaptured`                 | `LlmAgent`             | Raised when a passthrough or debug image is captured for a multimodal prompt. |
| `onTranscript`                    | `SpeechToTextAgent`    | Emits the recognized transcript after STT completes.                          |
| `onClipReady`                     | `TextToSpeechAgent`    | Fired when a synthesized `AudioClip` is ready for playback.                   |
| `onSpeakStarting`                 | `TextToSpeechAgent`    | Fired just before playback, passing the text that will be spoken.             |
| `onSpeakFinished`                 | `TextToSpeechAgent`    | Fired when the `AudioSource` finishes or playback is stopped.                 |

### Notes

- **MRUK-only events** (like `OnBoxesUpdated`) are wrapped in compile guards and
  only available if MRUK is installed and the Passthrough Camera is available.
- `LlmAgentHelper` connects existing events (`onPromptSent`,
  `onResponseReceived`) for simplified wiring but doesn't introduce new ones.

<box height="10px"></box>

## Wiring Agents Together

All Agents use a unified public event-driven architecture. You can connect them
directly through the Unity Inspector or your custom logic.

### Example 1: Conversational Chain

<box height="5px"></box>

<pre><code class="language-plaintext">[Microphone Input] → SpeechToTextAgent → LlmAgent → TextToSpeechAgent → [Audio Output]</code></pre>

<box height="10px"></box>

### Example 2: Vision + LLM Hybrid

<box height="5px"></box>

<pre><code class="language-plaintext">[Camera Frame] → ObjectDetectionAgent → LlmAgent (context injection) → TextToSpeechAgent</code></pre>

<box height="10px"></box>

## Extending Agents

Developers can subclass any Agent to add new behavior such as gesture input,
haptic feedback, or custom UI updates.

<pre><code class="language-csharp">public class MyCustomAgent : ObjectDetectionAgent
{
    // Override HandleResults to inject custom logic after detection processing
    // Use this pattern to add haptic feedback, audio cues, or custom UI updates
    protected override void HandleResults(List&lt;Detection&gt; results)
    {
        // Call base implementation first to ensure proper detection handling
        base.HandleResults(results);

        // Add custom behavior that responds to detection results
        foreach (var detection in results)
        {
            if (detection.confidence > 0.8f)
            {
                // Trigger haptic feedback, play sound, or update custom UI
                Debug.Log($"High confidence detection: {detection.label}");
            }
        }
    }
}
</code></pre>

<box height="10px"></box> Because Agents are Provider-agnostic, switching from a
**cloud model** to an **on-device model** only requires assigning a new Provider
asset — no code changes.

<box height="20px"></box>

<p>
  → <strong>Next: </strong>
  <a href="/documentation/unity/unity-ai-add-new-provider/">Adding New Providers</a>
</p>