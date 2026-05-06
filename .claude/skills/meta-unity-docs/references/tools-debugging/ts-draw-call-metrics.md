# Ts Draw Call Metrics

**Documentation Index:** Learn about ts draw call metrics in this documentation.

---

---
title: "Draw Call Metrics"
description: "Track and analyze draw call metrics to identify rendering performance bottlenecks on Meta Quest."
---

Draw call metrics can be retrieved by using RenderDoc Meta Fork to perform a draw call trace on a frame capture. The following table lists currently available metrics.

Note that all percentage values are of clock cycles for a draw call.

<table>
  <tr>
   <td><strong>Metric</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td>Clocks
   </td>
   <td>Number of GPU clocks that elapsed while a draw call was being executed. If a draw call does not touch some tiles,  there will be no GPU clocks added from rendering that tile. However, there will be a small fixed cost of setup overhead for that tile.
   </td>
  </tr>
  <tr>
   <td>% Vertex Fetch Stall
   </td>
   <td>Percentage of clock cycles where the GPU cannot make any more requests for vertex data. A high value for this metric implies the GPU cannot get vertex data from memory fast enough, and rendering performance may be negatively affected
   </td>
  </tr>
  <tr>
   <td>% Texture Fetch Stall
   </td>
   <td>Percentage of clock cycles where the shader processors cannot make any more requests for texture data. A high value for this metric implies the shaders cannot get texture data from the texture pipe (L1, L2 cache or memory) fast enough, and rendering performance may be negatively affected
   </td>
  </tr>
  <tr>
   <td>L1 Texture Cache Miss Per Pixel
   </td>
   <td>Average number of Texture L1 cache misses per pixel. Lower values for this metric imply better memory coherency. If this value is high, consider using compressed textures or reducing texture usage.
   </td>
  </tr>
  <tr>
   <td>% Texture L1 Miss
   </td>
   <td>Number of L1 texture cache misses divided by L1 texture cache requests. This metric does not consider how many texture requests are made per time period, but is a simple miss to request ratio.
   </td>
  </tr>
  <tr>
   <td>% Texture L2 Miss
   </td>
   <td>Number of L2 texture cache misses divided by L2 texture cache requests. This metric does not consider how many texture requests are made per time period, but is a simple miss to request ratio.
   </td>
  </tr>
  <tr>
   <td>% Stalled on System Memory
   </td>
   <td>Percentage of draw call cycles the L2 cache is stalled while waiting for data from system memory.
   </td>
  </tr>
  <tr>
   <td>% Instruction Cache Miss
   </td>
   <td>Number of L1 instruction cache misses divided by L1 instruction cache requests.
   </td>
  </tr>
  <tr>
   <td>Pre-clipped Polygon
   </td>
   <td>Number of polygons submitted to the GPU before any hardware clipping.
   </td>
  </tr>
  <tr>
   <td>% Prims Trivially Rejected
   </td>
   <td>Percentage of primitives that are trivially rejected. A primitive can be trivially rejected if it is outside the visible region of the render surface. These primitives are ignored by the rasterizer.
   </td>
  </tr>
  <tr>
   <td>% Prims Clipped
   </td>
   <td>Percentage of primitives clipped by the GPU, where new primitives are generated. For a primitive to be clipped, it must have a visible portion inside the viewport, but extend outside the "guardband", which is an area that surrounds the viewport and significantly reduces the number of primitives the hardware has to clip.
   </td>
  </tr>
  <tr>
   <td>Average Vertices / Polygon
   </td>
   <td>Average number of vertices per polygon. This will be around 3 for triangles, and close to 1 for triangle strips.
   </td>
  </tr>
  <tr>
   <td>Reused Vertices / Second
   </td>
   <td>Number of vertices used from the post-transform vertex buffer cache. A vertex may be used in multiple primitives; a high value for this metric (compared to number of vertices shaded) indicates good reuse of transformed vertices, reducing vertex shader workload.
   </td>
  </tr>
  <tr>
   <td>Average Polygon Area
   </td>
   <td>Average number of pixels per polygon. Adreno's binning architecture will count a primitive for each bin it covers, so this metric may not exactly match expectations.
   </td>
  </tr>
  <tr>
   <td>% Shaders Busy
   </td>
   <td>Percentage of time that all shader cores are busy.<br/>

Generally, the shader is considered busy any time it is processing a draw, so stall cycles count as busy cycles, as do cycles where the shader core is making actual progress.<br/>

