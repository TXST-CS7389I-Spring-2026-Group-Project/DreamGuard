# Move Scripts Shaders

**Documentation Index:** Learn about move scripts shaders in this documentation.

---

---
title: "Scripts and Shaders"
description: "Locate and understand the Movement SDK scripts and shaders available for Unity body tracking and retargeting."
last_updated: "2025-05-01"
---

## Scripts

Scripts are in `Packages/Meta XR Movement SDK/Runtime/Scripts` and `Packages/Meta XR Movement SDK/Editor`.

## Shaders

### MovementPBRMetallic

Metallic flow PBR shader similar to Unity standard’s metallic PBR. It includes more effects such as diffuse wrap to provide a softer appearance for skin materials, and better specularity using area light sampling.

### MovementPBRSpecular

Specular flow PBR shader similar to Unity standard’s specular PBR. It includes more effects such as diffuse wrap to provide a softer appearance for skin materials, and better specularity using area light sampling.

### ProceduralGradientSkybox

Produces a skybox by having a gradient that shifts between colors that exist at the bottom of the skybox, the horizon, and the top of the skybox.