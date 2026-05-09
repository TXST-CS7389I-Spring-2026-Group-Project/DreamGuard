# DreamGuard Study

## Research Motivation

- VR users lose awareness of their physical space
- Meta's default Guardian: a grid that appears at the play-area boundary
  - Passthrough if you leave the set boundary or double tap your headset
- The system only knows when to passthrough based on where you are relative to the defined boundary

## Conditions

| Room | Condition | Trigger | Aware of |
|------|-----------|---------|----------|
| 1 | Guardian Grid (Meta default) | Default | Default |
| 2 | Window | Always on, forward-facing | What's in front of you |
| 3 | Window + Detection | YOLO object recognition | Anything in view of the external cameras |
| 4 | Depth Bubble | Depth threshold (Meta Depth API) | Anything in view of the external depth sensors |

Passthrough technique implementations: `src/dreamguard/unity/Runtime/Passthrough`

## Study Design

4 maze rooms, each with 4 collectible orbs. Collecting the 2nd orb activates the room's passthrough technique for 15 seconds before deactivating. An arrow UI on the bottom of the screen guides the player to the next orb; a counter displays orbs collected and remaining. The player is blocked from entering the next room until all 4 orbs are collected.

Data collection: `docs/LOGGING.md`

## Discussion

- **Guardian Grid**: Minimal dynamic intrusion detection and developer control
- **Window**: Simplest, but limited by direction and always obscures some virtual world
- **Window + Detection**: High potential; detection can prioritize events by class (e.g., increase sensitivity to people, ignore the user's own hands or monitor), but limited by detection model and compute
- **Depth Bubble**: Most flexible using a fine-grained depth map, allowing minimal disruption to the virtual world view; can enable virtual-heavy mixed reality applications

---

## Independent Variable

Passthrough technique — 4 levels, within-subjects, fixed order (not counterbalanced).

## Dependent Variables

- Safety awareness (A1), system confidence (A2) — perceived protection
- Presence (A3), disruption (A4) — immersion cost
- Trust (A5), false alarm perception (A6) — reliability
- Forced rankings (B1–B3) — comparative preference
- Overall preference (B4) — stated adoption intent
- Activation latency — `TRIGGER.ts − ROOM_HALFWAY.ts` (objective, from logs)
- Qualitative (A7, B5–B7, C1–C5)

## Confounds (critical — no counterbalancing)

1. **Order effects** — participants always experience the same sequence. Fatigue, learning, and anchoring all accumulate in one direction. This is the study's biggest internal validity risk. Any differences between rooms are entangled with room position.
2. **Practice effects** — participants get faster and more comfortable with the orb task over time; later rooms may feel less cognitively demanding regardless of technique.
3. **Carryover / contrast effects** — each technique is rated relative to what came before. Room 2 (Window) is always compared against Room 1 (Grid); Room 4 (Depth Bubble) benefits from contrast with three prior conditions.
4. **Room content differences** — rooms have different virtual contents (objects, décor) despite equal physical size. Content could independently affect immersion and perceived safety.
5. **Novelty / habituation** — first-room novelty is confounded with the Guardian Grid condition specifically.

## Controlled Variables

- Physical room dimensions (identical across all 4 rooms)
- Task structure (4 orbs per room, 2nd orb triggers passthrough, all 4 required to advance)
- Navigation affordance (arrow UI present throughout)
- Trigger mechanism (always orb 2 of 4)

## Nuisance Variables (not controlled, should be measured/reported)

- Prior VR experience (collected in demographics)
- Prior Meta Quest experience
- Individual sensitivity to presence disruption

---

**Key design note:** The fixed linear order means between-condition differences cannot be claimed as order-independent. Frame this honestly as a pilot/within-subjects sequential design, and note counterbalancing as future work.
