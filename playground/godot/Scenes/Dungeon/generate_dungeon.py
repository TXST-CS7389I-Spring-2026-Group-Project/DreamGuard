#!/usr/bin/env python3
"""
Dungeon.tscn generator for DreamGuard.

Layout overview (each grid cell = 2 world-units):

  Center ring  – 7×7 outer / 3×3 hollow interior → 2-tile-wide corridors on ALL sides
  North        – 2×2-tile hallway → 3×3 room → 2-wide dead-end branch west
  South        – 2×2-tile hallway → 3×3 room → 2-wide dead-end spur south
  East         – 2×2-tile hallway → 2×2 room → 2-wide loopback back to ring bottom
  West         – 2×2-tile hallway → 2×2 room (dead-end)

All corridors ≥ 2 tiles (4 world-units) wide.

Wall-rotation convention (KayKit default face = −Z / "forward"):
  North wall  (face →+Z):  180°
  South wall  (face →−Z):    0°
  East  wall  (face →−X):   90°
  West  wall  (face →+X):  270°

Corner rotations (convex exterior corners):
  NW: 0°   NE: 90°   SE: 180°   SW: 270°
"""

import os

OUT = os.path.join(os.path.dirname(__file__), "Dungeon.tscn")

# ─── Floor layout ────────────────────────────────────────────────────────────
floor = set()

# ── Center ring ──────────────────────────────────────────────────────────────
# 7×7 outer (cx/cz -3..3), 3×3 hollow interior (cx/cz -1..1).
# Every cell where |cx|>=2 OR |cz|>=2 (within the 7×7 box) is a ring tile.
# This gives 2-tile-wide corridors on every side.
for cx in range(-3, 4):
    for cz in range(-3, 4):
        if abs(cx) >= 2 or abs(cz) >= 2:
            floor.add((cx, cz))

# ── North hallway (2 wide: cx = -1, 0) ──────────────────────────────────────
for cx in (-1, 0):
    floor.add((cx, -4))
    floor.add((cx, -5))

# ── North room (5×5: cx = -2..2, cz = -6..-10) ───────────────────────────────
for cx in range(-2, 3):
    for cz in range(-6, -11, -1):   # -6,-7,-8,-9,-10
        floor.add((cx, cz))

# ── Dead-end west from north room (2 wide in Z, 3 deep going west) ────────────
# Exits the west wall of the north room at mid-height (cz=-8,-9)
for cz in (-8, -9):
    floor.add((-3, cz))
    floor.add((-4, cz))
    floor.add((-5, cz))

# ── South hallway (2 wide: cx = -1, 0) ──────────────────────────────────────
for cx in (-1, 0):
    floor.add((cx, 4))
    floor.add((cx, 5))

# ── South room (5×5: cx = -2..2, cz = 6..10) ────────────────────────────────
for cx in range(-2, 3):
    for cz in range(6, 11):         # 6,7,8,9,10
        floor.add((cx, cz))

# ── Dead-end south from south room (2 wide: cx = -1, 0, 3 deep) ──────────────
for cx in (-1, 0):
    floor.add((cx, 11))
    floor.add((cx, 12))
    floor.add((cx, 13))

# ── East hallway (2 wide: cz = -1, 0) ────────────────────────────────────────
for cz in (-1, 0):
    floor.add((4, cz))
    floor.add((5, cz))

# ── East room (5×5: cx = 6..10, cz = -2..2) ─────────────────────────────────
for cx in range(6, 11):
    for cz in range(-2, 3):         # -2,-1,0,1,2
        floor.add((cx, cz))

# ── East loopback ─────────────────────────────────────────────────────────────
# Exits SW corner of east room (cx=6,7 at cz=3 — one step south of room edge).
# South leg: 2-wide going south (cz=3,4), then west leg back to ring (3,3).
for cx in (6, 7):
    floor.add((cx, 3))
    floor.add((cx, 4))
for cx in (4, 5):                   # (4,3) adj to ring (3,3) ✓
    floor.add((cx, 3))
    floor.add((cx, 4))

