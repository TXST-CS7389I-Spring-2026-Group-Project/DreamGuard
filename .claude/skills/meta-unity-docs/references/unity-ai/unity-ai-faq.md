# Unity Ai Faq

**Documentation Index:** Learn about unity ai faq in this documentation.

---

---
title: "Troubleshooting and FAQ"
description: "Solutions for setup, performance, and integration issues when using AI Building Blocks in Unity."
last_updated: "2025-12-11"
---

<box height="5px"></box>

This guide helps you diagnose and resolve **common issues** when integrating AI
Building Blocks — including setup errors, on-device performance bottlenecks,
provider misconfiguration, and streaming latency.

<box height="10px"></box>

<oc-devui-note type="important" heading="Before You Start">
  Most issues stem from missing credentials, wrong model IDs, or incorrect endpoints. Always verify your API keys, endpoint URLs, and Unity console logs first.
</oc-devui-note>

<box height="10px"></box>

## Common Setup Issues

<oc-devui-collapsible-card heading="Missing API keys or invalid credentials. HTTP 401/403.">
  <p><strong>Symptoms:</strong> HTTP 401 / 403 errors or empty responses.</p>
  <ul>
    <li>Verify your API key in the Provider Inspector.</li>
    <li>Ensure it’s valid for the selected endpoint (Hugging Face router vs. hosted model).</li>
    <li>For HuggingFace make sure to check if the inference runs through the HuggingFace endpoint or the one of the inference provider.</li>
    <li>Check your provider's API key settings for the correct scopes (for example, read/write access).</li>
    <li>Some models (for example, <b>gpt-4o</b>) require a premium subscription or API credits.</li>
  </ul>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Model not responding or timing out. HTTP 429.">
  <p><strong>Symptoms:</strong> Unity freezing, empty responses, or HTTP 429 errors (rate-limiting).</p>
  <ul>
    <li>Check your Internet connection or local server availability.</li>
    <li>Increase timeout in <b>HttpTransport.PostJsonAsync()</b> if needed.</li>
    <li>Avoid sending multiple concurrent HTTP requests to the same model endpoint.</li>
    <li>Inspect Unity Console logs for HTTP or JSON parsing exceptions.</li>
  </ul>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Wrong endpoint or model ID. HTTP 404.">
  <p><strong>Symptoms:</strong> 404 or “Model not found.”</p>
  <ul>
    <li>Copy endpoints directly from the provider’s example <b>curl</b> commands. Go to the deployment tab and copy the name of the exact endpoint and model name. Also not which API key you are supposed to use for that endpoint. All this information can be found in the <b>curl</b> command.</li>
    <li>Ensure router and hosted model endpoints are not mixed up.</li>
    <li>Confirm your API key has access to the specific model under your organization.</li>
  </ul>
</oc-devui-collapsible-card>

<box height="10px"></box>

## Unity Inference Engine

<oc-devui-collapsible-card heading="On-device model fails to load">
  <p><strong>Symptoms:</strong> <b>.onnx</b> or <b>.sentis</b> models fail to initialize or return null tensors.</p>
  <ul>
    <li>Check tensor input/output shapes against your expected data.</li>
    <li>Look for Unity Inference Engine console errors such as <em>Unsupported operator</em>.</li>
    <li>If in doubt, place the model under <b>Resources/</b> or <b>StreamingAssets/</b>.</li>
  </ul>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="First inference causes stutter or freeze">
  <p><strong>Symptoms:</strong> Frame spike during the first AI model run.</p>
  <ul>
    <li>Run a <strong>warm-up inference</strong> at application startup.</li>
    <li>Enable <strong>Split Over Frames</strong> and adjust <strong>Layers Per Frame</strong> in your Inference Engine Provider.</li>
    <li>Preload models during splash or loading screens to allocate buffers early.</li>
  </ul>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="General performance recommendations">
  <ul>
    <li>Reuse <code>IWorker</code> instances instead of recreating them per frame.</li>
    <li>Pre-allocate and reuse tensors instead of reallocating.</li>
    <li>Quantize large models to <code>FP16</code> or <code>INT8</code> to reduce GPU load.</li>
    <li>Batch smaller inputs when possible.</li>
    <li>Profile frequently using Unity Profiler and Quest Developer Hub.</li>
  </ul>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Object Detection optimization">
  <ul>
    <li>Reduce input texture resolution.</li>
    <li>Move NMS (Non-Max Suppression) to GPU using <code>GpuNMS</code> and <code>NMSCompute.compute</code>.</li>
    <li>Adjust <strong>Layers Per Frame</strong> for stable frame rate.</li>
  </ul>
</oc-devui-collapsible-card>

<box height="10px"></box>

## Frequently Asked Questions

<oc-devui-collapsible-card heading="Can I use multiple Providers at once?">
  <p>Yes. Assign different Providers to different Agents — for example, run Object Detection locally while using a cloud LLM for text generation.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="How can I reduce API cost or token usage?">
  <p>Use <strong>local models</strong> (Ollama, Unity Inference Engine) during development and switch to cloud Providers for production builds. Make sure no inference is running every frame unless absolutely necessary. Do not ship any API credentials in a production build or GitHub repo.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Can I use fine-tuned models?">
  <p>Yes. Export your fine-tuned models to ONNX or serve them via HTTP, then connect through a custom Provider implementing the correct interface.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Why doesn’t my Provider appear in the setup wizard?">
  <p>Ensure your Provider implements at least one task interface (<code>IChatTask</code>, <code>IObjectDetectionTask</code>, and so on) and defines a valid <code>CreateAssetMenu</code> path.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="Can I use the Meta Quest microphone and camera directly?">
  <p>Yes. <code>SpeechToTextAgent</code> accesses Unity’s Microphone API, and <code>ObjectDetectionAgent</code> supports both <code>WebCamTexture</code> and <code>PassthroughCameraAccess</code>.</p>
</oc-devui-collapsible-card>

<oc-devui-collapsible-card heading="What’s the difference between ONNX and Sentis models?">
  <ul>
    <li><strong>ONNX</strong>: Generic open format, slower to load.</li>
    <li><strong>Sentis (.sentis)</strong>: Precompiled Unity format — optimized for fast startup and low memory usage.</li>
  </ul>
</oc-devui-collapsible-card>

<box height="20px"></box>

<p>
  → <strong>Next: </strong>
  <a href="/documentation/unity/unity-ai-building-blocks-overview/">Back to Overview</a>
</p>