tool
extends EditorPlugin

func _enter_tree():
 add_custom_type(
   "FirstPersonRaycaster",
   "Node2D",
   preload("res://addons/FirstPersonRaycaster/src/raycaster.cs"),
   preload("res://addons/FirstPersonRaycaster/icon.png")
 )
 pass
 
func _exit_tree():
 remove_custom_type("FirstPersonRaycaster")
 pass