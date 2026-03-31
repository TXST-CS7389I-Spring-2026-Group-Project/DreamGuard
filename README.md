# DreamGuard: Immersion-Preserving Passthrough Safety Transitions in VR

## Overview

DreamGuard is a proximity-triggered safety system for consumer VR headsets that replaces the standard guardian grid overlay with gradual, aesthetically-matched visual transitions. Instead of jarring grid interruptions, DreamGuard blends the passthrough camera feed into the virtual scene using soft fog, light bloom, or environment-themed effects when a user approaches a physical boundary or nearby person.

The core research question: does a seamless aesthetic blend produce fewer cybersickness symptoms and less presence disruption than the standard guardian grid, while maintaining equivalent safety outcomes?

## Motivation

Current VR safety systems (e.g., Meta Quest Guardian) interrupt the virtual experience with a hard grid overlay that has no aesthetic relationship to the virtual world. This abrupt transition can cause disorientation and contribute to cybersickness. DreamGuard treats the safety transition itself as a designable interaction artifact, operationalizing Milgram and Kishino's Reality-Virtuality Continuum dynamically rather than as a binary mode switch.

## Proposed System

- Depth estimation or person detection via headset cameras triggers the transition before collision is imminent
- Passthrough is gradually blended into the virtual scene using visually coherent effects
- The system fades back to full VR once the user returns to safety

## Proposed Study

A within-subjects lab study comparing three conditions:
1. Standard Meta Quest guardian grid
2. DreamGuard aesthetic passthrough blend
3. No-warning control

Participants complete an immersive exploration task while scripted proximity events occur at fixed intervals. Measures include presence (IPQ), cybersickness (SSQ), and safety outcomes via motion-capture logging.

## Status

Early-stage research project. Design and implementation in progress.
