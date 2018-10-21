#include "Shader.h"
#include <iostream>
#include <fstream>
#include <string>
#include <sstream>
#include "Renderer.h"

struct ShaderSources {
	std::string VertexSource;
	std::string FramentSource;
};

ShaderSources ParseShader(const std::string& filepath) {
	std::ifstream stream(filepath);


	enum class ShaderType
	{
		NONE = -1, VERTEX = 0, FRAGMENT = 1

	};
	ShaderType type = ShaderType::NONE;


	std::string line;
	std::stringstream ss[2];
	while (std::getline(stream, line))
	{
		if (line.find("#shader") != std::string::npos) {
			if (line.find("vertex") != std::string::npos) {
				type = ShaderType::VERTEX;
			}
			else if (line.find("fragment") != std::string::npos) {
				type = ShaderType::FRAGMENT;
			}
		}
		else {
			ss[(int)type] << line << '\n';
		}
	}

	return { ss[0].str(), ss[1].str() };
}


Shader::Shader(const std::string & filepath) : m_RendererID(0)
{
	ShaderSources source = ParseShader(filepath);
	m_RendererID = CreateShader(source.VertexSource, source.FramentSource);
}

Shader::~Shader()
{
	GLCall(glDeleteProgram(m_RendererID));
}

bool Shader::CompileShader()
{
	return false;
}



unsigned int Shader::CompileShader(unsigned int type, const std::string& source) {

	unsigned int id = glCreateShader(type);
	const char* src = source.c_str();
	glShaderSource(id, 1, &src, nullptr);
	glCompileShader(id);

	int resault;
	glGetShaderiv(id, GL_COMPILE_STATUS, &resault);


	if (resault == GL_FALSE) {

		int length;
		glGetShaderiv(id, GL_INFO_LOG_LENGTH, &length);
		char* message = (char*)alloca(length * sizeof(char));
		glGetShaderInfoLog(id, length, &length, message);

		std::cout << message << std::endl;
		glDeleteShader(id);
		return 0;
	}



	return id;

}

unsigned int Shader::CreateShader(const std::string& vertexShader, const std::string& fragmentShader)
{
	unsigned int program = glCreateProgram();
	unsigned int vs = CompileShader(GL_VERTEX_SHADER, vertexShader);
	unsigned int fs = CompileShader(GL_FRAGMENT_SHADER, fragmentShader);

	glAttachShader(program, vs);
	glAttachShader(program, fs);
	glLinkProgram(program);
	glValidateProgram(program);

	glDeleteShader(vs);
	glDeleteShader(fs);


	return program;

}

void Shader::Bind() const
{
	GLCall(glUseProgram(m_RendererID));
}

void Shader::UnBind() const
{
	GLCall(glUseProgram(0));
}

void Shader::SetUniform4f(const std::string & name, float v0, float v1, float v2, float v3)
{
	int location = GetUniformLocation(name);
	GLCall(glUniform4f(location,v0,v1,v2,v3))
}

void Shader::SetUniform1i(const std::string & name, int value)
{
	int location = GetUniformLocation(name);
	GLCall(glUniform1i(location, value))
}

void Shader::SetUniformMat4f(const std::string & name, const glm::mat4 & matrix)
{
	GLCall(glUniformMatrix4fv(GetUniformLocation(name), 1, GL_FALSE, &matrix[0][0]));
}



int Shader::GetUniformLocation(const std::string & name)
{
	if (m_uniformLocationCashe.find(name) != m_uniformLocationCashe.end())
		return m_uniformLocationCashe[name];


	GLCall(int location = glGetUniformLocation(m_RendererID, name.c_str()));

	if (location == -1) {

	}
	else {
		m_uniformLocationCashe[name] = location;
	}

	return location;
}