# ── West hallway (2 wide: cz = -1, 0) ────────────────────────────────────────
for cz in (-1, 0):
    floor.add((-4, cz))
    floor.add((-5, cz))

# ── West room (5×5: cx = -6..-10, cz = -2..2) ───────────────────────────────
for cx in range(-6, -11, -1):       # -6,-7,-8,-9,-10
    for cz in range(-2, 3):
        floor.add((cx, cz))

# ─── Wall generation ─────────────────────────────────────────────────────────
ROT = {
    0:   "1, 0, 0, 0, 1, 0, 0, 0, 1",
    90:  "0, 0, -1, 0, 1, 0, 1, 0, 0",
    180: "-1, 0, 0, 0, 1, 0, 0, 0, -1",
    270: "0, 0, 1, 0, 1, 0, -1, 0, 0",
}

def tf(rot, wx, wy, wz):
    return f"Transform3D({ROT[rot]}, {wx}, {wy}, {wz})"

SIDE_RULES = [
    # (ndx, ndz, woff_x, woff_z, rot)
    ( 0, -1,  0, -1, 180),   # north
    ( 0,  1,  0,  1,   0),   # south
    ( 1,  0,  1,  0,  90),   # east
    (-1,  0, -1,  0, 270),   # west
]

CORNER_RULES = [
    # (adj1_dx, adj1_dz, adj2_dx, adj2_dz, coff_x, coff_z, rot)
    (-1,  0,  0, -1, -1, -1,   0),   # NW 0°
    ( 1,  0,  0, -1,  1, -1,  90),   # NE 90°
    ( 1,  0,  0,  1,  1,  1, 180),   # SE 180°
    (-1,  0,  0,  1, -1,  1, 270),   # SW 270°
]

walls   = []
corners = []
wset    = set()
cset    = set()

for (cx, cz) in floor:
    wx, wz = cx * 2, cz * 2

    for (ndx, ndz, wox, woz, rot) in SIDE_RULES:
        if (cx + ndx, cz + ndz) not in floor:
            key = (wx + wox, wz + woz)
            if key not in wset:
                walls.append((wx + wox, wz + woz, rot))
                wset.add(key)

    for (a1x, a1z, a2x, a2z, cox, coz, rot) in CORNER_RULES:
        if (cx + a1x, cz + a1z) not in floor and (cx + a2x, cz + a2z) not in floor:
            key = (wx + cox, wz + coz)
            if key not in cset:
                corners.append((wx + cox, wz + coz, rot))
                cset.add(key)

walls.sort()
corners.sort()

print(f"Floor tiles : {len(floor)}")
print(f"Wall segs   : {len(walls)}")
print(f"Corners     : {len(corners)}")

# ─── Connectivity check ───────────────────────────────────────────────────────
# BFS from an arbitrary floor cell to verify all cells are reachable.
start = next(iter(floor))
visited = {start}
queue  = [start]
while queue:
    cx, cz = queue.pop()
    for dx, dz in ((0,1),(0,-1),(1,0),(-1,0)):
        nb = (cx+dx, cz+dz)
        if nb in floor and nb not in visited:
            visited.add(nb)
            queue.append(nb)
isolated = floor - visited
if isolated:
    print(f"WARNING: {len(isolated)} isolated tile(s): {sorted(isolated)[:10]}")
else:
    print("Connectivity: all tiles reachable ✓")

# ─── Props (resource_id, wx, wy, wz, rot) ────────────────────────────────────
# World positions = cx*2, cz*2  for grid cell (cx, cz).
# Torch rotations:  north-wall → 180°  south-wall → 0°
#                   east-wall  →  90°  west-wall  → 270°

PROPS = [
    # ── Ring – outer-wall torches ─────────────────────────────────────────────
    # Wall positions: north wall of tile (cx, cz=-3) sits at z = cz*2-1 = -7
    #                 west  wall of tile (cx=-3, cz)  sits at x = cx*2-1 = -7, etc.
    ("torch_mounted", -4, 1.5,  -7, 180),   # N outer wall, tile(-2,-3)
    ("torch_mounted",  4, 1.5,  -7, 180),   # N outer wall, tile( 2,-3)
    ("torch_mounted", -4, 1.5,   7,   0),   # S outer wall, tile(-2, 3)
    ("torch_mounted",  4, 1.5,   7,   0),   # S outer wall, tile( 2, 3)
    ("torch_mounted", -7, 1.5,  -2, 270),   # W outer wall, tile(-3,-1)
    ("torch_mounted", -7, 1.5,   2, 270),   # W outer wall, tile(-3, 1)
    ("torch_mounted",  7, 1.5,  -2,  90),   # E outer wall, tile( 3,-1)
    ("torch_mounted",  7, 1.5,   2,  90),   # E outer wall, tile( 3, 1)

    # ── Hallway torches ───────────────────────────────────────────────────────
    # North hall: east wall of tile(0,-4) → x=0*2+1=1, z=-4*2=-8
    ("torch_mounted",  1, 1.5,  -8,  90),
    ("torch_mounted",  1, 1.5, -10,  90),   # tile(0,-5)
    # South hall: east wall of tile(0,4) → x=1, z=8
    ("torch_mounted",  1, 1.5,   8,  90),
    ("torch_mounted",  1, 1.5,  10,  90),   # tile(0,5)
    # East hall: north wall of tile(4,-1) → z=-1*2-1=-3, x=4*2=8
    ("torch_mounted",  8, 1.5,  -3, 180),
    # East hall: south wall of tile(4, 0) → z=0*2+1=1, x=8
    ("torch_mounted",  8, 1.5,   1,   0),
    # West hall: north wall of tile(-4,-1) → z=-3, x=-4*2=-8
    ("torch_mounted", -8, 1.5,  -3, 180),
    # West hall: south wall of tile(-4, 0) → z=1, x=-8
    ("torch_mounted", -8, 1.5,   1,   0),

    # ── North room (5×5: cx=-2..2, cz=-6..-10) ────────────────────────────────
    # North wall tiles at cz=-10 → wall z = -10*2-1 = -21
    # West wall tiles at cx=-2  → wall x = -2*2-1 = -5
    # East wall tiles at cx= 2  → wall x =  2*2+1 =  5
    ("column",        -2,   0, -14,   0),
    ("column",         2,   0, -14,   0),
    ("column",        -2,   0, -18,   0),
    ("column",         2,   0, -18,   0),
    ("chest_gold",     0,   0, -16,   0),
    ("torch_mounted", -2, 1.5, -21, 180),   # N wall, tile(-1,-10)
    ("torch_mounted",  2, 1.5, -21, 180),   # N wall, tile( 1,-10)
    ("torch_mounted", -5, 1.5, -16, 270),   # W wall, tile(-2,-8)
    ("torch_mounted",  5, 1.5, -16,  90),   # E wall, tile( 2,-8)

    # ── North dead-end (cx=-3..-5, cz=-8,-9) ────────────────────────────────
    ("rubble_large",  -8,  0, -18,   0),
    ("rubble_large", -10,  0, -16, 180),

    # ── East room (5×5: cx=6..10, cz=-2..2) ──────────────────────────────────
    # East wall tiles at cx=10 → wall x = 10*2+1 = 21
    # North wall tiles at cz=-2 → wall z = -2*2-1 = -5
    ("column",        14,   0,  -4,   0),
    ("column",        18,   0,  -4,   0),
    ("column",        14,   0,   4,   0),
    ("column",        18,   0,   4,   0),
    ("chest_gold",    16,   0,   0,   0),
    ("barrel_large",  20,   0,  -2,   0),
    ("barrel_large",  20,   0,   2,   0),
    ("torch_mounted", 21, 1.5,  -2,  90),   # E wall, tile(10,-1)
    ("torch_mounted", 21, 1.5,   2,  90),   # E wall, tile(10, 1)
    ("torch_mounted", 16, 1.5,  -5, 180),   # N wall, tile( 8,-2)

    # ── East loopback ─────────────────────────────────────────────────────────
    ("rubble_large",  12,   0,   6,  90),

    # ── South room (5×5: cx=-2..2, cz=6..10) ────────────────────────────────
    # South wall tiles at cz=10 → wall z = 10*2+1 = 21
    ("column",        -2,   0,  14,   0),
    ("column",         2,   0,  14,   0),
    ("column",        -2,   0,  18,   0),
    ("column",         2,   0,  18,   0),
    ("barrel_large",  -4,   0,  12,   0),
    ("barrel_large",   4,   0,  12,   0),
    ("rubble_large",   4,   0,  20,   0),
    ("torch_mounted", -2, 1.5,  21,   0),   # S wall, tile(-1,10)
    ("torch_mounted",  2, 1.5,  21,   0),   # S wall, tile( 1,10)
    ("torch_mounted", -5, 1.5,  16, 270),   # W wall, tile(-2, 8)
    ("torch_mounted",  5, 1.5,  16,  90),   # E wall, tile( 2, 8)

    # ── South dead-end ────────────────────────────────────────────────────────
    ("rubble_large",   0,   0,  26,   0),

    # ── West room (5×5: cx=-6..-10, cz=-2..2) ────────────────────────────────
    # West wall tiles at cx=-10 → wall x = -10*2-1 = -21
    # North wall tiles at cz=-2  → wall z = -5
    ("column",       -14,   0,  -4,   0),
    ("column",       -18,   0,  -4,   0),
    ("column",       -14,   0,   4,   0),
    ("column",       -18,   0,   4,   0),
    ("barrel_large", -20,   0,  -2,   0),
    ("barrel_large", -20,   0,   2,   0),
    ("rubble_large", -12,   0,   4,  90),
    ("torch_mounted",-21, 1.5,  -2, 270),   # W wall, tile(-10,-1)
    ("torch_mounted",-21, 1.5,   2, 270),   # W wall, tile(-10, 1)
    ("torch_mounted",-16, 1.5,  -5, 180),   # N wall, tile(-8,-2)
]

