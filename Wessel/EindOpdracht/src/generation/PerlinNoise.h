#include <cmath>
#include <algorithm>

class PerlinNoise {
private: //section for private variables
	int* perm;
private: //section for private functions
	float fade(float t);
	float lerp(float t, float a, float b);
	float grad(int hash, float x, float y);
	double noise(float x, float y);
public: //section for public functions
	PerlinNoise();
	double octaveNoise(float x, float y, int amtOfOctaves);
	~PerlinNoise();
};