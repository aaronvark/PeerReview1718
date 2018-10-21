#include "ChunkMeshGenerator.h"
#include "VertexBufferLayout.h"
#include <vector>
#include <iostream>




const float t = 1.0f / 16.0f;




void Addplane(std::vector<float>* vertexBuffer, side s, int x, int y, int z, int texX, int texY) {

	float ux = x;
	float uy = y;
	float uz = z;

	float yMin = (16 - (texY + 1)) * t;
	float yMax = (16 - (texY)) * t;
	float xMin = texX * t;
	float xMax = (texX + 1) * t;
											 																					
	if (s == side::Left) {
		float left[] = {							 
		//left							    	 
		ux - 0.5f,  uy + 0.5f,  uz + 0.5f, -1.0f, 0.0f, 0.0f,  xMin, yMax,
		ux - 0.5f,  uy + 0.5f,  uz - 0.5f, -1.0f, 0.0f, 0.0f,  xMax, yMax,
		ux - 0.5f,  uy - 0.5f,  uz - 0.5f, -1.0f, 0.0f, 0.0f,  xMax, yMin,
		ux - 0.5f,  uy - 0.5f,  uz - 0.5f, -1.0f, 0.0f, 0.0f,  xMax, yMin,
		ux - 0.5f,  uy - 0.5f,  uz + 0.5f, -1.0f, 0.0f, 0.0f,  xMin, yMin,
		ux - 0.5f,  uy + 0.5f,  uz + 0.5f, -1.0f, 0.0f, 0.0f,  xMin, yMax,

	};
		vertexBuffer->insert(vertexBuffer->end(), &left[0], &left[6 * 8]);
	}

	if (s == side::Right) {
		float right[] = {
		//right							    
		ux + 0.5f,  uy + 0.5f,  uz + 0.5f, 1.0f, 0.0f, 0.0f, xMin, yMax,
		ux + 0.5f,  uy + 0.5f,  uz - 0.5f, 1.0f, 0.0f, 0.0f, xMax, yMax,
		ux + 0.5f,  uy - 0.5f,  uz - 0.5f, 1.0f, 0.0f, 0.0f, xMax, yMin,
		ux + 0.5f,  uy - 0.5f,  uz - 0.5f, 1.0f, 0.0f, 0.0f, xMax, yMin,
		ux + 0.5f,  uy - 0.5f,  uz + 0.5f, 1.0f, 0.0f, 0.0f, xMin, yMin,
		ux + 0.5f,  uy + 0.5f,  uz + 0.5f, 1.0f, 0.0f, 0.0f, xMin, yMax,
	};	
		vertexBuffer->insert(vertexBuffer->end(), &right[0], &right[6 * 8]);
	}

	if (s == side::Front) {
		float front[] = {							 
		//front									 
		ux - 0.5f, uy - 0.5f,  uz + 0.5f, 0.0f, 0.0f, 1.0f,  xMin, yMin,
		ux + 0.5f, uy - 0.5f,  uz + 0.5f, 0.0f, 0.0f, 1.0f,  xMax, yMin,
		ux + 0.5f, uy + 0.5f,  uz + 0.5f, 0.0f, 0.0f, 1.0f,  xMax, yMax,
		ux + 0.5f, uy + 0.5f,  uz + 0.5f, 0.0f, 0.0f, 1.0f,  xMax, yMax,
		ux - 0.5f, uy + 0.5f,  uz + 0.5f, 0.0f, 0.0f, 1.0f,  xMin, yMax,
		ux - 0.5f, uy - 0.5f,  uz + 0.5f, 0.0f, 0.0f, 1.0f,  xMin, yMin,
	};	
		vertexBuffer->insert(vertexBuffer->end(), &front[0], &front[6 * 8]);
	}

	if (s == side::Back) {
		float back[] = {							 
		//back									 
		ux - 0.5f, uy - 0.5f, uz - 0.5f, 0.0f, 0.0f, -1.0f,  xMin, yMin,
		ux + 0.5f, uy - 0.5f, uz - 0.5f, 0.0f, 0.0f, -1.0f,  xMax, yMin,
		ux + 0.5f, uy + 0.5f, uz - 0.5f, 0.0f, 0.0f, -1.0f,  xMax, yMax,
		ux + 0.5f, uy + 0.5f, uz - 0.5f, 0.0f, 0.0f, -1.0f,  xMax, yMax,
		ux - 0.5f, uy + 0.5f, uz - 0.5f, 0.0f, 0.0f, -1.0f,  xMin, yMax,
		ux - 0.5f, uy - 0.5f, uz - 0.5f, 0.0f, 0.0f, -1.0f,  xMin, yMin,
	};	
		vertexBuffer->insert(vertexBuffer->end(), &back[0], &back[6 * 8]);
	}

	if (s == side::Top) {
		float top[] = {								 
		//top							    	 
		ux - 0.5f,  uy + 0.5f,  uz - 0.5f, 0.0f, 1.0f, 0.0f, xMin, yMax,
		ux + 0.5f,  uy + 0.5f, uz - 0.5f, 0.0f, 1.0f, 0.0f, xMax, yMax,
		ux + 0.5f,  uy + 0.5f, uz + 0.5f, 0.0f, 1.0f, 0.0f, xMax, yMin,
		ux + 0.5f,  uy + 0.5f, uz + 0.5f, 0.0f, 1.0f, 0.0f, xMax, yMin,
		ux - 0.5f,  uy + 0.5f,  uz + 0.5f, 0.0f, 1.0f, 0.0f, xMin, yMin,
		ux - 0.5f,  uy + 0.5f,  uz - 0.5f, 0.0f, 1.0f, 0.0f, xMin, yMax
	};		
		vertexBuffer->insert(vertexBuffer->end(), &top[0], &top[6 * 8]);
	}

	if (s == side::Bottom) {
		float bottom[] = {							 
		//bottom						    	 
		ux - 0.5f, uy - 0.5f,  uz - 0.5f, 0.0f, -1.0f, 0.0f,  xMin, yMax,
		ux + 0.5f, uy - 0.5f,  uz - 0.5f, 0.0f, -1.0f, 0.0f,  xMax, yMax,
		ux + 0.5f, uy - 0.5f,  uz + 0.5f, 0.0f, -1.0f, 0.0f,  xMax, yMin,
		ux + 0.5f, uy - 0.5f,  uz + 0.5f, 0.0f, -1.0f, 0.0f,  xMax, yMin,
		ux - 0.5f, uy - 0.5f,  uz + 0.5f, 0.0f, -1.0f, 0.0f,  xMin, yMin,
		ux - 0.5f, uy - 0.5f,  uz - 0.5f, 0.0f, -1.0f, 0.0f,  xMin, yMax,
	};		
		vertexBuffer->insert(vertexBuffer->end(), &bottom[0], &bottom[6 * 8]);
	}

}

