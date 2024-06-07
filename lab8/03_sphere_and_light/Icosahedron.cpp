#include "stdafx.h"
#include "Icosahedron.h"
#include "Ray.h"
#include "Intersection.h"

Icosahedron::Icosahedron(CMatrix4d const& transform)
{
	double _t = ((1.0 + sqrt(5.0)) / 2.0) + 0.001;

	double len = CVector3d(-1, ((1.0 + sqrt(5.0)) / 2.0), 0).GetLength();
	std::vector<CVector3d> vertices = {
		{ -1, _t, 0 },
		{ 1, _t, 0 },
		{ -1, -_t, 0 },
		{ 1, -_t, 0 },
		{ 0, -1, _t },
		{ 0, 1, _t },
		{ 0, -1, -_t },
		{ 0, 1, -_t },
		{ _t, 0, -1 },
		{ _t, 0, 1 },
		{ -_t, 0, -1 },
		{ -_t, 0, 1 }
	};

	for (auto& vert : vertices)
	{
		vert /= _t;
		//vert.Normalize();
	}

	std::vector<std::vector<int>> sides = {
		{ 11, 10, 2 },
		{ 3, 4, 2 },
		{ 8, 6, 7 },
		{ 7, 1, 8 },
		{ 1, 5, 9 },
		{ 0, 11, 5 },
		{ 3, 8, 9 },
		{ 9, 8, 1 },
		{ 10, 7, 6 },
		{ 0, 10, 11 },
		{ 0, 1, 7 },
		{ 3, 2, 6 },
		{ 0, 7, 10 },
		{ 3, 9, 4 },
		{ 6, 2, 10 },
		{ 3, 6, 8 },
		{ 0, 5, 1 },
		{ 5, 11, 4 },
		{ 2, 4, 11 },
		{ 4, 9, 5 }
	};

	for (int i = 0; i < sides.size(); i++)
	{
		m_trinagles.push_back({ vertices[sides[i][0]], vertices[sides[i][1]], vertices[sides[i][2]] });
	}


}

bool Icosahedron::Hit(CRay const& ray, CIntersection& intersection) const
{
	CRay invRay = Transform(ray, GetInverseTransform());
	double len = CVector3d(-1, ((1.0 + sqrt(5.0)) / 2.0), 0).GetLength();
	double a = Dot(invRay.GetDirection(), invRay.GetDirection());
	double b = Dot(invRay.GetStart(), invRay.GetDirection());
	double c = Dot(invRay.GetStart(), invRay.GetStart()) - len;
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
		//if (Dot(hits[i].GetHitPointInObjectSpace(), invRay.GetDirection()) >= 0)
			intersection.AddHit(hits[i]);
	}

	return intersection.GetHitsCount() > 0;
}
