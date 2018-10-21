#pragma once
#include "VertexArray.h"
#include "Block.h"
#include "ChunkMesh.h"
#include "Chunk.h"


class ChunkMeshGenerator {

private:
	VertexBufferLayout* layout;

public:
	ChunkMeshGenerator();
	ChunkMesh* generateMesh(const Chunk& chunk);

	
};