The shader core will often not be busy during memory load and store operations. There are also times at the beginning of a bin when the GPU is busy getting a draw call set up (processing state, fetching vertex indices, fetching vertices) but the shader does not yet have vertices or fragments to process.
   </td>
  </tr>
  <tr>
   <td>Vertices Shaded
   </td>
   <td>Number of vertices submitted to the shader engine.
   </td>
  </tr>
  <tr>
   <td>Fragments Shaded
   </td>
   <td>Number of fragments submitted to the shader engine.
   </td>
  </tr>
  <tr>
   <td>Vertex Instructions
   </td>
   <td>Total number of scalar vertex shader instructions issued. Includes full precision Arithmetic Logic Unit (ALU) vertex instructions and EFU vertex instructions. Does not include medium precision instructions, since they are not used for vertex shaders. Does not include vertex fetch or texture fetch instructions.
<br/>
The GPU ALU/EFU hardware counters count scalar instructions. Vector operations are counted as multiple scalar operations.
   </td>
  </tr>
  <tr>
   <td>Fragment Instructions
   </td>
   <td>Total number of fragment shader instructions issued. Reported as full precision scalar Arithmetic Logic Unit (ALU) instructions, where 2 medium precision instructions equal 1 full precision instruction. Also includes interpolation instructions (which are executed on the ALU hardware) and EFU (Elementary Function Unit) instructions. Does not include texture fetch instructions.
<br/>
The GPU ALU/EFU hardware counters count scalar instructions. Vector operations are counted as multiple scalar operations.
   </td>
  </tr>
  <tr>
   <td>Fragment ALU Instructions(Full)
   </td>
   <td>Total number of full precision fragment shader instructions issued. Does not include medium precision instructions or texture fetch instructions.
   </td>
  </tr>
  <tr>
   <td>Fragment ALU Instructions(Half)
   </td>
   <td>Total number of half precision scalar fragment shader instructions issued. Does not include full precision instructions or texture fetch instructions.
<br/>
The Meta Quest supports high precision (32-bit) and medium precision (16-bit) operations. If you specify "lowp" in your shader, that will map to a 16 bit operation and count with the Half precision counters.
   </td>
  </tr>
  <tr>
   <td>Fragment EFU Instructions
   </td>
   <td>Total number of scalar fragment shader Elementary Function Unit (EFU) instructions issued. These include math functions like sin, cos, pow, and so on.
   </td>
  </tr>
  <tr>
   <td>Textures / Vertex
   </td>
   <td>Average number of textures referenced per vertex.
   </td>
  </tr>
  <tr>
   <td>Textures / Fragment
   </td>
   <td>Average number of textures referenced per fragment.
   </td>
  </tr>
  <tr>
   <td>ALU / Vertex
   </td>
   <td>Average number of vertex scalar shader Arithmetic Logic Unit (ALU) instructions issued per shaded vertex. Does not include fragment shader instructions.
   </td>
  </tr>
  <tr>
   <td>ALU / Fragment
   </td>
   <td>Average number of scalar fragment shader Arithmetic Logic Unit (ALU) instructions issued per shaded fragment, expressed as full precision ALUs (2 mediump = 1 highp). Includes interpolation instruction. Does not include vertex shader instructions.
   </td>
  </tr>
  <tr>
   <td>EFU / Fragment
   </td>
   <td>Average number of scalar fragment shader EFU instructions issued per shaded fragment. Does not include Vertex EFU instructions
   </td>
  </tr>
  <tr>
   <td>EFU / Vertex
   </td>
   <td>Average number of scalar vertex shader EFU instructions issued per shaded vertex. Does not include fragment EFU instructions
   </td>
  </tr>
  <tr>
   <td>% Time Shading Fragments
   </td>
   <td>Amount of time spent shading fragments compared to the total time spent shading everything.
   </td>
  </tr>
  <tr>
   <td>% Time Shading Vertices
   </td>
   <td>Amount of time spent shading vertices compared to the total time spent shading everything.
   </td>
  </tr>
  <tr>
   <td>% Time Compute
   </td>
   <td>Amount of time spent in compute work compared to the total time spent shading everything.
   </td>
  </tr>
  <tr>
   <td>% Shader ALU Capacity Utilized
   </td>
   <td>Percent of maximum shader capacity (ALU operations) utilized. For each cycle that the shaders are working, the average percentage of the total shader ALU capacity that is utilized for that cycle.
