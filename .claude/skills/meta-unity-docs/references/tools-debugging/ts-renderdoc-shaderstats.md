# Ts Renderdoc Shaderstats

**Documentation Index:** Learn about ts renderdoc shaderstats in this documentation.

---

---
title: "Accessing Vulkan Shader Stats"
description: "View Vulkan shader performance statistics on Meta Quest through RenderDoc Meta Fork."
last_updated: "2024-12-01"
---

RenderDoc Meta Fork offers access to Meta Quest headset shader stats through the Vulkan extension `KHR_pipeline_executable_properties`. RenderDoc Meta Fork implements this extension as a shader disassembly option in the shader module panel, so it is not necessary to write code to retrieve them.

## Taking and Loading a Capture

A capture is necessary to retrieve Vulkan shader stats. Read [Taking and Loading a Capture](/documentation/unity/ts-renderdoc-capture/) before following the instructions in this section. You can use either the profiling or non-profiling replay context when retrieving shader stats.

## Access Shader Stats

To access Vulkan shader stats in RenderDoc Meta Fork, load a capture and do the following:

1. In the **Event Browser**, select the draw call you are interested in.

2. Go to the **Pipeline State** panel. Select **Vertex Shader (VS)**, **Fragment Shader (FS)**, or **Compute Shader (CS)** and click **View**.<br>

3. The **Shader Module** panel opens. Select **KHR_pipeline_executable_properties** from the drop-down menu to see the Vulkan shader stats.

## Shader Stats Descriptions

### Instruction Count All

| | |
| --- | --- |
| Description | Total count of all instructions. Complex shaders with high instruction counts may have long execution times. More instructions mean longer shader execution time in most cases. An instruction count larger than instruction cache size causes `I$` miss and harms shader performance. |
| Value Range | Positive integer |
| Optimization | Avoid redundant operations. |

### ALU Instruction Count 32bit

| | |
| --- | --- |
| Description | Total count of all 32-bit ALU instructions. More ALU instructions might not affect performance, but can consume more power. |
| Value Range | 0 - Total instructions |
| Optimization | Remove redundant computations. |

### ALU Instruction Count 16bit

| | |
| --- | --- |
| Description | Total count of all 16-bit ALU instructions. 16-bit ALU instructions perform better and use less register space than 32-bit instructions. These are similar to full precision ALU instructions. Converting full-precision ALU instructions into half-precision ALU instructions can improve performance in ALU-bound shaders. |
| Value Range | 0 - Total instructions |
| Optimization | Use lower precision as much as possible. |

### Complex Instruction Count

| | |
| --- | --- |
| Description | Total count of all complex instructions (sin, cos, and so on). EFU instructions are more time consuming than ALU instructions. Short latency sync instruction is needed for the dependency between an EFU and its use instruction. |
| Value Range | 0 - Total instructions |
| Optimization | If EFU instructions must be used, group some of them together to reduce short sync latency instructions. Aggressively grouping is not recommended. |

### Texture Read Instruction Count

| | |
| --- | --- |
| Description | Total count of all texture read instructions. Texture fetch causes memory access latency, which must be hidden by ALU instructions. Latency is decided by the number of texture fetches and their locality in cache. |
| Value Range | 0 - Total instructions |
| Optimization | Texture fetches that can be coalesced should be grouped together to avoid cache thrashing. Limit the number of texture fetches in each group to be below 15. |
| Additional Information | Generally, `VkImage` reads with or without a `VkSampler`. Also includes input attachment reads. |

### Flow Control Instruction Count

| | |
| --- | --- |
| Description | Total count of all flow control instructions. More flow control instructions mean more divergence in shader code, which can harm shader performance. A control flow instruction takes more execution time than an ALU instruction. |
| Value Range | 0 - Total instructions |
| Optimization | Reduce instructions inside control flow blocks as much as possible so that the control flow can be flattened by the compiler. |

### Barrier and Fence Instruction Count

| | |
| --- | --- |
| Description | Total count of all barrier and fence instructions. Global sync reduces wave parallelism and extends application execution time. More power is consumed if the execution time is longer. |
| Value Range | 0 - Total instructions |
| Optimization | Avoid frequent global synchronization. |
| Additional Information | Generally, these are `op*Barrier` instructions in the shader. |

