# Snowball's Voxel Merging Playground

Given a number of binary voxels, let's assume we store them in a format that is not a large bit-array, but something like...

```
int positionX;
int positionY;
int positionZ;
int sizeX;
int sizeY;
int sizeZ;
```

...for every Voxel. We'll feel the urge to merge connected voxels into larger cuboids to save storage space and also computing resources. To do this efficiently **and** fast turns out to be quite complicated. 

This little program was used as a testing platform to play with different strategies to merge multiple voxels
