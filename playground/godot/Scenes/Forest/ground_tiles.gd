extends Node3D

@export var tile_scene: PackedScene
@export var grid_size: int = 15        # tiles per side (covers -14 to +14 m)
@export var tile_spacing: float = 2.0
@export var tile_overlap: float = 1.02 # scale factor to close seam gaps

func _ready() -> void:
	var half := (grid_size - 1) / 2.0 * tile_spacing
	for row in range(grid_size):
		for col in range(grid_size):
			var tile := tile_scene.instantiate()
			add_child(tile)
			tile.position = Vector3(
				-half + col * tile_spacing,
				-1.0,
				-half + row * tile_spacing
			)
			tile.scale = Vector3(tile_overlap, 1.0, tile_overlap)
