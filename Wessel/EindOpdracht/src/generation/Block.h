#pragma once
#include <map>

enum side { Front, Back, Right, Left, Top, Bottom };
enum blockType { 
	Dirt, Stone, Sand, GrayBrick, RedBrick,
	OakLog, BirchLog, OakPlank, BirchPlank, 
	BirchLeaf, OakLeaf, Cactus, Grass,
	Water, Bedrock,
	Air
};

class BlockPlane {
public:
	BlockPlane() {}
	BlockPlane(side s, int xTex, int yTex) : s(s), xTex(xTex), yTex(yTex){}
	side s;
	int xTex;
	int yTex;
};

class Block {
private:
	int x, y, z;
	blockType type;
public:
	Block(int x, int y, int z, blockType type);
	std::map<side, BlockPlane> Planes;
	int getXPos() { return x; }
	int getYPos() { return y; }
	int getZPos() { return z; }
	blockType getType() { return type; }
};