tool
extends EditorPlugin

func _enter_tree():
 add_custom_type(
   "FirstPersonRaycaster",
   "Node2D",
   preload("res://addons/FirstPersonRaycasterCSharp/src/Raycaster.cs"),
   preload("res://addons/FirstPersonRaycasterCSharp/icon.png")
 )
 pass
 
func _exit_tree():
 remove_custom_type("FirstPersonRaycaster")
 pass