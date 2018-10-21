#pragma once
#include "VertexArray.h"
#include "VertexBuffer.h"
#include "IndexBuffer.h"
#include <vector>

class ChunkMesh {

private:


public:
	ChunkMesh(){}
	~ChunkMesh() { delete va; delete layout; delete buffer; }

	std::vector<float>* buffer;
	VertexArray* va;
	VertexBufferLayout* layout;

};