<br/>
The Arithmetic Logic Units (ALUs) are a large SIMD array that process many vertices or fragments at a time. If all the ALU elements in the array are active in one cycle, that ALU is operating at full capacity. However, there are times where not every ALU element is active. In the case of very small triangles, for example, the way the GPU allocates work will leave some of the (fragment) ALU elements empty. Or, if some fragments pass the z test and some nearby fragments fail, there may also be some empty slots in the ALU array. This metric attempts to convey how efficiently the workload is running on the ALUs. If the ALU capacity utilized is near 100%, that means the ALUs are working as efficiently as they can,  with every entry in the SIMD doing useful work every cycle. If this metric is low, it means that there are empty slots in the ALU SIMD and the system is not running as efficiently as it could. Note, however, that low ALU utilization isn’t necessarily bad. It can be low just because there isn’t a lot of ALU work compared to other work (such as texture work).

   </td>
  </tr>
  <tr>
   <td>% Time ALUs Working
   </td>
   <td>Percentage of time the Arithmetic Logic Units (ALUs) are working while the shaders are busy.
   </td>
  </tr>
  <tr>
   <td>% Time EFUs Working
   </td>
   <td>Percentage of time the EFUs are working while the shaders are busy.
   </td>
  </tr>
  <tr>
   <td>% Nearest Filtered
   </td>
   <td>Percent of texels filtered using the "nearest" sampling method.
   </td>
  </tr>
  <tr>
   <td>% Linear Filtered
   </td>
   <td>Percent of texels filtered using the "linear" sampling method.
   </td>
  </tr>
  <tr>
   <td>% Anisotropic Filtered
   </td>
   <td>Percent of texels filtered using the anisotropic sampling method.
   </td>
  </tr>
  <tr>
   <td>% Non-Base Level Textures
   </td>
   <td>Percent of texels coming from a non-base MIP level.
   </td>
  </tr>
  <tr>
   <td>% Texture Pipes Busy
   </td>
   <td>Percentage of time that any texture pipe is busy.
   </td>
  </tr>
  <tr>
   <td>Read Total (Bytes)
   </td>
   <td>Total number of bytes read by the GPU from memory. This represents the total amount of data read by the GPU, regardless of which block requested the memory.
   </td>
  </tr>
  <tr>
   <td>Write Total (Bytes)
   </td>
   <td>Total number of bytes written by the GPU to memory. This represents the total amount of memory written by the GPU during the sample period, regardless of which GPU block was doing the writing.
   </td>
  </tr>
  <tr>
   <td>Texture Memory Read BW (Bytes)
   </td>
   <td>Bytes of texture data read from memory. This represents data requested by the texture pipes for any type of operation (vertex textures, fragment textures, compute operations that read a texture).
   </td>
  </tr>
  <tr>
   <td>Vertex Memory Read (Bytes)
   </td>
   <td>Bytes of vertex data read from memory. This represents data read by the vertex processing pipeline besides texture data (vertex positions, attributes).
   </td>
  </tr>
  <tr>
   <td>SP Memory Read (Bytes)
   </td>
   <td>Bytes of data read from memory by the shader processors. This represents data requested by the shader processor through an explicit load type operation.
   </td>
  </tr>
  <tr>
   <td>Avg Bytes / Fragment
   </td>
   <td>Average number of bytes transferred from main memory for each fragment.
<br/>
More accurately, this is the average amount of texture data read per fragment. It divides the texture memory read by the number of fragments shaded. This is not a particularly precise metric, but for many graphics use cases, it provides a relatively accurate picture of how much texture data is required (on average) for each pixel.
   </td>
  </tr>
  <tr>
   <td>Avg Bytes / Vertex
   </td>
   <td>Average number of bytes transferred from main memory for each vertex. This metric divides the Vertex Memory Read (Bytes) metric described above and divides it by the number of vertices shaded.
   </td>
  </tr>
   <tr>
   <td>Preemption
   </td>
   <td>The number of GPU preemptions that occurred.
   </td>
  </tr>
  <tr>
   <td>Avg Preemption Delay
   </td>
   <td>Average time (us) from the preemption request to preemption start.
<br/>
This is an average because preemption can happen more than once. In practice, it is unlikely the same draw call will be preempted multiple times. The same set of metrics are used at various levels of granularity, so for cases where this is used over a longer time period multiple preemptions would be more likely.
   </td>
  </tr>
</table>

## See Also

* [Use RenderDoc Meta Fork for GPU Profiling](/documentation/unity/ts-renderdoc-for-oculus/)
* [Performing a Draw Call Trace](/documentation/unity/ts-renderdoc-drawcall/)

* [Developer Tools for Meta Quest](/resources/developer-tools/)