# Metroidvania

## Features: 
- Basic character movement - left / right movement, jump, descent
- Adding the hero's attack after pressing a button on the keyboard
- Addition of a simple money-collecting system upon entry into cash. Information about the collected money should be on the UI
- Addition of a simple hit point system for the player. Information about your health points should be on the UI (as a rule, the player has 3 health points)
- Implementation of a simple "mushroom" enemy with patrol points that:
    1) If there are no designated patrol points, it stands still.
    2) If it has defined patrol points, it moves between them.
    3) Has 2 health points. One hit of the player takes 1 life point. After losing all points, he dies
    4) If a player enters an opponent, he takes 1 point of damage and is pushed back
- After losing all hit points, the player dies and cannot be controlled
- The animations change depending on the character's condition
- Create your own level using Tilemaps and the included sprite atlases.
- Adding a new type of enemy - Goblin, which can attack the player if he gets close to a certain distance. Like a mushroom, it can stand still or patrol, depending on the settings in the inspector.
- Project execution using GIT
- Camera shake upon player & enemy taking damage