# Ambient corridor lights (world x, z — placed at y=2.0)
AMBIENT_LIGHTS = [
    # Ring quadrants
    (-4, -6), ( 4, -6),
    (-4,  6), ( 4,  6),
    (-6, -2), (-6,  2),
    ( 6, -2), ( 6,  2),
    # Hallways
    (-1, -9),  ( 1, -9),
    (-1,  9),  ( 1,  9),
    ( 9, -1),  ( 9,  1),
    (-9, -1),  (-9,  1),
    # Rooms (centre of each 5×5)
    ( 0, -16),  # north room
    (16,   0),  # east room
    ( 0,  16),  # south room
    (-16,  0),  # west room
    # East loopback
    (13,  4), (9, 4),
    # Dead-ends
    (-8, -17),  # north dead-end
    (-1,  24),  # south dead-end
]

# ─── TSCN assembly ───────────────────────────────────────────────────────────
L = []

L += [
    "[gd_scene format=3]",
    "",
    '[ext_resource type="PackedScene" path="res://Scenes/Dungeon/assets/floor_tile_large.gltf" id="floor_tile_large"]',
    '[ext_resource type="PackedScene" path="res://Scenes/Dungeon/assets/wall.gltf" id="wall"]',
    '[ext_resource type="PackedScene" path="res://Scenes/Dungeon/assets/wall_corner.gltf" id="wall_corner"]',
    '[ext_resource type="PackedScene" path="res://Scenes/Dungeon/assets/torch_mounted.gltf" id="torch_mounted"]',
    '[ext_resource type="PackedScene" path="res://Scenes/Dungeon/assets/chest_gold.gltf" id="chest_gold"]',
    '[ext_resource type="PackedScene" path="res://Scenes/Dungeon/assets/barrel_large.gltf" id="barrel_large"]',
    '[ext_resource type="PackedScene" path="res://Scenes/Dungeon/assets/column.gltf" id="column"]',
    '[ext_resource type="PackedScene" path="res://Scenes/Dungeon/assets/rubble_large.gltf" id="rubble_large"]',
    "",
    '[sub_resource type="ProceduralSkyMaterial" id="CaveSkyMat"]',
    "sky_top_color = Color(0.02, 0.02, 0.04, 1)",
    "sky_horizon_color = Color(0.05, 0.03, 0.02, 1)",
    "sky_curve = 0.1",
    "ground_bottom_color = Color(0.01, 0.01, 0.01, 1)",
    "ground_horizon_color = Color(0.05, 0.03, 0.02, 1)",
    "",
    '[sub_resource type="Sky" id="CaveSky"]',
    'sky_material = SubResource("CaveSkyMat")',
    "",
    '[sub_resource type="Environment" id="CaveEnv"]',
    "background_mode = 2",
    'sky = SubResource("CaveSky")',
    "ambient_light_source = 3",
    "ambient_light_color = Color(0.08, 0.05, 0.03, 1)",
    "ambient_light_energy = 0.3",
    "tonemap_mode = 2",
    "",
    '[node name="Dungeon" type="Node3D"]',
    "",
    '[node name="WorldEnvironment" type="WorldEnvironment" parent="."]',
    'environment = SubResource("CaveEnv")',
    "",
]

