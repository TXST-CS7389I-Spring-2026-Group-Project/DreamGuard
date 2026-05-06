# Unity Pca Sentis

**Documentation Index:** Learn about unity pca sentis in this documentation.

---

---
title: "Unity Inference Engine For On-Device ML/CV Models"
description: "Run on-device machine learning models with the Unity Inference Engine and Passthrough Camera API on Meta Quest."
last_updated: "2026-02-09"
---

This section describes using the Unity Inference Engine framework with the Passthrough Camera API. The Inference Engine provides a framework for loading models from common open source platforms and compiling them on-device. To learn more about Inference Engine, see the [Inference Engine product page](https://docs.unity3d.com/Packages/com.unity.ai.inference@2.3/manual/index.html). This tutorial explains how to set up the **Inference Engine** and the **Yolov8n** model to identify real objects at runtime on Quest 3 devices.

After completing this section, the developer should be able to:

- Recompile and load the YOLO sample with Unity.
- Build a new project using Unity and Inference Engine.
- Compile and build Inference Engine models to support models other than YOLO.
- Integrate this API with the Unity Inference Engine architecture to access ML/CV models. Note that the Unity Inference Engine integration is not a required dependency and many developers will choose to access different or proprietary frameworks. However, it is provided as a convenience for developers to experiment quickly.

## Use cases

The framework described in this section can be followed to load the ML/CV model of your choice.  Note that the complexity of the models available online differ widely and you will need to find a model that meets the performance profile required by your application. Unity also provides samples that can be modified for use to do other things like digit recognition.

## Prerequisites

- Horizon OS: **v74** or later
- Devices: **Quest 3** or **Quest 3S**
- Grant Camera and Spatial data permissions to your app. The Spatial data permission is used by the EnvironmentRaycastManager.
- Install **Unity 2022.3.58f1**.
- Inference Engine package **2.2.1** (`com.unity.ai.inference`) installed in your project. See the [Upgrade to Inference Engine](https://docs.unity3d.com/Packages/com.unity.ai.inference@2.2/manual/upgrade-guide.html) if you used Unity Sentis before.
- Install [Mixed Reality Utility Kit](/documentation/unity/unity-mr-utility-kit-gs/#installation) (MRUK) **74.0.0** or later.

## Known issues

1. The model accuracy is not 100% because the model has been optimized to work on devices like Quest 3.
2. This model has been trained with 80 classes (not objects), it means that some object will be included inside a class. For example: Monitor and TV are in the same class `TV_Monitor`. The table in the next section identifies the classes the model can identify.
3. Some classes are hard to identify, for example, cell phones are difficult to identify, in most cases are identified as the `TV_Monitor` class.
4. Quest 3 controllers might be unrecognized or be mislabeled as remote controllers.

5. The bounding boxes visualization in the MultiObjectDetection sample doesn't perfectly align with the detected objects. For a better example of the camera to world projection, refer to the CameraToWorld sample.

## YOLO sample

This sample draws 2D boxes around the detected objects and spawns a marker in the approximate 3D position of each object when the user presses the A button.

### Build instructions

1. Clone the [Unity-PassthroughCameraApiSamples](https://github.com/oculus-samples/Unity-PassthroughCameraApiSamples) GitHub repository.
2. Open the **Unity-PassthroughCameraApiSamples** project in Unity Editor.
3. In **Build Profiles** switch the **Active Platform** to Android.
4. Open the `Assets/PassthroughCameraApiSamples/MultiObjectDetection/MultiObjectDetection.unity` sample scene.
5. Navigate to **Meta** > **Tools** > **Project Setup Tool**. If the rule "MR Utility Kit recommends Scene Support to be set to Required" exists, select **...** > **Ignore**.
   Click **Fix all** and **Apply all** to resolve the other issues and recommendations.
6. Build the app and test it on your headset.

### Preview the scene

- **Description:** This sample shows how to identify multiple objects using the Inference Engine and a pretrained YOLO model.

- **Controls**: This sample uses the Quest 3 controllers:
  - Menus (Start and Pause):
    - **Button A**: start playing
  - In Game:
    - **Button A**: place a marker in the world position for each detected object
  - At any moment:
    - **Button MENU**: back to Samples selection.

- **How to play**:
  - Start the application and use the device to look around you.
  - When an object is detected, you will see 2D floating boxes around the detected objects.
  - If you press the A button, a 3D marker will be placed in the real world position of the detected objects with the class name.
  - This model can identify the following objects (80):

<table>
  <tr>
   <td>
<code>person</code>
   </td>
   <td><code>fire hydrant</code>
   </td>
   <td><code>elephant</code>
   </td>
   <td><code>skis</code>
   </td>
   <td><code>wine glass</code>
   </td>
   <td><code>broccoli</code>
   </td>
   <td><code>dining table</code>
   </td>
   <td><code>toaster</code>
   </td>
  </tr>
  <tr>
   <td><code>bicycle</code>
   </td>
   <td><code>stop sign</code>
   </td>
   <td><code>bear</code>
   </td>
   <td><code>snowboard</code>
   </td>
   <td><code>cup</code>
   </td>
   <td><code>carrot</code>
   </td>
   <td><code>toilet</code>
   </td>
   <td><code>sink</code>
   </td>
  </tr>
  <tr>
   <td><code>car</code>
   </td>
   <td><code>parking meter</code>
   </td>
   <td><code>zebra</code>
   </td>
   <td><code>sports ball</code>
   </td>
   <td><code>fork</code>
   </td>
   <td><code>hot dog</code>
   </td>
   <td><code>tv monitor</code>
   </td>
   <td><code>refrigerator</code>
   </td>
  </tr>
  <tr>
   <td><code>motorbike</code>
   </td>
   <td><code>bench</code>
   </td>
   <td><code>giraffe</code>
   </td>
   <td><code>kite</code>
   </td>
   <td><code>knife</code>
   </td>
   <td><code>pizza</code>
   </td>
   <td><code>laptop</code>
   </td>
   <td><code>book</code>
   </td>
  </tr>
  <tr>
   <td><code>aeroplane</code>
   </td>
   <td><code>bird</code>
   </td>
   <td><code>backpack</code>
   </td>
   <td><code>baseball bat</code>
   </td>
   <td><code>spoon</code>
   </td>
   <td><code>donut</code>
   </td>
   <td><code>mouse</code>
   </td>
   <td><code>clock</code>
   </td>
  </tr>
  <tr>
   <td><code>bus</code>
   </td>
   <td><code>cat</code>
   </td>
   <td><code>umbrella</code>
   </td>
   <td><code>baseball glove</code>
   </td>
   <td><code>bowl</code>
   </td>
   <td><code>cake</code>
   </td>
   <td><code>remote</code>
   </td>
   <td><code>vase</code>
   </td>
  </tr>
  <tr>
   <td><code>train</code>
   </td>
   <td><code>dog</code>
   </td>
   <td><code>handbag</code>
   </td>
   <td><code>skateboard</code>
   </td>
   <td><code>banana</code>
   </td>
   <td><code>chair</code>
   </td>
   <td><code>keyboard</code>
   </td>
   <td><code>scissors</code>
   </td>
  </tr>
  <tr>
   <td><code>truck</code>
   </td>
   <td><code>horse</code>
   </td>
   <td><code>tie</code>
   </td>
   <td><code>surfboard</code>
   </td>
   <td><code>apple</code>
   </td>
   <td><code>sofa</code>
   </td>
   <td><code>cell phone</code>
   </td>
   <td><code>teddy bear</code>
   </td>
  </tr>
  <tr>
   <td><code>boat</code>
   </td>
   <td><code>sheep</code>
   </td>
   <td><code>suitcase</code>
   </td>
   <td><code>tennis racket</code>
   </td>
   <td><code>sandwich</code>
   </td>
   <td><code>potted plant</code>
   </td>
   <td><code>microwave</code>
   </td>
   <td><code>hair drier</code>
   </td>
  </tr>
  <tr>
   <td><code>traffic light</code>
   </td>
   <td><code>cow</code>
   </td>
   <td><code>frisbee</code>
   </td>
   <td><code>bottle</code>
   </td>
   <td><code>orange</code>
   </td>
   <td><code>bed</code>
   </td>
   <td><code>oven</code>
   </td>
   <td><code>toothbrush</code>
   </td>
  </tr>
</table>

## Unity project structure

This section describes the components used in the **MultiObjectDectection.unity scene**.

The sample scene contains the following prefabs to run the gameplay:

- **[BuildingBlock] Camera Rig**: added using Meta XR Building Blocks. This Game Object contains additional prefabs:
  - As child of **CenterEyeAnchor**:
    - **DetectionUiMenuPrefab**: manages and shows the UI of the sample.
- **[BuildingBlock] Passthrough**: Meta XR Building Blocks entity to configure and enable the Passthrough feature.
- **DetectionManagerPrefab**: contains the scanner logic to get the camera data and run the Inference Engine inference to update the UI elements.
- **SentisInferenceManagerPrefab**: contains the multi-object detection inference and UI logic.
- **EnvironmentRaycastPrefab**: contains the logic to use the MRUK Raycast to get the real world 3D point using the Quest Depth Data.
- **PassthroughCameraAccessPrefab**: creates a PassthroughCameraAccess component and manages its settings.
- **ReturnToStartScene**: common prefab used to go back to Samples selection.

### Sample Detection Manager Prefab

The main prefab that manages the logic of this sample is **DetectionManagerPrefab**.

This prefab contains the following components:

- ***DetectionManager.cs***: This script contains the sample logic. It gets the camera image from the **PassthroughCameraAccessPrefab** to send it to Inference Engine inference. Also, it manages the placement action when the user presses the A button.

### AI models used in this project

This project uses **Unity Inference Engine (2.2.1)** to run AI models locally.
Below you can find the details about each AI model used with the Inference Engine and which type of data used and can be used from each model.

### Multi-object detection model

To identify real world objects, the project uses the **Yolov9t model** prepared for the Inference Engine with an extra layer (Non-Max suppression) and quantized to Uint8. This model **requires just an image as input** and **returns the 2D coordinates and the class type of the detected objects**.

### Technical information

- Model name: **Yolov9t**
- Model format: **SENTIS Quantized Uint**
- Model size: **6,284 KB**
- Classes trained: **80 different classes**

### YOLO control scripts

`SentisInferenceRunManager.cs`: contains the logic to run inference using the Inference Engine 2.2.1.

This component contains the following parameters:

- **Backend**: the local backend used by the Inference Engine to run the inferences
- **Sentis Model**: the model asset (`.sentis`) used for these inferences
- **K_Layers Per Frame**: the number of layers executed per frame.
- **Label Asset**: a file containing a list of objects detected by the pretrained Yolov8n model.
- **UIInference**: references to the ***SentisInferenceUiManager*** scripts.

***SentisInferenceUiManager.cs:*** contains the logic to draw the UI box around the tracked object.

This component contains the following parameters:

- **Display Image**: the reference to the UI element that will contain the 2D bounding boxes.
- **Box Texture**: the texture used by the 2d Box.
- **Box Color**: the color of the bounding box.
- **Font**: the font type used to write the detected object information.
- **Font Color**: the color for the object information
- **Font size**: the size of the font used to show the object information.
- **On Object Detected event:** event launched when YOLO detects an object.

**Input values:** **TensorFloat** with the current **Camera RGB raw data image**.

- The Inference Engine framework provides a function to convert a texture to tensor float.

**Output values:** the model returns 2 tensors:

- **TensorFloat** of screen coordinates (X,Y) for all detected objects.
- **TensorInt** with the class ID for each object detected

Once you have the object and its coordinates (X and Y percentage of the input image), use the **Depth placement with MRUK Raycast** to get the real world position.

### ONNX to SENTIS converter

This sample includes an editor code to convert the Yolov9 ONNX model to the Inference Engine format.

The **Inference Engine Inference Run Manager** component lets you adjust the Intersection over Union (IoU) and score threshold for the YOLO model and generate a `.sentis` file with the Non-max-Suppression layer and quantized to UInt8.

- **IoU Threshold**: YOLO IoU threshold used to identify the objects.
- **Score Threshold**: YOLO score threshold value used to identify the objects.

The Inference Engine functions to quantize a model and save it are located inside the `SentisModelEditorConverter.cs` script.

```csharp
. . .
ModelQuantizer.QuantizeWeights(QuantizationType.Uint8, ref m_model);
ModelWriter.Save(FILEPATH, m_model);
. . .
```

## Depth placement with MRUK Raycast

To place a marker in the real world position using the 2D coordinates for each object detected, the sample uses the **MRUK Environment Raycasting (Beta) feature**. This feature performs a raycast from the 2D position to the real world using the device's depth map data.

The **EnvironmentRaycast prefab** contains the two components used to perform the depth raycast.

- **MRUKCastManager class**: this component contains the logic to perform the MRUK DepthRaycast using the UI position of each object detected.
- **Environment Raycast Manager**: this script is provided by the MRUK and contains the logic to perform the RayCast to the internal depth map generated by the Quest 3 sensors.

### MRUK Ray Cast Manager class

The `MRUKRayCastManager.cs` is the class that contains the function to get the `Transform` data of the real world point using the 2D coordinates from the detected objects in the camera image.

```csharp
public Transform PlaceGameObject(Vector3 cameraPosition)
{
    this.transform.position = Camera.position;
    this.transform.LookAt(cameraPosition);
    var ray = new Ray(Camera.position, this.transform.forward);
    if (RaycastManager.Raycast(ray, out EnvironmentRaycastHit hitInfo))
    {
        this.transform.SetPositionAndRotation(
            hitInfo.point,
            Quaternion.LookRotation(hitInfo.normal, Vector3.up));
    }

    return this.transform;
}
```

The `PlaceGameObject()` function performs a Raycast from the camera position (center eye anchor) to the world position of each UI box drawn around the object detected by the Yolov8n model.

This class uses **EnvironmentRaycastManager** from the **Meta.XR.MRUtilityKit** package to use the Ray function.

## How to generate your own YOLO ONNX model

The YOLO model used in this sample is the yolov9-t-converted.pt model from the [MultimediaTechLab/YOLO GitHub repo](https://github.com/MultimediaTechLab/YOLO) converted to ONNX.

If you want to export any YOLO model, follow the steps below to generate the ONNX file:

- Open a console in your Windows or macOS device.
- Create a **conda** environment. See [Installing conda](https://docs.conda.io/projects/conda/en/latest/user-guide/install/index.html) for more information.

```python
# Create conda environment
conda create -n "myenv" python=3.10
# Activate conda environment
conda activate myenv
```

- Install PyTorch

```python
# Install PyTorch
pip3 install torch torchvision torchaudio
```

- Install the `ultralytics` package

```python
# Install the ultralytics package from PyPI
pip3 install ultralytics
```

- Create a new python script (`yolo.py`) and add the following code inside

```python
from ultralytics import YOLO
# Load a pretrained YOLO model (recommended for training)
model = YOLO("yolov8n.pt")
# Train the model using the 'coco8.yaml' dataset for 3 epochs
results = model.train(data="coco8.yaml", epochs=3)
# Evaluate the model's performance on the validation set
results = model.val()
# Export the model to ONNX format
success = model.export(format="onnx")
```

- Run the python script to generate the `Yolov8n.onnx` file.

```python
python3 yolo.py
```

- This script will show you the path of your onnx file when it finishes:

```python
// ending yolo.py output sample
. . .
ONNX: starting export with onnx 1.17.0 opset 19...
ONNX: slimming with onnxslim 0.1.39...
ONNX: export success ✅ 0.8s, saved as 'runs/detect/train/weights/best.onnx' (12.2 MB)
Export complete (1.0s)
Results saved to /Users/username/yolo/runs/detect/train/weights
Predict:         yolo predict task=detect model=runs/detect/train/weights/best.onnx imgsz=640
Validate:        yolo val task=detect model=runs/detect/train/weights/best.onnx imgsz=640 data=/opt/anaconda3/envs/Executorch/lib/python3.10/site-packages/ultralytics/cfg/datasets/coco8.yaml
Visualize:       https://netron.app
. . .
```

Learn more about YOLO at [https://docs.ultralytics.com/quickstart/#use-ultralytics-with-python](https://docs.ultralytics.com/quickstart/#use-ultralytics-with-python).

## Building a new project with the Inference Engine

This section describes the steps necessary to build your own project using Unity and the Inference Engine for use with the Passthrough Camera API.

1. Download the [Unity Passthrough Camera API samples repository](https://github.com/oculus-samples/Unity-PassthroughCameraApiSamples) and copy this folder into a new project: `Assets/Samples/PassthroughCamera`.
2. Use [Unity Package Manager UI](https://docs.unity3d.com/Manual/upm-ui.html) to install the Inference Engine 2.2.1 into your project (`com.unity.ai.inference`).
3. Navigate to **Meta** > **Tools** > **Project Setup Tool** and fix any issues that it finds in the configuration of your project.
4. Create a new empty scene and ensure it doesn't contain a **Camera** component.
5. Add an **Event System** to your scene.
6. Navigate to **Meta** > **Tools** > **Building Blocks** and add a **Camera Rig**.
7. To run a different AI model, you can follow a similar procedure to the Inference Engine sample:
   1. Check the content from the **Assets/PassthroughCameraApiSamples/MultiObjectDetection** folder.
   2. Drag the following prefabs into the project hierarchy:
      1. **DetectionManager**
      2. **SentisInferenceManagerPrefab**
      3. **EnvironmentRaycastPrefab**
      4. **PassthroughCameraApiPrefab**
   3. Drag the **DetectionUIMenuManager** prefab to the **CenterEyeAnchor**. Then, drag the **CenterEyeAnchor** camera to the event camera for the canvas.
   4. Iterate through the prefabs and ensure the fields are populated as in the sample package
   5. Build and run to verify it works.
8. The management and processing of the Inference Engine framework are managed with the following scripts:
   1. **DetectionManagerPrefab** See [Sample Detection Manager Prefab](#sample-detection-manager-prefab) in the previous section.
      Within `DetectionManager.cs`, the `Update()` function gets the CPU texture data from the WebCamTexture object (PassthroughCameraApiPrefab reference) and kick off the inference model by calling `RunInference(captureBuffer)` once the WebCamTexture object is ready. This process checks for a camera texture and waits for the current inference to complete before starting a new one. This is a standard approach that you can re-use with ML models.
   2. **SentisInferenceManagerPrefab** contains the critical features for running Inference Engine and can be used as a design pattern for your Inference Engine models. See [YOLO control scripts](#yolo-control-scripts) in the previous section.
      1. The public variables here allow you to configure the backend for the Inference Engine to run on either the GPU or the CPU. Select the Sentis model that you plan to use (Yolov8n in this case). See [How to generate your own YOLO ONNX model](#how-to-generate-your-own-yolo-onnx-model) to learn how to create these models. ONNX is the standard format accepted by Inference Engine. Finally, it lets you set the layers per frame.
      2. **SentisInferenceRunManager.cs** controls the Inference Engine model. This includes `LoadModel()`, which takes the Inference Engine model, compiles the graph for the model, and spawns the Worker thread that will execute the model. Most importantly, it contains a public function `RunInference(Texture2D targetTexture)`, which converts the input camera image into the Tensor that can be consumed by the model and kicks off the inference.
   3. Modify the Inference Engine inference based on your model requirements:
      1. Input data will be the same (texture to tensor). The `RunInference(Texture2D targetTexture)` function converts the texture to a tensor.
      2. Output data will vary, so you must write your own post-processing scripts. Refer to the `GetInferencesResults()` function to get the output of the model using the pull request technique.
      3. Use the *layer by layer inference technique* and *pull request download* function to read the output to get better performance results on Quest. See the `InferenceUpdate()` function to see how layer by layer inference works.
9. The UI varies depending on the model you chose. The following explains the selections used in the sample:
   1.  **SentisInferenceUiManager.cs** is the script that updates the UI based on the output of the model.

   **Note:** You must create your specific script to interpret the output of the model that you choose for your own application. In the case of YOLO, use `DrawUIBoxes(output labelDs, imageWidth, imageHeight)`.
   2. **DetectionUIMenuManager** prefab is under the CenterEyeAnchor and manages the display of the initial and start panels that you see as the app launches along with the countdown.

## Recommendations for using Inference Engine on Meta Quest devices

- **Model architecture:**
  - Use models that do not require complex architectures.

    **Note:** Large generative models and LLMs do not perform well on devices like Meta Quest 3.
  - Inference Engine runs on the main thread of your Unity app, so it will impact other main processes like render pipeline.
    - Use the *layer by layer inference technique* (split the inference in layer per frame) to prevent blocking the main thread.
- **Model size:**
  - The model is loaded to the CPU or GPU of your device. This can cause multiple issues when you try to use large models:
    - Long loading times
    - Lag on the main thread during the first inference
    - Reduction of the memory budget for other resources
  - Recommendations:
    - Use the smallest version of the model that you need. For example, in the MultiObject detection sample, the smallest version of the model is YOLO (8 MB), because the medium size of YOLO (146 MB) performs worse.
    - Convert the model to Inference Engine format and quantize it to Uint8 to reduce the loading times.
- **GPU versus CPU:**
  - Choose a suitable strategy to minimize transferring data between GPU and CPU contexts:
    - If you are using an AI model to perform graphics-related work, keep all the processing on the GPU. Select the **GPUCompute backend** for Inference Engine and use the procedures that operate on the GPU to send camera data directly to Inference Engine. Finally, if you don't need to access the output of the model on the CPU, you can send it directly to your shader or material.
    - If you need to send the output to the CPU, you can:
      - Run the model on the GPU, then send results asynchronously to the CPU.
      - Run the model on the CPU.
    - Recommendations:
      - Get the output data asynchronously in any backend to prevent blocking the main thread. Inference Engine provides different techniques to accomplish this. To learn more, see [Read output from a model asynchronously](https://docs.unity3d.com/Packages/com.unity.sentis@2.1/manual/read-output-async.html).
- **NPU backend on Inference Engine:**
  - Currently, Inference Engine doesn't use NPU or hardware acceleration to run the inferences. Inference Engine is part of Unity Engine and is designed to run on multiple platforms. For Quest devices, Inference Engine is running as a regular Android platform with no specific accelerations. Keep this in mind when selecting the model.