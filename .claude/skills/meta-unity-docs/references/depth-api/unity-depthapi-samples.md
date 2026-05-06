# Unity Depthapi Samples

**Documentation Index:** Learn about unity depthapi samples in this documentation.

---

---
title: "Depth API sample and support packages"
description: "Links to the github Depth API sample and support packages."
---

A great place to start with the Depth API in Unity is [the GitHub sample](https://github.com/oculus-samples/Unity-DepthAPI). It has a set of simple scenes that showcase features such as hand removal and environment depth bias. It also contains a set of support packages that provide useful shaders.

## Sample

The sample contains various use cases for Depth API. More detailed information is covered in the documentation on the GitHub page itself.

### OcclusionToggler

This scene showcases the general setup of the occlusion feature and how to switch between different modes.

### SceneAPIPlacement

This scene covers the usage of EnvironmentDepthBias.

### HandsRemoval

This scene covers the usage of the hands removal feature and replaces them with a cutout version of tracked hands to provide an alternative way of having hands occlusion.

## Support packages

The GitHub repository contains two useful packages that supply shaders with occlusion support, such as the standard Unity shaders. The documentation covers their usage and installation in more detail.

The `com.meta.xr.depthapi` package is the first package and contains standard shaders for BiRP.

<table>
  <tr>
   <td><strong>Unity shader</strong>
   </td>
   <td><strong>Depth API shader</strong>
   </td>
  </tr>
  <tr>
   <td>Standard
   </td>
   <td>Occlusion Standard
   </td>
  </tr>
  <tr>
   <td>ParticleStandardUnlit
   </td>
   <td>OcclusionParticleStandardUnlit
   </td>
  </tr>
</table>

The `com.meta.xr.depthapi.urp` package is the second package and contains standard shaders for URP.

These are the shaders that come prepackaged with the Depth API for URP:
<table>
  <tr>
   <td><strong>Unity Shader</strong>

   </td>
   <td><strong>Depth API shader</strong>

   </td>
  </tr>
  <tr>
   <td>Lit

   </td>
   <td>Occlusion Lit

   </td>
  </tr>
  <tr>
   <td>Unlit

   </td>
   <td>Occlusion Unlit

   </td>
  </tr>
  <tr>
   <td>Simple Lit

   </td>
   <td>Occlusion Simple Lit

   </td>
  </tr>
  <tr>
   <td>Baked Lit

   </td>
   <td>Occlusion Baked Lit

   </td>
  </tr>
  <tr>
   <td>Particles / Unlit (/Lit / Simple Lit)

   </td>
   <td>Occlusion Particles / Unlit (/Lit / Simple Lit)

   </td>
  </tr>
</table>

## Learn more

See the [Unity Depth API GitHub repo](https://github.com/oculus-samples/Unity-DepthAPI) for samples and support packages, along with detailed documentation on their usage.