# Floor
L.append('[node name="Floor" type="Node3D" parent="."]')
L.append("")
for (cx, cz) in sorted(floor):
    wx, wz = cx * 2, cz * 2
    name = f"T_{cx}_{cz}".replace("-", "n")
    L.append(f'[node name="{name}" parent="Floor" instance=ExtResource("floor_tile_large")]')
    L.append(f"transform = {tf(0, wx, 0, wz)}")
    L.append("")

# Walls
L.append('[node name="Walls" type="Node3D" parent="."]')
L.append("")
for i, (wx, wz, rot) in enumerate(walls):
    L.append(f'[node name="W{i}" parent="Walls" instance=ExtResource("wall")]')
    L.append(f"transform = {tf(rot, wx, 0, wz)}")
    L.append("")

# Corners
L.append('[node name="Corners" type="Node3D" parent="."]')
L.append("")
for i, (wx, wz, rot) in enumerate(corners):
    L.append(f'[node name="C{i}" parent="Corners" instance=ExtResource("wall_corner")]')
    L.append(f"transform = {tf(rot, wx, 0, wz)}")
    L.append("")

# Props  (torch arm extends local +Z, so add 180° to face into room)
L.append('[node name="Props" type="Node3D" parent="."]')
L.append("")
for i, (rid, wx, wy, wz, rot) in enumerate(PROPS):
    safe = rid.replace("_", "")
    node_name = f"P{i}_{safe}"
    is_torch = rid == "torch_mounted"
    actual_rot = (rot + 180) % 360 if is_torch else rot
    L.append(f'[node name="{node_name}" parent="Props" instance=ExtResource("{rid}")]')
    L.append(f"transform = {tf(actual_rot, wx, wy, wz)}")
    L.append("")
    if is_torch:
        # Embed light as child so it moves with the torch in the editor
        L.append(f'[node name="Light" type="OmniLight3D" parent="Props/{node_name}"]')
        L.append(f"transform = {tf(0, 0, 0.3, 0.3)}")
        L.append("light_color = Color(1.0, 0.6, 0.2, 1)")
        L.append("light_energy = 2.5")
        L.append("omni_range = 6.0")
        L.append("shadow_enabled = true")
        L.append("")

# Ambient fill lights
L.append('[node name="Lights" type="Node3D" parent="."]')
L.append("")
for i, (ax, az) in enumerate(AMBIENT_LIGHTS):
    L.append(f'[node name="Amb{i}" type="OmniLight3D" parent="Lights"]')
    L.append(f"transform = {tf(0, ax, 2.0, az)}")
    L.append("light_color = Color(0.55, 0.4, 0.25, 1)")
    L.append("light_energy = 0.6")
    L.append("omni_range = 7.0")
    L.append("")

with open(OUT, "w") as f:
    f.write("\n".join(L))

print(f"✓  Written → {OUT}")
