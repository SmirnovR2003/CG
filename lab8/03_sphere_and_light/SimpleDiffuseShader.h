#pragma once
#include "IShader.h"
#include "SimpleMaterial.h"
#include "ILightSource.h"
#include "SceneObject_fwd.h"

/*
Простой шейдер, выполняющий расчет диффузной составляющей отраженного света
*/
class CSimpleShader :
	public IShader
{
public:
	CSimpleShader();

	/*
	В качестве параметра шейдера выступает класс CSimpleMaterial, хранящий в простейшем случае
	диффузный цвет материала поверхности объекта
	*/
	void SetMaterial(CSimpleMaterial const& material);

	/*
	Вычисление цвета с объекта
	*/
	virtual CVector4f Shade(CShadeContext const & shadeContext)const;

	bool HasShadow(CShadeContext const& shadeContext, ILightSource const& lightSource) const;

private:
	CSimpleMaterial m_material;
};
