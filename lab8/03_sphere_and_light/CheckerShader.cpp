#include "stdafx.h"
#include "CheckerShader.h"
#include "Vector4.h"
#include "Scene.h"
#include "ILightSource.h"
#include "VectorMath.h"
#include "IGeometryObject.h"
#include "Ray.h"
#include "Intersection.h"
#include "SceneObject.h"
#include "IShader.h"
#include "ShadeContext.h"

CCheckerShader::CCheckerShader(CMatrix4d const& textureTransform)
:m_textureTransform(textureTransform)
{
}

void CCheckerShader::SetTextureTransform(CMatrix4d const& textureTransform)
{
	m_textureTransform = textureTransform;
}

CVector4f CCheckerShader::Shade(CShadeContext const & shadeContext)const
{
	/*
	Шейдер шахматной доски подвергает точку, заданную в системе координат объекта,
	преобразованию, заданному матрицей трансформации.
	У полученной точки определяется принадлежность к черной или белой клетке трехмерного шахматного пространства
	*/

	// Представление точки в виде 4-мерного вектора
	CVector4d pt(shadeContext.GetSurfacePointInObjectSpace(), 1.0);
	// Трансформируем координаты матрицей трансформации текстурных координат
	CVector3d transformedPoint = (m_textureTransform * pt).Project();
	/*
	Вычисляем дробную часть координат точки в систем координат объекта
	*/
	CVector3d fract = Fract(transformedPoint);
	/*
	Координаты, превышающие 0.5, будут иметь значение 1, а не превышающие - 0
	*/
	CVector3d s = Step(0.5, fract);


	CVector4f cellColor;
	/*
	Применяем операцию XOR для определения принадлежности точки либо к черному, либо к белому кубу
	*/
	if (int(s.x) ^ int(s.y) ^ int(s.z))
	{
		cellColor = CVector4f(0.9, 0.9, 0.9, 1);
	}
	else
	{
		cellColor = CVector4f(0.1, 0.1, 0.1, 1);
	}

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
		CVector3d const& n = shadeContext.GetSurfaceNormal();

		// Вычисляем скалярное произведение нормали и орт-вектора направления на источник света
		double nDotL = Max(Dot(n, Normalize(lightDirection)), 0.0);


		if (!HasShadow(shadeContext, light))
		{
			shadedColor += cellColor;
		}
		shadedColor += cellColor * 0.2;
	} // Проделываем данные действия для других источников света

	// Возвращаем результирующий цвет точки
	return shadedColor / numLights;


}

bool CCheckerShader::HasShadow(CShadeContext const& shadeContext, ILightSource const& lightSource) const
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
			if (intersection.GetHit(0).GetHitTime() < shadeContext.GetHitTime())
				return true;
			else
				return false;
		}
	}

	return false;
}