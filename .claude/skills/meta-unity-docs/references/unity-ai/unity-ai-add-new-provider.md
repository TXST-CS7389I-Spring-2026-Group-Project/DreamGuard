# Unity Ai Add New Provider

**Documentation Index:** Learn about unity ai add new provider in this documentation.

---

---
title: "Adding New Providers"
description: "Create and register custom AI providers that integrate with the Meta AI Building Blocks architecture."
last_updated: "2025-12-11"
---

<box height="5px"></box>

## Learning Objectives

- Understand how **Providers** serve as modular inference backends for AI
  Building Blocks.
- Learn to create **custom Providers** that integrate seamlessly with existing
  Agents.
- Design intuitive **Editor interfaces** for Provider configuration and
  validation.
- Implement **custom network transports** (for example, WebSocket or gRPC) for
  real-time or low-latency inference.
- Extend or adapt **Agents** to add contextual or multimodal behavior with
  minimal code changes.

<box height="10px"></box>

The **AI Building Blocks** framework is **provider-agnostic** and fully
extensible. You can integrate new inference backends, custom transports, or
unique model types simply by implementing standard task interfaces. This design
allows your Unity Agents (LLM, Object Detection, STT, TTS) to remain unchanged
while switching to any compatible model or service.

<box height="10px"></box>

<oc-devui-note type="note" heading="Before You Begin">
  Review existing Providers (Llama API, OpenAI, HuggingFace) before creating your own. Use them as templates. All Providers follow the same task interface patterns and are discovered automatically at install time.
</oc-devui-note>

<box height="10px"></box>

## Architecture Recap

Each AI Building Block consists of two layers:

| Layer                           | Role                                                                                                             |
| ------------------------------- | ---------------------------------------------------------------------------------------------------------------- |
| **Agent (MonoBehaviour)**       | Handles Unity-side orchestration: captures input, triggers inference, and dispatches results.                    |
| **Provider (ScriptableObject)** | Defines the backend — how and where inference happens (Cloud, Local, On-Device). Defines input/output structure. |

By creating a new **Provider**, you can integrate:

- A custom REST API or WebSocket inference service.
- A new model type (for example, segmentation, diffusion, emotion recognition).

<box height="10px"></box>

## Core Interfaces

Every Provider implements one or more of the following interfaces:

| Interface              | Purpose                                               |
| ---------------------- | ----------------------------------------------------- |
| `IChatTask`            | Defines text or multimodal LLM chat tasks.            |
| `IObjectDetectionTask` | Handles object detection (boxes, labels, confidence). |
| `ISpeechToTextTask`    | Converts audio input to transcribed text.             |
| `ITextToSpeechTask`    | Converts text into playable speech audio.             |

All interfaces expose a single async method (for example, `RunTaskAsync()` or
`RunChatAsync()`) that accepts structured input and returns a strongly typed
result.

<box height="10px"></box>

## Example — IChatTask Provider

<box height="5px"></box>