void AddBlock(std::vector<float>* vertexBuffer, Block b) {
	for (int i = 0; i < 6; i++) {
		BlockPlane bp = b.Planes[(side)i];
		Addplane(vertexBuffer, bp.s, b.getXPos(), b.getYPos(), b.getZPos(), bp.xTex, bp.yTex);
	}
}

ChunkMeshGenerator::ChunkMeshGenerator()
{
	layout = new VertexBufferLayout;
	
	layout->Push<float>(3);
	layout->Push<float>(3);
	layout->Push<float>(2);
}

ChunkMesh* ChunkMeshGenerator::generateMesh(const Chunk& chunk)
{

	ChunkMesh* mesh = new ChunkMesh;
	
	mesh->va = new VertexArray;
	mesh->buffer = new std::vector<float>;

	int height = chunk.GetHeight();
	int size = chunk.GetSize();
	
	for (int x = 0; x < size; x++) {
		for (int y = 0; y < height; y++) {
			for (int z = 0; z < size; z++) {
				 blockType b = chunk.GetBlockType(x,y,z);
					if (b != blockType::Air) {
						for (int i = 0; i < 6; i++) {
							blockType temp = chunk.GetNeighbourType(x, y, z, (side)i);
							if (temp == blockType::Air) {
								Block bl = chunk.GetBlock(x, y, z);
								Addplane(mesh->buffer, (side)i, x + chunk.GetXPos(), y, z + chunk.GetYPos(), bl.Planes[(side)i].xTex, bl.Planes[(side)i].yTex);
							}
						}
					
					}
			}
		}
	}
	


	VertexBuffer* vb = new VertexBuffer(&((*mesh->buffer)[0]), mesh->buffer->size() * sizeof(float));

	mesh->va->AddBuffer(*vb, *layout);
	
	
	return mesh;
}



