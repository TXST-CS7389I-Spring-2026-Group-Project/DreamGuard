# Po Assetstreaming

**Documentation Index:** Learn about po assetstreaming in this documentation.

---

---
title: "Open World Games and Asset Streaming"
description: "Walkthrough of a sample and its approach to asset streaming."
last_updated: "2024-12-09"
---

One of the central challenges in developing a large, open world game is figuring out how to fit this world into memory at runtime. The world you've designed has countless meshes, materials, sound effects, animations, and events. If you were to try loading the entire thing at once, you'd quickly find yourself at the limit of the system's memory. This is a limit that you'll meet on even the most powerful machines, but it comes at you especially quickly on hardware-constrained targets like the Meta Quest.

Fortunately, the portion of the world that the player experiences at any given time is a small fraction of the world as a whole. Consider an environment like a high-rise building; while it may have a hundred floors and thousands of rooms, the player is only going to be interacting with one or two floors at a time. Similarly, in an outdoor environment, the player will only be interacting with nearby parts of the environment.

In both examples, this means that we only need to load the full fidelity of the game's assets when there is the potential for the player to interact with them. Conversely, this means that anything that is far enough away can be loaded at a lower fidelity, or not loaded at all. This also means that as the player moves around the world, we can unload previous areas to make space for new ones.

