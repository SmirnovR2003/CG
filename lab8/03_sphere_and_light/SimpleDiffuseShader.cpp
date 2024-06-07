#include "stdafx.h"
#include "SimpleDiffuseShader.h"
#include "Vector4.h"
#include "Scene.h"
#include "ShadeContext.h"
#include "ILightSource.h"
#include "VectorMath.h"
#include "IGeometryObject.h"
#include "Ray.h"
#include "Intersection.h"
#include "SceneObject.h"
#include "IShader.h"
#include "ShadeContext.h"


CSimpleShader::CSimpleShader(void)
{
}

/*
Запоминаем параметры материала, связанного с шейдером
*/
void CSimpleShader::SetMaterial(CSimpleMaterial const& material)
{
	m_material = material;
}

CVector4f CSimpleShader::Shade(CShadeContext const & shadeContext)const
{
	/*
	Получаем сцену из контекста закрашивания для того, чтобы вычислить вклад
	каждого из источников света в освещенность обрабатываемой точки
	*/
	CScene const& scene = shadeContext.GetScene();

	// Результирующий цвет
	CVector4f shadedColor;

	const size_t numLights = scene.GetLightsCount();

	// Пробегаемся по всем источникам света в сцене
	for (size_t i = 0; i < numLights; ++i)
	{
		// Получаем источник света
		ILightSource const& light = scene.GetLight(i);

		// Вычисляем вектор направления на источник света из текущей точке
		CVector3d lightDirection = light.GetDirectionFromPoint(shadeContext.GetSurfacePoint());

		// Вычисляем интенсивность света в направлении от источника к текущей точке
		double lightIntensity = light.GetIntensityInDirection(-lightDirection);

		// Получаем нормаль к поверхности в обрабатываемой точке
		CVector3d n = shadeContext.GetSurfaceNormal();

		if (Dot(n, Normalize(lightDirection)) < 0)
		{
			n *= -1;
		}

		// Вычисляем скалярное произведение нормали и орт-вектора направления на источник света
		double nDotL = Max(Dot(n, Normalize(lightDirection)), 0.0);

		CVector3d viewDir = Normalize(-shadeContext.GetSurfacePoint());
		CVector3d reflectDir = light.Reflect(-Normalize(lightDirection), n);
		double spec = pow(Max(0.0, Dot(viewDir, reflectDir)), m_material.GetShininess()); 

		CVector4f diffuseColor = light.GetDiffuseIntensity() * m_material.GetDiffuseColor() * nDotL;
		
		CVector4f ambientColor = light.GetAmbientIntensity() * m_material.GetAmbientColor();
		CVector4f specularColor = light.GetSpecularIntensity() * m_material.GetSpecularColor() * spec;


		//добавить в шахматную доску
		if (!HasShadow(shadeContext, light))
		{
			shadedColor += diffuseColor;
			shadedColor += specularColor;

		}
		shadedColor += ambientColor;
	}	// Проделываем данные действия для других источников света

	// Возвращаем результирующий цвет точки
	return shadedColor;
}

bool CSimpleShader::HasShadow(CShadeContext const& shadeContext, ILightSource const& lightSource) const
{
	
	CVector3d ray = lightSource.GetDirectionFromPoint(shadeContext.GetSurfacePoint());
	CVector3d point = shadeContext.GetSurfacePoint();

	const SceneObjects& objects = shadeContext.GetScene().GetSceneObjects();

	CIntersection intersection;
	for (int i = 0; i < objects.size(); i++)
	{
		CSceneObject const& sceneObject = *objects[i];
		IGeometryObject const& geometryObject = sceneObject.GetGeometryObject();

		if (geometryObject.Hit(CRay(point, ray), intersection))
		{
			if (intersection.GetHit(0).GetHitTime() < shadeContext.GetHitTime() )
				return true;
			else
				return false;
		}

	}

	return false;
}

