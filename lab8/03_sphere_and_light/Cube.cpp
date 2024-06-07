#include "stdafx.h"
#include "Intersection.h"
#include "Ray.h"
#include "Cube.h"

Cube::Cube(CMatrix4d const& transform)
	: CGeometryObjectImpl(transform)
	, m_minusX(1, 0, 0, 1, { { -1, 1, 1 }, { -1, -1, 1 }, { -1, -1, -1 }, { -1, 1, -1 } })
	, m_minusY(0, 1, 0, 1, { { 1, -1, 1 }, { -1, -1, 1 }, { -1, -1, -1 }, { 1, -1, -1 } })
	, m_minusZ(0, 0, 1, 1, { { 1, 1, -1 }, { 1, -1, -1 }, { -1, -1, -1 }, { -1, 1, -1 } })
	, m_plusX(1, 0, 0, -1, { { 1, 1, 1 }, { 1, -1, 1 }, { 1, -1, -1 }, { 1, 1, -1 } })
	, m_plusY(0, 1, 0, -1, { { 1, 1, 1 }, { -1, 1, 1 }, { -1, 1, -1 }, { 1, 1, -1 } })
	, m_plusZ(0, 0, 1, -1, { { 1, 1, 1 }, { 1, -1, 1 }, { -1, -1, 1 }, { -1, 1, 1 } })
{
}

bool Cube::Hit(CRay const& ray, CIntersection& intersection) const
{


	CRay invRay = Transform(ray, GetInverseTransform());

	double a = Dot(invRay.GetDirection(), invRay.GetDirection());
	double b = Dot(invRay.GetStart(), invRay.GetDirection());
	double c = Dot(invRay.GetStart(), invRay.GetStart()) - 3;
	double d = b * b - a * c;

	if (d < 0)
	{
		return false;
	}

	std::vector<CHitInfo> hits;

	CHitInfo hit;
	CIntersection tempIntersection;
	
	if (m_minusX.Hit(invRay, tempIntersection))
	{
		hit = tempIntersection.GetHit(0);
		hit = CHitInfo(
			hit.GetHitTime(), // Когда столкнулись
			*this, // С кем
			ray.GetPointAtTime(hit.GetHitTime()), hit.GetHitPointInObjectSpace(), // Точка соударения луча с поверхностью
			Normalize(GetNormalMatrix() * hit.GetNormal()), hit.GetNormalInObjectSpace() // Нормаль к поверхности в точке соударения
		); 
		hits.push_back(hit);
	}
	if (m_minusY.Hit(invRay, tempIntersection))
	{
		hit = tempIntersection.GetHit(0);
		hit = CHitInfo(
			hit.GetHitTime(), // Когда столкнулись
			*this, // С кем
			ray.GetPointAtTime(hit.GetHitTime()), hit.GetHitPointInObjectSpace(), // Точка соударения луча с поверхностью
			Normalize(GetNormalMatrix() * hit.GetNormal()), hit.GetNormalInObjectSpace() // Нормаль к поверхности в точке соударения
		); 
		hits.push_back(hit);
	}
	if (m_minusZ.Hit(invRay, tempIntersection))
	{
		hit = tempIntersection.GetHit(0);
		hit = CHitInfo(
			hit.GetHitTime(), // Когда столкнулись
			*this, // С кем
			ray.GetPointAtTime(hit.GetHitTime()), hit.GetHitPointInObjectSpace(), // Точка соударения луча с поверхностью
			Normalize(GetNormalMatrix() * hit.GetNormal()), hit.GetNormalInObjectSpace() // Нормаль к поверхности в точке соударения
		); 
		hits.push_back(hit);
	}
	if (m_plusX.Hit(invRay, tempIntersection))
	{
		hit = tempIntersection.GetHit(0);
		hit = CHitInfo(
			hit.GetHitTime(), // Когда столкнулись
			*this, // С кем
			ray.GetPointAtTime(hit.GetHitTime()), hit.GetHitPointInObjectSpace(), // Точка соударения луча с поверхностью
			Normalize(GetNormalMatrix() * hit.GetNormal()), hit.GetNormalInObjectSpace() // Нормаль к поверхности в точке соударения
		); 
		hits.push_back(hit);
	}
	if (m_plusY.Hit(invRay, tempIntersection))
	{
		hit = tempIntersection.GetHit(0);
		hit = CHitInfo(
			hit.GetHitTime(), // Когда столкнулись
			*this, // С кем
			ray.GetPointAtTime(hit.GetHitTime()), hit.GetHitPointInObjectSpace(), // Точка соударения луча с поверхностью
			Normalize(GetNormalMatrix() * hit.GetNormal()), hit.GetNormalInObjectSpace() // Нормаль к поверхности в точке соударения
		); 
		hits.push_back(hit);
	}
	if (m_plusZ.Hit(invRay, tempIntersection))
	{
		hit = tempIntersection.GetHit(0);
		hit = CHitInfo(
			hit.GetHitTime(), // Когда столкнулись
			*this, // С кем
			ray.GetPointAtTime(hit.GetHitTime()), hit.GetHitPointInObjectSpace(), // Точка соударения луча с поверхностью
			Normalize(GetNormalMatrix() * hit.GetNormal()), hit.GetNormalInObjectSpace() // Нормаль к поверхности в точке соударения
		); 
		hits.push_back(hit);
	}
	std::sort(hits.begin(), hits.end(), [](CHitInfo const& h1, CHitInfo const& h2) {
		return h1.GetHitTime() < h2.GetHitTime();
		}
	);


	for (int i = 0; i < hits.size(); i++)
	{
		intersection.AddHit(hits[i]);
	}

	return intersection.GetHitsCount() > 0;
}