While solving your memory problems may not be that simple, we hope that [this example project](https://github.com/oculus-samples/Unity-AssetStreaming) can get you started on the right path. In it, we will describe the process of evaluating our assets (from the Oculus Studios title *Dead & Buried 2*), profiling the runtime performance of the game, generating assets at different levels of detail (LODs), creating a system to load and unload those LODs based on player position, writing tools to confirm that the asset management systems in our game are actually working, and conclude with suggestions for ways to further improve the runtime performance of the experience.

It should be noted that this sample is focused on loading level geometry, but a real-world project will also have to deal with game objects and their myriad components, which can include things like audio, animation, additional meshes, and more. As you work through those yourself, we trust that the core ideas of this article will still apply. Namely, be sure to prepare your assets for different levels of detail, don't load more than you have to, and don't try to cram all the expensive work into a single frame.

## Evaluating Asset Complexity

Before we can begin optimizing our assets, we need to understand them. This sample is focused on reducing environmental complexity, which is largely a product of the complexity of the meshes that make up the environment and the materials/shaders used by those meshes.

### Mesh Complexity

To check the mesh complexity in a scene, the easiest thing to do is to enable the wireframe in the editor. In this scene, the mesh complexity seems pretty good. Most triangles cover a lot of pixels, but on the stairs and the church, we see many very thin triangles. We can’t make out the small details on assets that are farther in the distance, so these should probably have lower complexity LODs.

Make sure to enable the statistics window to get the triangle count in the current view. This view has 525,000 triangles visible. This is high. However, keep in mind that this view is still rendering editor-only primitives, unused UI elements, and does not utilize the occlusion culling system. In game, we would expect the triangle count to be much lower.

While the [Triangle Counts on Meta Quest](/documentation/unity/unity-perf#triangle-counts-on-meta-quest) documentation lists target triangle count targets per headset, large open-world levels should target 50% of the target triangle count. This is because those targets come from examining workloads rendering smaller, enclosed levels. The geometry can be more complex in smaller, enclosed levels due to draw distances being shorter and occlusion culling working more effectively. After culling, most triangles will no longer be rendered, even if technically still within the view frustum. In larger, open levels, the complexity of the geometry has to be adjusted because culling is generally less effective. Keep in mind that both of these depend largely on how complex your shaders are.

Finally, it’s important to reduce the amount of small triangles on screen. Not only does this reduce the vertex count, it also reduces the amount of extra samples being rendered when MSAA is enabled.

### Shader Complexity

To check the shader complexity, we first inspect the code. In this case we immediately see that the shaders are quite simple. There is no direct lighting at all. All lighting comes from the lightmap, so determining the fragment color is just a matter of a few texture samples and some scalar multiplication.

To get a more precise measure of shader complexity, you can select the shader in the project browser and then select **Compile and show code** for D3D. This outputs the shader disassembly and some basic statistics. Disassembly is harder to read for a human, but it makes it fairly easy to see which shaders are expensive since more complex shaders will require more operations. As a general rule, higher stats and more lines of disassembly code means the shader will be slower.

## Measuring and Reducing Draw Calls

A top priority when trying to get the most performance out of the GPU is to reduce the number of draw calls required per frame. For more information, refer to the following articles:

- [Down the Rabbit Hole, Quest Developer Best Practices](/blog/down-the-rabbit-hole-w-oculus-quest-developer-best-practices-the-store/)
- [Mobile Draw Call Analysis](/documentation/unity/po-draw-call-analysis/)
- [How to Optimize your Quest App with RenderDoc (Part 1)](/documentation/unity/po-renderdoc-optimizations-1/)
- [How to Optimize your Quest App with RenderDoc (Part 2)](/documentation/unity/po-renderdoc-optimizations-2/)

After you’ve read those, you’ll be ready to continue.

This is a view from one subsection of the level, looking from the most southern point in the level to the north. Unity combines draw calls which share the same materials and properties into what it calls “static batches.” This view had a setpass call in between almost every draw call, which breaks these static batches. This was caused by the lightmapping. The level had a total of twelve lightmaps, and objects near each other were often in different lightmaps.

We were able to reduce the number of set pass calls by forcing these meshes into the same lightmap. We did this through [Lightmap Parameters Assets](https://docs.unity3d.com/Manual/class-LightmapParameters.html). This allows you to assign objects into a group by setting the **System Tag** parameter. You can also limit how many lightmaps the objects using the lightmap parameter asset are allowed to generate. We forced the terrain into lightmap 0 and the rest of the meshes into lightmap 1. For our purposes, this created good looking lightmaps. It also reduced the number of setpass calls from 148 to just 16. The total number of batches were reduced from 156 to 94.

You can also reduce the number of lightmaps by decreasing the **Lightmap Resolution** parameter in your lightmapping settings. This will have the same effect, but you’ll have less control.

## Mesh Batching and LOD Generation

**Note**: Mesh batching refers to combining meshes into one big mesh. Static batching refers to Unity’s batching of draw calls. A static batch has a single set pass call but can have multiple draw calls. A batched mesh only has one draw call.

Our level was built using a mix of complex and modular meshes. Buildings often use a complex mesh which was then decorated using the smaller modular pieces. Some parts of the level were completely made from modular pieces. Without batching the meshes, this would result in thousands of draw and set pass calls. Also, none of these meshes had LODs, so the full complexity of the mesh would be used even if the object itself was barely visible on screen.

As can be seen in this screenshot, the building and rock are large meshes. The wooden logs and the tent are medium sized, and the ice on the door is small.

The shaders also use no direct lighting. The lighting comes entirely from the lightmaps. Note that lightmaps can break static batches, so it’s important that meshes use the same lightmap when possible. It’s also important that all LODs of a mesh share the same lightmap. This saves in memory usage (fewer lightmaps), eliminates inconsistencies in the lightmap when switching LODs, and you only have to bake the lightmap once instead of per LOD. If your assets come with LODs, make sure their lightmap UVs match the LOD0 (highest detail) mesh.

### Goals

This is a screenshot of the Unity profiler rendering window when we started. For this view we had 4105 draw calls, 129 set pass calls, 342 static batches and we were rendering 1.4 million triangles.

From experience, we know that on the original Quest, using the GLES API, you should target less than 100 draw calls for the static geometry in your level, with around 200 draw calls in total, including all dynamic draw calls. When targeting the Quest 2 or when using the Vulkan API, these targets can be increased slightly.

The target triangle count is dependent on the complexity of the shaders. In our case, we have a very simple fragment shader which leaves us with a lot of headroom on the GPU. This allows us to have a lot of triangles. From experience, we know that on the original Quest, when using a fragment shader with lightmapping, direct lighting from a single directional light, and normal and specular maps, we'd be limited to around 300k triangles. In this case, where we only have lightmapping, we can easily handle over one million triangles.

One thing to be particularly careful of are the set pass calls. Set pass calls can be very expensive on the rendering thread, especially when using GLES. A set pass call happens every time the material is switched in between draw calls. Our aim is to only have a single set pass call for all static geometry in the level, excluding things like the skybox and terrain, since these require very different shaders compared to the normal geometry. To do this you’ll have to combine your textures into either a texture atlas or a texture array and modify the meshes and shaders to sample from the correct texture coordinates.

Our world is a combination of four sub-levels which are then repeated to create one big world. We wanted to be able to bake each sub-level individually. This allows each sub-level to use different LOD settings. Also, if one of the sub-levels changes we only need to bake that one instead of all four again.

### Our Approach

For each of the sub-levels, we take the following steps: Bake materials, generate lightmap mesh, bake lightmaps, generate LODs, and create the LOD structure.

#### Bake Materials

In this step we create one material which will be used on all the LOD meshes. We were able to use texture atlases to combine the textures used by the original materials. We didn’t have many unique textures per level, so we were able to fit all textures into a single 4096x4096 texture without losing any quality.

If a 4096x4096 texture atlas is not enough to store all textures, you should consider combining the textures into a texture array. Once the new textures have been generated you can modify the texture coordinates of the meshes to sample from the correct texture UVs.

#### Generate Lightmap Mesh

We batched all our meshes into one big mesh. Then we had Unity unwrap that mesh to generate lightmap UVs. We will be using this mesh to generate the lightmaps and then copy the lightmap UVs from this mesh to our LOD meshes.

#### Bake Lightmaps

We bake the lightmaps using Unity’s built-in lightmapper.

#### Generate LODs

Our system combines meshes by grouping them into grid cells. Each grid cell will create a batched mesh of all the meshes it contains. Each LOD level creates cells double the size of the previous level. This allows us to use them in a quadtree-based hierarchical LOD system later. To reduce complexity on distant meshes, higher LOD levels remove small meshes before batching. Then we batch the meshes to create an LOD for that grid cell. To further reduce the triangle count you can choose to remove all triangles below the terrain.

After generation, we copy the lightmap UVs from our lightmap mesh to our LOD mesh. At this point all vertices have an identical vertex in the lightmap mesh because all we’ve done is remove vertices from the original mesh.

Next we decimate the batched mesh on all levels except LOD0, with each level allowing greater and greater error. This has to be done after copying the lightmap UVs since this process can alter vertex position.

Here the white lines are the cells. The green squares are the LOD0 meshes. The blue squares are the LOD1 meshes. The red square is an LOD2 mesh. Each square is a single mesh. In LOD0 you can still see a lot of small objects (campfire, logs). In LOD1 these small objects have been removed, but trees and buildings are still visible. In LOD2 only the large objects remain (buildings, rocks). You can see that the lightmap of LOD2 still has shadows where the meshes have been removed. This happens because the lightmaps are shared between all LOD levels.

#### Create the LOD Structure

Next we create a tree structure for the generated meshes. We also store the lightmap texture and its offset and scaling. We copy the colliders from the original meshes and attach them to a separate gameobject.

For more detail, refer to the following files:

- [`LODGenerator.cs`](https://github.com/oculus-samples/Unity-AssetStreaming/blob/main/Assets/AssetStreaming/Scripts/LODGenerator.cs)
- [`LODGeneratorEditor.cs`](https://github.com/oculus-samples/Unity-AssetStreaming/blob/main/Assets/AssetStreaming/Scripts/Editor/LODGeneratorEditor.cs)
- [`LODManager.cs`](https://github.com/oculus-samples/Unity-AssetStreaming/blob/main/Assets/AssetStreaming/Scripts/LODManager.cs)

#### Third-Party Libraries

In our implementation of the LOD Generator, we used a package from the Asset Store called [Mesh Baker](https://assetstore.unity.com/packages/tools/modeling/mesh-baker-5017). We highly recommend using Mesh Baker to create texture atlases and combine your meshes. Alternatively, you could write your own or get a license for something like Simplygon.

### Combining the Sub-Levels

Once all sub-levels have finished generating LODs, we must combine them to create the final level.

#### Lightmapping

Unity stores all lightmapping information in a lightmap data asset. This asset can not be created or modified. This means that when we combine the sub-levels into the final level, we lose the lightmap data. To work around this, we combine all lightmaps into a texture array. We modify the shaders to sample from this texture array instead of using the default lightmap texture. We also modify the lightmap UVs on the meshes to include the lightmap offset and scale.

Note that the lightmap texture array can take up a lot of memory. Because of this, it is important that the lightmap texture array is in a compressed format. This can be achieved by first compressing the source texture to the desired format, then blitting it to the texture array.

It is important to store the lightmap offset, scale, and index as a vertex attribute to avoid creating unnecessary set pass calls.

#### Scenes

To allow for streamed loading, each LOD mesh should be put in its own scene. Each scene should only contain a single mesh. The mesh should already be offset to its final position. Scenes themselves do not have a transform so if the meshes don’t get offset they will all spawn at the origin of the level.

Each scene should be added to the build settings. The LOD system should then be given the index of the scene that contains the matching LOD mesh in the build settings.

## LOD System

The system in charge of tracking the current and target state of the mesh LODs must be efficient. The Quest hardware is not capable of checking the distance of each mesh to the camera every frame. All time spent tracking the LOD state takes time away from the rest of the game.

### Two Approaches

At first we used a grid-based system in which each cell calculated its own distance to the camera, starting with the highest LOD (LOD2, the coarsest detail) and proceeding to lowest (LOD0, full detail). The LOD level of a cell was only allowed to be overridden if all meshes in the area covered by the higher LOD would then get replaced by the lower LOD, so that no gaps would appear. Unfortunately this system was slow and didn’t scale well to larger levels with more cells. The only reason to use something like this is if you don’t want each LOD level to be exactly double the size of the previous one. You can, for example, have the cells of both LOD0 and LOD1 be the same size, but still use a more simplified mesh for LOD1.

We switched to a hierarchical approach based on a [quadtree](https://en.wikipedia.org/wiki/Quadtree). This allows us to cover large areas very efficiently. If we calculate that a cell should be rendering LOD2 then we can skip checking all LOD1 and LOD0 cells entirely, as they would be contained by the LOD2 area. This means most cells never get checked. A hierarchical structure also simplifies streaming. A lazy streaming system can easily be implemented by showing the parent LOD until all LODs have finished loading. Alternatively, you can always load the child cells of the one currently being shown, at the cost of more memory.

If the camera occupies the yellow grid cell, you get this LOD structure. Green is LOD0, blue is LOD1 and red is LOD2. In this case twenty LOD2 meshes have been loaded but only fourteen are visible. Twenty-four LOD1 meshes have been loaded and twenty are visible. Sixteen LOD0 meshes have been loaded and they’re all visible. The white cells on the outside are nodes from the octree structure, these don’t have meshes.

### LOD State

Each time the camera walks into a different cell, we traverse the tree from top to bottom. At each level of the tree, we calculate the distance from the camera in cells. The cell size at each level is half that of the previous level. If the distance from the camera is less than 1 cell, we load but don’t show the mesh and go down to the next level. If we don’t have to go down or if we’ve reached a leaf node then we load and show the mesh associated with that node. Nodes which are loading notify their parent node.

We first do a pass in which we calculate the state we want each node to be in. Then we do another pass to apply the correct state. The reason for this is that we don’t want any of the child nodes to become visible until all child nodes have finished loading. Otherwise you could see meshes pop in. If a node knows any of its child nodes are loading it forces itself to be visible while keeping the child nodes invisible. Each time a child node has finished loading it will notify the parent node. The parent node will enable the child nodes and disable itself once all have finished loading.

To stop the LOD state from changing rapidly when the camera is on the edge between cells, we stop the LOD system from updating until the camera has moved at least 1 meter from the position where we were when we last calculated the LOD states.

For more detail, refer to the following files:

- [`LODManager.cs`](https://github.com/oculus-samples/Unity-AssetStreaming/blob/main/Assets/AssetStreaming/Scripts/LODManager.cs)
- [`LODTreeNode.cs`](https://github.com/oculus-samples/Unity-AssetStreaming/blob/main/Assets/AssetStreaming/Scripts/LODTreeNode.cs)

### Asynchronous Loading and Unloading

Initially we loaded scenes when necessary and unloaded them when not. However, as the functions to load and unload are asynchronous this leads to race conditions if you don’t take the appropriate precautions.

In our first iterations of the LOD streaming system we ran into two major issues. Sometimes meshes would be shown twice, at two different LODs, and sometimes meshes would get stuck at a high LOD level. These problems were caused, respectively, by a sub-scene unloading before it had finished loading and a scene loading, unloading, and loading before its first load operation had finished.

**Note**: In Unity, an asynchronous operation cannot be stopped once started. It’s also not possible to unload a scene before it has completely finished loading.

Both of these problems were solved by queueing the load and unload operations. We only start a new operation once the previous one has finished. This solves both problems mentioned above. This means that you can never attempt to unload a scene that hasn’t finished loading. As a shortcut in the second case, you can have the second load operation cancel out the unload operation and skip both.

Although the majority of the work will be done asynchronously on another thread, starting the asynchronous operations can also be expensive on the main thread. Because of this, we have implemented a system that spreads the work over multiple frames.

#### Asset Unloading

When a scene is additively loaded all the assets used in the scene are also loaded. But when you unload the scene the assets don’t get unloaded. Because of this the memory usage continues to increase until all LODs are in memory.

When the Quest starts to run low on memory you will start getting hitches, which could eventually lead to freezes and crashes. This happens because in the background the low memory killer daemon is being invoked by the system.

Unity provides two ways to deallocate these now unused assets:

- [Resources.UnloadUnusedAssets](https://docs.unity3d.com/ScriptReference/Resources.UnloadUnusedAssets.html), which is a very slow operation that unloads all unused assets. This can’t really be used during gameplay. It will cause multiple missed frames. It could be used when the player is teleporting or at a loading screen.
- [Resources.UnloadAsset](https://docs.unity3d.com/ScriptReference/Resources.UnloadAsset.html), which can be used to unload assets during gameplay. You’ll still need a system to load balance this since it’s a heavy operation (unloading a single mesh can cost about 0.3ms on the main thread).

**Note**: `Resources.UnloadAsset` only works when you pass the mesh used by the scene that’s being unloaded. If you try to pass a copy of the same mesh, only the copy will get unloaded. We attempted to serialize all meshes used by a LOD node so that we could avoid collecting the meshes at runtime, but this doesn’t work because the meshes serialized by the script are copies of the mesh and not the mesh used by the scene.

### Debugging Tools

These systems are complex, so it can be worthwhile to build debugging and visualization tools to help you check your assumptions and investigate errors.

#### LOD Debug View

To clearly visualize the LODs being displayed, we wrote a shader that outputs a color based on an integer material parameter (set to the LOD level index, green for the highest detail and red for the lowest detail). At runtime we create a number of materials using this shader. One for each LOD level, plus one that indicates a transition between LOD levels. When the scene gets loaded it checks if LOD debugging is enabled. If so, it applies the material corresponding to its LOD level.

The debug view material has no material-specific properties. This allows us to share the same material across all meshes of an LOD level. However, mesh properties are still available, and since our lightmaps are a global parameter, we were able to use the lightmaps to create depth in the otherwise flat shading of the debug shader.

#### Benchmark Mode

It can be helpful to have a way to run your game in a predictable, predetermined, automatic mode, which can, for example, be run automatically each night to confirm that everything still works the way you expect it to after a day of commits.

For this benchmark mode, we first created a simple tool to create a path from a set of waypoints. The camera will follow this path at a constant speed. The camera direction is also fixed to stop variations in the benchmark results due to the view direction.

Our first attempt used bezier curves to create a smooth path. Although a valid option, it makes it more difficult to create the path, so we switched to just using straight lines between waypoints.

Our tool automatically placed the waypoints exactly two meters above ground level. This is important because if the camera is too low or too high up in the air this can affect occlusion culling performance.

We do not fix the camera position to be on the path. Instead the camera will always look one meter ahead on the path, then move forward trying to maintain a one meter gap. This smoothes out sharp corners a little bit. When setting the camera view direction, we only change the rotation around the Y axis. We restrict it to Y because changing the rotation of the X or Z axis causes the OVR Camera Rig to roll, which can lead to the camera going upside down or flipping its forward direction every frame.

#### Free Flight Camera

An easy way to confirm that the LOD system is working properly is to actually see all the tiles in frame at once, and the only way to do that is with the free flight camera.

For this mode, we modified the `SimpleCapsuleWithStickMovement` script that comes with the [Unity Starter Samples](https://github.com/oculus-samples/Unity-StarterSamples) to allow movement in the Y direction. We also necessarily disabled gravity on the rigidbody. As with the benchmark mode, do not try to rotate the OVR Camera Rig around anything but the Y axis.

#### Force LOD Level

The LOD system is designed to increase detail as the camera approaches, but during development we'll sometimes want to inspect the low detail meshes up close. To facilitate this, we include a mode that lets you freeze the LODs to a specific level.

When forcing the LOD level, you must take into account that forcing everything to LOD0 will cause the app to use more memory then usual. We are not limited by memory here so we can get away with having every LOD0 mesh loaded at the same time, but in a heavier environment this might not be an option. If you do find yourself dealing with such an environment, consider forcing the LOD level only on cells which would normally be visible (as we've done here). If a cell is so far away that not even LOD2 (our highest LOD level) is showing then we do not need to force the LOD on that cell. This can easily be accomplished by traversing the tree as normal until you’ve reached the LOD2 nodes, after which you force it to go down the tree until the desired LOD level is reached.

#### Freeze LOD Level

To freeze the LOD levels of the meshes, simply stop updating the LOD system.

For more detail, refer to the following files:

- [`LODManager.cs`](https://github.com/oculus-samples/Unity-AssetStreaming/blob/main/Assets/AssetStreaming/Scripts/LODManager.cs)
- [`LODTreeNode.cs`](https://github.com/oculus-samples/Unity-AssetStreaming/blob/main/Assets/AssetStreaming/Scripts/LODTreeNode.cs)
- [`Benchmark.cs`](https://github.com/oculus-samples/Unity-AssetStreaming/blob/main/Assets/AssetStreaming/Scripts/Benchmark.cs)
- [`BenchmarkWalker.cs`](https://github.com/oculus-samples/Unity-AssetStreaming/blob/main/Assets/AssetStreaming/Scripts/BenchmarkWalker.cs)
- [`UserInput.cs`](https://github.com/oculus-samples/Unity-AssetStreaming/blob/main/Assets/AssetStreaming/Scripts/UserInput.cs)

### Using the LOD System

The LOD system can be used as is, without any modifications or improvements. Unfortunately the LOD Generator can not, because we’ve had to remove parts of the code. To use the LOD system, you’ll have to generate your own LOD meshes. See the [Third-Party Libraries](#third-party-libraries) section above for more information.

The LODs have to follow a grid pattern and higher LOD levels should be double the size of the previous. This is necessary because the LOD system uses a quadtree internally. To populate the LOD Manager, call `LODManager.SetLOD`. This will then create a quadtree from the objects being passed in. At this point the LODs won't use streaming, all meshes will be loaded (it just toggles the meshes on/off).

To enable mesh streaming, place a Sublevel Combiner script in your scene and run it. This should create a scene for each of the meshes and set up the LOD Manager to use streaming. These systems do make assumptions about the materials, lightmapping, and collision setup of your LODs. They might need to be modified to work with your setup.

## Occlusion Culling

Depending on the game, occlusion culling can make a huge difference in performance. In our case we walk on the ground in between large buildings and/or tall rock formations, which lends itself very well to occlusion culling.

### Built-In Occlusion Culling System

Unity’s built-in occlusion culling system is easy to use, the occlusion data doesn’t take long to generate, and it’s pretty accurate. However, it’s not well suited for mobile devices. The system seems to eat up a lot of memory, regardless of the size of the occlusion data. It can also be taxing on the CPU and take up a lot of time on the main thread.

Despite those downsides, we still used the built-in occlusion system, as we weren't bottlenecked by memory or CPU. To generate the occlusion data for the scene you'll first have to load all LOD meshes (additively load the scenes containing the LOD meshes), and then you can generate the occlusion data like you would normally.

### Custom Occlusion Culling System

In a more complex game the built-in occlusion system can’t be used for the reasons mentioned above. In this case there are ways to create your own occlusion culling system that is better suited for mobile devices.

For static geometry, such a system can be implemented relatively easily. You'll need to split the scene up into cells. For each cell you render the scene to a cubemap with a custom material that outputs a unique identifier for that mesh. Then you read back the cubemap on the CPU and collect the identifiers of all the meshes that are visible. You store the results so you can access them at runtime. At runtime you then disable the meshes that are not on the list whenever you enter a cell. No occlusion checks are done at runtime, making this a very CPU friendly approach.

This is a very simple explanation. A naive implementation of which could result in much worse performance compared to just using the built-in occlusion culling system. Baking times for the occlusion data can also become a problem depending on the size of the levels and the way this has been implemented.