# ProyectoFinal
Our final project goal was to create a tower defense game, where the player attempts to fend off enemies as a horde of them make their way to destroy the towers. The presented game is a prototype with the basic features and functions, without implementation of game mechanics that are designed to add more control to the player in the managing of enemies.

## Terrain

We started off the project by creating a terrain 3D GameObject. Following are the terrain position coordinates and dimensions:

**Position:**
- Position X = -75  
- Position Y = 9  
- Position Z = -75  

**Dimensions:**
- Terrain Width = 150  
- Terrain Length = 150  
- Terrain Height = 600  

Terrain was then modified to simulate a rocky wasteland and a space to accommodate the battlefield geometry, using Unity’s *Raise or Lower Terrain*. Textures were then added to make the arena look natural. An area of 75 x 75 was left to position the arena where gameplay will take place.

## Pro-Builder

Once the terrain is set, the next step was creating the arena itself. The first step was creating a cube shape using ProBuilder. The following are the arena dimensions for the top face:

**Position:**
- Position X = 0  
- Position Y = 12  
- Position Z = 0  

**Dimensions:**
- X = 73.06591  
- Y = 1.345045  
- Z = 70.52448  

After creating the geometry, it was subdivided until obtaining a symmetrical pattern. This was done to allow inward extrusion of selected sections to position towers.

Random vertices in the outer small faces were then selected and slightly raised above the rest of the vertices to simulate mounds.

Next, the faces that would be extruded to place towers were selected.

After applying these modifications, textures were applied so that the ProBuilder geometry blended in with the created terrain.

A steep drop-off was created after the ProBuilder arena to establish a clear separation between playable and unplayable areas for enemies. Additionally, care was taken to ensure that the ProBuilder cube was not in direct contact with the terrain, as this can cause issues when applying the NavMesh surface for enemy behavior.

## Towers

There are two types of towers in the game:
- Main Tower  
- Outer Towers  

Following are the position coordinates and dimensions of all towers.

### 1st Quadrant Tower

**Position:**
- X = 11.4  
- Y = 16.7  
- Z = 11  

**Dimensions:**
- X = 4  
- Y = 10  
- Z = 4  

### 2nd Quadrant Tower

**Position:**
- X = -11.4  
- Y = 16.7  
- Z = 11  

**Dimensions:**
- X = 4  
- Y = 10  
- Z = 4  

### 3rd Quadrant Tower

**Position:**
- X = -11.4  
- Y = 16.7  
- Z = -11  

**Dimensions:**
- X = 4  
- Y = 10  
- Z = 4  

### 4th Quadrant Tower

**Position:**
- X = 11.4  
- Y = 16.7  
- Z = -11  

**Dimensions:**
- X = 4  
- Y = 10  
- Z = 4  

### Main Tower

**Position:**
- X = 0  
- Y = 18.6  
- Z = 0  

**Dimensions:**
- X = 8.5  
- Y = 15  
- Z = 8.5  

After following these instructions, the terrain and game area should look something like this:
