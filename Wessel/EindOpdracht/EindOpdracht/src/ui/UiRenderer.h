#pragma once
#include "Renderer.h"
#include "UiElement.h"
#include "Block.h"
#include "ChunkMesh.h"

class UiRenderer {

private:

	Texture terrain;

	Renderer renderer;
	VertexArray quadMesh;
	VertexBuffer vb;
	VertexBufferLayout lay;
	Shader Ui;


	std::vector<ChunkMesh*> iconMeshes;
	VertexBufferLayout cubeLayout;
	Shader cubeShader;

public:
	UiRenderer(Renderer& renderer);
	void RenderElement(UiElement& element);
	void RenderCube();
	
};