### Short Latency Sync Instruction Count

| | |
| --- | --- |
| Description | Total count of all short latency sync instructions. Shader instruction execution can be delayed if it is too close to the instruction that causes this short latency sync, and there is no other wave to hide this latency |
| Value Range | 0 - Total  instructions |
| Optimization | Put EFU instructions together if this does not increase the def-use distance too much. |

### Long Latency Sync Instruction Count

| | |
| --- | --- |
| Description | Total count of all long latency sync instructions. This is caused by memory access and it can delay shader instruction execution if the latency is long and there are not enough waves to hide the latency. |
| Value Range | 0 - Total instructions |
| Optimization | Latency is determined by multiple factors. Improving the locality of memory instructions can help. |

### Full Precision Register Footprint Per Shader Instance

| | |
| --- | --- |
| Description | Number of 128-bit registers used by each shader instance. Each 128-bit register can store 4 FP32 values. This is similar to the number of registers. This is the full-precision registers needed by the shader. |
| Value Range | 0 - Max full registers allowed by ISA |
| Optimization | In addition to the items related to the number of registers, use lower precision variables to avoid high register use in a shader. |

### Half Precision Register Footprint Per Shader Instance

| | |
| --- | --- |
| Description | Number of 64-bit registers used by each shader instance. Each 64-bit register can store 4 FP16 values. This is similar to the number of registers. This is the half-precision registers needed by the shader. |
| Value Range | 0 - Max half registers allowed by ISA |
| Optimization | Try to use half-precision variables, but avoid excessive mixed-precision operations. |

### Overall Register Footprint Per Shader Instance

| | |
| --- | --- |
| Description | Number of 128-bit registers used by each shader instance. Each 128-bit register can store 4 FP32 values, or 8 FP16 values. Using too many registers reduces active waves in shader execution and can cause register spill in some cases. Higher active wave counts can hide longer memory latency. However, low active wave count cannot hide latency and ALU utilization will be low. |
| Value Range | 0 - Max registers allowed by ISA |
| Optimization | Avoid large vector variables with dynamic accesses. Use a constant array/vector if possible, instead of declaring an array/vector and assigning constant values to it. |

### Scratch Memory Usage Per Shader Instance

| | |
| --- | --- |
| Description | Number of 128-bit slots of scratch memory used by each shader instance.  |
| Value Range | 0 - Max scratch memory size |
| Optimization | Avoid use of scratch memory. If the shader uses any scratch memory, it will perform poorly. |

### Output Component Count

| | |
| --- | --- |
| Description | Total count of all shader stage output components. |
| Value Range | 0 - 128 |

### Input Component Count

| | |
| --- | --- |
| Description | Total count of all shader stage input components. |
| Value Range | 0 - 128 |

### Shader Processor Utilization Percentage

| | |
| --- | --- |
| Description | The maximum shader processor utilization for the shader. A higher shader processor utilization percentage means more parallelism in shader execution. Low fiber count can cause memory access latency and idle time on the ALU or other GPU components. |
| Value Range | 0 - 100% |
| Optimization | Try to lower register use to increase total fiber count. If this number is low, the shader may perform poorly. |

### Memory Read Instruction Count

| | |
| --- | --- |
| Description | Total count of all memory read instructions. This is similar  to texture fetches, but to different memory access units |
| Value Range | 0 - Total instructions |
| Optimization | Similar to texture fetches. |
| Additional Information | Generally, these are `VkImage`/`VkBuffer` reads through a storage descriptor. |

### Memory Write Instruction Count

| | |
| --- | --- |
| Description | Total count of all memory write instructions. |
| Value Range | 0 - Total instructions |
| Optimization | Use vector store when possible. Scattered memory store instructions to contiguous memory locations can harm performance.  |
| Additional Information | Generally, these are `VkImage`/`VkBuffer` writes through a storage descriptor. |

## See Also

* [Use RenderDoc Meta Fork for GPU Profiling](/documentation/unity/ts-renderdoc-for-oculus/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)