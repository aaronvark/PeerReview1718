#include "ChunkManager.h"
#include <iostream>

ChunkManager::ChunkManager(Renderer& renderer) : renderer(renderer), ChunkSize(0){}

void ChunkManager::AddChunk(const Chunk& chunk)
{
	glm::vec2 pos(chunk.GetXPos(), chunk.GetYPos());
	chunks.insert(std::make_pair(pos,chunk));
	meshes.insert(std::make_pair(pos, generator.generateMesh(chunk)));

	if (ChunkSize == 0)
		ChunkSize = chunk.GetSize();
}

void ChunkManager::RemoveChunk(int x, int z)
{
    glm::vec2 pos(x, z);
	chunks.erase(pos);
	delete meshes[pos];
	meshes.erase(pos);
}

Chunk* ChunkManager::GetChunk(float x, float z)
{
	x = std::floor(x / (float)ChunkSize) * ChunkSize;
	z = std::floor(z / (float)ChunkSize) * ChunkSize;

	if (!ChunkExist(x,z)) {
		std::cout << "Chunk with position(" << " x: " << x <<" z: " << z << ") doesn't exist" << std::endl;
		return nullptr;
	}


	return &chunks[glm::vec2(x, z)];
}

Block ChunkManager::GetBlock(float x, float y, float z)
{
	Chunk* c = GetChunk(x, z);

	if (c == nullptr) {
		std::cout << "Block with position(" << " x: " << x << " y: " << y << " z: " << z << ") doesn't exist" << std::endl;
		return Block(0, 0, 0, blockType::Air);
	}	
	
	x -= c->GetXPos();
	z -= c->GetYPos();

	
	return c->GetBlock(x,y,z);
}

blockType ChunkManager::GetBlockType(float x, float y, float z)
{
	Chunk* c = GetChunk(x, z);

	if (c == nullptr) {
		std::cout << "Block with position(" << " x: " << x << " y: " << y << " z: " << z << ") doesn't exist" << std::endl;
		return blockType::Air;
	}

	x -= c->GetXPos();
	z -= c->GetYPos();


	return c->GetBlockType(x, y, z);
}

blockType ChunkManager::GetNeighbourType(float x, float y, float z, side s)
{
	Chunk* c = GetChunk(x, z);

	if (c == nullptr) {
		std::cout << "Block with position(" << " x: " << x << " y: " << y << " z: " << z << ") doesn't exist" << std::endl;
		return blockType::Air;
	}

	x -= c->GetXPos();
	z -= c->GetYPos();


	if (s == side::Back) {
		return GetBlockType(x, y, z - 1);
	}
	if (s == side::Bottom) {
		return GetBlockType(x, y - 1, z);
	}
	if (s == side::Front) {
		return GetBlockType(x, y, z + 1);
	}
	if (s == side::Left) {
		return GetBlockType(x - 1, y, z);
	}
	if (s == side::Right) {
		return GetBlockType(x + 1, y, z);
	}
	if (s == side::Top) {
		return GetBlockType(x, y + 1, z);
	}

	return blockType::Air;
}

void ChunkManager::RemoveBlock(float x, float y, float z)
{
	Chunk* chunk = GetChunk(x, z);
	
	float xChunkSpace = x - chunk->GetXPos();
	float zChunkSpace = z - chunk->GetYPos();


	chunk->RemoveBlock(xChunkSpace, y, zChunkSpace);
	ChunkMesh* mesh = meshes[glm::vec2(chunk->GetXPos(), chunk->GetYPos())];
	delete mesh;
	meshes[glm::vec2(chunk->GetXPos(), chunk->GetYPos())] = generator.generateMesh(*chunk);

}

void ChunkManager::AddBlock(float x, float y, float z, blockType type)
{
	Chunk* chunk = GetChunk(x, z);

	float xChunkSpace = x - chunk->GetXPos();
	float zChunkSpace = z - chunk->GetYPos();


	chunk->AddBlock(xChunkSpace, y, zChunkSpace, type);
	ChunkMesh* mesh = meshes[glm::vec2(chunk->GetXPos(), chunk->GetYPos())];
	delete mesh;
	meshes[glm::vec2(chunk->GetXPos(), chunk->GetYPos())] = generator.generateMesh(*chunk);
}


bool ChunkManager::ChunkExist(float x, float z)
{
	if (chunks.find(glm::vec2(x, z)) == chunks.end())
		return false;
	else
		return true;
}

void ChunkManager::DisplayAllChunks()
{
    std::unordered_map<glm::vec2,ChunkMesh*>::iterator it;
    
    for(it = meshes.begin(); it != meshes.end(); it++)
    {
    	renderer.Draw(it->second);
    }
}