<oc-devui-collapsible-card heading="Llama API Provider implementing IChatTask">
    <p>The <b>Llama API Provider</b> demonstrates a full implementation of an <b>IChatTask</b> Provider for the <b>Maverick 4</b> model — Meta’s latest multimodal Llama model capable of both text and image understanding.</p>

    <pre><code>
    [CreateAssetMenu(menuName = "Meta/AI/Providers/Cloud/Llama API Provider")]
    public class LlamaApiProvider : AIProviderBase, IChatTask
    {

    [Header("Llama API Settings")]
    [SerializeField] private string endpointUrl = "https://api.llama.com/v1/chat/completions";
    [SerializeField] private string apiKey;
    [SerializeField] private string model = "llama-4-maverick";
    [SerializeField] private bool supportsVision = true;
    [SerializeField] private bool inlineRemoteImages = true;
    [SerializeField] private bool resolveRemoteRedirects = false;

    public override string ProviderName =&gt; "Llama API (Maverick 4)";

    public async Task&lt;string&gt; RunChatAsync(string prompt, CancellationToken token)
    {
        // Construct the Maverick 4 request payload
        var body = new
        {
            model = model,
            messages = new[]
            {
                new { role = "user", content = prompt }
            },
            temperature = 0.6f,
            max_tokens = 512,
            top_p = 0.9f,
            stream = false
        };

        var json = JsonUtility.ToJson(body);

        // Prepare headers for the API call
        var headers = new Dictionary&lt;string, string&gt;
        {
            { "Authorization", $"Bearer {apiKey}" }
        };

        // Send the request using the shared HttpTransport
        var response = await HttpTransport.PostJsonAsync(endpointUrl, json, token, headers);

        if (string.IsNullOrEmpty(response))
        {
            HandleError("Empty response from Llama API Maverick 4");
            return string.Empty;
        }

        // Parse the response and extract the message content
        var result = JsonUtility.FromJson&lt;LlamaResponse&gt;(response);
        return result?.choices != null &amp;&amp; result.choices.Length &gt; 0
            ? result.choices[0].message.content
            : string.Empty;
        }

        [System.Serializable]
        private class LlamaResponse
        {
            public Choice[] choices;
        }

        [System.Serializable]
        private class Choice
        {
            public Message message;
        }

        [System.Serializable]
        private class Message
        {
            public string role;
            public string content;
        }
    }
    </code></pre>

    <p>This provider supports text-only and multimodal prompts (text + image) when <b>supportsVision</b> is enabled. It can be assigned directly to any <b>LlmAgent</b>, which automatically manages the prompt flow and image packaging for multimodal tasks.</p>

</oc-devui-collapsible-card>

<box height="10px"></box>

## Using `AIProviderBase`

All Providers in the framework — including `LlamaApiProvider` — inherit from
**`AIProviderBase`**, which provides:

| Responsibility             | Description                                                                                           |
| -------------------------- | ----------------------------------------------------------------------------------------------------- |
| **API Key Management**     | Stores and secures tokens as serialized fields.                                                       |
| **Endpoint Configuration** | Defines your API URL and model ID (for example, `llama-4-maverick`).                                  |
| **Network Handling**       | Uses `HttpTransport` for all JSON or binary POST requests with built-in retry and cancellation logic. |
| **Error Handling**         | Centralized `HandleError()` and `OnError` events for reliable debugging and logging.                  |

This shared base class ensures that all Providers behave consistently and
integrate seamlessly with Unity’s async execution model.

<box height="10px"></box>

## Registering Your Provider

Your Provider is automatically detected by the **AIBlocksInstallationRoutine**.
To make it appear in the setup wizard:

1. Add a `CreateAssetMenu` attribute: `Meta/AI/Providers/<YourCategory>`
2. Implement one of the known interfaces (`IChatTask`, `IObjectDetectionTask`,
   and so on).
3. Save the Provider as an asset in your project.

During installation, the system scans all available Provider assets, groups them
by **inference type**, and displays them in the configuration dialog.

<box height="10px"></box>

## Integrating Custom Transports

The built-in `HttpTransport` handles standard HTTP workflows, but you can extend
it for **real-time streaming** or **gRPC/WebSocket** backends.

### Example Workflow

1. Create `MyWebSocketTransport.cs`.
2. Implement connection setup, message sending, and receive callbacks.
3. Use it internally within your Provider instead of `HttpTransport`.

This lets you build Providers that support live streaming (for example,
token-by-token LLM output) or extremely low-latency local inference — without
modifying any Agent code.

<box height="10px"></box>

## Extending Existing Agents

Sometimes it’s easier to extend an existing Agent rather than a Provider.
Subclassing allows for custom prompts, pre/post-processing, or contextual
awareness. This keeps your Provider unchanged while customizing runtime
behavior.

<pre><code>public class ContextualLlmAgent : LlmAgent
{
    public string Context = "You are a helpful assistant.";

    public override async Task SendPrompt(string userInput)
    {
        var fullPrompt = Context + "\n" + userInput;
        await base.SendPrompt(fullPrompt);
    }
}
</code></pre>

<box height="25px"></box>

<p>
  → <strong>Next: </strong>
  <a href="/documentation/unity/unity-ai-faq/">Troubleshooting and FAQ</a>
</p>