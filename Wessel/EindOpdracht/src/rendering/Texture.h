#pragma once
#include "ErrorHandling.h"
#include <string>

class Texture {
private:
	unsigned int m_RendererID;
	std::string m_FilePath;
	unsigned char* m_localBuffer;
	int m_With, m_Height, m_BPP;
public:
	Texture(const std::string& path);
	~Texture();


	void Bind(unsigned int slot = 0) const;
	void Unbind() const;

	inline int GetWith() const { return m_With; }
	inline int GetHeight() const { return m_Height; }
};