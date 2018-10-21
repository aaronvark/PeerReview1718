#pragma once
#include <vector>
#include "ChunkGenerator.h"
#include "Camera.h"
#include "ChunkManager.h"

class WorldGeneration {
private: //section for constants
	const int biomePool = 2;
private: //section for private variables
	int startBiomeInterval, biomeInterval;
	int size, amtOfChunks;
	float heightScale;
	biome type;
	ChunkGenerator* chunkGen;
	ChunkManager* man;
	Camera* player;
private: //section for private functions
	std::vector<glm::vec2> getNeighbours();
	void pickNextBiome();
public: //section for public functions
	WorldGeneration(ChunkManager* man, Camera* player);
	void generateWorld(int size, int height, int amtOfChunks, int amtOfOctaves, float heightScale, int chanceAtBiome);
	void updateChunks();
	~WorldGeneration();
};