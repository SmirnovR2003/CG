#include "stdafx.h"
#include "Octahedron.h"
#include "Ray.h"
#include "Intersection.h"
Octahedron::Octahedron(CMatrix4d const& transform)
	: CGeometryObjectImpl(transform)
{
	m_trinagles.push_back({{ 0, 0, 1 }, {0,1,0}, {1,0,0}});
	m_trinagles.push_back({{ 0, 0, 1 }, {0,1,0}, {-1,0,0}});
	m_trinagles.push_back({{ 0, 0, -1 }, {0,1,0}, {1,0,0}});
	m_trinagles.push_back({{ 0, 0, -1 }, {0,1,0}, {-1,0,0}});

	m_trinagles.push_back({{ 0, 0, 1 }, {0,-1,0}, {1,0,0}});
	m_trinagles.push_back({{ 0, 0, 1 }, {0,-1,0}, {-1,0,0}});
	m_trinagles.push_back({{ 0, 0, -1 }, {0,-1,0}, {1,0,0}});
	m_trinagles.push_back({{ 0, 0, -1 }, {0,-1,0}, {-1,0,0}});
}

bool Octahedron::Hit(CRay const& ray, CIntersection& intersection) const
{
	CRay invRay = Transform(ray, GetInverseTransform());

	double a = Dot(invRay.GetDirection(), invRay.GetDirection());
	double b = Dot(invRay.GetStart(), invRay.GetDirection());
	double c = Dot(invRay.GetStart(), invRay.GetStart()) - 1;
	double d = b * b - a * c;

	if (d < 0)
	{
		return false;
	}

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
