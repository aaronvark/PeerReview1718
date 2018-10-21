#pragma once

#include <GL/glew.h>
#include "VertexArray.h"
#include "VertexBuffer.h"
#include "VertexBufferLayout.h"
#include "IndexBuffer.h"
#include "Shader.h"

#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"
#include "ChunkMesh.h"
#include "Texture.h"

#define ASSERT(x) if (!(x)) __debugbreak();
#define GLCall(x) GLClearError(); x; ASSERT(GLLogCall(#x, __FILE__, __LINE__))


void GLClearError();
bool GLLogCall(const char* function, const char* file, int line);


class Renderer {
	
private:
	glm::mat4& proj;
	glm::mat4* view;
	glm::mat4& uiProj;
	int& screenHeight;
	int& screenWith;
	Texture terrain;
	Shader sh;




public:
	Renderer(glm::mat4& proj, glm::mat4* view, glm::mat4& uiProj, int& screenHeight, int& screenWith);
	~Renderer();
	void Draw(const VertexArray& va, const IndexBuffer& ib, Shader& shader, glm::mat4 modelTransform);
	void Draw(const VertexArray& va, Shader& shader, glm::mat4 modelTransform);
	void Draw(const VertexArray& va, Shader& shader, glm::mat4 modelTransform, unsigned int amountOfVerts);
	void Draw(ChunkMesh* chunk);
	void DrawUi(VertexArray& va,Shader& ui, glm::vec2 Pos, glm::vec2 Size, glm::vec3 rot, Texture& tex);
	void DrawIcon(VertexArray& va, Shader& ui, glm::vec2 Pos, glm::vec2 Size);
};