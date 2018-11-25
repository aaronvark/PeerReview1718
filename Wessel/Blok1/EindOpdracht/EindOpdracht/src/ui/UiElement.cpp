#include "UiElement.h"

UiElement::UiElement(glm::vec2 position, glm::vec2 size, std::string texturePath) :
	position(position), size(size), texture(texturePath){}
