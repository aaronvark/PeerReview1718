#include <GL\glew.h>
#include <GLFW\glfw3.h>
#include <iostream>
#include <fstream>
#include <string>
#include <sstream>
#include <future>

#include "Renderer.h"
#include "VertexBuffer.h"
#include "VertexBufferLayout.h"
#include "IndexBuffer.h"
#include "VertexArray.h"
#include "Shader.h"
#include "Texture.h"

#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"

#include "Camera.h"
#include "ChunkGenerator.h"
#include "ChunkMeshGenerator.h"
#include "ChunkMesh.h"
#include "WorldGeneration.h"
#include <ctime>

#include "Chunk.h"
#include "ChunkManager.h"
#include "UiRenderer.h"

const bool FULLSCREEN = true;

float deltaTime = 0.0f;	// Time between current frame and last frame
float lastFrame = 0.0f; // Time of last frame

bool endApp = false;
bool firstFrame = true;


int SCREENWIDTH = FULLSCREEN ? 1920 : 896;
int SCREENHEIGHT = FULLSCREEN ? 1080 : 504;

glm::mat4 proj = glm::perspective(glm::radians(45.0f), (float)SCREENWIDTH / (float)SCREENHEIGHT, 0.1f, 300.0f);
glm::mat4 uiProj = glm::ortho(0.0f, 896.0f, 0.0f, 504.0f,0.1f, 500.0f);

void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
	glViewport(0, 0, width, height);
	SCREENWIDTH = width;
	SCREENHEIGHT = height;
	proj = glm::perspective(glm::radians(45.0f), (float)SCREENWIDTH / SCREENHEIGHT, 0.1f, 300.0f);
}

void processInput(GLFWwindow *window)
{
	if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS) {
			endApp = true;
	}
}



//void LoadChunks(WorldGeneration* w) {
//	w->updateChunks();
//}


int main(void)
{
		glm::mat4 view(1.0f);
		std::srand(time(0));

		GLFWwindow* window;

		/* Initialize the library */
		if (!glfwInit())
			return -1;

		/* Create a windowed mode window and its OpenGL context */


		GLFWmonitor* monitor = glfwGetPrimaryMonitor();

		const GLFWvidmode* mode = glfwGetVideoMode(monitor);


		glfwWindowHint(GLFW_RED_BITS, mode->redBits);
		glfwWindowHint(GLFW_GREEN_BITS, mode->greenBits);
		glfwWindowHint(GLFW_BLUE_BITS, mode->blueBits);
		glfwWindowHint(GLFW_REFRESH_RATE, mode->refreshRate);

		window = glfwCreateWindow(SCREENWIDTH, SCREENHEIGHT, "Hello World", NULL, NULL);
		
		if(FULLSCREEN)
			glfwSetWindowMonitor(window, monitor, 0, 0, mode->width, mode->height, mode->refreshRate);

		if (!window)
		{
			glfwTerminate();
			return -1;
		}
		
		glfwSetWindowSizeCallback(window, framebuffer_size_callback);

		

		/* Make the window's context current */
		glfwMakeContextCurrent(window);
		glfwSwapInterval(0);

		if (glewInit() != GLEW_OK) {
			std::cout << "Error!" << std::endl;
		}

		glClearColor(0.5f, 0.75f, 0.94f, 1.0f);
		glViewport(0, 0, SCREENWIDTH, SCREENHEIGHT);

		std::cout << glGetString(GL_VERSION) << std::endl;

		
			
		GLCall(glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA));
		GLCall(glEnable(GL_BLEND));
		glEnable(GL_DEPTH_TEST);
		
		
	







		//RENDERINGSCOPE------------------------------------------------------------------------------------------------------
		{

			//--------------------------------------------------------------------------------------------------------------------

			Renderer renderer(proj, &view, uiProj, SCREENHEIGHT, SCREENWIDTH);
			UiRenderer UiRenderer(renderer);
			Camera cam(window);
			

			UiElement crosair(glm::vec2(896/2, 504/2), glm::vec2(16,16), "res/textures/Crossair.png");

			int s = (896 - (9 * 40))/2 + 20;
			std::vector<UiElement*> el;
			std::vector<UiElement*> els;

			for (size_t i = 0; i < 9; i++) {
				el.push_back( new UiElement(glm::vec2(s + 37 * i, 20), glm::vec2(40, 40), "res/textures/Slot.png"));
				els.push_back( new  UiElement(glm::vec2(s + 37 * i, 20), glm::vec2(40, 40), "res/textures/SlotSelected.png"));
			}
			
			ChunkMeshGenerator mg;
			ChunkManager manager(renderer);
			WorldGeneration w(&manager, &cam);

			/*size | height | amount of chunks | amount of perlin octaves | height scale | biome interval*/
			w.generateWorld(6, 33, pow(40, 2), 4, 3.5f, 20);
			cam.SetManager(&manager);


			/* Loop until the user closes the window */
			while (!glfwWindowShouldClose(window) && !endApp)
			{
				processInput(window);
				view = cam.getView(deltaTime);

				float currentFrame = glfwGetTime();
				
				if (!firstFrame) {
					deltaTime = currentFrame - lastFrame;
				}
				else {
					firstFrame = false;
				}

				lastFrame = currentFrame;

				//chance at biome switch!
				w.updateChunks();

				if(endApp)
					glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_NORMAL);
				else
					glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);

				/* Render here */
				glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

				manager.DisplayAllChunks();


				
				
				UiRenderer.RenderElement(crosair);

				for (size_t i = 0; i < 9; i++)
				{
					if (cam.selectedBlock == i) {
						UiRenderer.RenderElement(*els[i]);
					}
					else {
						UiRenderer.RenderElement(*el[i]);
					}
				}
				UiRenderer.RenderCube();
				

				/* Swap front and back buffers */
				glfwSwapBuffers(window);

				/* Poll for and process events */
				glfwPollEvents();
			}

		}
	
	glfwTerminate();
	return 0;
}
