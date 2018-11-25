#pragma once
#include "Chunk.h"

enum entityType {
	Oak_Tree, Birch_Tree, Cactus_Plant
};

class Entity {
private: //section for constants
	const int cactusLength = 3;
	const int treeLength = rand() % 3 + 4;
private: //section for private variables
	int xPos, yPos, zPos;
	Chunk* chunk;
private: //section for private functions
	void spawnOak();
	void spawnBirch();
	void spawnCactus();
	bool ifCorner(int a, int b, int t);
public: //section for public functions
	Entity(Chunk* chunk);
	void generateEntity(int x, int y, int z, entityType type);
	~Entity();
};

