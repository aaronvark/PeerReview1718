#pragma once
#include <glm\glm.hpp>
#include "Texture.h"


class UiElement {

public:
	UiElement(glm::vec2 position, glm::vec2 size, std::string texturePath);
	glm::vec2 position;
	glm::vec2 size;
	Texture texture;


};