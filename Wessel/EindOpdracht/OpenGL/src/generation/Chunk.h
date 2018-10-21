#pragma once
#include "Block.h"
#include <vector>
#include <array>

class Chunk {

private:
	int size, height;
	int xPos, yPos, zPos;
	std::vector<std::vector<std::vector<char>>> blocks;
public:
	Chunk();
	Chunk(int size,int height, int xPos, int yPos, int zPos);
	void AddBlock(int x, int y, int z, blockType t);
	void RemoveBlock(int x, int y, int z);
	int GetSize() const { return size; }
	int GetHeight() const { return height; }
	int GetXPos() const { return xPos; }
	int GetYPos() const { return zPos; }
	Block GetBlock(int x, int y, int z) const;
	Block GetNeighbour(int x, int y, int z, side s) const;

	blockType GetBlockType(int x, int y, int z) const;
	blockType GetNeighbourType(int x, int y, int z, side s) const;
};
