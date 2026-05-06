# Unity Colocation Overview

**Documentation Index:** Learn about unity colocation overview in this documentation.

---

---
title: "Colocation Overview"
description: "Colocation enables multiple Meta Quest users in the same physical space to see and interact with shared virtual objects."
last_updated: "2025-04-22"
---

## What is Colocation and how can it be used?

Colocation is the concept of multiple users being in the same physical space but can interact with the same virtual objects in the physical space. Colocated apps generally enable the following use cases:

 - Same-room collaboration and productivity
 - Shared and social experiences
 - Local multiplayer experiences

To enable a colocated experience using Meta's SDKs generally requires 2 Major Steps

### Step 1: Group Formation

Group formation enables quest users to discover and advertise data to each other. Colocation Discovery is part of the Meta XR Core SDK that allows quest users to find each other and enable group formation.

### Step 2: Anchor Sharing/Loading

Anchor Sharing/Loading enables quest users to share a point of reference (i.e. an anchor) so that each users coordinate frames are aligned. There are two types of Anchors that can be shared

  1) Spatial Anchors - Anchors that can be created as an arbitrary point in the physical world. For colocation, spatial anchors are used when your app does not need awareness of the physical environment (i.e. walls, floor, ceiling, couch, etc.)

  2) Scene Anchors - Anchors that are created from the physical environment (i.e. walls, floor, ceiling, couch etc.) by running Space Setup. For colocation, scene anchors are used when your app needs awareness of the physical environment.

## Understanding how to Colocation works

### Using Spatial Anchors

### Using Scene Anchors