#pragma once
#include "ILightSource.h"
#include "Matrix4.h"
#include "Vector4.h"
#include "VectorMath.h"

/*
Реализация методов, общих для различных типов источников света
*/
class CLightSourceImpl :
	public ILightSource
{
public:
	/*
	Получение матрицы трансформации источника света
	*/
	CMatrix4d const& GetTransform()const
	{
		return m_transform;
	}

	/*
	Установка матрицы трансформации источника света
	*/
	void SetTransform(CMatrix4d const& transform)
	{
		m_transform = transform;
	}

	virtual void SetDiffuseIntensity(CVector4f const& diffuseIntensity)
	{
		m_diffuseIntensity = diffuseIntensity;
	}

	virtual CVector4f const& GetDiffuseIntensity()const
	{
		return m_diffuseIntensity;
	}
	virtual void SetSpecularIntensity(CVector4f const& specularIntensity)
	{
		m_specularIntensity = specularIntensity;
	}

	virtual CVector4f const& GetSpecularIntensity()const
	{
		return m_specularIntensity;
	}

	virtual void SetAmbientIntensity(CVector4f const& ambientIntensity)
	{
		m_ambientIntensity = ambientIntensity;
	}

	virtual CVector4f const& GetAmbientIntensity()const
	{
		return m_ambientIntensity;
	}

	virtual CVector3d Reflect(const CVector3d& incident, const CVector3d& normal) const
	{
		float dotProduct = Dot(incident, normal);
		CVector3d reflectedVector;
		
		reflectedVector.x = incident.x - 2.f * dotProduct * normal.x;
		reflectedVector.y = incident.y - 2.f * dotProduct * normal.y;
		reflectedVector.z = incident.z - 2.f * dotProduct * normal.z;
		return reflectedVector;
	};

protected:
	CLightSourceImpl(CMatrix4d const& transform = CMatrix4d())
		:m_transform(transform)
	{
	}

private:
	CVector4f m_diffuseIntensity;
	CVector4f m_specularIntensity;
	CVector4f m_ambientIntensity;
	CMatrix4d m_transform;

};
