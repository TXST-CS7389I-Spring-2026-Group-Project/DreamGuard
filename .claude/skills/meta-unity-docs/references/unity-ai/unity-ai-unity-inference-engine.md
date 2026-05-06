# Unity Ai Unity Inference Engine

**Documentation Index:** Learn about unity ai unity inference engine in this documentation.

---

---
title: "Unity Inference Engine"
description: "Configure, optimize, and deploy on-device AI models using the Unity Inference Engine on Meta Quest."
last_updated: "2025-12-11"
---

<box height="5px"></box>

## Learning objectives

- Understand how to configure and run AI models directly on-device using the
  **Unity Inference Engine**.
- Learn how to convert, serialize, and quantize `.onnx` models into optimized
  **`.sentis`** assets for faster performance.
- Implement model warm-up routines to prevent stutters and ensure smooth runtime
  initialization.
- Optimize inference with **GPU-based Non-Max Suppression (NMS)** and **Split
  Over Frames** scheduling.
- How to transform 2D object detections into spatially anchored 3D
  visualizations using **DepthTextureAccess**.

<box height="10px"></box>

**On-device inference** allows AI models to run directly on Meta Quest headsets,
eliminating network dependencies and enabling low-latency processing. This
capability is powered by the
[Unity Inference Engine](https://docs.unity3d.com/Packages/com.unity.ai.inference@2.3/manual/index.html)
(formerly _Unity Sentis_), which executes ONNX or Sentis models efficiently on
CPU or GPU backends. Running inference on-device enables instant feedback,
offline operation, and complete data privacy.

<box height="10px"></box>

## Why run models on-device

| Benefit                       | Description                                                                              |
| ----------------------------- | ---------------------------------------------------------------------------------------- |
| **Offline Operation**         | Works fully offline — essential for exhibitions, enterprise, and privacy-sensitive apps. |
| **Ultra-Low Latency**         | All computation runs locally, removing network delays.                                   |
| **Full Privacy**              | Sensitive inputs like passthrough images never leave the device.                         |
| **Deterministic Performance** | Performance remains stable regardless of network or server load.                         |

<box height="10px"></box>

## The UnityInferenceEngineProvider

The `UnityInferenceEngineProvider` is the bridge between Unity and the on-device
inference runtime. It wraps your AI model asset (for example, `.onnx` or
`.sentis`) and provides configuration options for **execution backend**, **frame
scheduling**, and **GPU-based post-processing**.

### Inspector parameters

| Field                  | Description                                                         |
| ---------------------- | ------------------------------------------------------------------- |
| **Model Asset**        | The trained model file (`.onnx` or `.sentis`).                      |
| **Backend Type**       | Choose between `CPU` and `GPUCompute` backends.                     |
| **Split Over Frames**  | Run a portion of the model per frame to maintain framerate.         |
| **Layers Per Frame**   | Number of layers to execute each frame (used when splitting).       |
| **NMS Compute Shader** | Optional GPU Non-Max Suppression for faster bounding box filtering. |
| **Class Labels File**  | Optional `.txt` file mapping class indices to readable labels.      |

<box height="10px"></box>

## Model conversion, serialization, and quantization

This guide is for when you are planning to use your own models for the Object
Detection Building Block, for example. Converting, serializing, and quantizing
your models are key steps to prepare them for efficient runtime execution in
Unity. These optimizations ensure faster load times, lower memory usage, and
consistent performance across devices. The following sections explain how to
clean up ONNX models, convert them into Unity’s optimized **`.sentis`** format,
and optionally serialize or quantize them for deployment.

To make this process easier, Meta provides an editor window located at **Meta →
Tools → Unity Inference Engine → ONNX → Sentis Converter**, which allows you to
import, clean up, quantize, and export your ONNX models as optimized
**`.sentis`** assets with just a few clicks. The following sections explain how
to use this tool and how to serialize or quantize models for deployment.

### 1. Quantize to reduce size

Quantization compresses your model by storing weights in lower precision formats
(Float16 or Uint8). This reduces file size and memory usage with minimal
accuracy loss.

| Type        | Bits | Description                                  |
| ----------- | ---- | -------------------------------------------- |
| **None**    | 32   | Full precision (default)                     |
| **Float16** | 16   | Half precision, preserves most accuracy      |
| **Uint8**   | 8    | Highly compact, may slightly reduce accuracy |

<box height="5px"></box> You can quantize and serialize models directly from
code:

<pre><code class="language-csharp">using Unity.InferenceEngine;

void QuantizeAndSave(Model model, string path)
{
    ModelQuantizer.QuantizeWeights(QuantizationType.Float16, ref model);
    ModelWriter.Save(path, model);
}
</code></pre>

<box height="10px"></box>

### 2. Convert your model

Most ONNX models require cleanup before use in Unity. Use the
**OnnxModelConverterEditor** (Window → Meta → AI → ONNX Model Converter) to:

1. Import your `.onnx` model.
2. Apply cleanup options (for example, Softmax, NMS removal).
3. Choose your Quantization Type (None, Float16, Uint8).
4. Enter the desired path and name of your converted model
5. Press **Convert to Sentis**.

This generates a `.sentis` asset optimized for the Unity runtime.

<box height="10px"></box>

### 3. Serialize and load models

Optionally, for large models, create a serialized asset to speed up loading:

1. In the **Project window**, select your ONNX model.
2. In the **Inspector**, click **Serialize to StreamingAssets**.
3. Unity generates a `.sentis` file inside your `StreamingAssets` folder.

You can then load it at runtime:

<pre><code class="language-csharp">using Unity.InferenceEngine;
Model model = ModelLoader.Load(Application.streamingAssetsPath + "/mymodel.sentis");
</code></pre>

#### Advantages of serialization:

- Faster load times and smaller project size
- Unity-validated format (guaranteed compatibility)
- Easier to share between projects

<box height="10px"></box>

## Runtime initialization and warm-up

When a model first runs, Unity Inference Engine must allocate buffers, compile
GPU kernels, and upload weights. This can cause a one-time delay of **several
seconds** at startup.

Always perform a **warm-up inference** during loading or splash screens:

<pre><code class="language-csharp">IEnumerator Start()
{
    var worker = model.CreateWorker();
    var input = new Tensor(1, 3, 224, 224);
    worker.Execute(input);
    yield return null;
    Debug.Log("Model warmed up and ready");
}
</code></pre>

### Best practices

- Warm up before gameplay starts.
- Keep the worker alive across frames.
- Dispose workers only on scene unload.
- For multiple models (for example, STT + detection), warm them up sequentially.

<box height="10px"></box>

## Non-Max Suppression (NMS)

Object detectors often output multiple overlapping boxes for the same object.
**Non-Max Suppression (NMS)** filters these out, keeping only the most confident
ones.

### How to efficiently run NMS on the GPU

Some ONNX models include a CPU-based `NonMaxSuppression` op, like YoloX, which
can cause performance bottlenecks. If you have ever tried to run Yolo on the CPU
backend, or GPU for that matter, you have likely experienced significant frame
drops. Furthermore, simply changing the backend to GPU does not solve the issue,
as the `NonMaxSuppression` op is still executed on the CPU. Instead, on the
`GPUCompute` backend, you will notice that the detection results are not
filtered at all, which leads to the model outputting a large number of bounding
boxes for the same object.

To tackle this, you can post-process detections using the provided GPU NMS
implementation:

- `GpuNMS.cs`
- `NMSCompute.compute`

These run NMS entirely on the GPU, preventing GPU-to-CPU sync stalls. Important
to note is that for this to work, the NMS layer must be removed from the model.
This is done automatically when you convert your model to the `.sentis` format
using the **OnnxModelConverterEditor** and check the **Remove NMS** option.

<box height="5px"></box>

<oc-devui-note type="important" heading="CPU Bottlenecks with Unity Inference Engine" markdown="block">
You will notice performance spikes, despite the Object Detection Building
Block removing the NMS layer and running it on the `GPUCompute` backend. This is
because it copies all results from the CPU to the GPU to then filter and
places bounding boxes in 3D. This bottleneck will be resolved in a future release.
</oc-devui-note>

<box height="20px"></box>

See [Agents and Building Blocks](/documentation/unity/unity-ai-agents) to learn how each AI building block works.