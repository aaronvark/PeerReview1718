#include "ChunkGenerator.h"

ChunkGenerator::ChunkGenerator(int size, int height, int amtOfOctaves) : size(size), height(height + airLayer), amtOfOctaves(amtOfOctaves) {
	//get a random starting position for the perlin noise
	startX = rand() % 9999;
	startZ = rand() % 9999;
}

Chunk* ChunkGenerator::generateChunk(int xPos, int zPos, float heightScale, biome type) {
	Chunk* chunk = new Chunk(size, height, xPos, 0, zPos);
	std::unique_ptr<Plant> plant = std::make_unique<Plant>();
	std::unique_ptr<Tree> tree = std::make_unique<Tree>();

	if (heightScale < 1.0f) heightScale = 1.0f;

	this->heightScale = heightScale;
	this->xPos = xPos;
	this->zPos = zPos;
	waterPlane = height / 5;

	if (type == Desert) topType = Sand, middleType = Sand, bottomType = Stone; 
	if (type == Forest) topType = Grass, middleType = Dirt, bottomType = Stone;

	for (int x = 0; x < size; ++x) {
		for (int z = 0; z < size; ++z) {
			//make a vector3 containing the top layer positions (with pnoise heightmap)
			//the other blocks are spawned below/above the top layer
			topLayer = new glm::vec3(x, heights(x, z), z);
			for (int i = -airLayer; i < 1; ++i) {
				chunk->AddBlock(topLayer->x, height + i, topLayer->z, blockType::Air);
			}
			chunk->AddBlock(topLayer->x, topLayer->y, topLayer->z, topType);
			for (int i = 1; i < middleDepth + 1; ++i) {
				middleLayer = new glm::vec3(topLayer->x, topLayer->y - i, topLayer->z);
				chunk->AddBlock(middleLayer->x, middleLayer->y, middleLayer->z, middleType);
			}
			for (int i = 1; i < middleLayer->y + 1; ++i) {
				bottomLayer = new glm::vec3(middleLayer->x, middleLayer->y - i, middleLayer->z);
				chunk->AddBlock(bottomLayer->x, bottomLayer->y, bottomLayer->z, bottomType);
			}
			if (rand() % density == 1 && topLayer->y > waterPlane) {
				if (type == Desert) plant->generatePlant(topLayer, chunk, blockType::Cactus);
				if (type == Forest && topType == Grass) {
					if (rand() % 2 == 1) tree->generateTree(topLayer, chunk, treeType::Oak);
					else tree->generateTree(topLayer, chunk, treeType::Birch);
				}
			}
			chunk->AddBlock(x, -1, z, blockType::Bedrock);
			if (chunk->GetBlock(x, waterPlane, z).getType() == Air) {
				chunk->AddBlock(x, waterPlane, z, blockType::Water);
			}
		}
	}
	return chunk;
}

int ChunkGenerator::heights(int a, int b) {
	int* heightMap = new int[size * size];
	for (int x = 0; x < size; ++x) {
		for (int z = 0; z < size; ++z) {
			heightMap[x + z * size] = std::round(calculateHeights(x, z));
		}
	}
	return heightMap[a + b * size];
}	

double ChunkGenerator::calculateHeights(int a, int b) {
	//calculate the appropriate coordinates for the perlin noise
	float xCoord = (((float)a / size) + (startX + (xPos / size))) / ((float)(height - airLayer) / heightScale);
	float zCoord = (((float)b / size) + (startZ + (zPos / size))) / ((float)(height - airLayer) / heightScale);
	return pn.octaveNoise(xCoord, zCoord, amtOfOctaves) * (height - airLayer);
}

ChunkGenerator::~ChunkGenerator() {
	delete topLayer;	
	delete middleLayer;
	delete bottomLayer;
}
