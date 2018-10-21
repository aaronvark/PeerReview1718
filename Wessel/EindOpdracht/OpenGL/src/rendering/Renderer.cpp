#include "Renderer.h"
#include <iostream>
#include <glm/gtc/matrix_transform.hpp>


bool terrainBound = true;



void GLClearError() {
	while (glGetError() != GL_NO_ERROR);
}

bool GLLogCall(const char* function, const char* file, int line) {
	while (GLenum error = glGetError()) {
		std::cout << "[OpenGL] (" << error << ")" << function << " " << file << ":" << line << std::endl;
		return false;
	}
	return true;
}


Renderer::Renderer(glm::mat4& proj, glm::mat4* view, glm::mat4& uiProj, int& screenHeight, int& screenWith) : sh("res/shaders/Sprite.shader"), terrain("res/textures/CANDEMAN.png"), proj(proj), uiProj(uiProj), screenHeight(screenHeight),screenWith(screenWith)
{
	sh.Bind();
	terrain.Bind();
	this->proj = proj;
	this->view = view;

	
}

Renderer::~Renderer()
{
	sh.Bind();
}


void Renderer::Draw(const VertexArray & va, const IndexBuffer & ib, Shader & shader, glm::mat4 modelTransform)
{
	glm::mat4 mvp = proj * (*view) * modelTransform;
	shader.Bind();
	shader.SetUniformMat4f("u_MVP", mvp);
	va.Bind();
	ib.Bind();
	GLCall(glDrawElements(GL_TRIANGLES, ib.GetCount(), GL_UNSIGNED_INT, nullptr));
}

void Renderer::Draw(const VertexArray & va, Shader& shader, glm::mat4 modelTransform)
{
	glm::mat4 mvp = proj * (*view) * modelTransform;
	
	shader.Bind();
	shader.SetUniformMat4f("u_MVP", mvp);

	va.Bind();
	glDrawArrays(GL_TRIANGLES, 0, 36);
}

void Renderer::Draw(const VertexArray & va, Shader& shader, glm::mat4 modelTransform, unsigned int amountOfVerts)
{
	glm::mat4 mvp = proj * (*view) * modelTransform;
	
	if (!terrainBound)
		shader.Bind();


	shader.SetUniformMat4f("u_MVP", mvp);

	va.Bind();
	glDrawArrays(GL_TRIANGLES, 0, amountOfVerts);
	terrainBound = true;
}

void Renderer::Draw(ChunkMesh* mesh)
{
	if(!terrainBound)
		terrain.Bind();
	
	Draw(*(mesh->va), sh, glm::mat4(1), mesh->buffer->size() / 8);
}

void Renderer::DrawUi(VertexArray& va, Shader& ui, glm::vec2 Pos, glm::vec2 Size, glm::vec3 rot, Texture& tex)
{
	tex.Bind();
	glm::mat4 transform(1.0f);
	
	
	transform = glm::translate(transform, glm::vec3((Pos.x - Size.x / 2), (Pos.y - Size.y / 2), -300.0f));
	transform = glm::scale(transform, glm::vec3(Size.x, Size.y, 1.0f));
	
	
	

	glm::mat4 mvp = glm::scale(glm::mat4(1.0f), glm::vec3(((float)screenHeight * 896)/((float)screenWith * 504), 1.0f, 1.0f)) * uiProj * transform;
	ui.Bind();
	ui.SetUniformMat4f("u_MP", mvp);
	
	
	va.Bind();
	glDrawArrays(GL_TRIANGLES, 0, 6);
	
	terrainBound = false;
}
void Renderer::DrawIcon(VertexArray& va, Shader& ui, glm::vec2 Pos, glm::vec2 Size)
{
	terrain.Bind();
	glm::mat4 transform(1.0f);

	transform = glm::translate(transform, glm::vec3((Pos.x - Size.x / 2), (Pos.y - Size.y / 2), -250.0f));
	transform = glm::rotate(transform, (float)45 * 0.0174532925f, glm::vec3(1.0f, 1.5f, 0.4f));
	transform = glm::scale(transform, glm::vec3(Size.x, Size.y, Size.x));

	glm::mat4 mvp = glm::scale(glm::mat4(1.0f), glm::vec3(((float)screenHeight * 896) / ((float)screenWith * 504), 1.0f, 1.0f)) * uiProj * transform;
	ui.Bind();
	ui.SetUniformMat4f("u_MP", mvp);

	va.Bind();
	glDrawArrays(GL_TRIANGLES, 0, 36);

	terrainBound = true;
}