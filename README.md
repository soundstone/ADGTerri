ADGTerri
========
This is Team 2 Advanced Gameplay project featuring Terri Turkey and his great escape.

Game1 passes control to the GameManager which decides if we are at Title Screen or Menu Screen or Playing. If Title - 
it displays the title. If Menu control passes to Menu Manager.

MenuManager decides which menu item is currently selected and draws it differently. The menuStates are Begin, HowTo, and
Exit. Begin sends control back to GameManager and GameManager sets gameState to Playing.

Once playing GameManager creates the level and fills it with platforms and the player. GameManager also holds collision
for the platforms and the player. 
