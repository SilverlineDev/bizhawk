#include "newstate.h"
#include <cstring>
#include <algorithm>

namespace gambatte {

NewStateDummy::NewStateDummy()
	:length(0)
{
}
void NewStateDummy::Save(const void *ptr, size_t size, const char *name)
{
	length += size;
}
void NewStateDummy::Load(void *ptr, size_t size, const char *name)
{
}

NewStateExternalBuffer::NewStateExternalBuffer(char *buffer, long maxlength)
	:buffer(buffer), maxlength(maxlength), length(0)
{
}

void NewStateExternalBuffer::Save(const void *ptr, size_t size, const char *name)
{
	const char *src = static_cast<const char *>(ptr);
	if (maxlength - length >= size)
	{
		std::memcpy(buffer + length, ptr, size);
	}
	length += size;
}

void NewStateExternalBuffer::Load(void *ptr, size_t size, const char *name)
{
	char *dst = static_cast<char *>(ptr);
	if (maxlength - length >= size)
	{
		std::memcpy(dst, buffer + length, size);
	}
	length += size;
}

NewStateExternalFunctions::NewStateExternalFunctions(
	void (*Save_)(const void *ptr, size_t size, const char *name),
	void (*Load_)(void *ptr, size_t size, const char *name),
	void (*EnterSection_)(const char *name),
	void (*ExitSection_)(const char *name))
	:Save_(Save_),
	Load_(Load_),
	EnterSection_(EnterSection_),
	ExitSection_(ExitSection_)
{
}

void NewStateExternalFunctions::Save(const void *ptr, size_t size, const char *name)
{
	Save_(ptr, size, name);
}
void NewStateExternalFunctions::Load(void *ptr, size_t size, const char *name)
{
	Load_(ptr, size, name);
}
void NewStateExternalFunctions::EnterSection(const char *name)
{
	EnterSection_(name);
}
void NewStateExternalFunctions::ExitSection(const char *name)
{
	ExitSection_(name);
}


}
