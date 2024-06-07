#pragma once

#include "Vector4.h"

class CSimpleMaterial
{
public:
	CSimpleMaterial(void)
	{}

	CVector4f const& GetDiffuseColor()const
	{
		return m_diffuseColor;
	}

	void SetDiffuseColor(CVector4f const& diffuseColor)
	{
		m_diffuseColor = diffuseColor;
	}

	CVector4f const& GetSpecularColor()const
	{
		return m_specularColor;
	}

	void SetSpecularColor(CVector4f const& specularColor)
	{
		m_specularColor = specularColor;
	}

	CVector4f const& GetAmbientColor()const
	{
		return m_ambientColor;
	}

	void SetAmbientColor(CVector4f const& ambientColor)
	{
		m_ambientColor = ambientColor;
	}
		 

	int GetShininess()const
	{
		return m_shininess;
	}

	void SetShininess(int shininess)
	{
		m_shininess = shininess;
	}

private:
	CVector4f m_diffuseColor;
	CVector4f m_specularColor;
	CVector4f m_ambientColor;
	int m_shininess;
};
