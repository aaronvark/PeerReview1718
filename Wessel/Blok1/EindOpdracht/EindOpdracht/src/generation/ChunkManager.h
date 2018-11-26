#pragma once

#define GLM_ENABLE_EXPERIMENTAL

#include "Renderer.h"
#include "Chunk.h"
#include "ChunkMeshGenerator.h"
#include <map>
#include <glm/glm.hpp>
#include <glm/gtx/hash.hpp>



class ChunkManager{

private:
	int ChunkSize;
	std::unordered_map <glm::vec2, Chunk, std::hash<glm::vec2>> chunks;
	std::unordered_map <glm::vec2, ChunkMesh*, std::hash<glm::vec2>> meshes;

	ChunkMeshGenerator generator;

	Renderer& renderer;

public:

	//===== ALL COORDINATES ARE WORLD SPACE ======
	ChunkManager(Renderer& renderer);

	void AddChunk(const Chunk& chunk);
	void RemoveChunk(int x, int z);

	Chunk* GetChunk(float x, float z);
	Block GetBlock(float x, float y, float z);
	blockType GetBlockType(float x, float y, float z);
	blockType GetNeighbourType(float x, float y, float z, side s);

	void RemoveBlock(float x, float y, float z);
	void AddBlock(float x, float y, float z,blockType type);
	bool ChunkExist(float x, float z);


	void DisplayAllChunks();

};