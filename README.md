ADGTerri
========
This is Team 2 Advanced Gameplay project featuring Terri Turkey and his great escape.

Input or control changes should be done within the InputHelper class. 

Everything is created through the Level class which is controlled by the static GameManager. Drawing is determined by
the GameState of the GameManager - Title / Menu / Playing. 

When GameState is Menu - The static MenuManager class takes over control of the display until MenuState = playing. Then
control falls back to the GameManager.

Camera and Player classes are used locally in the Game1 class. All other entities should be loaded and used throught the
Level class. 
