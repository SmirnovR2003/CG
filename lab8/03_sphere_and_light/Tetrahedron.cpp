#include "stdafx.h"
#include "Tetrahedron.h"
#include "Intersection.h"
#include "Ray.h"

Tetrahedron::Tetrahedron(CVector3d const& A, CVector3d const& B, CVector3d const& C, CVector3d const& D, CMatrix4d const& transform)
	: CGeometryObjectImpl(transform)
{
	m_trinagles.push_back({ A, B, C });
	m_trinagles.push_back({ B, C, D });
	m_trinagles.push_back({ A, B, D });
	m_trinagles.push_back({ A, C, D });

}

bool Tetrahedron::Hit(CRay const& ray, CIntersection& intersection) const
{
	CRay invRay = Transform(ray, GetInverseTransform());

	
	std::vector<CHitInfo> hits;

	CHitInfo hit;
	CIntersection tempIntersection;

	for (int i = 0; i < m_trinagles.size(); i++)
	{
		if (m_trinagles[i].Hit(invRay, tempIntersection))
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
	}

	std::sort(hits.begin(), hits.end(), [](CHitInfo const& h1, CHitInfo const& h2) {
		return h1.GetHitTime() < h2.GetHitTime();
	});

	for (int i = 0; i < hits.size(); i++)
	{
		intersection.AddHit(hits[i]);
	}

	return intersection.GetHitsCount() > 0;
}
